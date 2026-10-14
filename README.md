# Graph Report - nextcalibur-control-center  (2026-10-14)

## Corpus Check
- 192 files · ~240,468 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3224 nodes · 7130 edges · 186 communities (152 shown, 34 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 413 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `6c4a00ac`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- AppSettings
- FnShortcut
- Authenticode
- ProcessPresence
- .CheckProgram
- GpuClockReader
- Nextcalibur.Core.Configuration
- RunawayWatchTests
- CpuLimits
- ProtectedStore
- IShellLinkW
- WindowsFaults
- ProgramRule
- Window
- RadioButton
- PlanTuning
- .Warn
- .WireSettingsPage
- TrayPresence
- xunit
- NetworkCostTests
- .RequestRestart
- .Info
- Button
- .Check
- SystemInfo
- WindowsFaultsTests
- SystemTools
- .For
- CoolingPolicyTests
- Elevation
- .StartSlowTimer
- Nextcalibur.Core.Hardware
- .Survey
- .Get
- .OnClosing
- Nextcalibur.Core.Security
- EcMailbox
- SystemModeService
- MachineScanRecord
- ModeProfileTests
- Fact
- DevicePowerSaving
- ToggleButton
- Dependency
- UserPresence
- CpuPowerReader
- system_diagnostics
- .Ask
- .Read
- DotNetRuntimeDependency
- Nextcalibur.Core.csproj
- .RefreshIdleRows
- IdleSetting
- RestorePoint
- BacklightKeyWatcher
- MemoryTrimmer
- DependencyStatus
- GpuModeService
- analyse-autopsy.py
- RuntimeDependencyTests
- .GameTick
- PowerOverlayService
- ResourceDictionary
- UpdateService
- .GetColour
- Program
- .ForegroundFullScreenProcessId
- LedController
- ColourWheel
- DevicePowerState
- SystemMode
- .OnSystemModeChanged
- CoreLoad
- .ApplyFnShortcuts
- LedState
- FanGauge
- DonutGauge
- BatteryModePolicy
- Hardware Protocol
- SegmentedBar
- Nextcalibur Control Center
- .IsUnderProgramFiles
- LogInjectionTests
- Grid
- .Check
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Strings
- PackageTemperatureTests
- AuditLog
- SmiCommandTests
- Nextcalibur 0.5.3
- StackPanel
- .Sanitised
- Border
- ThemeService
- NvidiaDriverState
- .Dictionary
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- .PowerModeControls
- .LoadGpuMode
- PawnIoDependency
- ThermalSample
- Probe
- CeilingTests
- Nextcalibur 0.5.1
- Log
- LedEffect
- NotificationState
- Test-Ui.ps1
- Text
- Nextcalibur 0.5.5
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- .Pick
- .ForegroundProcess
- TourPage
- .TryShow
- Log-Graphics.ps1
- Privacy
- Nextcalibur 0.6.2
- Security
- .OnBrightnessChanged
- LedZone
- MachineWideGate
- PowerSource
- PawnIoModuleTests.cs
- SystemModesTests
- Notice
- Nextcalibur 0.5.11
- AppIdentity.cs
- ComboBox
- .Set
- .A_polling_interval_from_the_file_is_a_sane_one
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- 2. Command structure
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- Nextcalibur 0.6.1
- .ArrangeOverride
- .TryParseRgb
- ThemeService.cs
- Tools
- 0.5.7.md
- CpuWarnValue
- DialogCheck
- Words
- ColGauge
- DialogProgress
- DriveDot
- DriveList
- EffectPanel
- PART_ContentHost
- TourCanvas
- Wheel
- Icons.xaml
- Palette.xaml
- Palette.Dark.xaml
- Palette.Light.xaml
- Strings.en.xaml
- Strings.tr.xaml
- Typography.xaml

## God Nodes (most connected - your core abstractions)
1. `MainWindow` - 381 edges
2. `Window` - 187 edges
3. `AppSettings` - 82 edges
4. `Nextcalibur.Core.Hardware` - 62 edges
5. `SystemMode` - 55 edges
6. `RadioButton` - 41 edges
7. `Nextcalibur.Core.Configuration` - 40 edges
8. `WindowsFaultsTests` - 40 edges
9. `TextBlock` - 39 edges
10. `Nextcalibur.Core.Power` - 38 edges

## Surprising Connections (you probably didn't know these)
- `Deep memory & resource leak eradication` --references--> `MainWindow`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.App/MainWindow.Audit.cs
- `Deep memory & resource leak eradication` --references--> `CpuPowerReader`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.Core/Hardware/CpuPowerReader.cs
- `Zero-bottleneck gaming & heavy workload architecture` --references--> `MemoryTrimmer`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.Core/Hardware/MemoryTrimmer.cs
- `Zero-bottleneck gaming & heavy workload architecture` --references--> `WindowsFaults`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.Core/Hardware/WindowsFaults.cs
- `Fix GPU Switch Restart Prompt & Windows Privilege Adjustment` --references--> `TokenPrivileges`  [INFERRED]
  docs/releases/0.5.8.md → src/Nextcalibur.App/MainWindow.xaml.cs

## Import Cycles
- None detected.

## Communities (186 total, 34 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.03
Nodes (67): CheckBox, DeferredOverheat, HwndSource, ObservableCollection, PluggedIn, Queue, Slider, MainWindow (+59 more)

### Community 1 - "AppSettings"
Cohesion: 0.04
Nodes (59): DateTime, Dictionary, FnShortcut, JsonElement, Limits, List, ProgramRule, SystemMode (+51 more)

### Community 2 - "FnShortcut"
Cohesion: 0.05
Nodes (42): INotifyPropertyChanged, Problem, DriveRow, Detail, Name, Percent, PercentText, Dictionary (+34 more)

### Community 3 - "Authenticode"
Cohesion: 0.06
Nodes (38): CatalogInfo, Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on (+30 more)

### Community 4 - "ProcessPresence"
Cohesion: 0.05
Nodes (28): DateTime, IReadOnlyList, MachineHistory, DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence (+20 more)

### Community 5 - ".CheckProgram"
Cohesion: 0.06
Nodes (27): Lazy, ProcessModule, FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Dictionary, HashSet (+19 more)

### Community 6 - "GpuClockReader"
Cohesion: 0.08
Nodes (23): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+15 more)

### Community 7 - "Nextcalibur.Core.Configuration"
Cohesion: 0.08
Nodes (29): Nextcalibur.App, Nextcalibur.App.Controls, Nextcalibur.Core, Nextcalibur.Core.Configuration, system_componentmodel, system_drawing, system_globalization, system_io (+21 more)

### Community 8 - "RunawayWatchTests"
Cohesion: 0.07
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 9 - "CpuLimits"
Cohesion: 0.06
Nodes (30): Bit, IReadOnlySet, Now, Offset, PowerInfo, PowerLimits, Ratio, SinceBoot (+22 more)

### Community 10 - "ProtectedStore"
Cohesion: 0.09
Nodes (20): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+12 more)

### Community 11 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 12 - "WindowsFaults"
Cohesion: 0.08
Nodes (34): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+26 more)

### Community 13 - "ProgramRule"
Cohesion: 0.08
Nodes (25): GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, IEnumerable (+17 more)

### Community 14 - "Window"
Cohesion: 0.08
Nodes (46): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+38 more)

### Community 15 - "RadioButton"
Cohesion: 0.06
Nodes (39): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeBalanced, ModeGaming (+31 more)

### Community 16 - "PlanTuning"
Cohesion: 0.13
Nodes (18): Floor, Marker, Original, Action, AcDc, DllImport, Guid, IEnumerable (+10 more)

### Community 17 - ".Warn"
Cohesion: 0.10
Nodes (10): The threat model, and what the code does about it, Application, DllImport, Mutex, App, OverlayRepairPending, RegistryKey, Footprint (+2 more)

### Community 18 - ".WireSettingsPage"
Cohesion: 0.08
Nodes (15): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary, IReadOnlyDictionary, Task (+7 more)

### Community 19 - "TrayPresence"
Cohesion: 0.09
Nodes (18): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, ProcessInformation, List, Ms (+10 more)

### Community 20 - "xunit"
Cohesion: 0.11
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, nextcalibur_core_power_plantuning_acdc, system_text_regularexpressions, system_xml_linq, xunit

### Community 21 - "NetworkCostTests"
Cohesion: 0.11
Nodes (26): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+18 more)

### Community 22 - ".RequestRestart"
Cohesion: 0.08
Nodes (12): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, BannerRestartButton, DllImport, EventArgs, IntPtr (+4 more)

### Community 23 - ".Info"
Cohesion: 0.12
Nodes (5): Asynchronous dispatcher & UI thread hardening, Task, EventArgs, IProgress, Exception

### Community 24 - "Button"
Cohesion: 0.07
Nodes (25): IsChecked, BannerDismissButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, HideToTrayButton, MinimiseButton, OpenLogButton (+17 more)

### Community 25 - ".Check"
Cohesion: 0.11
Nodes (14): CommonAce, RawSecurityDescriptor, RegistryKey, SecurityIdentifier, MailboxAccess, MailboxAvailability, AccessNotGranted, Available (+6 more)

### Community 26 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 27 - "WindowsFaultsTests"
Cohesion: 0.17
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 28 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 29 - ".For"
Cohesion: 0.13
Nodes (13): AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, Baseline (+5 more)

### Community 30 - "CoolingPolicyTests"
Cohesion: 0.12
Nodes (16): At, Margin, DateTime, List, TimeSpan, CoolingPolicy, DegreesPerMinute, Engaged (+8 more)

### Community 31 - "Elevation"
Cohesion: 0.13
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 32 - ".StartSlowTimer"
Cohesion: 0.09
Nodes (5): SolidColorBrush, Guid, Ours, Plan, Func

### Community 33 - "Nextcalibur.Core.Hardware"
Cohesion: 0.12
Nodes (6): Nextcalibur.Core.Hardware, microsoft_win32, microsoft_win32_safehandles, system_management, system_runtime_interopservices, system_security

### Community 34 - ".Survey"
Cohesion: 0.15
Nodes (7): Func, IEnumerable, IReadOnlyList, RetirementEntry, Trace, Version, FootprintTests

### Community 35 - ".Get"
Cohesion: 0.15
Nodes (6): AssemblyInformationalVersionAttribute, GameEnd, List, MessageBoxButton, MessageBoxResult, ModeChoice

### Community 36 - ".OnClosing"
Cohesion: 0.09
Nodes (11): CancelEventArgs, SizeChangedEventArgs, KeyEventArgs, Point, RoutedEventArgs, Size, TourStep, Body (+3 more)

### Community 37 - "Nextcalibur.Core.Security"
Cohesion: 0.14
Nodes (8): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_security_cryptography_x509certificates, system_text_json

### Community 38 - "EcMailbox"
Cohesion: 0.15
Nodes (11): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+3 more)

### Community 39 - "SystemModeService"
Cohesion: 0.19
Nodes (8): Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, Ours, Plan, SystemModeService

### Community 40 - "MachineScanRecord"
Cohesion: 0.11
Nodes (18): DateTime, JsonSerializerOptions, List, MachineScan, RecordPath, MachineScanRecord, BiosVendor, BiosVersion (+10 more)

### Community 41 - "ModeProfileTests"
Cohesion: 0.16
Nodes (10): MemberData, AcDc, InlineData, Theory, Fact, InlineData, Theory, ModeProfileTests (+2 more)

### Community 42 - "Fact"
Cohesion: 0.18
Nodes (7): IList, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 43 - "DevicePowerSaving"
Cohesion: 0.17
Nodes (12): TextBlock, ToggleButton, AcDc, Guid, IEnumerable, DevicePowerSaving, Off, DeviceSaving (+4 more)

### Community 44 - "ToggleButton"
Cohesion: 0.08
Nodes (21): LedPower, OverheatWarningToggle, SelectAll, SettingAskGameMode, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice (+13 more)

### Community 45 - "Dependency"
Cohesion: 0.13
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 46 - "UserPresence"
Cohesion: 0.15
Nodes (5): NotificationState, SessionSwitchEventArgs, DateTime, TimeSpan, UserPresence

### Community 47 - "CpuPowerReader"
Cohesion: 0.21
Nodes (6): ArgumentOutOfRangeException, DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 48 - "system_diagnostics"
Cohesion: 0.13
Nodes (7): Nextcalibur.Cli, Nextcalibur.Core.Input, system_diagnostics, system_security_accesscontrol, system_security_cryptography, system_security_principal, system_text

### Community 49 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 50 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 51 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 52 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 53 - ".RefreshIdleRows"
Cohesion: 0.16
Nodes (6): ComboBox, Grid, Guid, DllImport, IntPtr, ScreenSaver

### Community 54 - "IdleSetting"
Cohesion: 0.17
Nodes (10): AcDc, Guid, IReadOnlyList, IdleSetting, DisplayOff, Sleep, IdleTimeouts, Timeouts (+2 more)

### Community 55 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 56 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 57 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 58 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 59 - "GpuModeService"
Cohesion: 0.20
Nodes (8): ArgumentNullException, GpuMode, Discrete, Hybrid, Uma, GpuModeService, SwitchOutcome, SwitchOutcome

### Community 60 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 61 - "RuntimeDependencyTests"
Cohesion: 0.22
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 62 - ".GameTick"
Cohesion: 0.12
Nodes (13): GameSession, DateTime, Dictionary, IReadOnlyList, Name, Pid, GameEnd, GameSession (+5 more)

### Community 63 - "PowerOverlayService"
Cohesion: 0.21
Nodes (7): DllImport, Guid, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, PowerOverlayService

### Community 64 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 65 - "UpdateService"
Cohesion: 0.14
Nodes (12): DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService, Available, AvailableVersion (+4 more)

### Community 66 - ".GetColour"
Cohesion: 0.22
Nodes (7): B, G, R, Fact, InlineData, Theory, LedStateTests

### Community 68 - ".ForegroundFullScreenProcessId"
Cohesion: 0.26
Nodes (7): MonitorInfo, Rect, DllImport, IntPtr, MarshalAs, StringBuilder, MonitorInfo

### Community 69 - "LedController"
Cohesion: 0.24
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 70 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 71 - "DevicePowerState"
Cohesion: 0.25
Nodes (6): DevPropKey, Discrete GPU sleep protection in Hybrid mode, DllImport, Guid, DevicePowerState, DevPropKey

### Community 72 - "SystemMode"
Cohesion: 0.17
Nodes (10): InvalidOperationException, IReadOnlyList, SystemMode, Balanced, Gaming, Office, Performance, SystemModes (+2 more)

### Community 73 - ".OnSystemModeChanged"
Cohesion: 0.24
Nodes (4): PowerModeChangedEventArgs, Guid, Ours, Plan

### Community 74 - "CoreLoad"
Cohesion: 0.13
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 76 - "LedState"
Cohesion: 0.13
Nodes (15): Dictionary, LedBrightness, LedProfile, BrightnessPercent, Colours, Effect, LedState, ActiveProfile (+7 more)

### Community 77 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 78 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 79 - "BatteryModePolicy"
Cohesion: 0.35
Nodes (5): BatteryModePolicy, QuietMode, Remembered, Fact, BatteryModePolicyTests

### Community 80 - "Hardware Protocol"
Cohesion: 0.14
Nodes (14): 1. Transport — ACPI-WMI mailbox, 3. Thermal / fan — `a1 = 0x0200`, 4. LED — `a1 = 0x0100`, 6. Safety rules, 7. Not supported, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`) (+6 more)

### Community 81 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 82 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 83 - ".IsUnderProgramFiles"
Cohesion: 0.23
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 84 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 85 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 86 - ".Check"
Cohesion: 0.18
Nodes (10): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+2 more)

### Community 87 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 88 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 89 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 90 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 91 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 92 - "PackageTemperatureTests"
Cohesion: 0.41
Nodes (4): Fact, InlineData, Theory, PackageTemperatureTests

### Community 93 - "AuditLog"
Cohesion: 0.20
Nodes (6): IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Fact, AuditLogTests

### Community 94 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 95 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 96 - "StackPanel"
Cohesion: 0.18
Nodes (11): BannerActions, CeilingRows, DialogActions, DriveTextStack, FnShortcutRows, IdleRows, PanelWindowsFaultsSubOptions, ProfileRow (+3 more)

### Community 97 - ".Sanitised"
Cohesion: 0.29
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 98 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 99 - "ThemeService"
Cohesion: 0.20
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 100 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 101 - ".Dictionary"
Cohesion: 0.27
Nodes (4): IReadOnlyDictionary, Stable, Dictionary, TopSharePercent

### Community 102 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 103 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 104 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 105 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 106 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 107 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 108 - ".PowerModeControls"
Cohesion: 0.22
Nodes (7): Option, Button, IEnumerable, IReadOnlyList, PowerModeOption, PowerOverlays, All

### Community 110 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 111 - "ThermalSample"
Cohesion: 0.31
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 112 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 113 - "CeilingTests"
Cohesion: 0.31
Nodes (5): Baseline, Fact, InlineData, Theory, CeilingTests

### Community 114 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 116 - "LedEffect"
Cohesion: 0.25
Nodes (8): LedEffect, Blink, Breathing, ColourCycle, Heartbeat, Off, Static, Wave

### Community 117 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 118 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 119 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 120 - "Nextcalibur 0.5.5"
Cohesion: 0.29
Nodes (5): Deep memory & resource leak eradication, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture

### Community 121 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 122 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 123 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 124 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 125 - ".ForegroundProcess"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Name, Pid

### Community 126 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 127 - ".TryShow"
Cohesion: 0.33
Nodes (4): Action, Toasts, windows_data_xml_dom, windows_ui_notifications

### Community 128 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 129 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 130 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 131 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 132 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 133 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 134 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 135 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 136 - "PawnIoModuleTests.cs"
Cohesion: 0.33
Nodes (4): system_reflection, InlineData, Theory, PawnIoModuleTests

### Community 138 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 139 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 140 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 141 - "ComboBox"
Cohesion: 0.40
Nodes (5): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, ComboBox

### Community 144 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 146 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 147 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 148 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 149 - "2. Command structure"
Cohesion: 0.50
Nodes (4): 2. Command structure, Call sequence, Command families (`a0`), Subsystems (`a1`)

### Community 150 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 151 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 152 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 153 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 155 - ".TryParseRgb"
Cohesion: 0.50
Nodes (3): B, G, R

### Community 156 - "ThemeService.cs"
Cohesion: 0.50
Nodes (3): Theme, Dark, Light

### Community 157 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 160 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 161 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

### Community 162 - "Words"
Cohesion: 0.67
Nodes (3): Func, Words, Resolver

## Knowledge Gaps
- **505 isolated node(s):** `OverlayRepairPending`, `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum` (+500 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 984 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **34 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `AppSettings`, `FnShortcut`, `ProcessPresence`, `.CheckProgram`, `.OnBrightnessChanged`, `GpuClockReader`, `LedZone`, `RunawayWatchTests`, `Nextcalibur.Core.Configuration`, `IShellLinkW`, `WindowsFaults`, `ProgramRule`, `Window`, `RadioButton`, `PlanTuning`, `.WireSettingsPage`, `TrayPresence`, `.RequestRestart`, `.Info`, `Button`, `SystemInfo`, `.For`, `CoolingPolicyTests`, `.StartSlowTimer`, `.Get`, `.OnClosing`, `Nextcalibur.Core.Security`, `EcMailbox`, `SystemModeService`, `DevicePowerSaving`, `ToggleButton`, `UserPresence`, `CpuPowerReader`, `.Read`, `.RefreshIdleRows`, `IdleSetting`, `BacklightKeyWatcher`, `DependencyStatus`, `GpuModeService`, `.GameTick`, `PowerOverlayService`, `UpdateService`, `LedController`, `SystemMode`, `.OnSystemModeChanged`, `.ApplyFnShortcuts`, `BatteryModePolicy`, `Grid`, `.Check`, `Strings`, `ThemeService`, `NvidiaDriverState`, `.PowerModeControls`, `.LoadGpuMode`, `ThermalSample`, `LedEffect`, `Nextcalibur 0.5.5`, `.ForegroundProcess`, `TourPage`?**
  _High betweenness centrality (0.489) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `.OnBrightnessChanged`, `ComboBox`, `RadioButton`, `.WireSettingsPage`, `Keycaps`, `DriveGauge`, `.RequestRestart`, `Button`, `CpuWarnValue`, `DialogCheck`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `ToggleButton`, `Wheel`, `Grid`, `StackPanel`, `Border`, `Text`?**
  _High betweenness centrality (0.085) - this node is a cross-community bridge._
- **Why does `Nextcalibur.Core.Hardware` connect `Nextcalibur.Core.Hardware` to `Nextcalibur.Core.Security`, `GpuClockReader`, `Nextcalibur.Core.Configuration`, `MachineWideGate`, `CpuLimits`, `PawnIoModuleTests.cs`, `ThermalSample`, `system_diagnostics`, `xunit`, `.Check`, `BacklightKeyWatcher`?**
  _High betweenness centrality (0.069) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `OverlayRepairPending`, `SelectedColour`, `VisualChildrenCount` to the rest of the system?**
  _505 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.028327228327228327 - nodes in this community are weakly interconnected._
- **Should `AppSettings` be split into smaller, more focused modules?**
  _Cohesion score 0.03555686159271231 - nodes in this community are weakly interconnected._