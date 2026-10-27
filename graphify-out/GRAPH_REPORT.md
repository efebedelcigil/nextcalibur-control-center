# Graph Report - nextcalibur-control-center  (2026-10-27)

## Corpus Check
- 221 files · ~268,912 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3791 nodes · 8438 edges · 209 communities (176 shown, 33 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 501 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `ae673b7f`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- AppSettings
- FnShortcut
- .OnLoaded
- ProtectedStore
- ProgramRule
- GameLibrary
- .Get
- RunawayWatchTests
- UpdateService
- Window
- Nextcalibur.App
- xunit
- WindowsFaults
- IShellLinkW
- EcMailbox
- RadioButton
- Nextcalibur.Core.Hardware
- LedState
- PlanTuning
- Fact
- .RuleRow
- CoolingPolicyTests
- NetworkCostTests
- GpuClockReader
- .Main
- .Info
- Fact
- Localized
- Nextcalibur.Core.Configuration
- SystemInfo
- UserPresence
- WindowsFaultsTests
- Button
- CpuLimits
- CrashGuard
- LedController
- InstallFolderGuard
- Api
- ColourWheel
- Program
- .RequestRestart
- ResourceDictionary
- .TunePlansAtStart
- Dependency
- .LoadDefinitionsAsync
- TestPreflight
- Integrity
- .For
- Fact
- .SignatureIsValid
- CeilingTestWindow
- Elevation
- SystemModeService
- Nextcalibur.Core.Security
- SystemMode
- .ShowSaving
- .Get
- CpuPowerReader
- Authenticode
- .Check
- .OnSystemModeChanged
- ToggleButton
- FirmwareInventory
- .PlaceTourStep
- DotNetRuntimeDependency
- MachineScanRecord
- Nextcalibur.Core.csproj
- BatteryModePolicy
- RestorePoint
- .Ask
- MemoryTrimmer
- DependencyStatus
- .ShowResult
- .Dictionary
- analyse-autopsy.py
- TrayPresence
- BacklightKeyWatcher
- ModeProfileTests
- DarkMenuColours
- .CheckProgram
- .CheckAwakeFault
- ProcessPresence
- TemperatureFusion
- PowerOverlayService
- RuntimeDependencyTests
- CoreLoad
- .Choose
- Unelevated
- .ApplyFnShortcuts
- FanGauge
- DevicePowerState
- DonutGauge
- MachineHistory
- SegmentedBar
- .Set
- Nextcalibur Control Center
- Log
- .RunAndRecord
- .HasAccess
- .Write
- LogInjectionTests
- Grid
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- .Pick
- Strings
- StackPanel
- PackageTemperatureTests
- AcDc
- Hardware Protocol
- Nextcalibur 0.5.3
- ThemeService
- VendorSoftware
- NoticeGate
- Border
- NvidiaDriverState
- .Parse
- SupportVerdict
- IntPtr
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- PawnIoDependency
- .IsSystemFileSignedBy
- Probe
- CeilingTests
- Nextcalibur 0.5.1
- Toasts
- .Run
- NduFix
- NotificationState
- IntegrityKind
- Test-Ui.ps1
- Text
- .BuildMenu
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- TourPage
- .Current
- .Values_are_the_ones_measured_from_the_vendor
- Log-Graphics.ps1
- Privacy
- Nextcalibur 0.5.5
- Nextcalibur 0.6.2
- Nextcalibur 0.7.0
- Security
- .OnBrightnessChanged
- ComboBox
- MachineWideGate
- PowerSource
- Notice
- Nextcalibur 0.5.11
- Nextcalibur 0.7.1
- Nextcalibur 0.7.2
- AppIdentity.cs
- PART_Track
- .Set
- SmiSubsystem
- .Read
- .FirmwareChanged
- RunningKind
- .A_polling_interval_from_the_file_is_a_sane_one
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Strings.cs
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- Nextcalibur 0.6.1
- .Modules_are_the_released_ones
- Tools
- Toggle
- 0.5.7.md
- CpuWarnValue
- DialogCheck
- EffectPanel
- PART_ContentHost
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
1. `MainWindow` - 451 edges
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

## Communities (209 total, 33 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.02
Nodes (85): Added, DeferredOverheat, Found, GameSession, HwndSource, ObservableCollection, PluggedIn, Queue (+77 more)

### Community 1 - "AppSettings"
Cohesion: 0.03
Nodes (68): DateTime, Dictionary, FnShortcut, JsonElement, Limits, List, ProgramRule, SystemMode (+60 more)

### Community 2 - "FnShortcut"
Cohesion: 0.05
Nodes (42): INotifyPropertyChanged, Problem, DriveRow, Detail, Name, Percent, PercentText, Dictionary (+34 more)

### Community 3 - ".OnLoaded"
Cohesion: 0.06
Nodes (15): Asynchronous dispatcher & UI thread hardening, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary, IReadOnlyDictionary (+7 more)

### Community 4 - "ProtectedStore"
Cohesion: 0.07
Nodes (26): Content, DirectorySecurity, Hash, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Dictionary (+18 more)

### Community 5 - "ProgramRule"
Cohesion: 0.05
Nodes (37): HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, GeneratedRegex, IEnumerable (+29 more)

### Community 6 - "GameLibrary"
Cohesion: 0.07
Nodes (20): Folder, Dictionary, GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, IReadOnlyList, List (+12 more)

### Community 7 - ".Get"
Cohesion: 0.06
Nodes (9): GameEnd, SolidColorBrush, DllImport, IntPtr, Name, Pid, IReadOnlyList, GameEnd (+1 more)

### Community 8 - "RunawayWatchTests"
Cohesion: 0.07
Nodes (34): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+26 more)

### Community 9 - "UpdateService"
Cohesion: 0.06
Nodes (26): FileStream, IOException, DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService (+18 more)

### Community 10 - "Window"
Cohesion: 0.07
Nodes (48): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+40 more)

### Community 11 - "Nextcalibur.App"
Cohesion: 0.10
Nodes (25): Nextcalibur.App, Nextcalibur.App.Controls, system_componentmodel, system_drawing, system_io, system_linq, system_runtime_compilerservices, system_windows (+17 more)

### Community 12 - "xunit"
Cohesion: 0.09
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, nextcalibur_core_power_plantuning_acdc, system_text_regularexpressions, system_xml_linq, xunit

### Community 13 - "WindowsFaults"
Cohesion: 0.08
Nodes (34): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+26 more)

### Community 14 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 15 - "EcMailbox"
Cohesion: 0.09
Nodes (19): ArgumentException, Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox (+11 more)

### Community 16 - "RadioButton"
Cohesion: 0.06
Nodes (42): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeBalanced, ModeDiscrete (+34 more)

### Community 17 - "Nextcalibur.Core.Hardware"
Cohesion: 0.09
Nodes (9): Nextcalibur.Cli, Nextcalibur.Core.Input, Nextcalibur.Core.Hardware, microsoft_win32_safehandles, system_diagnostics, system_management, system_reflection, system_runtime_interopservices (+1 more)

### Community 18 - "LedState"
Cohesion: 0.09
Nodes (26): B, Dictionary, G, LedBrightness, R, LedProfile, BrightnessPercent, Colours (+18 more)

### Community 19 - "PlanTuning"
Cohesion: 0.13
Nodes (18): Floor, Marker, Original, Action, AcDc, DllImport, Guid, IEnumerable (+10 more)

### Community 20 - "Fact"
Cohesion: 0.10
Nodes (10): Func, IEnumerable, IReadOnlyList, Version, RetirementEntry, Trace, Fact, FootprintTests (+2 more)

### Community 21 - ".RuleRow"
Cohesion: 0.09
Nodes (8): AssemblyInformationalVersionAttribute, Task, FrameworkElement, Func, Grid, HashSet, List, ModeChoice

### Community 22 - "CoolingPolicyTests"
Cohesion: 0.08
Nodes (26): At, Margin, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc (+18 more)

### Community 23 - "NetworkCostTests"
Cohesion: 0.11
Nodes (26): Answer, Bytes, Count, HttpMessageHandler, CancellationToken, ConcurrentDictionary, HttpClient, Task (+18 more)

### Community 24 - "GpuClockReader"
Cohesion: 0.13
Nodes (11): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DllImport, IntPtr, CpuClockReader, BaseMhz, GpuClockReader (+3 more)

### Community 25 - ".Main"
Cohesion: 0.10
Nodes (10): The threat model, and what the code does about it, Application, DllImport, Mutex, App, OverlayRepairPending, DllImport, RegistryKey (+2 more)

### Community 26 - ".Info"
Cohesion: 0.09
Nodes (7): CancelEventArgs, Guid, Ours, Plan, MessageBoxButton, MessageBoxResult, IReadOnlyDictionary

### Community 27 - "Fact"
Cohesion: 0.12
Nodes (12): IList, GpuLoad, GpuConfiguration, HardwareSupport, DateTimeOffset, ThermalSample, Fact, GpuModeTests (+4 more)

### Community 28 - "Localized"
Cohesion: 0.09
Nodes (15): ItemsControl, Property, DependencyObject, DependencyProperty, Func, List, TextBlock, Value (+7 more)

### Community 29 - "Nextcalibur.Core.Configuration"
Cohesion: 0.09
Nodes (11): Nextcalibur.Core.Configuration, microsoft_win32, Theme, Dark, Light, system_security, system_security_accesscontrol, system_security_cryptography (+3 more)

### Community 30 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 31 - "UserPresence"
Cohesion: 0.13
Nodes (14): LastInputInfo, MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr (+6 more)

### Community 32 - "WindowsFaultsTests"
Cohesion: 0.17
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 33 - "Button"
Cohesion: 0.06
Nodes (30): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, GameScanButton (+22 more)

### Community 34 - "CpuLimits"
Cohesion: 0.09
Nodes (18): Bit, Now, Offset, PowerInfo, PowerLimits, Ratio, SinceBoot, IReadOnlyList (+10 more)

### Community 35 - "CrashGuard"
Cohesion: 0.11
Nodes (19): Session, DateTimeOffset, Exception, List, TimeSpan, CrashGuard, SafeMode, UncleanPrevious (+11 more)

### Community 36 - "LedController"
Cohesion: 0.11
Nodes (18): LedController, EffectiveBrightnessPercent, HardwareLevel, State, LedEffect, Blink, Breathing, ColourCycle (+10 more)

### Community 37 - "InstallFolderGuard"
Cohesion: 0.14
Nodes (10): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Fact, InlineData, Theory, ElevationPolicyTests (+2 more)

### Community 38 - "Api"
Cohesion: 0.11
Nodes (23): Api, GetSettingFn, InitializeFn, ProfileOutFn, SessionFn, SessionOutFn, Set, SetSettingFn (+15 more)

### Community 39 - "ColourWheel"
Cohesion: 0.10
Nodes (19): BitmapSource, Control, DependencyPropertyChangedEventArgs, Ellipse, Hue, Saturation, Canvas, Color (+11 more)

### Community 40 - "Program"
Cohesion: 0.12
Nodes (6): ConsoleColor, B, G, R, Program, ThermalReader

### Community 41 - ".RequestRestart"
Cohesion: 0.12
Nodes (10): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr, MarshalAs (+2 more)

### Community 42 - "ResourceDictionary"
Cohesion: 0.10
Nodes (27): ActualWidth, Bar, Bg, Blade, Box, CardBorder, Grip, Guard (+19 more)

### Community 43 - ".TunePlansAtStart"
Cohesion: 0.12
Nodes (9): Kept, ComboBox, Func, Grid, Guid, Guid, IReadOnlyList, Plan (+1 more)

### Community 44 - "Dependency"
Cohesion: 0.11
Nodes (18): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+10 more)

### Community 45 - ".LoadDefinitionsAsync"
Cohesion: 0.13
Nodes (15): Grid, Task, GpuPowerLimits, IEnumerable, PowerDefinition, PowerDefinitions, SetBy, Intel (+7 more)

### Community 46 - "TestPreflight"
Cohesion: 0.14
Nodes (13): Display, IoCounters, ProcessLook, ConcurrentDictionary, Dictionary, DllImport, IntPtr, TimeSpan (+5 more)

### Community 47 - "Integrity"
Cohesion: 0.11
Nodes (11): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvapiPath, NvmlPath (+3 more)

### Community 48 - ".For"
Cohesion: 0.15
Nodes (13): AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, Baseline (+5 more)

### Community 49 - "Fact"
Cohesion: 0.10
Nodes (15): IReadOnlyList, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact (+7 more)

### Community 50 - ".SignatureIsValid"
Cohesion: 0.15
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 51 - "CeilingTestWindow"
Cohesion: 0.12
Nodes (14): CancellationTokenSource, Button, CancellationToken, CheckBox, Func, List, StackPanel, Task (+6 more)

### Community 52 - "Elevation"
Cohesion: 0.14
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 53 - "SystemModeService"
Cohesion: 0.18
Nodes (8): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, SystemModeService, UnsafeModeException

### Community 54 - "Nextcalibur.Core.Security"
Cohesion: 0.14
Nodes (7): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_security_cryptography_x509certificates

### Community 55 - "SystemMode"
Cohesion: 0.11
Nodes (15): Min, IReadOnlyList, Ours, Plan, SystemMode, Balanced, Gaming, Office (+7 more)

### Community 56 - ".ShowSaving"
Cohesion: 0.17
Nodes (12): TextBlock, ToggleButton, AcDc, Guid, IEnumerable, DevicePowerSaving, Off, DeviceSaving (+4 more)

### Community 57 - ".Get"
Cohesion: 0.16
Nodes (12): ArgumentNullException, FirmwareModeReading, GpuMode, Discrete, Hybrid, Uma, GpuModeService, SwitchOutcome (+4 more)

### Community 58 - "CpuPowerReader"
Cohesion: 0.21
Nodes (6): ArgumentOutOfRangeException, DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 59 - "Authenticode"
Cohesion: 0.25
Nodes (14): CatalogInfo, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle, Authenticode (+6 more)

### Community 60 - ".Check"
Cohesion: 0.14
Nodes (9): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported, IdempotenceTests (+1 more)

### Community 61 - ".OnSystemModeChanged"
Cohesion: 0.15
Nodes (8): Option, PowerModeChangedEventArgs, Button, Guid, IEnumerable, Ours, Plan, TextBlock

### Community 62 - "ToggleButton"
Cohesion: 0.09
Nodes (23): LedPower, OverheatWarningToggle, SelectAll, SettingAskGameMode, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice (+15 more)

### Community 63 - "FirmwareInventory"
Cohesion: 0.09
Nodes (22): IReadOnlyList, FirmwareInventory, BatteryChemistry, BatteryDesignMwh, BatteryName, BiosDate, BiosRelease, BiosVendor (+14 more)

### Community 64 - ".PlaceTourStep"
Cohesion: 0.11
Nodes (8): SizeChangedEventArgs, KeyEventArgs, RoutedEventArgs, TourStep, Body, SectionName, Title, TourPage

### Community 65 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 66 - "MachineScanRecord"
Cohesion: 0.09
Nodes (22): DateTime, List, MachineScanRecord, BiosDate, BiosRelease, BiosVendor, BiosVersion, BoardProduct (+14 more)

### Community 67 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.11.0), System.Diagnostics.EventLog (10.0.14), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 68 - "BatteryModePolicy"
Cohesion: 0.22
Nodes (7): BatteryModePolicy, QuietMode, Remembered, Fact, BatteryModePolicyTests, Fact, SystemModesTests

### Community 69 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 70 - ".Ask"
Cohesion: 0.25
Nodes (10): HttpStatusCode, CancellationToken, Fact, HttpRequestMessage, HttpResponseMessage, InlineData, Task, Theory (+2 more)

### Community 71 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 72 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Ok, Task (+7 more)

### Community 73 - ".ShowResult"
Cohesion: 0.20
Nodes (7): IEnumerable, IReadOnlyList, TimeSpan, CeilingStep, CeilingTest, Fact, CeilingTestTests

### Community 74 - ".Dictionary"
Cohesion: 0.15
Nodes (9): IReadOnlyDictionary, Stable, Dictionary, Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests (+1 more)

### Community 75 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 76 - "TrayPresence"
Cohesion: 0.12
Nodes (10): Deep memory & resource leak eradication, EventHandler, Icon, Item, NotifyIcon, List, Ms, Window (+2 more)

### Community 77 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 78 - "ModeProfileTests"
Cohesion: 0.22
Nodes (7): MemberData, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 79 - "DarkMenuColours"
Cohesion: 0.11
Nodes (19): ProfessionalColorTable, Color, DarkMenuColours, CheckBackground, CheckPressedBackground, CheckSelectedBackground, ImageMarginGradientBegin, ImageMarginGradientEnd (+11 more)

### Community 80 - ".CheckProgram"
Cohesion: 0.22
Nodes (5): Fact, IntegrityTests, Current, Exe, Manifest

### Community 81 - ".CheckAwakeFault"
Cohesion: 0.13
Nodes (10): DateTime, Func, IReadOnlyList, TimeSpan, GpuAwakeFault, InlineData, Theory, GpuAwakeFaultConditionTests (+2 more)

### Community 82 - "ProcessPresence"
Cohesion: 0.23
Nodes (8): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, Fact, ProcessPresenceTests, Self

### Community 83 - "TemperatureFusion"
Cohesion: 0.25
Nodes (9): DateTime, TimeSpan, TemperatureFusion, Offset, RegisterSetAside, Value, DateTime, Fact (+1 more)

### Community 84 - "PowerOverlayService"
Cohesion: 0.23
Nodes (6): DllImport, Guid, PowerModeOption, PowerOverlays, All, PowerOverlayService

### Community 85 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 86 - "CoreLoad"
Cohesion: 0.15
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 87 - ".Choose"
Cohesion: 0.29
Nodes (6): IEnumerable, IReadOnlyList, IdleLoadPicker, RunningItem, Fact, IdleLoadPickerTests

### Community 88 - "Unelevated"
Cohesion: 0.33
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 90 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 91 - "DevicePowerState"
Cohesion: 0.28
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 92 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 93 - "MachineHistory"
Cohesion: 0.28
Nodes (6): DateTime, IReadOnlyList, MachineHistory, DateTime, Fact, MachineHistoryTests

### Community 94 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 95 - ".Set"
Cohesion: 0.30
Nodes (7): PowerThrottlingState, DllImport, IntPtr, EfficiencyMode, PowerThrottlingState, Fact, EfficiencyModeTests

### Community 96 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 97 - "Log"
Cohesion: 0.26
Nodes (3): Exception, Log, Folder

### Community 98 - ".RunAndRecord"
Cohesion: 0.22
Nodes (6): JsonSerializerOptions, MachineScan, Firmware, RecordPath, Fact, MachineScanTests

### Community 99 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 100 - ".Write"
Cohesion: 0.23
Nodes (7): AcDc, Guid, IReadOnlyList, IdleSetting, DisplayOff, Sleep, IdleTimeouts

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

### Community 107 - "Nextcalibur 0.5.4"
Cohesion: 0.17
Nodes (11): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+3 more)

### Community 108 - ".Pick"
Cohesion: 0.17
Nodes (7): MouseEventArgs, MouseButtonEventArgs, Size, Arrow, Point, Tick, Path

### Community 109 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 110 - "StackPanel"
Cohesion: 0.17
Nodes (12): BannerActions, CeilingRows, DefinitionRows, DialogActions, DriveTextStack, FnShortcutRows, IdleLoadRows, IdleRows (+4 more)

### Community 111 - "PackageTemperatureTests"
Cohesion: 0.41
Nodes (4): Fact, InlineData, Theory, PackageTemperatureTests

### Community 112 - "AcDc"
Cohesion: 0.29
Nodes (5): AcDc, InlineData, Theory, Fact, IdleTimeoutsTests

### Community 113 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 114 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 115 - "ThemeService"
Cohesion: 0.18
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 116 - "VendorSoftware"
Cohesion: 0.25
Nodes (4): DateTime, TimeSpan, VendorSoftware, UninstallCommand

### Community 117 - "NoticeGate"
Cohesion: 0.22
Nodes (7): Key, Show, Action, List, NoticeGate, ShouldWait, WaitingCount

### Community 118 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 119 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 120 - ".Parse"
Cohesion: 0.29
Nodes (4): List, MemoryModule, Fact, FirmwareInventoryTests

### Community 121 - "SupportVerdict"
Cohesion: 0.22
Nodes (9): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites (+1 more)

### Community 122 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 123 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 124 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 125 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 126 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

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

### Community 131 - "Toasts"
Cohesion: 0.32
Nodes (5): Action, Toasts, Balloon, windows_data_xml_dom, windows_ui_notifications

### Community 132 - ".Run"
Cohesion: 0.39
Nodes (4): Detail, Func, Ok, SelfTest

### Community 134 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 135 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 136 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 137 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 139 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 140 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 141 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 142 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 143 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 144 - ".Current"
Cohesion: 0.48
Nodes (3): DllImport, IntPtr, ScreenSaver

### Community 145 - ".Values_are_the_ones_measured_from_the_vendor"
Cohesion: 0.33
Nodes (4): Fact, InlineData, Theory, ThermalProfileTests

### Community 146 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 147 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 148 - "Nextcalibur 0.5.5"
Cohesion: 0.33
Nodes (5): Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture

### Community 149 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 150 - "Nextcalibur 0.7.0"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.7.0, Tested on, Updated

### Community 151 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 152 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 153 - "ComboBox"
Cohesion: 0.33
Nodes (6): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, SettingNvPower, ComboBox

### Community 154 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 155 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 156 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 157 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 158 - "Nextcalibur 0.7.1"
Cohesion: 0.40
Nodes (4): Fixed, New, Nextcalibur 0.7.1, Tested on

### Community 159 - "Nextcalibur 0.7.2"
Cohesion: 0.40
Nodes (4): Fixed, New, Nextcalibur 0.7.2, Tested on

### Community 160 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 161 - "PART_Track"
Cohesion: 0.40
Nodes (5): PART_Track, RootGrid, Sword, Grid, Track

### Community 163 - "SmiSubsystem"
Cohesion: 0.40
Nodes (5): SmiSubsystem, DisplayMode, Led, Profile, Thermal

### Community 166 - "RunningKind"
Cohesion: 0.40
Nodes (5): RunningKind, App, Background, Service, Transfer

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

### Community 178 - ".Modules_are_the_released_ones"
Cohesion: 0.50
Nodes (3): InlineData, Theory, PawnIoModuleTests

### Community 179 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 181 - "Toggle"
Cohesion: 0.67
Nodes (3): IsDropDownOpen, Toggle, ToggleButton

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
- **613 isolated node(s):** `OverlayRepairPending`, `Accepted`, `SelectedColour`, `VisualChildrenCount`, `Diameter` (+608 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1184 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **33 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `AppSettings`, `FnShortcut`, `.OnLoaded`, `ProgramRule`, `.Get`, `RunawayWatchTests`, `UpdateService`, `Window`, `Nextcalibur.App`, `TourPage`, `EcMailbox`, `RadioButton`, `Nextcalibur.Core.Hardware`, `PlanTuning`, `.RuleRow`, `CoolingPolicyTests`, `.OnBrightnessChanged`, `GpuClockReader`, `.Info`, `Fact`, `Localized`, `Nextcalibur.Core.Configuration`, `SystemInfo`, `UserPresence`, `Button`, `LedController`, `Program`, `.RequestRestart`, `.TunePlansAtStart`, `.LoadDefinitionsAsync`, `Integrity`, `.For`, `CeilingTestWindow`, `SystemModeService`, `Nextcalibur.Core.Security`, `SystemMode`, `.ShowSaving`, `.Get`, `CpuPowerReader`, `.OnSystemModeChanged`, `.PlaceTourStep`, `BatteryModePolicy`, `DependencyStatus`, `TrayPresence`, `BacklightKeyWatcher`, `TemperatureFusion`, `PowerOverlayService`, `CoreLoad`, `.Choose`, `.ApplyFnShortcuts`, `.Write`, `Grid`, `.Pick`, `Strings`, `ThemeService`, `NvidiaDriverState`, `SupportVerdict`?**
  _High betweenness centrality (0.496) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Text`, `RadioButton`, `.OnBrightnessChanged`, `ComboBox`, `Button`, `Keycaps`, `DriveGauge`, `CpuWarnValue`, `DialogCheck`, `EffectPanel`, `PART_ContentHost`, `ToggleButton`, `ColGauge`, `DriveDot`, `DriveList`, `DialogProgress`, `TourCanvas`, `Wheel`, `Grid`, `StackPanel`, `Border`?**
  _High betweenness centrality (0.082) - this node is a cross-community bridge._
- **Why does `Nextcalibur.Core.Hardware` connect `Nextcalibur.Core.Hardware` to `CpuLimits`, `Program`, `Nextcalibur.App`, `xunit`, `BacklightKeyWatcher`, `Nextcalibur.Core.Security`, `SupportVerdict`, `MachineWideGate`, `Nextcalibur.Core.Configuration`?**
  _High betweenness centrality (0.064) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `OverlayRepairPending`, `Accepted`, `SelectedColour` to the rest of the system?**
  _613 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.022354694485842028 - nodes in this community are weakly interconnected._
- **Should `AppSettings` be split into smaller, more focused modules?**
  _Cohesion score 0.02939166097060834 - nodes in this community are weakly interconnected._