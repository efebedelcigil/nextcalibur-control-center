# Graph Report - nextcalibur-control-center  (2026-10-14)

## Corpus Check
- 202 files · ~250,454 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3438 nodes · 7612 edges · 195 communities (160 shown, 35 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 462 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `fc030d49`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- GpuClockReader
- Authenticode
- .Info
- .RefreshIdleRows
- AppSettings
- PowerOverlayService
- RunawayWatchTests
- SystemMode
- Nextcalibur.Core.Configuration
- ProtectedStore
- .Get
- Window
- ProgramRule
- IShellLinkW
- TrayPresence
- Nextcalibur.Core.Hardware
- WindowsFaults
- MachineScanRecord
- RadioButton
- .ApplyFnShortcuts
- .RequestRestart
- SystemInfo
- NetworkCostTests
- system_runtime_interopservices
- .Main
- Button
- UpdateService
- UserPresence
- .Check
- .RemoveMachineTraces
- WindowsFaultsTests
- SystemTools
- .Get
- ProcessPresence
- ResourceDictionary
- PlanTuning
- Fact
- Api
- CoolingPolicyTests
- Program
- FirmwareInventory
- Nextcalibur.Core.Security
- .For
- Dependency
- .Survey
- CeilingTestWindow
- EcMailbox
- Fact
- .Ask
- Integrity
- .Read
- DotNetRuntimeDependency
- Nextcalibur.Core.csproj
- AcDc
- ToggleButton
- BatteryModePolicy
- RestorePoint
- BacklightKeyWatcher
- MemoryTrimmer
- DependencyStatus
- CpuPowerReader
- CpuLimits
- analyse-autopsy.py
- .OnEffectChecked
- .CheckProgram
- TemperatureFusion
- .ModeName
- .GetColour
- RuntimeDependencyTests
- .Scan
- microsoft_win32
- LedController
- .Dictionary
- ColourWheel
- .LoadDefinitionsAsync
- LedState
- CpuLimitsTests
- FanGauge
- Unelevated
- DonutGauge
- .WireSettingsPage
- SegmentedBar
- Nextcalibur Control Center
- FnShortcuts
- .Warn
- .CheckShape
- LogInjectionTests
- .FromBytes
- GameSession
- CoreLoad
- .ShowResult
- SetBy
- Grid
- ThemeService
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- .MarkedPlans
- Strings
- StackPanel
- InstallFolderGuard
- .IsUnderProgramFiles
- PackageTemperatureTests
- SettingsTests.cs
- Hardware Protocol
- Nextcalibur 0.5.3
- Log
- .Sanitised
- HotkeyModifiers
- CeilingTestTests
- Border
- .Parse
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- PawnIoDependency
- Probe
- CeilingTests
- Nextcalibur 0.5.1
- LedEffect
- Preflight
- NotificationState
- IntegrityKind
- Test-Ui.ps1
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- .Pick
- .ForegroundProcess
- TourPage
- Log-Graphics.ps1
- Text
- Privacy
- Nextcalibur 0.6.2
- Security
- ComboBox
- LedZone
- MachineWideGate
- PowerSource
- Notice
- Nextcalibur 0.5.11
- .Set
- .ParseMarker
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- Nextcalibur 0.6.1
- .ArrangeOverride
- .TryParseRgb
- ThemePreference
- .Modules_are_the_released_ones
- Tools
- 0.5.7.md
- CpuWarnValue
- DialogCheck
- .ChooseTourCardPosition
- Toasts.cs
- .Driver_paths_are_read_in_every_form_Windows_writes_them
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
- .ReadManifest

## God Nodes (most connected - your core abstractions)
1. `MainWindow` - 400 edges
2. `Window` - 189 edges
3. `AppSettings` - 85 edges
4. `Nextcalibur.Core.Hardware` - 70 edges
5. `SystemMode` - 66 edges
6. `Nextcalibur.Core.Power` - 43 edges
7. `Nextcalibur.Core.Configuration` - 42 edges
8. `RadioButton` - 41 edges
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

## Communities (195 total, 35 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.03
Nodes (73): DeferredOverheat, HwndSource, ObservableCollection, PluggedIn, Queue, SizeChangedEventArgs, MainWindow, ComboBox (+65 more)

### Community 1 - "GpuClockReader"
Cohesion: 0.06
Nodes (27): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+19 more)

### Community 2 - "Authenticode"
Cohesion: 0.06
Nodes (38): CatalogInfo, Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on (+30 more)

### Community 3 - ".Info"
Cohesion: 0.07
Nodes (17): Asynchronous dispatcher & UI thread hardening, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary, IReadOnlyDictionary (+9 more)

### Community 4 - ".RefreshIdleRows"
Cohesion: 0.06
Nodes (28): ComboBox, Grid, Guid, TextBlock, ToggleButton, DllImport, IntPtr, ScreenSaver (+20 more)

### Community 5 - "AppSettings"
Cohesion: 0.04
Nodes (54): DateTime, Dictionary, FnShortcut, JsonElement, List, ProgramRule, AppSettings, AcceptedVendorSoftware (+46 more)

### Community 6 - "PowerOverlayService"
Cohesion: 0.06
Nodes (28): DateTime, IReadOnlyList, MachineHistory, DllImport, Guid, IReadOnlyList, OverlayDiagnosis, GuardMissing (+20 more)

### Community 7 - "RunawayWatchTests"
Cohesion: 0.06
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 8 - "SystemMode"
Cohesion: 0.08
Nodes (20): PowerModeChangedEventArgs, Guid, Ours, Plan, Dictionary, DllImport, Guid, IntPtr (+12 more)

### Community 9 - "Nextcalibur.Core.Configuration"
Cohesion: 0.09
Nodes (27): Nextcalibur.App, Nextcalibur.App.Controls, Nextcalibur.Core, Nextcalibur.Core.Configuration, system_componentmodel, system_drawing, system_globalization, system_linq (+19 more)

### Community 10 - "ProtectedStore"
Cohesion: 0.09
Nodes (20): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+12 more)

### Community 11 - ".Get"
Cohesion: 0.07
Nodes (11): GameEnd, SolidColorBrush, IReadOnlyList, GameEnd, OpenLogButton, Guid, Ours, Plan (+3 more)

### Community 12 - "Window"
Cohesion: 0.07
Nodes (47): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+39 more)

### Community 13 - "ProgramRule"
Cohesion: 0.08
Nodes (25): GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, IEnumerable (+17 more)

### Community 14 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 15 - "TrayPresence"
Cohesion: 0.07
Nodes (21): ContextMenuStrip, DevPropKey, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, EventHandler (+13 more)

### Community 16 - "Nextcalibur.Core.Hardware"
Cohesion: 0.11
Nodes (7): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, Nextcalibur.Core.Hardware, nextcalibur_core_power_plantuning_acdc, system_text_regularexpressions, system_xml_linq, xunit

### Community 17 - "WindowsFaults"
Cohesion: 0.08
Nodes (33): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+25 more)

### Community 18 - "MachineScanRecord"
Cohesion: 0.06
Nodes (36): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites (+28 more)

### Community 19 - "RadioButton"
Cohesion: 0.07
Nodes (36): ModeBalanced, ModeDiscrete, ModeGaming, ModeHybrid, ModeOffice, ModePerformance, ModeUma, NavDisplay (+28 more)

### Community 20 - ".ApplyFnShortcuts"
Cohesion: 0.07
Nodes (22): Problem, Detail, Dictionary, DllImport, IntPtr, FnAction, FanTurbo, KeyboardBacklight (+14 more)

### Community 21 - ".RequestRestart"
Cohesion: 0.08
Nodes (15): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, Option, Button, DllImport (+7 more)

### Community 22 - "SystemInfo"
Cohesion: 0.08
Nodes (21): MemoryStatusEx, DriveUse, INotifyPropertyChanged, DriveRow, Name, Percent, PercentText, DllImport (+13 more)

### Community 23 - "NetworkCostTests"
Cohesion: 0.11
Nodes (26): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+18 more)

### Community 24 - "system_runtime_interopservices"
Cohesion: 0.07
Nodes (11): Nextcalibur.Core.Input, microsoft_win32_safehandles, system_diagnostics, system_io_directory, system_io_ioexception, system_io_path, system_management, system_reflection (+3 more)

### Community 25 - ".Main"
Cohesion: 0.10
Nodes (9): The threat model, and what the code does about it, Application, DllImport, Mutex, App, OverlayRepairPending, RegistryKey, Footprint (+1 more)

### Community 26 - "Button"
Cohesion: 0.07
Nodes (26): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, HideToTrayButton (+18 more)

### Community 27 - "UpdateService"
Cohesion: 0.07
Nodes (16): CancelEventArgs, Slider, EventArgs, DateTime, DispatcherTimer, HashSet, Task, TimeSpan (+8 more)

### Community 28 - "UserPresence"
Cohesion: 0.13
Nodes (12): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr, MarshalAs (+4 more)

### Community 29 - ".Check"
Cohesion: 0.11
Nodes (13): CommonAce, RawSecurityDescriptor, RegistryKey, SecurityIdentifier, MailboxAccess, MailboxAvailability, AccessNotGranted, Available (+5 more)

### Community 30 - ".RemoveMachineTraces"
Cohesion: 0.12
Nodes (7): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, Fact, TaskFolderTests

### Community 31 - "WindowsFaultsTests"
Cohesion: 0.17
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 32 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 33 - ".Get"
Cohesion: 0.13
Nodes (13): ArgumentNullException, GpuLoad, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma, GpuModeService (+5 more)

### Community 34 - "ProcessPresence"
Cohesion: 0.12
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 35 - "ResourceDictionary"
Cohesion: 0.08
Nodes (29): ActualWidth, IsDropDownOpen, Arrow, Bg, CardBorder, Indicator, Knob, PART_EditableTextBox (+21 more)

### Community 36 - "PlanTuning"
Cohesion: 0.19
Nodes (10): Action, AcDc, DllImport, Guid, IEnumerable, IntPtr, Setting, PlanTuning (+2 more)

### Community 37 - "Fact"
Cohesion: 0.11
Nodes (9): Limits, SystemMode, InlineData, Theory, Fact, ForwardCompatibilityTests, MailboxAccessTests, SettingsTests (+1 more)

### Community 38 - "Api"
Cohesion: 0.11
Nodes (23): Api, GetSettingFn, InitializeFn, ProfileOutFn, SessionFn, SessionOutFn, Set, SetSettingFn (+15 more)

### Community 39 - "CoolingPolicyTests"
Cohesion: 0.12
Nodes (16): At, Margin, DateTime, List, TimeSpan, CoolingPolicy, DegreesPerMinute, Engaged (+8 more)

### Community 40 - "Program"
Cohesion: 0.13
Nodes (6): ConsoleColor, Program, HardwareSupport, DateTimeOffset, ThermalReader, ThermalSample

### Community 41 - "FirmwareInventory"
Cohesion: 0.08
Nodes (24): DllImport, IntPtr, IReadOnlyList, FirmwareInventory, BatteryChemistry, BatteryDesignMwh, BatteryName, BiosDate (+16 more)

### Community 42 - "Nextcalibur.Core.Security"
Cohesion: 0.14
Nodes (9): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_security_cryptography_x509certificates, system_text_json (+1 more)

### Community 43 - ".For"
Cohesion: 0.15
Nodes (14): AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, Baseline (+6 more)

### Community 44 - "Dependency"
Cohesion: 0.11
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 45 - ".Survey"
Cohesion: 0.17
Nodes (7): Func, IEnumerable, IReadOnlyList, RetirementEntry, Trace, Version, FootprintTests

### Community 46 - "CeilingTestWindow"
Cohesion: 0.15
Nodes (10): CancellationTokenSource, Button, CancellationToken, CheckBox, Func, StackPanel, Task, TextBlock (+2 more)

### Community 47 - "EcMailbox"
Cohesion: 0.16
Nodes (11): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+3 more)

### Community 48 - "Fact"
Cohesion: 0.17
Nodes (7): IList, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 49 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 50 - "Integrity"
Cohesion: 0.14
Nodes (8): Lazy, ProcessModule, HashSet, IReadOnlyList, Integrity, NvapiPath, NvmlPath, IntegrityFinding

### Community 51 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 52 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 53 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 54 - "AcDc"
Cohesion: 0.20
Nodes (8): MemberData, AcDc, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 55 - "ToggleButton"
Cohesion: 0.10
Nodes (20): OverheatWarningToggle, SelectAll, SettingAskGameMode, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost (+12 more)

### Community 56 - "BatteryModePolicy"
Cohesion: 0.22
Nodes (7): BatteryModePolicy, QuietMode, Remembered, Fact, BatteryModePolicyTests, Fact, SystemModesTests

### Community 57 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 58 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 59 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 60 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 61 - "CpuPowerReader"
Cohesion: 0.24
Nodes (5): DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 62 - "CpuLimits"
Cohesion: 0.13
Nodes (13): Bit, IReadOnlySet, Now, PowerInfo, PowerLimits, Ratio, SinceBoot, IReadOnlyList (+5 more)

### Community 63 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 64 - ".OnEffectChecked"
Cohesion: 0.12
Nodes (13): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic (+5 more)

### Community 65 - ".CheckProgram"
Cohesion: 0.22
Nodes (5): Fact, IntegrityTests, Current, Exe, Manifest

### Community 66 - "TemperatureFusion"
Cohesion: 0.25
Nodes (9): DateTime, TimeSpan, TemperatureFusion, Offset, RegisterSetAside, Value, DateTime, Fact (+1 more)

### Community 67 - ".ModeName"
Cohesion: 0.22
Nodes (3): AssemblyInformationalVersionAttribute, List, ModeChoice

### Community 68 - ".GetColour"
Cohesion: 0.22
Nodes (7): B, G, R, Fact, InlineData, Theory, LedStateTests

### Community 69 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 70 - ".Scan"
Cohesion: 0.18
Nodes (10): Display, IoCounters, ProcessLook, Dictionary, DllImport, IntPtr, TimeSpan, IoCounters (+2 more)

### Community 71 - "microsoft_win32"
Cohesion: 0.12
Nodes (8): InvalidOperationException, microsoft_win32, Theme, Dark, Light, UnsafeModeException, system_io, system_security

### Community 72 - "LedController"
Cohesion: 0.24
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 73 - ".Dictionary"
Cohesion: 0.16
Nodes (7): IReadOnlyDictionary, IReadOnlyList, Stable, Fact, AuditLogTests, Dictionary, TopSharePercent

### Community 74 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 75 - ".LoadDefinitionsAsync"
Cohesion: 0.24
Nodes (6): Task, GpuPowerLimits, IEnumerable, PowerDefinitions, Fact, PowerDefinitionsTests

### Community 76 - "LedState"
Cohesion: 0.13
Nodes (15): Dictionary, LedBrightness, LedProfile, BrightnessPercent, Colours, Effect, LedState, ActiveProfile (+7 more)

### Community 77 - "CpuLimitsTests"
Cohesion: 0.18
Nodes (6): ArgumentOutOfRangeException, Offset, Fact, CpuLimitsTests, ThermFlags, TjMax

### Community 78 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 79 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 80 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 82 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 83 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 84 - "FnShortcuts"
Cohesion: 0.32
Nodes (4): DllImport, IntPtr, StringBuilder, FnShortcuts

### Community 85 - ".Warn"
Cohesion: 0.20
Nodes (3): MessageBoxButton, MessageBoxResult, NduFix

### Community 86 - ".CheckShape"
Cohesion: 0.29
Nodes (4): IEnumerable, List, Fact, FnShortcutsTests

### Community 87 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 88 - ".FromBytes"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 89 - "GameSession"
Cohesion: 0.15
Nodes (11): GameSession, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc, Looks (+3 more)

### Community 90 - "CoreLoad"
Cohesion: 0.17
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 91 - ".ShowResult"
Cohesion: 0.27
Nodes (5): List, IEnumerable, TimeSpan, CeilingStep, CeilingTest

### Community 92 - "SetBy"
Cohesion: 0.18
Nodes (9): Grid, PowerDefinition, SetBy, Intel, LaptopMaker, Nextcalibur, Nvidia, Windows (+1 more)

### Community 93 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 94 - "ThemeService"
Cohesion: 0.17
Nodes (5): Color, Theme, ThemeService, Preference, Resolved

### Community 95 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 96 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 97 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 98 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 99 - ".MarkedPlans"
Cohesion: 0.18
Nodes (7): Floor, Marker, Original, IReadOnlyList, List, Name, Plan

### Community 100 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 101 - "StackPanel"
Cohesion: 0.17
Nodes (12): BannerActions, CeilingRows, DefinitionRows, DialogActions, DriveTextStack, FnShortcutRows, IdleRows, PanelWindowsFaultsSubOptions (+4 more)

### Community 102 - "InstallFolderGuard"
Cohesion: 0.35
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 103 - ".IsUnderProgramFiles"
Cohesion: 0.26
Nodes (5): InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 104 - "PackageTemperatureTests"
Cohesion: 0.41
Nodes (4): Fact, InlineData, Theory, PackageTemperatureTests

### Community 105 - "SettingsTests.cs"
Cohesion: 0.29
Nodes (4): Nextcalibur.Cli, system_security_accesscontrol, system_security_cryptography, system_security_principal

### Community 106 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 107 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 108 - "Log"
Cohesion: 0.29
Nodes (3): Exception, Log, Folder

### Community 109 - ".Sanitised"
Cohesion: 0.29
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 110 - "HotkeyModifiers"
Cohesion: 0.22
Nodes (8): HotkeyModifiers, Alt, Control, None, Shift, Windows, InlineData, Theory

### Community 111 - "CeilingTestTests"
Cohesion: 0.25
Nodes (5): IReadOnlyList, Fact, InlineData, Theory, CeilingTestTests

### Community 112 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 113 - ".Parse"
Cohesion: 0.29
Nodes (4): List, MemoryModule, Fact, FirmwareInventoryTests

### Community 114 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 115 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 116 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 117 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 118 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 119 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 121 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 122 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 123 - "CeilingTests"
Cohesion: 0.31
Nodes (5): Baseline, Fact, InlineData, Theory, CeilingTests

### Community 124 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 125 - "LedEffect"
Cohesion: 0.25
Nodes (8): LedEffect, Blink, Breathing, ColourCycle, Heartbeat, Off, Static, Wave

### Community 126 - "Preflight"
Cohesion: 0.25
Nodes (8): IReadOnlyList, Preflight, RunningItem, RunningKind, App, Background, Service, Transfer

### Community 127 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 128 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 129 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 130 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

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

### Community 135 - ".ForegroundProcess"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Name, Pid

### Community 136 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 137 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 138 - "Text"
Cohesion: 0.47
Nodes (6): RpmToDoubleConverter, Text, CpuFanGauge, GpuFanGauge, GpuName, FanGauge

### Community 139 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 140 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 141 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 142 - "ComboBox"
Cohesion: 0.33
Nodes (6): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, SettingNvPower, ComboBox

### Community 143 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 144 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 145 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 146 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 147 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 150 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 152 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 153 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 154 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 155 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 156 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 157 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 158 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 160 - ".TryParseRgb"
Cohesion: 0.50
Nodes (3): B, G, R

### Community 161 - "ThemePreference"
Cohesion: 0.50
Nodes (4): ThemePreference, Dark, Light, System

### Community 162 - ".Modules_are_the_released_ones"
Cohesion: 0.50
Nodes (3): InlineData, Theory, PawnIoModuleTests

### Community 163 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 166 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 167 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

## Knowledge Gaps
- **560 isolated node(s):** `OverlayRepairPending`, `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum` (+555 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1073 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **35 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `GpuClockReader`, `.Info`, `.RefreshIdleRows`, `AppSettings`, `PowerOverlayService`, `.ForegroundProcess`, `SystemMode`, `Nextcalibur.Core.Configuration`, `TourPage`, `.Get`, `RunawayWatchTests`, `ProgramRule`, `Window`, `TrayPresence`, `LedZone`, `MachineScanRecord`, `RadioButton`, `.ApplyFnShortcuts`, `.RequestRestart`, `SystemInfo`, `Button`, `UpdateService`, `UserPresence`, `.Get`, `ProcessPresence`, `PlanTuning`, `CoolingPolicyTests`, `.ChooseTourCardPosition`, `Program`, `.For`, `CeilingTestWindow`, `EcMailbox`, `Integrity`, `.Read`, `AcDc`, `ToggleButton`, `BatteryModePolicy`, `BacklightKeyWatcher`, `DependencyStatus`, `CpuPowerReader`, `.OnEffectChecked`, `TemperatureFusion`, `.ModeName`, `LedController`, `.LoadDefinitionsAsync`, `.WireSettingsPage`, `FnShortcuts`, `.Warn`, `GameSession`, `SetBy`, `Grid`, `ThemeService`, `Strings`, `Log`, `.SetCeiling`, `LedEffect`?**
  _High betweenness centrality (0.513) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Text`, `.Get`, `ComboBox`, `RadioButton`, `Keycaps`, `DriveGauge`, `Button`, `CpuWarnValue`, `DialogCheck`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `ToggleButton`, `.OnEffectChecked`, `Grid`, `StackPanel`, `Border`?**
  _High betweenness centrality (0.106) - this node is a cross-community bridge._
- **Why does `Nextcalibur.Core.Hardware` connect `Nextcalibur.Core.Hardware` to `.Get`, `microsoft_win32`, `Program`, `Nextcalibur.Core.Configuration`, `Nextcalibur.Core.Security`, `SettingsTests.cs`, `RunawayWatchTests`, `MachineWideGate`, `MachineScanRecord`, `system_runtime_interopservices`, `BacklightKeyWatcher`, `SetBy`, `CpuLimits`?**
  _High betweenness centrality (0.093) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `OverlayRepairPending`, `SelectedColour`, `VisualChildrenCount` to the rest of the system?**
  _560 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.025952203136669155 - nodes in this community are weakly interconnected._
- **Should `GpuClockReader` be split into smaller, more focused modules?**
  _Cohesion score 0.0593607305936073 - nodes in this community are weakly interconnected._