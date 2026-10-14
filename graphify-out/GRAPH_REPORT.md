# Graph Report - nextcalibur-control-center  (2026-10-14)

## Corpus Check
- 217 files · ~263,449 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3709 nodes · 8239 edges · 202 communities (164 shown, 38 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 488 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `f64f39b3`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- FnShortcut
- GpuClockReader
- Authenticode
- AppSettings
- ProtectedStore
- .Get
- .CheckProgram
- ProgramRule
- RunawayWatchTests
- Nextcalibur.Core.Configuration
- LedState
- xunit
- IShellLinkW
- Window
- RadioButton
- PlanTuning
- .Info
- .OnLoaded
- ResourceDictionary
- TextBlock
- WindowsFaults
- CoolingPolicyTests
- GameLibrary
- NetworkCostTests
- Nextcalibur.Core.Hardware
- UserPresence
- Elevation
- Fact
- SystemInfo
- .StartSlowTimer
- CeilingTestWindow
- WindowsFaultsTests
- Button
- EcMailbox
- SystemTools
- CrashGuard
- Nextcalibur.Core.Security
- .ModeName
- ProcessPresence
- Api
- .RequestRestart
- .Get
- SystemModeService
- Fact
- .LoadDefinitionsAsync
- LedController
- Fact
- Program
- DevicePowerSaving
- .For
- Dependency
- .Check
- .PlaceTourStep
- FirmwareInventory
- ColourWheel
- TestPreflight
- .Ask
- DotNetRuntimeDependency
- MachineScanRecord
- Nextcalibur.Core.csproj
- AcDc
- SystemMode
- IdleSetting
- RestorePoint
- DependencyStatus
- .SetCeiling
- CpuPowerReader
- .Dictionary
- CpuLimits
- analyse-autopsy.py
- BacklightKeyWatcher
- MemoryTrimmer
- DarkMenuColours
- UpdateService
- RuntimeDependencyTests
- TemperatureFusion
- PowerOverlayService
- CeilingStep
- App
- .Choose
- TrayPresence
- .RefreshBanner
- .Migrate
- CpuLimitsTests
- FanGauge
- DevicePowerState
- Unelevated
- ProcessMetrics
- DonutGauge
- .RefreshIdleRows
- SettingsTests.cs
- MachineHistory
- BatteryModePolicy
- SegmentedBar
- microsoft_win32
- .Set
- Nextcalibur Control Center
- .OnLanguageChanged
- .RunAndRecord
- .HasAccess
- LogInjectionTests
- Grid
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Strings
- StackPanel
- PackageTemperatureTests
- SmiCommandTests
- Hardware Protocol
- Nextcalibur 0.5.3
- ThemeService
- CardSwitchTasks
- NvidiaDriverState
- .Parse
- SupportVerdict
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- ThermalSample
- Footprint
- PawnIoDependency
- Probe
- CeilingTests
- Nextcalibur 0.5.1
- Nextcalibur 0.5.5
- NoticeGate
- Toasts
- .Recommended
- NotificationState
- Test-Ui.ps1
- Text
- .BuildMenu
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- TourPage
- .Current
- Log-Graphics.ps1
- Privacy
- Nextcalibur 0.6.2
- Nextcalibur 0.7.0
- Releasing, and signing
- Security
- .OnBrightnessChanged
- ComboBox
- MachineWideGate
- PowerSource
- SystemModesTests
- Notice
- Nextcalibur 0.5.11
- AppIdentity.cs
- .Set
- SmiSubsystem
- .Read
- .FirmwareChanged
- NduFix
- RunningKind
- .ParseMarker
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Strings.cs
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- Nextcalibur 0.6.1
- .Quietly
- .ArrangeOverride
- Tools
- 0.5.7.md
- DialogCheck
- EffectPanel
- PART_ContentHost
- .Paint
- .Work_that_must_not_be_interrupted
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

## Communities (202 total, 38 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.02
Nodes (82): Added, DeferredOverheat, Found, GameSession, HwndSource, ObservableCollection, PluggedIn, Queue (+74 more)

### Community 1 - "FnShortcut"
Cohesion: 0.05
Nodes (42): INotifyPropertyChanged, Problem, DriveRow, Detail, Name, Percent, PercentText, Dictionary (+34 more)

### Community 2 - "GpuClockReader"
Cohesion: 0.06
Nodes (29): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, ProcessorPerformance, Detail, Func, Ok, SelfTest (+21 more)

### Community 3 - "Authenticode"
Cohesion: 0.06
Nodes (38): CatalogInfo, Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on (+30 more)

### Community 4 - "AppSettings"
Cohesion: 0.03
Nodes (62): DateTime, Dictionary, FnShortcut, JsonElement, List, ProgramRule, AppSettings, AcceptedVendorSoftware (+54 more)

### Community 5 - "ProtectedStore"
Cohesion: 0.07
Nodes (26): Content, DirectorySecurity, Hash, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Dictionary (+18 more)

### Community 6 - ".Get"
Cohesion: 0.08
Nodes (15): Asynchronous dispatcher & UI thread hardening, GameEnd, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary (+7 more)

### Community 7 - ".CheckProgram"
Cohesion: 0.06
Nodes (28): Lazy, ProcessModule, FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Dictionary, HashSet (+20 more)

### Community 8 - "ProgramRule"
Cohesion: 0.06
Nodes (33): HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, GeneratedRegex, IEnumerable (+25 more)

### Community 9 - "RunawayWatchTests"
Cohesion: 0.07
Nodes (34): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+26 more)

### Community 10 - "Nextcalibur.Core.Configuration"
Cohesion: 0.10
Nodes (25): Nextcalibur.App, Nextcalibur.App.Controls, Nextcalibur.Core.Configuration, system_componentmodel, system_drawing, system_linq, system_runtime_compilerservices, system_windows (+17 more)

### Community 11 - "LedState"
Cohesion: 0.07
Nodes (32): B, Dictionary, G, LedBrightness, R, LedProfile, BrightnessPercent, Colours (+24 more)

### Community 12 - "xunit"
Cohesion: 0.09
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, nextcalibur_core_power_plantuning_acdc, system_text_regularexpressions, system_xml_linq, xunit

### Community 13 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 14 - "Window"
Cohesion: 0.07
Nodes (46): TemperatureToDoubleConverter, Foreground, IsMouseOver, ItemsSource.Count, Name, Banner, Bd, ColGauge (+38 more)

### Community 15 - "RadioButton"
Cohesion: 0.06
Nodes (42): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeBalanced, ModeDiscrete (+34 more)

### Community 16 - "PlanTuning"
Cohesion: 0.12
Nodes (18): Floor, Marker, Original, Action, AcDc, DllImport, Guid, IEnumerable (+10 more)

### Community 17 - ".Info"
Cohesion: 0.09
Nodes (9): Guid, Ours, Plan, MessageBoxButton, MessageBoxResult, Exception, Log, Folder (+1 more)

### Community 18 - ".OnLoaded"
Cohesion: 0.07
Nodes (12): AssemblyInformationalVersionAttribute, CancelEventArgs, Option, PowerModeChangedEventArgs, Slider, Button, Guid, IEnumerable (+4 more)

### Community 19 - "ResourceDictionary"
Cohesion: 0.07
Nodes (40): ActualWidth, IsDropDownOpen, Size, Arrow, Bar, Bg, Blade, Box (+32 more)

### Community 20 - "TextBlock"
Cohesion: 0.05
Nodes (41): Detail, PercentText, BannerBody, BannerTitle, BrightnessValue, CpuClock, CpuFan, CpuTemp (+33 more)

### Community 21 - "WindowsFaults"
Cohesion: 0.09
Nodes (24): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, CoreLoad, Count, ProcessorPerformance (+16 more)

### Community 22 - "CoolingPolicyTests"
Cohesion: 0.08
Nodes (26): At, Margin, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc (+18 more)

### Community 23 - "GameLibrary"
Cohesion: 0.11
Nodes (14): Dictionary, GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, IReadOnlyList, Path, Regex (+6 more)

### Community 24 - "NetworkCostTests"
Cohesion: 0.11
Nodes (26): Answer, Bytes, Count, HttpMessageHandler, CancellationToken, ConcurrentDictionary, HttpClient, Task (+18 more)

### Community 25 - "Nextcalibur.Core.Hardware"
Cohesion: 0.10
Nodes (8): Nextcalibur.Cli, Nextcalibur.Core.Input, Nextcalibur.Core.Hardware, microsoft_win32_safehandles, system_diagnostics, system_management, system_runtime_interopservices, system_text

### Community 26 - "UserPresence"
Cohesion: 0.12
Nodes (14): LastInputInfo, MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr (+6 more)

### Community 27 - "Elevation"
Cohesion: 0.11
Nodes (10): dynamic, Elevation, Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles (+2 more)

### Community 28 - "Fact"
Cohesion: 0.13
Nodes (9): Func, IEnumerable, IReadOnlyList, Version, RetirementEntry, Trace, Fact, FootprintTests (+1 more)

### Community 29 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 30 - ".StartSlowTimer"
Cohesion: 0.08
Nodes (3): IReadOnlyList, GameEnd, TextBlock

### Community 31 - "CeilingTestWindow"
Cohesion: 0.12
Nodes (14): CancellationTokenSource, Button, CancellationToken, CheckBox, Func, List, StackPanel, Task (+6 more)

### Community 32 - "WindowsFaultsTests"
Cohesion: 0.17
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 33 - "Button"
Cohesion: 0.06
Nodes (30): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, GameScanButton (+22 more)

### Community 34 - "EcMailbox"
Cohesion: 0.14
Nodes (13): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+5 more)

### Community 35 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 36 - "CrashGuard"
Cohesion: 0.11
Nodes (19): Session, DateTimeOffset, Exception, List, TimeSpan, CrashGuard, SafeMode, UncleanPrevious (+11 more)

### Community 37 - "Nextcalibur.Core.Security"
Cohesion: 0.11
Nodes (9): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_security_cryptography_x509certificates, system_text_json (+1 more)

### Community 38 - ".ModeName"
Cohesion: 0.12
Nodes (7): Task, FrameworkElement, Func, Grid, HashSet, List, ModeChoice

### Community 39 - "ProcessPresence"
Cohesion: 0.13
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 40 - "Api"
Cohesion: 0.11
Nodes (23): Api, GetSettingFn, InitializeFn, ProfileOutFn, SessionFn, SessionOutFn, Set, SetSettingFn (+15 more)

### Community 41 - ".RequestRestart"
Cohesion: 0.12
Nodes (10): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr, MarshalAs (+2 more)

### Community 42 - ".Get"
Cohesion: 0.13
Nodes (14): ArgumentNullException, GpuLoad, FirmwareModeReading, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma (+6 more)

### Community 43 - "SystemModeService"
Cohesion: 0.17
Nodes (10): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, Ours, Plan (+2 more)

### Community 44 - "Fact"
Cohesion: 0.16
Nodes (8): IList, HardwareSupport, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 45 - ".LoadDefinitionsAsync"
Cohesion: 0.13
Nodes (15): Grid, Task, GpuPowerLimits, IEnumerable, PowerDefinition, PowerDefinitions, SetBy, Intel (+7 more)

### Community 46 - "LedController"
Cohesion: 0.14
Nodes (12): LedController, EffectiveBrightnessPercent, HardwareLevel, State, LedEffect, Blink, Breathing, ColourCycle (+4 more)

### Community 47 - "Fact"
Cohesion: 0.11
Nodes (14): OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact, Task (+6 more)

### Community 48 - "Program"
Cohesion: 0.14
Nodes (5): ConsoleColor, B, G, R, Program

### Community 49 - "DevicePowerSaving"
Cohesion: 0.17
Nodes (12): TextBlock, ToggleButton, AcDc, Guid, IEnumerable, DevicePowerSaving, Off, DeviceSaving (+4 more)

### Community 50 - ".For"
Cohesion: 0.16
Nodes (13): AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, Baseline (+5 more)

### Community 51 - "Dependency"
Cohesion: 0.13
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 52 - ".Check"
Cohesion: 0.14
Nodes (9): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported, IdempotenceTests (+1 more)

### Community 53 - ".PlaceTourStep"
Cohesion: 0.11
Nodes (10): SizeChangedEventArgs, FrameworkElement, KeyEventArgs, RoutedEventArgs, TourStep, Body, SectionName, Title (+2 more)

### Community 54 - "FirmwareInventory"
Cohesion: 0.09
Nodes (22): IReadOnlyList, FirmwareInventory, BatteryChemistry, BatteryDesignMwh, BatteryName, BiosDate, BiosRelease, BiosVendor (+14 more)

### Community 55 - "ColourWheel"
Cohesion: 0.12
Nodes (13): BitmapSource, Control, Ellipse, MouseEventArgs, Canvas, DependencyProperty, Image, MouseButtonEventArgs (+5 more)

### Community 56 - "TestPreflight"
Cohesion: 0.16
Nodes (11): Display, Signing: what it is and what it buys, IoCounters, ProcessLook, ConcurrentDictionary, Dictionary, DllImport, IntPtr (+3 more)

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

### Community 61 - "AcDc"
Cohesion: 0.20
Nodes (8): MemberData, AcDc, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 62 - "SystemMode"
Cohesion: 0.14
Nodes (13): ThermalProfile, IReadOnlyList, SystemMode, Balanced, Gaming, Office, Performance, SystemModes (+5 more)

### Community 63 - "IdleSetting"
Cohesion: 0.17
Nodes (10): AcDc, Guid, IReadOnlyList, IdleSetting, DisplayOff, Sleep, IdleTimeouts, Timeouts (+2 more)

### Community 64 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 65 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Ok, Task (+7 more)

### Community 66 - ".SetCeiling"
Cohesion: 0.15
Nodes (4): DllImport, IntPtr, Name, Pid

### Community 67 - "CpuPowerReader"
Cohesion: 0.24
Nodes (5): DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 68 - ".Dictionary"
Cohesion: 0.15
Nodes (9): IReadOnlyDictionary, Stable, Dictionary, Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests (+1 more)

### Community 69 - "CpuLimits"
Cohesion: 0.13
Nodes (13): Bit, Now, PowerInfo, PowerLimits, Ratio, SinceBoot, IReadOnlyList, IReadOnlySet (+5 more)

### Community 70 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 71 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 72 - "MemoryTrimmer"
Cohesion: 0.14
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 73 - "DarkMenuColours"
Cohesion: 0.11
Nodes (19): ProfessionalColorTable, Color, DarkMenuColours, CheckBackground, CheckPressedBackground, CheckSelectedBackground, ImageMarginGradientBegin, ImageMarginGradientEnd (+11 more)

### Community 74 - "UpdateService"
Cohesion: 0.13
Nodes (12): DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService, Available, AvailableVersion (+4 more)

### Community 75 - "RuntimeDependencyTests"
Cohesion: 0.22
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 76 - "TemperatureFusion"
Cohesion: 0.25
Nodes (9): DateTime, TimeSpan, TemperatureFusion, Offset, RegisterSetAside, Value, DateTime, Fact (+1 more)

### Community 77 - "PowerOverlayService"
Cohesion: 0.23
Nodes (7): DllImport, Guid, IReadOnlyList, PowerModeOption, PowerOverlays, All, PowerOverlayService

### Community 78 - "CeilingStep"
Cohesion: 0.21
Nodes (7): IEnumerable, IReadOnlyList, TimeSpan, CeilingStep, CeilingTest, Fact, CeilingTestTests

### Community 79 - "App"
Cohesion: 0.16
Nodes (6): The threat model, and what the code does about it, Application, DllImport, Mutex, App, OverlayRepairPending

### Community 80 - ".Choose"
Cohesion: 0.29
Nodes (6): IEnumerable, IReadOnlyList, IdleLoadPicker, RunningItem, Fact, IdleLoadPickerTests

### Community 81 - "TrayPresence"
Cohesion: 0.14
Nodes (9): Icon, Item, NotifyIcon, List, Ms, Window, TrayPresence, Dark (+1 more)

### Community 83 - ".Migrate"
Cohesion: 0.17
Nodes (5): Limits, SystemMode, InlineData, Theory, SettingsTests

### Community 84 - "CpuLimitsTests"
Cohesion: 0.18
Nodes (6): ArgumentOutOfRangeException, Offset, Fact, CpuLimitsTests, ThermFlags, TjMax

### Community 85 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 86 - "DevicePowerState"
Cohesion: 0.28
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 87 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 88 - "ProcessMetrics"
Cohesion: 0.14
Nodes (14): Share, Dictionary, Pid, TimeSpan, FaultEvidence, ProcessMetrics, Name, PageFaultCount (+6 more)

### Community 89 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 90 - ".RefreshIdleRows"
Cohesion: 0.22
Nodes (3): ComboBox, Grid, Guid

### Community 91 - "SettingsTests.cs"
Cohesion: 0.19
Nodes (7): system_reflection, system_security_accesscontrol, system_security_cryptography, system_security_principal, InlineData, Theory, PawnIoModuleTests

### Community 92 - "MachineHistory"
Cohesion: 0.28
Nodes (6): DateTime, IReadOnlyList, MachineHistory, DateTime, Fact, MachineHistoryTests

### Community 93 - "BatteryModePolicy"
Cohesion: 0.35
Nodes (5): BatteryModePolicy, QuietMode, Remembered, Fact, BatteryModePolicyTests

### Community 94 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 95 - "microsoft_win32"
Cohesion: 0.14
Nodes (6): microsoft_win32, Theme, Dark, Light, system_io, system_security

### Community 96 - ".Set"
Cohesion: 0.30
Nodes (7): PowerThrottlingState, DllImport, IntPtr, EfficiencyMode, PowerThrottlingState, Fact, EfficiencyModeTests

### Community 97 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 99 - ".RunAndRecord"
Cohesion: 0.22
Nodes (6): JsonSerializerOptions, MachineScan, Firmware, RecordPath, Fact, MachineScanTests

### Community 100 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 101 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 102 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 103 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 104 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 105 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 106 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 107 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 108 - "StackPanel"
Cohesion: 0.17
Nodes (12): BannerActions, CeilingRows, DefinitionRows, DialogActions, DriveTextStack, FnShortcutRows, IdleLoadRows, IdleRows (+4 more)

### Community 109 - "PackageTemperatureTests"
Cohesion: 0.41
Nodes (4): Fact, InlineData, Theory, PackageTemperatureTests

### Community 110 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 111 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 112 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 113 - "ThemeService"
Cohesion: 0.18
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

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

### Community 123 - "ThermalSample"
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

### Community 130 - "NoticeGate"
Cohesion: 0.29
Nodes (7): Key, Show, Action, List, NoticeGate, ShouldWait, WaitingCount

### Community 131 - "Toasts"
Cohesion: 0.32
Nodes (5): Action, Toasts, Balloon, windows_data_xml_dom, windows_ui_notifications

### Community 132 - ".Recommended"
Cohesion: 0.29
Nodes (5): Min, Fact, InlineData, Theory, IdleRecommendedTests

### Community 133 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 134 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 135 - "Text"
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

### Community 140 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 141 - ".Current"
Cohesion: 0.48
Nodes (3): DllImport, IntPtr, ScreenSaver

### Community 142 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 143 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 144 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 145 - "Nextcalibur 0.7.0"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.7.0, Tested on, Updated

### Community 146 - "Releasing, and signing"
Cohesion: 0.33
Nodes (6): How a release happens, Releasing, and signing, Repository settings that matter, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 147 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 148 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 149 - "ComboBox"
Cohesion: 0.33
Nodes (6): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, SettingNvPower, ComboBox

### Community 150 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 151 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 153 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 154 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 155 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 157 - "SmiSubsystem"
Cohesion: 0.40
Nodes (5): SmiSubsystem, DisplayMode, Led, Profile, Thermal

### Community 161 - "RunningKind"
Cohesion: 0.40
Nodes (5): RunningKind, App, Background, Service, Transfer

### Community 163 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 165 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 166 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 167 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 169 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 170 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 171 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 172 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 175 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 178 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

### Community 179 - "EffectPanel"
Cohesion: 0.67
Nodes (3): EffectPanel, ProfileRow, UniformGrid

### Community 180 - "PART_ContentHost"
Cohesion: 0.67
Nodes (3): PART_ContentHost, SettingsScroll, ScrollViewer

## Knowledge Gaps
- **598 isolated node(s):** `OverlayRepairPending`, `Accepted`, `SelectedColour`, `VisualChildrenCount`, `Diameter` (+593 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1150 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **38 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `Nextcalibur 0.5.5`, `FnShortcut`, `GpuClockReader`, `AppSettings`, `.Get`, `.CheckProgram`, `ProgramRule`, `RunawayWatchTests`, `Nextcalibur.Core.Configuration`, `LedState`, `TourPage`, `Window`, `RadioButton`, `PlanTuning`, `.Info`, `.OnLoaded`, `ResourceDictionary`, `.OnBrightnessChanged`, `WindowsFaults`, `CoolingPolicyTests`, `UserPresence`, `SystemInfo`, `.StartSlowTimer`, `CeilingTestWindow`, `Button`, `EcMailbox`, `.ModeName`, `.RequestRestart`, `.Get`, `SystemModeService`, `.LoadDefinitionsAsync`, `LedController`, `DevicePowerSaving`, `.For`, `.PlaceTourStep`, `AcDc`, `SystemMode`, `IdleSetting`, `DependencyStatus`, `.SetCeiling`, `CpuPowerReader`, `BacklightKeyWatcher`, `UpdateService`, `TemperatureFusion`, `PowerOverlayService`, `.Choose`, `TrayPresence`, `.RefreshBanner`, `.RefreshIdleRows`, `BatteryModePolicy`, `.OnLanguageChanged`, `Grid`, `Strings`, `ThemeService`, `NvidiaDriverState`, `SupportVerdict`, `ThermalSample`?**
  _High betweenness centrality (0.494) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Button`, `Grid`, `Text`, `DriveGauge`, `Keycaps`, `StackPanel`, `RadioButton`, `DialogCheck`, `EffectPanel`, `TextBlock`, `.OnBrightnessChanged`, `ComboBox`, `PART_ContentHost`, `DriveDot`, `DriveList`, `TourCanvas`, `Wheel`?**
  _High betweenness centrality (0.087) - this node is a cross-community bridge._
- **Why does `Nextcalibur.Core.Hardware` connect `Nextcalibur.Core.Hardware` to `EcMailbox`, `CpuLimits`, `Nextcalibur.Core.Security`, `BacklightKeyWatcher`, `RunawayWatchTests`, `Nextcalibur.Core.Configuration`, `ThermalSample`, `xunit`, `SupportVerdict`, `MachineWideGate`, `SettingsTests.cs`, `microsoft_win32`?**
  _High betweenness centrality (0.060) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `OverlayRepairPending`, `Accepted`, `SelectedColour` to the rest of the system?**
  _598 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.022747156605424323 - nodes in this community are weakly interconnected._
- **Should `FnShortcut` be split into smaller, more focused modules?**
  _Cohesion score 0.053208137715179966 - nodes in this community are weakly interconnected._