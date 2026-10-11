# Graph Report - nextcalibur-control-center  (2026-10-11)

## Corpus Check
- 155 files · ~207,967 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2604 nodes · 5566 edges · 159 communities (127 shown, 32 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 284 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `a74d71e7`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- DotNetRuntimeDependency
- LedState
- Authenticode
- MainWindow
- RunawayWatchTests
- GpuClockReader
- ProtectedStore
- .CheckProgram
- Window
- RadioButton
- Nextcalibur.App
- IShellLinkW
- UserPresence
- PlanTuning
- .OnLoaded
- xunit
- .Get
- WindowsFaults
- AppSettings
- Fact
- SystemInfo
- TrayPresence
- .RequestRestart
- SystemTools
- SystemMode
- WindowsFaultsTests
- Button
- .Main
- ProcessPresence
- CpuPowerReader
- CoolingPolicyTests
- Nextcalibur.Core.Hardware
- EcMailbox
- Elevation
- .Survey
- .OnClosing
- Fact
- .Info
- PowerOverlayService
- .OnGpuModeChanged
- .Read
- Program
- Nextcalibur.Core.csproj
- MemoryTrimmer
- DependencyStatus
- analyse-autopsy.py
- Fact
- .Check
- ToggleButton
- ResourceDictionary
- .Migrate
- BacklightKeyWatcher
- LogInjectionTests
- UpdateService
- ColourWheel
- .Ask
- Unelevated
- CoreLoad
- .IsUnderProgramFiles
- FanGauge
- Nextcalibur.Core.Configuration
- DevicePowerState
- .OnPowerSourceMayHaveChanged
- DonutGauge
- Nextcalibur.Core.Power
- SegmentedBar
- ProcessMetrics
- .HasAccess
- PawnIoDependency
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur Control Center
- Strings
- Grid
- InstallFolderGuard
- Dependency
- GpuModeService
- .Check
- SmiCommandTests
- Nextcalibur.Core.Dependencies
- Hardware Protocol
- Nextcalibur 0.5.3
- .WireSettingsPage
- ReleaseFile
- Border
- ThemeService
- NvidiaDriverState
- .CalculateThreadShare
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- ThermalSample
- Probe
- Nextcalibur 0.5.1
- Nextcalibur 0.5.5
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Releasing, and signing
- DriveRow
- .Pick
- StackPanel
- TourPage
- Log-Graphics.ps1
- Privacy
- Security
- .OnBrightnessChanged
- MachineWideGate
- PowerSource
- Notice
- Nextcalibur 0.5.11
- .Set
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Strings.cs
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- .ArrangeOverride
- FanTurbo
- .SendAsync
- Tools
- 0.5.7.md
- CpuWarnValue
- Words
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
1. `MainWindow` - 250 edges
2. `Window` - 165 edges
3. `AppSettings` - 66 edges
4. `Nextcalibur.Core.Hardware` - 48 edges
5. `WindowsFaultsTests` - 40 edges
6. `RadioButton` - 39 edges
7. `TextBlock` - 36 edges
8. `WindowsFaults` - 33 edges
9. `Nextcalibur.Core.Configuration` - 31 edges
10. `GpuClockReader` - 31 edges

## Surprising Connections (you probably didn't know these)
- `Deep memory & resource leak eradication` --references--> `MainWindow`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.App/MainWindow.Cooling.cs
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

## Communities (159 total, 32 thin omitted)

### Community 0 - "DotNetRuntimeDependency"
Cohesion: 0.05
Nodes (46): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+38 more)

### Community 1 - "LedState"
Cohesion: 0.05
Nodes (42): B, Dictionary, G, R, LedProfile, BrightnessPercent, Colours, Effect (+34 more)

### Community 2 - "Authenticode"
Cohesion: 0.06
Nodes (38): CatalogInfo, Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on (+30 more)

### Community 3 - "MainWindow"
Cohesion: 0.04
Nodes (41): DeferredOverheat, ObservableCollection, Queue, Slider, DateTime, ManagementEventWatcher, MainWindow, CoolingWantsReadings (+33 more)

### Community 4 - "RunawayWatchTests"
Cohesion: 0.06
Nodes (38): Cpu, CpuMs, DllImport, IntPtr, Name, DateTime, Dictionary, Func (+30 more)

### Community 5 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 6 - "ProtectedStore"
Cohesion: 0.09
Nodes (20): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+12 more)

### Community 7 - ".CheckProgram"
Cohesion: 0.07
Nodes (23): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvmlPath, IntegrityFinding (+15 more)

### Community 8 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 9 - "RadioButton"
Cohesion: 0.07
Nodes (37): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeGaming, ModeOffice (+29 more)

### Community 10 - "Nextcalibur.App"
Cohesion: 0.08
Nodes (25): Nextcalibur.App, Nextcalibur.App.Controls, system_componentmodel, system_drawing, system_io, system_io_directory, system_io_ioexception, system_io_path (+17 more)

### Community 11 - "IShellLinkW"
Cohesion: 0.07
Nodes (13): PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder, AppIdentity (+5 more)

### Community 12 - "UserPresence"
Cohesion: 0.10
Nodes (20): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr, MarshalAs (+12 more)

### Community 13 - "PlanTuning"
Cohesion: 0.13
Nodes (16): AcDc, Floor, Original, Plan, Action, DllImport, Guid, IntPtr (+8 more)

### Community 14 - ".OnLoaded"
Cohesion: 0.10
Nodes (9): AssemblyInformationalVersionAttribute, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Action, Toasts (+1 more)

### Community 15 - "xunit"
Cohesion: 0.12
Nodes (7): Nextcalibur.Core.Tests, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_security_cryptography_x509certificates, system_text_regularexpressions, system_xml_linq, xunit

### Community 16 - ".Get"
Cohesion: 0.10
Nodes (5): SolidColorBrush, Func, MessageBoxButton, MessageBoxResult, Task

### Community 17 - "WindowsFaults"
Cohesion: 0.11
Nodes (21): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, DateTime, DllImport, Func (+13 more)

### Community 18 - "AppSettings"
Cohesion: 0.06
Nodes (31): JsonElement, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoCooling, AutoFanTurbo, AutoInstallUpdates, CompensateWindowsFaults (+23 more)

### Community 19 - "Fact"
Cohesion: 0.13
Nodes (10): ArgumentNullException, IList, GpuLoad, GpuConfiguration, Fact, GpuModeTests, Discrete, Hybrid (+2 more)

### Community 20 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 21 - "TrayPresence"
Cohesion: 0.10
Nodes (11): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, EventArgs, List, Ms (+3 more)

### Community 22 - ".RequestRestart"
Cohesion: 0.10
Nodes (11): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr (+3 more)

### Community 23 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 24 - "SystemMode"
Cohesion: 0.15
Nodes (13): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, SystemMode (+5 more)

### Community 25 - "WindowsFaultsTests"
Cohesion: 0.18
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 26 - "Button"
Cohesion: 0.08
Nodes (23): IsChecked, BannerDismissButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, HideToTrayButton, MinimiseButton, OpenLogButton (+15 more)

### Community 27 - ".Main"
Cohesion: 0.13
Nodes (8): The threat model, and what the code does about it, Application, DllImport, Mutex, App, RegistryKey, Footprint, STAThread

### Community 28 - "ProcessPresence"
Cohesion: 0.12
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 29 - "CpuPowerReader"
Cohesion: 0.16
Nodes (9): DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled, Fact, InlineData, Theory (+1 more)

### Community 30 - "CoolingPolicyTests"
Cohesion: 0.12
Nodes (16): At, Margin, DateTime, List, TimeSpan, CoolingPolicy, DegreesPerMinute, Engaged (+8 more)

### Community 31 - "Nextcalibur.Core.Hardware"
Cohesion: 0.13
Nodes (8): Nextcalibur.Cli, Nextcalibur.Core.Hardware, microsoft_win32_safehandles, system_diagnostics, system_management, system_reflection, system_runtime_interopservices, system_text

### Community 32 - "EcMailbox"
Cohesion: 0.16
Nodes (10): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+2 more)

### Community 33 - "Elevation"
Cohesion: 0.14
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 34 - ".Survey"
Cohesion: 0.14
Nodes (7): Func, IEnumerable, IReadOnlyList, Version, RetirementEntry, Trace, FootprintTests

### Community 35 - ".OnClosing"
Cohesion: 0.09
Nodes (11): CancelEventArgs, KeyEventArgs, SizeChangedEventArgs, Point, RoutedEventArgs, Size, TourStep, Body (+3 more)

### Community 36 - "Fact"
Cohesion: 0.11
Nodes (14): OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact, Task (+6 more)

### Community 37 - ".Info"
Cohesion: 0.16
Nodes (3): Log, Folder, NduFix

### Community 38 - "PowerOverlayService"
Cohesion: 0.16
Nodes (11): Option, Button, IEnumerable, DllImport, Guid, IReadOnlyList, PowerModeOption, PowerOverlays (+3 more)

### Community 39 - ".OnGpuModeChanged"
Cohesion: 0.11
Nodes (11): BannerRestartButton, FixButton, ModeDiscrete, ModeHybrid, ModeUma, RadioButton, FirmwareModeReading, GpuMode (+3 more)

### Community 40 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 41 - "Program"
Cohesion: 0.17
Nodes (5): ConsoleColor, B, G, R, Program

### Community 42 - "Nextcalibur.Core.csproj"
Cohesion: 0.12
Nodes (13): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Management (10.0.12), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk, net10.0-windows (+5 more)

### Community 43 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 44 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 45 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 46 - "Fact"
Cohesion: 0.16
Nodes (5): Fact, ForwardCompatibilityTests, MailboxAccessTests, SettingsTests, StartupPreferenceTests

### Community 47 - ".Check"
Cohesion: 0.20
Nodes (7): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported

### Community 48 - "ToggleButton"
Cohesion: 0.11
Nodes (16): LedPower, OverheatWarningToggle, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost, SettingFixWidgets (+8 more)

### Community 49 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 50 - ".Migrate"
Cohesion: 0.16
Nodes (9): DateTime, Dictionary, List, PendingRestartInfo, BootTimeUtc, ReasonArguments, Reasons, InlineData (+1 more)

### Community 51 - "BacklightKeyWatcher"
Cohesion: 0.14
Nodes (11): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, LedBrightness, Full, Half, Off, Fact (+3 more)

### Community 52 - "LogInjectionTests"
Cohesion: 0.19
Nodes (7): Exception, Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 53 - "UpdateService"
Cohesion: 0.15
Nodes (12): DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService, Available, AvailableVersion (+4 more)

### Community 54 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 55 - ".Ask"
Cohesion: 0.35
Nodes (7): HttpStatusCode, Fact, InlineData, Task, Theory, Answer, HostileAnswerTests

### Community 56 - "Unelevated"
Cohesion: 0.33
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 57 - "CoreLoad"
Cohesion: 0.13
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 58 - ".IsUnderProgramFiles"
Cohesion: 0.21
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 59 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 60 - "Nextcalibur.Core.Configuration"
Cohesion: 0.24
Nodes (6): Nextcalibur.Core.Configuration, system_security_accesscontrol, system_security_cryptography, system_security_principal, system_text_json, system_text_json_serialization

### Community 61 - "DevicePowerState"
Cohesion: 0.28
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 62 - ".OnPowerSourceMayHaveChanged"
Cohesion: 0.34
Nodes (5): PowerModeChangedEventArgs, BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

### Community 63 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 64 - "Nextcalibur.Core.Power"
Cohesion: 0.16
Nodes (6): Nextcalibur.Core.Power, microsoft_win32, Theme, Dark, Light, system_security

### Community 65 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 66 - "ProcessMetrics"
Cohesion: 0.15
Nodes (13): Share, Dictionary, TimeSpan, FaultEvidence, ProcessMetrics, Name, PageFaultCount, SessionId (+5 more)

### Community 67 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 68 - "PawnIoDependency"
Cohesion: 0.15
Nodes (11): CancellationToken, HttpClient, Task, Version, PawnIoDependency, ExpectedSigner, Id, Name (+3 more)

### Community 69 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 70 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 71 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 72 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 73 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 74 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 75 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 76 - "InstallFolderGuard"
Cohesion: 0.35
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 77 - "Dependency"
Cohesion: 0.17
Nodes (9): Version, Dependency, ExpectedSigner, Id, MayBeRemoved, Name, Purpose, SilentInstallArguments (+1 more)

### Community 78 - "GpuModeService"
Cohesion: 0.38
Nodes (3): GpuModeService, SwitchOutcome, SwitchOutcome

### Community 79 - ".Check"
Cohesion: 0.20
Nodes (10): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+2 more)

### Community 80 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 81 - "Nextcalibur.Core.Dependencies"
Cohesion: 0.35
Nodes (4): Nextcalibur.Core.Dependencies, system_collections_concurrent, system_net, system_net_http

### Community 82 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 83 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 85 - "ReleaseFile"
Cohesion: 0.33
Nodes (6): CancellationToken, HttpClient, Task, Uri, ReleaseFile, HttpClient

### Community 86 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 88 - "ThemeService"
Cohesion: 0.20
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 89 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 90 - ".CalculateThreadShare"
Cohesion: 0.27
Nodes (4): IReadOnlyDictionary, Stable, Dictionary, TopSharePercent

### Community 91 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 92 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 93 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 94 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 95 - "README.md"
Cohesion: 0.33
Nodes (3): How it works, Questions people ask, Using it

### Community 96 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 97 - "ThermalSample"
Cohesion: 0.33
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 98 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 99 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 100 - "Nextcalibur 0.5.5"
Cohesion: 0.25
Nodes (6): Asynchronous dispatcher & UI thread hardening, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on

### Community 101 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 102 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 103 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 104 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 105 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 106 - "DriveRow"
Cohesion: 0.29
Nodes (6): INotifyPropertyChanged, DriveRow, Detail, Name, Percent, PercentText

### Community 107 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 108 - "StackPanel"
Cohesion: 0.29
Nodes (7): BannerActions, DialogActions, DriveTextStack, PanelWindowsFaultsSubOptions, ProfileRow, SettingUpdatesAutomatic, StackPanel

### Community 109 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 110 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 111 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 112 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 113 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 114 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 115 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 116 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 117 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 119 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 121 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 122 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 123 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 125 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 126 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 127 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 130 - ".SendAsync"
Cohesion: 0.50
Nodes (3): CancellationToken, HttpRequestMessage, HttpResponseMessage

### Community 131 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 134 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 135 - "Words"
Cohesion: 0.67
Nodes (3): Func, Words, Resolver

## Knowledge Gaps
- **420 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+415 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 801 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **32 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `LedState`, `RunawayWatchTests`, `GpuClockReader`, `.CheckProgram`, `Window`, `RadioButton`, `Nextcalibur.App`, `UserPresence`, `PlanTuning`, `.OnLoaded`, `.Get`, `WindowsFaults`, `AppSettings`, `Fact`, `SystemInfo`, `TrayPresence`, `.RequestRestart`, `SystemMode`, `Button`, `ProcessPresence`, `CpuPowerReader`, `CoolingPolicyTests`, `EcMailbox`, `.OnClosing`, `.Info`, `PowerOverlayService`, `.OnGpuModeChanged`, `.Read`, `DependencyStatus`, `ToggleButton`, `BacklightKeyWatcher`, `LogInjectionTests`, `UpdateService`, `.OnPowerSourceMayHaveChanged`, `Strings`, `Grid`, `GpuModeService`, `.Check`, `.WireSettingsPage`, `.OnSystemModeChanged`, `ThemeService`, `NvidiaDriverState`, `ThermalSample`, `Nextcalibur 0.5.5`, `TourPage`, `.OnBrightnessChanged`?**
  _High betweenness centrality (0.460) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `CpuWarnValue`, `RadioButton`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `Button`, `.OnGpuModeChanged`, `ToggleButton`, `Grid`, `Border`, `Text`, `StackPanel`, `.OnBrightnessChanged`, `Keycaps`, `DriveGauge`?**
  _High betweenness centrality (0.142) - this node is a cross-community bridge._
- **Why does `Nextcalibur.Core.Hardware` connect `Nextcalibur.Core.Hardware` to `Nextcalibur.Core.Power`, `FanTurbo`, `ThermalSample`, `RunawayWatchTests`, `Nextcalibur.App`, `.Check`, `xunit`, `MachineWideGate`, `Nextcalibur.Core.Configuration`?**
  _High betweenness centrality (0.082) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _420 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DotNetRuntimeDependency` be split into smaller, more focused modules?**
  _Cohesion score 0.054527750730282376 - nodes in this community are weakly interconnected._
- **Should `LedState` be split into smaller, more focused modules?**
  _Cohesion score 0.05136986301369863 - nodes in this community are weakly interconnected._