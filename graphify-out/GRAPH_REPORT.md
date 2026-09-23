# Graph Report - nextcalibur-control-center  (2026-09-23)

## Corpus Check
- 143 files · ~197,724 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2417 nodes · 5116 edges · 154 communities (122 shown, 32 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 252 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `6340d7f7`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- Authenticode
- .CheckProgram
- GpuClockReader
- ProtectedStore
- AppSettings
- Window
- IShellLinkW
- UserPresence
- .Get
- SystemInfo
- CpuPowerReader
- RadioButton
- UpdateService
- Nextcalibur.App
- .Main
- WindowsFaults
- SystemMode
- .RequestRestart
- SystemTools
- Fact
- NetworkCostTests
- Nextcalibur.Core.Hardware
- Nextcalibur.Core.Configuration
- ProcessPresence
- WindowsFaultsTests
- Button
- .OfferDependency
- .Info
- Dependency
- Fact
- .Warn
- .OnGpuModeChanged
- .RunLighting
- .Survey
- Elevation
- PowerOverlayService
- .Ask
- DotNetRuntimeDependency
- MemoryTrimmer
- EcMailbox
- .OnLoaded
- Nextcalibur.Core.csproj
- .StartSlowTimer
- analyse-autopsy.py
- Program
- Fact
- SmiCommand
- .Check
- .Read
- ResourceDictionary
- .MayStartWithoutPrompt
- RuntimeDependencyTests
- CoreLoad
- ToggleButton
- .GetColour
- ColourWheel
- microsoft_win32
- Unelevated
- LedController
- LedState
- FanGauge
- ProcessMetrics
- DonutGauge
- Nextcalibur.Core.Tests.Attacks
- SegmentedBar
- ThemeService
- .HasAccess
- GpuModeService
- Nextcalibur.Core.Power
- BatteryModePolicy
- StringsDictionaryTests
- ValueConverters.cs
- DevicePowerState
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- BacklightKeyWatcher
- Nextcalibur Control Center
- Strings
- Grid
- InstallFolderGuard
- Hardware Protocol
- Nextcalibur 0.5.3
- .Sanitised
- NvidiaDriverState
- LedZone
- IntPtr
- Border
- SupportVerdict
- .CalculateThreadShare
- WordsInCodeTests
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- ThermalSample
- LedEffect
- PawnIoDependency
- NduFix
- Probe
- .GetAsync
- Nextcalibur 0.5.1
- Test-Ui.ps1
- Text
- Strings.cs
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Releasing, and signing
- .Pick
- TourPage
- Log-Graphics.ps1
- Privacy
- Security
- .OnBrightnessChanged
- StackPanel
- .Sample
- MachineWideGate
- Notice
- .Set
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Nextcalibur 0.5.6
- .ArrangeOverride
- ThermalProfileTests
- Tools
- 0.5.7.md
- CpuWarnValue
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
1. `MainWindow` - 223 edges
2. `Window` - 164 edges
3. `AppSettings` - 61 edges
4. `Nextcalibur.Core.Hardware` - 42 edges
5. `WindowsFaultsTests` - 40 edges
6. `RadioButton` - 39 edges
7. `TextBlock` - 36 edges
8. `TrayPresence` - 33 edges
9. `WindowsFaults` - 33 edges
10. `GpuClockReader` - 31 edges

## Surprising Connections (you probably didn't know these)
- `Deep memory & resource leak eradication` --references--> `MainWindow`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.App/MainWindow.Integrity.cs
- `Deep memory & resource leak eradication` --references--> `CpuPowerReader`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.Core/Hardware/CpuPowerReader.cs
- `Zero-bottleneck gaming & heavy workload architecture` --references--> `WindowsFaults`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.Core/Hardware/WindowsFaults.cs
- `Fix GPU Switch Restart Prompt & Windows Privilege Adjustment` --references--> `TokenPrivileges`  [INFERRED]
  docs/releases/0.5.8.md → src/Nextcalibur.App/MainWindow.xaml.cs
- `Zero-bottleneck gaming & heavy workload architecture` --references--> `MemoryTrimmer`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.Core/Hardware/MemoryTrimmer.cs

## Import Cycles
- None detected.

## Communities (154 total, 32 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.05
Nodes (42): DeferredOverheat, KeyEventArgs, ObservableCollection, Queue, SizeChangedEventArgs, DateTime, HashSet, List (+34 more)

### Community 1 - "Authenticode"
Cohesion: 0.08
Nodes (27): CatalogInfo, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle, X509Certificate2 (+19 more)

### Community 2 - ".CheckProgram"
Cohesion: 0.07
Nodes (23): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvmlPath, IntegrityFinding (+15 more)

### Community 3 - "GpuClockReader"
Cohesion: 0.09
Nodes (18): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+10 more)

### Community 4 - "ProtectedStore"
Cohesion: 0.10
Nodes (17): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+9 more)

### Community 5 - "AppSettings"
Cohesion: 0.06
Nodes (36): DateTime, Dictionary, JsonElement, List, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoInstallUpdates (+28 more)

### Community 6 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 7 - "IShellLinkW"
Cohesion: 0.07
Nodes (13): PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder, AppIdentity (+5 more)

### Community 8 - "UserPresence"
Cohesion: 0.10
Nodes (20): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr, MarshalAs (+12 more)

### Community 9 - ".Get"
Cohesion: 0.09
Nodes (14): CancelEventArgs, ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, MessageBoxButton, MessageBoxResult (+6 more)

### Community 10 - "SystemInfo"
Cohesion: 0.08
Nodes (22): MemoryStatusEx, DriveUse, INotifyPropertyChanged, DriveRow, Detail, Name, Percent, PercentText (+14 more)

### Community 11 - "CpuPowerReader"
Cohesion: 0.09
Nodes (21): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+13 more)

### Community 12 - "RadioButton"
Cohesion: 0.07
Nodes (33): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeGaming, ModeOffice (+25 more)

### Community 13 - "UpdateService"
Cohesion: 0.07
Nodes (28): Message, Ok, RestartRequired, DispatcherTimer, Func, HashSet, IProgress, Task (+20 more)

### Community 14 - "Nextcalibur.App"
Cohesion: 0.09
Nodes (22): Nextcalibur.App, Nextcalibur.App.Controls, system_drawing, system_io_directory, system_io_ioexception, system_io_path, system_linq, system_runtime_interopservices_comtypes (+14 more)

### Community 15 - ".Main"
Cohesion: 0.12
Nodes (8): The threat model, and what the code does about it, Application, DllImport, Mutex, App, RegistryKey, Footprint, STAThread

### Community 16 - "WindowsFaults"
Cohesion: 0.12
Nodes (20): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, DateTime, DllImport, Func (+12 more)

### Community 17 - "SystemMode"
Cohesion: 0.14
Nodes (13): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, SystemMode (+5 more)

### Community 18 - ".RequestRestart"
Cohesion: 0.10
Nodes (11): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, BannerRestartButton, DllImport, EventArgs, IntPtr (+3 more)

### Community 19 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 20 - "Fact"
Cohesion: 0.13
Nodes (11): IList, GpuLoad, Fact, InlineData, Theory, GpuAwakeFaultConditionTests, GpuModeTests, Discrete (+3 more)

### Community 21 - "NetworkCostTests"
Cohesion: 0.14
Nodes (19): Bytes, Count, HttpMessageHandler, CancellationToken, Dictionary, Fact, HttpRequestMessage, HttpResponseMessage (+11 more)

### Community 22 - "Nextcalibur.Core.Hardware"
Cohesion: 0.11
Nodes (12): Nextcalibur.Core.Hardware, microsoft_win32_safehandles, FaultEvaluation, FaultFound, system_componentmodel, system_diagnostics, system_io, system_management (+4 more)

### Community 23 - "Nextcalibur.Core.Configuration"
Cohesion: 0.14
Nodes (9): Nextcalibur.Core.Tests, Nextcalibur.Core.Security, Nextcalibur.Core.Configuration, system_security_cryptography_x509certificates, system_text_regularexpressions, system_xml_linq, Fact, TaskFolderTests (+1 more)

### Community 24 - "ProcessPresence"
Cohesion: 0.13
Nodes (11): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+3 more)

### Community 26 - "Button"
Cohesion: 0.07
Nodes (21): IsChecked, BannerDismissButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, HideToTrayButton, MinimiseButton, OpenLogButton (+13 more)

### Community 27 - ".OfferDependency"
Cohesion: 0.11
Nodes (10): Asynchronous dispatcher & UI thread hardening, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, UpdateNowButton, Task (+2 more)

### Community 28 - ".Info"
Cohesion: 0.15
Nodes (9): Exception, Log, Folder, Action, Fact, InlineData, Regex, Theory (+1 more)

### Community 29 - "Dependency"
Cohesion: 0.11
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 30 - "Fact"
Cohesion: 0.11
Nodes (14): OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact, Task (+6 more)

### Community 31 - ".Warn"
Cohesion: 0.14
Nodes (8): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, FixButton, Action, ToggleButton

### Community 32 - ".OnGpuModeChanged"
Cohesion: 0.11
Nodes (9): ModeDiscrete, ModeHybrid, ModeUma, FirmwareModeReading, GpuConfiguration, GpuMode, Discrete, Hybrid (+1 more)

### Community 33 - ".RunLighting"
Cohesion: 0.10
Nodes (6): Slider, LedPower, Action, Color, RadioButton, TextBox

### Community 34 - ".Survey"
Cohesion: 0.16
Nodes (6): Func, IEnumerable, IReadOnlyList, Version, RetirementEntry, FootprintTests

### Community 35 - "Elevation"
Cohesion: 0.17
Nodes (4): dynamic, Encoding, Elevation, CardSwitchTasks

### Community 36 - "PowerOverlayService"
Cohesion: 0.17
Nodes (10): Option, Button, IEnumerable, DllImport, Guid, IReadOnlyList, PowerModeOption, PowerOverlays (+2 more)

### Community 37 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 38 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 39 - "MemoryTrimmer"
Cohesion: 0.14
Nodes (14): Zero-bottleneck gaming & heavy workload architecture, MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX (+6 more)

### Community 40 - "EcMailbox"
Cohesion: 0.18
Nodes (9): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+1 more)

### Community 41 - ".OnLoaded"
Cohesion: 0.19
Nodes (7): AssemblyInformationalVersionAttribute, PowerModeChangedEventArgs, PowerModeBalanced, PowerModeBest, PowerModeBetter, PowerModeEfficiency, RoutedEventArgs

### Community 42 - "Nextcalibur.Core.csproj"
Cohesion: 0.12
Nodes (13): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Management (10.0.12), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk, net10.0-windows (+5 more)

### Community 43 - ".StartSlowTimer"
Cohesion: 0.13
Nodes (3): SolidColorBrush, Func, UninstallCommand

### Community 44 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 45 - "Program"
Cohesion: 0.18
Nodes (5): ConsoleColor, B, G, R, Program

### Community 46 - "Fact"
Cohesion: 0.17
Nodes (6): Fact, ForwardCompatibilityTests, IdempotenceTests, MailboxAccessTests, SettingsTests, StartupPreferenceTests

### Community 47 - "SmiCommand"
Cohesion: 0.19
Nodes (6): ArgumentException, SmiCommand, Fact, InlineData, Theory, SmiCommandTests

### Community 48 - ".Check"
Cohesion: 0.20
Nodes (7): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported

### Community 49 - ".Read"
Cohesion: 0.15
Nodes (11): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+3 more)

### Community 50 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 51 - ".MayStartWithoutPrompt"
Cohesion: 0.22
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 52 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 53 - "CoreLoad"
Cohesion: 0.12
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 54 - "ToggleButton"
Cohesion: 0.12
Nodes (16): OverheatWarningToggle, SelectAll, SettingAutoCheckUpdates, SettingAutoInstallUpdates, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost, SettingFixWidgets (+8 more)

### Community 55 - ".GetColour"
Cohesion: 0.24
Nodes (7): B, G, R, Fact, InlineData, Theory, LedStateTests

### Community 56 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 57 - "microsoft_win32"
Cohesion: 0.17
Nodes (8): microsoft_win32, system_security, system_security_accesscontrol, system_security_cryptography, system_security_principal, SecurityIdentifier, ProtectedStoreElevatedTests, Elevated

### Community 58 - "Unelevated"
Cohesion: 0.33
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 59 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 60 - "LedState"
Cohesion: 0.14
Nodes (14): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedState, ActiveProfile, BrightnessPercent (+6 more)

### Community 61 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 62 - "ProcessMetrics"
Cohesion: 0.15
Nodes (14): Share, Dictionary, TimeSpan, FaultEvidence, ProcessMetrics, Name, PageFaultCount, Pid (+6 more)

### Community 63 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 64 - "Nextcalibur.Core.Tests.Attacks"
Cohesion: 0.30
Nodes (6): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_text_json

### Community 65 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 66 - "ThemeService"
Cohesion: 0.15
Nodes (11): Theme, Theme, Dark, Light, ThemePreference, Dark, Light, System (+3 more)

### Community 67 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 68 - "GpuModeService"
Cohesion: 0.32
Nodes (4): ArgumentNullException, GpuModeService, SwitchOutcome, SwitchOutcome

### Community 69 - "Nextcalibur.Core.Power"
Cohesion: 0.17
Nodes (7): Nextcalibur.Cli, Nextcalibur.Core.Power, Trace, DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 70 - "BatteryModePolicy"
Cohesion: 0.42
Nodes (4): BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

### Community 71 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 72 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 73 - "DevicePowerState"
Cohesion: 0.32
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 74 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 75 - "Nextcalibur 0.5.4"
Cohesion: 0.17
Nodes (11): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+3 more)

### Community 76 - "BacklightKeyWatcher"
Cohesion: 0.20
Nodes (7): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, Fact, InlineData, Theory, BacklightKeyTests

### Community 77 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 78 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 79 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 80 - "InstallFolderGuard"
Cohesion: 0.35
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 81 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 82 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 83 - ".Sanitised"
Cohesion: 0.29
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 84 - "NvidiaDriverState"
Cohesion: 0.27
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 85 - "LedZone"
Cohesion: 0.18
Nodes (10): LedBrightness, Full, Half, Off, LedZone, AllKeyboard, Everything, Left (+2 more)

### Community 86 - "IntPtr"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 87 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 88 - "SupportVerdict"
Cohesion: 0.22
Nodes (9): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+1 more)

### Community 89 - ".CalculateThreadShare"
Cohesion: 0.27
Nodes (4): IReadOnlyDictionary, Stable, Dictionary, TopSharePercent

### Community 90 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 91 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 92 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 93 - "README.md"
Cohesion: 0.33
Nodes (3): How it works, Questions people ask, Using it

### Community 94 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 95 - "ThermalSample"
Cohesion: 0.33
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 96 - "LedEffect"
Cohesion: 0.22
Nodes (8): LedEffect, Blink, Breathing, ColourCycle, Heartbeat, Off, Static, Wave

### Community 97 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 99 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 100 - ".GetAsync"
Cohesion: 0.29
Nodes (7): Answer, ConcurrentDictionary, CancellationToken, HttpClient, Task, Answer, Conditional

### Community 101 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 102 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 103 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 104 - "Strings.cs"
Cohesion: 0.33
Nodes (5): Nextcalibur.Core, Func, Words, Resolver, system_globalization

### Community 105 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 106 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 107 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 108 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

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

### Community 114 - "StackPanel"
Cohesion: 0.33
Nodes (6): BannerActions, DialogActions, DriveTextStack, PanelWindowsFaultsSubOptions, ProfileRow, StackPanel

### Community 116 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 117 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

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

### Community 124 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 127 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 130 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

## Knowledge Gaps
- **394 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+389 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 745 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **32 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `.CheckProgram`, `GpuClockReader`, `AppSettings`, `Window`, `UserPresence`, `.Get`, `SystemInfo`, `CpuPowerReader`, `RadioButton`, `UpdateService`, `Nextcalibur.App`, `WindowsFaults`, `SystemMode`, `.RequestRestart`, `ProcessPresence`, `Button`, `.OfferDependency`, `.Info`, `.Warn`, `.OnGpuModeChanged`, `.RunLighting`, `PowerOverlayService`, `EcMailbox`, `.OnLoaded`, `.StartSlowTimer`, `.Read`, `ToggleButton`, `LedController`, `ThemeService`, `GpuModeService`, `BatteryModePolicy`, `BacklightKeyWatcher`, `Strings`, `Grid`, `NvidiaDriverState`, `LedZone`, `SupportVerdict`, `ThermalSample`, `LedEffect`, `TourPage`, `.OnBrightnessChanged`, `.Sample`?**
  _High betweenness centrality (0.454) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `CpuWarnValue`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `RadioButton`, `Wheel`, `.RequestRestart`, `Button`, `.OfferDependency`, `.Warn`, `.OnGpuModeChanged`, `.RunLighting`, `.OnLoaded`, `ToggleButton`, `Grid`, `Border`, `Text`, `.OnBrightnessChanged`, `StackPanel`, `Keycaps`, `DriveGauge`?**
  _High betweenness centrality (0.159) - this node is a cross-community bridge._
- **Why does `DependencyStatus` connect `UpdateService` to `MainWindow`, `Nextcalibur.Core.Tests.Attacks`, `.OfferDependency`, `Dependency`?**
  _High betweenness centrality (0.098) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _394 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.04689265536723164 - nodes in this community are weakly interconnected._
- **Should `Authenticode` be split into smaller, more focused modules?**
  _Cohesion score 0.08348457350272233 - nodes in this community are weakly interconnected._