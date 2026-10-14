# Graph Report - nextcalibur-control-center  (2026-10-14)

## Corpus Check
- 205 files · ~251,686 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3467 nodes · 7684 edges · 192 communities (157 shown, 35 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 465 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `a75fd119`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- AppSettings
- FnShortcut
- ProtectedStore
- GpuClockReader
- RunawayWatchTests
- Nextcalibur.Core.Configuration
- TrayPresence
- WindowsFaults
- .Get
- ProgramRule
- IShellLinkW
- Window
- SystemTools
- RadioButton
- PlanTuning
- LedState
- .OnLoaded
- .StartSlowTimer
- Nextcalibur.Core.Hardware
- CoolingPolicyTests
- .Info
- .WireSettingsPage
- NetworkCostTests
- Fact
- LedController
- UserPresence
- .Main
- CpuLimits
- SystemInfo
- WindowsFaultsTests
- Api
- ResourceDictionary
- Elevation
- Fact
- Button
- .RequestRestart
- .Scan
- BatteryModePolicy
- InstallFolderGuard
- EcMailbox
- .Get
- SystemModeService
- Dependency
- .SignatureIsValid
- .Check
- .LoadDefinitionsAsync
- FirmwareInventory
- Integrity
- CpuPowerReader
- Authenticode
- microsoft_win32
- PowerOverlayService
- ModeProfile
- .Ask
- .For
- .Read
- DotNetRuntimeDependency
- UpdateService
- Program
- RestorePoint
- Nextcalibur.Core.csproj
- SystemMode
- DevicePowerSaving
- ToggleButton
- MachineScanRecord
- IdleSetting
- CeilingTestWindow
- MemoryTrimmer
- DependencyStatus
- .ShowResult
- .Dictionary
- .CheckProgram
- analyse-autopsy.py
- system_runtime_interopservices
- ProcessPresence
- Fact
- TemperatureFusion
- CoreLoad
- .ApplyFnShortcuts
- RuntimeDependencyTests
- system_diagnostics
- .ResolveTourParts
- ColourWheel
- Nextcalibur.Core.Security
- BacklightKeyWatcher
- FanGauge
- Unelevated
- DonutGauge
- MachineHistory
- Hardware Protocol
- SegmentedBar
- .Set
- Nextcalibur Control Center
- LogInjectionTests
- StackPanel
- Grid
- .HasAccess
- VendorSoftware
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- Strings
- .RunawayTick
- .RefreshIdleRows
- PackageTemperatureTests
- SmiCommandTests
- Nextcalibur 0.5.3
- NvidiaDriverState
- IntPtr
- Border
- .RunAndRecord
- SupportVerdict
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- ThermalSample
- Log
- PawnIoDependency
- .Parse
- .IsSystemFileSignedBy
- Probe
- CeilingTests
- Nextcalibur 0.5.1
- NotificationState
- IntegrityKind
- Test-Ui.ps1
- Text
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- .Pick
- TourPage
- .Current
- Log-Graphics.ps1
- Privacy
- Nextcalibur 0.6.2
- Security
- .OnBrightnessChanged
- ComboBox
- .Load
- MachineWideGate
- Notice
- Nextcalibur 0.5.11
- .Set
- .ParseMarker
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
- .ArrangeOverride
- .Modules_are_the_released_ones
- Tools
- 0.5.7.md
- CpuWarnValue
- DialogCheck
- Toasts.cs
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

## God Nodes (most connected - your core abstractions)
1. `MainWindow` - 410 edges
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

## Communities (192 total, 35 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.02
Nodes (79): DeferredOverheat, GameSession, HwndSource, ObservableCollection, Option, PluggedIn, Queue, MainWindow (+71 more)

### Community 1 - "AppSettings"
Cohesion: 0.03
Nodes (61): DateTime, Dictionary, FnShortcut, JsonElement, Limits, List, ProgramRule, SystemMode (+53 more)

### Community 2 - "FnShortcut"
Cohesion: 0.05
Nodes (42): Nextcalibur.Core.Input, INotifyPropertyChanged, Problem, DriveRow, Detail, Name, Percent, PercentText (+34 more)

### Community 3 - "ProtectedStore"
Cohesion: 0.07
Nodes (26): Content, DirectorySecurity, Hash, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Dictionary (+18 more)

### Community 4 - "GpuClockReader"
Cohesion: 0.08
Nodes (22): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+14 more)

### Community 5 - "RunawayWatchTests"
Cohesion: 0.06
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 6 - "Nextcalibur.Core.Configuration"
Cohesion: 0.10
Nodes (25): Nextcalibur.App, Nextcalibur.App.Controls, Nextcalibur.Core.Configuration, system_componentmodel, system_drawing, system_linq, system_runtime_compilerservices, system_windows (+17 more)

### Community 7 - "TrayPresence"
Cohesion: 0.06
Nodes (22): ContextMenuStrip, DevPropKey, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture (+14 more)

### Community 8 - "WindowsFaults"
Cohesion: 0.07
Nodes (34): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+26 more)

### Community 9 - ".Get"
Cohesion: 0.09
Nodes (9): GameEnd, PowerModeChangedEventArgs, SizeChangedEventArgs, Grid, List, Guid, Ours, Plan (+1 more)

### Community 10 - "ProgramRule"
Cohesion: 0.08
Nodes (25): GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, IEnumerable (+17 more)

### Community 11 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 12 - "Window"
Cohesion: 0.08
Nodes (46): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+38 more)

### Community 13 - "SystemTools"
Cohesion: 0.06
Nodes (25): FileStream, IOException, Theme, Theme, Dark, Light, ThemePreference, Dark (+17 more)

### Community 14 - "RadioButton"
Cohesion: 0.06
Nodes (42): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeBalanced, ModeDiscrete (+34 more)

### Community 15 - "PlanTuning"
Cohesion: 0.12
Nodes (19): Floor, Marker, Original, Action, AcDc, DllImport, Guid, IEnumerable (+11 more)

### Community 16 - "LedState"
Cohesion: 0.08
Nodes (26): B, Dictionary, G, LedBrightness, R, LedProfile, BrightnessPercent, Colours (+18 more)

### Community 17 - ".OnLoaded"
Cohesion: 0.08
Nodes (9): AssemblyInformationalVersionAttribute, Asynchronous dispatcher & UI thread hardening, Slider, Task, Task, EventArgs, IProgress, Exception (+1 more)

### Community 18 - ".StartSlowTimer"
Cohesion: 0.07
Nodes (4): SolidColorBrush, IReadOnlyList, GameEnd, Func

### Community 19 - "Nextcalibur.Core.Hardware"
Cohesion: 0.13
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, Nextcalibur.Core.Hardware, nextcalibur_core_power_plantuning_acdc, system_xml_linq, xunit

### Community 20 - "CoolingPolicyTests"
Cohesion: 0.08
Nodes (26): At, Margin, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc (+18 more)

### Community 21 - ".Info"
Cohesion: 0.08
Nodes (6): Guid, Ours, Plan, MessageBoxButton, MessageBoxResult, IReadOnlyDictionary

### Community 22 - ".WireSettingsPage"
Cohesion: 0.09
Nodes (10): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary, IReadOnlyDictionary, Task (+2 more)

### Community 23 - "NetworkCostTests"
Cohesion: 0.11
Nodes (26): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+18 more)

### Community 24 - "Fact"
Cohesion: 0.13
Nodes (9): Func, IEnumerable, IReadOnlyList, RetirementEntry, Version, NduFix, Fact, FootprintTests (+1 more)

### Community 25 - "LedController"
Cohesion: 0.09
Nodes (22): LedBrightness, Full, Half, Off, LedController, EffectiveBrightnessPercent, HardwareLevel, State (+14 more)

### Community 26 - "UserPresence"
Cohesion: 0.12
Nodes (14): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, Point, Size, DateTime, DllImport (+6 more)

### Community 27 - ".Main"
Cohesion: 0.12
Nodes (9): The threat model, and what the code does about it, Application, DllImport, Mutex, App, OverlayRepairPending, RegistryKey, Footprint (+1 more)

### Community 28 - "CpuLimits"
Cohesion: 0.09
Nodes (18): Bit, IReadOnlySet, Now, Offset, PowerInfo, PowerLimits, Ratio, SinceBoot (+10 more)

### Community 29 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 31 - "Api"
Cohesion: 0.10
Nodes (23): Api, GetSettingFn, InitializeFn, ProfileOutFn, SessionFn, SessionOutFn, Set, SetSettingFn (+15 more)

### Community 32 - "ResourceDictionary"
Cohesion: 0.08
Nodes (29): ActualWidth, IsDropDownOpen, Arrow, Bg, CardBorder, Indicator, Knob, PART_EditableTextBox (+21 more)

### Community 33 - "Elevation"
Cohesion: 0.13
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 34 - "Fact"
Cohesion: 0.15
Nodes (9): ArgumentNullException, IList, HardwareSupport, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests (+1 more)

### Community 35 - "Button"
Cohesion: 0.07
Nodes (27): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, HideToTrayButton (+19 more)

### Community 36 - ".RequestRestart"
Cohesion: 0.12
Nodes (10): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr, MarshalAs (+2 more)

### Community 37 - ".Scan"
Cohesion: 0.10
Nodes (20): Display, IoCounters, ProcessLook, Dictionary, DllImport, IntPtr, IReadOnlyList, TimeSpan (+12 more)

### Community 38 - "BatteryModePolicy"
Cohesion: 0.15
Nodes (11): DllImport, BatteryModePolicy, QuietMode, Remembered, PowerSource, SystemPowerStatus, SystemPowerStatus, Fact (+3 more)

### Community 39 - "InstallFolderGuard"
Cohesion: 0.14
Nodes (10): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Fact, InlineData, Theory, ElevationPolicyTests (+2 more)

### Community 40 - "EcMailbox"
Cohesion: 0.16
Nodes (10): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+2 more)

### Community 41 - ".Get"
Cohesion: 0.15
Nodes (12): FirmwareModeReading, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma, GpuModeService, SwitchOutcome (+4 more)

### Community 42 - "SystemModeService"
Cohesion: 0.19
Nodes (8): Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, Ours, Plan, SystemModeService

### Community 43 - "Dependency"
Cohesion: 0.11
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 44 - ".SignatureIsValid"
Cohesion: 0.15
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 45 - ".Check"
Cohesion: 0.13
Nodes (10): CommonAce, RawSecurityDescriptor, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported (+2 more)

### Community 46 - ".LoadDefinitionsAsync"
Cohesion: 0.14
Nodes (14): Task, GpuPowerLimits, IEnumerable, PowerDefinition, PowerDefinitions, SetBy, Intel, LaptopMaker (+6 more)

### Community 47 - "FirmwareInventory"
Cohesion: 0.08
Nodes (23): IReadOnlyList, FirmwareInventory, BatteryChemistry, BatteryDesignMwh, BatteryName, BiosDate, BiosRelease, BiosVendor (+15 more)

### Community 48 - "Integrity"
Cohesion: 0.12
Nodes (11): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvapiPath, NvmlPath (+3 more)

### Community 49 - "CpuPowerReader"
Cohesion: 0.20
Nodes (6): ArgumentOutOfRangeException, DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 50 - "Authenticode"
Cohesion: 0.25
Nodes (14): CatalogInfo, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle, Authenticode (+6 more)

### Community 51 - "microsoft_win32"
Cohesion: 0.15
Nodes (9): Nextcalibur.Core.Dependencies, microsoft_win32, Trace, system_collections_concurrent, system_net, system_net_http, system_security, system_text_json (+1 more)

### Community 52 - "PowerOverlayService"
Cohesion: 0.17
Nodes (11): DllImport, Guid, IReadOnlyList, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, PowerModeOption (+3 more)

### Community 53 - "ModeProfile"
Cohesion: 0.13
Nodes (12): AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, Baseline (+4 more)

### Community 54 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 55 - ".For"
Cohesion: 0.22
Nodes (8): MemberData, Baseline, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 56 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 57 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 58 - "UpdateService"
Cohesion: 0.12
Nodes (13): CancelEventArgs, DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService, Available (+5 more)

### Community 59 - "Program"
Cohesion: 0.17
Nodes (5): ConsoleColor, B, G, R, Program

### Community 60 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 61 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 62 - "SystemMode"
Cohesion: 0.16
Nodes (10): InvalidOperationException, IReadOnlyList, SystemMode, Balanced, Gaming, Office, Performance, SystemModes (+2 more)

### Community 63 - "DevicePowerSaving"
Cohesion: 0.20
Nodes (10): AcDc, Guid, IEnumerable, DevicePowerSaving, Off, DeviceSaving, Usb, Wifi (+2 more)

### Community 64 - "ToggleButton"
Cohesion: 0.10
Nodes (21): LedPower, OverheatWarningToggle, SelectAll, SettingAskGameMode, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice (+13 more)

### Community 65 - "MachineScanRecord"
Cohesion: 0.10
Nodes (21): DateTime, List, MachineScanRecord, BiosDate, BiosRelease, BiosVendor, BiosVersion, BoardProduct (+13 more)

### Community 66 - "IdleSetting"
Cohesion: 0.17
Nodes (10): AcDc, Guid, IReadOnlyList, IdleSetting, DisplayOff, Sleep, IdleTimeouts, Timeouts (+2 more)

### Community 67 - "CeilingTestWindow"
Cohesion: 0.19
Nodes (11): CancellationTokenSource, Button, CancellationToken, CheckBox, Func, List, StackPanel, Task (+3 more)

### Community 68 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 69 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 70 - ".ShowResult"
Cohesion: 0.20
Nodes (7): IEnumerable, IReadOnlyList, TimeSpan, CeilingStep, CeilingTest, Fact, CeilingTestTests

### Community 71 - ".Dictionary"
Cohesion: 0.15
Nodes (9): IReadOnlyDictionary, Stable, Dictionary, Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests (+1 more)

### Community 72 - ".CheckProgram"
Cohesion: 0.20
Nodes (5): Fact, IntegrityTests, Current, Exe, Manifest

### Community 73 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 74 - "system_runtime_interopservices"
Cohesion: 0.11
Nodes (9): microsoft_win32_safehandles, system_io, system_io_directory, system_io_ioexception, system_io_path, system_management, system_reflection, system_runtime_interopservices (+1 more)

### Community 75 - "ProcessPresence"
Cohesion: 0.23
Nodes (8): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, Fact, ProcessPresenceTests, Self

### Community 76 - "Fact"
Cohesion: 0.14
Nodes (10): RepairOutcome, Empty, Fact, Task, CleanMachineTests, Guarded, Unguarded, CoexistencePolicyTests (+2 more)

### Community 77 - "TemperatureFusion"
Cohesion: 0.25
Nodes (9): DateTime, TimeSpan, TemperatureFusion, Offset, RegisterSetAside, Value, DateTime, Fact (+1 more)

### Community 78 - "CoreLoad"
Cohesion: 0.14
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 79 - ".ApplyFnShortcuts"
Cohesion: 0.15
Nodes (3): DllImport, IntPtr, KeyEventArgs

### Community 80 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 81 - "system_diagnostics"
Cohesion: 0.18
Nodes (6): Nextcalibur.Cli, system_diagnostics, system_security_accesscontrol, system_security_cryptography, system_security_principal, system_text

### Community 82 - ".ResolveTourParts"
Cohesion: 0.14
Nodes (7): KeyEventArgs, RoutedEventArgs, TourStep, Body, SectionName, Title, TourPage

### Community 83 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 84 - "Nextcalibur.Core.Security"
Cohesion: 0.16
Nodes (4): Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_security_cryptography_x509certificates, system_text_regularexpressions

### Community 85 - "BacklightKeyWatcher"
Cohesion: 0.14
Nodes (8): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, Fact, InlineData, Theory, BacklightKeyTests

### Community 86 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 87 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 88 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 89 - "MachineHistory"
Cohesion: 0.28
Nodes (6): DateTime, IReadOnlyList, MachineHistory, DateTime, Fact, MachineHistoryTests

### Community 90 - "Hardware Protocol"
Cohesion: 0.14
Nodes (14): 1. Transport — ACPI-WMI mailbox, 3. Thermal / fan — `a1 = 0x0200`, 4. LED — `a1 = 0x0100`, 6. Safety rules, 7. Not supported, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`) (+6 more)

### Community 91 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 92 - ".Set"
Cohesion: 0.30
Nodes (7): PowerThrottlingState, DllImport, IntPtr, EfficiencyMode, PowerThrottlingState, Fact, EfficiencyModeTests

### Community 93 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 94 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 95 - "StackPanel"
Cohesion: 0.15
Nodes (13): BannerActions, CeilingRows, DefinitionRows, DialogActions, DriveTextStack, FnShortcutRows, IdleLoadRows, IdleRows (+5 more)

### Community 96 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 97 - ".HasAccess"
Cohesion: 0.28
Nodes (5): SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 98 - "VendorSoftware"
Cohesion: 0.21
Nodes (4): DateTime, TimeSpan, VendorSoftware, UninstallCommand

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

### Community 104 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 105 - ".RunawayTick"
Cohesion: 0.21
Nodes (6): DllImport, IntPtr, Name, Pid, Action, Toasts

### Community 106 - ".RefreshIdleRows"
Cohesion: 0.27
Nodes (3): ComboBox, Grid, Guid

### Community 107 - "PackageTemperatureTests"
Cohesion: 0.41
Nodes (4): Fact, InlineData, Theory, PackageTemperatureTests

### Community 108 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 109 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 110 - "NvidiaDriverState"
Cohesion: 0.27
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 111 - "IntPtr"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 112 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 113 - ".RunAndRecord"
Cohesion: 0.29
Nodes (4): JsonSerializerOptions, MachineScan, Firmware, RecordPath

### Community 114 - "SupportVerdict"
Cohesion: 0.22
Nodes (9): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites (+1 more)

### Community 115 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 116 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 117 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 118 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 119 - "ThermalSample"
Cohesion: 0.33
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 121 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 122 - ".Parse"
Cohesion: 0.33
Nodes (3): List, Fact, FirmwareInventoryTests

### Community 123 - ".IsSystemFileSignedBy"
Cohesion: 0.39
Nodes (3): Fact, SystemFileTrustTests, Cmd

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

### Community 131 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 132 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 133 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 134 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 135 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 136 - ".Current"
Cohesion: 0.48
Nodes (3): DllImport, IntPtr, ScreenSaver

### Community 137 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 138 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 139 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 140 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 141 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 142 - "ComboBox"
Cohesion: 0.33
Nodes (6): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, SettingNvPower, ComboBox

### Community 144 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 145 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 146 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 149 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 151 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 152 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 153 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 155 - "2. Command structure"
Cohesion: 0.50
Nodes (4): 2. Command structure, Call sequence, Command families (`a0`), Subsystems (`a1`)

### Community 156 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 157 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 158 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 159 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 161 - ".Modules_are_the_released_ones"
Cohesion: 0.50
Nodes (3): InlineData, Theory, PawnIoModuleTests

### Community 162 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 165 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 166 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

## Knowledge Gaps
- **562 isolated node(s):** `OverlayRepairPending`, `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum` (+557 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1078 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **35 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `AppSettings`, `FnShortcut`, `GpuClockReader`, `RunawayWatchTests`, `Nextcalibur.Core.Configuration`, `TrayPresence`, `TourPage`, `.Get`, `ProgramRule`, `Window`, `.OnBrightnessChanged`, `RadioButton`, `PlanTuning`, `SystemTools`, `.OnLoaded`, `.StartSlowTimer`, `CoolingPolicyTests`, `.Info`, `.WireSettingsPage`, `LedController`, `UserPresence`, `Button`, `.RequestRestart`, `.Scan`, `BatteryModePolicy`, `EcMailbox`, `.Get`, `SystemModeService`, `.LoadDefinitionsAsync`, `Integrity`, `CpuPowerReader`, `PowerOverlayService`, `ModeProfile`, `.Read`, `UpdateService`, `SystemMode`, `DevicePowerSaving`, `IdleSetting`, `CeilingTestWindow`, `DependencyStatus`, `system_runtime_interopservices`, `TemperatureFusion`, `.ApplyFnShortcuts`, `.ResolveTourParts`, `BacklightKeyWatcher`, `Grid`, `Strings`, `.RunawayTick`, `.RefreshIdleRows`, `NvidiaDriverState`, `SupportVerdict`, `ThermalSample`?**
  _High betweenness centrality (0.503) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Text`, `.OnBrightnessChanged`, `RadioButton`, `ComboBox`, `Keycaps`, `DriveGauge`, `Button`, `CpuWarnValue`, `DialogCheck`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `ToggleButton`, `StackPanel`, `Grid`, `Border`?**
  _High betweenness centrality (0.096) - this node is a cross-community bridge._
- **Why does `Nextcalibur.Core.Hardware` connect `Nextcalibur.Core.Hardware` to `GpuClockReader`, `RunawayWatchTests`, `Nextcalibur.Core.Configuration`, `TrayPresence`, `WindowsFaults`, `MachineWideGate`, `LedController`, `UserPresence`, `CpuLimits`, `Api`, `Elevation`, `EcMailbox`, `.Get`, `.LoadDefinitionsAsync`, `FirmwareInventory`, `microsoft_win32`, `system_runtime_interopservices`, `CoreLoad`, `system_diagnostics`, `Nextcalibur.Core.Security`, `BacklightKeyWatcher`, `VendorSoftware`, `NvidiaDriverState`, `SupportVerdict`, `ThermalSample`?**
  _High betweenness centrality (0.078) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `OverlayRepairPending`, `SelectedColour`, `VisualChildrenCount` to the rest of the system?**
  _562 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.023757786469650877 - nodes in this community are weakly interconnected._
- **Should `AppSettings` be split into smaller, more focused modules?**
  _Cohesion score 0.03333333333333333 - nodes in this community are weakly interconnected._