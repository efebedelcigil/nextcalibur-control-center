using System.Management;
using System.Runtime.InteropServices;

namespace Nextcalibur.Core.Hardware;

/// <summary>
/// Reads the processor's actual running frequency.
///
/// <c>Win32_Processor.CurrentClockSpeed</c> is not usable for this: on modern
/// parts it reports the base clock whatever the processor is doing. The
/// dependable figure is the counter "% Processor Performance", the ratio of
/// actual to base frequency, which goes well above 100% under turbo — measured
/// at 180% on the machine this was written for, while the WMI property still
/// read 2300 MHz.
///
/// The counter is read through PDH rather than WMI. The equivalent WMI query
/// was measured at <b>275 ms</b> on this machine; called every couple of
/// seconds on the UI thread that is roughly a seventh of a core, permanently,
/// for one number. PDH answers the same question in well under a millisecond.
///
/// <c>PdhAddEnglishCounter</c> is deliberate: counter paths are localised, and
/// the English form is the only one that works on every machine.
/// </summary>
public sealed class CpuClockReader : IDisposable
{
    private const string CounterPath = @"\Processor Information(_Total)\% Processor Performance";
    private const uint PdhFmtDouble = 0x00000200;
    private const uint Success = 0;

    [DllImport("pdh.dll")]
    private static extern uint PdhOpenQueryW(string? dataSource, IntPtr userData, out IntPtr query);

    [DllImport("pdh.dll", CharSet = CharSet.Unicode)]
    private static extern uint PdhAddEnglishCounterW(IntPtr query, string path, IntPtr userData, out IntPtr counter);

    [DllImport("pdh.dll")]
    private static extern uint PdhCollectQueryData(IntPtr query);

    [DllImport("pdh.dll")]
    private static extern uint PdhGetFormattedCounterValue(
        IntPtr counter, uint format, out uint type, out PdhFmtCounterValue value);

    [DllImport("pdh.dll")]
    private static extern uint PdhCloseQuery(IntPtr query);

    [StructLayout(LayoutKind.Explicit)]
    private struct PdhFmtCounterValue
    {
        [FieldOffset(0)] public uint Status;
        [FieldOffset(8)] public double DoubleValue;
    }

    private IntPtr _query;
    private IntPtr _counter;
    private bool _ready;
    private bool _tried;
    private bool _disposed;
    private uint _baseMhz;

    /// <summary>The processor's base frequency in MHz, read once from WMI.</summary>
    private uint BaseMhz
    {
        get
        {
            if (_baseMhz != 0) return _baseMhz;

            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT MaxClockSpeed FROM Win32_Processor");
                foreach (ManagementObject cpu in searcher.Get())
                {
                    using (cpu)
                    {
                        if (cpu["MaxClockSpeed"] is uint mhz && mhz > 0) return _baseMhz = mhz;
                    }
                }
            }
            catch (ManagementException)
            {
                // Leaves the value at zero, which ReadGhz reports as unknown.
            }

            return _baseMhz;
        }
    }

    private bool EnsureReady()
    {
        if (_ready) return true;
        if (_tried || _disposed) return false;

        _tried = true;
        try
        {
            if (PdhOpenQueryW(null, IntPtr.Zero, out _query) != Success) return false;

            if (PdhAddEnglishCounterW(_query, CounterPath, IntPtr.Zero, out _counter) != Success)
            {
                PdhCloseQuery(_query);
                _query = IntPtr.Zero;
                return false;
            }

            // This counter is a rate, so the first collection only establishes a
            // baseline. The reading that follows is the first usable one.
            PdhCollectQueryData(_query);
            return _ready = true;
        }
        catch (DllNotFoundException)
        {
            return false;
        }
    }

    /// <summary>Current frequency in GHz, or null when it cannot be read.</summary>
    public double? ReadGhz()
    {
        if (!EnsureReady() || BaseMhz == 0) return null;

        if (PdhCollectQueryData(_query) != Success) return null;
        if (PdhGetFormattedCounterValue(_counter, PdhFmtDouble, out _, out var value) != Success) return null;

        return BaseMhz * value.DoubleValue / 100.0 / 1000.0;
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        if (_query == IntPtr.Zero) return;
        PdhCloseQuery(_query);
        _query = IntPtr.Zero;
        _ready = false;
    }
}

/// <summary>
/// Reads the graphics card's clock through NVML, the library NVIDIA ships with
/// its driver. The vendor software uses the same one.
///
/// Everything here fails quietly: a machine without an NVIDIA card, or with a
/// driver too old to expose NVML, simply reports nothing.
/// </summary>
public sealed class GpuClockReader : IDisposable
{
    private const string Nvml = "nvml.dll";
    private const int Success = 0;
    private const int ClockGraphics = 0;

    [DllImport(Nvml, EntryPoint = "nvmlInit_v2")]
    private static extern int Init();

    [DllImport(Nvml, EntryPoint = "nvmlShutdown")]
    private static extern int Shutdown();

    [DllImport(Nvml, EntryPoint = "nvmlDeviceGetHandleByIndex_v2")]
    private static extern int GetHandle(uint index, out IntPtr device);

    [DllImport(Nvml, EntryPoint = "nvmlDeviceGetClockInfo")]
    private static extern int GetClock(IntPtr device, int type, out uint mhz);

    private IntPtr _device;
    private bool _ready;
    private bool _tried;
    private bool _disposed;

    private bool EnsureReady()
    {
        if (_ready) return true;
        if (_tried || _disposed) return false;

        _tried = true;
        try
        {
            if (Init() != Success) return false;
            if (GetHandle(0, out _device) != Success)
            {
                Shutdown();
                return false;
            }
            return _ready = true;
        }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException)
        {
            return false;
        }
    }

    /// <summary>Current graphics clock in GHz, or null when unavailable.</summary>
    public double? ReadGhz()
    {
        if (!EnsureReady()) return null;

        try
        {
            return GetClock(_device, ClockGraphics, out var mhz) == Success ? mhz / 1000.0 : null;
        }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException)
        {
            _ready = false;
            return null;
        }
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        if (!_ready) return;
        try
        {
            Shutdown();
        }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException)
        {
            // Nothing useful to do while shutting down.
        }
        _ready = false;
    }
}
