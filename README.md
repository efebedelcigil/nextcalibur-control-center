# Graph Report - nextcalibur-control-center  (2026-10-11)

## Corpus Check
- 160 files · ~213,006 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2728 nodes · 5851 edges · 170 communities (133 shown, 37 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 302 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `88f9fd03`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- DotNetRuntimeDependency
- MainWindow
- UserPresence
- GpuClockReader
- FnShortcut
- RunawayWatchTests
- ProtectedStore
- Window
- IShellLinkW
- AppSettings
- RadioButton
- PlanTuning
- .Info
- TrayPresence
- SystemInfo
- CpuPowerReader
- .Get
- WindowsFaults
- MachineScanRecord
- Button
- ProcessPresence
- SystemTools
- WindowsFaultsTests
- .RequestRestart
- SystemMode
- CoolingPolicyTests
- Nextcalibur.Core.Hardware
- .RunFnAction
- EcMailbox
- Fact
- .OnGpuModeChanged
- LedState
- .SignatureIsValid
- .Survey
- .PlaceTourStep
- Authenticode
- Nextcalibur.Core.Configuration
- DevicePowerState
- Nextcalibur.Core.Security
- .Read
- .Get
- Nextcalibur.App
- Integrity
- PowerOverlayService
- ToggleButton
- .CheckProgram
- Program
- MainWindow.FnKeys.cs
- .Main
- Nextcalibur.Core.csproj
- DependencyStatus
- analyse-autopsy.py
- .Check
- UpdateService
- ResourceDictionary
- CoreLoad
- Fact
- ColourWheel
- .OnClosing
- Elevation
- .Ask
- system_runtime_interopservices
- LedController
- .MayStartWithoutPrompt
- FanGauge
- DonutGauge
- SegmentedBar
- LedEffect
- ThemeService
- InstallFolderGuard
- .HasAccess
- LogInjectionTests
- .Capture
- BacklightKeyWatcher
- LedBrightness
- .Migrate
- Footprint
- PawnIoDependency
- BatteryModePolicy
- StringsDictionaryTests
- ValueConverters.cs
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- Nextcalibur Control Center
- Strings
- Grid
- Dependency
- SmiCommandTests
- Hardware Protocol
- Nextcalibur 0.5.3
- CardSwitchTasks
- .WireSettingsPage
- Log
- ReleaseFile
- NvidiaDriverState
- .PowerModeControls
- .OnSystemModeChanged
- Border
- .Sanitised
- .CalculateThreadShare
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- ThermalSample
- NduFix
- .IsSystemFileSignedBy
- Probe
- Nextcalibur 0.5.1
- StackPanel
- IntegrityKind
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Releasing, and signing
- .Pick
- TourPage
- BatteryModePolicy.cs
- Log-Graphics.ps1
- Privacy
- Security
- .OnBrightnessChanged
- .ForegroundProcess
- .LoadGpuMode
- LedZone
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
- .SendAsync
- Tools
- 0.5.7.md
- CpuWarnValue
- ColGauge
- DialogProgress
- DriveDot
- DriveList
- EffectPanel
- FnSelectAll
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
1. `MainWindow` - 279 edges
2. `Window` - 168 edges
3. `AppSettings` - 70 edges
4. `Nextcalibur.Core.Hardware` - 51 edges
5. `WindowsFaultsTests` - 40 edges
6. `RadioButton` - 39 edges
7. `TextBlock` - 36 edges
8. `Nextcalibur.Core.Configuration` - 33 edges
9. `WindowsFaults` - 33 edges
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

## Communities (170 total, 37 thin omitted)

### Community 0 - "DotNetRuntimeDependency"
Cohesion: 0.05
Nodes (46): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+38 more)

### Community 1 - "MainWindow"
Cohesion: 0.04
Nodes (47): CheckBox, DeferredOverheat, HwndSource, ObservableCollection, Problem, Queue, Slider, DateTime (+39 more)

### Community 2 - "UserPresence"
Cohesion: 0.06
Nodes (33): MEMORYSTATUSEX, MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, Dictionary, DllImport (+25 more)

### Community 3 - "GpuClockReader"
Cohesion: 0.07
Nodes (26): IDisposable, NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr (+18 more)

### Community 4 - "FnShortcut"
Cohesion: 0.06
Nodes (34): Nextcalibur.Core.Input, DllImport, IEnumerable, IntPtr, List, StringBuilder, FnAction, FanTurbo (+26 more)

### Community 5 - "RunawayWatchTests"
Cohesion: 0.06
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 6 - "ProtectedStore"
Cohesion: 0.09
Nodes (20): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+12 more)

### Community 7 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 8 - "IShellLinkW"
Cohesion: 0.07
Nodes (13): PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder, AppIdentity (+5 more)

### Community 9 - "AppSettings"
Cohesion: 0.05
Nodes (40): Dictionary, FnShortcut, JsonElement, List, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoCooling (+32 more)

### Community 10 - "RadioButton"
Cohesion: 0.07
Nodes (37): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeGaming, ModeOffice (+29 more)

### Community 11 - "PlanTuning"
Cohesion: 0.13
Nodes (16): AcDc, Floor, Original, Plan, Action, DllImport, Guid, IntPtr (+8 more)

### Community 12 - ".Info"
Cohesion: 0.09
Nodes (5): AssemblyInformationalVersionAttribute, Asynchronous dispatcher & UI thread hardening, SolidColorBrush, KeyEventArgs, EventArgs

### Community 13 - "TrayPresence"
Cohesion: 0.10
Nodes (17): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, ProcessInformation, List, Ms (+9 more)

### Community 14 - "SystemInfo"
Cohesion: 0.08
Nodes (22): MemoryStatusEx, DriveUse, INotifyPropertyChanged, DriveRow, Detail, Name, Percent, PercentText (+14 more)

### Community 15 - "CpuPowerReader"
Cohesion: 0.09
Nodes (21): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+13 more)

### Community 16 - ".Get"
Cohesion: 0.10
Nodes (7): Func, MessageBoxButton, MessageBoxResult, Task, Action, Toasts, IntegrityFinding

### Community 17 - "WindowsFaults"
Cohesion: 0.09
Nodes (29): Process, Share, DateTime, Dictionary, Func, IReadOnlyList, List, TimeSpan (+21 more)

### Community 18 - "MachineScanRecord"
Cohesion: 0.07
Nodes (27): JsonSerializerOptions, IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+19 more)

### Community 19 - "Button"
Cohesion: 0.07
Nodes (24): IsChecked, BannerDismissButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, HideToTrayButton, MinimiseButton (+16 more)

### Community 20 - "ProcessPresence"
Cohesion: 0.11
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 21 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 23 - ".RequestRestart"
Cohesion: 0.11
Nodes (11): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr (+3 more)

### Community 24 - "SystemMode"
Cohesion: 0.15
Nodes (13): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, SystemMode (+5 more)

### Community 25 - "CoolingPolicyTests"
Cohesion: 0.12
Nodes (16): At, Margin, DateTime, List, TimeSpan, CoolingPolicy, DegreesPerMinute, Engaged (+8 more)

### Community 26 - "Nextcalibur.Core.Hardware"
Cohesion: 0.16
Nodes (5): Nextcalibur.Core.Tests, Nextcalibur.Core.Hardware, system_text_regularexpressions, system_xml_linq, xunit

### Community 27 - ".RunFnAction"
Cohesion: 0.10
Nodes (14): OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact, Task (+6 more)

### Community 28 - "EcMailbox"
Cohesion: 0.17
Nodes (9): Exception, FirmwareModeReading, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException, Held (+1 more)

### Community 29 - "Fact"
Cohesion: 0.16
Nodes (8): IList, HardwareSupport, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 30 - ".OnGpuModeChanged"
Cohesion: 0.12
Nodes (13): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, BannerRestartButton, ModeDiscrete, ModeHybrid (+5 more)

### Community 31 - "LedState"
Cohesion: 0.15
Nodes (15): B, G, R, LedState, ActiveProfile, BrightnessPercent, Current, Effect (+7 more)

### Community 32 - ".SignatureIsValid"
Cohesion: 0.15
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 33 - ".Survey"
Cohesion: 0.17
Nodes (7): Func, IEnumerable, IReadOnlyList, RetirementEntry, Trace, Version, FootprintTests

### Community 34 - ".PlaceTourStep"
Cohesion: 0.10
Nodes (10): SizeChangedEventArgs, KeyEventArgs, Point, RoutedEventArgs, Size, TourStep, Body, SectionName (+2 more)

### Community 35 - "Authenticode"
Cohesion: 0.25
Nodes (14): CatalogInfo, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle, Authenticode (+6 more)

### Community 36 - "Nextcalibur.Core.Configuration"
Cohesion: 0.16
Nodes (9): Nextcalibur.Cli, Nextcalibur.Core.Configuration, Nextcalibur.Core.Power, microsoft_win32, system_security, system_security_accesscontrol, system_security_cryptography, system_security_principal (+1 more)

### Community 37 - "DevicePowerState"
Cohesion: 0.15
Nodes (10): DevPropKey, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, DllImport, Guid (+2 more)

### Community 38 - "Nextcalibur.Core.Security"
Cohesion: 0.18
Nodes (8): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_security_cryptography_x509certificates, system_text_json

### Community 39 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 40 - ".Get"
Cohesion: 0.18
Nodes (8): ArgumentNullException, FirmwareModeReading, GpuModeService, SwitchOutcome, Func, Words, Resolver, SwitchOutcome

### Community 41 - "Nextcalibur.App"
Cohesion: 0.10
Nodes (13): Nextcalibur.App, system_drawing, system_io, system_io_directory, system_io_ioexception, system_io_path, system_linq, system_runtime_interopservices_comtypes (+5 more)

### Community 42 - "Integrity"
Cohesion: 0.14
Nodes (9): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvmlPath, InlineData (+1 more)

### Community 43 - "PowerOverlayService"
Cohesion: 0.20
Nodes (7): DllImport, Guid, IReadOnlyList, PowerModeOption, PowerOverlays, All, PowerOverlayService

### Community 44 - "ToggleButton"
Cohesion: 0.10
Nodes (18): LedPower, OverheatWarningToggle, SelectAll, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost (+10 more)

### Community 45 - ".CheckProgram"
Cohesion: 0.20
Nodes (5): Fact, IntegrityTests, Current, Exe, Manifest

### Community 46 - "Program"
Cohesion: 0.17
Nodes (5): ConsoleColor, B, G, R, Program

### Community 47 - "MainWindow.FnKeys.cs"
Cohesion: 0.21
Nodes (12): Nextcalibur.App.Controls, system_windows, system_windows_controls, system_windows_controls_button, system_windows_controls_checkbox, system_windows_controls_primitives, system_windows_input, system_windows_input_keyeventargs (+4 more)

### Community 48 - ".Main"
Cohesion: 0.19
Nodes (6): The threat model, and what the code does about it, Application, DllImport, Mutex, App, STAThread

### Community 49 - "Nextcalibur.Core.csproj"
Cohesion: 0.12
Nodes (13): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Management (10.0.12), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk, net10.0-windows (+5 more)

### Community 50 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 51 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 52 - ".Check"
Cohesion: 0.18
Nodes (8): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported, IdempotenceTests

### Community 53 - "UpdateService"
Cohesion: 0.14
Nodes (13): DateTime, DispatcherTimer, HashSet, IProgress, Task, TimeSpan, UpdateService, Available (+5 more)

### Community 54 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 55 - "CoreLoad"
Cohesion: 0.12
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 56 - "Fact"
Cohesion: 0.18
Nodes (5): Fact, ForwardCompatibilityTests, MailboxAccessTests, SettingsTests, StartupPreferenceTests

### Community 57 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 58 - ".OnClosing"
Cohesion: 0.13
Nodes (3): CancelEventArgs, DllImport, IntPtr

### Community 59 - "Elevation"
Cohesion: 0.23
Nodes (4): dynamic, Elevation, Fact, TaskFolderTests

### Community 60 - ".Ask"
Cohesion: 0.35
Nodes (7): HttpStatusCode, Fact, InlineData, Task, Theory, Answer, HostileAnswerTests

### Community 61 - "system_runtime_interopservices"
Cohesion: 0.17
Nodes (7): microsoft_win32_safehandles, system_componentmodel, system_diagnostics, system_reflection, system_runtime_compilerservices, system_runtime_interopservices, system_text

### Community 62 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 63 - ".MayStartWithoutPrompt"
Cohesion: 0.23
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 64 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 65 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 66 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 67 - "LedEffect"
Cohesion: 0.14
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 68 - "ThemeService"
Cohesion: 0.15
Nodes (11): Theme, Theme, Dark, Light, ThemePreference, Dark, Light, System (+3 more)

### Community 69 - "InstallFolderGuard"
Cohesion: 0.30
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 70 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 71 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 72 - ".Capture"
Cohesion: 0.26
Nodes (6): EnumWindowsProc, IO_COUNTERS, PROCESS_MEMORY_COUNTERS, ProcessMetrics, DllImport, IntPtr

### Community 73 - "BacklightKeyWatcher"
Cohesion: 0.15
Nodes (6): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, Known, system_management

### Community 74 - "LedBrightness"
Cohesion: 0.18
Nodes (8): LedBrightness, Full, Half, Off, Fact, InlineData, Theory, BacklightKeyTests

### Community 75 - ".Migrate"
Cohesion: 0.22
Nodes (3): DateTime, InlineData, Theory

### Community 77 - "PawnIoDependency"
Cohesion: 0.15
Nodes (11): CancellationToken, HttpClient, Task, Version, PawnIoDependency, ExpectedSigner, Id, Name (+3 more)

### Community 78 - "BatteryModePolicy"
Cohesion: 0.42
Nodes (4): BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

### Community 79 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 80 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 81 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 82 - "Nextcalibur 0.5.4"
Cohesion: 0.17
Nodes (11): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+3 more)

### Community 83 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 84 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 85 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 86 - "Dependency"
Cohesion: 0.17
Nodes (9): Version, Dependency, ExpectedSigner, Id, MayBeRemoved, Name, Purpose, SilentInstallArguments (+1 more)

### Community 87 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 88 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 89 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 92 - "Log"
Cohesion: 0.29
Nodes (3): Exception, Log, Folder

### Community 93 - "ReleaseFile"
Cohesion: 0.33
Nodes (6): CancellationToken, HttpClient, Task, Uri, ReleaseFile, HttpClient

### Community 94 - "NvidiaDriverState"
Cohesion: 0.27
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 95 - ".PowerModeControls"
Cohesion: 0.22
Nodes (4): Option, Button, IEnumerable, TextBlock

### Community 97 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 98 - ".Sanitised"
Cohesion: 0.33
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 99 - ".CalculateThreadShare"
Cohesion: 0.27
Nodes (4): IReadOnlyDictionary, Stable, Dictionary, TopSharePercent

### Community 100 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 101 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 102 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 103 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 104 - "README.md"
Cohesion: 0.33
Nodes (3): How it works, Questions people ask, Using it

### Community 105 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 106 - "ThermalSample"
Cohesion: 0.33
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 108 - ".IsSystemFileSignedBy"
Cohesion: 0.39
Nodes (3): Fact, SystemFileTrustTests, Cmd

### Community 109 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 110 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 111 - "StackPanel"
Cohesion: 0.25
Nodes (8): BannerActions, DialogActions, DriveTextStack, FnShortcutRows, PanelWindowsFaultsSubOptions, ProfileRow, SettingUpdatesAutomatic, StackPanel

### Community 112 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 113 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 114 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 115 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 116 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 117 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 118 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 119 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 120 - "BatteryModePolicy.cs"
Cohesion: 0.33
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 121 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 122 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 123 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 124 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 125 - ".ForegroundProcess"
Cohesion: 0.47
Nodes (3): DllImport, IntPtr, Name

### Community 127 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 128 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 129 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 131 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 133 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 134 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 135 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 137 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 138 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 139 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 141 - ".SendAsync"
Cohesion: 0.50
Nodes (3): CancellationToken, HttpRequestMessage, HttpResponseMessage

### Community 142 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 145 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

## Knowledge Gaps
- **452 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+447 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 848 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **37 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `UserPresence`, `GpuClockReader`, `FnShortcut`, `RunawayWatchTests`, `Window`, `AppSettings`, `RadioButton`, `PlanTuning`, `.Info`, `TrayPresence`, `SystemInfo`, `CpuPowerReader`, `.Get`, `WindowsFaults`, `MachineScanRecord`, `Button`, `ProcessPresence`, `.RequestRestart`, `SystemMode`, `CoolingPolicyTests`, `.RunFnAction`, `EcMailbox`, `.OnGpuModeChanged`, `.PlaceTourStep`, `DevicePowerState`, `.Read`, `.Get`, `Nextcalibur.App`, `PowerOverlayService`, `ToggleButton`, `MainWindow.FnKeys.cs`, `DependencyStatus`, `UpdateService`, `.OnClosing`, `LedController`, `LedEffect`, `ThemeService`, `BacklightKeyWatcher`, `LedBrightness`, `BatteryModePolicy`, `Strings`, `Grid`, `.WireSettingsPage`, `Log`, `NvidiaDriverState`, `.PowerModeControls`, `.OnSystemModeChanged`, `ThermalSample`, `TourPage`, `.OnBrightnessChanged`, `.ForegroundProcess`, `.LoadGpuMode`, `LedZone`?**
  _High betweenness centrality (0.497) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Keycaps`, `DriveGauge`, `RadioButton`, `CpuWarnValue`, `Button`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `FnSelectAll`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `.OnGpuModeChanged`, `ToggleButton`, `Grid`, `Border`, `StackPanel`, `Text`, `.OnBrightnessChanged`?**
  _High betweenness centrality (0.118) - this node is a cross-community bridge._
- **Why does `AppSettings` connect `AppSettings` to `MainWindow`, `.Sanitised`, `Nextcalibur.Core.Configuration`, `ThemeService`, `ProtectedStore`, `.Migrate`, `.Info`, `TrayPresence`, `Footprint`, `NduFix`, `Strings`, `WindowsFaultsTests`, `SystemMode`, `Fact`?**
  _High betweenness centrality (0.082) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _452 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DotNetRuntimeDependency` be split into smaller, more focused modules?**
  _Cohesion score 0.05348101265822785 - nodes in this community are weakly interconnected._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.03994732221246708 - nodes in this community are weakly interconnected._