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
    private bool _baseTried;

    /// <summary>
    /// The processor's base frequency in MHz, read once from WMI.
    ///
    /// Asked exactly once, whether or not the answer arrives. Retrying on every
    /// reading would put a WMI query on a two-second timer for a number that
    /// cannot change, and on the window's thread each of those costs a kernel
    /// handle — precisely the fault this codebase already had once.
    ///
    /// The failure is caught broadly on purpose. WMI reports trouble as
    /// <see cref="ManagementException"/> in some conditions and as a raw
    /// <see cref="COMException"/> in others — the latter was observed on this
    /// machine — and a frequency this cannot supply is a blank reading, never a
    /// reason to take the application down.
    /// </summary>
    private uint BaseMhz
    {
        get
        {
            if (_baseMhz != 0 || _baseTried) return _baseMhz;
            _baseTried = true;

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
            catch (Exception ex) when (ex is ManagementException
                                          or COMException
                                          or TypeInitializationException
                                          or UnauthorizedAccessException)
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

/// <summary>What the graphics card is drawing, and how busy it is.</summary>
/// <param name="Watts">Board power draw.</param>
/// <param name="UtilisationPercent">How much of the last sampling period the GPU was busy.</param>
public readonly record struct GpuLoad(double Watts, int UtilisationPercent);

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

    [DllImport(Nvml, EntryPoint = "nvmlDeviceGetPowerUsage")]
    private static extern int GetPower(IntPtr device, out uint milliwatts);

    [DllImport(Nvml, EntryPoint = "nvmlDeviceGetUtilizationRates")]
    private static extern int GetUtilisation(IntPtr device, out NvmlUtilisation rates);

    [StructLayout(LayoutKind.Sequential)]
    private struct NvmlUtilisation
    {
        public uint Gpu;
        public uint Memory;
    }

    private IntPtr _device;
    private bool _ready;
    private bool _tried;
    private bool _disposed;

    /// <summary>
    /// Forgets the device handle so the next read starts from a fresh
    /// <c>nvmlInit</c>. Call it before and after the discrete adapter is
    /// switched off or on as a device.
    ///
    /// The handle NVML hands out is only good while the device it names stays
    /// where it was. Disable the adapter and it points at nothing;
    /// <c>nvmlDeviceGetClockInfo</c> on it does not return an error, it dies
    /// inside nvml.dll with an access violation, and .NET cannot catch that -
    /// the process simply ends. Nextcalibur crashed exactly that way the first
    /// time it switched its own card back on, on a two-second timer that was
    /// still reading the clock. The vendor's Control Center dies the same way
    /// four seconds after its own UMA button, and this project had already
    /// noted that with some satisfaction before doing it itself.
    ///
    /// So: no probing a handle that might be stale. Drop it, and let the next
    /// read re-enumerate. While the device is disabled, <c>nvmlInit</c> finds
    /// no device and the reads return null, which is the right answer.
    /// </summary>
    public void Reset()
    {
        if (_disposed) return;

        if (_ready)
        {
            try { Shutdown(); }
            catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException) { }
        }

        _device = IntPtr.Zero;
        _ready = false;
        _tried = false;
    }

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

    /// <summary>
    /// What the card is drawing and how busy it is, or null when unreadable.
    ///
    /// Together these say something neither says alone. A card at 16 watts under
    /// load is working; a card at 16 watts doing nothing is a machine left in a
    /// display mode that never lets it idle, which is the graphics twin of the
    /// power-overlay fault this project was started for.
    /// </summary>
    public GpuLoad? ReadLoad()
    {
        if (!EnsureReady()) return null;

        try
        {
            if (GetPower(_device, out var milliwatts) != Success) return null;
            if (GetUtilisation(_device, out var rates) != Success) return null;

            return new GpuLoad(milliwatts / 1000.0, (int)rates.Gpu);
        }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException)
        {
            _ready = false;
            return null;
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
