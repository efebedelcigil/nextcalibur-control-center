# Graph Report - nextcalibur-control-center  (2026-10-14)

## Corpus Check
- 217 files · ~263,669 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3712 nodes · 8245 edges · 212 communities (177 shown, 35 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 488 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `98216bcf`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- AppSettings
- SystemMode
- ProtectedStore
- ProgramRule
- .Get
- RunawayWatchTests
- Nextcalibur.Core.Configuration
- WindowsFaults
- IShellLinkW
- GpuClockReader
- .Warn
- Window
- xunit
- RadioButton
- Fact
- Nextcalibur.Core.Hardware
- PlanTuning
- WindowsFaultsTests
- ResourceDictionary
- Nextcalibur.Core.Security
- SystemInfo
- CoolingPolicyTests
- GameLibrary
- NetworkCostTests
- .Main
- Button
- .RequestRestart
- UserPresence
- .Info
- .ModeName
- CpuLimits
- SystemTools
- CrashGuard
- .Get
- Log
- .Choose
- Api
- UpdateService
- .For
- .LoadDefinitionsAsync
- .RemoveMachineTraces
- .SignatureIsValid
- EcMailbox
- Integrity
- .OnSystemModeChanged
- Fact
- Dependency
- Authenticode
- .Check
- Program
- TestPreflight
- .RefreshIdleRows
- FirmwareInventory
- IdleSetting
- CpuPowerReader
- ColourWheel
- .Ask
- DotNetRuntimeDependency
- MachineScanRecord
- Nextcalibur.Core.csproj
- PowerOverlayService
- ToggleButton
- Fact
- RestorePoint
- BacklightKeyWatcher
- MemoryTrimmer
- DependencyStatus
- DevicePowerSaving
- .CheckProgram
- CeilingTestWindow
- analyse-autopsy.py
- ModeProfileTests
- DarkMenuColours
- RuntimeDependencyTests
- ProcessPresence
- TemperatureFusion
- TrayPresence
- .WarnAboutNewConflicts
- .MayStartWithoutPrompt
- CeilingStep
- CoreLoad
- .OnLanguageChanged
- .ApplyFnShortcuts
- .GetColour
- LedController
- LedState
- FanGauge
- DevicePowerState
- Unelevated
- DonutGauge
- MachineHistory
- SegmentedBar
- microsoft_win32
- .Set
- Nextcalibur Control Center
- .RunAndRecord
- .HasAccess
- Grid
- InstallFolderGuard
- FnShortcuts
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Strings
- StackPanel
- VendorSoftware
- .Sanitised
- PackageTemperatureTests
- .Check
- .CheckShape
- FnShortcut
- AcDc
- SmiCommandTests
- Hardware Protocol
- Nextcalibur 0.5.3
- .RunStepsAsync
- HotkeyModifiers
- NoticeGate
- Border
- ThemeService
- NvidiaDriverState
- .Parse
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- LedEffect
- ThermalSample
- PawnIoDependency
- NduFix
- .IsSystemFileSignedBy
- Probe
- CeilingTests
- Nextcalibur 0.5.1
- .Run
- IntegrityKind
- Test-Ui.ps1
- Text
- .BuildMenu
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- .ForegroundProcess
- TourPage
- GpuAwakeFault
- Log-Graphics.ps1
- Privacy
- Nextcalibur 0.5.5
- Nextcalibur 0.6.2
- Nextcalibur 0.7.0
- Security
- .OnBrightnessChanged
- ComboBox
- LedZone
- MachineWideGate
- PowerSource
- PawnIoModuleTests.cs
- Notice
- Nextcalibur 0.5.11
- AppIdentity.cs
- TourStep
- .Set
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
- .ArrangeOverride
- .GpuAwakeFault_identifies_true_stuck_awake_conditions
- Tools
- 0.5.7.md
- CpuWarnValue
- DialogCheck
- EffectPanel
- PART_ContentHost
- Toasts.cs
- .Paint
- .Work_that_must_not_be_interrupted
- .LoadIcon
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
1. `MainWindow` - 435 edges
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

## Communities (212 total, 35 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.02
Nodes (91): Added, DeferredOverheat, Found, GameSession, HwndSource, ObservableCollection, PluggedIn, Queue (+83 more)

### Community 1 - "AppSettings"
Cohesion: 0.03
Nodes (67): DateTime, Dictionary, FnShortcut, JsonElement, Limits, List, ProgramRule, SystemMode (+59 more)

### Community 2 - "SystemMode"
Cohesion: 0.05
Nodes (36): InvalidOperationException, SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile (+28 more)

### Community 3 - "ProtectedStore"
Cohesion: 0.06
Nodes (26): Content, DirectorySecurity, Hash, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Dictionary (+18 more)

### Community 4 - "ProgramRule"
Cohesion: 0.06
Nodes (33): HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, GeneratedRegex, IEnumerable (+25 more)

### Community 5 - ".Get"
Cohesion: 0.08
Nodes (13): GameEnd, SolidColorBrush, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary (+5 more)

### Community 6 - "RunawayWatchTests"
Cohesion: 0.07
Nodes (34): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+26 more)

### Community 7 - "Nextcalibur.Core.Configuration"
Cohesion: 0.09
Nodes (25): Nextcalibur.App, Nextcalibur.App.Controls, Nextcalibur.Core, Nextcalibur.Core.Configuration, system_drawing, system_globalization, system_linq, system_windows (+17 more)

### Community 8 - "WindowsFaults"
Cohesion: 0.07
Nodes (35): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+27 more)

### Community 9 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 10 - "GpuClockReader"
Cohesion: 0.10
Nodes (14): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+6 more)

### Community 11 - ".Warn"
Cohesion: 0.07
Nodes (11): IReadOnlyList, GameEnd, Guid, Ours, Plan, Func, MessageBoxButton, MessageBoxResult (+3 more)

### Community 12 - "Window"
Cohesion: 0.07
Nodes (47): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+39 more)

### Community 13 - "xunit"
Cohesion: 0.09
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, nextcalibur_core_power_plantuning_acdc, system_text_regularexpressions, system_xml_linq, xunit

### Community 14 - "RadioButton"
Cohesion: 0.06
Nodes (42): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeBalanced, ModeDiscrete (+34 more)

### Community 15 - "Fact"
Cohesion: 0.09
Nodes (20): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+12 more)

### Community 16 - "Nextcalibur.Core.Hardware"
Cohesion: 0.09
Nodes (8): Nextcalibur.Cli, Nextcalibur.Core.Hardware, microsoft_win32_safehandles, system_componentmodel, system_diagnostics, system_management, system_runtime_compilerservices, system_runtime_interopservices

### Community 17 - "PlanTuning"
Cohesion: 0.13
Nodes (18): Floor, Marker, Original, Action, AcDc, DllImport, Guid, IEnumerable (+10 more)

### Community 18 - "WindowsFaultsTests"
Cohesion: 0.12
Nodes (7): IReadOnlyDictionary, FaultEvaluation, Stable, Fact, WindowsFaultsTests, Dictionary, TopSharePercent

### Community 19 - "ResourceDictionary"
Cohesion: 0.07
Nodes (40): ActualWidth, IsDropDownOpen, Size, Arrow, Bar, Bg, Blade, Box (+32 more)

### Community 20 - "Nextcalibur.Core.Security"
Cohesion: 0.09
Nodes (14): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Input, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_security_accesscontrol (+6 more)

### Community 21 - "SystemInfo"
Cohesion: 0.08
Nodes (21): MemoryStatusEx, DriveUse, INotifyPropertyChanged, DriveRow, Name, Percent, PercentText, DllImport (+13 more)

### Community 22 - "CoolingPolicyTests"
Cohesion: 0.08
Nodes (26): At, Margin, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc (+18 more)

### Community 23 - "GameLibrary"
Cohesion: 0.11
Nodes (14): Dictionary, GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, IReadOnlyList, Path, Regex (+6 more)

### Community 24 - "NetworkCostTests"
Cohesion: 0.11
Nodes (26): Answer, Bytes, Count, HttpMessageHandler, CancellationToken, ConcurrentDictionary, HttpClient, Task (+18 more)

### Community 25 - ".Main"
Cohesion: 0.09
Nodes (10): The threat model, and what the code does about it, Application, DllImport, Mutex, App, OverlayRepairPending, DllImport, RegistryKey (+2 more)

### Community 26 - "Button"
Cohesion: 0.06
Nodes (29): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, GameScanButton (+21 more)

### Community 27 - ".RequestRestart"
Cohesion: 0.10
Nodes (10): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, IntPtr, MarshalAs, TextBlock (+2 more)

### Community 28 - "UserPresence"
Cohesion: 0.13
Nodes (14): LastInputInfo, MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr (+6 more)

### Community 29 - ".Info"
Cohesion: 0.13
Nodes (4): Asynchronous dispatcher & UI thread hardening, Task, Task, IProgress

### Community 30 - ".ModeName"
Cohesion: 0.11
Nodes (8): AssemblyInformationalVersionAttribute, Task, FrameworkElement, Func, Grid, HashSet, List, ModeChoice

### Community 31 - "CpuLimits"
Cohesion: 0.09
Nodes (18): Bit, Now, Offset, PowerInfo, PowerLimits, Ratio, SinceBoot, IReadOnlyList (+10 more)

### Community 32 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 33 - "CrashGuard"
Cohesion: 0.11
Nodes (19): Session, DateTimeOffset, Exception, List, TimeSpan, CrashGuard, SafeMode, UncleanPrevious (+11 more)

### Community 34 - ".Get"
Cohesion: 0.13
Nodes (14): ArgumentNullException, GpuLoad, FirmwareModeReading, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma (+6 more)

### Community 35 - "Log"
Cohesion: 0.11
Nodes (9): Exception, Log, Folder, Action, Fact, InlineData, Regex, Theory (+1 more)

### Community 36 - ".Choose"
Cohesion: 0.12
Nodes (19): IEnumerable, IReadOnlyList, IdleLoadPicker, RunningItem, RunningKind, App, Background, Service (+11 more)

### Community 37 - "Api"
Cohesion: 0.11
Nodes (23): Api, GetSettingFn, InitializeFn, ProfileOutFn, SessionFn, SessionOutFn, Set, SetSettingFn (+15 more)

### Community 38 - "UpdateService"
Cohesion: 0.09
Nodes (14): CancelEventArgs, EventArgs, DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService (+6 more)

### Community 39 - ".For"
Cohesion: 0.13
Nodes (15): AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, Baseline (+7 more)

### Community 40 - ".LoadDefinitionsAsync"
Cohesion: 0.12
Nodes (15): Grid, Task, GpuPowerLimits, IEnumerable, PowerDefinition, PowerDefinitions, SetBy, Intel (+7 more)

### Community 41 - ".RemoveMachineTraces"
Cohesion: 0.14
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 42 - ".SignatureIsValid"
Cohesion: 0.14
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 43 - "EcMailbox"
Cohesion: 0.16
Nodes (10): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+2 more)

### Community 44 - "Integrity"
Cohesion: 0.12
Nodes (11): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvapiPath, NvmlPath (+3 more)

### Community 45 - ".OnSystemModeChanged"
Cohesion: 0.14
Nodes (7): Option, PowerModeChangedEventArgs, Button, Guid, IEnumerable, Ours, Plan

### Community 46 - "Fact"
Cohesion: 0.17
Nodes (7): IList, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 47 - "Dependency"
Cohesion: 0.13
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 48 - "Authenticode"
Cohesion: 0.25
Nodes (14): CatalogInfo, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle, Authenticode (+6 more)

### Community 49 - ".Check"
Cohesion: 0.14
Nodes (9): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported, IdempotenceTests (+1 more)

### Community 50 - "Program"
Cohesion: 0.15
Nodes (5): ConsoleColor, B, G, R, Program

### Community 51 - "TestPreflight"
Cohesion: 0.17
Nodes (11): Display, IoCounters, ProcessLook, ConcurrentDictionary, Dictionary, DllImport, IntPtr, TimeSpan (+3 more)

### Community 52 - ".RefreshIdleRows"
Cohesion: 0.15
Nodes (6): ComboBox, Grid, Guid, DllImport, IntPtr, ScreenSaver

### Community 53 - "FirmwareInventory"
Cohesion: 0.09
Nodes (22): IReadOnlyList, FirmwareInventory, BatteryChemistry, BatteryDesignMwh, BatteryName, BiosDate, BiosRelease, BiosVendor (+14 more)

### Community 54 - "IdleSetting"
Cohesion: 0.14
Nodes (13): Min, AcDc, Guid, IReadOnlyList, IdleSetting, DisplayOff, Sleep, IdleTimeouts (+5 more)

### Community 55 - "CpuPowerReader"
Cohesion: 0.21
Nodes (6): ArgumentOutOfRangeException, DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 56 - "ColourWheel"
Cohesion: 0.12
Nodes (13): BitmapSource, Control, Ellipse, MouseEventArgs, Canvas, DependencyProperty, Image, MouseButtonEventArgs (+5 more)

### Community 57 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 58 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 59 - "MachineScanRecord"
Cohesion: 0.09
Nodes (22): DateTime, List, MachineScanRecord, BiosDate, BiosRelease, BiosVendor, BiosVersion, BoardProduct (+14 more)

### Community 60 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 61 - "PowerOverlayService"
Cohesion: 0.19
Nodes (10): DllImport, Guid, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, PowerModeOption, PowerOverlays (+2 more)

### Community 62 - "ToggleButton"
Cohesion: 0.10
Nodes (21): OverheatWarningToggle, SettingAskGameMode, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost, SettingFixWidgets (+13 more)

### Community 63 - "Fact"
Cohesion: 0.13
Nodes (11): IReadOnlyList, RepairOutcome, Empty, Fact, Task, CleanMachineTests, Guarded, Unguarded (+3 more)

### Community 64 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 65 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 66 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 67 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Ok, Task (+7 more)

### Community 68 - "DevicePowerSaving"
Cohesion: 0.21
Nodes (10): AcDc, Guid, IEnumerable, DevicePowerSaving, Off, DeviceSaving, Usb, Wifi (+2 more)

### Community 69 - ".CheckProgram"
Cohesion: 0.21
Nodes (5): Fact, IntegrityTests, Current, Exe, Manifest

### Community 70 - "CeilingTestWindow"
Cohesion: 0.15
Nodes (10): CancellationTokenSource, Button, CheckBox, Func, StackPanel, TextBlock, CeilingTestWindow, Accepted (+2 more)

### Community 71 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 72 - "ModeProfileTests"
Cohesion: 0.22
Nodes (7): MemberData, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 73 - "DarkMenuColours"
Cohesion: 0.11
Nodes (19): ProfessionalColorTable, Color, DarkMenuColours, CheckBackground, CheckPressedBackground, CheckSelectedBackground, ImageMarginGradientBegin, ImageMarginGradientEnd (+11 more)

### Community 74 - "RuntimeDependencyTests"
Cohesion: 0.22
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 75 - "ProcessPresence"
Cohesion: 0.23
Nodes (8): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, Fact, ProcessPresenceTests, Self

### Community 76 - "TemperatureFusion"
Cohesion: 0.25
Nodes (9): DateTime, TimeSpan, TemperatureFusion, Offset, RegisterSetAside, Value, DateTime, Fact (+1 more)

### Community 77 - "TrayPresence"
Cohesion: 0.13
Nodes (10): Deep memory & resource leak eradication, EventHandler, Item, NotifyIcon, List, Ms, Window, TrayPresence (+2 more)

### Community 78 - ".WarnAboutNewConflicts"
Cohesion: 0.17
Nodes (12): Problem, Detail, Dictionary, KeyEventArgs, ShortcutProblem, Duplicate, NotAKey, ReservedBySystem (+4 more)

### Community 79 - ".MayStartWithoutPrompt"
Cohesion: 0.22
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 80 - "CeilingStep"
Cohesion: 0.21
Nodes (7): IEnumerable, IReadOnlyList, TimeSpan, CeilingStep, CeilingTest, Fact, CeilingTestTests

### Community 81 - "CoreLoad"
Cohesion: 0.15
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 82 - ".OnLanguageChanged"
Cohesion: 0.17
Nodes (3): Action, ToggleButton, EventArgs

### Community 84 - ".GetColour"
Cohesion: 0.24
Nodes (7): B, G, R, Fact, InlineData, Theory, LedStateTests

### Community 85 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 86 - "LedState"
Cohesion: 0.13
Nodes (15): Dictionary, LedBrightness, LedProfile, BrightnessPercent, Colours, Effect, LedState, ActiveProfile (+7 more)

### Community 87 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 88 - "DevicePowerState"
Cohesion: 0.28
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 89 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 90 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 91 - "MachineHistory"
Cohesion: 0.28
Nodes (6): DateTime, IReadOnlyList, MachineHistory, DateTime, Fact, MachineHistoryTests

### Community 92 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 93 - "microsoft_win32"
Cohesion: 0.14
Nodes (6): microsoft_win32, Theme, Dark, Light, system_io, system_security

### Community 94 - ".Set"
Cohesion: 0.30
Nodes (7): PowerThrottlingState, DllImport, IntPtr, EfficiencyMode, PowerThrottlingState, Fact, EfficiencyModeTests

### Community 95 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 96 - ".RunAndRecord"
Cohesion: 0.22
Nodes (6): JsonSerializerOptions, MachineScan, Firmware, RecordPath, Fact, MachineScanTests

### Community 97 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 98 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 99 - "InstallFolderGuard"
Cohesion: 0.33
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 100 - "FnShortcuts"
Cohesion: 0.36
Nodes (4): DllImport, IntPtr, StringBuilder, FnShortcuts

### Community 101 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 102 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 103 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 104 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 105 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 106 - "StackPanel"
Cohesion: 0.17
Nodes (12): BannerActions, CeilingRows, DefinitionRows, DialogActions, DriveTextStack, FnShortcutRows, IdleLoadRows, IdleRows (+4 more)

### Community 107 - "VendorSoftware"
Cohesion: 0.23
Nodes (4): DateTime, TimeSpan, VendorSoftware, UninstallCommand

### Community 108 - ".Sanitised"
Cohesion: 0.26
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 109 - "PackageTemperatureTests"
Cohesion: 0.41
Nodes (4): Fact, InlineData, Theory, PackageTemperatureTests

### Community 110 - ".Check"
Cohesion: 0.20
Nodes (10): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+2 more)

### Community 111 - ".CheckShape"
Cohesion: 0.36
Nodes (3): IEnumerable, Fact, FnShortcutsTests

### Community 112 - "FnShortcut"
Cohesion: 0.17
Nodes (10): List, FnAction, FanTurbo, KeyboardBacklight, SystemMode, FnShortcut, Action, Enabled (+2 more)

### Community 113 - "AcDc"
Cohesion: 0.29
Nodes (5): AcDc, InlineData, Theory, Fact, IdleTimeoutsTests

### Community 114 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 115 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 116 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 117 - ".RunStepsAsync"
Cohesion: 0.36
Nodes (3): CancellationToken, List, Task

### Community 118 - "HotkeyModifiers"
Cohesion: 0.22
Nodes (8): HotkeyModifiers, Alt, Control, None, Shift, Windows, InlineData, Theory

### Community 119 - "NoticeGate"
Cohesion: 0.22
Nodes (7): Key, Show, Action, List, NoticeGate, ShouldWait, WaitingCount

### Community 120 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 121 - "ThemeService"
Cohesion: 0.20
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 122 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 123 - ".Parse"
Cohesion: 0.29
Nodes (4): List, MemoryModule, Fact, FirmwareInventoryTests

### Community 124 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 125 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 126 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 127 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 128 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 129 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 130 - "LedEffect"
Cohesion: 0.22
Nodes (8): LedEffect, Blink, Breathing, ColourCycle, Heartbeat, Off, Static, Wave

### Community 131 - "ThermalSample"
Cohesion: 0.33
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 132 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 134 - ".IsSystemFileSignedBy"
Cohesion: 0.39
Nodes (3): Fact, SystemFileTrustTests, Cmd

### Community 135 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 136 - "CeilingTests"
Cohesion: 0.31
Nodes (5): Baseline, Fact, InlineData, Theory, CeilingTests

### Community 137 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 138 - ".Run"
Cohesion: 0.39
Nodes (4): Detail, Func, Ok, SelfTest

### Community 139 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 140 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 141 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 143 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 144 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 145 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 146 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 147 - ".ForegroundProcess"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Name, Pid

### Community 148 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 149 - "GpuAwakeFault"
Cohesion: 0.29
Nodes (4): TimeSpan, GpuAwakeFault, InlineData, Theory

### Community 150 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 151 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 152 - "Nextcalibur 0.5.5"
Cohesion: 0.33
Nodes (5): Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture

### Community 153 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 154 - "Nextcalibur 0.7.0"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.7.0, Tested on, Updated

### Community 155 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 156 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 157 - "ComboBox"
Cohesion: 0.33
Nodes (6): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, SettingNvPower, ComboBox

### Community 158 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 159 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 160 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 161 - "PawnIoModuleTests.cs"
Cohesion: 0.33
Nodes (4): system_reflection, InlineData, Theory, PawnIoModuleTests

### Community 162 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 163 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 164 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 165 - "TourStep"
Cohesion: 0.40
Nodes (5): TourStep, Body, SectionName, Title, TourPage

### Community 169 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 171 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 172 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 173 - "DriveGauge"
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

### Community 179 - ".GpuAwakeFault_identifies_true_stuck_awake_conditions"
Cohesion: 0.50
Nodes (3): InlineData, Theory, GpuAwakeFaultConditionTests

### Community 180 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 183 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 184 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

### Community 185 - "EffectPanel"
Cohesion: 0.67
Nodes (3): EffectPanel, ProfileRow, UniformGrid

### Community 186 - "PART_ContentHost"
Cohesion: 0.67
Nodes (3): PART_ContentHost, SettingsScroll, ScrollViewer

## Knowledge Gaps
- **598 isolated node(s):** `OverlayRepairPending`, `Accepted`, `SelectedColour`, `VisualChildrenCount`, `Diameter` (+593 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1151 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **35 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `AppSettings`, `SystemMode`, `LedEffect`, `ThermalSample`, `.Get`, `RunawayWatchTests`, `Nextcalibur.Core.Configuration`, `WindowsFaults`, `IShellLinkW`, `GpuClockReader`, `.Warn`, `ProgramRule`, `Window`, `RadioButton`, `PlanTuning`, `.ForegroundProcess`, `ResourceDictionary`, `TourPage`, `CoolingPolicyTests`, `SystemInfo`, `Button`, `.RequestRestart`, `.OnBrightnessChanged`, `.Info`, `.ModeName`, `UserPresence`, `LedZone`, `.Get`, `Log`, `.Choose`, `TourStep`, `UpdateService`, `.For`, `.LoadDefinitionsAsync`, `EcMailbox`, `Integrity`, `.OnSystemModeChanged`, `.RefreshIdleRows`, `IdleSetting`, `CpuPowerReader`, `PowerOverlayService`, `BacklightKeyWatcher`, `DependencyStatus`, `DevicePowerSaving`, `CeilingTestWindow`, `TemperatureFusion`, `TrayPresence`, `.WarnAboutNewConflicts`, `CoreLoad`, `.OnLanguageChanged`, `.ApplyFnShortcuts`, `LedController`, `Grid`, `Strings`, `VendorSoftware`, `.Check`, `FnShortcut`, `ThemeService`, `NvidiaDriverState`?**
  _High betweenness centrality (0.491) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Text`, `RadioButton`, `Button`, `.OnBrightnessChanged`, `ComboBox`, `Keycaps`, `DriveGauge`, `CpuWarnValue`, `DialogCheck`, `EffectPanel`, `PART_ContentHost`, `ToggleButton`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `TourCanvas`, `Wheel`, `Grid`, `StackPanel`, `Border`?**
  _High betweenness centrality (0.087) - this node is a cross-community bridge._
- **Why does `DependencyStatus` connect `DependencyStatus` to `MainWindow`, `Nextcalibur.Core.Security`, `.Info`, `Dependency`?**
  _High betweenness centrality (0.059) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `OverlayRepairPending`, `Accepted`, `SelectedColour` to the rest of the system?**
  _598 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.020760857367298845 - nodes in this community are weakly interconnected._
- **Should `AppSettings` be split into smaller, more focused modules?**
  _Cohesion score 0.03053221288515406 - nodes in this community are weakly interconnected._