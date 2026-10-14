# Graph Report - nextcalibur-control-center  (2026-10-14)

## Corpus Check
- 201 files · ~249,771 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3430 nodes · 7580 edges · 198 communities (164 shown, 34 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 456 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `64ed7dcb`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- AppSettings
- .Get
- GpuClockReader
- RunawayWatchTests
- ProtectedStore
- ProgramRule
- IShellLinkW
- FnShortcut
- Window
- ProcessPresence
- Nextcalibur.App
- LedState
- xunit
- PlanTuning
- Nextcalibur.Core.Hardware
- .OnLoaded
- CoolingPolicyTests
- SystemInfo
- RadioButton
- NetworkCostTests
- .RequestRestart
- .Warn
- .ApplyFnShortcuts
- .Survey
- CeilingTestWindow
- .Check
- TrayPresence
- WindowsFaults
- Button
- CpuLimits
- SystemTools
- .For
- LedController
- Api
- ResourceDictionary
- Fact
- WindowsFaultsTests
- Program
- Elevation
- .SignatureIsValid
- Nextcalibur.Core.Configuration
- EcMailbox
- .CheckProgram
- SystemModeService
- .ShowResult
- Dependency
- Authenticode
- .PlaceTourStep
- .Processor
- FirmwareInventory
- CpuPowerReader
- TestPreflight
- .Ask
- .Read
- DotNetRuntimeDependency
- Fact
- Nextcalibur.Core.csproj
- .RefreshIdleRows
- DevicePowerSaving
- ToggleButton
- MachineScanRecord
- RestorePoint
- BacklightKeyWatcher
- MemoryTrimmer
- DependencyStatus
- .Dictionary
- GpuModeService
- analyse-autopsy.py
- ModeProfileTests
- RuntimeDependencyTests
- TemperatureFusion
- system_text_json
- Integrity
- UserPresence
- PowerOverlayService
- ColourWheel
- DevicePowerState
- Unelevated
- CoreLoad
- UpdateService
- .IsUnderProgramFiles
- .OnClosing
- FanGauge
- SystemMode
- .RunawayTick
- .LoadSystemMode
- DonutGauge
- .RunAndRecord
- BatteryModePolicy
- IdleSetting
- SegmentedBar
- Nextcalibur Control Center
- ProcessMetrics
- LogInjectionTests
- Nextcalibur.Core.Security
- AcDc
- Grid
- .OnEffectChecked
- InstallFolderGuard
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- Strings
- PackageTemperatureTests
- SmiCommandTests
- Hardware Protocol
- Nextcalibur 0.5.3
- StackPanel
- SupportVerdict
- MachineWideGate
- .WireTypedThreshold
- Border
- ThemeService
- NvidiaDriverState
- .Parse
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- Log
- PawnIoDependency
- .IsSystemFileSignedBy
- Probe
- CeilingTests
- Nextcalibur 0.5.1
- NotificationState
- IntegrityKind
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- .Pick
- .ForegroundProcess
- TourPage
- .Entries
- Log-Graphics.ps1
- Privacy
- Nextcalibur 0.6.2
- Security
- .LoadPowerModes
- ComboBox
- RunningKind
- PowerSource
- SystemModesTests
- Notice
- Nextcalibur 0.5.11
- .ChooseTourCardPosition
- AppIdentity.cs
- .Read
- .Set
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- Nextcalibur 0.6.1
- .ArrangeOverride
- BrightnessSlider
- .Modules_are_the_released_ones
- Tools
- 0.5.7.md
- .OnSessionSwitch
- CpuWarnValue
- DialogCheck
- .ShowOverlayDialog
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
1. `MainWindow` - 395 edges
2. `Window` - 188 edges
3. `AppSettings` - 85 edges
4. `Nextcalibur.Core.Hardware` - 69 edges
5. `SystemMode` - 66 edges
6. `Nextcalibur.Core.Power` - 43 edges
7. `RadioButton` - 41 edges
8. `Nextcalibur.Core.Configuration` - 41 edges
9. `WindowsFaultsTests` - 40 edges
10. `TextBlock` - 39 edges

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

## Communities (198 total, 34 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.03
Nodes (72): DeferredOverheat, GameEnd, GameSession, HwndSource, ObservableCollection, PluggedIn, Queue, SolidColorBrush (+64 more)

### Community 1 - "AppSettings"
Cohesion: 0.03
Nodes (63): DateTime, Dictionary, FnShortcut, JsonElement, Limits, List, ProgramRule, SystemMode (+55 more)

### Community 2 - ".Get"
Cohesion: 0.07
Nodes (13): Dictionary, IReadOnlyDictionary, Task, ModeDiscrete, ModeHybrid, ModeUma, Guid, Ours (+5 more)

### Community 3 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 4 - "RunawayWatchTests"
Cohesion: 0.06
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 5 - "ProtectedStore"
Cohesion: 0.09
Nodes (20): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+12 more)

### Community 6 - "ProgramRule"
Cohesion: 0.08
Nodes (25): GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, IEnumerable (+17 more)

### Community 7 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 8 - "FnShortcut"
Cohesion: 0.08
Nodes (25): DllImport, IEnumerable, IntPtr, List, StringBuilder, FnAction, FanTurbo, KeyboardBacklight (+17 more)

### Community 9 - "Window"
Cohesion: 0.08
Nodes (46): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+38 more)

### Community 10 - "ProcessPresence"
Cohesion: 0.08
Nodes (18): DateTime, IReadOnlyList, MachineHistory, DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence (+10 more)

### Community 11 - "Nextcalibur.App"
Cohesion: 0.10
Nodes (25): Nextcalibur.App, Nextcalibur.App.Controls, system_componentmodel, system_io, system_linq, system_runtime_compilerservices, system_windows, system_windows_controls (+17 more)

### Community 12 - "LedState"
Cohesion: 0.08
Nodes (26): B, Dictionary, G, LedBrightness, R, LedProfile, BrightnessPercent, Colours (+18 more)

### Community 13 - "xunit"
Cohesion: 0.10
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, nextcalibur_core_power_plantuning_acdc, system_text_regularexpressions, system_xml_linq, xunit

### Community 14 - "PlanTuning"
Cohesion: 0.13
Nodes (18): Floor, Marker, Original, Action, AcDc, DllImport, Guid, IEnumerable (+10 more)

### Community 15 - "Nextcalibur.Core.Hardware"
Cohesion: 0.09
Nodes (9): Nextcalibur.Cli, Nextcalibur.Core.Input, Nextcalibur.Core.Hardware, microsoft_win32_safehandles, system_diagnostics, system_management, system_reflection, system_runtime_interopservices (+1 more)

### Community 16 - ".OnLoaded"
Cohesion: 0.08
Nodes (11): AssemblyInformationalVersionAttribute, Asynchronous dispatcher & UI thread hardening, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Task, Task, Action (+3 more)

### Community 17 - "CoolingPolicyTests"
Cohesion: 0.08
Nodes (26): At, Margin, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc (+18 more)

### Community 18 - "SystemInfo"
Cohesion: 0.08
Nodes (21): MemoryStatusEx, DriveUse, INotifyPropertyChanged, DriveRow, Name, Percent, PercentText, DllImport (+13 more)

### Community 19 - "RadioButton"
Cohesion: 0.07
Nodes (33): ModeBalanced, ModeGaming, ModeOffice, ModePerformance, NavDisplay, NavLighting, NavPower, NavSettings (+25 more)

### Community 20 - "NetworkCostTests"
Cohesion: 0.11
Nodes (26): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+18 more)

### Community 21 - ".RequestRestart"
Cohesion: 0.09
Nodes (13): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, BannerRestartButton, DllImport, EventArgs (+5 more)

### Community 22 - ".Warn"
Cohesion: 0.12
Nodes (9): The threat model, and what the code does about it, Application, DllImport, Mutex, App, OverlayRepairPending, RegistryKey, Footprint (+1 more)

### Community 23 - ".ApplyFnShortcuts"
Cohesion: 0.09
Nodes (14): Problem, Detail, Dictionary, DllImport, IntPtr, KeyEventArgs, ShortcutProblem, Duplicate (+6 more)

### Community 24 - ".Survey"
Cohesion: 0.11
Nodes (8): Func, IEnumerable, IReadOnlyList, RetirementEntry, Trace, Version, NduFix, FootprintTests

### Community 25 - "CeilingTestWindow"
Cohesion: 0.11
Nodes (17): CancellationTokenSource, Button, CancellationToken, CheckBox, Func, StackPanel, Task, TextBlock (+9 more)

### Community 26 - ".Check"
Cohesion: 0.11
Nodes (14): CommonAce, RawSecurityDescriptor, RegistryKey, SecurityIdentifier, MailboxAccess, MailboxAvailability, AccessNotGranted, Available (+6 more)

### Community 27 - "TrayPresence"
Cohesion: 0.09
Nodes (13): ContextMenuStrip, Deep memory & resource leak eradication, EventHandler, Icon, Item, NotifyIcon, EventArgs, List (+5 more)

### Community 28 - "WindowsFaults"
Cohesion: 0.12
Nodes (20): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, DateTime, DllImport, Func (+12 more)

### Community 29 - "Button"
Cohesion: 0.07
Nodes (25): IsChecked, BannerDismissButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, HideToTrayButton, MinimiseButton (+17 more)

### Community 30 - "CpuLimits"
Cohesion: 0.09
Nodes (18): Bit, IReadOnlySet, Now, Offset, PowerInfo, PowerLimits, Ratio, SinceBoot (+10 more)

### Community 31 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 32 - ".For"
Cohesion: 0.12
Nodes (13): AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, Baseline (+5 more)

### Community 33 - "LedController"
Cohesion: 0.11
Nodes (18): LedController, EffectiveBrightnessPercent, HardwareLevel, State, LedEffect, Blink, Breathing, ColourCycle (+10 more)

### Community 34 - "Api"
Cohesion: 0.10
Nodes (23): Api, GetSettingFn, InitializeFn, ProfileOutFn, SessionFn, SessionOutFn, Set, SetSettingFn (+15 more)

### Community 35 - "ResourceDictionary"
Cohesion: 0.08
Nodes (29): ActualWidth, IsDropDownOpen, Arrow, Bg, CardBorder, Indicator, Knob, PART_EditableTextBox (+21 more)

### Community 36 - "Fact"
Cohesion: 0.14
Nodes (9): IList, GpuLoad, GpuConfiguration, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests (+1 more)

### Community 37 - "WindowsFaultsTests"
Cohesion: 0.20
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 38 - "Program"
Cohesion: 0.13
Nodes (8): ConsoleColor, B, G, R, Program, DateTimeOffset, ThermalReader, ThermalSample

### Community 39 - "Elevation"
Cohesion: 0.14
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 40 - ".SignatureIsValid"
Cohesion: 0.14
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 41 - "Nextcalibur.Core.Configuration"
Cohesion: 0.10
Nodes (11): Nextcalibur.Core, Nextcalibur.Core.Configuration, microsoft_win32, Theme, Dark, Light, system_drawing, system_globalization (+3 more)

### Community 42 - "EcMailbox"
Cohesion: 0.16
Nodes (10): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+2 more)

### Community 43 - ".CheckProgram"
Cohesion: 0.16
Nodes (8): ProcessModule, IReadOnlyList, IntegrityFinding, Fact, IntegrityTests, Current, Exe, Manifest

### Community 44 - "SystemModeService"
Cohesion: 0.19
Nodes (8): Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, Ours, Plan, SystemModeService

### Community 45 - ".ShowResult"
Cohesion: 0.15
Nodes (10): List, IEnumerable, IReadOnlyList, TimeSpan, CeilingStep, CeilingTest, Fact, InlineData (+2 more)

### Community 46 - "Dependency"
Cohesion: 0.13
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 47 - "Authenticode"
Cohesion: 0.25
Nodes (14): CatalogInfo, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle, Authenticode (+6 more)

### Community 48 - ".PlaceTourStep"
Cohesion: 0.11
Nodes (10): SizeChangedEventArgs, FrameworkElement, KeyEventArgs, RoutedEventArgs, TourStep, Body, SectionName, Title (+2 more)

### Community 49 - ".Processor"
Cohesion: 0.15
Nodes (13): GpuPowerLimits, IEnumerable, PowerDefinition, PowerDefinitions, SetBy, Intel, LaptopMaker, Nextcalibur (+5 more)

### Community 50 - "FirmwareInventory"
Cohesion: 0.09
Nodes (22): IReadOnlyList, FirmwareInventory, BatteryChemistry, BatteryDesignMwh, BatteryName, BiosDate, BiosRelease, BiosVendor (+14 more)

### Community 51 - "CpuPowerReader"
Cohesion: 0.21
Nodes (6): ArgumentOutOfRangeException, DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 52 - "TestPreflight"
Cohesion: 0.11
Nodes (16): Display, How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow (+8 more)

### Community 53 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 54 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 55 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 56 - "Fact"
Cohesion: 0.12
Nodes (13): IReadOnlyList, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact (+5 more)

### Community 57 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 58 - ".RefreshIdleRows"
Cohesion: 0.16
Nodes (6): ComboBox, Grid, Guid, DllImport, IntPtr, ScreenSaver

### Community 59 - "DevicePowerSaving"
Cohesion: 0.20
Nodes (10): AcDc, Guid, IEnumerable, DevicePowerSaving, Off, DeviceSaving, Usb, Wifi (+2 more)

### Community 60 - "ToggleButton"
Cohesion: 0.10
Nodes (20): OverheatWarningToggle, SelectAll, SettingAskGameMode, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost (+12 more)

### Community 61 - "MachineScanRecord"
Cohesion: 0.10
Nodes (21): DateTime, List, MachineScanRecord, BiosDate, BiosRelease, BiosVendor, BiosVersion, BoardProduct (+13 more)

### Community 62 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 63 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 64 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 65 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 66 - ".Dictionary"
Cohesion: 0.15
Nodes (9): IReadOnlyDictionary, Stable, Dictionary, Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests (+1 more)

### Community 67 - "GpuModeService"
Cohesion: 0.20
Nodes (9): ArgumentNullException, FirmwareModeReading, GpuMode, Discrete, Hybrid, Uma, GpuModeService, SwitchOutcome (+1 more)

### Community 68 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 69 - "ModeProfileTests"
Cohesion: 0.22
Nodes (7): MemberData, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 70 - "RuntimeDependencyTests"
Cohesion: 0.22
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 71 - "TemperatureFusion"
Cohesion: 0.25
Nodes (9): DateTime, TimeSpan, TemperatureFusion, Offset, RegisterSetAside, Value, DateTime, Fact (+1 more)

### Community 72 - "system_text_json"
Cohesion: 0.20
Nodes (6): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_text_json

### Community 73 - "Integrity"
Cohesion: 0.14
Nodes (8): Lazy, Dictionary, HashSet, Integrity, NvapiPath, NvmlPath, InlineData, Theory

### Community 74 - "UserPresence"
Cohesion: 0.29
Nodes (7): MonitorInfo, DateTime, DllImport, IntPtr, MarshalAs, StringBuilder, UserPresence

### Community 75 - "PowerOverlayService"
Cohesion: 0.25
Nodes (6): DllImport, Guid, PowerModeOption, PowerOverlays, All, PowerOverlayService

### Community 76 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 77 - "DevicePowerState"
Cohesion: 0.25
Nodes (6): DevPropKey, Discrete GPU sleep protection in Hybrid mode, DllImport, Guid, DevicePowerState, DevPropKey

### Community 78 - "Unelevated"
Cohesion: 0.33
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 79 - "CoreLoad"
Cohesion: 0.13
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 80 - "UpdateService"
Cohesion: 0.17
Nodes (12): DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService, Available, AvailableVersion (+4 more)

### Community 81 - ".IsUnderProgramFiles"
Cohesion: 0.21
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 82 - ".OnClosing"
Cohesion: 0.17
Nodes (6): CancelEventArgs, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner

### Community 83 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 84 - "SystemMode"
Cohesion: 0.18
Nodes (10): InvalidOperationException, IReadOnlyList, SystemMode, Balanced, Gaming, Office, Performance, SystemModes (+2 more)

### Community 86 - ".LoadSystemMode"
Cohesion: 0.20
Nodes (4): PowerModeChangedEventArgs, Guid, Ours, Plan

### Community 87 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 88 - ".RunAndRecord"
Cohesion: 0.21
Nodes (6): JsonSerializerOptions, MachineScan, Firmware, RecordPath, Fact, MachineScanTests

### Community 89 - "BatteryModePolicy"
Cohesion: 0.35
Nodes (5): BatteryModePolicy, QuietMode, Remembered, Fact, BatteryModePolicyTests

### Community 90 - "IdleSetting"
Cohesion: 0.24
Nodes (8): AcDc, Guid, IReadOnlyList, IdleSetting, DisplayOff, Sleep, IdleTimeouts, Timeouts

### Community 91 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 92 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 93 - "ProcessMetrics"
Cohesion: 0.15
Nodes (13): Share, Dictionary, TimeSpan, FaultEvidence, ProcessMetrics, Name, PageFaultCount, SessionId (+5 more)

### Community 94 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 95 - "Nextcalibur.Core.Security"
Cohesion: 0.27
Nodes (5): Nextcalibur.Core.Security, system_security_accesscontrol, system_security_cryptography, system_security_cryptography_x509certificates, system_security_principal

### Community 96 - "AcDc"
Cohesion: 0.26
Nodes (5): AcDc, InlineData, Theory, Fact, IdleTimeoutsTests

### Community 97 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 98 - ".OnEffectChecked"
Cohesion: 0.17
Nodes (8): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, LedPower, ReloadButton

### Community 99 - "InstallFolderGuard"
Cohesion: 0.33
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

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

### Community 104 - "Nextcalibur 0.5.4"
Cohesion: 0.17
Nodes (11): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+3 more)

### Community 105 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 106 - "PackageTemperatureTests"
Cohesion: 0.41
Nodes (4): Fact, InlineData, Theory, PackageTemperatureTests

### Community 107 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 108 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 109 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 110 - "StackPanel"
Cohesion: 0.18
Nodes (11): BannerActions, CeilingRows, DialogActions, DriveTextStack, FnShortcutRows, IdleRows, PanelWindowsFaultsSubOptions, ProfileRow (+3 more)

### Community 111 - "SupportVerdict"
Cohesion: 0.20
Nodes (10): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+2 more)

### Community 112 - "MachineWideGate"
Cohesion: 0.20
Nodes (6): Mutex, TimeSpan, MachineWideGate, system_threading, Task, MachineWideGateTests

### Community 113 - ".WireTypedThreshold"
Cohesion: 0.22
Nodes (4): RoutedPropertyChangedEventArgs, Slider, Action, TextBox

### Community 114 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 115 - "ThemeService"
Cohesion: 0.20
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 116 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 117 - ".Parse"
Cohesion: 0.29
Nodes (4): List, MemoryModule, Fact, FirmwareInventoryTests

### Community 118 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 119 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 120 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 121 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 122 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 124 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 125 - ".IsSystemFileSignedBy"
Cohesion: 0.39
Nodes (3): Fact, SystemFileTrustTests, Cmd

### Community 126 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 127 - "CeilingTests"
Cohesion: 0.31
Nodes (5): Baseline, Fact, InlineData, Theory, CeilingTests

### Community 128 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 129 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 130 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 131 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 132 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 133 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 134 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 135 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 136 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 137 - ".ForegroundProcess"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Name, Pid

### Community 138 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 139 - ".Entries"
Cohesion: 0.38
Nodes (3): IReadOnlyList, Fact, AuditLogTests

### Community 140 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 141 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 142 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 143 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 144 - ".LoadPowerModes"
Cohesion: 0.33
Nodes (3): Option, Button, IEnumerable

### Community 145 - "ComboBox"
Cohesion: 0.33
Nodes (6): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, SettingNvPower, ComboBox

### Community 146 - "RunningKind"
Cohesion: 0.33
Nodes (6): RunningItem, RunningKind, App, Background, Service, Transfer

### Community 147 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 149 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 150 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 151 - ".ChooseTourCardPosition"
Cohesion: 0.40
Nodes (4): Rect, Point, Size, MonitorInfo

### Community 152 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 155 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 157 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 158 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 159 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 160 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 161 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 162 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 163 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 165 - "BrightnessSlider"
Cohesion: 0.50
Nodes (4): BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 166 - ".Modules_are_the_released_ones"
Cohesion: 0.50
Nodes (3): InlineData, Theory, PawnIoModuleTests

### Community 167 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 171 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 172 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

### Community 174 - "Words"
Cohesion: 0.67
Nodes (3): Func, Words, Resolver

## Knowledge Gaps
- **560 isolated node(s):** `OverlayRepairPending`, `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum` (+555 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1071 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **34 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `AppSettings`, `.Get`, `GpuClockReader`, `RunawayWatchTests`, `ProgramRule`, `FnShortcut`, `.ForegroundProcess`, `TourPage`, `ProcessPresence`, `Nextcalibur.App`, `Window`, `PlanTuning`, `Nextcalibur.Core.Hardware`, `.OnLoaded`, `CoolingPolicyTests`, `SystemInfo`, `RadioButton`, `.LoadPowerModes`, `.RequestRestart`, `.ApplyFnShortcuts`, `.ChooseTourCardPosition`, `CeilingTestWindow`, `TrayPresence`, `Button`, `.For`, `LedController`, `Fact`, `Program`, `Nextcalibur.Core.Configuration`, `.OnSessionSwitch`, `EcMailbox`, `SystemModeService`, `.ShowOverlayDialog`, `.CheckProgram`, `.PlaceTourStep`, `CpuPowerReader`, `.Read`, `.RefreshIdleRows`, `DevicePowerSaving`, `ToggleButton`, `BacklightKeyWatcher`, `DependencyStatus`, `GpuModeService`, `TemperatureFusion`, `PowerOverlayService`, `UpdateService`, `.OnClosing`, `SystemMode`, `.RunawayTick`, `.LoadSystemMode`, `BatteryModePolicy`, `IdleSetting`, `AcDc`, `Grid`, `.OnEffectChecked`, `Strings`, `SupportVerdict`, `.WireTypedThreshold`, `ThemeService`, `NvidiaDriverState`?**
  _High betweenness centrality (0.478) - this node is a cross-community bridge._
- **Why does `Nextcalibur.Core.Hardware` connect `Nextcalibur.Core.Hardware` to `RunawayWatchTests`, `Program`, `system_text_json`, `Nextcalibur.Core.Configuration`, `EcMailbox`, `Nextcalibur.App`, `xunit`, `SupportVerdict`, `MachineWideGate`, `.Processor`, `Nextcalibur.Core.Security`, `CpuLimits`, `BacklightKeyWatcher`?**
  _High betweenness centrality (0.098) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `.Get`, `Text`, `ComboBox`, `RadioButton`, `.RequestRestart`, `Button`, `Keycaps`, `DriveGauge`, `BrightnessSlider`, `CpuWarnValue`, `DialogCheck`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `ToggleButton`, `Grid`, `.OnEffectChecked`, `StackPanel`, `Border`?**
  _High betweenness centrality (0.094) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `OverlayRepairPending`, `SelectedColour`, `VisualChildrenCount` to the rest of the system?**
  _560 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.025482093663911846 - nodes in this community are weakly interconnected._
- **Should `AppSettings` be split into smaller, more focused modules?**
  _Cohesion score 0.033458177278401995 - nodes in this community are weakly interconnected._