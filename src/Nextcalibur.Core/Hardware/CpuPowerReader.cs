using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace Nextcalibur.Core.Hardware;

/// <summary>
/// The processor package's power draw, through PawnIO.
///
/// Windows gives user-mode code no way to read the CPU's energy counters;
/// they are model-specific registers, and reading one takes kernel code.
/// PawnIO is that kernel code, done carefully: a Microsoft-signed driver
/// that runs only modules signed by its author, each module exposing a few
/// narrow calls. The <c>IntelMSR</c> module offers <c>ioctl_read_msr</c>;
/// this reads two registers through it - the RAPL unit register and the
/// package energy counter - and turns the counter's growth into watts.
///
/// Set by the owner on 12 September 2026, with the rule that the number is
/// optional: PawnIO absent, the module refused, an AMD machine, a read that
/// fails - all of it reads as "no number", never as an error and never as a
/// guess. The module binary ships with the application (LGPL-2.1, its
/// licence beside it); the driver is the person's to install.
/// </summary>
public sealed class CpuPowerReader : IDisposable
{
    private const uint MsrRaplPowerUnit = 0x606;
    private const uint MsrPkgEnergyStatus = 0x611;

    // PawnIO's device and control codes, as its own client library defines them.
    private const string DevicePath = @"\\?\GLOBALROOT\Device\PawnIO";
    private const uint DeviceType = 41394u << 16;
    private const uint IoctlLoadBinary = DeviceType | (0x821 << 2);
    private const uint IoctlExecute = DeviceType | (0x841 << 2);
    private const int FunctionNameLength = 32;

    private SafeFileHandle? _device;
    private double _joulesPerUnit;
    private uint _lastEnergy;
    private long _lastTicks;
    private bool _tried;

    /// <summary>True when the PawnIO driver is installed, whether or not it answers.</summary>
    public static bool DriverInstalled
    {
        get
        {
            try
            {
                using var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\PawnIO");
                return key is not null;
            }
            catch (Exception ex) when (ex is System.Security.SecurityException or UnauthorizedAccessException)
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Package power in watts over the interval since the previous call, or
    /// null when there is no number to give. The first call primes the
    /// counter and returns null; calls closer than a quarter second apart
    /// return null too, because the counter's resolution would make the
    /// division noise.
    /// </summary>
    public double? ReadWatts()
    {
        if (!Open()) return null;

        if (!ReadMsr(MsrPkgEnergyStatus, out var raw)) { Close(); return null; }
        var energy = unchecked((uint)raw);
        var now = Stopwatch.GetTimestamp();

        if (_lastTicks == 0)
        {
            _lastEnergy = energy;
            _lastTicks = now;
            return null;
        }

        var seconds = (now - _lastTicks) / (double)Stopwatch.Frequency;
        if (seconds < 0.25) return null;

        var joules = unchecked(energy - _lastEnergy) * _joulesPerUnit;
        _lastEnergy = energy;
        _lastTicks = now;

        var watts = joules / seconds;
        return watts is >= 0 and < 1000 ? watts : null;
    }

    /// <summary>
    /// The module is Intel's MSR map; on any other processor the registers
    /// mean something else, and a read would be a guess dressed as a number.
    /// AMD has its own PawnIO module, and its own day.
    /// </summary>
    private static bool IsIntel()
    {
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor\0");
            return key?.GetValue("VendorIdentifier") is string vendor
                && vendor.Equals("GenuineIntel", StringComparison.OrdinalIgnoreCase);
        }
        catch (Exception ex) when (ex is System.Security.SecurityException or UnauthorizedAccessException or IOException)
        {
            return false;
        }
    }

    private bool Open()
    {
        if (_device is { IsInvalid: false, IsClosed: false }) return true;
        if (_tried) return false;
        _tried = true;
        if (!IsIntel()) return false;

        try
        {
            var handle = CreateFile(DevicePath, 0xC0000000 /* GENERIC_READ|WRITE */, 3 /* share r/w */,
                IntPtr.Zero, 3 /* OPEN_EXISTING */, 0x80 /* FILE_ATTRIBUTE_NORMAL */, IntPtr.Zero);
            if (handle.IsInvalid) return false;

            using var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("Nextcalibur.Core.Resources.PawnIO.IntelMSR.bin");
            if (stream is null) { handle.Dispose(); return false; }
            var module = new byte[stream.Length];
            stream.ReadExactly(module);

            if (!DeviceIoControl(handle, IoctlLoadBinary, module, (uint)module.Length, null, 0, out _, IntPtr.Zero))
            {
                handle.Dispose();
                return false;
            }

            _device = handle;

            // Energy units: bits 8-12 of the unit register, as 1/2^n joules.
            if (!ReadMsr(MsrRaplPowerUnit, out var unit)) { Close(); return false; }
            var shift = (int)((unit >> 8) & 0x1F);
            _joulesPerUnit = 1.0 / (1L << shift);
            return true;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or DllNotFoundException)
        {
            return false;
        }
    }

    private bool ReadMsr(uint index, out ulong value)
    {
        value = 0;
        if (_device is null) return false;

        var input = new byte[FunctionNameLength + sizeof(long)];
        var name = System.Text.Encoding.ASCII.GetBytes("ioctl_read_msr");
        Buffer.BlockCopy(name, 0, input, 0, name.Length);
        BitConverter.TryWriteBytes(input.AsSpan(FunctionNameLength), (long)index);

        var output = new byte[sizeof(long)];
        if (!DeviceIoControl(_device, IoctlExecute, input, (uint)input.Length, output, (uint)output.Length, out var read, IntPtr.Zero)
            || read < sizeof(long))
            return false;

        value = BitConverter.ToUInt64(output, 0);
        return true;
    }

    private void Close()
    {
        _device?.Dispose();
        _device = null;
        _lastTicks = 0;
    }

    public void Dispose() => Close();

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern SafeFileHandle CreateFile(string name, uint access, uint share, IntPtr security,
        uint disposition, uint flags, IntPtr template);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool DeviceIoControl(SafeFileHandle device, uint code, byte[] input, uint inputSize,
        byte[]? output, uint outputSize, out uint returned, IntPtr overlapped);
}
