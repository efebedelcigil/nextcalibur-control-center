using System.Runtime.InteropServices;
using System.Text;

namespace Nextcalibur.Core.Hardware;

/// <summary>One memory module as the firmware lists it.</summary>
public sealed record MemoryModule(string? Locator, int SizeMb, int SpeedMts, int ConfiguredSpeedMts, string? Manufacturer, string? PartNumber);

/// <summary>
/// What the firmware says about the machine - its SMBIOS tables, read at
/// every start (the owner's ask of 15 October 2026: read the BIOS's own
/// account first, so nothing later rests on a guess). Read through Windows'
/// GetSystemFirmwareTable: a copy Windows took at boot, no interrupt, no
/// write, nothing the firmware notices. Anything absent stays null.
///
/// Used for: the machine's identity beside the support verdict, the embedded
/// controller's firmware version (a change is worth knowing about - the
/// protocol was read from one), the processor's top clock for the ceiling
/// test's steps, and the battery's design capacity where WMI gives none.
/// </summary>
public sealed record FirmwareInventory
{
    public string? BiosVendor { get; init; }
    public string? BiosVersion { get; init; }
    public string? BiosDate { get; init; }
    /// <summary>The system BIOS's own release number, major.minor, when the firmware fills it.</summary>
    public string? BiosRelease { get; init; }
    /// <summary>The embedded controller's firmware release, major.minor, when the firmware fills it.</summary>
    public string? EcRelease { get; init; }

    public string? Manufacturer { get; init; }
    public string? Product { get; init; }
    public string? ProductVersion { get; init; }
    public string? Sku { get; init; }
    public string? Family { get; init; }
    public string? BoardManufacturer { get; init; }
    public string? BoardProduct { get; init; }

    public string? ProcessorName { get; init; }
    /// <summary>The processor's highest clock as the firmware lists it, in MHz.</summary>
    public int? ProcessorMaxMhz { get; init; }
    public int? ProcessorCores { get; init; }
    public int? ProcessorThreads { get; init; }

    public IReadOnlyList<MemoryModule> Memory { get; init; } = [];

    public string? BatteryName { get; init; }
    public string? BatteryChemistry { get; init; }
    /// <summary>The battery's design capacity in mWh, from the firmware's portable-battery table.</summary>
    public int? BatteryDesignMwh { get; init; }

    /// <summary>Reads the tables now. Empty (all null) when Windows will not give them.</summary>
    public static FirmwareInventory Read()
    {
        try
        {
            var size = GetSystemFirmwareTable(Rsmb, 0, IntPtr.Zero, 0);
            if (size == 0) return new FirmwareInventory();
            var buffer = Marshal.AllocHGlobal((int)size);
            try
            {
                if (GetSystemFirmwareTable(Rsmb, 0, buffer, size) != size) return new FirmwareInventory();
                var bytes = new byte[size];
                Marshal.Copy(buffer, bytes, 0, (int)size);
                return Parse(bytes);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException)
        {
            return new FirmwareInventory();
        }
    }

    /// <summary>
    /// Parses Windows' RawSMBIOSData: an 8-byte header, then the structures -
    /// type, length, handle, the formatted part, then its strings, each ended
    /// by a zero and the set by a second zero.
    /// </summary>
    public static FirmwareInventory Parse(byte[] raw)
    {
        if (raw.Length < 8) return new FirmwareInventory();
        var length = BitConverter.ToInt32(raw, 4);
        var end = Math.Min(raw.Length, 8 + length);
        var inv = new FirmwareInventory();
        var memory = new List<MemoryModule>();

        for (var at = 8; at + 4 <= end;)
        {
            var type = raw[at];
            var formatted = raw[at + 1];
            if (formatted < 4 || at + formatted > end) break;
            var strings = ReadStrings(raw, at + formatted, end, out var next);
            byte B(int offset) => offset < formatted ? raw[at + offset] : (byte)0;
            int W(int offset) => offset + 1 < formatted ? BitConverter.ToUInt16(raw, at + offset) : 0;
            string? S(int offset) => offset < formatted && raw[at + offset] is var i and > 0 && i <= strings.Count ? strings[i - 1] : null;

            switch (type)
            {
                case 0:
                    inv = inv with
                    {
                        BiosVendor = S(0x04), BiosVersion = S(0x05), BiosDate = S(0x08),
                        BiosRelease = B(0x14) != 0xFF && formatted > 0x15 ? $"{B(0x14)}.{B(0x15)}" : null,
                        EcRelease = B(0x16) != 0xFF && formatted > 0x17 ? $"{B(0x16)}.{B(0x17)}" : null,
                    };
                    break;
                case 1:
                    inv = inv with { Manufacturer = S(0x04), Product = S(0x05), ProductVersion = S(0x06), Sku = S(0x19), Family = S(0x1A) };
                    break;
                case 2 when inv.BoardProduct is null:
                    inv = inv with { BoardManufacturer = S(0x04), BoardProduct = S(0x05) };
                    break;
                case 4 when inv.ProcessorName is null:
                    var threads = B(0x25) == 0xFF && formatted > 0x2F ? W(0x2E) : B(0x25);
                    var cores = B(0x23) == 0xFF && formatted > 0x2B ? W(0x2A) : B(0x23);
                    inv = inv with
                    {
                        ProcessorName = S(0x10)?.Trim(), ProcessorMaxMhz = W(0x14) is > 0 and var mhz ? mhz : null,
                        ProcessorCores = cores > 0 ? cores : null, ProcessorThreads = threads > 0 ? threads : null,
                    };
                    break;
                case 17:
                    var sizeWord = W(0x0C);
                    if (sizeWord is 0 or 0xFFFF) break; // an empty slot, or unknown
                    var sizeMb = sizeWord == 0x7FFF && formatted > 0x1F ? (int)BitConverter.ToUInt32(raw, at + 0x1C)
                        : (sizeWord & 0x8000) != 0 ? (sizeWord & 0x7FFF) / 1024 : sizeWord;
                    memory.Add(new MemoryModule(S(0x10), sizeMb, W(0x15), W(0x20), S(0x17)?.Trim(), S(0x1A)?.Trim()));
                    break;
                case 22 when inv.BatteryName is null:
                    var multiplier = Math.Max(1, (int)B(0x15));
                    inv = inv with
                    {
                        BatteryName = S(0x08), BatteryChemistry = S(0x14),
                        BatteryDesignMwh = W(0x0A) is > 0 and var cap ? cap * multiplier : null,
                    };
                    break;
                case 127:
                    at = end;
                    continue;
            }
            at = next;
        }
        return inv with { Memory = memory };
    }

    /// <summary>A short line for the log.</summary>
    public string Describe() =>
        $"BIOS {BiosVendor} {BiosVersion} ({BiosDate}{(BiosRelease is null ? "" : $", release {BiosRelease}")}), EC {EcRelease ?? "?"}; " +
        $"{Manufacturer} {Product} {ProductVersion} sku {Sku} family {Family}; board {BoardManufacturer} {BoardProduct}; " +
        $"CPU {ProcessorName} max {ProcessorMaxMhz} MHz {ProcessorCores}c/{ProcessorThreads}t; " +
        $"memory {string.Join(" + ", Memory.Select(m => $"{m.SizeMb / 1024.0:0.#} GB {m.ConfiguredSpeedMts} MT/s"))}; " +
        $"battery {BatteryName} {BatteryChemistry} {BatteryDesignMwh} mWh";

    private static List<string> ReadStrings(byte[] raw, int from, int end, out int next)
    {
        var strings = new List<string>();
        var i = from;
        if (i + 1 < end && raw[i] == 0 && raw[i + 1] == 0)
        {
            next = i + 2;
            return strings;
        }
        while (i < end)
        {
            var start = i;
            while (i < end && raw[i] != 0) i++;
            strings.Add(Encoding.ASCII.GetString(raw, start, i - start));
            i++;
            if (i >= end || raw[i] == 0)
            {
                i++;
                break;
            }
        }
        next = i;
        return strings;
    }

    private const uint Rsmb = 0x52534D42; // 'RSMB'

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern uint GetSystemFirmwareTable(uint provider, uint id, IntPtr buffer, uint size);
}
