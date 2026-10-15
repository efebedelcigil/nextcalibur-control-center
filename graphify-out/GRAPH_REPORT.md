# Graph Report - nextcalibur-control-center  (2026-10-15)

## Corpus Check
- 219 files · ~265,862 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3735 nodes · 8310 edges · 209 communities (176 shown, 33 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 507 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `cac7bfe3`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- AppSettings
- FnShortcut
- .Get
- Nextcalibur.Core.Configuration
- Window
- ProgramRule
- GpuClockReader
- ProtectedStore
- RunawayWatchTests
- .Info
- xunit
- WindowsFaults
- IShellLinkW
- RadioButton
- ProcessPresence
- Nextcalibur.Core.Hardware
- PlanTuning
- .StartSlowTimer
- TextBlock
- .OnLoaded
- WindowsFaultsTests
- CoolingPolicyTests
- GameLibrary
- NetworkCostTests
- .Survey
- EcMailbox
- SystemInfo
- UserPresence
- .Check
- CrashGuard
- CpuLimits
- Nextcalibur.Core.Security
- SystemTools
- .RequestRestart
- .StartProgramRules
- InstallFolderGuard
- .Choose
- Api
- .Get
- ColourWheel
- Elevation
- LedState
- ResourceDictionary
- .Main
- Dependency
- TrayPresence
- Integrity
- .For
- SystemModeService
- .SignatureIsValid
- .RefreshIdleRows
- PowerOverlayService
- Fact
- DevicePowerSaving
- Authenticode
- TestPreflight
- .Processor
- FirmwareInventory
- CpuPowerReader
- DotNetRuntimeDependency
- MachineScanRecord
- RestorePoint
- Nextcalibur.Core.csproj
- .ShowResult
- .LoadDefinitionsAsync
- .ApplyFnShortcuts
- Program
- BacklightKeyWatcher
- .Ask
- MemoryTrimmer
- DependencyStatus
- Fact
- analyse-autopsy.py
- ModeProfileTests
- DarkMenuColours
- UpdateService
- .CheckProgram
- TemperatureFusion
- CeilingTestWindow
- RuntimeDependencyTests
- CoreLoad
- LedController
- FanGauge
- DevicePowerState
- SystemMode
- Unelevated
- DonutGauge
- ThemeService
- Integrity.cs
- Log
- BatteryModePolicy
- SegmentedBar
- .Set
- Nextcalibur Control Center
- .Write
- LogInjectionTests
- Grid
- LedEffect
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- .Pick
- Strings
- StackPanel
- PackageTemperatureTests
- .Check
- AcDc
- SmiCommandTests
- Hardware Protocol
- Nextcalibur 0.5.3
- NoticeGate
- Border
- .Run
- .Sanitised
- NvidiaDriverState
- .Parse
- WordsInCodeTests
- IntPtr
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- ThermalSample
- PawnIoDependency
- .RunAndRecord
- .IsSystemFileSignedBy
- Probe
- CeilingTests
- Nextcalibur 0.5.1
- Nextcalibur 0.5.5
- Toasts
- .Recommended
- IntegrityKind
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- .RunStepsAsync
- ColGauge
- TourPage
- .Current
- BatteryModePolicy.cs
- .Entries
- .Values_are_the_ones_measured_from_the_vendor
- Log-Graphics.ps1
- Privacy
- Nextcalibur 0.6.2
- Nextcalibur 0.7.0
- Security
- .OnBrightnessChanged
- ComboBox
- LedZone
- .Load
- MachineWideGate
- SystemModesTests
- Notice
- Nextcalibur 0.5.11
- Nextcalibur 0.7.1
- AppIdentity.cs
- PART_Track
- .Set
- SmiSubsystem
- .FirmwareChanged
- .A_polling_interval_from_the_file_is_a_sane_one
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- Nextcalibur 0.6.1
- .TryParseRgb
- .CalculateThreadShare
- Tools
- Toggle
- 0.5.7.md
- CpuWarnValue
- EffectPanel
- .OnMinimiseToTrayClick
- PART_ContentHost
- .GetSystemFirmwareTable
- .Work_that_must_not_be_interrupted
- .LoadIcon
- DialogProgress
- DriveDot
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
1. `MainWindow` - 446 edges
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

## Communities (209 total, 33 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.02
Nodes (92): Added, DeferredOverheat, Found, GameSession, HwndSource, ObservableCollection, PluggedIn, Queue (+84 more)

### Community 1 - "AppSettings"
Cohesion: 0.03
Nodes (72): DateTime, Dictionary, FnShortcut, JsonElement, Limits, List, ProgramRule, SystemMode (+64 more)

### Community 2 - "FnShortcut"
Cohesion: 0.05
Nodes (42): INotifyPropertyChanged, Problem, DriveRow, Detail, Name, Percent, PercentText, Dictionary (+34 more)

### Community 3 - ".Get"
Cohesion: 0.07
Nodes (12): GameEnd, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary, IReadOnlyDictionary (+4 more)

### Community 4 - "Nextcalibur.Core.Configuration"
Cohesion: 0.08
Nodes (28): Nextcalibur.App, Nextcalibur.App.Controls, Nextcalibur.Core, Nextcalibur.Core.Configuration, system_componentmodel, system_drawing, system_globalization, system_io (+20 more)

### Community 5 - "Window"
Cohesion: 0.06
Nodes (58): TemperatureToDoubleConverter, Foreground, IsChecked, IsMouseOver, ItemsSource.Count, Name, BannerDismissButton, BannerRestartButton (+50 more)

### Community 6 - "ProgramRule"
Cohesion: 0.06
Nodes (33): HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, GeneratedRegex, IEnumerable (+25 more)

### Community 7 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 8 - "ProtectedStore"
Cohesion: 0.09
Nodes (20): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+12 more)

### Community 9 - "RunawayWatchTests"
Cohesion: 0.07
Nodes (34): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+26 more)

### Community 10 - ".Info"
Cohesion: 0.07
Nodes (14): Option, PowerModeChangedEventArgs, Action, Guid, Ours, Plan, Button, Guid (+6 more)

### Community 11 - "xunit"
Cohesion: 0.09
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, nextcalibur_core_power_plantuning_acdc, system_text_regularexpressions, system_xml_linq, xunit

### Community 12 - "WindowsFaults"
Cohesion: 0.08
Nodes (34): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+26 more)

### Community 13 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 14 - "RadioButton"
Cohesion: 0.06
Nodes (42): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeBalanced, ModeDiscrete (+34 more)

### Community 15 - "ProcessPresence"
Cohesion: 0.09
Nodes (18): DateTime, IReadOnlyList, MachineHistory, DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence (+10 more)

### Community 16 - "Nextcalibur.Core.Hardware"
Cohesion: 0.09
Nodes (11): Nextcalibur.Cli, Nextcalibur.Core.Input, Nextcalibur.Core.Hardware, microsoft_win32, microsoft_win32_safehandles, Trace, system_diagnostics, system_management (+3 more)

### Community 17 - "PlanTuning"
Cohesion: 0.13
Nodes (17): Floor, Marker, Original, AcDc, DllImport, Guid, IEnumerable, IntPtr (+9 more)

### Community 18 - ".StartSlowTimer"
Cohesion: 0.06
Nodes (7): SolidColorBrush, DllImport, IntPtr, Name, Pid, Func, TextBlock

### Community 19 - "TextBlock"
Cohesion: 0.05
Nodes (41): Detail, PercentText, BannerBody, BannerTitle, BrightnessValue, CpuClock, CpuFan, CpuTemp (+33 more)

### Community 20 - ".OnLoaded"
Cohesion: 0.09
Nodes (7): Asynchronous dispatcher & UI thread hardening, Slider, Task, Task, EventArgs, IProgress, TextBox

### Community 21 - "WindowsFaultsTests"
Cohesion: 0.13
Nodes (4): FaultEvaluation, Fact, WindowsFaultsTests, Dictionary

### Community 22 - "CoolingPolicyTests"
Cohesion: 0.08
Nodes (26): At, Margin, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc (+18 more)

### Community 23 - "GameLibrary"
Cohesion: 0.11
Nodes (14): Dictionary, GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, IReadOnlyList, Path, Regex (+6 more)

### Community 24 - "NetworkCostTests"
Cohesion: 0.11
Nodes (26): Answer, Bytes, Count, HttpMessageHandler, CancellationToken, ConcurrentDictionary, HttpClient, Task (+18 more)

### Community 25 - ".Survey"
Cohesion: 0.10
Nodes (9): Func, IEnumerable, IReadOnlyList, RegistryKey, Version, Footprint, RetirementEntry, NduFix (+1 more)

### Community 26 - "EcMailbox"
Cohesion: 0.12
Nodes (14): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+6 more)

### Community 27 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 28 - "UserPresence"
Cohesion: 0.13
Nodes (14): LastInputInfo, MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr (+6 more)

### Community 29 - ".Check"
Cohesion: 0.11
Nodes (14): CommonAce, RawSecurityDescriptor, RegistryKey, SecurityIdentifier, MailboxAccess, MailboxAvailability, AccessNotGranted, Available (+6 more)

### Community 30 - "CrashGuard"
Cohesion: 0.11
Nodes (19): Session, DateTimeOffset, Exception, List, TimeSpan, CrashGuard, SafeMode, UncleanPrevious (+11 more)

### Community 31 - "CpuLimits"
Cohesion: 0.09
Nodes (18): Bit, Now, Offset, PowerInfo, PowerLimits, Ratio, SinceBoot, IReadOnlyList (+10 more)

### Community 32 - "Nextcalibur.Core.Security"
Cohesion: 0.11
Nodes (12): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, JsonSerializerOptions, AuditLog, AppVersion, system_collections_concurrent, system_net (+4 more)

### Community 33 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 34 - ".RequestRestart"
Cohesion: 0.11
Nodes (10): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr, MarshalAs (+2 more)

### Community 35 - ".StartProgramRules"
Cohesion: 0.12
Nodes (8): AssemblyInformationalVersionAttribute, Task, FrameworkElement, Func, Grid, HashSet, List, ModeChoice

### Community 36 - "InstallFolderGuard"
Cohesion: 0.14
Nodes (10): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Fact, InlineData, Theory, ElevationPolicyTests (+2 more)

### Community 37 - ".Choose"
Cohesion: 0.12
Nodes (19): IEnumerable, IReadOnlyList, IdleLoadPicker, RunningItem, RunningKind, App, Background, Service (+11 more)

### Community 38 - "Api"
Cohesion: 0.11
Nodes (23): Api, GetSettingFn, InitializeFn, ProfileOutFn, SessionFn, SessionOutFn, Set, SetSettingFn (+15 more)

### Community 39 - ".Get"
Cohesion: 0.14
Nodes (14): ArgumentNullException, GpuLoad, FirmwareModeReading, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma (+6 more)

### Community 40 - "ColourWheel"
Cohesion: 0.10
Nodes (19): BitmapSource, Control, DependencyObject, DependencyPropertyChangedEventArgs, Ellipse, Hue, Saturation, Canvas (+11 more)

### Community 41 - "Elevation"
Cohesion: 0.13
Nodes (7): dynamic, Encoding, DllImport, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 42 - "LedState"
Cohesion: 0.14
Nodes (17): B, G, LedBrightness, R, LedState, ActiveProfile, BrightnessPercent, Current (+9 more)

### Community 43 - "ResourceDictionary"
Cohesion: 0.10
Nodes (27): ActualWidth, Bar, Bg, Blade, Box, CardBorder, Grip, Guard (+19 more)

### Community 44 - ".Main"
Cohesion: 0.13
Nodes (7): The threat model, and what the code does about it, Application, DllImport, Mutex, App, OverlayRepairPending, STAThread

### Community 45 - "Dependency"
Cohesion: 0.11
Nodes (18): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+10 more)

### Community 46 - "TrayPresence"
Cohesion: 0.12
Nodes (12): ContextMenuStrip, EventHandler, Item, NotifyIcon, List, Ms, Window, TrayPresence (+4 more)

### Community 47 - "Integrity"
Cohesion: 0.11
Nodes (11): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvapiPath, NvmlPath (+3 more)

### Community 48 - ".For"
Cohesion: 0.14
Nodes (13): AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, Baseline (+5 more)

### Community 49 - "SystemModeService"
Cohesion: 0.19
Nodes (8): Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, Ours, Plan, SystemModeService

### Community 50 - ".SignatureIsValid"
Cohesion: 0.15
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 51 - ".RefreshIdleRows"
Cohesion: 0.13
Nodes (8): Kept, ComboBox, Grid, Guid, Guid, IReadOnlyList, Plan, Timeouts

### Community 52 - "PowerOverlayService"
Cohesion: 0.16
Nodes (11): DllImport, Guid, IReadOnlyList, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, PowerModeOption (+3 more)

### Community 53 - "Fact"
Cohesion: 0.17
Nodes (7): IList, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 54 - "DevicePowerSaving"
Cohesion: 0.17
Nodes (12): TextBlock, ToggleButton, AcDc, Guid, IEnumerable, DevicePowerSaving, Off, DeviceSaving (+4 more)

### Community 55 - "Authenticode"
Cohesion: 0.25
Nodes (14): CatalogInfo, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle, Authenticode (+6 more)

### Community 56 - "TestPreflight"
Cohesion: 0.17
Nodes (11): Display, IoCounters, ProcessLook, ConcurrentDictionary, Dictionary, DllImport, IntPtr, TimeSpan (+3 more)

### Community 57 - ".Processor"
Cohesion: 0.15
Nodes (13): GpuPowerLimits, IEnumerable, PowerDefinition, PowerDefinitions, SetBy, Intel, LaptopMaker, Nextcalibur (+5 more)

### Community 58 - "FirmwareInventory"
Cohesion: 0.09
Nodes (22): IReadOnlyList, FirmwareInventory, BatteryChemistry, BatteryDesignMwh, BatteryName, BiosDate, BiosRelease, BiosVendor (+14 more)

### Community 59 - "CpuPowerReader"
Cohesion: 0.21
Nodes (6): ArgumentOutOfRangeException, DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 60 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 61 - "MachineScanRecord"
Cohesion: 0.09
Nodes (22): DateTime, List, MachineScanRecord, BiosDate, BiosRelease, BiosVendor, BiosVersion, BoardProduct (+14 more)

### Community 62 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 63 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 64 - ".ShowResult"
Cohesion: 0.19
Nodes (8): List, IEnumerable, IReadOnlyList, TimeSpan, CeilingStep, CeilingTest, Fact, CeilingTestTests

### Community 65 - ".LoadDefinitionsAsync"
Cohesion: 0.14
Nodes (3): Task, Action, ToggleButton

### Community 66 - ".ApplyFnShortcuts"
Cohesion: 0.13
Nodes (3): CancelEventArgs, DllImport, IntPtr

### Community 68 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 69 - ".Ask"
Cohesion: 0.25
Nodes (10): HttpStatusCode, CancellationToken, Fact, HttpRequestMessage, HttpResponseMessage, InlineData, Task, Theory (+2 more)

### Community 70 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 71 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Ok, Task (+7 more)

### Community 72 - "Fact"
Cohesion: 0.14
Nodes (10): RepairOutcome, Empty, Fact, Task, CleanMachineTests, Guarded, Unguarded, CoexistencePolicyTests (+2 more)

### Community 73 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 74 - "ModeProfileTests"
Cohesion: 0.22
Nodes (7): MemberData, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 75 - "DarkMenuColours"
Cohesion: 0.11
Nodes (19): ProfessionalColorTable, Color, DarkMenuColours, CheckBackground, CheckPressedBackground, CheckSelectedBackground, ImageMarginGradientBegin, ImageMarginGradientEnd (+11 more)

### Community 76 - "UpdateService"
Cohesion: 0.13
Nodes (12): DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService, Available, AvailableVersion (+4 more)

### Community 77 - ".CheckProgram"
Cohesion: 0.22
Nodes (5): Fact, IntegrityTests, Current, Exe, Manifest

### Community 78 - "TemperatureFusion"
Cohesion: 0.25
Nodes (9): DateTime, TimeSpan, TemperatureFusion, Offset, RegisterSetAside, Value, DateTime, Fact (+1 more)

### Community 79 - "CeilingTestWindow"
Cohesion: 0.14
Nodes (11): CancellationTokenSource, Button, CheckBox, Func, StackPanel, TextBlock, CeilingTestWindow, Accepted (+3 more)

### Community 80 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 81 - "CoreLoad"
Cohesion: 0.15
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 82 - "LedController"
Cohesion: 0.24
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 83 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 84 - "DevicePowerState"
Cohesion: 0.28
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 85 - "SystemMode"
Cohesion: 0.18
Nodes (10): InvalidOperationException, IReadOnlyList, SystemMode, Balanced, Gaming, Office, Performance, SystemModes (+2 more)

### Community 86 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 87 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 88 - "ThemeService"
Cohesion: 0.14
Nodes (11): Theme, Theme, Dark, Light, ThemePreference, Dark, Light, System (+3 more)

### Community 89 - "Integrity.cs"
Cohesion: 0.17
Nodes (7): system_reflection, system_security_accesscontrol, system_security_cryptography, system_security_principal, InlineData, Theory, PawnIoModuleTests

### Community 90 - "Log"
Cohesion: 0.26
Nodes (3): Exception, Log, Folder

### Community 91 - "BatteryModePolicy"
Cohesion: 0.35
Nodes (5): BatteryModePolicy, QuietMode, Remembered, Fact, BatteryModePolicyTests

### Community 92 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 93 - ".Set"
Cohesion: 0.30
Nodes (7): PowerThrottlingState, DllImport, IntPtr, EfficiencyMode, PowerThrottlingState, Fact, EfficiencyModeTests

### Community 94 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 95 - ".Write"
Cohesion: 0.23
Nodes (7): AcDc, Guid, IReadOnlyList, IdleSetting, DisplayOff, Sleep, IdleTimeouts

### Community 96 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 97 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 98 - "LedEffect"
Cohesion: 0.15
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 99 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 100 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 101 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 102 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 103 - "Nextcalibur 0.5.4"
Cohesion: 0.17
Nodes (11): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+3 more)

### Community 104 - ".Pick"
Cohesion: 0.17
Nodes (7): MouseEventArgs, MouseButtonEventArgs, Size, Arrow, Point, Tick, Path

### Community 105 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 106 - "StackPanel"
Cohesion: 0.17
Nodes (12): BannerActions, CeilingRows, DefinitionRows, DialogActions, DriveTextStack, FnShortcutRows, IdleLoadRows, IdleRows (+4 more)

### Community 107 - "PackageTemperatureTests"
Cohesion: 0.41
Nodes (4): Fact, InlineData, Theory, PackageTemperatureTests

### Community 108 - ".Check"
Cohesion: 0.20
Nodes (10): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+2 more)

### Community 109 - "AcDc"
Cohesion: 0.29
Nodes (5): AcDc, InlineData, Theory, Fact, IdleTimeoutsTests

### Community 110 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 111 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 112 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 113 - "NoticeGate"
Cohesion: 0.22
Nodes (7): Key, Show, Action, List, NoticeGate, ShouldWait, WaitingCount

### Community 114 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 115 - ".Run"
Cohesion: 0.29
Nodes (4): Detail, Func, Ok, SelfTest

### Community 116 - ".Sanitised"
Cohesion: 0.33
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 117 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 118 - ".Parse"
Cohesion: 0.29
Nodes (4): List, MemoryModule, Fact, FirmwareInventoryTests

### Community 119 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 120 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 121 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 122 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 123 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 124 - "ThermalSample"
Cohesion: 0.33
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 125 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 126 - ".RunAndRecord"
Cohesion: 0.33
Nodes (4): JsonSerializerOptions, MachineScan, Firmware, RecordPath

### Community 127 - ".IsSystemFileSignedBy"
Cohesion: 0.39
Nodes (3): Fact, SystemFileTrustTests, Cmd

### Community 128 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 129 - "CeilingTests"
Cohesion: 0.31
Nodes (5): Baseline, Fact, InlineData, Theory, CeilingTests

### Community 130 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 131 - "Nextcalibur 0.5.5"
Cohesion: 0.25
Nodes (6): Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture

### Community 132 - "Toasts"
Cohesion: 0.32
Nodes (5): Action, Toasts, Balloon, windows_data_xml_dom, windows_ui_notifications

### Community 133 - ".Recommended"
Cohesion: 0.29
Nodes (5): Min, Fact, InlineData, Theory, IdleRecommendedTests

### Community 134 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 135 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 136 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 137 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 138 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 139 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 140 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 142 - "ColGauge"
Cohesion: 0.19
Nodes (7): ColGauge, DialogCheck, DriveList, FnSelectAll, CheckBox, ColumnDefinition, ItemsControl

### Community 143 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 144 - ".Current"
Cohesion: 0.48
Nodes (3): DllImport, IntPtr, ScreenSaver

### Community 145 - "BatteryModePolicy.cs"
Cohesion: 0.33
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 146 - ".Entries"
Cohesion: 0.38
Nodes (3): IReadOnlyList, Fact, AuditLogTests

### Community 147 - ".Values_are_the_ones_measured_from_the_vendor"
Cohesion: 0.33
Nodes (4): Fact, InlineData, Theory, ThermalProfileTests

### Community 148 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 149 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 150 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 151 - "Nextcalibur 0.7.0"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.7.0, Tested on, Updated

### Community 152 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 153 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 154 - "ComboBox"
Cohesion: 0.33
Nodes (6): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, SettingNvPower, ComboBox

### Community 155 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 157 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 159 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 160 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 161 - "Nextcalibur 0.7.1"
Cohesion: 0.40
Nodes (4): Fixed, New, Nextcalibur 0.7.1, Tested on

### Community 162 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 163 - "PART_Track"
Cohesion: 0.40
Nodes (5): PART_Track, RootGrid, Sword, Grid, Track

### Community 165 - "SmiSubsystem"
Cohesion: 0.40
Nodes (5): SmiSubsystem, DisplayMode, Led, Profile, Thermal

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

### Community 173 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 174 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 175 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 176 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 177 - ".TryParseRgb"
Cohesion: 0.50
Nodes (3): B, G, R

### Community 178 - ".CalculateThreadShare"
Cohesion: 0.50
Nodes (3): IReadOnlyDictionary, Stable, TopSharePercent

### Community 179 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 181 - "Toggle"
Cohesion: 0.67
Nodes (3): IsDropDownOpen, Toggle, ToggleButton

### Community 183 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 184 - "EffectPanel"
Cohesion: 0.67
Nodes (3): EffectPanel, ProfileRow, UniformGrid

### Community 186 - "PART_ContentHost"
Cohesion: 0.67
Nodes (3): PART_ContentHost, SettingsScroll, ScrollViewer

## Knowledge Gaps
- **605 isolated node(s):** `OverlayRepairPending`, `Accepted`, `SelectedColour`, `VisualChildrenCount`, `Diameter` (+600 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1162 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **33 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `AppSettings`, `FnShortcut`, `.Get`, `Nextcalibur 0.5.5`, `Nextcalibur.Core.Configuration`, `ProgramRule`, `GpuClockReader`, `Window`, `RunawayWatchTests`, `.Info`, `RadioButton`, `TourPage`, `PlanTuning`, `.StartSlowTimer`, `.OnLoaded`, `CoolingPolicyTests`, `.OnBrightnessChanged`, `EcMailbox`, `SystemInfo`, `UserPresence`, `LedZone`, `.RequestRestart`, `.StartProgramRules`, `.Choose`, `.Get`, `TrayPresence`, `Integrity`, `.For`, `SystemModeService`, `.RefreshIdleRows`, `PowerOverlayService`, `DevicePowerSaving`, `.OnMinimiseToTrayClick`, `CpuPowerReader`, `.LoadDefinitionsAsync`, `.ApplyFnShortcuts`, `BacklightKeyWatcher`, `DependencyStatus`, `UpdateService`, `TemperatureFusion`, `CeilingTestWindow`, `CoreLoad`, `LedController`, `SystemMode`, `ThemeService`, `BatteryModePolicy`, `.Write`, `Grid`, `LedEffect`, `.Pick`, `Strings`, `.Check`, `NvidiaDriverState`, `ThermalSample`?**
  _High betweenness centrality (0.483) - this node is a cross-community bridge._
- **Why does `Nextcalibur.Core.Hardware` connect `Nextcalibur.Core.Hardware` to `Nextcalibur.Core.Security`, `Nextcalibur.Core.Configuration`, `BacklightKeyWatcher`, `xunit`, `.Check`, `ThermalSample`, `Integrity.cs`, `EcMailbox`, `.Processor`, `MachineWideGate`, `CpuLimits`?**
  _High betweenness centrality (0.081) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Text`, `ColGauge`, `RadioButton`, `TextBlock`, `.OnBrightnessChanged`, `ComboBox`, `Keycaps`, `DriveGauge`, `CpuWarnValue`, `EffectPanel`, `.OnMinimiseToTrayClick`, `PART_ContentHost`, `DialogProgress`, `DriveDot`, `TourCanvas`, `Wheel`, `Grid`, `StackPanel`, `Border`?**
  _High betweenness centrality (0.078) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `OverlayRepairPending`, `Accepted`, `SelectedColour` to the rest of the system?**
  _605 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.02087307821793289 - nodes in this community are weakly interconnected._
- **Should `AppSettings` be split into smaller, more focused modules?**
  _Cohesion score 0.029605263157894735 - nodes in this community are weakly interconnected._