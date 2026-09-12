using System.Text.Json;
using System.Text.Json.Serialization;
using Nextcalibur.Core.Hardware;

namespace Nextcalibur.Core.Configuration;

/// <summary>One saved lighting profile.</summary>
public sealed class LedProfile
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LedEffect Effect { get; set; } = LedEffect.Static;

    /// <summary>
    /// Perceived brightness, 0-100.
    ///
    /// The hardware's own brightness field has only three steps, which is too
    /// coarse to be useful, so this is applied by scaling the colour instead —
    /// the same technique the vendor software uses. Scaling happens at write
    /// time so the colours below stay at full strength: dimming and restoring
    /// must not lose the colour the user picked.
    /// </summary>
    public int BrightnessPercent { get; set; } = 100;

    /// <summary>Per-zone colour as 0xRRGGBB, keyed by zone name.</summary>
    public Dictionary<string, uint> Colours { get; set; } = new()
    {
        [nameof(LedZone.Left)] = 0xFFFFFF,
        [nameof(LedZone.Middle)] = 0xFFFFFF,
        [nameof(LedZone.Right)] = 0xFFFFFF,
    };
}

/// <summary>
/// The lighting state, persisted per user.
///
/// This exists because the hardware cannot report its own lighting: LED reads
/// echo the request header and return zero for every payload field. Without a
/// stored copy there would be no way to recolour one zone without resetting the
/// others, or to restore the lighting after a restart.
///
/// Edits always land in the active profile, mirroring how the vendor software
/// treats its four saved presets.
/// </summary>
public sealed class LedState
{
    public const string Office = "Office";
    public const string Gaming = "Gaming";
    public const string Performance = "Performance";
    public const string UserDefine = "UserDefine";

    public static readonly string[] ProfileNames = [Office, Gaming, Performance, UserDefine];

    /// <summary>Whether the lighting is switched on at all.</summary>
    public bool Enabled { get; set; } = true;

    public string ActiveProfile { get; set; } = UserDefine;

    public Dictionary<string, LedProfile> Profiles { get; set; } = new()
    {
        [Office] = new LedProfile { Effect = LedEffect.Static, BrightnessPercent = 60 },
        [Gaming] = new LedProfile
        {
            Effect = LedEffect.ColourCycle,
            Colours = new Dictionary<string, uint>
            {
                [nameof(LedZone.Left)] = 0xFF0080,
                [nameof(LedZone.Middle)] = 0xFF0080,
                [nameof(LedZone.Right)] = 0xFF0080,
            },
        },
        [Performance] = new LedProfile
        {
            Effect = LedEffect.Static,
            Colours = new Dictionary<string, uint>
            {
                [nameof(LedZone.Left)] = 0xFF3000,
                [nameof(LedZone.Middle)] = 0xFF3000,
                [nameof(LedZone.Right)] = 0xFF3000,
            },
        },
        [UserDefine] = new LedProfile(),
    };

    /// <summary>The profile currently in effect. Edits land here.</summary>
    [JsonIgnore]
    public LedProfile Current
    {
        get
        {
            if (!Profiles.TryGetValue(ActiveProfile, out var profile))
            {
                profile = new LedProfile();
                Profiles[ActiveProfile] = profile;
            }
            return profile;
        }
    }

    [JsonIgnore]
    public LedEffect Effect
    {
        get => Current.Effect;
        set => Current.Effect = value;
    }

    [JsonIgnore]
    public int BrightnessPercent
    {
        get => Current.BrightnessPercent;
        set => Current.BrightnessPercent = value;
    }

    public (byte R, byte G, byte B) GetColour(LedZone zone)
    {
        var packed = Current.Colours.TryGetValue(zone.ToString(), out var v) ? v : 0xFFFFFF;
        return ((byte)((packed >> 16) & 0xFF), (byte)((packed >> 8) & 0xFF), (byte)(packed & 0xFF));
    }

    public void SetColour(LedZone zone, byte r, byte g, byte b)
    {
        var packed = ((uint)r << 16) | ((uint)g << 8) | b;

        if (zone is LedZone.AllKeyboard or LedZone.Everything)
        {
            foreach (var z in new[] { LedZone.Left, LedZone.Middle, LedZone.Right })
                Current.Colours[z.ToString()] = packed;
            return;
        }

        Current.Colours[zone.ToString()] = packed;
    }

    private static string Path => System.IO.Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "Nextcalibur", "led.json");

    public static LedState Load()
    {
        try
        {
            if (File.Exists(Path))
            {
                var loaded = JsonSerializer.Deserialize<LedState>(File.ReadAllText(Path));
                if (loaded is not null)
                {
                    // A file written by an older build may be missing profiles.
                    foreach (var name in ProfileNames)
                        loaded.Profiles.TryAdd(name, new LedProfile());
                    return loaded;
                }
            }
        }
        catch
        {
            // A corrupt file just means falling back to white, which is harmless.
        }
        return new LedState();
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(Path)!);
            // Written beside and moved into place, so a write cut short - the
            // battery giving out, a forced power-off - leaves the previous file
            // whole rather than a truncated one that loads as defaults.
            var temporary = Path + ".tmp";
            File.WriteAllText(temporary, JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true }));
            File.Move(temporary, Path, overwrite: true);
        }
        catch
        {
            // Losing the stored lighting is not worth failing a colour change over.
        }
    }
}
