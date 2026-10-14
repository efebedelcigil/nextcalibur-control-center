using Nextcalibur.Core.Hardware;
using Xunit;

namespace Nextcalibur.Core.Tests;

public class IdleLoadPickerTests
{
    private static RunningItem Bg(string name, double cpu) => new(RunningKind.Background, name, 100, @"C:\Apps\" + name + ".exe", cpu, 0, null);
    private static RunningItem App(string name, double cpu) => new(RunningKind.App, name, 200, @"C:\Apps\" + name + ".exe", cpu, 0, null);

    [Fact]
    public void Busy_on_two_scans_in_a_row_is_picked() =>
        Assert.Equal(["Updater"], IdleLoadPicker.Choose([Bg("Updater", 2)], [Bg("Updater", 3)], [], []));

    [Fact]
    public void Busy_once_is_not() =>
        Assert.Empty(IdleLoadPicker.Choose([], [Bg("Updater", 5)], [], []));

    [Fact]
    public void A_program_with_a_window_is_never_picked() =>
        Assert.Empty(IdleLoadPicker.Choose([App("Editor", 9)], [App("Editor", 9)], [], []));

    [Fact]
    public void Below_the_threshold_is_not_picked() =>
        Assert.Empty(IdleLoadPicker.Choose([Bg("Sync", 0.5)], [Bg("Sync", 0.6)], [], []));

    [Fact]
    public void One_the_person_took_out_is_never_picked_again() =>
        Assert.Empty(IdleLoadPicker.Choose([Bg("Sync", 4)], [Bg("Sync", 4)], [], ["sync"]));

    [Fact]
    public void One_already_on_is_not_picked_twice() =>
        Assert.Empty(IdleLoadPicker.Choose([Bg("Sync", 4)], [Bg("Sync", 4)], ["Sync"], []));

    [Fact]
    public void At_most_a_few_at_a_time_the_busiest_first()
    {
        var before = Enumerable.Range(1, 6).Select(i => Bg("P" + i, i)).ToList();
        var now = Enumerable.Range(1, 6).Select(i => Bg("P" + i, i)).ToList();
        Assert.Equal(["P6", "P5", "P4"], IdleLoadPicker.Choose(before, now, [], []));
    }

    [Fact]
    public void Nextcalibur_itself_is_never_picked() =>
        Assert.Empty(IdleLoadPicker.Choose([Bg("NextcaliburHelper", 4)], [Bg("NextcaliburHelper", 4)], [], []));
}
