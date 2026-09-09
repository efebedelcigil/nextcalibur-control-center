using System.Text.Json;
using System.Text.Json.Serialization;
using Nextcalibur.Core.Hardware;

namespace Nextcalibur.Core.Configuration;

/// <summary>
/// The lighting state, persisted per user.
///
/// This exists because the hardware cannot report its own lighting: LED reads
/// echo the request header and return zero for every payload field. Without a
/// stored copy there would be no way to recolour one zone without resetting the
/// others, or to restore the lighting after a restart.
/// </summary>
public sealed class LedState
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LedEffect Effect { get; set; } = LedEffect.Static;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LedBrightness Brightness { get; set; } = LedBrightness.Full;

    /// <summary>Per-zone colour as 0xRRGGBB, keyed by zone name.</summary>
    public Dictionary<string, uint> Colours { get; set; } = new()
    {
        [nameof(LedZone.Left)] = 0xFFFFFF,
        [nameof(LedZone.Middle)] = 0xFFFFFF,
        [nameof(LedZone.Right)] = 0xFFFFFF,
    };

    public (byte R, byte G, byte B) GetColour(LedZone zone)
    {
        var packed = Colours.TryGetValue(zone.ToString(), out var v) ? v : 0xFFFFFF;
        return ((byte)((packed >> 16) & 0xFF), (byte)((packed >> 8) & 0xFF), (byte)(packed & 0xFF));
    }

    public void SetColour(LedZone zone, byte r, byte g, byte b)
    {
        if (zone is LedZone.AllKeyboard or LedZone.Everything)
        {
            foreach (var z in new[] { LedZone.Left, LedZone.Middle, LedZone.Right })
                Colours[z.ToString()] = ((uint)r << 16) | ((uint)g << 8) | b;
            return;
        }

        Colours[zone.ToString()] = ((uint)r << 16) | ((uint)g << 8) | b;
    }

    private static string Path => System.IO.Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "Nextcalibur", "led.json");

    public static LedState Load()
    {
        try
        {
            if (File.Exists(Path))
                return JsonSerializer.Deserialize<LedState>(File.ReadAllText(Path)) ?? new LedState();
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
            File.WriteAllText(Path, JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true }));
        }
        catch
        {
            // Losing the stored lighting is not worth failing a colour change over.
        }
    }
}
