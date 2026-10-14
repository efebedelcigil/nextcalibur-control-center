# Graph Report - nextcalibur-control-center  (2026-10-14)

## Corpus Check
- 187 files · ~236,999 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3146 nodes · 6923 edges · 178 communities (145 shown, 33 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 399 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `e28babdb`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- SystemMode
- FnShortcut
- ProtectedStore
- ProcessPresence
- GpuClockReader
- RunawayWatchTests
- .OnLoaded
- .Get
- IShellLinkW
- LedState
- WindowsFaults
- ProgramRule
- RadioButton
- AppSettings
- Window
- WindowsFaultsTests
- PlanTuning
- CoolingPolicyTests
- NetworkCostTests
- Nextcalibur.App
- Nextcalibur.Core.Power
- Nextcalibur.Core.Hardware
- UserPresence
- .Info
- .RequestRestart
- SystemInfo
- TrayPresence
- DotNetRuntimeDependency
- CpuLimits
- Nextcalibur.Core.Configuration
- Fact
- Button
- Elevation
- Dependency
- Program
- Fact
- EcMailbox
- MachineScanRecord
- .SignatureIsValid
- .Get
- LedController
- .OnClosing
- .Check
- system_text_json
- Authenticode
- .Main
- Integrity
- .Survey
- .Read
- .Processor
- .RefreshProgramRuleRows
- .For
- RestorePoint
- Nextcalibur.Core.csproj
- .Save
- .CheckProgram
- .Ask
- MemoryTrimmer
- DependencyStatus
- UpdateService
- CpuPowerReader
- analyse-autopsy.py
- BacklightKeyWatcher
- ModeProfileTests
- ToggleButton
- ResourceDictionary
- SmiCommand
- .ApplyFnShortcuts
- ColourWheel
- FnShortcuts.cs
- FanGauge
- DevicePowerState
- HostileFilesTests
- Unelevated
- DonutGauge
- Hardware Protocol
- SegmentedBar
- Nextcalibur Control Center
- InstallFolderGuard
- AcDc
- LogInjectionTests
- SystemTools
- CoreLoad
- Grid
- PendingRestartInfo
- PackageTemperatureTests
- .HasAccess
- ElevationPolicyTests
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- Strings
- .RefreshIdleRows
- AcDc
- Nextcalibur 0.5.3
- ThemeService
- Border
- StackPanel
- NvidiaDriverState
- SupportVerdict
- WordsInCodeTests
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- .Migrate
- Log
- PawnIoDependency
- .IsSystemFileSignedBy
- ProfileFiles
- Probe
- Nextcalibur 0.5.1
- .LatestAsync
- NotificationState
- IntegrityKind
- Test-Ui.ps1
- Text
- Strings.cs
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
- PowerSource
- Notice
- Nextcalibur 0.5.11
- ComboBox
- .Set
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- 2. Command structure
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- Nextcalibur 0.6.1
- .ArrangeOverride
- .Modules_are_the_released_ones
- Tools
- 0.5.7.md
- .DecodeTarget
- CpuWarnValue
- DialogCheck
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
1. `MainWindow` - 355 edges
2. `Window` - 182 edges
3. `AppSettings` - 79 edges
4. `Nextcalibur.Core.Hardware` - 62 edges
5. `SystemMode` - 52 edges
6. `RadioButton` - 41 edges
7. `WindowsFaultsTests` - 40 edges
8. `Nextcalibur.Core.Configuration` - 38 edges
9. `TextBlock` - 37 edges
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

## Communities (178 total, 33 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.03
Nodes (64): CheckBox, DeferredOverheat, GameEnd, GameSession, HwndSource, ObservableCollection, PluggedIn, Queue (+56 more)

### Community 1 - "SystemMode"
Cohesion: 0.05
Nodes (36): InvalidOperationException, Ours, BatteryModePolicy, QuietMode, Remembered, DllImport, Guid, IReadOnlyList (+28 more)

### Community 2 - "FnShortcut"
Cohesion: 0.05
Nodes (42): INotifyPropertyChanged, Problem, DriveRow, Detail, Name, Percent, PercentText, Dictionary (+34 more)

### Community 3 - "ProtectedStore"
Cohesion: 0.07
Nodes (26): Content, DirectorySecurity, Hash, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Dictionary (+18 more)

### Community 4 - "ProcessPresence"
Cohesion: 0.05
Nodes (28): DateTime, IReadOnlyList, MachineHistory, DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence (+20 more)

### Community 5 - "GpuClockReader"
Cohesion: 0.08
Nodes (23): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+15 more)

### Community 6 - "RunawayWatchTests"
Cohesion: 0.06
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 7 - ".OnLoaded"
Cohesion: 0.06
Nodes (8): SolidColorBrush, DllImport, IntPtr, Name, Pid, Func, Action, Toasts

### Community 8 - ".Get"
Cohesion: 0.10
Nodes (11): Asynchronous dispatcher & UI thread hardening, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary, IReadOnlyDictionary (+3 more)

### Community 9 - "IShellLinkW"
Cohesion: 0.06
Nodes (16): IPropertyStore, PropertyKey, PropVariant, Slider, DllImport, Guid, IEnumerable, IntPtr (+8 more)

### Community 10 - "LedState"
Cohesion: 0.07
Nodes (32): B, Dictionary, G, LedBrightness, R, LedProfile, BrightnessPercent, Colours (+24 more)

### Community 11 - "WindowsFaults"
Cohesion: 0.08
Nodes (34): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+26 more)

### Community 12 - "ProgramRule"
Cohesion: 0.08
Nodes (25): GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, IEnumerable (+17 more)

### Community 13 - "RadioButton"
Cohesion: 0.06
Nodes (42): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeBalanced, ModeDiscrete (+34 more)

### Community 14 - "AppSettings"
Cohesion: 0.04
Nodes (44): FnShortcut, JsonElement, ProgramRule, AppSettings, AcceptedVendorSoftware, AskModeForGames, AutoCheckForUpdates, AutoCooling (+36 more)

### Community 15 - "Window"
Cohesion: 0.08
Nodes (44): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+36 more)

### Community 16 - "WindowsFaultsTests"
Cohesion: 0.12
Nodes (7): IReadOnlyDictionary, FaultEvaluation, Stable, Fact, WindowsFaultsTests, Dictionary, TopSharePercent

### Community 17 - "PlanTuning"
Cohesion: 0.14
Nodes (16): Floor, Marker, Original, Action, DllImport, Guid, IEnumerable, IntPtr (+8 more)

### Community 18 - "CoolingPolicyTests"
Cohesion: 0.08
Nodes (26): At, Margin, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc (+18 more)

### Community 19 - "NetworkCostTests"
Cohesion: 0.11
Nodes (26): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+18 more)

### Community 20 - "Nextcalibur.App"
Cohesion: 0.10
Nodes (22): Nextcalibur.App, Nextcalibur.App.Controls, system_drawing, system_runtime_compilerservices, system_windows, system_windows_controls, system_windows_controls_button, system_windows_controls_checkbox (+14 more)

### Community 21 - "Nextcalibur.Core.Power"
Cohesion: 0.07
Nodes (15): Nextcalibur.Core.Power, microsoft_win32, microsoft_win32_safehandles, Theme, Dark, Light, system_io, system_io_directory (+7 more)

### Community 22 - "Nextcalibur.Core.Hardware"
Cohesion: 0.13
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Hardware, nextcalibur_core_power_plantuning_acdc, system_xml_linq, IdempotenceTests, xunit

### Community 23 - "UserPresence"
Cohesion: 0.12
Nodes (14): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, Point, Size, DateTime, DllImport (+6 more)

### Community 24 - ".Info"
Cohesion: 0.11
Nodes (6): Option, PowerModeChangedEventArgs, Button, IEnumerable, MessageBoxButton, MessageBoxResult

### Community 25 - ".RequestRestart"
Cohesion: 0.10
Nodes (12): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr (+4 more)

### Community 26 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 27 - "TrayPresence"
Cohesion: 0.09
Nodes (16): ContextMenuStrip, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, EventHandler, Icon (+8 more)

### Community 28 - "DotNetRuntimeDependency"
Cohesion: 0.12
Nodes (16): Uri, Version, DotNetRuntimeDependency, ExpectedSigner, Id, MayBeRemoved, Name, Purpose (+8 more)

### Community 29 - "CpuLimits"
Cohesion: 0.09
Nodes (17): ArgumentOutOfRangeException, Bit, IReadOnlySet, Now, PowerInfo, PowerLimits, Ratio, SinceBoot (+9 more)

### Community 30 - "Nextcalibur.Core.Configuration"
Cohesion: 0.13
Nodes (9): Nextcalibur.Cli, Nextcalibur.Core.Security, Nextcalibur.Core.Configuration, system_componentmodel, system_diagnostics, system_linq, system_security_accesscontrol, system_security_cryptography (+1 more)

### Community 31 - "Fact"
Cohesion: 0.16
Nodes (8): Func, IEnumerable, IReadOnlyList, RetirementEntry, Version, Fact, FootprintTests, StartupPreferenceTests

### Community 32 - "Button"
Cohesion: 0.07
Nodes (26): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, HideToTrayButton (+18 more)

### Community 33 - "Elevation"
Cohesion: 0.14
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 34 - "Dependency"
Cohesion: 0.11
Nodes (18): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+10 more)

### Community 35 - "Program"
Cohesion: 0.13
Nodes (8): ConsoleColor, B, G, R, Program, DateTimeOffset, ThermalReader, ThermalSample

### Community 36 - "Fact"
Cohesion: 0.16
Nodes (8): IList, HardwareSupport, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 37 - "EcMailbox"
Cohesion: 0.13
Nodes (13): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+5 more)

### Community 38 - "MachineScanRecord"
Cohesion: 0.11
Nodes (18): DateTime, JsonSerializerOptions, List, MachineScan, RecordPath, MachineScanRecord, BiosVendor, BiosVersion (+10 more)

### Community 39 - ".SignatureIsValid"
Cohesion: 0.15
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 40 - ".Get"
Cohesion: 0.15
Nodes (10): ArgumentNullException, FirmwareModeReading, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma, GpuModeService (+2 more)

### Community 41 - "LedController"
Cohesion: 0.14
Nodes (12): LedController, EffectiveBrightnessPercent, HardwareLevel, State, LedEffect, Blink, Breathing, ColourCycle (+4 more)

### Community 42 - ".OnClosing"
Cohesion: 0.10
Nodes (9): CancelEventArgs, SizeChangedEventArgs, KeyEventArgs, RoutedEventArgs, TourStep, Body, SectionName, Title (+1 more)

### Community 43 - ".Check"
Cohesion: 0.13
Nodes (9): CommonAce, RawSecurityDescriptor, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported (+1 more)

### Community 44 - "system_text_json"
Cohesion: 0.14
Nodes (9): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_security_cryptography_x509certificates, system_text_json, system_text_json_serialization (+1 more)

### Community 45 - "Authenticode"
Cohesion: 0.25
Nodes (14): CatalogInfo, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle, Authenticode (+6 more)

### Community 46 - ".Main"
Cohesion: 0.16
Nodes (7): The threat model, and what the code does about it, Application, DllImport, Mutex, App, OverlayRepairPending, STAThread

### Community 47 - "Integrity"
Cohesion: 0.14
Nodes (10): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvmlPath, IntegrityFinding (+2 more)

### Community 48 - ".Survey"
Cohesion: 0.15
Nodes (4): RegistryKey, Footprint, Trace, NduFix

### Community 49 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 50 - ".Processor"
Cohesion: 0.16
Nodes (12): IEnumerable, PowerDefinition, PowerDefinitions, SetBy, Intel, LaptopMaker, Nextcalibur, Nvidia (+4 more)

### Community 51 - ".RefreshProgramRuleRows"
Cohesion: 0.18
Nodes (3): AssemblyInformationalVersionAttribute, List, ModeChoice

### Community 52 - ".For"
Cohesion: 0.19
Nodes (11): Baseline, AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList (+3 more)

### Community 53 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 54 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 55 - ".Save"
Cohesion: 0.11
Nodes (4): Action, ToggleButton, TextBlock, EventArgs

### Community 56 - ".CheckProgram"
Cohesion: 0.20
Nodes (5): Fact, IntegrityTests, Current, Exe, Manifest

### Community 57 - ".Ask"
Cohesion: 0.25
Nodes (10): HttpStatusCode, CancellationToken, Fact, HttpRequestMessage, HttpResponseMessage, InlineData, Task, Theory (+2 more)

### Community 58 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 59 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 60 - "UpdateService"
Cohesion: 0.13
Nodes (13): DateTime, DispatcherTimer, HashSet, IProgress, Task, TimeSpan, UpdateService, Available (+5 more)

### Community 61 - "CpuPowerReader"
Cohesion: 0.24
Nodes (5): DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 62 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 63 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 64 - "ModeProfileTests"
Cohesion: 0.22
Nodes (7): MemberData, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 65 - "ToggleButton"
Cohesion: 0.11
Nodes (19): LedPower, OverheatWarningToggle, SelectAll, SettingAskGameMode, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice (+11 more)

### Community 66 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 67 - "SmiCommand"
Cohesion: 0.21
Nodes (6): ArgumentException, SmiCommand, Fact, InlineData, Theory, SmiCommandTests

### Community 69 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 70 - "FnShortcuts.cs"
Cohesion: 0.22
Nodes (6): Nextcalibur.Core.Input, system_text, DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 71 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 72 - "DevicePowerState"
Cohesion: 0.28
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 73 - "HostileFilesTests"
Cohesion: 0.27
Nodes (5): IOException, Fact, InlineData, Theory, HostileFilesTests

### Community 74 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 75 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 76 - "Hardware Protocol"
Cohesion: 0.14
Nodes (14): 1. Transport — ACPI-WMI mailbox, 3. Thermal / fan — `a1 = 0x0200`, 4. LED — `a1 = 0x0100`, 6. Safety rules, 7. Not supported, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`) (+6 more)

### Community 77 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 78 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 79 - "InstallFolderGuard"
Cohesion: 0.30
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 80 - "AcDc"
Cohesion: 0.23
Nodes (6): IReadOnlyList, AcDc, InlineData, Theory, Fact, IdleTimeoutsTests

### Community 81 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 82 - "SystemTools"
Cohesion: 0.17
Nodes (9): FileStream, SystemTools, Cmd, Explorer, Pnputil, Powercfg, PowerShell, Schtasks (+1 more)

### Community 83 - "CoreLoad"
Cohesion: 0.17
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 84 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 85 - "PendingRestartInfo"
Cohesion: 0.19
Nodes (7): DateTime, Dictionary, List, PendingRestartInfo, BootTimeUtc, ReasonArguments, Reasons

### Community 86 - "PackageTemperatureTests"
Cohesion: 0.36
Nodes (4): Fact, InlineData, Theory, PackageTemperatureTests

### Community 87 - ".HasAccess"
Cohesion: 0.28
Nodes (5): SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 88 - "ElevationPolicyTests"
Cohesion: 0.23
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 89 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 90 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 91 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 92 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 93 - "Nextcalibur 0.5.4"
Cohesion: 0.17
Nodes (11): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+3 more)

### Community 94 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 95 - ".RefreshIdleRows"
Cohesion: 0.26
Nodes (3): ComboBox, Grid, Guid

### Community 96 - "AcDc"
Cohesion: 0.32
Nodes (7): AcDc, Guid, IdleSetting, DisplayOff, Sleep, IdleTimeouts, Timeouts

### Community 97 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 98 - "ThemeService"
Cohesion: 0.18
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 99 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 100 - "StackPanel"
Cohesion: 0.20
Nodes (10): BannerActions, DialogActions, DriveTextStack, FnShortcutRows, IdleRows, PanelWindowsFaultsSubOptions, ProfileRow, ProgramRuleRows (+2 more)

### Community 101 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 102 - "SupportVerdict"
Cohesion: 0.22
Nodes (9): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites (+1 more)

### Community 103 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

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

### Community 110 - "PawnIoDependency"
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

### Community 115 - ".LatestAsync"
Cohesion: 0.50
Nodes (4): CancellationToken, HttpClient, JsonElement, Task

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

### Community 120 - "Strings.cs"
Cohesion: 0.33
Nodes (5): Nextcalibur.Core, Func, Words, Resolver, system_globalization

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

### Community 126 - ".Current"
Cohesion: 0.48
Nodes (3): DllImport, IntPtr, ScreenSaver

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

### Community 132 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 133 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 134 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 135 - "ComboBox"
Cohesion: 0.40
Nodes (5): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, ComboBox

### Community 137 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 139 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 140 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 141 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 142 - "2. Command structure"
Cohesion: 0.50
Nodes (4): 2. Command structure, Call sequence, Command families (`a0`), Subsystems (`a1`)

### Community 143 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 144 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 145 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 146 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 148 - ".Modules_are_the_released_ones"
Cohesion: 0.50
Nodes (3): InlineData, Theory, PawnIoModuleTests

### Community 149 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 153 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 154 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

## Knowledge Gaps
- **498 isolated node(s):** `OverlayRepairPending`, `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum` (+493 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 964 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **33 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `SystemMode`, `FnShortcut`, `.OnBrightnessChanged`, `GpuClockReader`, `RunawayWatchTests`, `.OnLoaded`, `.Get`, `IShellLinkW`, `LedState`, `WindowsFaults`, `ProgramRule`, `RadioButton`, `AppSettings`, `Window`, `PlanTuning`, `CoolingPolicyTests`, `Nextcalibur.App`, `Nextcalibur.Core.Power`, `UserPresence`, `.Info`, `.RequestRestart`, `SystemInfo`, `TrayPresence`, `Nextcalibur.Core.Configuration`, `Button`, `Program`, `EcMailbox`, `.Get`, `LedController`, `.OnClosing`, `Integrity`, `.Read`, `.RefreshProgramRuleRows`, `.Save`, `DependencyStatus`, `UpdateService`, `CpuPowerReader`, `BacklightKeyWatcher`, `.ApplyFnShortcuts`, `Grid`, `Strings`, `.RefreshIdleRows`, `AcDc`, `ThemeService`, `NvidiaDriverState`, `SupportVerdict`, `TourPage`?**
  _High betweenness centrality (0.447) - this node is a cross-community bridge._
- **Why does `AppSettings` connect `AppSettings` to `MainWindow`, `SystemMode`, `ThemeService`, `ProtectedStore`, `HostileFilesTests`, `LedState`, `system_text_json`, `.Migrate`, `.Survey`, `WindowsFaultsTests`, `PendingRestartInfo`, `.Save`, `TrayPresence`, `Strings`, `Fact`?**
  _High betweenness centrality (0.080) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `.OnBrightnessChanged`, `ComboBox`, `Keycaps`, `DriveGauge`, `RadioButton`, `CpuWarnValue`, `DialogCheck`, `ColGauge`, `DialogProgress`, `DriveDot`, `Button`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `ToggleButton`, `Grid`, `Border`, `StackPanel`, `Text`?**
  _High betweenness centrality (0.073) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `OverlayRepairPending`, `SelectedColour`, `VisualChildrenCount` to the rest of the system?**
  _498 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.028686868686868688 - nodes in this community are weakly interconnected._
- **Should `SystemMode` be split into smaller, more focused modules?**
  _Cohesion score 0.05421686746987952 - nodes in this community are weakly interconnected._