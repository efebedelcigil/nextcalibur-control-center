# Graph Report - nextcalibur-control-center  (2026-10-15)

## Corpus Check
- 218 files · ~265,184 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3727 nodes · 8295 edges · 203 communities (165 shown, 38 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 503 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `c0c6aa89`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- AppSettings
- LedState
- FnShortcut
- ProtectedStore
- Log
- .Get
- ProgramRule
- .OnLoaded
- xunit
- RunawayWatchTests
- Nextcalibur.Core.Configuration
- .Warn
- CeilingTestWindow
- Nextcalibur.Core.Hardware
- WindowsFaults
- RadioButton
- Window
- PlanTuning
- IShellLinkW
- ProcessPresence
- ResourceDictionary
- WindowsFaultsTests
- CoolingPolicyTests
- GameLibrary
- NetworkCostTests
- UserPresence
- .RequestRestart
- .Main
- .Retirements
- Button
- SystemInfo
- Fact
- CpuLimits
- SystemTools
- .Choose
- Api
- .RefreshIdleRows
- .LoadDefinitionsAsync
- GpuClockReader
- Dependency
- EcMailbox
- Fact
- SystemModeService
- .SignatureIsValid
- Fact
- .Check
- Program
- system_text_json
- TestPreflight
- Integrity
- DevicePowerSaving
- .For
- CpuPowerReader
- Authenticode
- PowerOverlayService
- FirmwareInventory
- ColourWheel
- Timeouts
- .Read
- DotNetRuntimeDependency
- MachineScanRecord
- Nextcalibur.Core.csproj
- ToggleButton
- .CheckProgram
- RestorePoint
- BacklightKeyWatcher
- .Ask
- MemoryTrimmer
- DependencyStatus
- .WireSettingsPage
- .Get
- analyse-autopsy.py
- ModeProfileTests
- DarkMenuColours
- TemperatureFusion
- Hardware Protocol
- UpdateService
- RuntimeDependencyTests
- SmiCommand
- SystemMode
- .ApplyFnShortcuts
- TrayPresence
- .OnSystemModeChanged
- .IsUnderProgramFiles
- FanGauge
- DevicePowerState
- Unelevated
- DonutGauge
- AcDc
- BatteryModePolicy
- SegmentedBar
- .Set
- .Run
- Nextcalibur Control Center
- GpuMode
- .RunAndRecord
- Grid
- .HasAccess
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- CpuClockReader
- Strings
- StackPanel
- InstallFolderGuard
- PackageTemperatureTests
- Nextcalibur 0.5.3
- Integrity.cs
- Border
- ThemeService
- .CheckAwakeFault
- NvidiaDriverState
- .Parse
- SupportVerdict
- WordsInCodeTests
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
- .WireTypedThreshold
- ThermalSample
- .IsGpuAwakeFaultCondition
- NduFix
- IntegrityKind
- Test-Ui.ps1
- Text
- .BuildMenu
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- .ForegroundProcess
- TourPage
- CoreLoad
- Log-Graphics.ps1
- Privacy
- Nextcalibur 0.6.2
- Nextcalibur 0.7.0
- Security
- .OnBrightnessChanged
- ComboBox
- PowerSource
- SystemModesTests
- Strings.cs
- Notice
- Nextcalibur 0.5.11
- AppIdentity.cs
- .Read
- .FirmwareChanged
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- Nextcalibur 0.6.1
- .Quietly
- .ArrangeOverride
- ThemeService.cs
- .CalculateThreadShare
- Tools
- 0.5.7.md
- CpuWarnValue
- DialogCheck
- EffectPanel
- PART_ContentHost
- Toasts.cs
- .Paint
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
1. `MainWindow` - 444 edges
2. `Window` - 198 edges
3. `AppSettings` - 95 edges
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

## Communities (203 total, 38 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.02
Nodes (93): Added, DeferredOverheat, Found, GameSession, HwndSource, ObservableCollection, PluggedIn, Queue (+85 more)

### Community 1 - "AppSettings"
Cohesion: 0.03
Nodes (70): DateTime, Dictionary, FnShortcut, JsonElement, Limits, List, ProgramRule, SystemMode (+62 more)

### Community 2 - "LedState"
Cohesion: 0.05
Nodes (44): B, Dictionary, G, LedBrightness, R, LedProfile, BrightnessPercent, Colours (+36 more)

### Community 3 - "FnShortcut"
Cohesion: 0.05
Nodes (42): INotifyPropertyChanged, Problem, DriveRow, Detail, Name, Percent, PercentText, Dictionary (+34 more)

### Community 4 - "ProtectedStore"
Cohesion: 0.07
Nodes (26): Content, DirectorySecurity, Hash, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Dictionary (+18 more)

### Community 5 - "Log"
Cohesion: 0.06
Nodes (28): Session, DateTimeOffset, Exception, List, TimeSpan, CrashGuard, SafeMode, UncleanPrevious (+20 more)

### Community 6 - ".Get"
Cohesion: 0.07
Nodes (13): CancelEventArgs, GameEnd, Task, Task, Guid, Ours, Plan, FrameworkElement (+5 more)

### Community 7 - "ProgramRule"
Cohesion: 0.06
Nodes (33): HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, GeneratedRegex, IEnumerable (+25 more)

### Community 8 - ".OnLoaded"
Cohesion: 0.07
Nodes (13): AssemblyInformationalVersionAttribute, Asynchronous dispatcher & UI thread hardening, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary (+5 more)

### Community 9 - "xunit"
Cohesion: 0.08
Nodes (8): Nextcalibur.Core.Tests, Nextcalibur.Core.Security, Nextcalibur.Core.Power, nextcalibur_core_power_plantuning_acdc, system_security_cryptography_x509certificates, system_text_regularexpressions, system_xml_linq, xunit

### Community 10 - "RunawayWatchTests"
Cohesion: 0.07
Nodes (34): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+26 more)

### Community 11 - "Nextcalibur.Core.Configuration"
Cohesion: 0.09
Nodes (25): Nextcalibur.App, Nextcalibur.App.Controls, Nextcalibur.Core.Configuration, system_componentmodel, system_drawing, system_linq, system_runtime_compilerservices, system_windows (+17 more)

### Community 12 - ".Warn"
Cohesion: 0.06
Nodes (11): SolidColorBrush, IReadOnlyList, GameEnd, Task, Func, MessageBoxButton, MessageBoxResult, Task (+3 more)

### Community 13 - "CeilingTestWindow"
Cohesion: 0.08
Nodes (22): CancellationTokenSource, Button, CancellationToken, CheckBox, Func, List, StackPanel, Task (+14 more)

### Community 14 - "Nextcalibur.Core.Hardware"
Cohesion: 0.08
Nodes (12): Nextcalibur.Cli, Nextcalibur.Core.Input, Nextcalibur.Core.Hardware, microsoft_win32, microsoft_win32_safehandles, system_diagnostics, system_io, system_management (+4 more)

### Community 15 - "WindowsFaults"
Cohesion: 0.07
Nodes (35): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+27 more)

### Community 16 - "RadioButton"
Cohesion: 0.06
Nodes (42): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeBalanced, ModeDiscrete (+34 more)

### Community 17 - "Window"
Cohesion: 0.07
Nodes (47): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+39 more)

### Community 18 - "PlanTuning"
Cohesion: 0.12
Nodes (18): Floor, Marker, Original, Action, AcDc, DllImport, Guid, IEnumerable (+10 more)

### Community 19 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 20 - "ProcessPresence"
Cohesion: 0.08
Nodes (18): DateTime, IReadOnlyList, MachineHistory, DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence (+10 more)

### Community 21 - "ResourceDictionary"
Cohesion: 0.07
Nodes (40): ActualWidth, IsDropDownOpen, Size, Arrow, Bar, Bg, Blade, Box (+32 more)

### Community 22 - "WindowsFaultsTests"
Cohesion: 0.13
Nodes (4): FaultEvaluation, Fact, WindowsFaultsTests, Dictionary

### Community 23 - "CoolingPolicyTests"
Cohesion: 0.08
Nodes (26): At, Margin, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc (+18 more)

### Community 24 - "GameLibrary"
Cohesion: 0.11
Nodes (14): Dictionary, GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, IReadOnlyList, Path, Regex (+6 more)

### Community 25 - "NetworkCostTests"
Cohesion: 0.11
Nodes (26): Answer, Bytes, Count, HttpMessageHandler, CancellationToken, ConcurrentDictionary, HttpClient, Task (+18 more)

### Community 26 - "UserPresence"
Cohesion: 0.12
Nodes (14): LastInputInfo, MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr (+6 more)

### Community 27 - ".RequestRestart"
Cohesion: 0.09
Nodes (11): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr, MarshalAs (+3 more)

### Community 28 - ".Main"
Cohesion: 0.10
Nodes (9): The threat model, and what the code does about it, Application, DllImport, Mutex, App, OverlayRepairPending, RegistryKey, Footprint (+1 more)

### Community 29 - ".Retirements"
Cohesion: 0.11
Nodes (9): dynamic, Encoding, StartupRegistration, IsEnabled, DllImport, Elevation, CardSwitchTasks, Fact (+1 more)

### Community 30 - "Button"
Cohesion: 0.06
Nodes (29): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, GameScanButton (+21 more)

### Community 31 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 32 - "Fact"
Cohesion: 0.12
Nodes (9): Func, IEnumerable, IReadOnlyList, Version, RetirementEntry, Trace, Fact, FootprintTests (+1 more)

### Community 33 - "CpuLimits"
Cohesion: 0.09
Nodes (18): Bit, Now, Offset, PowerInfo, PowerLimits, Ratio, SinceBoot, IReadOnlyList (+10 more)

### Community 34 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 35 - ".Choose"
Cohesion: 0.12
Nodes (19): IEnumerable, IReadOnlyList, IdleLoadPicker, RunningItem, RunningKind, App, Background, Service (+11 more)

### Community 36 - "Api"
Cohesion: 0.11
Nodes (23): Api, GetSettingFn, InitializeFn, ProfileOutFn, SessionFn, SessionOutFn, Set, SetSettingFn (+15 more)

### Community 37 - ".RefreshIdleRows"
Cohesion: 0.12
Nodes (10): Kept, ComboBox, Grid, Guid, Guid, IReadOnlyList, Plan, DllImport (+2 more)

### Community 38 - ".LoadDefinitionsAsync"
Cohesion: 0.12
Nodes (15): Grid, Task, GpuPowerLimits, IEnumerable, PowerDefinition, PowerDefinitions, SetBy, Intel (+7 more)

### Community 39 - "GpuClockReader"
Cohesion: 0.17
Nodes (7): NvmlProcessInfo, NvmlUtilisation, DllImport, IntPtr, GpuClockReader, NvmlProcessInfo, NvmlUtilisation

### Community 40 - "Dependency"
Cohesion: 0.11
Nodes (18): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+10 more)

### Community 41 - "EcMailbox"
Cohesion: 0.13
Nodes (13): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+5 more)

### Community 42 - "Fact"
Cohesion: 0.16
Nodes (8): IList, HardwareSupport, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 43 - "SystemModeService"
Cohesion: 0.19
Nodes (8): Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, Ours, Plan, SystemModeService

### Community 44 - ".SignatureIsValid"
Cohesion: 0.15
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 45 - "Fact"
Cohesion: 0.11
Nodes (14): OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact, Task (+6 more)

### Community 46 - ".Check"
Cohesion: 0.13
Nodes (10): CommonAce, RawSecurityDescriptor, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported (+2 more)

### Community 47 - "Program"
Cohesion: 0.14
Nodes (5): ConsoleColor, B, G, R, Program

### Community 48 - "system_text_json"
Cohesion: 0.14
Nodes (7): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_text_json, system_text_json_serialization

### Community 49 - "TestPreflight"
Cohesion: 0.14
Nodes (12): Display, IoCounters, ProcessLook, ConcurrentDictionary, Dictionary, DllImport, IntPtr, IoCounters (+4 more)

### Community 50 - "Integrity"
Cohesion: 0.12
Nodes (11): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvapiPath, NvmlPath (+3 more)

### Community 51 - "DevicePowerSaving"
Cohesion: 0.17
Nodes (12): TextBlock, ToggleButton, AcDc, Guid, IEnumerable, DevicePowerSaving, Off, DeviceSaving (+4 more)

### Community 52 - ".For"
Cohesion: 0.16
Nodes (13): AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, Baseline (+5 more)

### Community 53 - "CpuPowerReader"
Cohesion: 0.20
Nodes (6): ArgumentOutOfRangeException, DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 54 - "Authenticode"
Cohesion: 0.25
Nodes (14): CatalogInfo, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle, Authenticode (+6 more)

### Community 55 - "PowerOverlayService"
Cohesion: 0.16
Nodes (10): Option, Button, IEnumerable, DllImport, Guid, IReadOnlyList, PowerModeOption, PowerOverlays (+2 more)

### Community 56 - "FirmwareInventory"
Cohesion: 0.09
Nodes (22): IReadOnlyList, FirmwareInventory, BatteryChemistry, BatteryDesignMwh, BatteryName, BiosDate, BiosRelease, BiosVendor (+14 more)

### Community 57 - "ColourWheel"
Cohesion: 0.12
Nodes (13): BitmapSource, Control, Ellipse, MouseEventArgs, Canvas, DependencyProperty, Image, MouseButtonEventArgs (+5 more)

### Community 58 - "Timeouts"
Cohesion: 0.15
Nodes (12): Min, AcDc, Guid, IdleSetting, DisplayOff, Sleep, IdleTimeouts, Timeouts (+4 more)

### Community 59 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 60 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 61 - "MachineScanRecord"
Cohesion: 0.09
Nodes (22): DateTime, List, MachineScanRecord, BiosDate, BiosRelease, BiosVendor, BiosVersion, BoardProduct (+14 more)

### Community 62 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 63 - "ToggleButton"
Cohesion: 0.10
Nodes (21): OverheatWarningToggle, SettingAskGameMode, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost, SettingFixWidgets (+13 more)

### Community 64 - ".CheckProgram"
Cohesion: 0.20
Nodes (5): Fact, IntegrityTests, Current, Exe, Manifest

### Community 65 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 66 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 67 - ".Ask"
Cohesion: 0.25
Nodes (10): HttpStatusCode, CancellationToken, Fact, HttpRequestMessage, HttpResponseMessage, InlineData, Task, Theory (+2 more)

### Community 68 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 69 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Ok, Task (+7 more)

### Community 71 - ".Get"
Cohesion: 0.21
Nodes (7): ArgumentNullException, GpuModeService, SwitchOutcome, Func, Words, Resolver, SwitchOutcome

### Community 72 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 73 - "ModeProfileTests"
Cohesion: 0.22
Nodes (7): MemberData, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 74 - "DarkMenuColours"
Cohesion: 0.11
Nodes (19): ProfessionalColorTable, Color, DarkMenuColours, CheckBackground, CheckPressedBackground, CheckSelectedBackground, ImageMarginGradientBegin, ImageMarginGradientEnd (+11 more)

### Community 75 - "TemperatureFusion"
Cohesion: 0.25
Nodes (9): DateTime, TimeSpan, TemperatureFusion, Offset, RegisterSetAside, Value, DateTime, Fact (+1 more)

### Community 76 - "Hardware Protocol"
Cohesion: 0.11
Nodes (18): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 4. LED — `a1 = 0x0100`, 6. Safety rules, 7. Not supported, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`) (+10 more)

### Community 77 - "UpdateService"
Cohesion: 0.14
Nodes (12): DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService, Available, AvailableVersion (+4 more)

### Community 78 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 79 - "SmiCommand"
Cohesion: 0.21
Nodes (6): ArgumentException, SmiCommand, Fact, InlineData, Theory, SmiCommandTests

### Community 80 - "SystemMode"
Cohesion: 0.15
Nodes (10): InvalidOperationException, IReadOnlyList, SystemMode, Balanced, Gaming, Office, Performance, SystemModes (+2 more)

### Community 82 - "TrayPresence"
Cohesion: 0.14
Nodes (9): Icon, Item, NotifyIcon, List, Ms, Window, TrayPresence, Dark (+1 more)

### Community 83 - ".OnSystemModeChanged"
Cohesion: 0.24
Nodes (4): PowerModeChangedEventArgs, Guid, Ours, Plan

### Community 84 - ".IsUnderProgramFiles"
Cohesion: 0.21
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 85 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 86 - "DevicePowerState"
Cohesion: 0.28
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 87 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 88 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 89 - "AcDc"
Cohesion: 0.21
Nodes (6): IReadOnlyList, AcDc, InlineData, Theory, Fact, IdleTimeoutsTests

### Community 90 - "BatteryModePolicy"
Cohesion: 0.35
Nodes (5): BatteryModePolicy, QuietMode, Remembered, Fact, BatteryModePolicyTests

### Community 91 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 92 - ".Set"
Cohesion: 0.30
Nodes (7): PowerThrottlingState, DllImport, IntPtr, EfficiencyMode, PowerThrottlingState, Fact, EfficiencyModeTests

### Community 93 - ".Run"
Cohesion: 0.19
Nodes (8): ProcessorPerformance, Detail, Func, Ok, SelfTest, DefaultDllImportSearchPaths, DllImport, IntPtr

### Community 94 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 95 - "GpuMode"
Cohesion: 0.18
Nodes (7): GpuLoad, FirmwareModeReading, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma

### Community 96 - ".RunAndRecord"
Cohesion: 0.22
Nodes (6): JsonSerializerOptions, MachineScan, Firmware, RecordPath, Fact, MachineScanTests

### Community 97 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 98 - ".HasAccess"
Cohesion: 0.28
Nodes (5): SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

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

### Community 104 - "CpuClockReader"
Cohesion: 0.27
Nodes (4): PdhFmtCounterValue, CpuClockReader, BaseMhz, PdhFmtCounterValue

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

### Community 109 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 110 - "Integrity.cs"
Cohesion: 0.20
Nodes (6): system_reflection, system_security_accesscontrol, system_security_cryptography, InlineData, Theory, PawnIoModuleTests

### Community 111 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 112 - "ThemeService"
Cohesion: 0.20
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 113 - ".CheckAwakeFault"
Cohesion: 0.31
Nodes (5): DateTime, Func, IReadOnlyList, TimeSpan, GpuAwakeFault

### Community 114 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 115 - ".Parse"
Cohesion: 0.29
Nodes (4): List, MemoryModule, Fact, FirmwareInventoryTests

### Community 116 - "SupportVerdict"
Cohesion: 0.22
Nodes (9): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites (+1 more)

### Community 117 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

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

### Community 131 - "ThermalSample"
Cohesion: 0.39
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 132 - ".IsGpuAwakeFaultCondition"
Cohesion: 0.25
Nodes (5): InlineData, Theory, GpuAwakeFaultConditionTests, InlineData, Theory

### Community 134 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 135 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 136 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 138 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 139 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 140 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 141 - ".ForegroundProcess"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Name, Pid

### Community 142 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 143 - "CoreLoad"
Cohesion: 0.29
Nodes (3): CoreLoad, Count, ProcessorPerformance

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

### Community 151 - "ComboBox"
Cohesion: 0.33
Nodes (6): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, SettingNvPower, ComboBox

### Community 152 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 155 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 156 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 157 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

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

### Community 165 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 166 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 167 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 168 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 171 - "ThemeService.cs"
Cohesion: 0.50
Nodes (3): Theme, Dark, Light

### Community 172 - ".CalculateThreadShare"
Cohesion: 0.50
Nodes (3): IReadOnlyDictionary, Stable, TopSharePercent

### Community 173 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 176 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 177 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

### Community 178 - "EffectPanel"
Cohesion: 0.67
Nodes (3): EffectPanel, ProfileRow, UniformGrid

### Community 179 - "PART_ContentHost"
Cohesion: 0.67
Nodes (3): PART_ContentHost, SettingsScroll, ScrollViewer

## Knowledge Gaps
- **601 isolated node(s):** `OverlayRepairPending`, `Accepted`, `SelectedColour`, `VisualChildrenCount`, `Diameter` (+596 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1157 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **38 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `Nextcalibur 0.5.5`, `AppSettings`, `.WireTypedThreshold`, `FnShortcut`, `LedState`, `Log`, `.Get`, `ThermalSample`, `.OnLoaded`, `ProgramRule`, `RunawayWatchTests`, `Nextcalibur.Core.Configuration`, `.Warn`, `CeilingTestWindow`, `.ForegroundProcess`, `TourPage`, `RadioButton`, `CoreLoad`, `PlanTuning`, `WindowsFaults`, `ProcessPresence`, `ResourceDictionary`, `.AutoEfficiencyTick`, `CoolingPolicyTests`, `.OnBrightnessChanged`, `Window`, `UserPresence`, `.RequestRestart`, `Button`, `SystemInfo`, `.Choose`, `.RefreshIdleRows`, `.LoadDefinitionsAsync`, `GpuClockReader`, `EcMailbox`, `SystemModeService`, `Integrity`, `DevicePowerSaving`, `.For`, `CpuPowerReader`, `PowerOverlayService`, `Timeouts`, `.Read`, `BacklightKeyWatcher`, `DependencyStatus`, `.WireSettingsPage`, `.Get`, `TemperatureFusion`, `UpdateService`, `SystemMode`, `.ApplyFnShortcuts`, `TrayPresence`, `.OnSystemModeChanged`, `AcDc`, `BatteryModePolicy`, `GpuMode`, `Grid`, `CpuClockReader`, `Strings`, `ThemeService`, `NvidiaDriverState`, `SupportVerdict`?**
  _High betweenness centrality (0.481) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Text`, `RadioButton`, `.OnBrightnessChanged`, `ComboBox`, `Button`, `Keycaps`, `DriveGauge`, `CpuWarnValue`, `DialogCheck`, `EffectPanel`, `PART_ContentHost`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `TourCanvas`, `Wheel`, `ToggleButton`, `Grid`, `StackPanel`, `Border`?**
  _High betweenness centrality (0.080) - this node is a cross-community bridge._
- **Why does `Nextcalibur.Core.Hardware` connect `Nextcalibur.Core.Hardware` to `CpuLimits`, `BacklightKeyWatcher`, `ThermalSample`, `.LoadDefinitionsAsync`, `EcMailbox`, `RunawayWatchTests`, `Nextcalibur.Core.Configuration`, `xunit`, `Integrity.cs`, `system_text_json`, `SupportVerdict`, `Strings.cs`?**
  _High betweenness centrality (0.064) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `OverlayRepairPending`, `Accepted`, `SelectedColour` to the rest of the system?**
  _601 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.02075134168157424 - nodes in this community are weakly interconnected._
- **Should `AppSettings` be split into smaller, more focused modules?**
  _Cohesion score 0.029548563611491108 - nodes in this community are weakly interconnected._