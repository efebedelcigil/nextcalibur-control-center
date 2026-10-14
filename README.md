# Graph Report - nextcalibur-control-center  (2026-10-14)

## Corpus Check
- 199 files · ~247,711 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3368 nodes · 7496 edges · 196 communities (162 shown, 34 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 455 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `8d013e28`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- FnShortcut
- ProtectedStore
- AppSettings
- RunawayWatchTests
- .Get
- GpuClockReader
- Window
- .OfferDependency
- .Info
- ProgramRule
- IShellLinkW
- TrayPresence
- WindowsFaults
- Nextcalibur.App
- RadioButton
- Nextcalibur.Core.Hardware
- WindowsFaultsTests
- PlanTuning
- CoolingPolicyTests
- TextBlock
- NetworkCostTests
- xunit
- UserPresence
- MachineScanRecord
- .RequestRestart
- .Check
- SystemInfo
- SystemTools
- .Survey
- Fact
- Elevation
- .Main
- Fact
- Api
- Nextcalibur.Core.Configuration
- UpdateService
- SystemModeService
- Dependency
- .SignatureIsValid
- .OnSystemModeChanged
- Integrity
- DevicePowerSaving
- Authenticode
- App
- .PlaceTourStep
- .Processor
- CpuPowerReader
- Program
- .Ask
- SystemMode
- .For
- .Read
- DotNetRuntimeDependency
- EcMailbox
- Nextcalibur.Core.csproj
- .CheckProgram
- .Get
- BatteryModePolicy
- IdleSetting
- RestorePoint
- MemoryTrimmer
- DependencyStatus
- Fact
- analyse-autopsy.py
- BacklightKeyWatcher
- ProcessPresence
- ModeProfile
- TemperatureFusion
- .Scan
- ResourceDictionary
- RuntimeDependencyTests
- CeilingStep
- SmiCommand
- CpuLimitsTests
- PowerOverlayService
- .ApplyFnShortcuts
- .GetColour
- ColourWheel
- .WireSettingsPage
- LedController
- LedState
- CpuLimits
- FanGauge
- Unelevated
- DonutGauge
- ThermalSample
- MachineHistory
- CeilingTestWindow
- Nextcalibur.Core.Tests.Attacks
- SegmentedBar
- Nextcalibur Control Center
- InstallFolderGuard
- LogInjectionTests
- CoreLoad
- Grid
- PackageTemperatureTests
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- Strings
- .RunStepsAsync
- .RunawayTick
- .RefreshIdleRows
- .Sanitised
- Hardware Protocol
- Nextcalibur 0.5.3
- StackPanel
- ThemeService
- CardSwitchTasks
- Border
- NvidiaDriverState
- VendorSoftware
- .IsSystemFileSignedBy
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- LedEffect
- PawnIoDependency
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
- Releasing, and signing
- .Pick
- AcDc
- TourPage
- .Current
- Log-Graphics.ps1
- Privacy
- Nextcalibur 0.6.2
- Security
- .OnBrightnessChanged
- ComboBox
- LedZone
- MachineWideGate
- PowerSource
- Notice
- Nextcalibur 0.5.11
- AppIdentity.cs
- RunningKind
- PawnIoModuleTests.cs
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
- OverlayDiagnosis
- Tools
- 0.5.7.md
- CpuWarnValue
- DialogCheck
- .OnMinimiseToTrayClick
- Toasts.cs
- .Work_that_must_not_be_interrupted
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
4. `Nextcalibur.Core.Hardware` - 67 edges
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

## Communities (196 total, 34 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.03
Nodes (69): DeferredOverheat, GameSession, HwndSource, ObservableCollection, PluggedIn, Queue, Slider, MainWindow (+61 more)

### Community 1 - "FnShortcut"
Cohesion: 0.05
Nodes (42): INotifyPropertyChanged, Problem, DriveRow, Detail, Name, Percent, PercentText, Dictionary (+34 more)

### Community 2 - "ProtectedStore"
Cohesion: 0.07
Nodes (26): Content, DirectorySecurity, Hash, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Dictionary (+18 more)

### Community 3 - "AppSettings"
Cohesion: 0.04
Nodes (55): DateTime, Dictionary, FnShortcut, JsonElement, List, ProgramRule, AppSettings, AcceptedVendorSoftware (+47 more)

### Community 4 - "RunawayWatchTests"
Cohesion: 0.06
Nodes (36): Cpu, CpuMs, Display, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList (+28 more)

### Community 5 - ".Get"
Cohesion: 0.07
Nodes (6): GameEnd, SolidColorBrush, IReadOnlyList, GameEnd, Func, Task

### Community 6 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 7 - "Window"
Cohesion: 0.07
Nodes (52): TemperatureToDoubleConverter, Foreground, IsChecked, IsMouseOver, ItemsSource.Count, Name, BannerDismissButton, BannerRestartButton (+44 more)

### Community 8 - ".OfferDependency"
Cohesion: 0.08
Nodes (11): Asynchronous dispatcher & UI thread hardening, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary, IReadOnlyDictionary (+3 more)

### Community 9 - ".Info"
Cohesion: 0.08
Nodes (9): AssemblyInformationalVersionAttribute, Guid, Ours, Plan, List, MessageBoxButton, MessageBoxResult, ModeChoice (+1 more)

### Community 10 - "ProgramRule"
Cohesion: 0.08
Nodes (25): GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, IEnumerable (+17 more)

### Community 11 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 12 - "TrayPresence"
Cohesion: 0.07
Nodes (21): ContextMenuStrip, DevPropKey, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, EventHandler (+13 more)

### Community 13 - "WindowsFaults"
Cohesion: 0.08
Nodes (33): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+25 more)

### Community 14 - "Nextcalibur.App"
Cohesion: 0.09
Nodes (25): Nextcalibur.App, Nextcalibur.App.Controls, system_componentmodel, system_drawing, system_io, system_linq, system_runtime_compilerservices, system_windows (+17 more)

### Community 15 - "RadioButton"
Cohesion: 0.06
Nodes (42): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeBalanced, ModeDiscrete (+34 more)

### Community 16 - "Nextcalibur.Core.Hardware"
Cohesion: 0.08
Nodes (11): Nextcalibur.Cli, Nextcalibur.Core.Input, Nextcalibur.Core.Hardware, microsoft_win32, microsoft_win32_safehandles, system_diagnostics, system_management, system_reflection (+3 more)

### Community 17 - "WindowsFaultsTests"
Cohesion: 0.12
Nodes (7): IReadOnlyDictionary, FaultEvaluation, Stable, Fact, WindowsFaultsTests, Dictionary, TopSharePercent

### Community 18 - "PlanTuning"
Cohesion: 0.13
Nodes (18): Floor, Marker, Original, Action, AcDc, DllImport, Guid, IEnumerable (+10 more)

### Community 19 - "CoolingPolicyTests"
Cohesion: 0.08
Nodes (26): At, Margin, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc (+18 more)

### Community 20 - "TextBlock"
Cohesion: 0.05
Nodes (40): Detail, PercentText, BannerBody, BannerTitle, BrightnessValue, CpuClock, CpuFan, CpuTemp (+32 more)

### Community 21 - "NetworkCostTests"
Cohesion: 0.11
Nodes (26): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+18 more)

### Community 22 - "xunit"
Cohesion: 0.11
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, nextcalibur_core_power_plantuning_acdc, system_text_regularexpressions, system_xml_linq, xunit

### Community 23 - "UserPresence"
Cohesion: 0.12
Nodes (14): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, Point, Size, DateTime, DllImport (+6 more)

### Community 24 - "MachineScanRecord"
Cohesion: 0.07
Nodes (27): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites (+19 more)

### Community 25 - ".RequestRestart"
Cohesion: 0.10
Nodes (11): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr (+3 more)

### Community 26 - ".Check"
Cohesion: 0.11
Nodes (14): CommonAce, RawSecurityDescriptor, RegistryKey, SecurityIdentifier, MailboxAccess, MailboxAvailability, AccessNotGranted, Available (+6 more)

### Community 27 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 28 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 29 - ".Survey"
Cohesion: 0.13
Nodes (8): Func, IEnumerable, IReadOnlyList, RetirementEntry, Trace, Version, NduFix, FootprintTests

### Community 30 - "Fact"
Cohesion: 0.13
Nodes (9): ArgumentNullException, IList, HardwareSupport, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests (+1 more)

### Community 31 - "Elevation"
Cohesion: 0.12
Nodes (10): dynamic, Elevation, Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles (+2 more)

### Community 32 - ".Main"
Cohesion: 0.13
Nodes (6): RegistryKey, Footprint, Exception, Log, Folder, STAThread

### Community 33 - "Fact"
Cohesion: 0.11
Nodes (9): Limits, SystemMode, InlineData, Theory, Fact, ForwardCompatibilityTests, MailboxAccessTests, SettingsTests (+1 more)

### Community 34 - "Api"
Cohesion: 0.11
Nodes (23): Api, GetSettingFn, InitializeFn, ProfileOutFn, SessionFn, SessionOutFn, Set, SetSettingFn (+15 more)

### Community 35 - "Nextcalibur.Core.Configuration"
Cohesion: 0.12
Nodes (10): Nextcalibur.Core.Security, Nextcalibur.Core.Configuration, Theme, Dark, Light, system_security_accesscontrol, system_security_cryptography, system_security_cryptography_x509certificates (+2 more)

### Community 36 - "UpdateService"
Cohesion: 0.09
Nodes (14): CancelEventArgs, EventArgs, DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService (+6 more)

### Community 37 - "SystemModeService"
Cohesion: 0.19
Nodes (8): Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, Ours, Plan, SystemModeService

### Community 38 - "Dependency"
Cohesion: 0.11
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 39 - ".SignatureIsValid"
Cohesion: 0.15
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 40 - ".OnSystemModeChanged"
Cohesion: 0.13
Nodes (8): Option, PowerModeChangedEventArgs, Button, Guid, IEnumerable, Ours, Plan, TextBlock

### Community 41 - "Integrity"
Cohesion: 0.12
Nodes (11): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvapiPath, NvmlPath (+3 more)

### Community 42 - "DevicePowerSaving"
Cohesion: 0.17
Nodes (12): TextBlock, ToggleButton, AcDc, Guid, IEnumerable, DevicePowerSaving, Off, DeviceSaving (+4 more)

### Community 43 - "Authenticode"
Cohesion: 0.25
Nodes (14): CatalogInfo, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle, Authenticode (+6 more)

### Community 44 - "App"
Cohesion: 0.12
Nodes (8): The threat model, and what the code does about it, Application, DllImport, Mutex, App, OverlayRepairPending, StartupRegistration, IsEnabled

### Community 45 - ".PlaceTourStep"
Cohesion: 0.11
Nodes (10): SizeChangedEventArgs, FrameworkElement, KeyEventArgs, RoutedEventArgs, TourStep, Body, SectionName, Title (+2 more)

### Community 46 - ".Processor"
Cohesion: 0.15
Nodes (13): GpuPowerLimits, IEnumerable, PowerDefinition, PowerDefinitions, SetBy, Intel, LaptopMaker, Nextcalibur (+5 more)

### Community 47 - "CpuPowerReader"
Cohesion: 0.21
Nodes (6): ArgumentOutOfRangeException, DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 48 - "Program"
Cohesion: 0.16
Nodes (5): ConsoleColor, B, G, R, Program

### Community 49 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 50 - "SystemMode"
Cohesion: 0.15
Nodes (10): InvalidOperationException, IReadOnlyList, SystemMode, Balanced, Gaming, Office, Performance, SystemModes (+2 more)

### Community 51 - ".For"
Cohesion: 0.22
Nodes (8): MemberData, Baseline, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 52 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 53 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 54 - "EcMailbox"
Cohesion: 0.18
Nodes (9): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+1 more)

### Community 55 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 56 - ".CheckProgram"
Cohesion: 0.20
Nodes (5): Fact, IntegrityTests, Current, Exe, Manifest

### Community 57 - ".Get"
Cohesion: 0.18
Nodes (11): FirmwareModeReading, GpuMode, Discrete, Hybrid, Uma, GpuModeService, SwitchOutcome, Func (+3 more)

### Community 58 - "BatteryModePolicy"
Cohesion: 0.22
Nodes (7): BatteryModePolicy, QuietMode, Remembered, Fact, BatteryModePolicyTests, Fact, SystemModesTests

### Community 59 - "IdleSetting"
Cohesion: 0.17
Nodes (10): AcDc, Guid, IReadOnlyList, IdleSetting, DisplayOff, Sleep, IdleTimeouts, Timeouts (+2 more)

### Community 60 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 61 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 62 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 63 - "Fact"
Cohesion: 0.14
Nodes (10): RepairOutcome, Empty, Fact, Task, CleanMachineTests, Guarded, Unguarded, CoexistencePolicyTests (+2 more)

### Community 64 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 65 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 66 - "ProcessPresence"
Cohesion: 0.23
Nodes (8): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, Fact, ProcessPresenceTests, Self

### Community 67 - "ModeProfile"
Cohesion: 0.17
Nodes (12): AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, Baseline (+4 more)

### Community 68 - "TemperatureFusion"
Cohesion: 0.25
Nodes (9): DateTime, TimeSpan, TemperatureFusion, Offset, RegisterSetAside, Value, DateTime, Fact (+1 more)

### Community 69 - ".Scan"
Cohesion: 0.16
Nodes (12): IoCounters, ProcessLook, Dictionary, DllImport, IntPtr, IReadOnlyList, TimeSpan, IoCounters (+4 more)

### Community 70 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 71 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 72 - "CeilingStep"
Cohesion: 0.21
Nodes (7): IEnumerable, IReadOnlyList, TimeSpan, CeilingStep, CeilingTest, Fact, CeilingTestTests

### Community 73 - "SmiCommand"
Cohesion: 0.21
Nodes (6): ArgumentException, SmiCommand, Fact, InlineData, Theory, SmiCommandTests

### Community 74 - "CpuLimitsTests"
Cohesion: 0.15
Nodes (8): Now, Offset, SinceBoot, IReadOnlyList, Fact, CpuLimitsTests, ThermFlags, TjMax

### Community 75 - "PowerOverlayService"
Cohesion: 0.23
Nodes (7): DllImport, Guid, IReadOnlyList, PowerModeOption, PowerOverlays, All, PowerOverlayService

### Community 77 - ".GetColour"
Cohesion: 0.24
Nodes (7): B, G, R, Fact, InlineData, Theory, LedStateTests

### Community 78 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 80 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 81 - "LedState"
Cohesion: 0.13
Nodes (15): Dictionary, LedBrightness, LedProfile, BrightnessPercent, Colours, Effect, LedState, ActiveProfile (+7 more)

### Community 82 - "CpuLimits"
Cohesion: 0.17
Nodes (10): Bit, IReadOnlySet, PowerInfo, PowerLimits, Ratio, CpuLimits, PowerInfo, PowerLimits (+2 more)

### Community 83 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 84 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 85 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 86 - "ThermalSample"
Cohesion: 0.23
Nodes (5): GpuLoad, GpuConfiguration, DateTimeOffset, ThermalReader, ThermalSample

### Community 87 - "MachineHistory"
Cohesion: 0.28
Nodes (6): DateTime, IReadOnlyList, MachineHistory, DateTime, Fact, MachineHistoryTests

### Community 88 - "CeilingTestWindow"
Cohesion: 0.18
Nodes (8): CancellationTokenSource, Button, CheckBox, Func, StackPanel, TextBlock, CeilingTestWindow, Window

### Community 89 - "Nextcalibur.Core.Tests.Attacks"
Cohesion: 0.26
Nodes (5): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http

### Community 90 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 91 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 92 - "InstallFolderGuard"
Cohesion: 0.30
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 93 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 94 - "CoreLoad"
Cohesion: 0.17
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 95 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 96 - "PackageTemperatureTests"
Cohesion: 0.36
Nodes (4): Fact, InlineData, Theory, PackageTemperatureTests

### Community 97 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 98 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 99 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 100 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 101 - "Nextcalibur 0.5.4"
Cohesion: 0.17
Nodes (11): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+3 more)

### Community 102 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 103 - ".RunStepsAsync"
Cohesion: 0.33
Nodes (3): CancellationToken, List, Task

### Community 104 - ".RunawayTick"
Cohesion: 0.21
Nodes (6): DllImport, IntPtr, Name, Pid, Action, Toasts

### Community 105 - ".RefreshIdleRows"
Cohesion: 0.27
Nodes (3): ComboBox, Grid, Guid

### Community 106 - ".Sanitised"
Cohesion: 0.26
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 107 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 108 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 109 - "StackPanel"
Cohesion: 0.18
Nodes (11): BannerActions, CeilingRows, DialogActions, DriveTextStack, FnShortcutRows, IdleRows, PanelWindowsFaultsSubOptions, ProfileRow (+3 more)

### Community 110 - "ThemeService"
Cohesion: 0.18
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 112 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 113 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 114 - "VendorSoftware"
Cohesion: 0.27
Nodes (4): DateTime, TimeSpan, VendorSoftware, UninstallCommand

### Community 115 - ".IsSystemFileSignedBy"
Cohesion: 0.33
Nodes (3): Fact, SystemFileTrustTests, Cmd

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

### Community 122 - "LedEffect"
Cohesion: 0.22
Nodes (8): LedEffect, Blink, Breathing, ColourCycle, Heartbeat, Off, Static, Wave

### Community 123 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 124 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 125 - "CeilingTests"
Cohesion: 0.31
Nodes (5): Baseline, Fact, InlineData, Theory, CeilingTests

### Community 126 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 127 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 128 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 129 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 130 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 131 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 132 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 133 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 134 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 135 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 136 - "AcDc"
Cohesion: 0.43
Nodes (3): AcDc, InlineData, Theory

### Community 137 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 138 - ".Current"
Cohesion: 0.48
Nodes (3): DllImport, IntPtr, ScreenSaver

### Community 139 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 140 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 141 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 142 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 143 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 144 - "ComboBox"
Cohesion: 0.33
Nodes (6): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, SettingNvPower, ComboBox

### Community 145 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 146 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 147 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 148 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 149 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 150 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 151 - "RunningKind"
Cohesion: 0.40
Nodes (5): RunningKind, App, Background, Service, Transfer

### Community 152 - "PawnIoModuleTests.cs"
Cohesion: 0.40
Nodes (3): InlineData, Theory, PawnIoModuleTests

### Community 153 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 155 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 156 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 157 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 159 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 160 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 161 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 162 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 164 - "OverlayDiagnosis"
Cohesion: 0.50
Nodes (4): OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck

### Community 165 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 168 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 169 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

## Knowledge Gaps
- **524 isolated node(s):** `OverlayRepairPending`, `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum` (+519 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1030 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **34 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `FnShortcut`, `AppSettings`, `RunawayWatchTests`, `.Get`, `GpuClockReader`, `Window`, `.OfferDependency`, `.Info`, `AcDc`, `TourPage`, `TrayPresence`, `ProgramRule`, `Nextcalibur.App`, `.OnBrightnessChanged`, `RadioButton`, `LedZone`, `PlanTuning`, `CoolingPolicyTests`, `Nextcalibur.Core.Hardware`, `UserPresence`, `MachineScanRecord`, `.RequestRestart`, `SystemInfo`, `Nextcalibur.Core.Configuration`, `UpdateService`, `SystemModeService`, `.OnSystemModeChanged`, `Integrity`, `DevicePowerSaving`, `.OnMinimiseToTrayClick`, `.PlaceTourStep`, `CpuPowerReader`, `SystemMode`, `.Read`, `EcMailbox`, `.Get`, `BatteryModePolicy`, `IdleSetting`, `DependencyStatus`, `BacklightKeyWatcher`, `TemperatureFusion`, `PowerOverlayService`, `.ApplyFnShortcuts`, `.WireSettingsPage`, `LedController`, `ThermalSample`, `CeilingTestWindow`, `Grid`, `Strings`, `.RunawayTick`, `.RefreshIdleRows`, `ThemeService`, `NvidiaDriverState`, `LedEffect`?**
  _High betweenness centrality (0.470) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Text`, `.OnBrightnessChanged`, `RadioButton`, `ComboBox`, `TextBlock`, `Keycaps`, `DriveGauge`, `CpuWarnValue`, `DialogCheck`, `.OnMinimiseToTrayClick`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `Grid`, `StackPanel`, `Border`?**
  _High betweenness centrality (0.095) - this node is a cross-community bridge._
- **Why does `Nextcalibur.Core.Hardware` connect `Nextcalibur.Core.Hardware` to `PackageTemperatureTests`, `BacklightKeyWatcher`, `Nextcalibur.Core.Configuration`, `Nextcalibur.App`, `.Processor`, `CpuLimits`, `MachineWideGate`, `EcMailbox`, `ThermalSample`, `MachineScanRecord`, `Nextcalibur.Core.Tests.Attacks`, `xunit`, `PawnIoModuleTests.cs`, `Fact`?**
  _High betweenness centrality (0.068) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `OverlayRepairPending`, `SelectedColour`, `VisualChildrenCount` to the rest of the system?**
  _524 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.02777193351567431 - nodes in this community are weakly interconnected._
- **Should `FnShortcut` be split into smaller, more focused modules?**
  _Cohesion score 0.053208137715179966 - nodes in this community are weakly interconnected._