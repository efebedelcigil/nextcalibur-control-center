using Nextcalibur.Core.Hardware;
using Xunit;

namespace Nextcalibur.Core.Tests;

/// <summary>
/// The 32-byte block exchanged with the firmware. Every field is written at a
/// fixed offset the hardware decides, so a mistake here is not a compiler error
/// or an exception — it is a command the machine misreads, which is the one
/// class of bug in this project with physical consequences.
/// </summary>
public class SmiCommandTests
{
    [Fact]
    public void Block_is_thirty_two_bytes()
    {
        Assert.Equal(32, SmiCommand.SizeBytes);
        Assert.Equal(32, default(SmiCommand).ToBytes().Length);
    }

    [Fact]
    public void Every_field_survives_a_round_trip()
    {
        // Deliberately distinct values: a field written to the wrong offset
        // still round-trips if two fields happen to hold the same number.
        var sent = new SmiCommand
        {
            A0 = 0xFA00,
            A1 = 0x0200,
            A2 = 0x11223344,
            A3 = 0x55667788,
            A4 = 0x99AABBCC,
            A5 = 0xDDEEFF00,
            A6 = 0x0F1E2D3C,
            Reserved0 = 0x4B5A6978,
            Reserved1 = 0x87961A2B,
        };

        var back = SmiCommand.FromBytes(sent.ToBytes());

        Assert.Equal(sent.A0, back.A0);
        Assert.Equal(sent.A1, back.A1);
        Assert.Equal(sent.A2, back.A2);
        Assert.Equal(sent.A3, back.A3);
        Assert.Equal(sent.A4, back.A4);
        Assert.Equal(sent.A5, back.A5);
        Assert.Equal(sent.A6, back.A6);
        Assert.Equal(sent.Reserved0, back.Reserved0);
        Assert.Equal(sent.Reserved1, back.Reserved1);
    }

    [Fact]
    public void Fields_sit_at_the_offsets_the_firmware_expects()
    {
        // Documented in PROTOCOL.md §2: two 16-bit fields, then seven 32-bit
        // ones, little-endian. Asserted against the bytes rather than against
        // a round trip, which would agree with itself even if both halves were
        // wrong in the same way.
        var bytes = new SmiCommand { A0 = 0xFB00, A1 = 0x0100, A2 = 0x000000AB }.ToBytes();

        Assert.Equal(0x00, bytes[0]);
        Assert.Equal(0xFB, bytes[1]);
        Assert.Equal(0x00, bytes[2]);
        Assert.Equal(0x01, bytes[3]);
        Assert.Equal(0xAB, bytes[4]);
        Assert.Equal(0x00, bytes[5]);
        Assert.Equal(0x00, bytes[6]);
        Assert.Equal(0x00, bytes[7]);
    }

    [Fact]
    public void For_puts_the_family_and_subsystem_in_the_header()
    {
        var read = SmiCommand.For(SmiFamily.Read, SmiSubsystem.Thermal);

        Assert.Equal((ushort)SmiFamily.Read, read.A0);
        Assert.Equal((ushort)SmiSubsystem.Thermal, read.A1);
        Assert.Equal(0u, read.A2);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(31)]
    public void A_short_block_is_rejected(int length) =>
        Assert.Throws<ArgumentException>(() => SmiCommand.FromBytes(new byte[length]));

    [Fact]
    public void A_missing_block_is_rejected() =>
        Assert.Throws<ArgumentException>(() => SmiCommand.FromBytes(null!));

    [Fact]
    public void A_longer_block_is_read_from_its_start()
    {
        // The mailbox property has come back longer than 32 bytes. The extra is
        // not ours to interpret, but the header must still be read.
        var padded = new byte[64];
        padded[0] = 0x00;
        padded[1] = 0xFA;

        Assert.Equal(0xFA00, SmiCommand.FromBytes(padded).A0);
    }
}
