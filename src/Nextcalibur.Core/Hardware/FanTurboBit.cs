using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Microsoft.Win32.SafeHandles;

namespace Nextcalibur.Core.Hardware;

/// <summary>Why a read of the turbo bit gave no answer. Every one of these is a stop, never a retry.</summary>
public enum EcReadRefusal
{
    None,
    PawnIoMissingOrUntrusted,
    ModuleNotAsShipped,
    ModuleRefused,
    AnotherProgramHoldsTheEc,
    ControllerBusy,
    ControllerHoldsUnreadData,
    Timeout,
    PortCallFailed,
}

/// <summary>One reading of the controller byte that holds the turbo bit.</summary>
public readonly record struct TurboBitReading(byte? Value, EcReadRefusal Refusal)
{
    /// <summary>Bit 0 - what the BIOS calls FANM - or null when there was no reading.</summary>
    public bool? Turbo => Value is { } v ? (v & 1) != 0 : null;
}

/// <summary>
/// Reads the one byte of the embedded controller's memory that the BIOS's
/// own code names as the fan turbo's state: offset 0xC3, bit 0 (FANM). The
/// diagnostic the owner asked for on 11 October 2026, before anything is
/// ever written there: does that bit follow Fn+1?
///
/// Read only, and only that byte. There is no address parameter anywhere in
/// this class and no write command in it: the controller is sent the read
/// command (0x80) and the address, which is how every ACPI EC read is asked
/// for, and nothing else.
///
/// Through PawnIO, the signed driver this application already uses, with its
/// LpcACPIEC module - which allows the two ACPI EC ports, 0x62 and 0x66,
/// and no others. The module ships inside this application, and its SHA-256
/// is compared with the one release it was taken from before it is loaded;
/// the driver's own signature is checked before the device is opened.
///
/// Windows' ACPI driver uses the same two ports and shares no lock with
/// anybody. So nothing here pushes: the shared "Access_EC" mutex other
/// monitoring tools take is taken or the read is not made; a controller
/// that is mid-command, or holding a byte that belongs to Windows' driver,
/// is left alone; every wait has a short limit; a refused read is reported,
/// not repeated.
/// </summary>
public sealed class FanTurboBit : IDisposable
{
    /// <summary>The module's hash: PawnIO.Modules release 0.2.11, LpcACPIEC.bin.</summary>
    private const string ModuleSha256 = "c38fd116e7aff4d1fdb0a494e296be0a6708e5a22fc72f14587442fb7f8f7906";

    private const byte TurboByte = 0xC3;

    private const ushort DataPort = 0x62;
    private const ushort CommandPort = 0x66;
    private const byte ReadCommand = 0x80;
    private const byte OutputFull = 0x01;
    private const byte InputFull = 0x02;
    private static readonly TimeSpan WaitLimit = TimeSpan.FromMilliseconds(50);

    private const string DevicePath = @"\\?\GLOBALROOT\Device\PawnIO";
    private const uint DeviceType = 41394u << 16;
    private const uint IoctlLoadBinary = DeviceType | (0x821 << 2);
    private const uint IoctlExecute = DeviceType | (0x841 << 2);
    private const int FunctionNameLength = 32;

    private SafeFileHandle? _device;
    private EcReadRefusal _openRefusal = EcReadRefusal.None;

    /// <summary>One read of the turbo byte.</summary>
    public TurboBitReading Read()
    {
        if (!Open()) return new TurboBitReading(null, _openRefusal);

        using var mutex = new Mutex(false, @"Global\Access_EC");
        bool held;
        try { held = mutex.WaitOne(TimeSpan.FromMilliseconds(200)); }
        catch (AbandonedMutexException) { held = true; }
        if (!held) return new TurboBitReading(null, EcReadRefusal.AnotherProgramHoldsTheEc);

        try
        {
            if (In(CommandPort) is not { } status) return new TurboBitReading(null, EcReadRefusal.PortCallFailed);

            // A byte waiting in the output buffer is the answer to somebody
            // else's question - Windows' driver, most likely. Reading it would
            // take it from them. A command still being taken in is theirs too.
            if ((status & OutputFull) != 0) return new TurboBitReading(null, EcReadRefusal.ControllerHoldsUnreadData);
            if ((status & InputFull) != 0) return new TurboBitReading(null, EcReadRefusal.ControllerBusy);

            if (!Out(CommandPort, ReadCommand)) return new TurboBitReading(null, EcReadRefusal.PortCallFailed);
            if (!WaitFor(InputFull, clear: true)) return new TurboBitReading(null, EcReadRefusal.Timeout);
            if (!Out(DataPort, TurboByte)) return new TurboBitReading(null, EcReadRefusal.PortCallFailed);
            if (!WaitFor(OutputFull, clear: false)) return new TurboBitReading(null, EcReadRefusal.Timeout);

            return In(DataPort) is { } value
                ? new TurboBitReading(value, EcReadRefusal.None)
                : new TurboBitReading(null, EcReadRefusal.PortCallFailed);
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }

    private bool WaitFor(byte flag, bool clear)
    {
        var deadline = DateTime.UtcNow + WaitLimit;
        while (DateTime.UtcNow < deadline)
        {
            if (In(CommandPort) is not { } status) return false;
            if (((status & flag) == 0) == clear) return true;
            Thread.Sleep(1);
        }
        return false;
    }

    private bool Open()
    {
        if (_device is { IsInvalid: false, IsClosed: false }) return true;
        if (_openRefusal != EcReadRefusal.None) return false;

        if (!Security.Integrity.PawnIoIsTrusted()) { _openRefusal = EcReadRefusal.PawnIoMissingOrUntrusted; return false; }

        byte[] module;
        using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Nextcalibur.Core.Resources.PawnIO.LpcACPIEC.bin"))
        {
            if (stream is null) { _openRefusal = EcReadRefusal.ModuleNotAsShipped; return false; }
            module = new byte[stream.Length];
            stream.ReadExactly(module);
        }
        if (!string.Equals(Convert.ToHexString(SHA256.HashData(module)), ModuleSha256, StringComparison.OrdinalIgnoreCase))
        {
            _openRefusal = EcReadRefusal.ModuleNotAsShipped;
            return false;
        }

        var handle = CreateFile(DevicePath, 0xC0000000, 3, IntPtr.Zero, 3, 0x80, IntPtr.Zero);
        if (handle.IsInvalid) { handle.Dispose(); _openRefusal = EcReadRefusal.PawnIoMissingOrUntrusted; return false; }
        if (!DeviceIoControl(handle, IoctlLoadBinary, module, (uint)module.Length, null, 0, out _, IntPtr.Zero))
        {
            handle.Dispose();
            _openRefusal = EcReadRefusal.ModuleRefused;
            return false;
        }
        _device = handle;
        return true;
    }

    private byte? In(ushort port)
    {
        var output = new byte[sizeof(long)];
        return Execute("ioctl_pio_read", [port], output) ? output[0] : null;
    }

    private bool Out(ushort port, byte value) => Execute("ioctl_pio_write", [port, value], null);

    private bool Execute(string function, long[] arguments, byte[]? output)
    {
        if (_device is null) return false;
        var input = new byte[FunctionNameLength + arguments.Length * sizeof(long)];
        System.Text.Encoding.ASCII.GetBytes(function, 0, function.Length, input, 0);
        for (var i = 0; i < arguments.Length; i++)
            BitConverter.TryWriteBytes(input.AsSpan(FunctionNameLength + i * sizeof(long)), arguments[i]);
        return DeviceIoControl(_device, IoctlExecute, input, (uint)input.Length, output, (uint)(output?.Length ?? 0), out var returned, IntPtr.Zero)
               && (output is null || returned >= sizeof(long));
    }

    public void Dispose()
    {
        _device?.Dispose();
        _device = null;
    }

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern SafeFileHandle CreateFile(string name, uint access, uint share, IntPtr security,
        uint disposition, uint flags, IntPtr template);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool DeviceIoControl(SafeFileHandle device, uint code, byte[] input, uint inputSize,
        byte[]? output, uint outputSize, out uint returned, IntPtr overlapped);
}
