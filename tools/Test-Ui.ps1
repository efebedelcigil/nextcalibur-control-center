# Test-Ui.ps1 - drives the running (elevated) Nextcalibur window through UI
# Automation and writes a PASS/FAIL report. UIPI keeps an ordinary process
# from touching an elevated window, so run this from a scheduled task with
# RunLevel HighestAvailable (see tools/README.md); the report lands in the
# repository folder as nc-uitest.txt.
#
# Part 1: hide/show keeps the readings moving; a mode click refreshes the
#         Power Mode cards; Open issues opens the browser unelevated.
# Part 2: the Settings page writes the file; auto-install follows auto-check;
#         the close button asks, Yes exits, the log says so.
param([ValidateSet(1,2)][int]$Part = 1, [string]$Out = "C:\Users\efeva\Desktop\Nextcalibur\nc-uitest.txt")
if ($Part -eq 1) {
# Runs elevated from a scheduled task, because UIPI blocks an ordinary
# process from touching an elevated window. Writes a report next to the repo.
param([string]$Out = "C:\Users\efeva\Desktop\Nextcalibur\nc-uitest.txt")
$ErrorActionPreference = 'Continue'
Add-Type -AssemblyName UIAutomationClient, UIAutomationTypes, System.Windows.Forms
$report = New-Object System.Collections.Generic.List[string]
function Say($s) { $line = "{0} {1}" -f (Get-Date -Format HH:mm:ss.fff), $s; $report.Add($line); $line | Out-File $Out -Append -Encoding utf8 }
"" | Out-File $Out -Encoding utf8
Say ("elevated: " + ([Security.Principal.WindowsPrincipal][Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole('Administrators'))

function Window() {
  $root = [System.Windows.Automation.AutomationElement]::RootElement
  # By process, not by title: the title follows the language now.
  $w = $null
  foreach ($p in (Get-Process Nextcalibur -ErrorAction SilentlyContinue)) {
    $cond = New-Object System.Windows.Automation.PropertyCondition([System.Windows.Automation.AutomationElement]::ProcessIdProperty, $p.Id)
    $found = $root.FindFirst([System.Windows.Automation.TreeScope]::Children, $cond)
    if ($null -ne $found) { $w = $found }
  }
  if ($null -eq $w) {
    $procs = Get-Process Nextcalibur -ErrorAction SilentlyContinue
    foreach ($p in $procs) { if ($p.MainWindowHandle -ne 0) { $w = [System.Windows.Automation.AutomationElement]::FromHandle($p.MainWindowHandle) } }
  }
  return $w
}
function Find($w, $id) {
  $cond = New-Object System.Windows.Automation.PropertyCondition([System.Windows.Automation.AutomationElement]::AutomationIdProperty, $id)
  return $w.FindFirst([System.Windows.Automation.TreeScope]::Descendants, $cond)
}
function Invoke($w, $id) {
  $e = Find $w $id
  if ($null -eq $e) { Say "  [$id] not found"; return $false }
  try { $e.GetCurrentPattern([System.Windows.Automation.InvokePattern]::Pattern).Invoke(); Say "  [$id] invoked"; return $true }
  catch { try { $e.GetCurrentPattern([System.Windows.Automation.SelectionItemPattern]::Pattern).Select(); Say "  [$id] selected"; return $true } catch { Say "  [$id] no invoke/select: $_"; return $false } }
}
function Text($w, $id) { $e = Find $w $id; if ($null -eq $e) { return "<missing>" }; return $e.Current.Name }
function Enabled($w, $id) { $e = Find $w $id; if ($null -eq $e) { return "<missing>" }; return $e.Current.IsEnabled }

$w = Window
if ($null -eq $w) { Say "window not found"; exit 1 }
Say ("window: " + $w.Current.Name + " pid " + $w.Current.ProcessId)

# --- Test 1: hide to tray, bring back (a second launch wakes the first), readings resume
Say "T1 hide/show"
$before = Text $w 'SubtitleText'; Say "  subtitle before: $before"
Invoke $w 'HideToTrayButton' | Out-Null
Start-Sleep 6
$exe = (Get-Process Nextcalibur | Select-Object -First 1).Path
Say "  waking through a second launch of $exe"
Start-Process $exe | Out-Null
Start-Sleep 8
$w = Window
if ($null -eq $w) { Say "  window did not come back" } else {
  $t1 = Text $w 'SubtitleText'; Start-Sleep 5; $t2 = Text $w 'SubtitleText'
  Say "  subtitle after show: $t1"; Say "  five seconds later:   $t2"
  Say ("  RESULT T1: " + $(if ($t1 -ne $t2 -and $t2 -ne $before) { "PASS (readings moving)" } else { "FAIL (frozen)" }))
}

# --- Test 2: a mode click refreshes the Power Mode cards' range
Say "T2 mode -> power cards"
$w = Window
$current = @('ModeOffice','ModeGaming','ModePerformance') | Where-Object { (Find $w $_).GetCurrentPattern([System.Windows.Automation.SelectionItemPattern]::Pattern).Current.IsSelected }
Say "  mode now: $current"
Invoke $w 'ModeOffice' | Out-Null; Start-Sleep 3
Invoke $w 'NavPower' | Out-Null; Start-Sleep 2
$best = Enabled $w 'PowerModeBest'; $eff = Enabled $w 'PowerModeEfficiency'
Say "  in Office: Best enabled=$best  Efficiency enabled=$eff"
Say ("  RESULT T2: " + $(if ($best -eq $false -and $eff -eq $true) { "PASS" } else { "FAIL" }))
Invoke $w 'NavSystem' | Out-Null; Start-Sleep 1
Invoke $w 'ModeGaming' | Out-Null; Start-Sleep 3
Invoke $w 'NavPower' | Out-Null; Start-Sleep 2
Say ("  back in Gaming: Best enabled=" + (Enabled $w 'PowerModeBest') + "  Efficiency enabled=" + (Enabled $w 'PowerModeEfficiency'))
Invoke $w 'NavSystem' | Out-Null

# --- Test 3: Open issues opens the browser as the user, not elevated
Say "T3 Open issues unelevated"
$beforePids = (Get-Process | Select-Object -ExpandProperty Id)
Invoke $w 'NavSettings' | Out-Null; Start-Sleep 2
Invoke $w 'SettingReportButton' | Out-Null
Start-Sleep 6
$new = Get-Process | Where-Object { $beforePids -notcontains $_.Id -and $_.Name -notmatch 'powershell|conhost|schtasks' }
foreach ($p in $new) {
  try {
    $h = $p.Handle
    $token = [IntPtr]::Zero
    $sig = @'
using System; using System.Runtime.InteropServices;
public static class Tok {
  [DllImport("advapi32.dll", SetLastError=true)] public static extern bool OpenProcessToken(IntPtr h, uint access, out IntPtr token);
  [DllImport("advapi32.dll", SetLastError=true)] public static extern bool GetTokenInformation(IntPtr token, int cls, out int info, int len, out int ret);
  [DllImport("kernel32.dll")] public static extern bool CloseHandle(IntPtr h);
  public static int Elevation(IntPtr process) { IntPtr t; if (!OpenProcessToken(process, 8, out t)) return -1; int e; int r; bool ok = GetTokenInformation(t, 20, out e, 4, out r); CloseHandle(t); return ok ? e : -2; }
}
'@
    if (-not ("Tok" -as [type])) { Add-Type -TypeDefinition $sig }
    $el = [Tok]::Elevation($h)
    Say ("  new process " + $p.Name + " pid " + $p.Id + " elevated=" + $el)
  } catch { Say ("  new process " + $p.Name + ": " + $_) }
}
Invoke $w 'NavSystem' | Out-Null
Say "done"
} else {
$ErrorActionPreference = 'Continue'
Add-Type -AssemblyName UIAutomationClient, UIAutomationTypes
function Say($s) { ("{0} {1}" -f (Get-Date -Format HH:mm:ss.fff), $s) | Out-File $Out -Append -Encoding utf8 }
"" | Out-File $Out -Encoding utf8
function Window() {
  $root = [System.Windows.Automation.AutomationElement]::RootElement
  # By process, not by title: the title follows the language now.
  $w = $null
  foreach ($p in (Get-Process Nextcalibur -ErrorAction SilentlyContinue)) {
    $cond = New-Object System.Windows.Automation.PropertyCondition([System.Windows.Automation.AutomationElement]::ProcessIdProperty, $p.Id)
    $found = $root.FindFirst([System.Windows.Automation.TreeScope]::Children, $cond)
    if ($null -ne $found) { $w = $found }
  }
  return $w
}
function Find($w, $id) { $c = New-Object System.Windows.Automation.PropertyCondition([System.Windows.Automation.AutomationElement]::AutomationIdProperty, $id); return $w.FindFirst([System.Windows.Automation.TreeScope]::Descendants, $c) }
function Invoke($w, $id) {
  $e = Find $w $id; if ($null -eq $e) { Say "  [$id] not found"; return }
  try { $e.GetCurrentPattern([System.Windows.Automation.InvokePattern]::Pattern).Invoke(); Say "  [$id] invoked" }
  catch { try { $e.GetCurrentPattern([System.Windows.Automation.SelectionItemPattern]::Pattern).Select(); Say "  [$id] selected" } catch { try { $e.GetCurrentPattern([System.Windows.Automation.TogglePattern]::Pattern).Toggle(); Say "  [$id] toggled" } catch { Say "  [$id] no pattern: $_" } } }
}
function Enabled($w, $id) { $e = Find $w $id; if ($null -eq $e) { return "<missing>" }; return $e.Current.IsEnabled }
function Settings() { Get-Content "$env:APPDATA\Nextcalibur\settings.json" -Raw | ConvertFrom-Json }

$w = Window; if ($null -eq $w) { Say "window not found"; exit 1 }
Invoke $w 'NavSettings'; Start-Sleep 2

Say "T4 settings page writes the file (Office on battery)"
$before = (Settings).QuietOnBattery; Say "  file before: $before"
Invoke $w 'SettingOfficeOnBattery'; Start-Sleep 2
$after = (Settings).QuietOnBattery; Say "  file after:  $after"
Invoke $w 'SettingOfficeOnBattery'; Start-Sleep 2
$back = (Settings).QuietOnBattery; Say "  file back:   $back"
Say ("  RESULT T4: " + $(if ($after -ne $before -and $back -eq $before) { "PASS" } else { "FAIL" }))

Say "T8 auto-install follows auto-check"
$w = Window
Say ("  before: check=" + (Settings).AutoCheckForUpdates + " install-enabled=" + (Enabled $w 'SettingAutoInstallUpdates'))
Invoke $w 'SettingAutoCheckUpdates'; Start-Sleep 2
$offEnabled = Enabled $w 'SettingAutoInstallUpdates'; $offFile = (Settings).AutoCheckForUpdates
Say "  check off: file=$offFile install-enabled=$offEnabled"
Invoke $w 'SettingAutoCheckUpdates'; Start-Sleep 2
$onEnabled = Enabled $w 'SettingAutoInstallUpdates'; $onFile = (Settings).AutoCheckForUpdates
Say "  check on:  file=$onFile install-enabled=$onEnabled"
Say ("  RESULT T8: " + $(if ($offFile -eq $false -and $offEnabled -eq $false -and $onFile -eq $true -and $onEnabled -eq $true) { "PASS" } else { "FAIL" }))

Say "T9 close button asks, Yes exits, the log says so"
Invoke $w 'NavSystem'; Start-Sleep 1
Invoke $w 'CloseButton'; Start-Sleep 2
$w = Window
$title = (Find $w 'DialogTitleText'); Say ("  dialogue: " + $(if ($title) { $title.Current.Name } else { "<none>" }))
Invoke $w 'DialogButtonPrimary'; Start-Sleep 5
$alive = Get-Process Nextcalibur -ErrorAction SilentlyContinue
Say ("  process alive after Yes: " + ($null -ne $alive))
$log = Get-Content "$env:APPDATA\Nextcalibur\logs\nextcalibur-$(Get-Date -Format yyyyMMdd).log" | Select-String "exit:" | Select-Object -Last 1
Say ("  last exit line: " + $log)
Say ("  RESULT T9: " + $(if ($null -eq $alive -and "$log" -match "close button") { "PASS" } else { "FAIL" }))
Say "done"
}
