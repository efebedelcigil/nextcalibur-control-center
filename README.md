# Graph Report - nextcalibur-control-center  (2026-10-14)

## Corpus Check
- 205 files · ~251,587 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3466 nodes · 7682 edges · 212 communities (173 shown, 39 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 464 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `0b801603`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- FnShortcut
- .OnLoaded
- RunawayWatchTests
- ProtectedStore
- .Get
- ProgramRule
- AppSettings
- IShellLinkW
- Window
- ProcessPresence
- PlanTuning
- RadioButton
- Nextcalibur.Core.Configuration
- Nextcalibur.Core.Hardware
- CoolingPolicyTests
- SystemInfo
- .RequestRestart
- WindowsFaults
- RoutedEventArgs
- Nextcalibur.App
- .Main
- Fact
- Button
- CpuLimits
- .For
- NetworkCostTests
- GpuClockReader
- Api
- ResourceDictionary
- WindowsFaultsTests
- .Get
- .RemoveMachineTraces
- .InstallAsync
- LedState
- .OnClosing
- EcMailbox
- Integrity
- SystemModeService
- Dependency
- .SignatureIsValid
- .Check
- TrayPresence
- Fact
- FirmwareInventory
- Fact
- .LoadDefinitionsAsync
- DevicePowerSaving
- Authenticode
- PowerOverlayService
- .Read
- CpuPowerReader
- .Ask
- DllImport
- .Measure
- DotNetRuntimeDependency
- .ModeName
- CeilingTestWindow
- RestorePoint
- Nextcalibur.Core.csproj
- UserPresence
- .StartSlowTimer
- .RefreshIdleRows
- MachineScanRecord
- .Scan
- MemoryTrimmer
- analyse-autopsy.py
- ModeProfileTests
- ToggleButton
- .CheckProgram
- TemperatureFusion
- .Warn
- RuntimeDependencyTests
- SmiCommand
- Program
- system_text_json
- .ForegroundFullScreenProcessId
- .OnSystemModeChanged
- CoreLoad
- LedController
- .GetColour
- ColourWheel
- DevicePowerState
- .MayStartWithoutPrompt
- FanGauge
- Nextcalibur.Core.Security
- SystemMode
- HostileFilesTests
- Unelevated
- DonutGauge
- .RunawayTick
- BatteryModePolicy
- IdleSetting
- SegmentedBar
- .Set
- Nextcalibur Control Center
- .Ask
- InstallFolderGuard
- LogInjectionTests
- .Capture
- StackPanel
- AcDc
- Grid
- LedBrightness
- PendingRestartInfo
- .HasAccess
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- system_management
- Strings
- LedEffect
- PackageTemperatureTests
- .Check
- AuditLog
- Hardware Protocol
- Nextcalibur 0.5.3
- .Migrate
- NvidiaDriverState
- IntPtr
- Integrity.cs
- Border
- ThemeService
- .RunAndRecord
- .Dictionary
- WordsInCodeTests
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- ThermalSample
- PawnIoDependency
- .Parse
- .IsSystemFileSignedBy
- ProfileFiles
- Probe
- CeilingTests
- .GetAsync
- Nextcalibur 0.5.1
- Nextcalibur 0.5.5
- .IsGpuAwakeFaultCondition
- NotificationState
- IntegrityKind
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- .Pick
- TourPage
- TestPreflight.cs
- Log-Graphics.ps1
- Privacy
- Nextcalibur 0.6.2
- Security
- ComboBox
- .LoadSettingsPage
- LedZone
- .Load
- PowerSource
- SystemModesTests
- Notice
- Nextcalibur 0.5.11
- AppIdentity.cs
- .Set
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Strings.cs
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- Nextcalibur 0.6.1
- .ArrangeOverride
- BrightnessSlider
- .Modules_are_the_released_ones
- Tools
- 0.5.7.md
- CpuWarnValue
- DialogCheck
- .ShowOverlayDialog
- .GetSystemFirmwareTable
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
- .A_setting_this_version_does_not_know_survives_a_round_trip

## God Nodes (most connected - your core abstractions)
1. `MainWindow` - 409 edges
2. `Window` - 191 edges
3. `AppSettings` - 86 edges
4. `Nextcalibur.Core.Hardware` - 73 edges
5. `SystemMode` - 66 edges
6. `Nextcalibur.Core.Power` - 44 edges
7. `Nextcalibur.Core.Configuration` - 43 edges
8. `RadioButton` - 41 edges
9. `Nextcalibur.Core.Tests` - 40 edges
10. `WindowsFaultsTests` - 40 edges

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

## Communities (212 total, 39 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.03
Nodes (74): DeferredOverheat, GameEnd, GameSession, HwndSource, ObservableCollection, PluggedIn, Queue, MainWindow (+66 more)

### Community 1 - "FnShortcut"
Cohesion: 0.06
Nodes (38): Nextcalibur.Core.Input, Problem, Detail, Dictionary, KeyEventArgs, DllImport, IEnumerable, IntPtr (+30 more)

### Community 2 - ".OnLoaded"
Cohesion: 0.06
Nodes (24): Asynchronous dispatcher & UI thread hardening, Guid, Ours, Plan, Task, EventArgs, DateTime, DispatcherTimer (+16 more)

### Community 3 - "RunawayWatchTests"
Cohesion: 0.06
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 4 - "ProtectedStore"
Cohesion: 0.09
Nodes (20): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+12 more)

### Community 5 - ".Get"
Cohesion: 0.10
Nodes (8): Dictionary, IReadOnlyDictionary, Task, Task, Guid, Ours, Plan, IReadOnlyDictionary

### Community 6 - "ProgramRule"
Cohesion: 0.08
Nodes (25): GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, IEnumerable (+17 more)

### Community 7 - "AppSettings"
Cohesion: 0.04
Nodes (48): FnShortcut, JsonElement, ProgramRule, AppSettings, AcceptedVendorSoftware, AskModeForGames, AutoCheckForUpdates, AutoCooling (+40 more)

### Community 8 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 9 - "Window"
Cohesion: 0.08
Nodes (46): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+38 more)

### Community 10 - "ProcessPresence"
Cohesion: 0.08
Nodes (18): DateTime, IReadOnlyList, MachineHistory, DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence (+10 more)

### Community 11 - "PlanTuning"
Cohesion: 0.13
Nodes (18): Floor, Marker, Original, Action, AcDc, DllImport, Guid, IEnumerable (+10 more)

### Community 12 - "RadioButton"
Cohesion: 0.06
Nodes (38): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeDiscrete, ModeHybrid (+30 more)

### Community 13 - "Nextcalibur.Core.Configuration"
Cohesion: 0.07
Nodes (17): Nextcalibur.Core.Configuration, microsoft_win32, microsoft_win32_safehandles, Theme, Dark, Light, system_componentmodel, system_diagnostics (+9 more)

### Community 14 - "Nextcalibur.Core.Hardware"
Cohesion: 0.13
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, Nextcalibur.Core.Hardware, nextcalibur_core_power_plantuning_acdc, system_xml_linq, xunit

### Community 15 - "CoolingPolicyTests"
Cohesion: 0.08
Nodes (26): At, Margin, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc (+18 more)

### Community 16 - "SystemInfo"
Cohesion: 0.08
Nodes (21): MemoryStatusEx, DriveUse, INotifyPropertyChanged, DriveRow, Name, Percent, PercentText, DllImport (+13 more)

### Community 17 - ".RequestRestart"
Cohesion: 0.09
Nodes (11): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr, MarshalAs (+3 more)

### Community 18 - "WindowsFaults"
Cohesion: 0.09
Nodes (28): Process, Share, DateTime, Dictionary, Func, IReadOnlyList, List, TimeSpan (+20 more)

### Community 19 - "RoutedEventArgs"
Cohesion: 0.07
Nodes (9): RoutedPropertyChangedEventArgs, Slider, LedPower, SelectAll, Action, Color, RadioButton, RoutedEventArgs (+1 more)

### Community 20 - "Nextcalibur.App"
Cohesion: 0.14
Nodes (18): Nextcalibur.App, Nextcalibur.App.Controls, system_drawing, system_windows, system_windows_controls, system_windows_controls_button, system_windows_controls_checkbox, system_windows_controls_combobox (+10 more)

### Community 21 - ".Main"
Cohesion: 0.11
Nodes (9): The threat model, and what the code does about it, Application, DllImport, Mutex, App, OverlayRepairPending, RegistryKey, Footprint (+1 more)

### Community 22 - "Fact"
Cohesion: 0.15
Nodes (9): Func, IEnumerable, IReadOnlyList, RetirementEntry, Trace, Version, Fact, FootprintTests (+1 more)

### Community 23 - "Button"
Cohesion: 0.06
Nodes (27): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, HideToTrayButton (+19 more)

### Community 24 - "CpuLimits"
Cohesion: 0.09
Nodes (18): Bit, IReadOnlySet, Now, Offset, PowerInfo, PowerLimits, Ratio, SinceBoot (+10 more)

### Community 25 - ".For"
Cohesion: 0.12
Nodes (13): AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, Baseline (+5 more)

### Community 26 - "NetworkCostTests"
Cohesion: 0.14
Nodes (19): Bytes, Count, HttpMessageHandler, CancellationToken, Dictionary, Fact, HttpRequestMessage, HttpResponseMessage (+11 more)

### Community 27 - "GpuClockReader"
Cohesion: 0.12
Nodes (12): NvmlProcessInfo, NvmlUtilisation, DateTime, Func, IReadOnlyList, TimeSpan, GpuAwakeFault, GpuClockReader (+4 more)

### Community 28 - "Api"
Cohesion: 0.10
Nodes (23): Api, GetSettingFn, InitializeFn, ProfileOutFn, SessionFn, SessionOutFn, Set, SetSettingFn (+15 more)

### Community 29 - "ResourceDictionary"
Cohesion: 0.08
Nodes (29): ActualWidth, IsDropDownOpen, Arrow, Bg, CardBorder, Indicator, Knob, PART_EditableTextBox (+21 more)

### Community 31 - ".Get"
Cohesion: 0.13
Nodes (12): ArgumentNullException, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma, GpuModeService, SwitchOutcome (+4 more)

### Community 32 - ".RemoveMachineTraces"
Cohesion: 0.13
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 33 - ".InstallAsync"
Cohesion: 0.09
Nodes (20): FileStream, Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList (+12 more)

### Community 34 - "LedState"
Cohesion: 0.11
Nodes (19): Dictionary, LedBrightness, LedProfile, BrightnessPercent, Colours, Effect, LedState, ActiveProfile (+11 more)

### Community 35 - ".OnClosing"
Cohesion: 0.09
Nodes (11): CancelEventArgs, SizeChangedEventArgs, KeyEventArgs, Point, RoutedEventArgs, Size, TourStep, Body (+3 more)

### Community 36 - "EcMailbox"
Cohesion: 0.13
Nodes (14): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+6 more)

### Community 37 - "Integrity"
Cohesion: 0.11
Nodes (11): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvapiPath, NvmlPath (+3 more)

### Community 38 - "SystemModeService"
Cohesion: 0.18
Nodes (8): Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, Ours, Plan, SystemModeService

### Community 39 - "Dependency"
Cohesion: 0.11
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 40 - ".SignatureIsValid"
Cohesion: 0.15
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 41 - ".Check"
Cohesion: 0.13
Nodes (10): CommonAce, RawSecurityDescriptor, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported (+2 more)

### Community 42 - "TrayPresence"
Cohesion: 0.12
Nodes (11): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, List, Ms, Window (+3 more)

### Community 43 - "Fact"
Cohesion: 0.18
Nodes (7): IList, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 44 - "FirmwareInventory"
Cohesion: 0.08
Nodes (23): IReadOnlyList, FirmwareInventory, BatteryChemistry, BatteryDesignMwh, BatteryName, BiosDate, BiosRelease, BiosVendor (+15 more)

### Community 45 - "Fact"
Cohesion: 0.11
Nodes (14): OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact, Task (+6 more)

### Community 46 - ".LoadDefinitionsAsync"
Cohesion: 0.15
Nodes (13): Task, IEnumerable, PowerDefinition, PowerDefinitions, SetBy, Intel, LaptopMaker, Nextcalibur (+5 more)

### Community 47 - "DevicePowerSaving"
Cohesion: 0.17
Nodes (12): TextBlock, ToggleButton, AcDc, Guid, IEnumerable, DevicePowerSaving, Off, DeviceSaving (+4 more)

### Community 48 - "Authenticode"
Cohesion: 0.25
Nodes (14): CatalogInfo, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle, Authenticode (+6 more)

### Community 49 - "PowerOverlayService"
Cohesion: 0.16
Nodes (10): Option, Button, IEnumerable, DllImport, Guid, IReadOnlyList, PowerModeOption, PowerOverlays (+2 more)

### Community 50 - ".Read"
Cohesion: 0.12
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 51 - "CpuPowerReader"
Cohesion: 0.21
Nodes (6): ArgumentOutOfRangeException, DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 52 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 53 - "DllImport"
Cohesion: 0.19
Nodes (6): PdhFmtCounterValue, DllImport, IntPtr, CpuClockReader, BaseMhz, PdhFmtCounterValue

### Community 54 - ".Measure"
Cohesion: 0.18
Nodes (7): IEnumerable, IReadOnlyList, TimeSpan, CeilingStep, CeilingTest, Fact, CeilingTestTests

### Community 55 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 56 - ".ModeName"
Cohesion: 0.19
Nodes (3): AssemblyInformationalVersionAttribute, List, ModeChoice

### Community 57 - "CeilingTestWindow"
Cohesion: 0.17
Nodes (13): CancellationTokenSource, Button, CancellationToken, CheckBox, Func, List, StackPanel, Task (+5 more)

### Community 58 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 59 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 60 - "UserPresence"
Cohesion: 0.16
Nodes (5): NotificationState, SessionSwitchEventArgs, DateTime, TimeSpan, UserPresence

### Community 62 - ".RefreshIdleRows"
Cohesion: 0.16
Nodes (6): ComboBox, Grid, Guid, DllImport, IntPtr, ScreenSaver

### Community 63 - "MachineScanRecord"
Cohesion: 0.10
Nodes (21): DateTime, List, MachineScanRecord, BiosDate, BiosRelease, BiosVendor, BiosVersion, BoardProduct (+13 more)

### Community 64 - ".Scan"
Cohesion: 0.15
Nodes (12): Display, IoCounters, ProcessLook, Dictionary, DllImport, IntPtr, TimeSpan, IoCounters (+4 more)

### Community 65 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 66 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 67 - "ModeProfileTests"
Cohesion: 0.22
Nodes (7): MemberData, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 68 - "ToggleButton"
Cohesion: 0.11
Nodes (19): OverheatWarningToggle, SettingAskGameMode, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost, SettingFixWidgets (+11 more)

### Community 69 - ".CheckProgram"
Cohesion: 0.22
Nodes (5): Fact, IntegrityTests, Current, Exe, Manifest

### Community 70 - "TemperatureFusion"
Cohesion: 0.25
Nodes (9): DateTime, TimeSpan, TemperatureFusion, Offset, RegisterSetAside, Value, DateTime, Fact (+1 more)

### Community 71 - ".Warn"
Cohesion: 0.18
Nodes (3): Log, Folder, NduFix

### Community 72 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 73 - "SmiCommand"
Cohesion: 0.21
Nodes (6): ArgumentException, SmiCommand, Fact, InlineData, Theory, SmiCommandTests

### Community 75 - "system_text_json"
Cohesion: 0.22
Nodes (7): Nextcalibur.Core.Dependencies, system_collections_concurrent, system_net, system_net_http, system_text, system_text_json, system_text_json_serialization

### Community 76 - ".ForegroundFullScreenProcessId"
Cohesion: 0.26
Nodes (7): MonitorInfo, Rect, DllImport, IntPtr, MarshalAs, StringBuilder, MonitorInfo

### Community 77 - ".OnSystemModeChanged"
Cohesion: 0.16
Nodes (5): PowerModeChangedEventArgs, ModeBalanced, ModeGaming, ModeOffice, ModePerformance

### Community 78 - "CoreLoad"
Cohesion: 0.12
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 79 - "LedController"
Cohesion: 0.24
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 80 - ".GetColour"
Cohesion: 0.24
Nodes (7): B, G, R, Fact, InlineData, Theory, LedStateTests

### Community 81 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 82 - "DevicePowerState"
Cohesion: 0.25
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 83 - ".MayStartWithoutPrompt"
Cohesion: 0.23
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 84 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 85 - "Nextcalibur.Core.Security"
Cohesion: 0.17
Nodes (4): Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_security_cryptography_x509certificates, system_text_regularexpressions

### Community 86 - "SystemMode"
Cohesion: 0.18
Nodes (10): InvalidOperationException, IReadOnlyList, SystemMode, Balanced, Gaming, Office, Performance, SystemModes (+2 more)

### Community 87 - "HostileFilesTests"
Cohesion: 0.27
Nodes (5): IOException, Fact, InlineData, Theory, HostileFilesTests

### Community 88 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 89 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 90 - ".RunawayTick"
Cohesion: 0.16
Nodes (8): DllImport, IntPtr, Name, Pid, Action, Toasts, windows_data_xml_dom, windows_ui_notifications

### Community 91 - "BatteryModePolicy"
Cohesion: 0.35
Nodes (5): BatteryModePolicy, QuietMode, Remembered, Fact, BatteryModePolicyTests

### Community 92 - "IdleSetting"
Cohesion: 0.24
Nodes (8): AcDc, Guid, IReadOnlyList, IdleSetting, DisplayOff, Sleep, IdleTimeouts, Timeouts

### Community 93 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 94 - ".Set"
Cohesion: 0.30
Nodes (7): PowerThrottlingState, DllImport, IntPtr, EfficiencyMode, PowerThrottlingState, Fact, EfficiencyModeTests

### Community 95 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 96 - ".Ask"
Cohesion: 0.20
Nodes (5): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner

### Community 97 - "InstallFolderGuard"
Cohesion: 0.30
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 98 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 99 - ".Capture"
Cohesion: 0.26
Nodes (6): EnumWindowsProc, IO_COUNTERS, PROCESS_MEMORY_COUNTERS, ProcessMetrics, DllImport, IntPtr

### Community 100 - "StackPanel"
Cohesion: 0.15
Nodes (13): BannerActions, CeilingRows, DefinitionRows, DialogActions, DriveTextStack, FnShortcutRows, IdleLoadRows, IdleRows (+5 more)

### Community 101 - "AcDc"
Cohesion: 0.26
Nodes (5): AcDc, InlineData, Theory, Fact, IdleTimeoutsTests

### Community 102 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 103 - "LedBrightness"
Cohesion: 0.18
Nodes (8): LedBrightness, Full, Half, Off, Fact, InlineData, Theory, BacklightKeyTests

### Community 104 - "PendingRestartInfo"
Cohesion: 0.19
Nodes (7): DateTime, Dictionary, List, PendingRestartInfo, BootTimeUtc, ReasonArguments, Reasons

### Community 105 - ".HasAccess"
Cohesion: 0.28
Nodes (5): SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 106 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 107 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 108 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 109 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 110 - "Nextcalibur 0.5.4"
Cohesion: 0.17
Nodes (11): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+3 more)

### Community 111 - "system_management"
Cohesion: 0.17
Nodes (5): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, system_management

### Community 112 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 113 - "LedEffect"
Cohesion: 0.15
Nodes (11): B, G, R, LedEffect, Blink, Breathing, ColourCycle, Heartbeat (+3 more)

### Community 114 - "PackageTemperatureTests"
Cohesion: 0.41
Nodes (4): Fact, InlineData, Theory, PackageTemperatureTests

### Community 115 - ".Check"
Cohesion: 0.20
Nodes (10): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+2 more)

### Community 116 - "AuditLog"
Cohesion: 0.20
Nodes (6): IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Fact, AuditLogTests

### Community 117 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 118 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 119 - ".Migrate"
Cohesion: 0.22
Nodes (3): Limits, SystemMode, SettingsTests

### Community 120 - "NvidiaDriverState"
Cohesion: 0.27
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 121 - "IntPtr"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 122 - "Integrity.cs"
Cohesion: 0.31
Nodes (4): Nextcalibur.Cli, system_security_accesscontrol, system_security_cryptography, system_security_principal

### Community 123 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 124 - "ThemeService"
Cohesion: 0.20
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 125 - ".RunAndRecord"
Cohesion: 0.29
Nodes (4): JsonSerializerOptions, MachineScan, Firmware, RecordPath

### Community 126 - ".Dictionary"
Cohesion: 0.27
Nodes (4): IReadOnlyDictionary, Stable, Dictionary, TopSharePercent

### Community 127 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 128 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 129 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 130 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 131 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 132 - "ThermalSample"
Cohesion: 0.33
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 133 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 134 - ".Parse"
Cohesion: 0.33
Nodes (3): List, Fact, FirmwareInventoryTests

### Community 135 - ".IsSystemFileSignedBy"
Cohesion: 0.39
Nodes (3): Fact, SystemFileTrustTests, Cmd

### Community 137 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 138 - "CeilingTests"
Cohesion: 0.31
Nodes (5): Baseline, Fact, InlineData, Theory, CeilingTests

### Community 139 - ".GetAsync"
Cohesion: 0.29
Nodes (7): Answer, ConcurrentDictionary, CancellationToken, HttpClient, Task, Answer, Conditional

### Community 140 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 141 - "Nextcalibur 0.5.5"
Cohesion: 0.25
Nodes (6): Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture

### Community 142 - ".IsGpuAwakeFaultCondition"
Cohesion: 0.25
Nodes (5): InlineData, Theory, GpuAwakeFaultConditionTests, InlineData, Theory

### Community 143 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 144 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 145 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 146 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 147 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 148 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 149 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 150 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 151 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 152 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 153 - "TestPreflight.cs"
Cohesion: 0.33
Nodes (6): RunningItem, RunningKind, App, Background, Service, Transfer

### Community 154 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 155 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 156 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 157 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 158 - "ComboBox"
Cohesion: 0.33
Nodes (6): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, SettingNvPower, ComboBox

### Community 160 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 162 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 164 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 165 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 166 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 168 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 170 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 171 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 172 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 174 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 175 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 176 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 177 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 179 - "BrightnessSlider"
Cohesion: 0.50
Nodes (4): BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 180 - ".Modules_are_the_released_ones"
Cohesion: 0.50
Nodes (3): InlineData, Theory, PawnIoModuleTests

### Community 181 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 184 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 185 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

## Knowledge Gaps
- **561 isolated node(s):** `OverlayRepairPending`, `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum` (+556 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1077 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **39 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `FnShortcut`, `.OnLoaded`, `RunawayWatchTests`, `ThermalSample`, `.Get`, `ProgramRule`, `AppSettings`, `Window`, `ProcessPresence`, `PlanTuning`, `RadioButton`, `Nextcalibur 0.5.5`, `Nextcalibur.Core.Configuration`, `CoolingPolicyTests`, `SystemInfo`, `.RequestRestart`, `RoutedEventArgs`, `Nextcalibur.App`, `Button`, `TourPage`, `.For`, `TestPreflight.cs`, `GpuClockReader`, `.LoadSettingsPage`, `.Get`, `LedZone`, `.OnClosing`, `EcMailbox`, `Integrity`, `SystemModeService`, `TrayPresence`, `.LoadDefinitionsAsync`, `DevicePowerSaving`, `PowerOverlayService`, `.Read`, `CpuPowerReader`, `DllImport`, `.ModeName`, `CeilingTestWindow`, `.ShowOverlayDialog`, `UserPresence`, `.StartSlowTimer`, `.RefreshIdleRows`, `TemperatureFusion`, `.OnSystemModeChanged`, `LedController`, `Nextcalibur.Core.Security`, `SystemMode`, `.RunawayTick`, `BatteryModePolicy`, `IdleSetting`, `.Ask`, `AcDc`, `Grid`, `LedBrightness`, `system_management`, `Strings`, `LedEffect`, `.Check`, `NvidiaDriverState`, `ThemeService`?**
  _High betweenness centrality (0.490) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `RadioButton`, `Text`, `RoutedEventArgs`, `Button`, `ComboBox`, `Keycaps`, `DriveGauge`, `BrightnessSlider`, `CpuWarnValue`, `DialogCheck`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `ToggleButton`, `TourCanvas`, `Wheel`, `.OnSystemModeChanged`, `StackPanel`, `Grid`, `Border`?**
  _High betweenness centrality (0.100) - this node is a cross-community bridge._
- **Why does `Nextcalibur.Core.Hardware` connect `Nextcalibur.Core.Hardware` to `RunawayWatchTests`, `ThermalSample`, `ProcessPresence`, `Nextcalibur.Core.Configuration`, `WindowsFaults`, `Nextcalibur.App`, `CpuLimits`, `TestPreflight.cs`, `GpuClockReader`, `Api`, `.Get`, `.RemoveMachineTraces`, `EcMailbox`, `FirmwareInventory`, `.LoadDefinitionsAsync`, `.Read`, `UserPresence`, `system_text_json`, `CoreLoad`, `DevicePowerState`, `Nextcalibur.Core.Security`, `LedBrightness`, `system_management`, `.Check`, `NvidiaDriverState`, `Integrity.cs`?**
  _High betweenness centrality (0.078) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `OverlayRepairPending`, `SelectedColour`, `VisualChildrenCount` to the rest of the system?**
  _561 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.025095188646590515 - nodes in this community are weakly interconnected._
- **Should `FnShortcut` be split into smaller, more focused modules?**
  _Cohesion score 0.05970149253731343 - nodes in this community are weakly interconnected._