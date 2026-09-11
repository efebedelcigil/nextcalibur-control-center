using System.Management;
using System.Runtime.InteropServices;

namespace Nextcalibur.Core.Hardware;

/// <summary>Command family (field <c>a0</c>).</summary>
public enum SmiFamily : ushort
{
    Read = 0xFA00,
    Write = 0xFB00,
}

/// <summary>Subsystem selector (field <c>a1</c>).</summary>
public enum SmiSubsystem : ushort
{
    Led = 0x0100,
    Thermal = 0x0200,

    /// <summary>
    /// Which chip drives the panel. Register 3 of the thermal family, which
    /// is evidently a wider "platform" family than its name suggests.
    ///
    /// Found by watching the mailbox while the vendor's Display Mode button
    /// was pressed and the restart accepted, 11 September 2026: the last thing
    /// written before the machine went down was <c>FB00/0203</c> with 2, and it
    /// came back Discrete; the way back it was 1, and it came back Hybrid.
    /// See PROTOCOL.md.
    /// </summary>
    DisplayMode = 0x0203,

    /// <summary>
    /// The firmware's fan and thermal profile, the half of a system mode
    /// that is not the Windows power plan. 0 Performance, 1 Gaming, 2 Office;
    /// see <see cref="ThermalProfile"/>.
    /// </summary>
    Profile = 0x0300,
}

/// <summary>
/// The 32-byte command/response block exchanged through the ACPI-WMI mailbox.
/// Layout is fixed by firmware; see docs/PROTOCOL.md §2.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct SmiCommand
{
    public ushort A0;
    public ushort A1;
    public uint A2;
    public uint A3;
    public uint A4;
    public uint A5;
    public uint A6;
    public uint Reserved0;
    public uint Reserved1;

    public const int SizeBytes = 32;

    public static SmiCommand For(SmiFamily family, SmiSubsystem subsystem) =>
        new() { A0 = (ushort)family, A1 = (ushort)subsystem };

    public byte[] ToBytes()
    {
        var bytes = new byte[SizeBytes];
        BitConverter.TryWriteBytes(bytes.AsSpan(0), A0);
        BitConverter.TryWriteBytes(bytes.AsSpan(2), A1);
        BitConverter.TryWriteBytes(bytes.AsSpan(4), A2);
        BitConverter.TryWriteBytes(bytes.AsSpan(8), A3);
        BitConverter.TryWriteBytes(bytes.AsSpan(12), A4);
        BitConverter.TryWriteBytes(bytes.AsSpan(16), A5);
        BitConverter.TryWriteBytes(bytes.AsSpan(20), A6);
        BitConverter.TryWriteBytes(bytes.AsSpan(24), Reserved0);
        BitConverter.TryWriteBytes(bytes.AsSpan(28), Reserved1);
        return bytes;
    }

    public static SmiCommand FromBytes(byte[] b)
    {
        if (b is null || b.Length < SizeBytes)
            throw new ArgumentException($"Expected {SizeBytes} bytes.", nameof(b));

        return new SmiCommand
        {
            A0 = BitConverter.ToUInt16(b, 0),
            A1 = BitConverter.ToUInt16(b, 2),
            A2 = BitConverter.ToUInt32(b, 4),
            A3 = BitConverter.ToUInt32(b, 8),
            A4 = BitConverter.ToUInt32(b, 12),
            A5 = BitConverter.ToUInt32(b, 16),
            A6 = BitConverter.ToUInt32(b, 20),
            Reserved0 = BitConverter.ToUInt32(b, 24),
            Reserved1 = BitConverter.ToUInt32(b, 28),
        };
    }

    public override string ToString() =>
        $"a0=0x{A0:X4} a1=0x{A1:X4} a2={A2} a3={A3} a4={A4} a5={A5} a6={A6}";
}

/// <summary>Thrown when the mailbox is missing or unusable.</summary>
public sealed class EcMailboxUnavailableException(string message, Exception? inner = null)
    : Exception(message, inner);

/// <summary>
/// Access to the firmware mailbox exposed as <c>root\wmi:RW_GMWMI</c>.
///
/// The mailbox is shared, mutable, machine-wide state. Anything else talking to
/// it — notably the vendor's own software — writes to the same buffer, so reads
/// can catch a partially written response. Every read here is validated and
/// retried; see docs/PROTOCOL.md §6.
/// </summary>
public sealed class EcMailbox : IDisposable
{
    private const string Scope = @"root\wmi";
    private const string ClassName = "RW_GMWMI";
    private const string BufferProperty = "BufferBytes";

    private readonly ManagementScope _scope;

    /// <summary>
    /// Serialises whole command/response pairs.
    ///
    /// The mailbox is one buffer. A write followed by a read is only meaningful
    /// if nothing else writes in between, and callers now reach this from more
    /// than one thread: sensor reads run on the thread pool — <c>System.Management</c>
    /// leaks a kernel handle per call when driven from a single-threaded
    /// apartment — while lighting changes still arrive from the user-interface
    /// thread. Without this the two would interleave and each would read the
    /// other's answer.
    /// </summary>
    private readonly object _gate = new();

    // The mailbox is a single fixed WMI instance. Re-running a WQL query for
    // every read would cost far more than refreshing the object we already
    // hold, and this type is polled continuously.
    private ManagementObject? _instance;
    private bool _disposed;

    public EcMailbox()
    {
        _scope = new ManagementScope(Scope);
        try
        {
            _scope.Connect();
        }
        catch (Exception ex)
        {
            throw new EcMailboxUnavailableException(
                $@"Could not connect to {Scope}. This machine may not expose the interface.", ex);
        }
    }

    /// <summary>True when the interface is present and reports itself active.</summary>
    public static bool IsSupported()
    {
        try
        {
            using var searcher = new ManagementObjectSearcher(
                new ManagementScope(Scope), new ObjectQuery($"SELECT * FROM {ClassName}"));
            using var results = searcher.Get();
            foreach (ManagementObject mo in results)
            {
                using (mo)
                {
                    return mo[BufferProperty] is byte[];
                }
            }
        }
        catch
        {
            // Absent interface is a normal answer, not an error.
        }
        return false;
    }

    private ManagementObject Instance()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        if (_instance is not null) return _instance;

        using var searcher = new ManagementObjectSearcher(
            _scope, new ObjectQuery($"SELECT * FROM {ClassName}"));
        using var results = searcher.Get();
        foreach (ManagementObject mo in results)
        {
            _instance = mo;
            return _instance;
        }

        throw new EcMailboxUnavailableException($"No {ClassName} instance found.");
    }

    /// <summary>Drops the cached instance so the next call re-binds it.</summary>
    private void Invalidate()
    {
        _instance?.Dispose();
        _instance = null;
    }

    /// <summary>Reads the raw mailbox contents without interpreting them.</summary>
    public byte[] ReadRaw()
    {
        lock (_gate)
        {
            var mo = Instance();
            mo.Get();   // refresh in place - no new query
            if (mo[BufferProperty] is not byte[] buffer || buffer.Length < SmiCommand.SizeBytes)
                throw new EcMailboxUnavailableException($"{BufferProperty} was not a {SmiCommand.SizeBytes}-byte array.");
            return buffer;
        }
    }

    /// <summary>Writes a command into the mailbox.</summary>
    public void Write(SmiCommand command)
    {
        lock (_gate)
        {
            var mo = Instance();
            mo[BufferProperty] = command.ToBytes();
            mo.Put();
        }
    }

    /// <summary>
    /// Sends a command and returns the response, retrying while the response
    /// looks torn or stale.
    /// </summary>
    /// <param name="command">The command to send.</param>
    /// <param name="isValid">
    /// Decides whether a response is complete. Firmware fills the payload fields
    /// after echoing the header, so a caller that knows which fields it needs is
    /// the only thing that can tell a finished response from a half-written one.
    /// </param>
    /// <param name="attempts">How many times to send before giving up.</param>
    /// <param name="delayMs">Pause between attempts, in milliseconds.</param>
    public SmiCommand Execute(
        SmiCommand command,
        Func<SmiCommand, bool> isValid,
        int attempts = 8,
        int delayMs = 40)
    {
        ArgumentNullException.ThrowIfNull(isValid);

        // Held across the retries, not just around each call: a response only
        // belongs to us if nothing wrote between our write and our read.
        lock (_gate)
        {
            Exception? last = null;
            for (var attempt = 0; attempt < attempts; attempt++)
            {
                try
                {
                    Write(command);
                    var response = SmiCommand.FromBytes(ReadRaw());

                    // The header must come back as sent; otherwise another writer
                    // raced us for the mailbox and this response belongs to them.
                    if (response.A0 == command.A0 && response.A1 == command.A1 && isValid(response))
                        return response;
                }
                catch (ManagementException ex)
                {
                    last = ex;
                    Invalidate();   // stale handle - rebind on the next attempt
                }
                catch (UnauthorizedAccessException ex)
                {
                    throw new EcMailboxUnavailableException(
                        "Access denied writing to the mailbox. Try running elevated.", ex);
                }

                Thread.Sleep(delayMs);
            }

            // Contention was the only explanation this ever offered, and for a
            // machine without the vendor's software installed it is the wrong
            // one: no reply comes back because the account is not allowed to
            // use the data block, and telling somebody to close software they
            // do not have sends them nowhere.
            var reason = MailboxAccess.Check() == MailboxAvailability.AccessNotGranted
                ? "This account is not allowed to use the firmware interface. " +
                  "Run 'nextcalibur access --grant' from an elevated prompt, once."
                : "Another application may be using the mailbox - close the vendor Control Center and retry.";

            throw new EcMailboxUnavailableException(
                $"No valid response after {attempts} attempts. " + reason, last);
        }
    }

    /// <summary>
    /// Holds the mailbox until the returned object is disposed.
    ///
    /// <see cref="Execute"/> already keeps one command and its response
    /// together, but some operations are a sequence — writing the three lighting
    /// zones, for instance — and the hardware drops the sequence if anything
    /// else writes partway through. Stopping the sampling timer is no longer
    /// enough to prevent that, because a sample already in flight runs on
    /// another thread.
    /// </summary>
    public IDisposable Hold()
    {
        Monitor.Enter(_gate);
        return new Held(_gate);
    }

    private sealed class Held(object gate) : IDisposable
    {
        private object? _gate = gate;

        public void Dispose()
        {
            var taken = Interlocked.Exchange(ref _gate, null);
            if (taken is not null) Monitor.Exit(taken);
        }
    }

    /// <summary>
    /// Reads the mailbox without writing to it, returning the last response left
    /// there by whoever wrote last. Useful for passive observation only: the
    /// contents may be arbitrarily old.
    /// </summary>
    public SmiCommand PeekLastResponse() => SmiCommand.FromBytes(ReadRaw());

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        lock (_gate) Invalidate();
    }
}
