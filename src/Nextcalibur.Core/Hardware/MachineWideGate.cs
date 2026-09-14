using System.Threading;

namespace Nextcalibur.Core.Hardware;

/// <summary>
/// Keeps two copies of this application, in two Windows sessions, from
/// writing to the firmware mailbox at the same moment.
///
/// The mailbox is one buffer on one chip, and a command is a write followed
/// by a read: interleave two of those and each copy can read the other's
/// answer. Inside one process a plain lock settles it. Across sessions -
/// fast user switching, two administrators signed in at once - nothing does,
/// and both copies are elevated and equally entitled. So the transaction is
/// also held under a name the whole machine can see.
///
/// Three rules, learnt from the single-instance name earlier the same day:
/// this never blocks for long, never throws, and never refuses to do the
/// work. Anything running as any account can create the name first and
/// refuse everyone - so a name that cannot be had is noted once and the
/// work goes ahead, exactly as it did before this existed.
/// </summary>
internal sealed class MachineWideGate : IDisposable
{
    private const string Name = @"Global\Nextcalibur.FirmwareMailbox";

    /// <summary>
    /// A quarter of a second: long enough for the other copy's WMI call to
    /// finish, short enough that the window never notices. The readings run
    /// on the interface thread, and no correctness worth having is worth a
    /// window that stops answering - so under real contention both copies
    /// go ahead and the transaction's own retries sort the answer out, as
    /// they did before this gate existed.
    /// </summary>
    private static readonly TimeSpan Patience = TimeSpan.FromMilliseconds(250);

    private static readonly object InitLock = new();
    private static Mutex? _mutex;
    private static bool _unavailable;
    private readonly bool _held;

    private MachineWideGate(bool held) => _held = held;

    /// <summary>Takes the gate, or does not; either way the caller carries on.</summary>
    public static MachineWideGate Take()
    {
        if (_unavailable) return new MachineWideGate(false);

        try
        {
            Mutex mutex;
            lock (InitLock)
            {
                if (_unavailable) return new MachineWideGate(false);
                _mutex ??= new Mutex(false, Name);
                mutex = _mutex;
            }
            return new MachineWideGate(mutex.WaitOne(Patience));
        }
        catch (AbandonedMutexException)
        {
            // The other copy stopped mid-transaction. The gate is ours; what
            // it left behind is the firmware's business, and every command
            // is written whole before it is read.
            return new MachineWideGate(true);
        }
        catch (Exception ex) when (ex is UnauthorizedAccessException or WaitHandleCannotBeOpenedException or IOException or NotSupportedException)
        {
            // Held by something that will not share it, or a Windows that
            // will not give it out. Said once; then never asked again.
            _unavailable = true;
            Configuration.Log.Warn("mailbox", "No machine-wide gate for the firmware mailbox: " + ex.Message);
            return new MachineWideGate(false);
        }
    }

    public void Dispose()
    {
        if (!_held) return;
        try { _mutex?.ReleaseMutex(); }
        catch (Exception ex) when (ex is ApplicationException or ObjectDisposedException) { }
    }
}
