namespace Nextcalibur.Core.Hardware;

/// <summary>How much of this application a machine is allowed to use.</summary>
public enum SupportLevel
{
    /// <summary>No firmware mailbox. Nothing here applies; nothing may be clicked.</summary>
    Unsupported,

    /// <summary>
    /// A mailbox exists but does not answer like this protocol. Readings may
    /// be shown; nothing that writes is offered.
    /// </summary>
    ReadOnly,

    /// <summary>The mailbox answers as measured on the reference machine. Everything.</summary>
    Supported,
}

/// <summary>The verdict, and the evidence it rests on.</summary>
/// <param name="Level">What may be used.</param>
/// <param name="Reasons">Why, one line each, in the order they were found.</param>
public readonly record struct SupportVerdict(SupportLevel Level, IReadOnlyList<string> Reasons)
{
    public bool AllowsWrites => Level == SupportLevel.Supported;
    public bool AllowsReads => Level != SupportLevel.Unsupported;
}

/// <summary>
/// Decides whether this machine is one the application knows how to drive.
///
/// The application writes to firmware and switches a graphics card off. On the
/// laptop it was built against those are measured and safe; on one it was not
/// built against they are a way to break somebody's machine, and the
/// repository is public. So nothing is offered until the machine has shown,
/// by its own answers, that it speaks this protocol.
///
/// The model name is no help. SMBIOS on the reference machine reads
/// <c>Type1MTM</c> and <c>Type2ProjectName</c> - the vendor never filled it in -
/// so the identity has to be the mailbox itself: whether it exists, and whether
/// a thermal read comes back with numbers a laptop could actually have.
///
/// <b>Nothing here writes.</b> A machine is judged by its answers to reads it
/// was always going to receive anyway.
/// </summary>
public static class HardwareSupport
{
    /// <summary>Runs the check against the machine.</summary>
    public static SupportVerdict Check()
    {
        var reasons = new List<string>();

        var access = MailboxAccess.Check();
        if (access == MailboxAvailability.NotSupported)
        {
            reasons.Add("No firmware mailbox (RW_GMWMI) - this laptop does not expose the interface.");
            return new SupportVerdict(SupportLevel.Unsupported, reasons);
        }

        if (access == MailboxAvailability.AccessNotGranted)
        {
            // Present but not yet readable by this account. That is a
            // permission question, answered elsewhere, not a hardware verdict -
            // so it is neither Unsupported nor proven. Read-only until granted.
            reasons.Add("The mailbox is present but this account cannot read it yet.");
            return new SupportVerdict(SupportLevel.ReadOnly, reasons);
        }

        reasons.Add("Firmware mailbox present.");

        ThermalSample thermal;
        try
        {
            using var mailbox = new EcMailbox();
            if (!new ThermalReader(mailbox).TryRead(out thermal))
            {
                reasons.Add("The mailbox did not answer a thermal read.");
                return new SupportVerdict(SupportLevel.ReadOnly, reasons);
            }

            if (!Judge(thermal, reasons))
                return new SupportVerdict(SupportLevel.ReadOnly, reasons);

            // The register the graphics switch writes. Read only; a machine
            // whose register does not hold one of the two known values is not
            // one whose register gets written.
            var mode = GpuModeService.ReadFirmwareMode(mailbox);
            if (mode.Mode is null)
            {
                reasons.Add("The display-mode register did not read as 1 or 2.");
                return new SupportVerdict(SupportLevel.ReadOnly, reasons);
            }
            reasons.Add($"Display-mode register reads {mode.Mode}.");
        }
        catch (EcMailboxUnavailableException ex)
        {
            reasons.Add($"The mailbox could not be used: {ex.Message}");
            return new SupportVerdict(SupportLevel.ReadOnly, reasons);
        }

        return new SupportVerdict(SupportLevel.Supported, reasons);
    }

    /// <summary>
    /// Whether a thermal reading looks like one this protocol produces.
    /// Pure, so it can be tested against numbers rather than hardware.
    /// </summary>
    /// <remarks>
    /// The reference machine idles around 45-60 °C and runs fans between 0 and
    /// 5200 rpm. The bounds here are generous - a laptop can be colder in a
    /// cold room and a fan can spin faster - and exist to reject nonsense: a
    /// mailbox that answers zeros, or a different firmware whose bytes land in
    /// these fields meaning something else entirely.
    /// </remarks>
    public static bool Judge(ThermalSample sample, IList<string>? reasons = null)
    {
        var plausible = true;

        if (sample.CpuTemperatureC is < 5 or > 125)
        {
            reasons?.Add($"CPU temperature {sample.CpuTemperatureC} °C is not a number a laptop reports.");
            plausible = false;
        }
        if (sample.GpuTemperatureC is < 0 or > 125)
        {
            reasons?.Add($"GPU temperature {sample.GpuTemperatureC} °C is not a number a laptop reports.");
            plausible = false;
        }
        if (sample.CpuFanRpm is < 0 or > 12000 || sample.GpuFanRpm is < 0 or > 12000)
        {
            reasons?.Add($"Fan speeds {sample.CpuFanRpm} / {sample.GpuFanRpm} rpm are outside anything a laptop fan does.");
            plausible = false;
        }
        if (sample.CpuTemperatureC == 0 && sample.GpuTemperatureC == 0 && sample.CpuFanRpm == 0 && sample.GpuFanRpm == 0)
        {
            reasons?.Add("Every reading is zero, which is what a mailbox that is not this protocol tends to say.");
            plausible = false;
        }

        if (plausible)
            reasons?.Add($"Thermal read is plausible: CPU {sample.CpuTemperatureC} °C, GPU {sample.GpuTemperatureC} °C.");

        return plausible;
    }
}
