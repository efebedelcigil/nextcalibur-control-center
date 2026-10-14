using System.Text;
using Nextcalibur.Core.Hardware;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class FirmwareInventoryTests
{
    private static byte[] Structure(byte type, byte[] formatted, params string[] strings)
    {
        var body = new List<byte> { type, (byte)(formatted.Length + 4), 0, 0 };
        body.AddRange(formatted);
        if (strings.Length == 0) body.AddRange([0, 0]);
        else
        {
            foreach (var s in strings) { body.AddRange(Encoding.ASCII.GetBytes(s)); body.Add(0); }
            body.Add(0);
        }
        return body.ToArray();
    }

    private static byte[] Raw(params byte[][] structures)
    {
        var table = structures.SelectMany(s => s).ToArray();
        var header = new byte[8];
        BitConverter.GetBytes(table.Length).CopyTo(header, 4);
        return header.Concat(table).ToArray();
    }

    [Fact]
    public void The_bios_the_processor_and_memory_are_read()
    {
        // Type 0: vendor 1, version 2, date 3 (offset 0x08), release 5.27, EC 0xFF.
        var bios = new byte[0x14];
        bios[0x00] = 1; bios[0x01] = 2; bios[0x04] = 3;
        bios[0x10] = 5; bios[0x11] = 27; bios[0x12] = 0xFF; bios[0x13] = 0xFF;
        // Type 4: name at 0x10 -> index 1, max 4700 at 0x14, cores 10 at 0x23, threads 16 at 0x25.
        var cpu = new byte[0x24];
        cpu[0x0C] = 1; BitConverter.GetBytes((ushort)4700).CopyTo(cpu, 0x10);
        cpu[0x1F] = 10; cpu[0x21] = 16;
        // Type 17: size 16384 MB at 0x0C, locator 0x10, speed 3200 at 0x15, maker 0x17, part 0x1A, configured 0x20.
        var dimm = new byte[0x24];
        BitConverter.GetBytes((ushort)16384).CopyTo(dimm, 0x08); dimm[0x0C] = 1;
        BitConverter.GetBytes((ushort)3200).CopyTo(dimm, 0x11); dimm[0x13] = 2; dimm[0x16] = 3;
        BitConverter.GetBytes((ushort)3200).CopyTo(dimm, 0x1C);

        var f = FirmwareInventory.Parse(Raw(
            Structure(0, bios, "AMI", "QQ141", "06/27/2024"),
            Structure(4, cpu, "Intel Core i7"),
            Structure(17, dimm, "DIMM0", "Crucial", "CT16G4"),
            Structure(127, [])));

        Assert.Equal("AMI", f.BiosVendor);
        Assert.Equal("QQ141", f.BiosVersion);
        Assert.Equal("06/27/2024", f.BiosDate);
        Assert.Equal("5.27", f.BiosRelease);
        Assert.Null(f.EcRelease);
        Assert.Equal("Intel Core i7", f.ProcessorName);
        Assert.Equal(4700, f.ProcessorMaxMhz);
        Assert.Equal(10, f.ProcessorCores);
        Assert.Equal(16, f.ProcessorThreads);
        var m = Assert.Single(f.Memory);
        Assert.Equal(16384, m.SizeMb);
        Assert.Equal(3200, m.ConfiguredSpeedMts);
        Assert.Equal("Crucial", m.Manufacturer);
    }

    [Fact]
    public void A_short_or_empty_table_is_nothing() =>
        Assert.Null(FirmwareInventory.Parse([1, 2, 3]).BiosVersion);
}
