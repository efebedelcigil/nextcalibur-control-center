# Graph Report - nextcalibur-control-center  (2026-10-14)

## Corpus Check
- 182 files · ~233,009 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3069 nodes · 6707 edges · 183 communities (150 shown, 33 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 372 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `30d88867`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- LedState
- CpuPowerReader
- FnShortcut
- ProtectedStore
- RunawayWatchTests
- GpuClockReader
- WindowsFaults
- ProgramRule
- IShellLinkW
- Window
- .Get
- .CheckProgram
- AppSettings
- RadioButton
- WindowsFaultsTests
- CoolingPolicyTests
- Nextcalibur.Core.Configuration
- NetworkCostTests
- .Main
- PlanTuning
- Nextcalibur.Core.Hardware
- .Warn
- UserPresence
- .Info
- xunit
- SystemInfo
- EcMailbox
- Fact
- DotNetRuntimeDependency
- .RemoveMachineTraces
- ProcessPresence
- .RequestRestart
- .Get
- Button
- TrayPresence
- Dependency
- SystemModeService
- MachineScanRecord
- .SignatureIsValid
- .OnClosing
- system_windows
- Fact
- Fact
- Authenticode
- .Check
- Nextcalibur.Core.Tests
- .Migrate
- .For
- Nextcalibur.Core.csproj
- RestorePoint
- .Ask
- MemoryTrimmer
- DependencyStatus
- SystemMode
- PowerOverlayService
- ToggleButton
- analyse-autopsy.py
- Program
- BacklightKeyWatcher
- SettingsTests.cs
- ModeProfileTests
- .ApplyFnShortcuts
- ResourceDictionary
- UpdateService
- .Ask
- ColourWheel
- .Save
- FanGauge
- DevicePowerState
- HostileFilesTests
- Unelevated
- DonutGauge
- SegmentedBar
- Nextcalibur Control Center
- .IdleChosen
- .IsUnderProgramFiles
- .HasAccess
- AcDc
- LogInjectionTests
- SystemTools
- CoreLoad
- Grid
- InstallFolderGuard
- BatteryModePolicy
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Strings
- .RefreshProgramRuleRows
- .Check
- AcDc
- SmiCommandTests
- Hardware Protocol
- Nextcalibur 0.5.3
- Nextcalibur 0.5.4
- ThemeService
- .OnPowerModeChanged
- Border
- StackPanel
- NvidiaDriverState
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- ThermalSample
- PawnIoDependency
- NduFix
- .IsSystemFileSignedBy
- ProfileFiles
- Probe
- Nextcalibur 0.5.1
- Nextcalibur 0.5.5
- .LatestAsync
- NotificationState
- IntegrityKind
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- .Pick
- .ForegroundProcess
- TourPage
- .Current
- .Values_are_the_ones_measured_from_the_vendor
- Log-Graphics.ps1
- Privacy
- Nextcalibur 0.6.2
- Security
- .OnBrightnessChanged
- MachineWideGate
- Notice
- Nextcalibur 0.5.11
- SmiSubsystem
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- Nextcalibur 0.6.1
- .PinnedFloors
- .ArrangeOverride
- ComboBox
- ThemeService.cs
- Tools
- FnShortcuts.cs
- 0.5.7.md
- CpuWarnValue
- DialogCheck
- .ChooseTourCardPosition
- SmiFamily
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
1. `MainWindow` - 348 edges
2. `Window` - 179 edges
3. `AppSettings` - 75 edges
4. `Nextcalibur.Core.Hardware` - 56 edges
5. `SystemMode` - 43 edges
6. `WindowsFaultsTests` - 40 edges
7. `RadioButton` - 39 edges
8. `Nextcalibur.Core.Configuration` - 38 edges
9. `TextBlock` - 36 edges
10. `WindowsFaults` - 33 edges

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

## Communities (183 total, 33 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.03
Nodes (60): CheckBox, DeferredOverheat, GameSession, HwndSource, ObservableCollection, PluggedIn, Queue, MainWindow (+52 more)

### Community 1 - "LedState"
Cohesion: 0.05
Nodes (44): B, Dictionary, G, LedBrightness, R, LedProfile, BrightnessPercent, Colours (+36 more)

### Community 2 - "CpuPowerReader"
Cohesion: 0.05
Nodes (32): ArgumentOutOfRangeException, Bit, IReadOnlySet, Now, Offset, PowerInfo, PowerLimits, Ratio (+24 more)

### Community 3 - "FnShortcut"
Cohesion: 0.05
Nodes (42): INotifyPropertyChanged, Problem, DriveRow, Detail, Name, Percent, PercentText, Dictionary (+34 more)

### Community 4 - "ProtectedStore"
Cohesion: 0.07
Nodes (26): Content, DirectorySecurity, Hash, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Dictionary (+18 more)

### Community 5 - "RunawayWatchTests"
Cohesion: 0.07
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 6 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 7 - "WindowsFaults"
Cohesion: 0.07
Nodes (37): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+29 more)

### Community 8 - "ProgramRule"
Cohesion: 0.07
Nodes (26): GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, IEnumerable (+18 more)

### Community 9 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 10 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 11 - ".Get"
Cohesion: 0.11
Nodes (8): Asynchronous dispatcher & UI thread hardening, GameEnd, SolidColorBrush, Dictionary, IReadOnlyDictionary, Task, Task, IReadOnlyDictionary

### Community 12 - ".CheckProgram"
Cohesion: 0.10
Nodes (15): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvmlPath, IntegrityFinding (+7 more)

### Community 13 - "AppSettings"
Cohesion: 0.05
Nodes (40): FnShortcut, JsonElement, ProgramRule, AppSettings, AcceptedVendorSoftware, AskModeForGames, AutoCheckForUpdates, AutoCooling (+32 more)

### Community 14 - "RadioButton"
Cohesion: 0.07
Nodes (36): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeDiscrete, ModeGaming (+28 more)

### Community 15 - "WindowsFaultsTests"
Cohesion: 0.13
Nodes (4): FaultEvaluation, Fact, WindowsFaultsTests, Dictionary

### Community 16 - "CoolingPolicyTests"
Cohesion: 0.08
Nodes (26): At, Margin, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc (+18 more)

### Community 17 - "Nextcalibur.Core.Configuration"
Cohesion: 0.06
Nodes (19): Nextcalibur.App, Nextcalibur.Core, Nextcalibur.Core.Configuration, system_componentmodel, system_drawing, system_globalization, system_io, system_io_directory (+11 more)

### Community 18 - "NetworkCostTests"
Cohesion: 0.11
Nodes (26): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+18 more)

### Community 19 - ".Main"
Cohesion: 0.09
Nodes (10): The threat model, and what the code does about it, Application, DllImport, Mutex, App, StartupRegistration, IsEnabled, RegistryKey (+2 more)

### Community 20 - "PlanTuning"
Cohesion: 0.16
Nodes (13): Marker, Original, Action, DllImport, Guid, IEnumerable, IntPtr, IReadOnlyList (+5 more)

### Community 21 - "Nextcalibur.Core.Hardware"
Cohesion: 0.10
Nodes (8): Nextcalibur.Core.Hardware, microsoft_win32, microsoft_win32_safehandles, system_diagnostics, system_management, system_runtime_interopservices, system_security, system_text

### Community 22 - ".Warn"
Cohesion: 0.09
Nodes (7): IReadOnlyList, GameEnd, Func, MessageBoxButton, MessageBoxResult, Action, Toasts

### Community 23 - "UserPresence"
Cohesion: 0.13
Nodes (12): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr, MarshalAs (+4 more)

### Community 24 - ".Info"
Cohesion: 0.11
Nodes (5): AssemblyInformationalVersionAttribute, IProgress, Exception, Log, Folder

### Community 25 - "xunit"
Cohesion: 0.13
Nodes (9): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_security_cryptography_x509certificates, system_text_json (+1 more)

### Community 26 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 27 - "EcMailbox"
Cohesion: 0.14
Nodes (11): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+3 more)

### Community 28 - "Fact"
Cohesion: 0.16
Nodes (9): Func, IEnumerable, IReadOnlyList, RetirementEntry, Trace, Version, Fact, FootprintTests (+1 more)

### Community 29 - "DotNetRuntimeDependency"
Cohesion: 0.12
Nodes (16): Uri, Version, DotNetRuntimeDependency, ExpectedSigner, Id, MayBeRemoved, Name, Purpose (+8 more)

### Community 30 - ".RemoveMachineTraces"
Cohesion: 0.13
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 31 - "ProcessPresence"
Cohesion: 0.13
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 32 - ".RequestRestart"
Cohesion: 0.12
Nodes (10): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr, MarshalAs (+2 more)

### Community 33 - ".Get"
Cohesion: 0.13
Nodes (14): ArgumentNullException, GpuLoad, FirmwareModeReading, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma (+6 more)

### Community 34 - "Button"
Cohesion: 0.07
Nodes (26): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, HideToTrayButton (+18 more)

### Community 35 - "TrayPresence"
Cohesion: 0.11
Nodes (11): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, EventArgs, List, Ms (+3 more)

### Community 36 - "Dependency"
Cohesion: 0.11
Nodes (18): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+10 more)

### Community 37 - "SystemModeService"
Cohesion: 0.17
Nodes (11): InvalidOperationException, Ours, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList (+3 more)

### Community 38 - "MachineScanRecord"
Cohesion: 0.11
Nodes (18): DateTime, JsonSerializerOptions, List, MachineScan, RecordPath, MachineScanRecord, BiosVendor, BiosVersion (+10 more)

### Community 39 - ".SignatureIsValid"
Cohesion: 0.15
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 40 - ".OnClosing"
Cohesion: 0.09
Nodes (9): CancelEventArgs, SizeChangedEventArgs, KeyEventArgs, RoutedEventArgs, TourStep, Body, SectionName, Title (+1 more)

### Community 41 - "system_windows"
Cohesion: 0.17
Nodes (14): Nextcalibur.App.Controls, system_linq, system_windows, system_windows_controls, system_windows_controls_button, system_windows_controls_checkbox, system_windows_controls_combobox, system_windows_controls_comboboxitem (+6 more)

### Community 42 - "Fact"
Cohesion: 0.18
Nodes (7): IList, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 43 - "Fact"
Cohesion: 0.11
Nodes (14): OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact, Task (+6 more)

### Community 44 - "Authenticode"
Cohesion: 0.23
Nodes (15): CatalogInfo, Security, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle (+7 more)

### Community 45 - ".Check"
Cohesion: 0.14
Nodes (9): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported, IdempotenceTests (+1 more)

### Community 46 - "Nextcalibur.Core.Tests"
Cohesion: 0.13
Nodes (5): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, nextcalibur_core_power_plantuning_acdc, system_text_regularexpressions, system_xml_linq

### Community 47 - ".Migrate"
Cohesion: 0.12
Nodes (8): DateTime, Dictionary, List, PendingRestartInfo, BootTimeUtc, ReasonArguments, Reasons, SettingsTests

### Community 48 - ".For"
Cohesion: 0.19
Nodes (11): Baseline, AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList (+3 more)

### Community 49 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 50 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 51 - ".Ask"
Cohesion: 0.25
Nodes (10): HttpStatusCode, CancellationToken, Fact, HttpRequestMessage, HttpResponseMessage, InlineData, Task, Theory (+2 more)

### Community 52 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 53 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 54 - "SystemMode"
Cohesion: 0.21
Nodes (5): PowerModeChangedEventArgs, SystemMode, Gaming, Office, Performance

### Community 55 - "PowerOverlayService"
Cohesion: 0.20
Nodes (7): DllImport, Guid, IReadOnlyList, PowerModeOption, PowerOverlays, All, PowerOverlayService

### Community 56 - "ToggleButton"
Cohesion: 0.10
Nodes (20): LedPower, OverheatWarningToggle, SelectAll, SettingAskGameMode, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice (+12 more)

### Community 57 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 58 - "Program"
Cohesion: 0.18
Nodes (5): ConsoleColor, B, G, R, Program

### Community 59 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 60 - "SettingsTests.cs"
Cohesion: 0.14
Nodes (8): Nextcalibur.Cli, system_reflection, system_security_accesscontrol, system_security_cryptography, system_security_principal, InlineData, Theory, PawnIoModuleTests

### Community 61 - "ModeProfileTests"
Cohesion: 0.22
Nodes (7): MemberData, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 63 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 64 - "UpdateService"
Cohesion: 0.14
Nodes (12): DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService, Available, AvailableVersion (+4 more)

### Community 65 - ".Ask"
Cohesion: 0.15
Nodes (5): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner

### Community 66 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 67 - ".Save"
Cohesion: 0.14
Nodes (4): Slider, Action, ToggleButton, TextBox

### Community 68 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 69 - "DevicePowerState"
Cohesion: 0.28
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 70 - "HostileFilesTests"
Cohesion: 0.27
Nodes (5): IOException, Fact, InlineData, Theory, HostileFilesTests

### Community 71 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 72 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 73 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 74 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 75 - ".IdleChosen"
Cohesion: 0.21
Nodes (3): ComboBox, Grid, Guid

### Community 76 - ".IsUnderProgramFiles"
Cohesion: 0.23
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 77 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 78 - "AcDc"
Cohesion: 0.23
Nodes (6): IReadOnlyList, AcDc, InlineData, Theory, Fact, IdleTimeoutsTests

### Community 79 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 80 - "SystemTools"
Cohesion: 0.17
Nodes (9): FileStream, SystemTools, Cmd, Explorer, Pnputil, Powercfg, PowerShell, Schtasks (+1 more)

### Community 81 - "CoreLoad"
Cohesion: 0.17
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 82 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 83 - "InstallFolderGuard"
Cohesion: 0.33
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 84 - "BatteryModePolicy"
Cohesion: 0.42
Nodes (4): BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

### Community 85 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 86 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 87 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 88 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 89 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 91 - ".Check"
Cohesion: 0.20
Nodes (10): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+2 more)

### Community 92 - "AcDc"
Cohesion: 0.32
Nodes (7): AcDc, Guid, IdleSetting, DisplayOff, Sleep, IdleTimeouts, Timeouts

### Community 93 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 94 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 95 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 96 - "Nextcalibur 0.5.4"
Cohesion: 0.18
Nodes (10): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, the second pass, Smaller, Tested on, The language row, The .NET runtime is now kept current like everything else (+2 more)

### Community 97 - "ThemeService"
Cohesion: 0.18
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 98 - ".OnPowerModeChanged"
Cohesion: 0.20
Nodes (8): Option, PowerModeBalanced, PowerModeBest, PowerModeBetter, PowerModeEfficiency, Button, IEnumerable, TextBlock

### Community 99 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 100 - "StackPanel"
Cohesion: 0.20
Nodes (10): BannerActions, DialogActions, DriveTextStack, FnShortcutRows, IdleRows, PanelWindowsFaultsSubOptions, ProfileRow, ProgramRuleRows (+2 more)

### Community 101 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 102 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 103 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 104 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 105 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 106 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 107 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 108 - "ThermalSample"
Cohesion: 0.33
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 109 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 111 - ".IsSystemFileSignedBy"
Cohesion: 0.39
Nodes (3): Fact, SystemFileTrustTests, Cmd

### Community 113 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 114 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 115 - "Nextcalibur 0.5.5"
Cohesion: 0.25
Nodes (6): Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture

### Community 116 - ".LatestAsync"
Cohesion: 0.50
Nodes (4): CancellationToken, HttpClient, JsonElement, Task

### Community 117 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 118 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 119 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 120 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 121 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 122 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 123 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 124 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 125 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 126 - ".ForegroundProcess"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Name, Pid

### Community 127 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 128 - ".Current"
Cohesion: 0.48
Nodes (3): DllImport, IntPtr, ScreenSaver

### Community 129 - ".Values_are_the_ones_measured_from_the_vendor"
Cohesion: 0.33
Nodes (4): Fact, InlineData, Theory, ThermalProfileTests

### Community 130 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 131 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 132 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 133 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 134 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 135 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 136 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 137 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 138 - "SmiSubsystem"
Cohesion: 0.40
Nodes (5): SmiSubsystem, DisplayMode, Led, Profile, Thermal

### Community 139 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 141 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 142 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 143 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 144 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 145 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 146 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 147 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 148 - ".PinnedFloors"
Cohesion: 0.50
Nodes (3): Floor, Name, Plan

### Community 150 - "ComboBox"
Cohesion: 0.50
Nodes (4): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, ComboBox

### Community 151 - "ThemeService.cs"
Cohesion: 0.50
Nodes (3): Theme, Dark, Light

### Community 152 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 156 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 157 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

### Community 159 - "SmiFamily"
Cohesion: 0.67
Nodes (3): SmiFamily, Read, Write

## Knowledge Gaps
- **484 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+479 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 949 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **33 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `LedState`, `CpuPowerReader`, `FnShortcut`, `RunawayWatchTests`, `.OnBrightnessChanged`, `GpuClockReader`, `WindowsFaults`, `ProgramRule`, `Window`, `.Get`, `.CheckProgram`, `AppSettings`, `RadioButton`, `CoolingPolicyTests`, `Nextcalibur.Core.Configuration`, `PlanTuning`, `Nextcalibur.Core.Hardware`, `.Warn`, `UserPresence`, `.Info`, `SystemInfo`, `EcMailbox`, `.ChooseTourCardPosition`, `.RequestRestart`, `.Get`, `Button`, `TrayPresence`, `SystemModeService`, `.OnClosing`, `system_windows`, `DependencyStatus`, `SystemMode`, `PowerOverlayService`, `BacklightKeyWatcher`, `.ApplyFnShortcuts`, `UpdateService`, `.Ask`, `.Save`, `.IdleChosen`, `Grid`, `BatteryModePolicy`, `Strings`, `.RefreshProgramRuleRows`, `.Check`, `AcDc`, `ThemeService`, `.OnPowerModeChanged`, `NvidiaDriverState`, `ThermalSample`, `Nextcalibur 0.5.5`, `.ForegroundProcess`, `TourPage`?**
  _High betweenness centrality (0.476) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `.OnBrightnessChanged`, `RadioButton`, `DriveGauge`, `Keycaps`, `ComboBox`, `CpuWarnValue`, `DialogCheck`, `Button`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `ToggleButton`, `Grid`, `.OnPowerModeChanged`, `Border`, `StackPanel`, `Text`?**
  _High betweenness centrality (0.093) - this node is a cross-community bridge._
- **Why does `DependencyStatus` connect `DependencyStatus` to `MainWindow`, `xunit`, `.Get`, `Dependency`?**
  _High betweenness centrality (0.069) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _484 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.029853951890034366 - nodes in this community are weakly interconnected._
- **Should `LedState` be split into smaller, more focused modules?**
  _Cohesion score 0.05009009009009009 - nodes in this community are weakly interconnected._