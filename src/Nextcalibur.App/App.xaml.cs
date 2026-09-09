using System.Windows;
using Nextcalibur.Core.Power;
using Velopack;

namespace Nextcalibur.App;

public partial class App : Application
{
    [STAThread]
    public static void Main(string[] args)
    {
        // Velopack takes over the process during install, update and uninstall
        // hooks, so this must run before any UI is created.
        VelopackApp.Build()
            .OnFirstRun(_ => RepairPowerOverlayOnFirstRun())
            .Run();

        var app = new App();
        app.InitializeComponent();
        app.Run();
    }

    /// <summary>
    /// Repairs the stuck power-overlay fault the first time the app runs after
    /// installation.
    ///
    /// This is the one fault worth fixing without being asked: on an affected
    /// machine the CPU is pinned at maximum frequency at idle, and nothing in
    /// the vendor software can change it. The repair is reversible and is
    /// reported to the user rather than done silently.
    /// </summary>
    private static void RepairPowerOverlayOnFirstRun()
    {
        try
        {
            var service = new PowerOverlayService();
            if (!service.Diagnose().NeedsRepair) return;

            var actions = service.Repair();
            if (actions.Count == 0) return;

            MessageBox.Show(
                "Nextcalibur found and repaired a Windows power configuration fault:\n\n" +
                string.Join("\n\n", actions) +
                "\n\nYour CPU can now idle down properly. You can undo this at any time " +
                "from the Power Mode panel.",
                "Nextcalibur - power fault repaired",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            // Never let a failed repair block startup.
            MessageBox.Show(
                "Nextcalibur could not repair the Windows power overlay automatically:\n\n" +
                ex.Message + "\n\nYou can retry from the Power Mode panel.",
                "Nextcalibur",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
    }
}
