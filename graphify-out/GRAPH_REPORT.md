# Graph Report - nextcalibur-control-center  (2026-10-11)

## Corpus Check
- 166 files · ~218,459 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2793 nodes · 6024 edges · 160 communities (127 shown, 33 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 318 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `fb7a1394`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- DotNetRuntimeDependency
- MainWindow
- LedState
- Authenticode
- .CheckProgram
- FnShortcut
- UserPresence
- RunawayWatchTests
- GpuClockReader
- IShellLinkW
- .OnLoaded
- SystemMode
- .Get
- Window
- Nextcalibur.Core.Configuration
- PowerOverlayService
- PlanTuning
- TrayPresence
- CpuPowerReader
- SystemInfo
- RadioButton
- RoutedEventArgs
- AppSettings
- MachineScanRecord
- .Warn
- .WireSettingsPage
- WindowsFaults
- .RequestRestart
- Fact
- Nextcalibur.Core.Hardware
- ProcessPresence
- WindowsFaultsTests
- CoolingPolicyTests
- ProtectedStore
- Button
- Fact
- .Info
- Nextcalibur.Core.Security
- system_runtime_interopservices
- Elevation
- EcMailbox
- GpuModeService
- .PlaceTourStep
- .Check
- .Read
- Nextcalibur.Core.csproj
- RestorePoint
- BacklightKeyWatcher
- DependencyStatus
- analyse-autopsy.py
- Program
- BatteryModePolicy
- ResourceDictionary
- Nextcalibur.Core.Power
- .Read
- ColourWheel
- DevicePowerState
- .Ask
- CoreLoad
- ToggleButton
- FanGauge
- HostileFilesTests
- DonutGauge
- ReleaseFile
- system_diagnostics
- SegmentedBar
- ProcessMetrics
- .HasAccess
- .Record
- SystemTools
- PendingRestartInfo
- StringsDictionaryTests
- ValueConverters.cs
- Nextcalibur 0.5.2
- Nextcalibur Control Center
- Strings
- Grid
- Dependency
- SmiCommandTests
- Hardware Protocol
- Nextcalibur 0.5.3
- Border
- ThermalSample
- ThemeService
- NvidiaDriverState
- .CalculateThreadShare
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- .Migrate
- PawnIoDependency
- ProfileFiles
- Probe
- Nextcalibur 0.5.1
- StackPanel
- IntegrityKind
- ProtectedStoreElevatedTests
- Test-Ui.ps1
- Text
- Strings.cs
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- .Pick
- TourPage
- NduFix
- Log-Graphics.ps1
- Privacy
- Nextcalibur 0.5.5
- Security
- .OnBrightnessChanged
- .OnThresholdMoved
- MachineWideGate
- Notice
- Nextcalibur 0.5.11
- .Set
- .MayStartWithoutPrompt
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- .ArrangeOverride
- .SendAsync
- .Modules_are_the_released_ones
- Tools
- 0.5.7.md
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
1. `MainWindow` - 288 edges
2. `Window` - 169 edges
3. `AppSettings` - 70 edges
4. `Nextcalibur.Core.Hardware` - 52 edges
5. `WindowsFaultsTests` - 40 edges
6. `RadioButton` - 39 edges
7. `TextBlock` - 36 edges
8. `Nextcalibur.Core.Configuration` - 35 edges
9. `WindowsFaults` - 33 edges
10. `GpuClockReader` - 31 edges

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

## Communities (160 total, 33 thin omitted)

### Community 0 - "DotNetRuntimeDependency"
Cohesion: 0.05
Nodes (46): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+38 more)

### Community 1 - "MainWindow"
Cohesion: 0.04
Nodes (42): CheckBox, DeferredOverheat, HwndSource, ObservableCollection, Queue, MainWindow, DateTime, CoolingWantsReadings (+34 more)

### Community 2 - "LedState"
Cohesion: 0.05
Nodes (42): B, Dictionary, G, R, LedProfile, BrightnessPercent, Colours, Effect (+34 more)

### Community 3 - "Authenticode"
Cohesion: 0.06
Nodes (38): CatalogInfo, Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on (+30 more)

### Community 4 - ".CheckProgram"
Cohesion: 0.06
Nodes (23): Lazy, ProcessModule, FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Dictionary, HashSet (+15 more)

### Community 5 - "FnShortcut"
Cohesion: 0.06
Nodes (37): Problem, Detail, Dictionary, KeyEventArgs, DllImport, IEnumerable, IntPtr, List (+29 more)

### Community 6 - "UserPresence"
Cohesion: 0.06
Nodes (35): MEMORYSTATUSEX, MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, Point, Size, DateTime (+27 more)

### Community 7 - "RunawayWatchTests"
Cohesion: 0.06
Nodes (38): Cpu, CpuMs, DllImport, IntPtr, Name, DateTime, Dictionary, Func (+30 more)

### Community 8 - "GpuClockReader"
Cohesion: 0.08
Nodes (22): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+14 more)

### Community 9 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 10 - ".OnLoaded"
Cohesion: 0.08
Nodes (16): AssemblyInformationalVersionAttribute, Asynchronous dispatcher & UI thread hardening, EventArgs, DateTime, DispatcherTimer, HashSet, IProgress, Task (+8 more)

### Community 11 - "SystemMode"
Cohesion: 0.10
Nodes (14): InvalidOperationException, PowerModeChangedEventArgs, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList (+6 more)

### Community 12 - ".Get"
Cohesion: 0.08
Nodes (11): SolidColorBrush, ModeDiscrete, ModeHybrid, ModeUma, Func, MessageBoxButton, MessageBoxResult, Task (+3 more)

### Community 13 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 14 - "Nextcalibur.Core.Configuration"
Cohesion: 0.09
Nodes (24): Nextcalibur.App, Nextcalibur.App.Controls, Nextcalibur.Core.Configuration, system_componentmodel, system_drawing, system_io, system_linq, system_runtime_compilerservices (+16 more)

### Community 15 - "PowerOverlayService"
Cohesion: 0.08
Nodes (22): DllImport, Guid, IReadOnlyList, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, PowerModeOption (+14 more)

### Community 16 - "PlanTuning"
Cohesion: 0.13
Nodes (16): AcDc, Floor, Original, Plan, Action, DllImport, Guid, IntPtr (+8 more)

### Community 17 - "TrayPresence"
Cohesion: 0.10
Nodes (17): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, ProcessInformation, List, Ms (+9 more)

### Community 18 - "CpuPowerReader"
Cohesion: 0.09
Nodes (21): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+13 more)

### Community 19 - "SystemInfo"
Cohesion: 0.08
Nodes (21): MemoryStatusEx, DriveUse, INotifyPropertyChanged, DriveRow, Name, Percent, PercentText, DllImport (+13 more)

### Community 20 - "RadioButton"
Cohesion: 0.08
Nodes (33): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeGaming, ModeOffice (+25 more)

### Community 21 - "RoutedEventArgs"
Cohesion: 0.06
Nodes (16): Option, BannerRestartButton, LedPower, PowerModeBalanced, PowerModeBest, PowerModeBetter, PowerModeEfficiency, ReloadButton (+8 more)

### Community 22 - "AppSettings"
Cohesion: 0.05
Nodes (35): FnShortcut, JsonElement, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoCooling, AutoFanTurbo, AutoInstallUpdates (+27 more)

### Community 23 - "MachineScanRecord"
Cohesion: 0.07
Nodes (27): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites (+19 more)

### Community 24 - ".Warn"
Cohesion: 0.12
Nodes (8): The threat model, and what the code does about it, Application, DllImport, Mutex, App, RegistryKey, Footprint, STAThread

### Community 25 - ".WireSettingsPage"
Cohesion: 0.10
Nodes (11): CancelEventArgs, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary, IReadOnlyDictionary (+3 more)

### Community 26 - "WindowsFaults"
Cohesion: 0.11
Nodes (21): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, DateTime, DllImport, Func (+13 more)

### Community 27 - ".RequestRestart"
Cohesion: 0.10
Nodes (11): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr (+3 more)

### Community 28 - "Fact"
Cohesion: 0.16
Nodes (9): Func, IEnumerable, IReadOnlyList, RetirementEntry, Trace, Version, Fact, FootprintTests (+1 more)

### Community 29 - "Nextcalibur.Core.Hardware"
Cohesion: 0.15
Nodes (5): Nextcalibur.Core.Tests, Nextcalibur.Core.Hardware, system_text_regularexpressions, system_xml_linq, xunit

### Community 30 - "ProcessPresence"
Cohesion: 0.12
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 31 - "WindowsFaultsTests"
Cohesion: 0.20
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 32 - "CoolingPolicyTests"
Cohesion: 0.12
Nodes (16): At, Margin, DateTime, List, TimeSpan, CoolingPolicy, DegreesPerMinute, Engaged (+8 more)

### Community 33 - "ProtectedStore"
Cohesion: 0.13
Nodes (13): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+5 more)

### Community 34 - "Button"
Cohesion: 0.07
Nodes (21): IsChecked, BannerDismissButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, HideToTrayButton, MinimiseButton (+13 more)

### Community 35 - "Fact"
Cohesion: 0.16
Nodes (8): IList, HardwareSupport, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 36 - ".Info"
Cohesion: 0.14
Nodes (9): Exception, Log, Folder, Action, Fact, InlineData, Regex, Theory (+1 more)

### Community 37 - "Nextcalibur.Core.Security"
Cohesion: 0.14
Nodes (9): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_security_cryptography_x509certificates, system_text_json (+1 more)

### Community 38 - "system_runtime_interopservices"
Cohesion: 0.09
Nodes (8): Nextcalibur.Core.Input, system_io_directory, system_io_ioexception, system_io_path, system_management, system_runtime_interopservices, system_runtime_interopservices_comtypes, system_text

### Community 39 - "Elevation"
Cohesion: 0.14
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 40 - "EcMailbox"
Cohesion: 0.16
Nodes (10): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+2 more)

### Community 41 - "GpuModeService"
Cohesion: 0.17
Nodes (10): ArgumentNullException, FirmwareModeReading, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma, GpuModeService (+2 more)

### Community 42 - ".PlaceTourStep"
Cohesion: 0.11
Nodes (10): SizeChangedEventArgs, FrameworkElement, KeyEventArgs, RoutedEventArgs, TourStep, Body, SectionName, Title (+2 more)

### Community 43 - ".Check"
Cohesion: 0.15
Nodes (8): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported, MailboxAccessTests

### Community 44 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 45 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.12), System.Management (10.0.12), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk (+6 more)

### Community 46 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 47 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 48 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 49 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 50 - "Program"
Cohesion: 0.18
Nodes (5): ConsoleColor, B, G, R, Program

### Community 51 - "BatteryModePolicy"
Cohesion: 0.23
Nodes (8): DllImport, BatteryModePolicy, Remembered, PowerSource, SystemPowerStatus, SystemPowerStatus, Fact, BatteryModePolicyTests

### Community 52 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 53 - "Nextcalibur.Core.Power"
Cohesion: 0.14
Nodes (8): Nextcalibur.Core.Power, microsoft_win32, microsoft_win32_safehandles, Theme, Dark, Light, system_reflection, system_security

### Community 54 - ".Read"
Cohesion: 0.30
Nodes (3): Fact, List, ProtectedStoreTests

### Community 55 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 56 - "DevicePowerState"
Cohesion: 0.25
Nodes (6): DevPropKey, Discrete GPU sleep protection in Hybrid mode, DllImport, Guid, DevicePowerState, DevPropKey

### Community 57 - ".Ask"
Cohesion: 0.35
Nodes (7): HttpStatusCode, Fact, InlineData, Task, Theory, Answer, HostileAnswerTests

### Community 58 - "CoreLoad"
Cohesion: 0.13
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 59 - "ToggleButton"
Cohesion: 0.12
Nodes (16): OverheatWarningToggle, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost, SettingFixWidgets, SettingFnShortcuts (+8 more)

### Community 60 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 61 - "HostileFilesTests"
Cohesion: 0.27
Nodes (5): IOException, Fact, InlineData, Theory, HostileFilesTests

### Community 62 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 63 - "ReleaseFile"
Cohesion: 0.22
Nodes (9): CancellationToken, HttpClient, Task, Uri, ReleaseFile, CancellationToken, HttpClient, Task (+1 more)

### Community 64 - "system_diagnostics"
Cohesion: 0.23
Nodes (5): Nextcalibur.Cli, system_diagnostics, system_security_accesscontrol, system_security_cryptography, system_security_principal

### Community 65 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 66 - "ProcessMetrics"
Cohesion: 0.15
Nodes (13): Share, Dictionary, TimeSpan, FaultEvidence, ProcessMetrics, Name, PageFaultCount, SessionId (+5 more)

### Community 67 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 68 - ".Record"
Cohesion: 0.20
Nodes (7): IReadOnlyDictionary, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Fact, AuditLogTests

### Community 69 - "SystemTools"
Cohesion: 0.17
Nodes (9): FileStream, SystemTools, Cmd, Explorer, Pnputil, Powercfg, PowerShell, Schtasks (+1 more)

### Community 70 - "PendingRestartInfo"
Cohesion: 0.19
Nodes (7): DateTime, Dictionary, List, PendingRestartInfo, BootTimeUtc, ReasonArguments, Reasons

### Community 71 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 72 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 73 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 74 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 75 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 76 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 77 - "Dependency"
Cohesion: 0.17
Nodes (9): Version, Dependency, ExpectedSigner, Id, MayBeRemoved, Name, Purpose, SilentInstallArguments (+1 more)

### Community 78 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 79 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 80 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 81 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 82 - "ThermalSample"
Cohesion: 0.29
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 83 - "ThemeService"
Cohesion: 0.20
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 84 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 85 - ".CalculateThreadShare"
Cohesion: 0.27
Nodes (4): IReadOnlyDictionary, Stable, Dictionary, TopSharePercent

### Community 86 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 87 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 88 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 89 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 90 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 91 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 93 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 95 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 96 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 97 - "StackPanel"
Cohesion: 0.25
Nodes (8): BannerActions, DialogActions, DriveTextStack, FnShortcutRows, PanelWindowsFaultsSubOptions, ProfileRow, SettingUpdatesAutomatic, StackPanel

### Community 98 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 99 - "ProtectedStoreElevatedTests"
Cohesion: 0.32
Nodes (4): Fact, SecurityIdentifier, ProtectedStoreElevatedTests, Elevated

### Community 100 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 101 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 102 - "Strings.cs"
Cohesion: 0.33
Nodes (5): Nextcalibur.Core, Func, Words, Resolver, system_globalization

### Community 103 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 104 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 105 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 106 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 107 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 108 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 110 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 111 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 112 - "Nextcalibur 0.5.5"
Cohesion: 0.33
Nodes (4): Deep memory & resource leak eradication, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on

### Community 113 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 114 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 116 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 117 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 118 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 121 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 123 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 124 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 125 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 126 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 127 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 128 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 130 - ".SendAsync"
Cohesion: 0.50
Nodes (3): CancellationToken, HttpRequestMessage, HttpResponseMessage

### Community 131 - ".Modules_are_the_released_ones"
Cohesion: 0.50
Nodes (3): InlineData, Theory, PawnIoModuleTests

### Community 132 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 135 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 136 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

## Knowledge Gaps
- **459 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+454 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 867 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **33 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `LedState`, `FnShortcut`, `UserPresence`, `RunawayWatchTests`, `GpuClockReader`, `IShellLinkW`, `.OnLoaded`, `SystemMode`, `.Get`, `Window`, `Nextcalibur.Core.Configuration`, `PowerOverlayService`, `PlanTuning`, `TrayPresence`, `CpuPowerReader`, `SystemInfo`, `RadioButton`, `RoutedEventArgs`, `AppSettings`, `MachineScanRecord`, `.WireSettingsPage`, `WindowsFaults`, `.RequestRestart`, `ProcessPresence`, `CoolingPolicyTests`, `Button`, `.Info`, `EcMailbox`, `GpuModeService`, `.PlaceTourStep`, `.Read`, `BacklightKeyWatcher`, `DependencyStatus`, `BatteryModePolicy`, `Strings`, `Grid`, `ThermalSample`, `ThemeService`, `NvidiaDriverState`, `TourPage`, `Nextcalibur 0.5.5`, `.OnBrightnessChanged`, `.OnThresholdMoved`?**
  _High betweenness centrality (0.459) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `CpuWarnValue`, `DialogCheck`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `.Get`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `RadioButton`, `RoutedEventArgs`, `Button`, `ToggleButton`, `Grid`, `Border`, `StackPanel`, `Text`, `.OnBrightnessChanged`, `Keycaps`, `DriveGauge`?**
  _High betweenness centrality (0.113) - this node is a cross-community bridge._
- **Why does `AppSettings` connect `AppSettings` to `MainWindow`, `LedState`, `Nextcalibur.Core.Security`, `PendingRestartInfo`, `.OnLoaded`, `Strings`, `SystemMode`, `NduFix`, `Fact`, `TrayPresence`, `ThemeService`, `.Read`, `.Migrate`, `HostileFilesTests`, `WindowsFaultsTests`?**
  _High betweenness centrality (0.076) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _459 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DotNetRuntimeDependency` be split into smaller, more focused modules?**
  _Cohesion score 0.054527750730282376 - nodes in this community are weakly interconnected._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.04 - nodes in this community are weakly interconnected._