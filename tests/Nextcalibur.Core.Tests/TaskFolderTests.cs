using Nextcalibur.Core.Configuration;
using Xunit;

namespace Nextcalibur.Core.Tests;

/// <summary>
/// The first start of a fresh install asks the Task Scheduler for a folder
/// that is not there yet. On .NET 10 that answer is a FileNotFoundException
/// from the COM binder, and it ended the process (sandbox, 23 September
/// 2026). Asked here of the real Task Scheduler - reading only.
/// </summary>
public class TaskFolderTests
{
    [Fact]
    public void A_missing_task_folder_is_null_not_a_crash()
    {
        var type = Type.GetTypeFromProgID("Schedule.Service");
        Assert.NotNull(type);
        dynamic service = Activator.CreateInstance(type!)!;
        service.Connect();

        Assert.Null(Elevation.TryGetFolder(service, "\\Nextcalibur-no-such-folder-" + Guid.NewGuid().ToString("N")));
        Assert.NotNull(Elevation.TryGetFolder(service, "\\"));
    }
}
