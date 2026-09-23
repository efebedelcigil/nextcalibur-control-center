# Graph Report - nextcalibur-control-center  (2026-09-23)

## Corpus Check
- 132 files · ~187,196 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2235 nodes · 4648 edges · 142 communities (112 shown, 30 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 243 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `9f396d3b`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- DotNetRuntimeDependency
- Dependency
- LedState
- UserPresence
- MainWindow
- GpuClockReader
- .SignatureIsValid
- .InstallAsync
- Window
- IShellLinkW
- CpuPowerReader
- MainWindow.xaml.cs
- RadioButton
- .Get
- WindowsFaults
- SystemInfo
- .Survey
- Button
- .OnLoaded
- WindowsFaultsTests
- .RequestRestart
- AppSettings
- ProcessPresence
- Nextcalibur.Core.Configuration
- .Main
- EcMailbox
- Fact
- SystemModeService
- Nextcalibur.Core.Hardware
- Fact
- .OnGpuModeChanged
- .OnSystemModeChanged
- Nextcalibur.Core.Dependencies
- .OfferDependency
- Nextcalibur.Core.csproj
- ToggleButton
- .Info
- .Get
- analyse-autopsy.py
- system_runtime_interopservices
- .Migrate
- Fact
- Program
- PowerOverlayService
- ResourceDictionary
- UpdateService
- BacklightKeyWatcher
- .PowerModeControls
- ColourWheel
- Unelevated
- CoreLoad
- FanGauge
- ProcessMetrics
- DonutGauge
- SystemMode
- SegmentedBar
- Elevation
- .HasAccess
- LogInjectionTests
- .FromBytes
- .RunLighting
- .Check
- ElevationPolicyTests
- BatteryModePolicy
- StringsDictionaryTests
- ValueConverters.cs
- DevicePowerState
- Nextcalibur 0.5.2
- CardSwitchTasks
- Nextcalibur Control Center
- Strings
- Grid
- .WireSettingsPage
- Hardware Protocol
- Nextcalibur 0.5.3
- Border
- ThemeService
- NvidiaDriverState
- SupportVerdict
- .CalculateThreadShare
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- InstallFolderGuard
- .For
- Nextcalibur 0.5.1
- ThermalSample
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.5
- Releasing, and signing
- DriveRow
- .Pick
- TourPage
- Log-Graphics.ps1
- Privacy
- Security
- .OnBrightnessChanged
- StackPanel
- MachineWideGate
- PowerSource
- Notice
- AppIdentity.cs
- .Set
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Nextcalibur 0.5.6
- .ArrangeOverride
- MailboxAvailability
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
1. `MainWindow` - 209 edges
2. `Window` - 164 edges
3. `AppSettings` - 59 edges
4. `Nextcalibur.Core.Hardware` - 40 edges
5. `WindowsFaultsTests` - 40 edges
6. `RadioButton` - 39 edges
7. `TextBlock` - 36 edges
8. `TrayPresence` - 33 edges
9. `WindowsFaults` - 33 edges
10. `GpuClockReader` - 31 edges

## Surprising Connections (you probably didn't know these)
- `Deep memory & resource leak eradication` --references--> `MainWindow`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.App/MainWindow.Settings.cs
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

## Communities (142 total, 30 thin omitted)

### Community 0 - "DotNetRuntimeDependency"
Cohesion: 0.06
Nodes (43): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+35 more)

### Community 1 - "Dependency"
Cohesion: 0.05
Nodes (44): HttpStatusCode, CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner (+36 more)

### Community 2 - "LedState"
Cohesion: 0.06
Nodes (38): B, Dictionary, G, R, LedProfile, BrightnessPercent, Colours, Effect (+30 more)

### Community 3 - "UserPresence"
Cohesion: 0.06
Nodes (33): MEMORYSTATUSEX, MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, Dictionary, DllImport (+25 more)

### Community 4 - "MainWindow"
Cohesion: 0.05
Nodes (40): CancelEventArgs, DeferredOverheat, KeyEventArgs, ObservableCollection, Queue, SizeChangedEventArgs, SolidColorBrush, Button (+32 more)

### Community 5 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 6 - ".SignatureIsValid"
Cohesion: 0.07
Nodes (29): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+21 more)

### Community 7 - ".InstallAsync"
Cohesion: 0.07
Nodes (25): FileStream, IOException, Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress (+17 more)

### Community 8 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 9 - "IShellLinkW"
Cohesion: 0.07
Nodes (13): PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder, AppIdentity (+5 more)

### Community 10 - "CpuPowerReader"
Cohesion: 0.09
Nodes (21): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+13 more)

### Community 11 - "MainWindow.xaml.cs"
Cohesion: 0.09
Nodes (23): Nextcalibur.App, Nextcalibur.App.Controls, Nextcalibur.Core, system_componentmodel, system_drawing, system_globalization, system_io, system_linq (+15 more)

### Community 12 - "RadioButton"
Cohesion: 0.07
Nodes (34): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, NavDisplay, NavLighting (+26 more)

### Community 13 - ".Get"
Cohesion: 0.10
Nodes (13): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, MessageBoxButton, MessageBoxResult, EventArgs (+5 more)

### Community 14 - "WindowsFaults"
Cohesion: 0.11
Nodes (21): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, DateTime, DllImport, Func (+13 more)

### Community 15 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 16 - ".Survey"
Cohesion: 0.11
Nodes (10): Func, IEnumerable, IReadOnlyList, RegistryKey, Version, Footprint, SettingsPath, RetirementEntry (+2 more)

### Community 17 - "Button"
Cohesion: 0.07
Nodes (24): IsChecked, BannerDismissButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, HideToTrayButton, MinimiseButton (+16 more)

### Community 18 - ".OnLoaded"
Cohesion: 0.10
Nodes (5): AssemblyInformationalVersionAttribute, Func, Task, GpuLoad, GpuConfiguration

### Community 19 - "WindowsFaultsTests"
Cohesion: 0.19
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 20 - ".RequestRestart"
Cohesion: 0.11
Nodes (11): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr (+3 more)

### Community 21 - "AppSettings"
Cohesion: 0.07
Nodes (27): JsonElement, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoInstallUpdates, CompensateWindowsFaults, CpuWarningTemperatureC, DisableNdu (+19 more)

### Community 22 - "ProcessPresence"
Cohesion: 0.14
Nodes (11): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+3 more)

### Community 23 - "Nextcalibur.Core.Configuration"
Cohesion: 0.12
Nodes (11): Nextcalibur.Cli, Nextcalibur.Core.Configuration, Nextcalibur.Core.Power, microsoft_win32, Theme, Dark, Light, system_security (+3 more)

### Community 24 - ".Main"
Cohesion: 0.16
Nodes (6): The threat model, and what the code does about it, Application, DllImport, Mutex, App, STAThread

### Community 25 - "EcMailbox"
Cohesion: 0.16
Nodes (10): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+2 more)

### Community 26 - "Fact"
Cohesion: 0.17
Nodes (7): IList, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 27 - "SystemModeService"
Cohesion: 0.17
Nodes (9): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, SystemModeService (+1 more)

### Community 28 - "Nextcalibur.Core.Hardware"
Cohesion: 0.17
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Hardware, system_management, system_text_regularexpressions, system_xml_linq, xunit

### Community 29 - "Fact"
Cohesion: 0.11
Nodes (14): OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact, Task (+6 more)

### Community 30 - ".OnGpuModeChanged"
Cohesion: 0.11
Nodes (14): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, BannerRestartButton, ModeDiscrete, ModeHybrid (+6 more)

### Community 31 - ".OnSystemModeChanged"
Cohesion: 0.15
Nodes (5): PowerModeChangedEventArgs, ModeGaming, ModeOffice, ModePerformance, UninstallCommand

### Community 32 - "Nextcalibur.Core.Dependencies"
Cohesion: 0.19
Nodes (8): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_security_cryptography_x509certificates, system_text_json

### Community 33 - ".OfferDependency"
Cohesion: 0.16
Nodes (7): Asynchronous dispatcher & UI thread hardening, Action, Toasts, DependencyStatus, Missing, NeedsAction, Outdated

### Community 34 - "Nextcalibur.Core.csproj"
Cohesion: 0.12
Nodes (13): net8.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Management (8.0.0), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk, net8.0-windows (+5 more)

### Community 35 - "ToggleButton"
Cohesion: 0.10
Nodes (17): LedPower, OverheatWarningToggle, SelectAll, SettingAutoCheckUpdates, SettingAutoInstallUpdates, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost (+9 more)

### Community 36 - ".Info"
Cohesion: 0.16
Nodes (4): Exception, Log, Folder, NduFix

### Community 37 - ".Get"
Cohesion: 0.20
Nodes (7): ArgumentNullException, GpuModeService, SwitchOutcome, Func, Words, Resolver, SwitchOutcome

### Community 38 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 39 - "system_runtime_interopservices"
Cohesion: 0.13
Nodes (5): microsoft_win32_safehandles, system_diagnostics, system_reflection, system_runtime_interopservices, system_text

### Community 40 - ".Migrate"
Cohesion: 0.15
Nodes (9): DateTime, Dictionary, List, PendingRestartInfo, BootTimeUtc, ReasonArguments, Reasons, InlineData (+1 more)

### Community 41 - "Fact"
Cohesion: 0.16
Nodes (5): Fact, ForwardCompatibilityTests, MailboxAccessTests, SettingsTests, StartupPreferenceTests

### Community 42 - "Program"
Cohesion: 0.18
Nodes (5): ConsoleColor, B, G, R, Program

### Community 43 - "PowerOverlayService"
Cohesion: 0.23
Nodes (7): DllImport, Guid, IReadOnlyList, PowerModeOption, PowerOverlays, All, PowerOverlayService

### Community 44 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 45 - "UpdateService"
Cohesion: 0.15
Nodes (13): DispatcherTimer, Func, HashSet, IProgress, Task, TimeSpan, UpdateService, AutomaticChecksEnabled (+5 more)

### Community 46 - "BacklightKeyWatcher"
Cohesion: 0.14
Nodes (11): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, LedBrightness, Full, Half, Off, Fact (+3 more)

### Community 47 - ".PowerModeControls"
Cohesion: 0.13
Nodes (5): Option, Button, IEnumerable, RadioButton, TextBlock

### Community 48 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 49 - "Unelevated"
Cohesion: 0.33
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 50 - "CoreLoad"
Cohesion: 0.13
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 51 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 52 - "ProcessMetrics"
Cohesion: 0.15
Nodes (14): Share, Dictionary, TimeSpan, FaultEvidence, ProcessMetrics, Name, PageFaultCount, Pid (+6 more)

### Community 53 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 54 - "SystemMode"
Cohesion: 0.20
Nodes (9): ThermalProfile, SystemMode, Gaming, Office, Performance, Fact, InlineData, Theory (+1 more)

### Community 55 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 57 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 58 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 59 - ".FromBytes"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 60 - ".RunLighting"
Cohesion: 0.19
Nodes (4): Slider, Action, Color, TextBox

### Community 61 - ".Check"
Cohesion: 0.31
Nodes (3): RegistryKey, MailboxAccess, IdempotenceTests

### Community 62 - "ElevationPolicyTests"
Cohesion: 0.26
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 63 - "BatteryModePolicy"
Cohesion: 0.42
Nodes (4): BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

### Community 64 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 65 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 66 - "DevicePowerState"
Cohesion: 0.32
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 67 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 69 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 70 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 71 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 73 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 74 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 75 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 76 - "ThemeService"
Cohesion: 0.20
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 77 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 78 - "SupportVerdict"
Cohesion: 0.22
Nodes (9): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+1 more)

### Community 79 - ".CalculateThreadShare"
Cohesion: 0.27
Nodes (4): IReadOnlyDictionary, Stable, Dictionary, TopSharePercent

### Community 80 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 81 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 82 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 83 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 84 - "README.md"
Cohesion: 0.33
Nodes (3): How it works, Questions people ask, Using it

### Community 85 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 86 - "InstallFolderGuard"
Cohesion: 0.39
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 87 - ".For"
Cohesion: 0.22
Nodes (8): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal

### Community 88 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 89 - "ThermalSample"
Cohesion: 0.39
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 90 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 91 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 92 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 93 - "Nextcalibur 0.5.5"
Cohesion: 0.29
Nodes (5): Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on

### Community 94 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 95 - "DriveRow"
Cohesion: 0.29
Nodes (6): INotifyPropertyChanged, DriveRow, Detail, Name, Percent, PercentText

### Community 96 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 97 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 98 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 99 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 100 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 101 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 102 - "StackPanel"
Cohesion: 0.33
Nodes (6): BannerActions, DialogActions, DriveTextStack, PanelWindowsFaultsSubOptions, ProfileRow, StackPanel

### Community 103 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 104 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 105 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 106 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 108 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 110 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 111 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 112 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 113 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 115 - "MailboxAvailability"
Cohesion: 0.50
Nodes (4): MailboxAvailability, AccessNotGranted, Available, NotSupported

### Community 116 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 119 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

## Knowledge Gaps
- **372 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+367 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 697 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **30 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `LedState`, `UserPresence`, `GpuClockReader`, `Window`, `CpuPowerReader`, `MainWindow.xaml.cs`, `RadioButton`, `.Get`, `WindowsFaults`, `SystemInfo`, `Button`, `.OnLoaded`, `.RequestRestart`, `AppSettings`, `ProcessPresence`, `EcMailbox`, `SystemModeService`, `.OnGpuModeChanged`, `.OnSystemModeChanged`, `.OfferDependency`, `ToggleButton`, `.Info`, `.Get`, `PowerOverlayService`, `UpdateService`, `BacklightKeyWatcher`, `.PowerModeControls`, `SystemMode`, `.RunLighting`, `BatteryModePolicy`, `Strings`, `Grid`, `.WireSettingsPage`, `ThemeService`, `NvidiaDriverState`, `SupportVerdict`, `ThermalSample`, `Nextcalibur 0.5.5`, `TourPage`, `.OnBrightnessChanged`?**
  _High betweenness centrality (0.521) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `TourCanvas`, `Wheel`, `MainWindow`, `RadioButton`, `Button`, `.OnGpuModeChanged`, `.OnSystemModeChanged`, `ToggleButton`, `Grid`, `Border`, `Text`, `.OnBrightnessChanged`, `StackPanel`, `Keycaps`, `DriveGauge`, `CpuWarnValue`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`?**
  _High betweenness centrality (0.173) - this node is a cross-community bridge._
- **Why does `DependencyStatus` connect `.OfferDependency` to `Nextcalibur.Core.Dependencies`, `Dependency`, `MainWindow`, `.InstallAsync`?**
  _High betweenness centrality (0.103) - this node is a cross-community bridge._
- **Are the 11 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 11 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _372 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DotNetRuntimeDependency` be split into smaller, more focused modules?**
  _Cohesion score 0.05789473684210526 - nodes in this community are weakly interconnected._
- **Should `Dependency` be split into smaller, more focused modules?**
  _Cohesion score 0.05314685314685315 - nodes in this community are weakly interconnected._