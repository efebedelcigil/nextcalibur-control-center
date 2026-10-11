using Nextcalibur.Core.Input;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class FnShortcutsTests
{
    private const HotkeyModifiers CtrlAlt = HotkeyModifiers.Control | HotkeyModifiers.Alt;

    private static FnShortcut S(HotkeyModifiers m, uint key, FnAction a = FnAction.FanTurbo) =>
        new() { Action = a, Modifiers = m, Key = key };

    [Fact]
    public void The_defaults_pass_every_check_that_needs_no_system()
    {
        var all = FnShortcuts.Defaults();
        Assert.All(all, s => Assert.Null(FnShortcuts.CheckShape(s, all)));
        Assert.Equal(Enum.GetValues<FnAction>().Length, all.Select(s => s.Action).Distinct().Count());
    }

    [Fact]
    public void One_modifier_is_not_enough() =>
        Assert.Equal(ShortcutProblem.TooFewModifiers, FnShortcuts.CheckShape(S(HotkeyModifiers.Control, 0x31), []));

    [Fact]
    public void The_windows_key_is_left_to_windows() =>
        Assert.Equal(ShortcutProblem.WindowsKey, FnShortcuts.CheckShape(S(HotkeyModifiers.Windows | HotkeyModifiers.Alt, 0x31), []));

    [Theory]
    [InlineData(HotkeyModifiers.Control | HotkeyModifiers.Alt, 0x2Eu)]     // Ctrl+Alt+Del
    [InlineData(HotkeyModifiers.Control | HotkeyModifiers.Shift, 0x1Bu)]   // Ctrl+Shift+Esc
    [InlineData(HotkeyModifiers.Control | HotkeyModifiers.Alt, 0x09u)]     // Ctrl+Alt+Tab
    public void What_windows_answers_itself_is_refused(HotkeyModifiers m, uint key) =>
        Assert.Equal(ShortcutProblem.ReservedBySystem, FnShortcuts.CheckShape(S(m, key), []));

    [Fact]
    public void Two_actions_on_the_same_keys_is_a_duplicate()
    {
        var a = S(CtrlAlt, 0x70, FnAction.FanTurbo);
        var b = S(CtrlAlt, 0x70, FnAction.SystemMode);
        Assert.Equal(ShortcutProblem.Duplicate, FnShortcuts.CheckShape(a, [a, b]));
        b.Enabled = false;
        Assert.Null(FnShortcuts.CheckShape(a, [a, b]));
    }

    [Fact]
    public void A_modifier_alone_is_not_a_key() =>
        Assert.Equal(ShortcutProblem.NotAKey, FnShortcuts.CheckShape(S(CtrlAlt, 0x10), []));

    [Fact]
    public void Function_keys_type_nothing_on_any_layout() =>
        Assert.Null(FnShortcuts.CharacterOnSomeLayout(CtrlAlt, 0x70));

    [Theory]
    [InlineData(HotkeyModifiers.Control | HotkeyModifiers.Alt, 0x70u, "Ctrl+Alt+F1")]
    [InlineData(HotkeyModifiers.Control | HotkeyModifiers.Shift | HotkeyModifiers.Alt, 0x31u, "Ctrl+Alt+Shift+1")]
    public void Described_the_way_windows_writes_them(HotkeyModifiers m, uint key, string text) =>
        Assert.Equal(text, FnShortcuts.Describe(m, key));
}
