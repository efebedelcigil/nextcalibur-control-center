# Graph Report - nextcalibur-control-center  (2026-10-14)

## Corpus Check
- 189 files · ~238,050 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3179 nodes · 7009 edges · 184 communities (154 shown, 30 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 403 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `e336b152`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- AppSettings
- .Get
- FnShortcut
- ProtectedStore
- GpuClockReader
- SystemMode
- RunawayWatchTests
- ProgramRule
- IShellLinkW
- RadioButton
- WindowsFaults
- Window
- TrayPresence
- WindowsFaultsTests
- Nextcalibur.App
- PlanTuning
- .CheckProgram
- .Survey
- CoolingPolicyTests
- Nextcalibur.Core.Hardware
- xunit
- NetworkCostTests
- UserPresence
- .StartSlowTimer
- .Check
- SystemInfo
- RoutedEventArgs
- SystemTools
- Nextcalibur.Core.Configuration
- ProcessPresence
- Button
- .RequestRestart
- .RemoveMachineTraces
- .Info
- .Main
- Fact
- SystemModeService
- Dependency
- MachineScanRecord
- .SignatureIsValid
- .Get
- Authenticode
- DevicePowerSaving
- .Ask
- DotNetRuntimeDependency
- Nextcalibur.Core.csproj
- .RefreshIdleRows
- BatteryModePolicy
- Fact
- RestorePoint
- BacklightKeyWatcher
- ModeProfileTests
- MemoryTrimmer
- DependencyStatus
- CpuPowerReader
- .For
- CpuLimits
- analyse-autopsy.py
- Program
- EcMailbox
- ToggleButton
- UpdateService
- ResourceDictionary
- RuntimeDependencyTests
- SmiCommand
- CoreLoad
- PowerOverlayService
- .GetColour
- ColourWheel
- LedController
- LedState
- CpuLimitsTests
- FanGauge
- Unelevated
- DonutGauge
- .Processor
- MachineHistory
- IdleSetting
- SegmentedBar
- Nextcalibur Control Center
- .IsUnderProgramFiles
- LogInjectionTests
- microsoft_win32
- Grid
- InstallFolderGuard
- .Check
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Strings
- .Sanitised
- PackageTemperatureTests
- AcDc
- Nextcalibur.Core.Dependencies
- Hardware Protocol
- Nextcalibur 0.5.3
- Nextcalibur 0.5.4
- .PowerModeControls
- Border
- StackPanel
- ThemeService
- NvidiaDriverState
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- LedEffect
- ThermalSample
- PawnIoDependency
- .IsSystemFileSignedBy
- Probe
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
- TourPage
- SetBy
- Log-Graphics.ps1
- Privacy
- Nextcalibur 0.6.2
- Security
- .OnBrightnessChanged
- LedZone
- MachineWideGate
- PowerSource
- PawnIoModuleTests.cs
- Strings.cs
- Notice
- Nextcalibur 0.5.11
- AppIdentity.cs
- ComboBox
- .Set
- SmiSubsystem
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
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
- SmiFamily
- UnsafeModeException
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
1. `MainWindow` - 362 edges
2. `Window` - 186 edges
3. `AppSettings` - 79 edges
4. `Nextcalibur.Core.Hardware` - 62 edges
5. `SystemMode` - 52 edges
6. `RadioButton` - 41 edges
7. `WindowsFaultsTests` - 40 edges
8. `TextBlock` - 39 edges
9. `Nextcalibur.Core.Configuration` - 38 edges
10. `GpuClockReader` - 35 edges

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

## Communities (184 total, 30 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.03
Nodes (69): CheckBox, DeferredOverheat, GameSession, HwndSource, ObservableCollection, PluggedIn, Queue, SizeChangedEventArgs (+61 more)

### Community 1 - "AppSettings"
Cohesion: 0.04
Nodes (57): DateTime, Dictionary, FnShortcut, JsonElement, List, ProgramRule, AppSettings, AcceptedVendorSoftware (+49 more)

### Community 2 - ".Get"
Cohesion: 0.06
Nodes (17): Asynchronous dispatcher & UI thread hardening, GameEnd, SolidColorBrush, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner (+9 more)

### Community 3 - "FnShortcut"
Cohesion: 0.05
Nodes (42): INotifyPropertyChanged, Problem, DriveRow, Detail, Name, Percent, PercentText, Dictionary (+34 more)

### Community 4 - "ProtectedStore"
Cohesion: 0.07
Nodes (26): Content, DirectorySecurity, Hash, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Dictionary (+18 more)

### Community 5 - "GpuClockReader"
Cohesion: 0.08
Nodes (22): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+14 more)

### Community 6 - "SystemMode"
Cohesion: 0.08
Nodes (15): AssemblyInformationalVersionAttribute, PowerModeChangedEventArgs, List, ModeChoice, ThermalProfile, IReadOnlyList, SystemMode, Balanced (+7 more)

### Community 7 - "RunawayWatchTests"
Cohesion: 0.07
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 8 - "ProgramRule"
Cohesion: 0.08
Nodes (25): GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, IEnumerable (+17 more)

### Community 9 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 10 - "RadioButton"
Cohesion: 0.06
Nodes (42): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeBalanced, ModeDiscrete (+34 more)

### Community 11 - "WindowsFaults"
Cohesion: 0.08
Nodes (34): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+26 more)

### Community 12 - "Window"
Cohesion: 0.08
Nodes (46): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+38 more)

### Community 13 - "TrayPresence"
Cohesion: 0.07
Nodes (21): ContextMenuStrip, DevPropKey, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, EventHandler (+13 more)

### Community 14 - "WindowsFaultsTests"
Cohesion: 0.12
Nodes (7): IReadOnlyDictionary, FaultEvaluation, Stable, Fact, WindowsFaultsTests, Dictionary, TopSharePercent

### Community 15 - "Nextcalibur.App"
Cohesion: 0.09
Nodes (25): Nextcalibur.App, Nextcalibur.App.Controls, system_drawing, system_io, system_linq, system_windows, system_windows_controls, system_windows_controls_button (+17 more)

### Community 16 - "PlanTuning"
Cohesion: 0.14
Nodes (17): Floor, Marker, Original, Action, AcDc, DllImport, Guid, IEnumerable (+9 more)

### Community 17 - ".CheckProgram"
Cohesion: 0.10
Nodes (15): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvmlPath, IntegrityFinding (+7 more)

### Community 18 - ".Survey"
Cohesion: 0.10
Nodes (9): Func, IEnumerable, IReadOnlyList, Footprint, RetirementEntry, Trace, Version, NduFix (+1 more)

### Community 19 - "CoolingPolicyTests"
Cohesion: 0.08
Nodes (26): At, Margin, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc (+18 more)

### Community 20 - "Nextcalibur.Core.Hardware"
Cohesion: 0.08
Nodes (10): Nextcalibur.Cli, Nextcalibur.Core.Input, Nextcalibur.Core.Hardware, microsoft_win32_safehandles, system_componentmodel, system_diagnostics, system_management, system_runtime_compilerservices (+2 more)

### Community 21 - "xunit"
Cohesion: 0.10
Nodes (9): Nextcalibur.Core.Tests, Nextcalibur.Core.Tests.Attacks, Nextcalibur.Core.Power, nextcalibur_core_power_plantuning_acdc, system_text_regularexpressions, system_xml_linq, Fact, ThermalProfileTests (+1 more)

### Community 22 - "NetworkCostTests"
Cohesion: 0.11
Nodes (26): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+18 more)

### Community 23 - "UserPresence"
Cohesion: 0.13
Nodes (14): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, Point, Size, DateTime, DllImport (+6 more)

### Community 24 - ".StartSlowTimer"
Cohesion: 0.09
Nodes (8): DllImport, IntPtr, Name, Pid, Func, Task, Action, Toasts

### Community 25 - ".Check"
Cohesion: 0.11
Nodes (14): CommonAce, RawSecurityDescriptor, RegistryKey, SecurityIdentifier, MailboxAccess, MailboxAvailability, AccessNotGranted, Available (+6 more)

### Community 26 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 27 - "RoutedEventArgs"
Cohesion: 0.07
Nodes (9): Slider, LedPower, ReloadButton, SelectAll, Action, Color, RadioButton, RoutedEventArgs (+1 more)

### Community 28 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 29 - "Nextcalibur.Core.Configuration"
Cohesion: 0.11
Nodes (8): Nextcalibur.Core.Security, Nextcalibur.Core.Configuration, system_security_accesscontrol, system_security_cryptography, system_security_cryptography_x509certificates, system_security_principal, system_text_json, system_text_json_serialization

### Community 30 - "ProcessPresence"
Cohesion: 0.12
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 31 - "Button"
Cohesion: 0.07
Nodes (25): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, HideToTrayButton (+17 more)

### Community 32 - ".RequestRestart"
Cohesion: 0.11
Nodes (11): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr (+3 more)

### Community 33 - ".RemoveMachineTraces"
Cohesion: 0.13
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 34 - ".Info"
Cohesion: 0.13
Nodes (5): CancelEventArgs, MessageBoxButton, MessageBoxResult, Log, Folder

### Community 35 - ".Main"
Cohesion: 0.12
Nodes (8): The threat model, and what the code does about it, Application, DllImport, Mutex, App, OverlayRepairPending, RegistryKey, STAThread

### Community 36 - "Fact"
Cohesion: 0.17
Nodes (8): ArgumentNullException, IList, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 37 - "SystemModeService"
Cohesion: 0.19
Nodes (8): Ours, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, Plan, SystemModeService

### Community 38 - "Dependency"
Cohesion: 0.11
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 39 - "MachineScanRecord"
Cohesion: 0.11
Nodes (18): DateTime, JsonSerializerOptions, List, MachineScan, RecordPath, MachineScanRecord, BiosVendor, BiosVersion (+10 more)

### Community 40 - ".SignatureIsValid"
Cohesion: 0.15
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 41 - ".Get"
Cohesion: 0.15
Nodes (12): FirmwareModeReading, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma, GpuModeService, SwitchOutcome (+4 more)

### Community 42 - "Authenticode"
Cohesion: 0.23
Nodes (15): CatalogInfo, Security, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle (+7 more)

### Community 43 - "DevicePowerSaving"
Cohesion: 0.17
Nodes (12): TextBlock, ToggleButton, AcDc, Guid, IEnumerable, DevicePowerSaving, Off, DeviceSaving (+4 more)

### Community 44 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 45 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 46 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 47 - ".RefreshIdleRows"
Cohesion: 0.16
Nodes (6): ComboBox, Grid, Guid, DllImport, IntPtr, ScreenSaver

### Community 48 - "BatteryModePolicy"
Cohesion: 0.22
Nodes (7): BatteryModePolicy, QuietMode, Remembered, Fact, BatteryModePolicyTests, Fact, SystemModesTests

### Community 49 - "Fact"
Cohesion: 0.13
Nodes (11): IReadOnlyList, RepairOutcome, Empty, Fact, Task, CleanMachineTests, Guarded, Unguarded (+3 more)

### Community 50 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 51 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 52 - "ModeProfileTests"
Cohesion: 0.20
Nodes (7): MemberData, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 53 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 54 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 55 - "CpuPowerReader"
Cohesion: 0.24
Nodes (5): DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 56 - ".For"
Cohesion: 0.21
Nodes (11): Baseline, AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList (+3 more)

### Community 57 - "CpuLimits"
Cohesion: 0.13
Nodes (13): Bit, IReadOnlySet, Now, PowerInfo, PowerLimits, Ratio, SinceBoot, IReadOnlyList (+5 more)

### Community 58 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 59 - "Program"
Cohesion: 0.18
Nodes (5): ConsoleColor, B, G, R, Program

### Community 60 - "EcMailbox"
Cohesion: 0.20
Nodes (9): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+1 more)

### Community 61 - "ToggleButton"
Cohesion: 0.11
Nodes (19): OverheatWarningToggle, SettingAskGameMode, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost, SettingFixWidgets (+11 more)

### Community 62 - "UpdateService"
Cohesion: 0.13
Nodes (12): DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService, Available, AvailableVersion (+4 more)

### Community 63 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 64 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 65 - "SmiCommand"
Cohesion: 0.21
Nodes (6): ArgumentException, SmiCommand, Fact, InlineData, Theory, SmiCommandTests

### Community 66 - "CoreLoad"
Cohesion: 0.15
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 67 - "PowerOverlayService"
Cohesion: 0.25
Nodes (6): DllImport, Guid, PowerModeOption, PowerOverlays, All, PowerOverlayService

### Community 68 - ".GetColour"
Cohesion: 0.24
Nodes (7): B, G, R, Fact, InlineData, Theory, LedStateTests

### Community 69 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 70 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 71 - "LedState"
Cohesion: 0.13
Nodes (15): Dictionary, LedBrightness, LedProfile, BrightnessPercent, Colours, Effect, LedState, ActiveProfile (+7 more)

### Community 72 - "CpuLimitsTests"
Cohesion: 0.18
Nodes (6): ArgumentOutOfRangeException, Offset, Fact, CpuLimitsTests, ThermFlags, TjMax

### Community 73 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 74 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 75 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 76 - ".Processor"
Cohesion: 0.27
Nodes (6): GpuPowerLimits, IEnumerable, PowerDefinition, PowerDefinitions, Fact, PowerDefinitionsTests

### Community 77 - "MachineHistory"
Cohesion: 0.28
Nodes (6): DateTime, IReadOnlyList, MachineHistory, DateTime, Fact, MachineHistoryTests

### Community 78 - "IdleSetting"
Cohesion: 0.24
Nodes (8): AcDc, Guid, IReadOnlyList, IdleSetting, DisplayOff, Sleep, IdleTimeouts, Timeouts

### Community 79 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 80 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 81 - ".IsUnderProgramFiles"
Cohesion: 0.23
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 82 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 83 - "microsoft_win32"
Cohesion: 0.15
Nodes (5): microsoft_win32, Theme, Dark, Light, system_security

### Community 84 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 85 - "InstallFolderGuard"
Cohesion: 0.33
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 86 - ".Check"
Cohesion: 0.18
Nodes (10): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+2 more)

### Community 87 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 88 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 89 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 90 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 91 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 92 - ".Sanitised"
Cohesion: 0.26
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 93 - "PackageTemperatureTests"
Cohesion: 0.41
Nodes (4): Fact, InlineData, Theory, PackageTemperatureTests

### Community 94 - "AcDc"
Cohesion: 0.29
Nodes (5): AcDc, InlineData, Theory, Fact, IdleTimeoutsTests

### Community 95 - "Nextcalibur.Core.Dependencies"
Cohesion: 0.35
Nodes (4): Nextcalibur.Core.Dependencies, system_collections_concurrent, system_net, system_net_http

### Community 96 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 97 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 98 - "Nextcalibur 0.5.4"
Cohesion: 0.18
Nodes (10): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, the second pass, Smaller, Tested on, The language row, The .NET runtime is now kept current like everything else (+2 more)

### Community 99 - ".PowerModeControls"
Cohesion: 0.22
Nodes (4): Option, Button, IEnumerable, TextBlock

### Community 100 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 101 - "StackPanel"
Cohesion: 0.20
Nodes (10): BannerActions, DialogActions, DriveTextStack, FnShortcutRows, IdleRows, PanelWindowsFaultsSubOptions, ProfileRow, ProgramRuleRows (+2 more)

### Community 102 - "ThemeService"
Cohesion: 0.20
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 103 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 104 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 105 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 106 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 107 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 108 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 109 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 110 - "LedEffect"
Cohesion: 0.22
Nodes (8): LedEffect, Blink, Breathing, ColourCycle, Heartbeat, Off, Static, Wave

### Community 111 - "ThermalSample"
Cohesion: 0.33
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 112 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 113 - ".IsSystemFileSignedBy"
Cohesion: 0.39
Nodes (3): Fact, SystemFileTrustTests, Cmd

### Community 114 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 115 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 116 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 117 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 118 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 119 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 120 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 121 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 122 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 123 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 124 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 125 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 126 - "SetBy"
Cohesion: 0.29
Nodes (7): SetBy, Intel, LaptopMaker, Nextcalibur, Nvidia, Windows, You

### Community 127 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 128 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 129 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 130 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 131 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 132 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 133 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 134 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 135 - "PawnIoModuleTests.cs"
Cohesion: 0.33
Nodes (4): system_reflection, InlineData, Theory, PawnIoModuleTests

### Community 137 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 138 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 139 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 140 - "ComboBox"
Cohesion: 0.40
Nodes (5): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, ComboBox

### Community 142 - "SmiSubsystem"
Cohesion: 0.40
Nodes (5): SmiSubsystem, DisplayMode, Led, Profile, Thermal

### Community 143 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 145 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 146 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 147 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 148 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 149 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 150 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 151 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 153 - "OverlayDiagnosis"
Cohesion: 0.50
Nodes (4): OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck

### Community 154 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 157 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 158 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

### Community 159 - "SmiFamily"
Cohesion: 0.67
Nodes (3): SmiFamily, Read, Write

## Knowledge Gaps
- **501 isolated node(s):** `OverlayRepairPending`, `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum` (+496 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 970 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **30 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `AppSettings`, `.Get`, `FnShortcut`, `.OnBrightnessChanged`, `GpuClockReader`, `SystemMode`, `LedZone`, `ProgramRule`, `IShellLinkW`, `RadioButton`, `RunawayWatchTests`, `WindowsFaults`, `TrayPresence`, `Window`, `Nextcalibur.App`, `PlanTuning`, `.CheckProgram`, `CoolingPolicyTests`, `Nextcalibur.Core.Hardware`, `UserPresence`, `.StartSlowTimer`, `SystemInfo`, `RoutedEventArgs`, `Nextcalibur.Core.Configuration`, `ProcessPresence`, `Button`, `.RequestRestart`, `.Info`, `SystemModeService`, `.Get`, `DevicePowerSaving`, `.RefreshIdleRows`, `BatteryModePolicy`, `BacklightKeyWatcher`, `DependencyStatus`, `CpuPowerReader`, `EcMailbox`, `UpdateService`, `PowerOverlayService`, `LedController`, `IdleSetting`, `Grid`, `.Check`, `Strings`, `.PowerModeControls`, `ThemeService`, `NvidiaDriverState`, `LedEffect`, `ThermalSample`, `TourPage`?**
  _High betweenness centrality (0.475) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `.OnBrightnessChanged`, `RadioButton`, `ComboBox`, `Keycaps`, `DriveGauge`, `RoutedEventArgs`, `CpuWarnValue`, `DialogCheck`, `Button`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `ToggleButton`, `Grid`, `Border`, `StackPanel`, `Text`?**
  _High betweenness centrality (0.107) - this node is a cross-community bridge._
- **Why does `DependencyStatus` connect `DependencyStatus` to `MainWindow`, `.Get`, `Dependency`, `Nextcalibur.Core.Dependencies`?**
  _High betweenness centrality (0.071) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `OverlayRepairPending`, `SelectedColour`, `VisualChildrenCount` to the rest of the system?**
  _501 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.02635529608006672 - nodes in this community are weakly interconnected._
- **Should `AppSettings` be split into smaller, more focused modules?**
  _Cohesion score 0.03673229503379371 - nodes in this community are weakly interconnected._