using System.Management;
using System.Runtime.InteropServices;

namespace Nextcalibur.Core.Hardware;

/// <summary>
/// Reads the processor's actual running frequency.
///
/// <c>Win32_Processor.CurrentClockSpeed</c> is not usable for this: on modern
/// parts it reports the base clock whatever the processor is doing. The
/// dependable figure is the performance counter "% Processor Performance",
/// which is the ratio of actual to base frequency and goes well above 100%
/// under turbo — measured at 180% on the machine this was written for, while
/// the WMI property still read 2300 MHz.
/// </summary>
public sealed class CpuClockReader : IDisposable
{
    private const string CounterQuery =
        "SELECT PercentProcessorPerformance FROM Win32_PerfFormattedData_Counters_ProcessorInformation " +
        "WHERE Name = '_Total'";

    private readonly ManagementObjectSearcher _searcher = new(CounterQuery);
    private uint _baseMhz;
    private bool _disposed;

    /// <summary>The processor's base frequency in MHz, read once.</summary>
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
                // Leaves _baseMhz at zero, which Read() reports as unknown.
            }

            return _baseMhz;
        }
    }

    /// <summary>Current frequency in GHz, or null when it cannot be read.</summary>
    public double? ReadGhz()
    {
        if (_disposed || BaseMhz == 0) return null;

        try
        {
            using var results = _searcher.Get();
            foreach (ManagementObject row in results)
            {
                using (row)
                {
                    if (row["PercentProcessorPerformance"] is not ulong percent) continue;
                    return BaseMhz * percent / 100.0 / 1000.0;
                }
            }
        }
        catch (ManagementException)
        {
            // A missed sample is not worth reporting; the caller shows nothing.
        }

        return null;
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _searcher.Dispose();
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
        catch (DllNotFoundException)
        {
            return false;
        }
        catch (EntryPointNotFoundException)
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
