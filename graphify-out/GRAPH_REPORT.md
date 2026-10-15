# Graph Report - nextcalibur-control-center  (2026-10-15)

## Corpus Check
- 221 files · ~268,622 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3789 nodes · 8432 edges · 209 communities (178 shown, 31 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 502 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `a832ea27`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- AppSettings
- GpuClockReader
- FnShortcut
- .Get
- .OnLoaded
- GameLibrary
- RunawayWatchTests
- ProgramRule
- ProtectedStore
- ProcessPresence
- WindowsFaults
- Window
- Nextcalibur.App
- IShellLinkW
- Nextcalibur.Core.Hardware
- PlanTuning
- RadioButton
- CoolingPolicyTests
- ResourceDictionary
- UserPresence
- NetworkCostTests
- .Retirements
- .RequestRestart
- .Main
- Fact
- .RuleRow
- .For
- CpuLimits
- Button
- SystemInfo
- WindowsFaultsTests
- Nextcalibur.Core.Configuration
- Localized
- CrashGuard
- SystemTools
- .RefreshIdleRows
- Api
- .Choose
- LedState
- .Get
- system_runtime_interopservices
- SystemModeService
- Dependency
- CeilingTestWindow
- TestPreflight
- Fact
- Integrity
- SystemMode
- InstallFolderGuard
- .SignatureIsValid
- .LoadDefinitionsAsync
- FirmwareInventory
- EcMailbox
- PowerOverlayService
- .ShowSaving
- Timeouts
- Authenticode
- Program
- TrayPresence
- .Check
- .OnSystemModeChanged
- .PlaceTourStep
- .Info
- DotNetRuntimeDependency
- MachineScanRecord
- RestorePoint
- Nextcalibur.Core.csproj
- .ShowResult
- CpuPowerReader
- ToggleButton
- .Ask
- MemoryTrimmer
- DependencyStatus
- .Dictionary
- analyse-autopsy.py
- ModeProfileTests
- DarkMenuColours
- .CheckProgram
- TemperatureFusion
- UpdateService
- RuntimeDependencyTests
- SmiCommand
- ColourWheel
- DevicePowerState
- Unelevated
- .WireSettingsPage
- LedController
- FanGauge
- system_text
- DonutGauge
- MachineHistory
- BatteryModePolicy
- .OnClosing
- Hardware Protocol
- SegmentedBar
- .Set
- Nextcalibur Control Center
- ThemeService
- LedEffect
- .RunAndRecord
- .HasAccess
- .Record
- LogInjectionTests
- microsoft_win32
- AcDc
- Grid
- LedBrightness
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- Strings
- StackPanel
- PackageTemperatureTests
- Nextcalibur 0.5.3
- NvidiaDriverState
- IntPtr
- .ToHsv
- NoticeGate
- Border
- .Sanitised
- SupportVerdict
- AppIdentity.cs
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- BacklightKeyWatcher
- PawnIoDependency
- .Parse
- .IsSystemFileSignedBy
- Probe
- CeilingTests
- .ArrangeOverride
- Nextcalibur 0.5.1
- Nextcalibur 0.5.5
- ThermalSample
- CoreLoad
- NduFix
- IntegrityKind
- Test-Ui.ps1
- Text
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- .LoadPowerModes
- .ForegroundProcess
- TourPage
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
- Nextcalibur 0.7.2
- SmiSubsystem
- .Read
- .FirmwareChanged
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- 2. Command structure
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- Nextcalibur 0.6.1
- .Modules_are_the_released_ones
- Tools
- 0.5.7.md
- CpuWarnValue
- DialogCheck
- EffectPanel
- PART_ContentHost
- .Paint
- Words
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
1. `MainWindow` - 449 edges
2. `Window` - 199 edges
3. `AppSettings` - 96 edges
4. `Nextcalibur.Core.Hardware` - 77 edges
5. `SystemMode` - 69 edges
6. `Nextcalibur.Core.Power` - 51 edges
7. `Nextcalibur.Core.Configuration` - 50 edges
8. `Nextcalibur.Core.Tests` - 46 edges
9. `RadioButton` - 41 edges
10. `TextBlock` - 41 edges

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

## Communities (209 total, 31 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.02
Nodes (87): Added, DeferredOverheat, Found, GameSession, HwndSource, ObservableCollection, PluggedIn, Queue (+79 more)

### Community 1 - "AppSettings"
Cohesion: 0.03
Nodes (71): DateTime, Dictionary, FnShortcut, JsonElement, Limits, List, ProgramRule, SystemMode (+63 more)

### Community 2 - "GpuClockReader"
Cohesion: 0.05
Nodes (35): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, ProcessorPerformance, Detail, Func, Ok, SelfTest (+27 more)

### Community 3 - "FnShortcut"
Cohesion: 0.05
Nodes (43): Nextcalibur.Core.Input, INotifyPropertyChanged, Problem, DriveRow, Detail, Name, Percent, PercentText (+35 more)

### Community 4 - ".Get"
Cohesion: 0.06
Nodes (14): GameEnd, SolidColorBrush, IReadOnlyList, GameEnd, Guid, Ours, Plan, Task (+6 more)

### Community 5 - ".OnLoaded"
Cohesion: 0.07
Nodes (13): Asynchronous dispatcher & UI thread hardening, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary, IReadOnlyDictionary (+5 more)

### Community 6 - "GameLibrary"
Cohesion: 0.07
Nodes (20): Folder, Dictionary, GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, IReadOnlyList, List (+12 more)

### Community 7 - "RunawayWatchTests"
Cohesion: 0.07
Nodes (34): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+26 more)

### Community 8 - "ProgramRule"
Cohesion: 0.06
Nodes (31): HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, GeneratedRegex, IEnumerable (+23 more)

### Community 9 - "ProtectedStore"
Cohesion: 0.09
Nodes (20): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+12 more)

### Community 10 - "ProcessPresence"
Cohesion: 0.06
Nodes (23): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+15 more)

### Community 11 - "WindowsFaults"
Cohesion: 0.07
Nodes (36): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+28 more)

### Community 12 - "Window"
Cohesion: 0.07
Nodes (48): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+40 more)

### Community 13 - "Nextcalibur.App"
Cohesion: 0.09
Nodes (24): Nextcalibur.App, Nextcalibur.App.Controls, system_drawing, system_linq, system_windows, system_windows_controls, system_windows_controls_button, system_windows_controls_checkbox (+16 more)

### Community 14 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 15 - "Nextcalibur.Core.Hardware"
Cohesion: 0.12
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, Nextcalibur.Core.Hardware, nextcalibur_core_power_plantuning_acdc, system_xml_linq, xunit

### Community 16 - "PlanTuning"
Cohesion: 0.13
Nodes (17): Floor, Marker, Original, Action, AcDc, DllImport, Guid, IEnumerable (+9 more)

### Community 17 - "RadioButton"
Cohesion: 0.07
Nodes (35): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeDiscrete, ModeHybrid (+27 more)

### Community 18 - "CoolingPolicyTests"
Cohesion: 0.08
Nodes (26): At, Margin, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc (+18 more)

### Community 19 - "ResourceDictionary"
Cohesion: 0.07
Nodes (38): ActualWidth, IsDropDownOpen, Size, Arrow, Bar, Bg, Blade, Box (+30 more)

### Community 20 - "UserPresence"
Cohesion: 0.11
Nodes (14): LastInputInfo, MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr (+6 more)

### Community 21 - "NetworkCostTests"
Cohesion: 0.11
Nodes (26): Answer, Bytes, Count, HttpMessageHandler, CancellationToken, ConcurrentDictionary, HttpClient, Task (+18 more)

### Community 22 - ".Retirements"
Cohesion: 0.11
Nodes (9): dynamic, Encoding, StartupRegistration, IsEnabled, DllImport, Elevation, CardSwitchTasks, Fact (+1 more)

### Community 23 - ".RequestRestart"
Cohesion: 0.09
Nodes (11): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr, MarshalAs (+3 more)

### Community 24 - ".Main"
Cohesion: 0.11
Nodes (9): The threat model, and what the code does about it, Application, DllImport, Mutex, App, OverlayRepairPending, RegistryKey, Footprint (+1 more)

### Community 25 - "Fact"
Cohesion: 0.11
Nodes (9): Func, IEnumerable, IReadOnlyList, Version, RetirementEntry, Trace, Fact, FootprintTests (+1 more)

### Community 26 - ".RuleRow"
Cohesion: 0.11
Nodes (8): AssemblyInformationalVersionAttribute, Task, FrameworkElement, Func, Grid, HashSet, List, ModeChoice

### Community 27 - ".For"
Cohesion: 0.12
Nodes (14): AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, Baseline (+6 more)

### Community 28 - "CpuLimits"
Cohesion: 0.09
Nodes (19): ArgumentOutOfRangeException, Bit, Now, Offset, PowerInfo, PowerLimits, Ratio, SinceBoot (+11 more)

### Community 29 - "Button"
Cohesion: 0.06
Nodes (29): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, GameScanButton (+21 more)

### Community 30 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 32 - "Nextcalibur.Core.Configuration"
Cohesion: 0.12
Nodes (10): Nextcalibur.Core.Security, Nextcalibur.Core.Configuration, Nextcalibur.Core.Tests.Attacks, system_security_accesscontrol, system_security_cryptography, system_security_cryptography_x509certificates, system_security_principal, system_text_json (+2 more)

### Community 33 - "Localized"
Cohesion: 0.09
Nodes (15): ItemsControl, Property, DependencyObject, DependencyProperty, Func, List, TextBlock, Value (+7 more)

### Community 34 - "CrashGuard"
Cohesion: 0.11
Nodes (19): Session, DateTimeOffset, Exception, List, TimeSpan, CrashGuard, SafeMode, UncleanPrevious (+11 more)

### Community 35 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 36 - ".RefreshIdleRows"
Cohesion: 0.11
Nodes (11): Kept, ComboBox, Func, Grid, Guid, Guid, IReadOnlyList, Plan (+3 more)

### Community 37 - "Api"
Cohesion: 0.10
Nodes (23): Api, GetSettingFn, InitializeFn, ProfileOutFn, SessionFn, SessionOutFn, Set, SetSettingFn (+15 more)

### Community 38 - ".Choose"
Cohesion: 0.12
Nodes (19): IEnumerable, IReadOnlyList, IdleLoadPicker, RunningItem, RunningKind, App, Background, Service (+11 more)

### Community 39 - "LedState"
Cohesion: 0.14
Nodes (17): B, G, LedBrightness, R, LedState, ActiveProfile, BrightnessPercent, Current (+9 more)

### Community 40 - ".Get"
Cohesion: 0.15
Nodes (11): ArgumentNullException, GpuLoad, FirmwareModeReading, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma (+3 more)

### Community 41 - "system_runtime_interopservices"
Cohesion: 0.10
Nodes (9): Nextcalibur.Cli, microsoft_win32_safehandles, system_componentmodel, system_diagnostics, system_io, system_management, system_reflection, system_runtime_compilerservices (+1 more)

### Community 42 - "SystemModeService"
Cohesion: 0.17
Nodes (10): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, Ours, Plan (+2 more)

### Community 43 - "Dependency"
Cohesion: 0.11
Nodes (18): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+10 more)

### Community 44 - "CeilingTestWindow"
Cohesion: 0.14
Nodes (12): CancellationTokenSource, Button, CancellationToken, CheckBox, Func, StackPanel, Task, TextBlock (+4 more)

### Community 45 - "TestPreflight"
Cohesion: 0.14
Nodes (13): Display, IoCounters, ProcessLook, ConcurrentDictionary, Dictionary, DllImport, IntPtr, TimeSpan (+5 more)

### Community 46 - "Fact"
Cohesion: 0.16
Nodes (8): IList, HardwareSupport, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 47 - "Integrity"
Cohesion: 0.11
Nodes (11): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvapiPath, NvmlPath (+3 more)

### Community 48 - "SystemMode"
Cohesion: 0.13
Nodes (13): ThermalProfile, IReadOnlyList, SystemMode, Balanced, Gaming, Office, Performance, SystemModes (+5 more)

### Community 49 - "InstallFolderGuard"
Cohesion: 0.14
Nodes (10): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Fact, InlineData, Theory, ElevationPolicyTests (+2 more)

### Community 50 - ".SignatureIsValid"
Cohesion: 0.15
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 51 - ".LoadDefinitionsAsync"
Cohesion: 0.14
Nodes (14): Task, GpuPowerLimits, IEnumerable, PowerDefinition, PowerDefinitions, SetBy, Intel, LaptopMaker (+6 more)

### Community 52 - "FirmwareInventory"
Cohesion: 0.08
Nodes (23): IReadOnlyList, FirmwareInventory, BatteryChemistry, BatteryDesignMwh, BatteryName, BiosDate, BiosRelease, BiosVendor (+15 more)

### Community 53 - "EcMailbox"
Cohesion: 0.15
Nodes (12): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+4 more)

### Community 54 - "PowerOverlayService"
Cohesion: 0.16
Nodes (11): DllImport, Guid, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, PowerModeOption, PowerOverlays (+3 more)

### Community 55 - ".ShowSaving"
Cohesion: 0.17
Nodes (12): TextBlock, ToggleButton, AcDc, Guid, IEnumerable, DevicePowerSaving, Off, DeviceSaving (+4 more)

### Community 56 - "Timeouts"
Cohesion: 0.13
Nodes (13): Min, AcDc, Guid, IReadOnlyList, IdleSetting, DisplayOff, Sleep, IdleTimeouts (+5 more)

### Community 57 - "Authenticode"
Cohesion: 0.25
Nodes (14): CatalogInfo, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle, Authenticode (+6 more)

### Community 58 - "Program"
Cohesion: 0.15
Nodes (5): ConsoleColor, B, G, R, Program

### Community 59 - "TrayPresence"
Cohesion: 0.15
Nodes (10): ContextMenuStrip, EventHandler, Item, NotifyIcon, List, Ms, Window, TrayPresence (+2 more)

### Community 60 - ".Check"
Cohesion: 0.15
Nodes (8): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported, MailboxAccessTests

### Community 61 - ".OnSystemModeChanged"
Cohesion: 0.15
Nodes (8): PowerModeChangedEventArgs, ModeBalanced, ModeGaming, ModeOffice, ModePerformance, Guid, Ours, Plan

### Community 62 - ".PlaceTourStep"
Cohesion: 0.11
Nodes (8): SizeChangedEventArgs, KeyEventArgs, RoutedEventArgs, TourStep, Body, SectionName, Title, TourPage

### Community 63 - ".Info"
Cohesion: 0.17
Nodes (3): Exception, Log, Folder

### Community 64 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 65 - "MachineScanRecord"
Cohesion: 0.09
Nodes (22): DateTime, List, MachineScanRecord, BiosDate, BiosRelease, BiosVendor, BiosVersion, BoardProduct (+14 more)

### Community 66 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 67 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 68 - ".ShowResult"
Cohesion: 0.19
Nodes (8): List, IEnumerable, IReadOnlyList, TimeSpan, CeilingStep, CeilingTest, Fact, CeilingTestTests

### Community 69 - "CpuPowerReader"
Cohesion: 0.22
Nodes (5): DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 70 - "ToggleButton"
Cohesion: 0.10
Nodes (21): OverheatWarningToggle, SettingAskGameMode, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost, SettingFixWidgets (+13 more)

### Community 71 - ".Ask"
Cohesion: 0.25
Nodes (10): HttpStatusCode, CancellationToken, Fact, HttpRequestMessage, HttpResponseMessage, InlineData, Task, Theory (+2 more)

### Community 72 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 73 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Ok, Task (+7 more)

### Community 74 - ".Dictionary"
Cohesion: 0.15
Nodes (9): IReadOnlyDictionary, Stable, Dictionary, Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests (+1 more)

### Community 75 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 76 - "ModeProfileTests"
Cohesion: 0.22
Nodes (7): MemberData, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 77 - "DarkMenuColours"
Cohesion: 0.11
Nodes (19): ProfessionalColorTable, Color, DarkMenuColours, CheckBackground, CheckPressedBackground, CheckSelectedBackground, ImageMarginGradientBegin, ImageMarginGradientEnd (+11 more)

### Community 78 - ".CheckProgram"
Cohesion: 0.22
Nodes (5): Fact, IntegrityTests, Current, Exe, Manifest

### Community 79 - "TemperatureFusion"
Cohesion: 0.25
Nodes (9): DateTime, TimeSpan, TemperatureFusion, Offset, RegisterSetAside, Value, DateTime, Fact (+1 more)

### Community 80 - "UpdateService"
Cohesion: 0.14
Nodes (12): DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService, Available, AvailableVersion (+4 more)

### Community 81 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 82 - "SmiCommand"
Cohesion: 0.21
Nodes (6): ArgumentException, SmiCommand, Fact, InlineData, Theory, SmiCommandTests

### Community 83 - "ColourWheel"
Cohesion: 0.14
Nodes (12): Control, Ellipse, MouseEventArgs, Canvas, DependencyProperty, Image, MouseButtonEventArgs, ColourWheel (+4 more)

### Community 84 - "DevicePowerState"
Cohesion: 0.25
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 85 - "Unelevated"
Cohesion: 0.33
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 86 - ".WireSettingsPage"
Cohesion: 0.20
Nodes (3): Task, Action, ToggleButton

### Community 87 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 88 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 89 - "system_text"
Cohesion: 0.23
Nodes (5): Nextcalibur.Core.Dependencies, system_collections_concurrent, system_net, system_net_http, system_text

### Community 90 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 91 - "MachineHistory"
Cohesion: 0.28
Nodes (6): DateTime, IReadOnlyList, MachineHistory, DateTime, Fact, MachineHistoryTests

### Community 92 - "BatteryModePolicy"
Cohesion: 0.35
Nodes (5): BatteryModePolicy, QuietMode, Remembered, Fact, BatteryModePolicyTests

### Community 93 - ".OnClosing"
Cohesion: 0.15
Nodes (3): CancelEventArgs, DllImport, IntPtr

### Community 94 - "Hardware Protocol"
Cohesion: 0.14
Nodes (14): 1. Transport — ACPI-WMI mailbox, 3. Thermal / fan — `a1 = 0x0200`, 4. LED — `a1 = 0x0100`, 6. Safety rules, 7. Not supported, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`) (+6 more)

### Community 95 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 96 - ".Set"
Cohesion: 0.30
Nodes (7): PowerThrottlingState, DllImport, IntPtr, EfficiencyMode, PowerThrottlingState, Fact, EfficiencyModeTests

### Community 97 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 98 - "ThemeService"
Cohesion: 0.14
Nodes (11): ThemeDark, ThemeLight, ThemeSystem, Theme, ThemePreference, Dark, Light, System (+3 more)

### Community 99 - "LedEffect"
Cohesion: 0.14
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 100 - ".RunAndRecord"
Cohesion: 0.22
Nodes (6): JsonSerializerOptions, MachineScan, Firmware, RecordPath, Fact, MachineScanTests

### Community 101 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 102 - ".Record"
Cohesion: 0.20
Nodes (7): IReadOnlyDictionary, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Fact, AuditLogTests

### Community 103 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 104 - "microsoft_win32"
Cohesion: 0.15
Nodes (5): microsoft_win32, Theme, Dark, Light, system_security

### Community 105 - "AcDc"
Cohesion: 0.26
Nodes (5): AcDc, InlineData, Theory, Fact, IdleTimeoutsTests

### Community 106 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 107 - "LedBrightness"
Cohesion: 0.18
Nodes (8): LedBrightness, Full, Half, Off, Fact, InlineData, Theory, BacklightKeyTests

### Community 108 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 109 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 110 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 111 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 112 - "Nextcalibur 0.5.4"
Cohesion: 0.17
Nodes (11): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+3 more)

### Community 113 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 114 - "StackPanel"
Cohesion: 0.17
Nodes (12): BannerActions, CeilingRows, DefinitionRows, DialogActions, DriveTextStack, FnShortcutRows, IdleLoadRows, IdleRows (+4 more)

### Community 115 - "PackageTemperatureTests"
Cohesion: 0.41
Nodes (4): Fact, InlineData, Theory, PackageTemperatureTests

### Community 116 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 117 - "NvidiaDriverState"
Cohesion: 0.27
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 118 - "IntPtr"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 119 - ".ToHsv"
Cohesion: 0.22
Nodes (6): DependencyPropertyChangedEventArgs, Hue, Saturation, Color, DependencyObject, Value

### Community 120 - "NoticeGate"
Cohesion: 0.22
Nodes (7): Key, Show, Action, List, NoticeGate, ShouldWait, WaitingCount

### Community 121 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 122 - ".Sanitised"
Cohesion: 0.33
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 123 - "SupportVerdict"
Cohesion: 0.22
Nodes (9): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites (+1 more)

### Community 124 - "AppIdentity.cs"
Cohesion: 0.22
Nodes (6): Nextcalibur.Core, system_globalization, system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 125 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 126 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 127 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 128 - "BacklightKeyWatcher"
Cohesion: 0.22
Nodes (4): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo

### Community 129 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 130 - ".Parse"
Cohesion: 0.33
Nodes (3): List, Fact, FirmwareInventoryTests

### Community 131 - ".IsSystemFileSignedBy"
Cohesion: 0.39
Nodes (3): Fact, SystemFileTrustTests, Cmd

### Community 132 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 133 - "CeilingTests"
Cohesion: 0.31
Nodes (5): Baseline, Fact, InlineData, Theory, CeilingTests

### Community 134 - ".ArrangeOverride"
Cohesion: 0.25
Nodes (3): BitmapSource, Size, Rect

### Community 135 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 136 - "Nextcalibur 0.5.5"
Cohesion: 0.25
Nodes (6): Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture

### Community 137 - "ThermalSample"
Cohesion: 0.39
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 138 - "CoreLoad"
Cohesion: 0.25
Nodes (3): CoreLoad, Count, ProcessorPerformance

### Community 140 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 141 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 142 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 143 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 144 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 145 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 146 - ".LoadPowerModes"
Cohesion: 0.29
Nodes (5): Option, Button, IEnumerable, Text, TextBlock

### Community 147 - ".ForegroundProcess"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Name, Pid

### Community 148 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 149 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 150 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 151 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 152 - "Nextcalibur 0.7.0"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.7.0, Tested on, Updated

### Community 153 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 154 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 155 - "ComboBox"
Cohesion: 0.33
Nodes (6): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, SettingNvPower, ComboBox

### Community 156 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 157 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 158 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 160 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 161 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 162 - "Nextcalibur 0.7.1"
Cohesion: 0.40
Nodes (4): Fixed, New, Nextcalibur 0.7.1, Tested on

### Community 163 - "Nextcalibur 0.7.2"
Cohesion: 0.40
Nodes (4): Fixed, New, Nextcalibur 0.7.2, Tested on

### Community 164 - "SmiSubsystem"
Cohesion: 0.40
Nodes (5): SmiSubsystem, DisplayMode, Led, Profile, Thermal

### Community 167 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 169 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 170 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 171 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 172 - "2. Command structure"
Cohesion: 0.50
Nodes (4): 2. Command structure, Call sequence, Command families (`a0`), Subsystems (`a1`)

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

### Community 177 - ".Modules_are_the_released_ones"
Cohesion: 0.50
Nodes (3): InlineData, Theory, PawnIoModuleTests

### Community 178 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 181 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 182 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

### Community 183 - "EffectPanel"
Cohesion: 0.67
Nodes (3): EffectPanel, ProfileRow, UniformGrid

### Community 184 - "PART_ContentHost"
Cohesion: 0.67
Nodes (3): PART_ContentHost, SettingsScroll, ScrollViewer

### Community 186 - "Words"
Cohesion: 0.67
Nodes (3): Func, Words, Resolver

## Knowledge Gaps
- **613 isolated node(s):** `OverlayRepairPending`, `Accepted`, `SelectedColour`, `VisualChildrenCount`, `Diameter` (+608 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1185 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **31 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `BacklightKeyWatcher`, `AppSettings`, `GpuClockReader`, `FnShortcut`, `.Get`, `.OnLoaded`, `RunawayWatchTests`, `Nextcalibur 0.5.5`, `ThermalSample`, `ProcessPresence`, `CoreLoad`, `WindowsFaults`, `ProgramRule`, `Nextcalibur.App`, `Window`, `PlanTuning`, `RadioButton`, `CoolingPolicyTests`, `.ForegroundProcess`, `ResourceDictionary`, `TourPage`, `.LoadPowerModes`, `.RequestRestart`, `UserPresence`, `.RuleRow`, `.For`, `.OnBrightnessChanged`, `Button`, `SystemInfo`, `LedZone`, `Nextcalibur.Core.Configuration`, `Localized`, `.RefreshIdleRows`, `.Choose`, `.Get`, `system_runtime_interopservices`, `SystemModeService`, `CeilingTestWindow`, `Integrity`, `SystemMode`, `.LoadDefinitionsAsync`, `EcMailbox`, `PowerOverlayService`, `.ShowSaving`, `Timeouts`, `TrayPresence`, `.OnSystemModeChanged`, `.PlaceTourStep`, `.Info`, `CpuPowerReader`, `DependencyStatus`, `TemperatureFusion`, `UpdateService`, `.WireSettingsPage`, `LedController`, `BatteryModePolicy`, `.OnClosing`, `ThemeService`, `LedEffect`, `AcDc`, `Grid`, `LedBrightness`, `Strings`, `NvidiaDriverState`, `SupportVerdict`?**
  _High betweenness centrality (0.512) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Text`, `RadioButton`, `.OnBrightnessChanged`, `ComboBox`, `Button`, `Keycaps`, `DriveGauge`, `CpuWarnValue`, `DialogCheck`, `EffectPanel`, `PART_ContentHost`, `.OnSystemModeChanged`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `TourCanvas`, `Wheel`, `ToggleButton`, `ThemeService`, `Grid`, `StackPanel`, `Border`?**
  _High betweenness centrality (0.078) - this node is a cross-community bridge._
- **Why does `Nextcalibur.Core.Hardware` connect `Nextcalibur.Core.Hardware` to `BacklightKeyWatcher`, `GpuClockReader`, `RunawayWatchTests`, `ThermalSample`, `CoreLoad`, `WindowsFaults`, `Nextcalibur.App`, `UserPresence`, `CpuLimits`, `MachineWideGate`, `Nextcalibur.Core.Configuration`, `Api`, `.Get`, `system_runtime_interopservices`, `SystemMode`, `.LoadDefinitionsAsync`, `FirmwareInventory`, `EcMailbox`, `DevicePowerState`, `system_text`, `microsoft_win32`, `LedBrightness`, `NvidiaDriverState`, `SupportVerdict`?**
  _High betweenness centrality (0.060) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `OverlayRepairPending`, `Accepted`, `SelectedColour` to the rest of the system?**
  _613 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.02227307398932113 - nodes in this community are weakly interconnected._
- **Should `AppSettings` be split into smaller, more focused modules?**
  _Cohesion score 0.02913659449345095 - nodes in this community are weakly interconnected._