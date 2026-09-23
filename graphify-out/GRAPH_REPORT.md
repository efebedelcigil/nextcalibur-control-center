# Graph Report - nextcalibur-control-center  (2026-09-23)

## Corpus Check
- 132 files · ~186,788 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2233 nodes · 4642 edges · 140 communities (109 shown, 31 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 243 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `3a75381f`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- DotNetRuntimeDependency
- MainWindow
- Dependency
- LedState
- GpuClockReader
- .SignatureIsValid
- Window
- IShellLinkW
- RadioButton
- SystemInfo
- UserPresence
- .Get
- MainWindow.xaml.cs
- Fact
- Button
- WindowsFaults
- WindowsFaultsTests
- SystemMode
- .Warn
- AppSettings
- .RequestRestart
- CpuPowerReader
- .InstallAsync
- .Info
- ProcessPresence
- .RefreshBanner
- EcMailbox
- .OnClosing
- .Main
- Fact
- Fact
- .OnLoaded
- .Check
- BacklightKeyWatcher
- .OnGpuModeChanged
- .Read
- Nextcalibur.Core.Hardware
- MemoryTrimmer
- Nextcalibur.Core.csproj
- ToggleButton
- .CalculateThreadShare
- analyse-autopsy.py
- .Get
- Program
- ProcessMetrics
- PowerOverlayService
- ResourceDictionary
- UpdateService
- ColourWheel
- Unelevated
- FanGauge
- microsoft_win32
- HostileFilesTests
- system_runtime_interopservices
- DonutGauge
- SegmentedBar
- ThemeService
- .Retirements
- CoreLoad
- PendingRestartInfo
- ElevationPolicyTests
- .HasAccess
- BatteryModePolicy
- StringsDictionaryTests
- system_diagnostics
- Nextcalibur.Core.Configuration
- ValueConverters.cs
- DevicePowerState
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- .OnSystemModeChanged
- Nextcalibur Control Center
- Strings
- Grid
- SmiCommandTests
- Hardware Protocol
- Nextcalibur 0.5.3
- NvidiaDriverState
- .Check
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- InstallFolderGuard
- .RunLighting
- Border
- ThermalSample
- .Migrate
- ProfileFiles
- IntPtr
- Nextcalibur 0.5.1
- Nextcalibur 0.5.5
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Releasing, and signing
- .Pick
- TourPage
- BatteryModePolicy.cs
- Log-Graphics.ps1
- Privacy
- Security
- .OnBrightnessChanged
- StackPanel
- MachineWideGate
- Nextcalibur.Core.Security
- Notice
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Nextcalibur 0.5.6
- .ArrangeOverride
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
2. `Window` - 163 edges
3. `AppSettings` - 59 edges
4. `WindowsFaultsTests` - 41 edges
5. `Nextcalibur.Core.Hardware` - 40 edges
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

## Communities (140 total, 31 thin omitted)

### Community 0 - "DotNetRuntimeDependency"
Cohesion: 0.06
Nodes (43): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+35 more)

### Community 1 - "MainWindow"
Cohesion: 0.04
Nodes (36): DeferredOverheat, ObservableCollection, Option, Queue, SolidColorBrush, Button, Grid, List (+28 more)

### Community 2 - "Dependency"
Cohesion: 0.05
Nodes (44): HttpStatusCode, CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner (+36 more)

### Community 3 - "LedState"
Cohesion: 0.06
Nodes (39): B, Dictionary, G, R, LedProfile, BrightnessPercent, Colours, Effect (+31 more)

### Community 4 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 5 - ".SignatureIsValid"
Cohesion: 0.07
Nodes (29): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+21 more)

### Community 6 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 7 - "IShellLinkW"
Cohesion: 0.07
Nodes (13): PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder, AppIdentity (+5 more)

### Community 8 - "RadioButton"
Cohesion: 0.07
Nodes (37): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeGaming, ModeOffice (+29 more)

### Community 9 - "SystemInfo"
Cohesion: 0.08
Nodes (22): MemoryStatusEx, DriveUse, INotifyPropertyChanged, DriveRow, Detail, Name, Percent, PercentText (+14 more)

### Community 10 - "UserPresence"
Cohesion: 0.10
Nodes (20): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr, MarshalAs (+12 more)

### Community 11 - ".Get"
Cohesion: 0.10
Nodes (13): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, MessageBoxButton, MessageBoxResult, EventArgs (+5 more)

### Community 12 - "MainWindow.xaml.cs"
Cohesion: 0.10
Nodes (21): Nextcalibur.App, Nextcalibur.App.Controls, system_componentmodel, system_drawing, system_io, system_linq, system_runtime_compilerservices, system_windows (+13 more)

### Community 13 - "Fact"
Cohesion: 0.12
Nodes (11): Func, IEnumerable, IReadOnlyList, Version, Footprint, SettingsPath, RetirementEntry, Trace (+3 more)

### Community 14 - "Button"
Cohesion: 0.07
Nodes (24): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, HideToTrayButton, MinimiseButton (+16 more)

### Community 15 - "WindowsFaults"
Cohesion: 0.12
Nodes (20): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, DateTime, DllImport, Func (+12 more)

### Community 17 - "SystemMode"
Cohesion: 0.15
Nodes (13): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, SystemMode (+5 more)

### Community 18 - ".Warn"
Cohesion: 0.11
Nodes (9): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, FixButton, Action, ToggleButton (+1 more)

### Community 19 - "AppSettings"
Cohesion: 0.07
Nodes (28): JsonElement, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoInstallUpdates, CompensateWindowsFaults, CpuWarningTemperatureC, DisableNdu (+20 more)

### Community 20 - ".RequestRestart"
Cohesion: 0.12
Nodes (10): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr, MarshalAs (+2 more)

### Community 21 - "CpuPowerReader"
Cohesion: 0.16
Nodes (9): SafeFileHandle, DllImport, IntPtr, CpuPowerReader, DriverInstalled, Fact, InlineData, Theory (+1 more)

### Community 22 - ".InstallAsync"
Cohesion: 0.09
Nodes (20): FileStream, Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList (+12 more)

### Community 23 - ".Info"
Cohesion: 0.13
Nodes (9): Exception, Log, Folder, Action, Fact, InlineData, Regex, Theory (+1 more)

### Community 24 - "ProcessPresence"
Cohesion: 0.14
Nodes (11): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+3 more)

### Community 25 - ".RefreshBanner"
Cohesion: 0.15
Nodes (3): Encoding, Elevation, CardSwitchTasks

### Community 26 - "EcMailbox"
Cohesion: 0.15
Nodes (11): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+3 more)

### Community 27 - ".OnClosing"
Cohesion: 0.09
Nodes (11): CancelEventArgs, KeyEventArgs, SizeChangedEventArgs, Point, RoutedEventArgs, Size, TourStep, Body (+3 more)

### Community 28 - ".Main"
Cohesion: 0.16
Nodes (7): The threat model, and what the code does about it, Application, DllImport, Mutex, App, RegistryKey, STAThread

### Community 29 - "Fact"
Cohesion: 0.18
Nodes (7): IList, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 30 - "Fact"
Cohesion: 0.11
Nodes (14): OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact, Task (+6 more)

### Community 31 - ".OnLoaded"
Cohesion: 0.14
Nodes (8): AssemblyInformationalVersionAttribute, Asynchronous dispatcher & UI thread hardening, Action, Toasts, DependencyStatus, Missing, NeedsAction, Outdated

### Community 32 - ".Check"
Cohesion: 0.13
Nodes (9): RawSecurityDescriptor, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported, IdempotenceTests (+1 more)

### Community 33 - "BacklightKeyWatcher"
Cohesion: 0.10
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, LedBrightness, Full, Half, Off, system_management (+4 more)

### Community 34 - ".OnGpuModeChanged"
Cohesion: 0.13
Nodes (10): ModeDiscrete, ModeHybrid, ModeUma, Task, GpuLoad, GpuConfiguration, GpuMode, Discrete (+2 more)

### Community 35 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 36 - "Nextcalibur.Core.Hardware"
Cohesion: 0.23
Nodes (5): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, Nextcalibur.Core.Hardware, system_xml_linq, xunit

### Community 37 - "MemoryTrimmer"
Cohesion: 0.14
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 38 - "Nextcalibur.Core.csproj"
Cohesion: 0.12
Nodes (13): net8.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Management (8.0.0), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk, net8.0-windows (+5 more)

### Community 39 - "ToggleButton"
Cohesion: 0.10
Nodes (17): LedPower, OverheatWarningToggle, SelectAll, SettingAutoCheckUpdates, SettingAutoInstallUpdates, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost (+9 more)

### Community 40 - ".CalculateThreadShare"
Cohesion: 0.15
Nodes (9): IReadOnlyDictionary, Stable, Dictionary, Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests (+1 more)

### Community 41 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 42 - ".Get"
Cohesion: 0.22
Nodes (7): ArgumentNullException, GpuModeService, SwitchOutcome, Func, Words, Resolver, SwitchOutcome

### Community 43 - "Program"
Cohesion: 0.20
Nodes (5): ConsoleColor, B, G, R, Program

### Community 44 - "ProcessMetrics"
Cohesion: 0.12
Nodes (16): Share, Dictionary, TimeSpan, FaultEvaluation, FaultEvidence, FaultFound, ProcessMetrics, Name (+8 more)

### Community 45 - "PowerOverlayService"
Cohesion: 0.22
Nodes (7): DllImport, Guid, IReadOnlyList, PowerModeOption, PowerOverlays, All, PowerOverlayService

### Community 46 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 47 - "UpdateService"
Cohesion: 0.15
Nodes (13): DispatcherTimer, Func, HashSet, IProgress, Task, TimeSpan, UpdateService, AutomaticChecksEnabled (+5 more)

### Community 48 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 49 - "Unelevated"
Cohesion: 0.33
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 50 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 51 - "microsoft_win32"
Cohesion: 0.27
Nodes (6): Nextcalibur.Core.Dependencies, microsoft_win32, system_collections_concurrent, system_net, system_net_http, system_text_json

### Community 52 - "HostileFilesTests"
Cohesion: 0.27
Nodes (5): IOException, Fact, InlineData, Theory, HostileFilesTests

### Community 53 - "system_runtime_interopservices"
Cohesion: 0.14
Nodes (7): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices, system_runtime_interopservices_comtypes, system_text, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 54 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 55 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 56 - "ThemeService"
Cohesion: 0.15
Nodes (11): Theme, Theme, Dark, Light, ThemePreference, Dark, Light, System (+3 more)

### Community 57 - ".Retirements"
Cohesion: 0.20
Nodes (3): StartupRegistration, IsEnabled, NduFix

### Community 58 - "CoreLoad"
Cohesion: 0.17
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 59 - "PendingRestartInfo"
Cohesion: 0.19
Nodes (7): DateTime, Dictionary, List, PendingRestartInfo, BootTimeUtc, ReasonArguments, Reasons

### Community 60 - "ElevationPolicyTests"
Cohesion: 0.26
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 61 - ".HasAccess"
Cohesion: 0.28
Nodes (5): SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 62 - "BatteryModePolicy"
Cohesion: 0.42
Nodes (4): BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

### Community 63 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 64 - "system_diagnostics"
Cohesion: 0.21
Nodes (6): Nextcalibur.Cli, microsoft_win32_safehandles, system_diagnostics, system_reflection, system_security_accesscontrol, system_security_principal

### Community 65 - "Nextcalibur.Core.Configuration"
Cohesion: 0.21
Nodes (6): Nextcalibur.Core, Nextcalibur.Core.Configuration, Nextcalibur.Core.Tests.Attacks, system_globalization, system_security, system_text_regularexpressions

### Community 66 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 67 - "DevicePowerState"
Cohesion: 0.32
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 68 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 69 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 71 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 72 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 73 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 74 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 75 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 76 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 77 - "NvidiaDriverState"
Cohesion: 0.27
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 78 - ".Check"
Cohesion: 0.22
Nodes (9): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+1 more)

### Community 79 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 80 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 81 - "README.md"
Cohesion: 0.33
Nodes (3): How it works, Questions people ask, Using it

### Community 82 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 83 - "InstallFolderGuard"
Cohesion: 0.39
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 84 - ".RunLighting"
Cohesion: 0.28
Nodes (3): Slider, Action, TextBox

### Community 85 - "Border"
Cohesion: 0.22
Nodes (9): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, ThemeSwitch, TourCard (+1 more)

### Community 86 - "ThermalSample"
Cohesion: 0.33
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 89 - "IntPtr"
Cohesion: 0.50
Nodes (3): DllImport, IntPtr, Program

### Community 90 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 91 - "Nextcalibur 0.5.5"
Cohesion: 0.25
Nodes (6): Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture

### Community 92 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 93 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 94 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 95 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 96 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 97 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 98 - "BatteryModePolicy.cs"
Cohesion: 0.33
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 99 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 100 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 101 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 102 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 103 - "StackPanel"
Cohesion: 0.33
Nodes (6): BannerActions, DialogActions, DriveTextStack, PanelWindowsFaultsSubOptions, ProfileRow, StackPanel

### Community 104 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 106 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 107 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 109 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 110 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 111 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 112 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 114 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 117 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

## Knowledge Gaps
- **372 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+367 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 697 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **31 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `LedState`, `GpuClockReader`, `Window`, `RadioButton`, `SystemInfo`, `UserPresence`, `.Get`, `MainWindow.xaml.cs`, `Button`, `WindowsFaults`, `SystemMode`, `.Warn`, `AppSettings`, `.RequestRestart`, `CpuPowerReader`, `.Info`, `.RefreshBanner`, `EcMailbox`, `.OnClosing`, `.OnLoaded`, `BacklightKeyWatcher`, `.OnGpuModeChanged`, `.Read`, `ToggleButton`, `.Get`, `PowerOverlayService`, `UpdateService`, `ThemeService`, `BatteryModePolicy`, `.OnSystemModeChanged`, `Strings`, `Grid`, `NvidiaDriverState`, `.Check`, `.RunLighting`, `ThermalSample`, `Nextcalibur 0.5.5`, `TourPage`, `.OnBrightnessChanged`?**
  _High betweenness centrality (0.503) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `RadioButton`, `Button`, `.Warn`, `.OnGpuModeChanged`, `ToggleButton`, `Grid`, `Border`, `Text`, `.OnBrightnessChanged`, `StackPanel`, `Keycaps`, `DriveGauge`, `CpuWarnValue`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`?**
  _High betweenness centrality (0.173) - this node is a cross-community bridge._
- **Why does `DependencyStatus` connect `.OnLoaded` to `MainWindow`, `Dependency`, `microsoft_win32`, `.InstallAsync`?**
  _High betweenness centrality (0.122) - this node is a cross-community bridge._
- **Are the 11 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 11 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _372 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DotNetRuntimeDependency` be split into smaller, more focused modules?**
  _Cohesion score 0.05789473684210526 - nodes in this community are weakly interconnected._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.04475703324808184 - nodes in this community are weakly interconnected._