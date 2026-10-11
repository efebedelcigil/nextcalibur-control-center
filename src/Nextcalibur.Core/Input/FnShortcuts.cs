using System.Runtime.InteropServices;
using System.Text;

namespace Nextcalibur.Core.Input;

/// <summary>What a shortcut does: the laptop keyboard's Fn keys, for a keyboard that has no way to send them.</summary>
public enum FnAction
{
    /// <summary>
    /// Fn+1, the fan turbo. Kept so stored numbers keep their meaning, never
    /// offered: the controller switches it only from its own keyboard
    /// (PROTOCOL.md), so no shortcut can.
    /// </summary>
    FanTurbo,

    /// <summary>Fn+Space: the keyboard light, full - off - half.</summary>
    KeyboardBacklight,

    /// <summary>Office, Gaming, Performance in turn - what the vendor's mode key did.</summary>
    SystemMode,
}

/// <summary>Modifier bits as RegisterHotKey takes them.</summary>
[Flags]
public enum HotkeyModifiers : uint
{
    None = 0,
    Alt = 0x1,
    Control = 0x2,
    Shift = 0x4,
    Windows = 0x8,
}

/// <summary>One shortcut as stored: what it does, whether it is on, and the keys.</summary>
public sealed class FnShortcut
{
    public FnAction Action { get; set; }
    public bool Enabled { get; set; } = true;
    public HotkeyModifiers Modifiers { get; set; }

    /// <summary>The virtual-key code of the key pressed with the modifiers.</summary>
    public uint Key { get; set; }

    public FnShortcut Clone() => new() { Action = Action, Enabled = Enabled, Modifiers = Modifiers, Key = Key };
}

/// <summary>Why a shortcut cannot be used, or null when it can.</summary>
public enum ShortcutProblem
{
    /// <summary>Fewer than two of Ctrl, Alt and Shift: programs use those for their own commands, and those cannot be seen from outside.</summary>
    TooFewModifiers,

    /// <summary>The Windows key: Windows keeps nearly every one of those for itself, and adds more with each release.</summary>
    WindowsKey,

    /// <summary>Windows itself answers it whatever is registered (Ctrl+Alt+Del, Ctrl+Shift+Esc, Alt+Tab ...).</summary>
    ReservedBySystem,

    /// <summary>On an installed keyboard layout Ctrl+Alt is AltGr, and this combination types a character.</summary>
    TypesACharacter,

    /// <summary>Another program has registered the same shortcut with Windows.</summary>
    TakenByAnotherProgram,

    /// <summary>Two of these shortcuts are the same keys.</summary>
    Duplicate,

    /// <summary>A key that is not a key to bind: a modifier alone, or nothing.</summary>
    NotAKey,
}

/// <summary>
/// The external keyboard's Fn keys: shortcuts the application registers with
/// Windows for what the laptop's own keyboard does with Fn - the owner's
/// request of 11 October 2026, after Fn+1 on an external keyboard did
/// nothing. An external keyboard cannot send Fn at all: every keyboard
/// resolves its Fn key inside itself, and the laptop's is wired to the
/// embedded controller, which acts on Fn+1 and Fn+Space without Windows
/// seeing a key. So these are ordinary shortcuts, and the work is in not
/// taking one somebody else already uses.
///
/// What is checked, the way shortcut tools (PowerToys among them) check it:
/// - registration with Windows - RegisterHotKey refuses a combination
///   another program holds, which is the only system-wide record there is;
/// - what Windows answers itself, whatever is registered, and the Windows
///   key, which it reserves wholesale;
/// - the keyboard layouts installed, because Ctrl+Alt is AltGr: on Turkish Q
///   Ctrl+Alt+Q types @, and a shortcut there would take a character away;
/// - two modifiers at least, since a program's own Ctrl+S or Alt+F lives
///   inside that program and no outside check can see it.
/// </summary>
public static class FnShortcuts
{
    /// <summary>The shortcuts offered at first: Ctrl+Alt with function keys, which no layout types with.</summary>
    public static List<FnShortcut> Defaults() =>
    [
        new() { Action = FnAction.KeyboardBacklight, Modifiers = HotkeyModifiers.Control | HotkeyModifiers.Alt, Key = 0x70 + 1 }, // F2
        new() { Action = FnAction.SystemMode, Modifiers = HotkeyModifiers.Control | HotkeyModifiers.Alt, Key = 0x70 + 2 },        // F3
    ];

    /// <summary>The checks that need nothing from the system: modifiers, reserved combinations, duplicates.</summary>
    public static ShortcutProblem? CheckShape(FnShortcut s, IEnumerable<FnShortcut> all)
    {
        if (s.Key == 0 || IsModifierKey(s.Key)) return ShortcutProblem.NotAKey;
        if (s.Modifiers.HasFlag(HotkeyModifiers.Windows)) return ShortcutProblem.WindowsKey;

        var count = (s.Modifiers.HasFlag(HotkeyModifiers.Control) ? 1 : 0)
                    + (s.Modifiers.HasFlag(HotkeyModifiers.Alt) ? 1 : 0)
                    + (s.Modifiers.HasFlag(HotkeyModifiers.Shift) ? 1 : 0);
        if (count < 2) return ShortcutProblem.TooFewModifiers;
        if (IsReserved(s.Modifiers, s.Key)) return ShortcutProblem.ReservedBySystem;

        if (all.Any(o => !ReferenceEquals(o, s) && o.Enabled && o.Modifiers == s.Modifiers && o.Key == s.Key))
            return ShortcutProblem.Duplicate;
        return null;
    }

    /// <summary>Combinations Windows acts on itself, registered or not.</summary>
    public static bool IsReserved(HotkeyModifiers m, uint key)
    {
        const uint delete = 0x2E, escape = 0x1B, tab = 0x09, f4 = 0x73, space = 0x20, snapshot = 0x2C;
        var ctrlAlt = HotkeyModifiers.Control | HotkeyModifiers.Alt;
        var ctrlShift = HotkeyModifiers.Control | HotkeyModifiers.Shift;
        return (m == ctrlAlt && key == delete)                      // the secure attention sequence
               || (m == ctrlShift && key == escape)                 // Task Manager
               || (m.HasFlag(HotkeyModifiers.Alt) && key is tab or escape or f4 or space)
               || key == snapshot;                                  // Print Screen and its combinations
    }

    /// <summary>
    /// The character, if any, that a combination with Ctrl+Alt types on one
    /// of the installed keyboard layouts - AltGr's characters. Null when none does.
    /// </summary>
    public static string? CharacterOnSomeLayout(HotkeyModifiers m, uint key)
    {
        if (!m.HasFlag(HotkeyModifiers.Control) || !m.HasFlag(HotkeyModifiers.Alt)) return null;

        var count = GetKeyboardLayoutList(0, null);
        if (count <= 0) return null;
        var layouts = new IntPtr[count];
        GetKeyboardLayoutList(count, layouts);

        var state = new byte[256];
        state[0x11] = 0x80; // VK_CONTROL
        state[0x12] = 0x80; // VK_MENU
        state[0xA2] = 0x80; // VK_LCONTROL
        state[0xA5] = 0x80; // VK_RMENU - AltGr is right Alt with left Ctrl
        if (m.HasFlag(HotkeyModifiers.Shift)) { state[0x10] = 0x80; state[0xA0] = 0x80; }

        var buffer = new StringBuilder(8);
        foreach (var layout in layouts)
        {
            var scan = MapVirtualKeyEx(key, 0 /* MAPVK_VK_TO_VSC */, layout);
            buffer.Clear();
            // Flag 4: do not change the keyboard state - no dead key left half pressed.
            var n = ToUnicodeEx(key, scan, state, buffer, buffer.Capacity, 4, layout);
            if (n > 0)
            {
                var typed = buffer.ToString(0, n);
                if (typed.Any(c => !char.IsControl(c))) return typed;
            }
        }
        return null;
    }

    private static bool IsModifierKey(uint vk) => vk is 0x10 or 0x11 or 0x12 or 0x5B or 0x5C or (>= 0xA0 and <= 0xA5);

    /// <summary>"Ctrl+Alt+F1", in the order Windows itself writes them.</summary>
    public static string Describe(HotkeyModifiers m, uint key)
    {
        var parts = new List<string>();
        if (m.HasFlag(HotkeyModifiers.Control)) parts.Add("Ctrl");
        if (m.HasFlag(HotkeyModifiers.Alt)) parts.Add("Alt");
        if (m.HasFlag(HotkeyModifiers.Shift)) parts.Add("Shift");
        if (m.HasFlag(HotkeyModifiers.Windows)) parts.Add("Win");
        parts.Add(KeyName(key));
        return string.Join("+", parts);
    }

    public static string KeyName(uint vk) => vk switch
    {
        >= 0x70 and <= 0x87 => "F" + (vk - 0x70 + 1),
        >= 0x30 and <= 0x39 => ((char)vk).ToString(),
        >= 0x41 and <= 0x5A => ((char)vk).ToString(),
        >= 0x60 and <= 0x69 => "Num" + (vk - 0x60),
        0x20 => "Space",
        0x0D => "Enter",
        0x08 => "Backspace",
        0x2D => "Insert",
        0x2E => "Delete",
        0x24 => "Home",
        0x23 => "End",
        0x21 => "PageUp",
        0x22 => "PageDown",
        0x25 => "Left",
        0x26 => "Up",
        0x27 => "Right",
        0x28 => "Down",
        _ => NameFromLayout(vk),
    };

    private static string NameFromLayout(uint vk)
    {
        var scan = MapVirtualKeyEx(vk, 0, GetKeyboardLayout(0));
        var name = new StringBuilder(32);
        return GetKeyNameText((int)(scan << 16), name, name.Capacity) > 0 ? name.ToString() : $"0x{vk:X2}";
    }

    [DllImport("user32.dll")]
    private static extern int GetKeyboardLayoutList(int count, IntPtr[]? list);

    [DllImport("user32.dll")]
    private static extern IntPtr GetKeyboardLayout(uint thread);

    [DllImport("user32.dll")]
    private static extern uint MapVirtualKeyEx(uint code, uint mapType, IntPtr layout);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int ToUnicodeEx(uint vk, uint scan, byte[] state, StringBuilder buffer, int size, uint flags, IntPtr layout);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int GetKeyNameText(int lParam, StringBuilder buffer, int size);
}
