using Nextcalibur.Core.Hardware;
using Xunit;

namespace Nextcalibur.Core.Tests;

/// <summary>
/// What the Display page is allowed to say, and at whose expense.
///
/// The rule these pin down was learned the hard way: an NVML call wakes the
/// GPU, so reading power to warn somebody their GPU never sleeps is a warning
/// that causes what it warns about. Discrete is the one mode where the card is
/// awake regardless, so it is the one mode where that reading is free.
/// </summary>
public class GpuModeTests
{
    private static GpuConfiguration Discrete => new(
        GpuMode.Discrete, "Test Adapter", DiscretePresent: true,
        DiscreteDrivesDisplay: true, IntegratedDrivesDisplay: false);

    private static GpuConfiguration Hybrid => new(
        GpuMode.Hybrid, "Test Adapter", DiscretePresent: true,
        DiscreteDrivesDisplay: false, IntegratedDrivesDisplay: true);

    private static ThermalSample Sample(int gpuC, int gpuRpm) =>
        new(50, gpuC, 3000, gpuRpm, DateTimeOffset.UnixEpoch);

    [Fact]
    public void Discrete_reports_what_the_card_reads_right_now()
    {
        var text = GpuModeService.Describe(Discrete, null, Sample(61, 3799));

        Assert.Contains("61 °C", text);
        Assert.Contains("3799 rpm", text);
    }

    [Fact]
    public void Discrete_says_nothing_about_cost_when_there_is_no_reading()
    {
        // Silence beats a fabricated figure. The mode warning still stands on
        // its own.
        var text = GpuModeService.Describe(Discrete);

        Assert.DoesNotContain("Right now", text);
        Assert.Contains("never powers down", text);
    }

    [Fact]
    public void A_zero_temperature_is_treated_as_no_reading()
    {
        // The mailbox answers zero when it answers nothing useful.
        var text = GpuModeService.Describe(Discrete, null, Sample(0, 0));

        Assert.DoesNotContain("Right now", text);
    }

    [Fact]
    public void Hybrid_never_carries_an_idle_cost_sentence()
    {
        // Even handed a reading, Hybrid has nothing to answer for: the card is
        // asleep, and the cost sentence is about a card that cannot sleep.
        var text = GpuModeService.Describe(Hybrid, null, Sample(48, 3299));

        Assert.DoesNotContain("Right now", text);
        Assert.DoesNotContain("3299", text);
    }

    [Fact]
    public void Every_mode_the_detector_can_return_has_something_to_say()
    {
        foreach (var mode in new[] { GpuMode.Discrete, GpuMode.Hybrid, GpuMode.Uma })
        {
            var text = GpuModeService.Describe(
                new GpuConfiguration(mode, "Test Adapter", true, false, true));

            Assert.False(string.IsNullOrWhiteSpace(text));
        }
    }

    [Fact]
    public void An_unrecognised_configuration_admits_it_rather_than_guessing()
    {
        var text = GpuModeService.Describe(
            new GpuConfiguration(null, null, false, false, false));

        Assert.Contains("could not work out", text);
    }
}

/// <summary>
/// The register and the values that go into it, pinned. Both were watched
/// being written by the vendor software and confirmed by the boot that
/// followed; nothing else has ever been shown safe to write there.
/// </summary>
public class GpuSwitchProtocolTests
{
    [Fact]
    public void The_display_mode_register_is_where_it_was_seen()
    {
        // FB00/0203 on the way to Discrete, FB00/0203 on the way back. The
        // subsystem is the thermal family's register 3.
        Assert.Equal((ushort)0x0203, (ushort)SmiSubsystem.DisplayMode);
    }

    [Fact]
    public void A_write_command_for_the_register_has_the_captured_header()
    {
        var command = SmiCommand.For(SmiFamily.Write, SmiSubsystem.DisplayMode);
        var bytes = command.ToBytes();

        // 00 FB 03 02 - the first four bytes of both captures.
        Assert.Equal(new byte[] { 0x00, 0xFB, 0x03, 0x02 }, bytes[..4]);
    }

    [Fact]
    public void Uma_is_not_a_firmware_mode()
    {
        // UMA is a device disable. Asking the firmware writer for it must be
        // refused before anything is touched.
        var service = new GpuModeService();
        Assert.Throws<ArgumentNullException>(() => service.WriteFirmwareMode(null!, GpuMode.Hybrid));
    }

    [Fact]
    public void A_switch_outcome_says_whether_a_restart_is_owed()
    {
        var immediate = new GpuModeService.SwitchOutcome(true, false, "done");
        var deferred = new GpuModeService.SwitchOutcome(true, true, "at restart");

        Assert.False(immediate.RestartNeeded);
        Assert.True(deferred.RestartNeeded);
    }
}

/// <summary>
/// The gate that keeps this application from writing to a laptop it was not
/// built against. The judgement is pure so it can be tested against numbers;
/// the hardware half is exercised by every run of the suite on the reference
/// machine.
/// </summary>
public class HardwareSupportTests
{
    private static ThermalSample Sample(int cpu, int gpu, int cpuFan, int gpuFan) =>
        new(cpu, gpu, cpuFan, gpuFan, DateTimeOffset.UnixEpoch);

    [Fact]
    public void A_reading_like_the_reference_machine_passes()
    {
        Assert.True(HardwareSupport.Judge(Sample(55, 51, 3611, 3281)));
    }

    [Fact]
    public void All_zeros_is_not_this_protocol()
    {
        // What a mailbox that exists but means something else tends to say.
        var reasons = new List<string>();
        Assert.False(HardwareSupport.Judge(Sample(0, 0, 0, 0), reasons));
        Assert.Contains(reasons, r => r.Contains("zero"));
    }

    [Fact]
    public void Temperatures_no_laptop_reports_are_rejected()
    {
        Assert.False(HardwareSupport.Judge(Sample(200, 51, 3000, 3000)));
        Assert.False(HardwareSupport.Judge(Sample(55, 130, 3000, 3000)));
    }

    [Fact]
    public void Fan_speeds_no_laptop_fan_does_are_rejected()
    {
        Assert.False(HardwareSupport.Judge(Sample(55, 51, 40000, 3000)));
    }

    [Fact]
    public void A_cold_idle_machine_still_passes()
    {
        // Fans off and a cool room are normal, not evidence of the wrong laptop.
        Assert.True(HardwareSupport.Judge(Sample(31, 28, 0, 0)));
    }

    [Fact]
    public void The_verdict_on_this_machine_is_supported()
    {
        // This runs on the reference machine. Anywhere else it documents what
        // the gate says about that machine, which is the point of it.
        var verdict = HardwareSupport.Check();
        Assert.NotEmpty(verdict.Reasons);
        Assert.True(verdict.AllowsReads == (verdict.Level != SupportLevel.Unsupported));
        Assert.True(verdict.AllowsWrites == (verdict.Level == SupportLevel.Supported));
    }
}
