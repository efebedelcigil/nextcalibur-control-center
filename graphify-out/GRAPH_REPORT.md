# Graph Report - nextcalibur-control-center  (2026-10-14)

## Corpus Check
- 196 files · ~245,306 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3310 nodes · 7375 edges · 195 communities (163 shown, 32 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 434 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `818ce1ad`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- AppSettings
- .OnLoaded
- ProtectedStore
- GpuClockReader
- FnShortcut
- RunawayWatchTests
- WindowsFaults
- IShellLinkW
- Window
- ProcessPresence
- RadioButton
- .Info
- PlanTuning
- CoolingPolicyTests
- xunit
- .StartSlowTimer
- NetworkCostTests
- .Survey
- .Main
- WindowsFaultsTests
- .RemoveMachineTraces
- SystemInfo
- UserPresence
- RoutedEventArgs
- CpuLimits
- SystemTools
- .RequestRestart
- Button
- SystemModeService
- system_windows
- Nextcalibur.Core.Hardware
- Nextcalibur.Core.Configuration
- .Scan
- .CheckProgram
- .SignatureIsValid
- Nextcalibur.App
- EcMailbox
- .Get
- Fact
- MachineScanRecord
- TrayPresence
- .OnSystemModeChanged
- .ApplyFnShortcuts
- .Check
- DevicePowerSaving
- Dependency
- GpuModeService
- CpuPowerReader
- Authenticode
- IntegrityTests
- .Processor
- .Ask
- .PlaceTourStep
- DotNetRuntimeDependency
- Nextcalibur.Core.csproj
- .ShowResult
- .RefreshIdleRows
- Fact
- RestorePoint
- BacklightKeyWatcher
- MemoryTrimmer
- DependencyStatus
- .Dictionary
- .For
- analyse-autopsy.py
- Program
- ModeProfileTests
- ToggleButton
- UpdateService
- RuntimeDependencyTests
- CeilingTestWindow
- PowerOverlayService
- ResourceDictionary
- .ShouldAsk
- Nextcalibur.Core.Tests.Attacks
- CoreLoad
- .Read
- .GetColour
- ProgramRule
- ColourWheel
- Nextcalibur.Core.Security
- DevicePowerState
- LedController
- LedState
- FanGauge
- Unelevated
- DonutGauge
- BatteryModePolicy
- IdleSetting
- SegmentedBar
- Nextcalibur Control Center
- InstallFolderGuard
- .HasAccess
- LogInjectionTests
- AcDc
- Grid
- SystemMode
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- Strings
- .Sanitised
- PackageTemperatureTests
- SmiCommandTests
- Hardware Protocol
- Nextcalibur 0.5.3
- StackPanel
- SupportVerdict
- MachineWideGate
- .Normalise
- Border
- ThemeService
- NvidiaDriverState
- IntPtr
- .ToHsv
- README.md
- Nextcalibur 0.5.0
- CardSwitchTasks
- LedEffect
- ThermalSample
- PawnIoDependency
- .IsSystemFileSignedBy
- Probe
- CeilingTests
- Nextcalibur on a machine that has never had the vendor software
- Nextcalibur 0.5.1
- CpuPowerReader.cs
- .ApplyProfile
- NotificationState
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.5
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- .Pick
- TourPage
- Log-Graphics.ps1
- Privacy
- Nextcalibur 0.6.2
- Security
- DriveRow
- .OnBrightnessChanged
- LedZone
- PowerSource
- SystemModesTests
- Notice
- Nextcalibur 0.5.11
- ComboBox
- SmiSubsystem
- .IsNotAGame
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
1. `MainWindow` - 390 edges
2. `Window` - 187 edges
3. `AppSettings` - 85 edges
4. `Nextcalibur.Core.Hardware` - 66 edges
5. `SystemMode` - 66 edges
6. `RadioButton` - 41 edges
7. `Nextcalibur.Core.Configuration` - 41 edges
8. `Nextcalibur.Core.Power` - 41 edges
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

## Communities (195 total, 32 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.03
Nodes (64): DeferredOverheat, GameSession, HwndSource, ObservableCollection, PluggedIn, Queue, SolidColorBrush, MainWindow (+56 more)

### Community 1 - "AppSettings"
Cohesion: 0.04
Nodes (62): DateTime, Dictionary, FnShortcut, JsonElement, Limits, List, ProgramRule, SystemMode (+54 more)

### Community 2 - ".OnLoaded"
Cohesion: 0.06
Nodes (16): AssemblyInformationalVersionAttribute, Asynchronous dispatcher & UI thread hardening, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary (+8 more)

### Community 3 - "ProtectedStore"
Cohesion: 0.07
Nodes (26): Content, DirectorySecurity, Hash, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Dictionary (+18 more)

### Community 4 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 5 - "FnShortcut"
Cohesion: 0.07
Nodes (33): DllImport, IEnumerable, IntPtr, List, StringBuilder, FnAction, FanTurbo, KeyboardBacklight (+25 more)

### Community 6 - "RunawayWatchTests"
Cohesion: 0.07
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 7 - "WindowsFaults"
Cohesion: 0.08
Nodes (34): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+26 more)

### Community 8 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 9 - "Window"
Cohesion: 0.08
Nodes (46): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+38 more)

### Community 10 - "ProcessPresence"
Cohesion: 0.08
Nodes (18): DateTime, IReadOnlyList, MachineHistory, DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence (+10 more)

### Community 11 - "RadioButton"
Cohesion: 0.06
Nodes (38): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeBalanced, ModeDiscrete (+30 more)

### Community 12 - ".Info"
Cohesion: 0.08
Nodes (7): CancelEventArgs, Guid, Ours, Plan, Exception, Log, Folder

### Community 13 - "PlanTuning"
Cohesion: 0.14
Nodes (17): Floor, Marker, Original, Action, AcDc, DllImport, Guid, IEnumerable (+9 more)

### Community 14 - "CoolingPolicyTests"
Cohesion: 0.08
Nodes (26): At, Margin, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc (+18 more)

### Community 15 - "xunit"
Cohesion: 0.11
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, nextcalibur_core_power_plantuning_acdc, system_text_regularexpressions, system_xml_linq, xunit

### Community 16 - ".StartSlowTimer"
Cohesion: 0.07
Nodes (9): DllImport, IntPtr, Name, Pid, IReadOnlyList, GameEnd, Func, Action (+1 more)

### Community 17 - "NetworkCostTests"
Cohesion: 0.11
Nodes (26): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+18 more)

### Community 18 - ".Survey"
Cohesion: 0.10
Nodes (10): StartupRegistration, IsEnabled, Func, IEnumerable, IReadOnlyList, RetirementEntry, Trace, Version (+2 more)

### Community 19 - ".Main"
Cohesion: 0.10
Nodes (9): The threat model, and what the code does about it, Application, DllImport, Mutex, App, OverlayRepairPending, RegistryKey, Footprint (+1 more)

### Community 20 - "WindowsFaultsTests"
Cohesion: 0.16
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 21 - ".RemoveMachineTraces"
Cohesion: 0.11
Nodes (11): Machine-wide modifications, dynamic, Elevation, Fact, InlineData, Theory, ElevationPolicyTests, Profile (+3 more)

### Community 22 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 23 - "UserPresence"
Cohesion: 0.13
Nodes (14): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, Point, Size, DateTime, DllImport (+6 more)

### Community 24 - "RoutedEventArgs"
Cohesion: 0.07
Nodes (9): Slider, LedPower, ReloadButton, SelectAll, Action, Color, RadioButton, RoutedEventArgs (+1 more)

### Community 25 - "CpuLimits"
Cohesion: 0.09
Nodes (18): Bit, IReadOnlySet, Now, Offset, PowerInfo, PowerLimits, Ratio, SinceBoot (+10 more)

### Community 26 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 27 - ".RequestRestart"
Cohesion: 0.11
Nodes (10): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr, MarshalAs (+2 more)

### Community 28 - "Button"
Cohesion: 0.07
Nodes (25): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, HideToTrayButton (+17 more)

### Community 29 - "SystemModeService"
Cohesion: 0.16
Nodes (10): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, Ours, Plan (+2 more)

### Community 30 - "system_windows"
Cohesion: 0.16
Nodes (16): Nextcalibur.App.Controls, system_windows, system_windows_controls, system_windows_controls_button, system_windows_controls_checkbox, system_windows_controls_combobox, system_windows_controls_comboboxitem, system_windows_controls_primitives (+8 more)

### Community 31 - "Nextcalibur.Core.Hardware"
Cohesion: 0.11
Nodes (5): Nextcalibur.Core.Input, Nextcalibur.Core.Hardware, system_management, system_runtime_interopservices, system_text

### Community 32 - "Nextcalibur.Core.Configuration"
Cohesion: 0.11
Nodes (9): Nextcalibur.Core.Configuration, microsoft_win32, Theme, Dark, Light, system_diagnostics, system_security, system_text_json (+1 more)

### Community 33 - ".Scan"
Cohesion: 0.10
Nodes (20): Display, IoCounters, ProcessLook, Dictionary, DllImport, IntPtr, IReadOnlyList, TimeSpan (+12 more)

### Community 34 - ".CheckProgram"
Cohesion: 0.11
Nodes (16): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvmlPath, IntegrityFinding (+8 more)

### Community 35 - ".SignatureIsValid"
Cohesion: 0.14
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 36 - "Nextcalibur.App"
Cohesion: 0.09
Nodes (15): Nextcalibur.App, system_componentmodel, system_drawing, system_io, system_io_directory, system_io_ioexception, system_io_path, system_linq (+7 more)

### Community 37 - "EcMailbox"
Cohesion: 0.16
Nodes (10): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+2 more)

### Community 38 - ".Get"
Cohesion: 0.15
Nodes (6): GameEnd, Task, List, MessageBoxButton, MessageBoxResult, ModeChoice

### Community 39 - "Fact"
Cohesion: 0.17
Nodes (8): IList, GpuLoad, GpuConfiguration, Fact, GpuModeTests, Discrete, Hybrid, HardwareSupportTests

### Community 40 - "MachineScanRecord"
Cohesion: 0.11
Nodes (18): DateTime, JsonSerializerOptions, List, MachineScan, RecordPath, MachineScanRecord, BiosVendor, BiosVersion (+10 more)

### Community 41 - "TrayPresence"
Cohesion: 0.12
Nodes (11): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, List, Ms, Window (+3 more)

### Community 42 - ".OnSystemModeChanged"
Cohesion: 0.14
Nodes (11): Option, PowerModeChangedEventArgs, PowerModeBalanced, PowerModeBest, PowerModeBetter, PowerModeEfficiency, Button, Guid (+3 more)

### Community 43 - ".ApplyFnShortcuts"
Cohesion: 0.12
Nodes (6): Problem, Detail, Dictionary, DllImport, IntPtr, KeyEventArgs

### Community 44 - ".Check"
Cohesion: 0.13
Nodes (9): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported, IdempotenceTests (+1 more)

### Community 45 - "DevicePowerSaving"
Cohesion: 0.17
Nodes (12): TextBlock, ToggleButton, AcDc, Guid, IEnumerable, DevicePowerSaving, Off, DeviceSaving (+4 more)

### Community 46 - "Dependency"
Cohesion: 0.13
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 47 - "GpuModeService"
Cohesion: 0.16
Nodes (10): ArgumentNullException, FirmwareModeReading, GpuMode, Discrete, Hybrid, Uma, GpuModeService, SwitchOutcome (+2 more)

### Community 48 - "CpuPowerReader"
Cohesion: 0.21
Nodes (6): ArgumentOutOfRangeException, DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 49 - "Authenticode"
Cohesion: 0.25
Nodes (14): CatalogInfo, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle, Authenticode (+6 more)

### Community 50 - "IntegrityTests"
Cohesion: 0.14
Nodes (7): Fact, InlineData, Theory, IntegrityTests, Current, Exe, Manifest

### Community 51 - ".Processor"
Cohesion: 0.15
Nodes (13): GpuPowerLimits, IEnumerable, PowerDefinition, PowerDefinitions, SetBy, Intel, LaptopMaker, Nextcalibur (+5 more)

### Community 52 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 53 - ".PlaceTourStep"
Cohesion: 0.10
Nodes (10): SizeChangedEventArgs, FrameworkElement, KeyEventArgs, RoutedEventArgs, TourStep, Body, SectionName, Title (+2 more)

### Community 54 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 55 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 56 - ".ShowResult"
Cohesion: 0.19
Nodes (8): List, IEnumerable, IReadOnlyList, TimeSpan, CeilingStep, CeilingTest, Fact, CeilingTestTests

### Community 57 - ".RefreshIdleRows"
Cohesion: 0.16
Nodes (6): ComboBox, Grid, Guid, DllImport, IntPtr, ScreenSaver

### Community 58 - "Fact"
Cohesion: 0.13
Nodes (12): OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact, CleanMachineTests (+4 more)

### Community 59 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 60 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 61 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 62 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 63 - ".Dictionary"
Cohesion: 0.15
Nodes (9): IReadOnlyDictionary, Stable, Dictionary, Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests (+1 more)

### Community 64 - ".For"
Cohesion: 0.18
Nodes (12): AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, Baseline (+4 more)

### Community 65 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 66 - "Program"
Cohesion: 0.18
Nodes (5): ConsoleColor, B, G, R, Program

### Community 67 - "ModeProfileTests"
Cohesion: 0.22
Nodes (7): MemberData, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 68 - "ToggleButton"
Cohesion: 0.11
Nodes (19): OverheatWarningToggle, SettingAskGameMode, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost, SettingFixWidgets (+11 more)

### Community 69 - "UpdateService"
Cohesion: 0.13
Nodes (12): DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService, Available, AvailableVersion (+4 more)

### Community 70 - "RuntimeDependencyTests"
Cohesion: 0.22
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 71 - "CeilingTestWindow"
Cohesion: 0.20
Nodes (9): CancellationTokenSource, Button, CancellationToken, CheckBox, Func, StackPanel, Task, TextBlock (+1 more)

### Community 72 - "PowerOverlayService"
Cohesion: 0.23
Nodes (7): DllImport, Guid, IReadOnlyList, PowerModeOption, PowerOverlays, All, PowerOverlayService

### Community 73 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 74 - ".ShouldAsk"
Cohesion: 0.20
Nodes (8): HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, Fact, TimeSpan, GameSessionsTests

### Community 75 - "Nextcalibur.Core.Tests.Attacks"
Cohesion: 0.20
Nodes (5): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http

### Community 76 - "CoreLoad"
Cohesion: 0.15
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 77 - ".Read"
Cohesion: 0.18
Nodes (8): SmiFamily, Read, Write, ThermalProfile, Fact, InlineData, Theory, ThermalProfileTests

### Community 78 - ".GetColour"
Cohesion: 0.24
Nodes (7): B, G, R, Fact, InlineData, Theory, LedStateTests

### Community 79 - "ProgramRule"
Cohesion: 0.26
Nodes (10): DateTime, IEnumerable, IReadOnlyDictionary, ProgramRule, Enabled, Process, DateTime, Dictionary (+2 more)

### Community 80 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 81 - "Nextcalibur.Core.Security"
Cohesion: 0.20
Nodes (6): Nextcalibur.Cli, Nextcalibur.Core.Security, system_security_accesscontrol, system_security_cryptography, system_security_cryptography_x509certificates, system_security_principal

### Community 82 - "DevicePowerState"
Cohesion: 0.25
Nodes (6): DevPropKey, Discrete GPU sleep protection in Hybrid mode, DllImport, Guid, DevicePowerState, DevPropKey

### Community 83 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 84 - "LedState"
Cohesion: 0.13
Nodes (15): Dictionary, LedBrightness, LedProfile, BrightnessPercent, Colours, Effect, LedState, ActiveProfile (+7 more)

### Community 85 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 86 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 87 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 88 - "BatteryModePolicy"
Cohesion: 0.35
Nodes (5): BatteryModePolicy, QuietMode, Remembered, Fact, BatteryModePolicyTests

### Community 89 - "IdleSetting"
Cohesion: 0.24
Nodes (8): AcDc, Guid, IReadOnlyList, IdleSetting, DisplayOff, Sleep, IdleTimeouts, Timeouts

### Community 90 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 91 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 92 - "InstallFolderGuard"
Cohesion: 0.30
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 93 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 94 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 95 - "AcDc"
Cohesion: 0.26
Nodes (5): AcDc, InlineData, Theory, Fact, IdleTimeoutsTests

### Community 96 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 97 - "SystemMode"
Cohesion: 0.21
Nodes (8): IReadOnlyList, SystemMode, Balanced, Gaming, Office, Performance, SystemModes, InOrder

### Community 98 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 99 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 100 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 101 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 102 - "Nextcalibur 0.5.4"
Cohesion: 0.17
Nodes (11): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+3 more)

### Community 103 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 104 - ".Sanitised"
Cohesion: 0.26
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 105 - "PackageTemperatureTests"
Cohesion: 0.41
Nodes (4): Fact, InlineData, Theory, PackageTemperatureTests

### Community 106 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 107 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 108 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 109 - "StackPanel"
Cohesion: 0.18
Nodes (11): BannerActions, CeilingRows, DialogActions, DriveTextStack, FnShortcutRows, IdleRows, PanelWindowsFaultsSubOptions, ProfileRow (+3 more)

### Community 110 - "SupportVerdict"
Cohesion: 0.20
Nodes (10): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+2 more)

### Community 111 - "MachineWideGate"
Cohesion: 0.20
Nodes (6): Mutex, TimeSpan, MachineWideGate, system_threading, Task, MachineWideGateTests

### Community 112 - ".Normalise"
Cohesion: 0.27
Nodes (5): GeneratedRegex, Regex, ProgramRules, InlineData, Theory

### Community 113 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 114 - "ThemeService"
Cohesion: 0.20
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 115 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 116 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 117 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 118 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 119 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 121 - "LedEffect"
Cohesion: 0.22
Nodes (8): LedEffect, Blink, Breathing, ColourCycle, Heartbeat, Off, Static, Wave

### Community 122 - "ThermalSample"
Cohesion: 0.33
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 123 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 124 - ".IsSystemFileSignedBy"
Cohesion: 0.39
Nodes (3): Fact, SystemFileTrustTests, Cmd

### Community 125 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 126 - "CeilingTests"
Cohesion: 0.31
Nodes (5): Baseline, Fact, InlineData, Theory, CeilingTests

### Community 127 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.25
Nodes (7): Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 128 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 129 - "CpuPowerReader.cs"
Cohesion: 0.25
Nodes (5): microsoft_win32_safehandles, system_reflection, InlineData, Theory, PawnIoModuleTests

### Community 131 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 132 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 133 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 134 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 135 - "Nextcalibur 0.5.5"
Cohesion: 0.29
Nodes (5): Deep memory & resource leak eradication, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture

### Community 136 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 137 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 138 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 139 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 140 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 141 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 142 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 143 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 144 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 145 - "DriveRow"
Cohesion: 0.33
Nodes (5): INotifyPropertyChanged, DriveRow, Name, Percent, PercentText

### Community 146 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 147 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 148 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 150 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 151 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 152 - "ComboBox"
Cohesion: 0.40
Nodes (5): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, ComboBox

### Community 153 - "SmiSubsystem"
Cohesion: 0.40
Nodes (5): SmiSubsystem, DisplayMode, Led, Profile, Thermal

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

### Community 161 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 162 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 163 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 164 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 166 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 169 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 170 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

### Community 171 - "Words"
Cohesion: 0.67
Nodes (3): Func, Words, Resolver

## Knowledge Gaps
- **512 isolated node(s):** `OverlayRepairPending`, `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum` (+507 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1010 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **32 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `AppSettings`, `.OnLoaded`, `.ApplyProfile`, `GpuClockReader`, `FnShortcut`, `RunawayWatchTests`, `Nextcalibur 0.5.5`, `WindowsFaults`, `Window`, `ProcessPresence`, `RadioButton`, `.Info`, `PlanTuning`, `CoolingPolicyTests`, `TourPage`, `.StartSlowTimer`, `.OnBrightnessChanged`, `LedZone`, `SystemInfo`, `UserPresence`, `RoutedEventArgs`, `.RequestRestart`, `Button`, `SystemModeService`, `system_windows`, `Nextcalibur.Core.Hardware`, `Nextcalibur.Core.Configuration`, `.CheckProgram`, `Nextcalibur.App`, `EcMailbox`, `.Get`, `Fact`, `TrayPresence`, `.OnSystemModeChanged`, `.ApplyFnShortcuts`, `DevicePowerSaving`, `GpuModeService`, `CpuPowerReader`, `.PlaceTourStep`, `.RefreshIdleRows`, `BacklightKeyWatcher`, `DependencyStatus`, `UpdateService`, `CeilingTestWindow`, `PowerOverlayService`, `.Read`, `ProgramRule`, `LedController`, `BatteryModePolicy`, `IdleSetting`, `AcDc`, `Grid`, `SystemMode`, `Strings`, `SupportVerdict`, `.Normalise`, `ThemeService`, `NvidiaDriverState`, `LedEffect`, `ThermalSample`?**
  _High betweenness centrality (0.457) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Text`, `RadioButton`, `.OnBrightnessChanged`, `ComboBox`, `RoutedEventArgs`, `Button`, `Keycaps`, `DriveGauge`, `CpuWarnValue`, `DialogCheck`, `.OnSystemModeChanged`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `ToggleButton`, `Grid`, `StackPanel`, `Border`?**
  _High betweenness centrality (0.097) - this node is a cross-community bridge._
- **Why does `Nextcalibur.Core.Hardware` connect `Nextcalibur.Core.Hardware` to `Nextcalibur.Core.Configuration`, `CpuPowerReader.cs`, `Nextcalibur.App`, `EcMailbox`, `Nextcalibur.Core.Tests.Attacks`, `SupportVerdict`, `MachineWideGate`, `xunit`, `Nextcalibur.Core.Security`, `.Processor`, `CpuLimits`, `ThermalSample`, `BacklightKeyWatcher`, `system_windows`?**
  _High betweenness centrality (0.066) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `OverlayRepairPending`, `SelectedColour`, `VisualChildrenCount` to the rest of the system?**
  _512 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.029258098223615466 - nodes in this community are weakly interconnected._
- **Should `AppSettings` be split into smaller, more focused modules?**
  _Cohesion score 0.03502051983584131 - nodes in this community are weakly interconnected._