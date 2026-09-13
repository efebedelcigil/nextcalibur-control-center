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

    [DllImport(Nvml, EntryPoint = "nvmlDeviceGetTemperature")]
    private static extern int GetTemperature(IntPtr device, int sensor, out uint celsius);

    [DllImport(Nvml, EntryPoint = "nvmlDeviceGetUtilizationRates")]
    private static extern int GetUtilisation(IntPtr device, out NvmlUtilisation rates);

    [DllImport(Nvml, EntryPoint = "nvmlDeviceGetPerformanceState")]
    private static extern int GetPerformanceState(IntPtr device, out int pState);

    [DllImport(Nvml, EntryPoint = "nvmlDeviceGetDisplayActive")]
    private static extern int GetDisplayActive(IntPtr device, out int isActive);

    [StructLayout(LayoutKind.Sequential)]
    private struct NvmlUtilisation
    {
        public uint Gpu;
        public uint Memory;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct NvmlProcessInfo
    {
        public uint Pid;
        public ulong UsedGpuMemory;
    }

    [DllImport(Nvml, EntryPoint = "nvmlDeviceGetGraphicsRunningProcesses")]
    private static extern int GetGraphicsProcesses(IntPtr device, ref uint infoCount, [In, Out] NvmlProcessInfo[]? infos);

    private IntPtr _device;
    private bool _ready;
    private bool _tried;
    private bool _disposed;
    private DateTime _awakeSince = DateTime.MinValue;
    private DateTime _lastSampleAt = DateTime.MinValue;

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
        _awakeSince = DateTime.MinValue;
        _lastSampleAt = DateTime.MinValue;
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

    /// <summary>
    /// The card's temperature in whole degrees, or null when there is no
    /// card answering. NVML costs a library call and no interrupt, which is
    /// the point: while the window is away this and the processor's own
    /// register decide whether the firmware needs asking at all.
    /// </summary>
    public int? ReadTemperatureC()
    {
        if (!EnsureReady()) return null;

        try
        {
            return GetTemperature(_device, 0 /* NVML_TEMPERATURE_GPU */, out var celsius) == Success && celsius is > 0 and < 130
                ? (int)celsius
                : null;
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

    /// <summary>Resets the unbroken awake timer.</summary>
    public void ResetAwakeFault()
    {
        _awakeSince = DateTime.MinValue;
        _lastSampleAt = DateTime.MinValue;
    }

    /// <summary>
    /// Pure arithmetic check: the card in P0 or P1, utilisation under 5 %,
    /// power over 10 W, and no display attached.
    /// </summary>
    public static bool IsGpuAwakeFaultCondition(int performanceState, int utilisationPercent, double watts, bool displayActive)
    {
        return (performanceState is 0 or 1)
            && utilisationPercent < 5
            && watts > 10.0
            && !displayActive;
    }

    /// <summary>
    /// Reads distinct process names holding graphics contexts on the discrete adapter through NVML.
    /// </summary>
    public IReadOnlyList<string> ReadGraphicsRunningProcessNames()
    {
        if (!EnsureReady()) return [];

        try
        {
            uint count = 64;
            var infos = new NvmlProcessInfo[count];
            var res = GetGraphicsProcesses(_device, ref count, infos);
            if (res == 6 && count > 64) // NVML_ERROR_INSUFFICIENT_SIZE
            {
                infos = new NvmlProcessInfo[count];
                res = GetGraphicsProcesses(_device, ref count, infos);
            }
            if (res != Success) return [];

            var names = new List<string>();
            var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            for (var i = 0; i < count && i < infos.Length; i++)
            {
                var pid = (int)infos[i].Pid;
                try
                {
                    using var p = System.Diagnostics.Process.GetProcessById(pid);
                    var name = p.ProcessName;
                    if (!string.IsNullOrWhiteSpace(name) && seen.Add(name))
                    {
                        names.Add(name);
                    }
                }
                catch
                {
                    // Process may have exited or access is denied.
                }
            }
            return names;
        }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException)
        {
            _ready = false;
            return [];
        }
    }

    /// <summary>
    /// Evaluates the awake fault state given the current reading and timestamp.
    /// Returns the active fault if the condition has held unbroken for at least the required window (10 minutes).
    /// </summary>
    public GpuAwakeFault? EvaluateAwakeFault(
        bool isFaultCondition,
        DateTime now,
        Func<IReadOnlyList<string>> readProcesses,
        double watts,
        int pState,
        int utilisation,
        TimeSpan? requiredWindow = null)
    {
        var window = requiredWindow ?? TimeSpan.FromMinutes(10);
        if (!isFaultCondition)
        {
            _awakeSince = DateTime.MinValue;
            return null;
        }

        if (_awakeSince == DateTime.MinValue)
        {
            _awakeSince = now;
            return null;
        }

        var duration = now - _awakeSince;
        if (duration < window)
        {
            return null;
        }

        var procs = readProcesses();
        return new GpuAwakeFault(pState, watts, utilisation, procs, duration);
    }

    /// <summary>
    /// Detects the discrete GPU sitting at full clocks (P0/P1) with under 5% utilisation,
    /// power over 10 W, and no display attached, for 10 minutes unbroken.
    /// Returns the fault details when detected, or null otherwise.
    /// </summary>
    public GpuAwakeFault? CheckAwakeFault(DateTime? utcNow = null)
    {
        if (!EnsureReady())
        {
            _awakeSince = DateTime.MinValue;
            return null;
        }

        try
        {
            if (GetPerformanceState(_device, out var pState) != Success)
            {
                _awakeSince = DateTime.MinValue;
                return null;
            }
            if (GetUtilisation(_device, out var rates) != Success)
            {
                _awakeSince = DateTime.MinValue;
                return null;
            }
            if (GetPower(_device, out var milliwatts) != Success)
            {
                _awakeSince = DateTime.MinValue;
                return null;
            }
            if (GetDisplayActive(_device, out var dispActive) != Success)
            {
                _awakeSince = DateTime.MinValue;
                return null;
            }

            var watts = milliwatts / 1000.0;
            var isFault = IsGpuAwakeFaultCondition(pState, (int)rates.Gpu, watts, dispActive != 0);
            var now = utcNow ?? DateTime.UtcNow;

            if (_lastSampleAt != DateTime.MinValue && (now - _lastSampleAt) > TimeSpan.FromMinutes(2))
            {
                _awakeSince = DateTime.MinValue;
            }
            _lastSampleAt = now;

            return EvaluateAwakeFault(isFault, now, ReadGraphicsRunningProcessNames, watts, pState, (int)rates.Gpu);
        }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException)
        {
            _ready = false;
            _awakeSince = DateTime.MinValue;
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

/// <summary>
/// Evidence of a discrete graphics card held awake at full clocks with nothing to draw.
/// </summary>
public sealed record GpuAwakeFault(
    int PerformanceState,
    double Watts,
    int UtilisationPercent,
    IReadOnlyList<string> HoldingProcesses,
    TimeSpan Duration)
{
    /// <summary>Describes what is happening in the current language.</summary>
    public string Describe()
    {
        var roundedWatts = (int)Math.Round(Watts);
        if (HoldingProcesses.Count > 0)
        {
            var procs = string.Join(", ", HoldingProcesses);
            return Words.Get("S.Core.Fault.GpuWake",
                "The graphics card is awake at full clocks with nothing to draw, costing about {0} W. Holding processes: {1}.",
                roundedWatts, procs);
        }

        return Words.Get("S.Core.Fault.GpuWakeNoProcesses",
            "The graphics card is awake at full clocks with nothing to draw, costing about {0} W.",
            roundedWatts);
    }
}

