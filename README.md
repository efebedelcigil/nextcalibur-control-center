# Graph Report - nextcalibur-control-center  (2026-10-15)

## Corpus Check
- 217 files · ~264,237 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3713 nodes · 8252 edges · 203 communities (169 shown, 34 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 493 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `caa7769c`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- AppSettings
- CeilingTestWindow
- FnShortcut
- GpuClockReader
- ProtectedStore
- Window
- ProgramRule
- .OnLoaded
- RunawayWatchTests
- .Get
- .Info
- WindowsFaults
- IShellLinkW
- Nextcalibur.App
- Nextcalibur.Core.Hardware
- RadioButton
- system_runtime_interopservices
- PlanTuning
- ResourceDictionary
- TextBlock
- Nextcalibur.Core.Configuration
- CoolingPolicyTests
- .Warn
- GameLibrary
- NetworkCostTests
- .Main
- Fact
- .For
- SystemInfo
- Elevation
- UserPresence
- WindowsFaultsTests
- .RequestRestart
- SystemTools
- CrashGuard
- SystemMode
- Api
- EcMailbox
- .Choose
- ProcessPresence
- Program
- .OnSystemModeChanged
- LedState
- .Get
- UpdateService
- CpuLimits
- Fact
- .SignatureIsValid
- Nextcalibur.Core.Security
- Integrity
- DevicePowerSaving
- Dependency
- Authenticode
- .Check
- .PlaceTourStep
- SystemModeService
- .Processor
- FirmwareInventory
- IdleSetting
- ColourWheel
- .Ask
- DotNetRuntimeDependency
- MachineScanRecord
- Nextcalibur.Core.csproj
- .CheckProgram
- Fact
- RestorePoint
- MemoryTrimmer
- DependencyStatus
- CpuPowerReader
- .Dictionary
- analyse-autopsy.py
- BacklightKeyWatcher
- ModeProfileTests
- DarkMenuColours
- RuntimeDependencyTests
- TemperatureFusion
- SmiCommand
- CoreLoad
- PowerOverlayService
- TrayPresence
- .ApplyFnShortcuts
- LedController
- FanGauge
- DevicePowerState
- Unelevated
- DonutGauge
- .RefreshIdleRows
- MachineHistory
- Hardware Protocol
- SegmentedBar
- .Set
- Nextcalibur Control Center
- LedEffect
- Log
- .RunAndRecord
- .HasAccess
- LogInjectionTests
- Grid
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- Strings
- StackPanel
- InstallFolderGuard
- PackageTemperatureTests
- AcDc
- Nextcalibur 0.5.3
- ThemeService
- CardSwitchTasks
- Border
- .Sanitised
- NvidiaDriverState
- .Parse
- SupportVerdict
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- PawnIoDependency
- .IsSystemFileSignedBy
- Probe
- CeilingTests
- Nextcalibur 0.5.1
- Nextcalibur 0.5.5
- NoticeGate
- Toasts
- IntegrityKind
- Test-Ui.ps1
- Text
- .BuildMenu
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- .DecodePowerInfo
- ColGauge
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
- Notice
- Nextcalibur 0.5.11
- .Set
- SmiSubsystem
- .Read
- .FirmwareChanged
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
- .Quietly
- .ArrangeOverride
- OverlayDiagnosis
- .Modules_are_the_released_ones
- Tools
- 0.5.7.md
- CpuWarnValue
- EffectPanel
- .OnMinimiseToTrayClick
- PART_ContentHost
- .Paint
- UnsafeModeException
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
1. `MainWindow` - 436 edges
2. `Window` - 198 edges
3. `AppSettings` - 92 edges
4. `Nextcalibur.Core.Hardware` - 77 edges
5. `SystemMode` - 68 edges
6. `Nextcalibur.Core.Power` - 50 edges
7. `Nextcalibur.Core.Configuration` - 49 edges
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

## Communities (203 total, 34 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.02
Nodes (82): Added, DeferredOverheat, Found, GameSession, HwndSource, ObservableCollection, PluggedIn, Queue (+74 more)

### Community 1 - "AppSettings"
Cohesion: 0.03
Nodes (67): DateTime, Dictionary, FnShortcut, JsonElement, Limits, List, ProgramRule, SystemMode (+59 more)

### Community 2 - "CeilingTestWindow"
Cohesion: 0.06
Nodes (34): CancellationTokenSource, Display, IoCounters, ProcessLook, Button, CancellationToken, CheckBox, Func (+26 more)

### Community 3 - "FnShortcut"
Cohesion: 0.05
Nodes (42): INotifyPropertyChanged, Problem, DriveRow, Detail, Name, Percent, PercentText, Dictionary (+34 more)

### Community 4 - "GpuClockReader"
Cohesion: 0.07
Nodes (25): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, Detail, Func, Ok, SelfTest, DateTime (+17 more)

### Community 5 - "ProtectedStore"
Cohesion: 0.06
Nodes (26): Content, DirectorySecurity, Hash, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Dictionary (+18 more)

### Community 6 - "Window"
Cohesion: 0.06
Nodes (58): TemperatureToDoubleConverter, Foreground, IsChecked, IsMouseOver, ItemsSource.Count, Name, BannerDismissButton, BannerRestartButton (+50 more)

### Community 7 - "ProgramRule"
Cohesion: 0.06
Nodes (33): HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, GeneratedRegex, IEnumerable (+25 more)

### Community 8 - ".OnLoaded"
Cohesion: 0.07
Nodes (14): AssemblyInformationalVersionAttribute, Asynchronous dispatcher & UI thread hardening, GameEnd, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner (+6 more)

### Community 9 - "RunawayWatchTests"
Cohesion: 0.07
Nodes (34): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+26 more)

### Community 10 - ".Get"
Cohesion: 0.07
Nodes (7): SolidColorBrush, Task, Grid, Task, Action, Task, ToggleButton

### Community 11 - ".Info"
Cohesion: 0.08
Nodes (7): Task, FrameworkElement, Func, Grid, HashSet, List, ModeChoice

### Community 12 - "WindowsFaults"
Cohesion: 0.08
Nodes (34): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+26 more)

### Community 13 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 14 - "Nextcalibur.App"
Cohesion: 0.10
Nodes (24): Nextcalibur.App, Nextcalibur.App.Controls, system_componentmodel, system_drawing, system_linq, system_runtime_compilerservices, system_windows, system_windows_controls (+16 more)

### Community 15 - "Nextcalibur.Core.Hardware"
Cohesion: 0.12
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, Nextcalibur.Core.Hardware, nextcalibur_core_power_plantuning_acdc, system_xml_linq, xunit

### Community 16 - "RadioButton"
Cohesion: 0.06
Nodes (42): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeBalanced, ModeDiscrete (+34 more)

### Community 17 - "system_runtime_interopservices"
Cohesion: 0.06
Nodes (12): Nextcalibur.Cli, Nextcalibur.Core.Input, microsoft_win32_safehandles, system_diagnostics, system_io_directory, system_io_ioexception, system_io_path, system_management (+4 more)

### Community 18 - "PlanTuning"
Cohesion: 0.13
Nodes (18): Floor, Marker, Original, Action, AcDc, DllImport, Guid, IEnumerable (+10 more)

### Community 19 - "ResourceDictionary"
Cohesion: 0.07
Nodes (40): ActualWidth, IsDropDownOpen, Size, Arrow, Bar, Bg, Blade, Box (+32 more)

### Community 20 - "TextBlock"
Cohesion: 0.05
Nodes (41): Detail, PercentText, BannerBody, BannerTitle, BrightnessValue, CpuClock, CpuFan, CpuTemp (+33 more)

### Community 21 - "Nextcalibur.Core.Configuration"
Cohesion: 0.08
Nodes (13): Nextcalibur.Core.Configuration, microsoft_win32, Theme, Dark, Light, system_io, system_security, system_security_accesscontrol (+5 more)

### Community 22 - "CoolingPolicyTests"
Cohesion: 0.08
Nodes (26): At, Margin, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc (+18 more)

### Community 23 - ".Warn"
Cohesion: 0.08
Nodes (8): IReadOnlyList, GameEnd, Guid, Ours, Plan, Func, MessageBoxButton, MessageBoxResult

### Community 24 - "GameLibrary"
Cohesion: 0.11
Nodes (14): Dictionary, GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, IReadOnlyList, Path, Regex (+6 more)

### Community 25 - "NetworkCostTests"
Cohesion: 0.11
Nodes (26): Answer, Bytes, Count, HttpMessageHandler, CancellationToken, ConcurrentDictionary, HttpClient, Task (+18 more)

### Community 26 - ".Main"
Cohesion: 0.10
Nodes (9): The threat model, and what the code does about it, Application, DllImport, Mutex, App, OverlayRepairPending, RegistryKey, Footprint (+1 more)

### Community 27 - "Fact"
Cohesion: 0.12
Nodes (10): Func, IEnumerable, IReadOnlyList, Version, RetirementEntry, Trace, NduFix, Fact (+2 more)

### Community 28 - ".For"
Cohesion: 0.11
Nodes (15): AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, Baseline (+7 more)

### Community 29 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 30 - "Elevation"
Cohesion: 0.11
Nodes (11): dynamic, DllImport, Elevation, Fact, InlineData, Theory, ElevationPolicyTests, Profile (+3 more)

### Community 31 - "UserPresence"
Cohesion: 0.13
Nodes (14): LastInputInfo, MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr (+6 more)

### Community 32 - "WindowsFaultsTests"
Cohesion: 0.17
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 33 - ".RequestRestart"
Cohesion: 0.10
Nodes (10): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr, MarshalAs (+2 more)

### Community 34 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 35 - "CrashGuard"
Cohesion: 0.11
Nodes (19): Session, DateTimeOffset, Exception, List, TimeSpan, CrashGuard, SafeMode, UncleanPrevious (+11 more)

### Community 36 - "SystemMode"
Cohesion: 0.14
Nodes (15): BatteryModePolicy, QuietMode, Remembered, IReadOnlyList, SystemMode, Balanced, Gaming, Office (+7 more)

### Community 37 - "Api"
Cohesion: 0.10
Nodes (23): Api, GetSettingFn, InitializeFn, ProfileOutFn, SessionFn, SessionOutFn, Set, SetSettingFn (+15 more)

### Community 38 - "EcMailbox"
Cohesion: 0.14
Nodes (13): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+5 more)

### Community 39 - ".Choose"
Cohesion: 0.12
Nodes (19): IEnumerable, IReadOnlyList, IdleLoadPicker, RunningItem, RunningKind, App, Background, Service (+11 more)

### Community 40 - "ProcessPresence"
Cohesion: 0.13
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 41 - "Program"
Cohesion: 0.12
Nodes (8): ConsoleColor, B, G, R, Program, DateTimeOffset, ThermalReader, ThermalSample

### Community 42 - ".OnSystemModeChanged"
Cohesion: 0.12
Nodes (8): Option, PowerModeChangedEventArgs, Button, Guid, IEnumerable, Ours, Plan, TextBlock

### Community 43 - "LedState"
Cohesion: 0.14
Nodes (17): B, G, LedBrightness, R, LedState, ActiveProfile, BrightnessPercent, Current (+9 more)

### Community 44 - ".Get"
Cohesion: 0.13
Nodes (14): ArgumentNullException, GpuLoad, FirmwareModeReading, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma (+6 more)

### Community 45 - "UpdateService"
Cohesion: 0.09
Nodes (14): CancelEventArgs, EventArgs, DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService (+6 more)

### Community 46 - "CpuLimits"
Cohesion: 0.10
Nodes (15): ArgumentOutOfRangeException, Bit, Now, Offset, PowerLimits, SinceBoot, IReadOnlyList, IReadOnlySet (+7 more)

### Community 47 - "Fact"
Cohesion: 0.16
Nodes (8): IList, HardwareSupport, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 48 - ".SignatureIsValid"
Cohesion: 0.14
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 49 - "Nextcalibur.Core.Security"
Cohesion: 0.14
Nodes (7): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_security_cryptography_x509certificates

### Community 50 - "Integrity"
Cohesion: 0.12
Nodes (11): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvapiPath, NvmlPath (+3 more)

### Community 51 - "DevicePowerSaving"
Cohesion: 0.17
Nodes (12): TextBlock, ToggleButton, AcDc, Guid, IEnumerable, DevicePowerSaving, Off, DeviceSaving (+4 more)

### Community 52 - "Dependency"
Cohesion: 0.13
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 53 - "Authenticode"
Cohesion: 0.25
Nodes (14): CatalogInfo, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle, Authenticode (+6 more)

### Community 54 - ".Check"
Cohesion: 0.14
Nodes (9): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported, IdempotenceTests (+1 more)

### Community 55 - ".PlaceTourStep"
Cohesion: 0.11
Nodes (10): SizeChangedEventArgs, FrameworkElement, KeyEventArgs, RoutedEventArgs, TourStep, Body, SectionName, Title (+2 more)

### Community 56 - "SystemModeService"
Cohesion: 0.21
Nodes (6): Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, SystemModeService

### Community 57 - ".Processor"
Cohesion: 0.15
Nodes (13): GpuPowerLimits, IEnumerable, PowerDefinition, PowerDefinitions, SetBy, Intel, LaptopMaker, Nextcalibur (+5 more)

### Community 58 - "FirmwareInventory"
Cohesion: 0.09
Nodes (22): IReadOnlyList, FirmwareInventory, BatteryChemistry, BatteryDesignMwh, BatteryName, BiosDate, BiosRelease, BiosVendor (+14 more)

### Community 59 - "IdleSetting"
Cohesion: 0.14
Nodes (13): Min, AcDc, Guid, IReadOnlyList, IdleSetting, DisplayOff, Sleep, IdleTimeouts (+5 more)

### Community 60 - "ColourWheel"
Cohesion: 0.12
Nodes (13): BitmapSource, Control, Ellipse, MouseEventArgs, Canvas, DependencyProperty, Image, MouseButtonEventArgs (+5 more)

### Community 61 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 62 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 63 - "MachineScanRecord"
Cohesion: 0.09
Nodes (22): DateTime, List, MachineScanRecord, BiosDate, BiosRelease, BiosVendor, BiosVersion, BoardProduct (+14 more)

### Community 64 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 65 - ".CheckProgram"
Cohesion: 0.20
Nodes (5): Fact, IntegrityTests, Current, Exe, Manifest

### Community 66 - "Fact"
Cohesion: 0.13
Nodes (11): IReadOnlyList, RepairOutcome, Empty, Fact, Task, CleanMachineTests, Guarded, Unguarded (+3 more)

### Community 67 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 68 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 69 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Ok, Task (+7 more)

### Community 70 - "CpuPowerReader"
Cohesion: 0.24
Nodes (5): DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 71 - ".Dictionary"
Cohesion: 0.15
Nodes (9): IReadOnlyDictionary, Stable, Dictionary, Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests (+1 more)

### Community 72 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 73 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 74 - "ModeProfileTests"
Cohesion: 0.22
Nodes (7): MemberData, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 75 - "DarkMenuColours"
Cohesion: 0.11
Nodes (19): ProfessionalColorTable, Color, DarkMenuColours, CheckBackground, CheckPressedBackground, CheckSelectedBackground, ImageMarginGradientBegin, ImageMarginGradientEnd (+11 more)

### Community 76 - "RuntimeDependencyTests"
Cohesion: 0.22
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 77 - "TemperatureFusion"
Cohesion: 0.25
Nodes (9): DateTime, TimeSpan, TemperatureFusion, Offset, RegisterSetAside, Value, DateTime, Fact (+1 more)

### Community 78 - "SmiCommand"
Cohesion: 0.21
Nodes (6): ArgumentException, SmiCommand, Fact, InlineData, Theory, SmiCommandTests

### Community 79 - "CoreLoad"
Cohesion: 0.15
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 80 - "PowerOverlayService"
Cohesion: 0.25
Nodes (6): DllImport, Guid, PowerModeOption, PowerOverlays, All, PowerOverlayService

### Community 81 - "TrayPresence"
Cohesion: 0.14
Nodes (9): Icon, Item, NotifyIcon, List, Ms, Window, TrayPresence, Dark (+1 more)

### Community 83 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 84 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 85 - "DevicePowerState"
Cohesion: 0.28
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 86 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 87 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 88 - ".RefreshIdleRows"
Cohesion: 0.22
Nodes (3): ComboBox, Grid, Guid

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

### Community 94 - "LedEffect"
Cohesion: 0.14
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 95 - "Log"
Cohesion: 0.26
Nodes (3): Exception, Log, Folder

### Community 96 - ".RunAndRecord"
Cohesion: 0.22
Nodes (6): JsonSerializerOptions, MachineScan, Firmware, RecordPath, Fact, MachineScanTests

### Community 97 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

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

### Community 104 - "Nextcalibur 0.5.4"
Cohesion: 0.17
Nodes (11): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+3 more)

### Community 105 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 106 - "StackPanel"
Cohesion: 0.17
Nodes (12): BannerActions, CeilingRows, DefinitionRows, DialogActions, DriveTextStack, FnShortcutRows, IdleLoadRows, IdleRows (+4 more)

### Community 107 - "InstallFolderGuard"
Cohesion: 0.35
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 108 - "PackageTemperatureTests"
Cohesion: 0.41
Nodes (4): Fact, InlineData, Theory, PackageTemperatureTests

### Community 109 - "AcDc"
Cohesion: 0.29
Nodes (5): AcDc, InlineData, Theory, Fact, IdleTimeoutsTests

### Community 110 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 111 - "ThemeService"
Cohesion: 0.18
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 113 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 114 - ".Sanitised"
Cohesion: 0.33
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 115 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 116 - ".Parse"
Cohesion: 0.29
Nodes (4): List, MemoryModule, Fact, FirmwareInventoryTests

### Community 117 - "SupportVerdict"
Cohesion: 0.22
Nodes (9): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites (+1 more)

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

### Community 127 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 128 - "Nextcalibur 0.5.5"
Cohesion: 0.25
Nodes (6): Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture

### Community 129 - "NoticeGate"
Cohesion: 0.29
Nodes (7): Key, Show, Action, List, NoticeGate, ShouldWait, WaitingCount

### Community 130 - "Toasts"
Cohesion: 0.32
Nodes (5): Action, Toasts, Balloon, windows_data_xml_dom, windows_ui_notifications

### Community 131 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 132 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 133 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 135 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 136 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 137 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 138 - ".DecodePowerInfo"
Cohesion: 0.33
Nodes (4): PowerInfo, Ratio, PowerInfo, Watts

### Community 139 - "ColGauge"
Cohesion: 0.19
Nodes (7): ColGauge, DialogCheck, DriveList, FnSelectAll, CheckBox, ColumnDefinition, ItemsControl

### Community 140 - ".ForegroundProcess"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Name, Pid

### Community 141 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 142 - ".Current"
Cohesion: 0.48
Nodes (3): DllImport, IntPtr, ScreenSaver

### Community 143 - ".Values_are_the_ones_measured_from_the_vendor"
Cohesion: 0.33
Nodes (4): Fact, InlineData, Theory, ThermalProfileTests

### Community 144 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 145 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 146 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 147 - "Nextcalibur 0.7.0"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.7.0, Tested on, Updated

### Community 148 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 149 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 150 - "ComboBox"
Cohesion: 0.33
Nodes (6): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, SettingNvPower, ComboBox

### Community 151 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 152 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 153 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 154 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 155 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 157 - "SmiSubsystem"
Cohesion: 0.40
Nodes (5): SmiSubsystem, DisplayMode, Led, Profile, Thermal

### Community 160 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 162 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 163 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 164 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 166 - "2. Command structure"
Cohesion: 0.50
Nodes (4): 2. Command structure, Call sequence, Command families (`a0`), Subsystems (`a1`)

### Community 167 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 168 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 169 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 170 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 173 - "OverlayDiagnosis"
Cohesion: 0.50
Nodes (4): OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck

### Community 174 - ".Modules_are_the_released_ones"
Cohesion: 0.50
Nodes (3): InlineData, Theory, PawnIoModuleTests

### Community 175 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 178 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 179 - "EffectPanel"
Cohesion: 0.67
Nodes (3): EffectPanel, ProfileRow, UniformGrid

### Community 181 - "PART_ContentHost"
Cohesion: 0.67
Nodes (3): PART_ContentHost, SettingsScroll, ScrollViewer

## Knowledge Gaps
- **598 isolated node(s):** `OverlayRepairPending`, `Accepted`, `SelectedColour`, `VisualChildrenCount`, `Diameter` (+593 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1151 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **34 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `Nextcalibur 0.5.5`, `AppSettings`, `CeilingTestWindow`, `FnShortcut`, `GpuClockReader`, `Window`, `ProgramRule`, `.OnLoaded`, `RunawayWatchTests`, `.Get`, `.Info`, `.ForegroundProcess`, `TourPage`, `Nextcalibur.App`, `RadioButton`, `system_runtime_interopservices`, `PlanTuning`, `ResourceDictionary`, `.OnBrightnessChanged`, `CoolingPolicyTests`, `.Warn`, `LedZone`, `Nextcalibur.Core.Configuration`, `.For`, `SystemInfo`, `UserPresence`, `.RequestRestart`, `SystemMode`, `EcMailbox`, `.Choose`, `Program`, `.OnSystemModeChanged`, `.Get`, `UpdateService`, `Nextcalibur.Core.Security`, `Integrity`, `DevicePowerSaving`, `.OnMinimiseToTrayClick`, `.PlaceTourStep`, `SystemModeService`, `IdleSetting`, `DependencyStatus`, `CpuPowerReader`, `BacklightKeyWatcher`, `TemperatureFusion`, `CoreLoad`, `PowerOverlayService`, `TrayPresence`, `.ApplyFnShortcuts`, `LedController`, `.RefreshIdleRows`, `LedEffect`, `Grid`, `Strings`, `ThemeService`, `NvidiaDriverState`, `SupportVerdict`?**
  _High betweenness centrality (0.508) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Text`, `ColGauge`, `RadioButton`, `TextBlock`, `.OnBrightnessChanged`, `ComboBox`, `Keycaps`, `DriveGauge`, `CpuWarnValue`, `EffectPanel`, `.OnMinimiseToTrayClick`, `PART_ContentHost`, `DialogProgress`, `DriveDot`, `TourCanvas`, `Wheel`, `Grid`, `StackPanel`, `Border`?**
  _High betweenness centrality (0.087) - this node is a cross-community bridge._
- **Why does `DependencyStatus` connect `DependencyStatus` to `MainWindow`, `.OnLoaded`, `Dependency`, `Nextcalibur.Core.Security`?**
  _High betweenness centrality (0.061) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `OverlayRepairPending`, `Accepted`, `SelectedColour` to the rest of the system?**
  _598 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.02318840579710145 - nodes in this community are weakly interconnected._
- **Should `AppSettings` be split into smaller, more focused modules?**
  _Cohesion score 0.03053221288515406 - nodes in this community are weakly interconnected._