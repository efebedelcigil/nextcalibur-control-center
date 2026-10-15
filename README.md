# Graph Report - nextcalibur-control-center  (2026-10-15)

## Corpus Check
- 219 files · ~267,530 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3762 nodes · 8372 edges · 203 communities (172 shown, 31 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 510 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `32775ab1`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- AppSettings
- GpuClockReader
- FnShortcut
- Authenticode
- .Get
- .CheckProgram
- ProgramRule
- GameLibrary
- Nextcalibur.Core.Configuration
- RunawayWatchTests
- ProtectedStore
- WindowsFaults
- xunit
- .WireSettingsPage
- Window
- IShellLinkW
- RadioButton
- ProcessPresence
- .Info
- PlanTuning
- ResourceDictionary
- Nextcalibur.Core.Hardware
- CoolingPolicyTests
- .OnLoaded
- NetworkCostTests
- WindowsFaultsTests
- Nextcalibur.Core.Security
- TrayPresence
- .RequestRestart
- SystemInfo
- Elevation
- UserPresence
- Fact
- Fact
- Button
- EcMailbox
- SystemTools
- CrashGuard
- .Choose
- Api
- .RuleRow
- .Main
- SystemModeService
- LedState
- .LoadDefinitionsAsync
- CeilingTestWindow
- TestPreflight
- .ModeName
- Fact
- Program
- DevicePowerSaving
- Dependency
- .Get
- .PlaceTourStep
- FirmwareInventory
- ColourWheel
- .Check
- App
- .Ask
- .For
- ToggleButton
- DotNetRuntimeDependency
- MachineScanRecord
- Nextcalibur.Core.csproj
- .ShowResult
- CpuPowerReader
- ModeProfile
- RestorePoint
- MemoryTrimmer
- DependencyStatus
- analyse-autopsy.py
- BacklightKeyWatcher
- DarkMenuColours
- PowerOverlayService
- UpdateService
- RuntimeDependencyTests
- TemperatureFusion
- CoreLoad
- .GetColour
- Unelevated
- LedController
- CpuLimitsTests
- CpuLimits
- FanGauge
- DevicePowerState
- DonutGauge
- BatteryModePolicy
- Hardware Protocol
- SegmentedBar
- SystemMode
- Nextcalibur Control Center
- .ApplyFnShortcuts
- Log
- .RunAndRecord
- .HasAccess
- .Write
- .Record
- LogInjectionTests
- Grid
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- microsoft_win32
- Strings
- StackPanel
- PackageTemperatureTests
- AcDc
- SmiCommandTests
- Nextcalibur 0.5.3
- ThemeService
- Border
- NvidiaDriverState
- .Parse
- SupportVerdict
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- CardSwitchTasks
- LedEffect
- ThermalSample
- PawnIoDependency
- Probe
- CeilingTests
- Nextcalibur 0.5.1
- Nextcalibur 0.5.5
- Toasts
- .Recommended
- Test-Ui.ps1
- Text
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- .ForegroundProcess
- TourPage
- .Current
- .Values_are_the_ones_measured_from_the_vendor
- Log-Graphics.ps1
- Privacy
- Nextcalibur 0.6.2
- Nextcalibur 0.7.0
- Security
- .OnBrightnessChanged
- ComboBox
- LedZone
- MachineWideGate
- PowerSource
- SystemModesTests
- Notice
- Nextcalibur 0.5.11
- Nextcalibur 0.7.1
- .PowerModeControls
- AppIdentity.cs
- SmiSubsystem
- .Read
- .FirmwareChanged
- PawnIoModuleTests.cs
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Strings.cs
- 2. Command structure
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- Nextcalibur 0.6.1
- .DecodeLimitReasons
- .ArrangeOverride
- Tools
- 0.5.7.md
- CpuWarnValue
- DialogCheck
- EffectPanel
- PART_ContentHost
- SmiFamily
- ColGauge
- DialogProgress
- DriveDot
- DriveList
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
1. `MainWindow` - 449 edges
2. `Window` - 198 edges
3. `AppSettings` - 96 edges
4. `Nextcalibur.Core.Hardware` - 77 edges
5. `SystemMode` - 69 edges
6. `Nextcalibur.Core.Power` - 51 edges
7. `Nextcalibur.Core.Configuration` - 50 edges
8. `Nextcalibur.Core.Tests` - 46 edges
9. `RadioButton` - 41 edges
10. `TextBlock` - 40 edges

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

## Communities (203 total, 31 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.02
Nodes (84): Added, DeferredOverheat, Found, GameSession, HwndSource, ObservableCollection, PluggedIn, Queue (+76 more)

### Community 1 - "AppSettings"
Cohesion: 0.03
Nodes (70): DateTime, Dictionary, FnShortcut, JsonElement, Limits, List, ProgramRule, SystemMode (+62 more)

### Community 2 - "GpuClockReader"
Cohesion: 0.06
Nodes (32): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, PowerThrottlingState, Detail, Func, Ok, SelfTest (+24 more)

### Community 3 - "FnShortcut"
Cohesion: 0.05
Nodes (43): INotifyPropertyChanged, Problem, DriveRow, Detail, Name, Percent, PercentText, Dictionary (+35 more)

### Community 4 - "Authenticode"
Cohesion: 0.06
Nodes (38): CatalogInfo, Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on (+30 more)

### Community 5 - ".Get"
Cohesion: 0.06
Nodes (10): GameEnd, SolidColorBrush, IReadOnlyList, GameEnd, Guid, Ours, Plan, Func (+2 more)

### Community 6 - ".CheckProgram"
Cohesion: 0.06
Nodes (28): Lazy, ProcessModule, FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Dictionary, HashSet (+20 more)

### Community 7 - "ProgramRule"
Cohesion: 0.05
Nodes (36): HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, GeneratedRegex, IEnumerable (+28 more)

### Community 8 - "GameLibrary"
Cohesion: 0.07
Nodes (20): Folder, Dictionary, GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, IReadOnlyList, List (+12 more)

### Community 9 - "Nextcalibur.Core.Configuration"
Cohesion: 0.09
Nodes (26): Nextcalibur.App, Nextcalibur.App.Controls, Nextcalibur.Core.Configuration, system_componentmodel, system_drawing, system_io, system_linq, system_runtime_compilerservices (+18 more)

### Community 10 - "RunawayWatchTests"
Cohesion: 0.07
Nodes (34): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+26 more)

### Community 11 - "ProtectedStore"
Cohesion: 0.09
Nodes (20): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+12 more)

### Community 12 - "WindowsFaults"
Cohesion: 0.07
Nodes (38): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+30 more)

### Community 13 - "xunit"
Cohesion: 0.09
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, nextcalibur_core_power_plantuning_acdc, system_text_regularexpressions, system_xml_linq, xunit

### Community 14 - ".WireSettingsPage"
Cohesion: 0.08
Nodes (8): Dictionary, IReadOnlyDictionary, Task, Task, Button, Action, Task, ToggleButton

### Community 15 - "Window"
Cohesion: 0.07
Nodes (47): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+39 more)

### Community 16 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 17 - "RadioButton"
Cohesion: 0.06
Nodes (42): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeBalanced, ModeDiscrete (+34 more)

### Community 18 - "ProcessPresence"
Cohesion: 0.09
Nodes (18): DateTime, IReadOnlyList, MachineHistory, DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence (+10 more)

### Community 19 - ".Info"
Cohesion: 0.07
Nodes (15): Kept, Key, Show, ComboBox, Grid, Guid, Guid, IReadOnlyList (+7 more)

### Community 20 - "PlanTuning"
Cohesion: 0.13
Nodes (18): Floor, Marker, Original, Action, AcDc, DllImport, Guid, IEnumerable (+10 more)

### Community 21 - "ResourceDictionary"
Cohesion: 0.07
Nodes (40): ActualWidth, IsDropDownOpen, Size, Arrow, Bar, Bg, Blade, Box (+32 more)

### Community 22 - "Nextcalibur.Core.Hardware"
Cohesion: 0.09
Nodes (9): Nextcalibur.Cli, Nextcalibur.Core.Input, Nextcalibur.Core.Hardware, microsoft_win32_safehandles, system_diagnostics, system_management, system_reflection, system_runtime_interopservices (+1 more)

### Community 23 - "CoolingPolicyTests"
Cohesion: 0.08
Nodes (26): At, Margin, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc (+18 more)

### Community 24 - ".OnLoaded"
Cohesion: 0.10
Nodes (9): CancelEventArgs, Asynchronous dispatcher & UI thread hardening, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Task (+1 more)

### Community 25 - "NetworkCostTests"
Cohesion: 0.11
Nodes (26): Answer, Bytes, Count, HttpMessageHandler, CancellationToken, ConcurrentDictionary, HttpClient, Task (+18 more)

### Community 26 - "WindowsFaultsTests"
Cohesion: 0.14
Nodes (4): FaultEvaluation, Fact, WindowsFaultsTests, Dictionary

### Community 27 - "Nextcalibur.Core.Security"
Cohesion: 0.10
Nodes (12): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_security_accesscontrol, system_security_cryptography (+4 more)

### Community 28 - "TrayPresence"
Cohesion: 0.09
Nodes (14): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, EventArgs, List, Ms (+6 more)

### Community 29 - ".RequestRestart"
Cohesion: 0.09
Nodes (10): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr, MarshalAs (+2 more)

### Community 30 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 31 - "Elevation"
Cohesion: 0.11
Nodes (11): dynamic, DllImport, Elevation, Fact, InlineData, Theory, ElevationPolicyTests, Profile (+3 more)

### Community 32 - "UserPresence"
Cohesion: 0.13
Nodes (14): LastInputInfo, MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr (+6 more)

### Community 33 - "Fact"
Cohesion: 0.13
Nodes (10): IList, GpuLoad, GpuConfiguration, HardwareSupport, Fact, GpuModeTests, Discrete, Hybrid (+2 more)

### Community 34 - "Fact"
Cohesion: 0.13
Nodes (9): Func, IEnumerable, IReadOnlyList, Version, RetirementEntry, Fact, FootprintTests, ForwardCompatibilityTests (+1 more)

### Community 35 - "Button"
Cohesion: 0.06
Nodes (30): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, GameScanButton (+22 more)

### Community 36 - "EcMailbox"
Cohesion: 0.14
Nodes (11): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+3 more)

### Community 37 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 38 - "CrashGuard"
Cohesion: 0.11
Nodes (19): Session, DateTimeOffset, Exception, List, TimeSpan, CrashGuard, SafeMode, UncleanPrevious (+11 more)

### Community 39 - ".Choose"
Cohesion: 0.12
Nodes (19): IEnumerable, IReadOnlyList, IdleLoadPicker, RunningItem, RunningKind, App, Background, Service (+11 more)

### Community 40 - "Api"
Cohesion: 0.11
Nodes (23): Api, GetSettingFn, InitializeFn, ProfileOutFn, SessionFn, SessionOutFn, Set, SetSettingFn (+15 more)

### Community 41 - ".RuleRow"
Cohesion: 0.12
Nodes (7): AssemblyInformationalVersionAttribute, Task, FrameworkElement, Func, Grid, List, ModeChoice

### Community 42 - ".Main"
Cohesion: 0.13
Nodes (5): RegistryKey, Footprint, Trace, NduFix, STAThread

### Community 43 - "SystemModeService"
Cohesion: 0.17
Nodes (8): Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, Ours, Plan, SystemModeService

### Community 44 - "LedState"
Cohesion: 0.11
Nodes (19): Dictionary, LedBrightness, LedProfile, BrightnessPercent, Colours, Effect, LedState, ActiveProfile (+11 more)

### Community 45 - ".LoadDefinitionsAsync"
Cohesion: 0.13
Nodes (15): Grid, Task, GpuPowerLimits, IEnumerable, PowerDefinition, PowerDefinitions, SetBy, Intel (+7 more)

### Community 46 - "CeilingTestWindow"
Cohesion: 0.15
Nodes (12): CancellationTokenSource, Button, CancellationToken, CheckBox, Func, StackPanel, Task, TextBlock (+4 more)

### Community 47 - "TestPreflight"
Cohesion: 0.14
Nodes (13): Display, IoCounters, ProcessLook, ConcurrentDictionary, Dictionary, DllImport, IntPtr, TimeSpan (+5 more)

### Community 48 - ".ModeName"
Cohesion: 0.15
Nodes (5): PowerModeChangedEventArgs, HashSet, Guid, Ours, Plan

### Community 49 - "Fact"
Cohesion: 0.11
Nodes (14): OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact, Task (+6 more)

### Community 50 - "Program"
Cohesion: 0.14
Nodes (5): ConsoleColor, B, G, R, Program

### Community 51 - "DevicePowerSaving"
Cohesion: 0.17
Nodes (12): TextBlock, ToggleButton, AcDc, Guid, IEnumerable, DevicePowerSaving, Off, DeviceSaving (+4 more)

### Community 52 - "Dependency"
Cohesion: 0.13
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 53 - ".Get"
Cohesion: 0.16
Nodes (12): ArgumentNullException, FirmwareModeReading, GpuMode, Discrete, Hybrid, Uma, GpuModeService, SwitchOutcome (+4 more)

### Community 54 - ".PlaceTourStep"
Cohesion: 0.11
Nodes (10): SizeChangedEventArgs, FrameworkElement, KeyEventArgs, RoutedEventArgs, TourStep, Body, SectionName, Title (+2 more)

### Community 55 - "FirmwareInventory"
Cohesion: 0.09
Nodes (22): IReadOnlyList, FirmwareInventory, BatteryChemistry, BatteryDesignMwh, BatteryName, BiosDate, BiosRelease, BiosVendor (+14 more)

### Community 56 - "ColourWheel"
Cohesion: 0.12
Nodes (13): BitmapSource, Control, Ellipse, MouseEventArgs, Canvas, DependencyProperty, Image, MouseButtonEventArgs (+5 more)

### Community 57 - ".Check"
Cohesion: 0.15
Nodes (8): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported, MailboxAccessTests

### Community 58 - "App"
Cohesion: 0.12
Nodes (8): The threat model, and what the code does about it, Application, DllImport, Mutex, App, OverlayRepairPending, StartupRegistration, IsEnabled

### Community 59 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 60 - ".For"
Cohesion: 0.22
Nodes (8): MemberData, Baseline, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 61 - "ToggleButton"
Cohesion: 0.09
Nodes (22): OverheatWarningToggle, SelectAll, SettingAskGameMode, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost (+14 more)

### Community 62 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 63 - "MachineScanRecord"
Cohesion: 0.09
Nodes (22): DateTime, List, MachineScanRecord, BiosDate, BiosRelease, BiosVendor, BiosVersion, BoardProduct (+14 more)

### Community 64 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 65 - ".ShowResult"
Cohesion: 0.19
Nodes (8): List, IEnumerable, IReadOnlyList, TimeSpan, CeilingStep, CeilingTest, Fact, CeilingTestTests

### Community 66 - "CpuPowerReader"
Cohesion: 0.22
Nodes (5): DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 67 - "ModeProfile"
Cohesion: 0.15
Nodes (12): AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, Baseline (+4 more)

### Community 68 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 69 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 70 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Ok, Task (+7 more)

### Community 71 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 72 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 73 - "DarkMenuColours"
Cohesion: 0.11
Nodes (19): ProfessionalColorTable, Color, DarkMenuColours, CheckBackground, CheckPressedBackground, CheckSelectedBackground, ImageMarginGradientBegin, ImageMarginGradientEnd (+11 more)

### Community 74 - "PowerOverlayService"
Cohesion: 0.21
Nodes (8): DllImport, Guid, IReadOnlyList, PowerModeOption, PowerOverlays, All, PowerOverlayService, IdempotenceTests

### Community 75 - "UpdateService"
Cohesion: 0.13
Nodes (12): DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService, Available, AvailableVersion (+4 more)

### Community 76 - "RuntimeDependencyTests"
Cohesion: 0.22
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 77 - "TemperatureFusion"
Cohesion: 0.25
Nodes (9): DateTime, TimeSpan, TemperatureFusion, Offset, RegisterSetAside, Value, DateTime, Fact (+1 more)

### Community 78 - "CoreLoad"
Cohesion: 0.15
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 79 - ".GetColour"
Cohesion: 0.24
Nodes (7): B, G, R, Fact, InlineData, Theory, LedStateTests

### Community 80 - "Unelevated"
Cohesion: 0.33
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 81 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 82 - "CpuLimitsTests"
Cohesion: 0.18
Nodes (6): ArgumentOutOfRangeException, Offset, Fact, CpuLimitsTests, ThermFlags, TjMax

### Community 83 - "CpuLimits"
Cohesion: 0.17
Nodes (10): Bit, PowerInfo, PowerLimits, Ratio, IReadOnlySet, CpuLimits, PowerInfo, PowerLimits (+2 more)

### Community 84 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 85 - "DevicePowerState"
Cohesion: 0.28
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 86 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 87 - "BatteryModePolicy"
Cohesion: 0.35
Nodes (5): BatteryModePolicy, QuietMode, Remembered, Fact, BatteryModePolicyTests

### Community 88 - "Hardware Protocol"
Cohesion: 0.14
Nodes (14): 1. Transport — ACPI-WMI mailbox, 3. Thermal / fan — `a1 = 0x0200`, 4. LED — `a1 = 0x0100`, 6. Safety rules, 7. Not supported, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`) (+6 more)

### Community 89 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 90 - "SystemMode"
Cohesion: 0.20
Nodes (10): InvalidOperationException, IReadOnlyList, SystemMode, Balanced, Gaming, Office, Performance, SystemModes (+2 more)

### Community 91 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 93 - "Log"
Cohesion: 0.26
Nodes (3): Exception, Log, Folder

### Community 94 - ".RunAndRecord"
Cohesion: 0.22
Nodes (6): JsonSerializerOptions, MachineScan, Firmware, RecordPath, Fact, MachineScanTests

### Community 95 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 96 - ".Write"
Cohesion: 0.23
Nodes (7): AcDc, Guid, IReadOnlyList, IdleSetting, DisplayOff, Sleep, IdleTimeouts

### Community 97 - ".Record"
Cohesion: 0.20
Nodes (7): IReadOnlyDictionary, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Fact, AuditLogTests

### Community 98 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 99 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 100 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 101 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 102 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 103 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 104 - "microsoft_win32"
Cohesion: 0.17
Nodes (5): microsoft_win32, Theme, Dark, Light, system_security

### Community 105 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 106 - "StackPanel"
Cohesion: 0.17
Nodes (12): BannerActions, CeilingRows, DefinitionRows, DialogActions, DriveTextStack, FnShortcutRows, IdleLoadRows, IdleRows (+4 more)

### Community 107 - "PackageTemperatureTests"
Cohesion: 0.41
Nodes (4): Fact, InlineData, Theory, PackageTemperatureTests

### Community 108 - "AcDc"
Cohesion: 0.29
Nodes (5): AcDc, InlineData, Theory, Fact, IdleTimeoutsTests

### Community 109 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 110 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 111 - "ThemeService"
Cohesion: 0.18
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 112 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 113 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 114 - ".Parse"
Cohesion: 0.29
Nodes (4): List, MemoryModule, Fact, FirmwareInventoryTests

### Community 115 - "SupportVerdict"
Cohesion: 0.22
Nodes (9): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites (+1 more)

### Community 116 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 117 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 118 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 119 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 120 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 121 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 123 - "LedEffect"
Cohesion: 0.22
Nodes (8): LedEffect, Blink, Breathing, ColourCycle, Heartbeat, Off, Static, Wave

### Community 124 - "ThermalSample"
Cohesion: 0.33
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 125 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 126 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 127 - "CeilingTests"
Cohesion: 0.31
Nodes (5): Baseline, Fact, InlineData, Theory, CeilingTests

### Community 128 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 129 - "Nextcalibur 0.5.5"
Cohesion: 0.25
Nodes (6): Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture

### Community 130 - "Toasts"
Cohesion: 0.32
Nodes (5): Action, Toasts, Balloon, windows_data_xml_dom, windows_ui_notifications

### Community 131 - ".Recommended"
Cohesion: 0.29
Nodes (5): Min, Fact, InlineData, Theory, IdleRecommendedTests

### Community 132 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 133 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 134 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 135 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 136 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 137 - ".ForegroundProcess"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Name, Pid

### Community 138 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 139 - ".Current"
Cohesion: 0.48
Nodes (3): DllImport, IntPtr, ScreenSaver

### Community 140 - ".Values_are_the_ones_measured_from_the_vendor"
Cohesion: 0.33
Nodes (4): Fact, InlineData, Theory, ThermalProfileTests

### Community 141 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 142 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 143 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 144 - "Nextcalibur 0.7.0"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.7.0, Tested on, Updated

### Community 145 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 146 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 147 - "ComboBox"
Cohesion: 0.33
Nodes (6): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, SettingNvPower, ComboBox

### Community 148 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 149 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 150 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 152 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 153 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 154 - "Nextcalibur 0.7.1"
Cohesion: 0.40
Nodes (4): Fixed, New, Nextcalibur 0.7.1, Tested on

### Community 155 - ".PowerModeControls"
Cohesion: 0.40
Nodes (4): Option, Button, IEnumerable, TextBlock

### Community 156 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 157 - "SmiSubsystem"
Cohesion: 0.40
Nodes (5): SmiSubsystem, DisplayMode, Led, Profile, Thermal

### Community 160 - "PawnIoModuleTests.cs"
Cohesion: 0.40
Nodes (3): InlineData, Theory, PawnIoModuleTests

### Community 161 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 163 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 164 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 165 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 167 - "2. Command structure"
Cohesion: 0.50
Nodes (4): 2. Command structure, Call sequence, Command families (`a0`), Subsystems (`a1`)

### Community 168 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 169 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 170 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 171 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 172 - ".DecodeLimitReasons"
Cohesion: 0.50
Nodes (3): Now, SinceBoot, IReadOnlyList

### Community 174 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 177 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 178 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

### Community 179 - "EffectPanel"
Cohesion: 0.67
Nodes (3): EffectPanel, ProfileRow, UniformGrid

### Community 180 - "PART_ContentHost"
Cohesion: 0.67
Nodes (3): PART_ContentHost, SettingsScroll, ScrollViewer

### Community 181 - "SmiFamily"
Cohesion: 0.67
Nodes (3): SmiFamily, Read, Write

## Knowledge Gaps
- **608 isolated node(s):** `OverlayRepairPending`, `Accepted`, `SelectedColour`, `VisualChildrenCount`, `Diameter` (+603 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1170 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **31 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `Nextcalibur 0.5.5`, `AppSettings`, `FnShortcut`, `GpuClockReader`, `.Get`, `.CheckProgram`, `ProgramRule`, `Nextcalibur.Core.Configuration`, `.ForegroundProcess`, `TourPage`, `RunawayWatchTests`, `WindowsFaults`, `.WireSettingsPage`, `Window`, `RadioButton`, `.OnBrightnessChanged`, `.Info`, `PlanTuning`, `ResourceDictionary`, `LedZone`, `CoolingPolicyTests`, `.OnLoaded`, `.PowerModeControls`, `TrayPresence`, `.RequestRestart`, `SystemInfo`, `UserPresence`, `Fact`, `Button`, `EcMailbox`, `.Choose`, `.RuleRow`, `SystemModeService`, `.LoadDefinitionsAsync`, `CeilingTestWindow`, `.ModeName`, `DevicePowerSaving`, `.Get`, `.PlaceTourStep`, `CpuPowerReader`, `ModeProfile`, `DependencyStatus`, `BacklightKeyWatcher`, `PowerOverlayService`, `UpdateService`, `TemperatureFusion`, `CoreLoad`, `LedController`, `BatteryModePolicy`, `SystemMode`, `.ApplyFnShortcuts`, `.Write`, `Grid`, `Strings`, `ThemeService`, `NvidiaDriverState`, `SupportVerdict`, `LedEffect`, `ThermalSample`?**
  _High betweenness centrality (0.473) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Text`, `RadioButton`, `.OnBrightnessChanged`, `ComboBox`, `Button`, `Keycaps`, `DriveGauge`, `CpuWarnValue`, `DialogCheck`, `EffectPanel`, `PART_ContentHost`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `TourCanvas`, `ToggleButton`, `Wheel`, `Grid`, `StackPanel`, `Border`?**
  _High betweenness centrality (0.074) - this node is a cross-community bridge._
- **Why does `DependencyStatus` connect `DependencyStatus` to `MainWindow`, `.OnLoaded`, `Nextcalibur.Core.Security`, `Dependency`?**
  _High betweenness centrality (0.061) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `OverlayRepairPending`, `Accepted`, `SelectedColour` to the rest of the system?**
  _608 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.02274373460814139 - nodes in this community are weakly interconnected._
- **Should `AppSettings` be split into smaller, more focused modules?**
  _Cohesion score 0.02967121090617482 - nodes in this community are weakly interconnected._