# Graph Report - nextcalibur-control-center  (2026-09-14)

## Corpus Check
- 132 files · ~186,463 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2179 nodes · 4504 edges · 140 communities (107 shown, 22 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 250 edges (avg confidence: 0.82)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `69db0e24`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- SystemMode
- DotNetRuntimeDependency
- .Main
- LedState
- MainWindow
- AppSettings
- GpuClockReader
- Window
- IShellLinkW
- RadioButton
- UserPresence
- WindowsFaults
- .SignatureIsValid
- WindowsFaultsTests
- SystemInfo
- TrayPresence
- .Get
- ProcessPresence
- CpuPowerReader
- Fact
- ColourWheel
- Program
- .RequestRestart
- .Get
- .OnLoaded
- EcMailbox
- DependencyStatus
- Button
- Nextcalibur.Core.Hardware
- Elevation
- .Ask
- .Check
- MemoryTrimmer
- .CalculateThreadShare
- InvalidOperationException
- Nextcalibur.Core.csproj
- UpdateService
- Dependency
- FanGauge
- .PowerModeControls
- .StartSlowTimer
- .OfferDependency
- ResourceDictionary
- SmiCommand
- SegmentedBar
- Nextcalibur.Core.Dependencies
- ToggleButton
- .Read
- HostileFilesTests
- Unelevated
- DonutGauge
- ThemeService
- BacklightKeyWatcher
- LogInjectionTests
- DevicePowerState
- CoreLoad
- PawnIoDependency
- .HasAccess
- SettingsTests.cs
- StringsDictionaryTests
- Nextcalibur.Core.Configuration
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- Nextcalibur Control Center
- Grid
- RpmToDoubleConverter
- Hardware Protocol
- Nextcalibur 0.5.3
- DllImport
- .Info
- .WireSettingsPage
- NvidiaDriverState
- IntPtr
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- Border
- SupportVerdict
- SystemTools
- Probe
- Nextcalibur 0.5.1
- Nextcalibur 0.5.5
- DriveRow
- analyse-autopsy.py
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Releasing, and signing
- Security
- .Pick
- .PlaceTourStep
- Action
- TourPage
- PowerSource
- Log-Graphics.ps1
- Privacy
- .OnBrightnessChanged
- StackPanel
- .Set
- NduFix
- Words
- Notice
- TourStep
- build_icon.py
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Nextcalibur 0.5.6
- Nextcalibur 0.5.8
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
1. `MainWindow` - 208 edges
2. `Window` - 163 edges
3. `AppSettings` - 60 edges
4. `WindowsFaultsTests` - 41 edges
5. `Nextcalibur.Core.Hardware` - 40 edges
6. `RadioButton` - 39 edges
7. `TextBlock` - 36 edges
8. `TrayPresence` - 34 edges
9. `WindowsFaults` - 32 edges
10. `GpuClockReader` - 31 edges

## Surprising Connections (you probably didn't know these)
- `Probe` --inherits--> `Dependency`  [EXTRACTED]
  tests/Nextcalibur.Core.Tests/Attacks/HostileAnswerTests.cs → src/Nextcalibur.Core/Dependencies/Dependency.cs
- `MainWindow` --inherits--> `Window`  [EXTRACTED]
  src/Nextcalibur.App/MainWindow.Settings.cs → src/Nextcalibur.Core/Hardware/WindowsFaults.cs
- `GpuModeTests` --references--> `GpuConfiguration`  [EXTRACTED]
  tests/Nextcalibur.Core.Tests/GpuModeTests.cs → src/Nextcalibur.Core/Hardware/GpuModeService.cs
- `CleanMachineTests` --references--> `OverlayDiagnosis`  [EXTRACTED]
  tests/Nextcalibur.Core.Tests/PolicyTests.cs → src/Nextcalibur.Core/Power/PowerOverlay.cs
- `MainWindow` --defines--> `_gpuPendingRestart`  [EXTRACTED]
  src/Nextcalibur.App/MainWindow.Settings.cs → src/Nextcalibur.App/MainWindow.xaml.cs

## Import Cycles
- None detected.

## Communities (140 total, 22 thin omitted)

### Community 0 - "SystemMode"
Cohesion: 0.05
Nodes (37): BatteryModePolicy, Remembered, DllImport, Guid, IReadOnlyList, OverlayDiagnosis, GuardMissing, NeedsRepair (+29 more)

### Community 1 - "DotNetRuntimeDependency"
Cohesion: 0.06
Nodes (43): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+35 more)

### Community 2 - ".Main"
Cohesion: 0.05
Nodes (23): FileSystemAccessRule, FileSystemRights, ResourceDictionary, Application, DllImport, Mutex, App, Strings (+15 more)

### Community 3 - "LedState"
Cohesion: 0.05
Nodes (42): B, Dictionary, G, R, LedProfile, BrightnessPercent, Colours, Effect (+34 more)

### Community 4 - "MainWindow"
Cohesion: 0.05
Nodes (33): CancelEventArgs, DeferredOverheat, KeyEventArgs, ObservableCollection, Queue, HideToTrayButton, Button, Grid (+25 more)

### Community 5 - "AppSettings"
Cohesion: 0.05
Nodes (37): DateTime, Dictionary, JsonElement, List, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoInstallUpdates (+29 more)

### Community 6 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 7 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 8 - "IShellLinkW"
Cohesion: 0.07
Nodes (13): PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder, AppIdentity (+5 more)

### Community 9 - "RadioButton"
Cohesion: 0.06
Nodes (40): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeDiscrete, ModeGaming (+32 more)

### Community 10 - "UserPresence"
Cohesion: 0.10
Nodes (20): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr, MarshalAs (+12 more)

### Community 11 - "WindowsFaults"
Cohesion: 0.08
Nodes (31): Process, ProcessMetrics, Share, DateTime, Dictionary, Func, IReadOnlyList, List (+23 more)

### Community 12 - ".SignatureIsValid"
Cohesion: 0.11
Nodes (18): DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, X509Certificate2, Authenticode, WinTrustData, WinTrustFileInfo (+10 more)

### Community 14 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 15 - "TrayPresence"
Cohesion: 0.10
Nodes (11): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, EventArgs, List, Ms (+3 more)

### Community 16 - ".Get"
Cohesion: 0.13
Nodes (9): IList, GpuLoad, GpuConfiguration, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests (+1 more)

### Community 17 - "ProcessPresence"
Cohesion: 0.12
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 18 - "CpuPowerReader"
Cohesion: 0.16
Nodes (9): SafeFileHandle, DllImport, IntPtr, CpuPowerReader, DriverInstalled, Fact, InlineData, Theory (+1 more)

### Community 19 - "Fact"
Cohesion: 0.17
Nodes (9): Func, IEnumerable, IReadOnlyList, Version, Footprint, SettingsPath, RetirementEntry, Fact (+1 more)

### Community 20 - "ColourWheel"
Cohesion: 0.11
Nodes (18): BitmapSource, Control, DependencyObject, DependencyPropertyChangedEventArgs, Ellipse, Hue, Saturation, Canvas (+10 more)

### Community 21 - "Program"
Cohesion: 0.13
Nodes (9): ConsoleColor, B, G, R, Program, HardwareSupport, DateTimeOffset, ThermalReader (+1 more)

### Community 22 - ".RequestRestart"
Cohesion: 0.13
Nodes (7): Luid, DllImport, EventArgs, IntPtr, MarshalAs, TokenPrivileges, TokenPrivileges

### Community 23 - ".Get"
Cohesion: 0.14
Nodes (5): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner

### Community 24 - ".OnLoaded"
Cohesion: 0.14
Nodes (3): AssemblyInformationalVersionAttribute, PowerModeChangedEventArgs, RoutedEventArgs

### Community 25 - "EcMailbox"
Cohesion: 0.15
Nodes (11): Exception, FirmwareModeReading, IDisposable, ManagementObject, Func, EcMailbox, EcMailboxUnavailableException, Held (+3 more)

### Community 26 - "DependencyStatus"
Cohesion: 0.13
Nodes (17): FileStream, IOException, Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress (+9 more)

### Community 27 - "Button"
Cohesion: 0.09
Nodes (23): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, MinimiseButton (+15 more)

### Community 28 - "Nextcalibur.Core.Hardware"
Cohesion: 0.12
Nodes (7): Nextcalibur.Cli, Nextcalibur.Core.Tests, Nextcalibur.Core.Power, Nextcalibur.Core.Hardware, Trace, Fact, ThermalProfileTests

### Community 29 - "Elevation"
Cohesion: 0.18
Nodes (3): Encoding, Elevation, CardSwitchTasks

### Community 30 - ".Ask"
Cohesion: 0.23
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 31 - ".Check"
Cohesion: 0.18
Nodes (8): ManagementScope, RegistryKey, RawSecurityDescriptor, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported

### Community 32 - "MemoryTrimmer"
Cohesion: 0.14
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 33 - ".CalculateThreadShare"
Cohesion: 0.15
Nodes (9): IReadOnlyDictionary, Stable, Dictionary, Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests (+1 more)

### Community 34 - "InvalidOperationException"
Cohesion: 0.19
Nodes (10): ArgumentNullException, InvalidOperationException, FirmwareModeReading, GpuMode, Discrete, Hybrid, Uma, GpuModeService (+2 more)

### Community 35 - "Nextcalibur.Core.csproj"
Cohesion: 0.12
Nodes (13): net8.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.0), System.Management (8.0.0), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk, net8.0-windows (+5 more)

### Community 36 - "UpdateService"
Cohesion: 0.13
Nodes (13): DispatcherTimer, Func, HashSet, IProgress, Task, TimeSpan, UpdateService, AutomaticChecksEnabled (+5 more)

### Community 37 - "Dependency"
Cohesion: 0.16
Nodes (14): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+6 more)

### Community 38 - "FanGauge"
Cohesion: 0.13
Nodes (15): ContentControl, SolidColorBrush, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush (+7 more)

### Community 39 - ".PowerModeControls"
Cohesion: 0.12
Nodes (5): Option, Button, IEnumerable, RadioButton, TextBlock

### Community 41 - ".OfferDependency"
Cohesion: 0.16
Nodes (3): Task, Action, Exception

### Community 42 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 43 - "SmiCommand"
Cohesion: 0.21
Nodes (6): ArgumentException, SmiCommand, Fact, InlineData, Theory, SmiCommandTests

### Community 44 - "SegmentedBar"
Cohesion: 0.12
Nodes (12): Nextcalibur.App.Controls, FrameworkElement, Brush, DependencyProperty, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 45 - "Nextcalibur.Core.Dependencies"
Cohesion: 0.15
Nodes (3): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks

### Community 46 - "ToggleButton"
Cohesion: 0.12
Nodes (17): LedPower, OverheatWarningToggle, SelectAll, SettingAutoCheckUpdates, SettingAutoInstallUpdates, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost (+9 more)

### Community 47 - ".Read"
Cohesion: 0.15
Nodes (11): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+3 more)

### Community 48 - "HostileFilesTests"
Cohesion: 0.28
Nodes (4): IEnumerable, ProfileFiles, Fact, HostileFilesTests

### Community 49 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 50 - "DonutGauge"
Cohesion: 0.17
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 51 - "ThemeService"
Cohesion: 0.14
Nodes (11): Theme, Theme, Dark, Light, ThemePreference, Dark, Light, System (+3 more)

### Community 52 - "BacklightKeyWatcher"
Cohesion: 0.16
Nodes (7): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, Fact, InlineData, Theory, BacklightKeyTests

### Community 53 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 54 - "DevicePowerState"
Cohesion: 0.28
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 55 - "CoreLoad"
Cohesion: 0.17
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 56 - "PawnIoDependency"
Cohesion: 0.15
Nodes (11): CancellationToken, HttpClient, Task, Version, PawnIoDependency, ExpectedSigner, Id, Name (+3 more)

### Community 57 - ".HasAccess"
Cohesion: 0.28
Nodes (5): SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 58 - "SettingsTests.cs"
Cohesion: 0.15
Nodes (4): ForwardCompatibilityTests, IdempotenceTests, MailboxAccessTests, StartupPreferenceTests

### Community 59 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 60 - "Nextcalibur.Core.Configuration"
Cohesion: 0.26
Nodes (3): Nextcalibur.App, Nextcalibur.Core.Configuration, Toasts

### Community 61 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 62 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 63 - "Nextcalibur 0.5.4"
Cohesion: 0.17
Nodes (11): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+3 more)

### Community 64 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 65 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 66 - "RpmToDoubleConverter"
Cohesion: 0.33
Nodes (6): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, Type

### Community 67 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 68 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 69 - "DllImport"
Cohesion: 0.29
Nodes (5): EnumWindowsProc, IO_COUNTERS, PROCESS_MEMORY_COUNTERS, DllImport, IntPtr

### Community 72 - "NvidiaDriverState"
Cohesion: 0.27
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 73 - "IntPtr"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 74 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 75 - "README.md"
Cohesion: 0.33
Nodes (3): How it works, Questions people ask, Using it

### Community 76 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 77 - "Border"
Cohesion: 0.22
Nodes (9): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, ThemeSwitch, TourCard (+1 more)

### Community 78 - "SupportVerdict"
Cohesion: 0.25
Nodes (8): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites

### Community 79 - "SystemTools"
Cohesion: 0.22
Nodes (7): SystemTools, Cmd, Explorer, Pnputil, Powercfg, PowerShell, Schtasks

### Community 80 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 81 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 82 - "Nextcalibur 0.5.5"
Cohesion: 0.25
Nodes (7): Asynchronous dispatcher & UI thread hardening, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture

### Community 83 - "DriveRow"
Cohesion: 0.25
Nodes (6): INotifyPropertyChanged, DriveRow, Detail, Name, Percent, PercentText

### Community 84 - "analyse-autopsy.py"
Cohesion: 0.43
Nodes (7): describe_file(), load(), main(), Path, Compare autopsy snapshots taken before, during and after an install. python…, section(), vendorish()

### Community 85 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 86 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 87 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 88 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 89 - "Security"
Cohesion: 0.29
Nodes (7): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, The threat model, and what the code does about it, What counts

### Community 90 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 91 - ".PlaceTourStep"
Cohesion: 0.43
Nodes (4): SizeChangedEventArgs, Point, Size, Rect

### Community 92 - "Action"
Cohesion: 0.33
Nodes (3): Slider, Action, TextBox

### Community 93 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 94 - "PowerSource"
Cohesion: 0.33
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 95 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 96 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 97 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 98 - "StackPanel"
Cohesion: 0.33
Nodes (6): BannerActions, DialogActions, DriveTextStack, PanelWindowsFaultsSubOptions, ProfileRow, StackPanel

### Community 101 - "Words"
Cohesion: 0.40
Nodes (4): Nextcalibur.Core, Func, Words, Resolver

### Community 102 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 103 - "TourStep"
Cohesion: 0.40
Nodes (5): TourStep, Body, SectionName, Title, TourPage

### Community 104 - "build_icon.py"
Cohesion: 0.50
Nodes (4): main(), Image, Build the Windows application icon from the brand mark. The mark is supplied as…, render()

### Community 105 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 107 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 108 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 109 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 110 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 111 - "Nextcalibur 0.5.8"
Cohesion: 0.50
Nodes (3): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on

### Community 112 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 115 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

## Knowledge Gaps
- **382 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+377 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 657 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **22 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `SystemMode`, `.Main`, `LedState`, `AppSettings`, `GpuClockReader`, `Window`, `RadioButton`, `UserPresence`, `WindowsFaults`, `TrayPresence`, `.Get`, `CpuPowerReader`, `Program`, `.RequestRestart`, `.Get`, `.OnLoaded`, `EcMailbox`, `DependencyStatus`, `InvalidOperationException`, `UpdateService`, `.PowerModeControls`, `.StartSlowTimer`, `.OfferDependency`, `ThemeService`, `BacklightKeyWatcher`, `Nextcalibur.Core.Configuration`, `Grid`, `.Info`, `.WireSettingsPage`, `NvidiaDriverState`, `SupportVerdict`, `.PlaceTourStep`, `Action`, `TourPage`, `.OnBrightnessChanged`, `TourStep`?**
  _High betweenness centrality (0.499) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `RadioButton`, `Button`, `ToggleButton`, `Grid`, `Border`, `Text`, `.OnBrightnessChanged`, `StackPanel`, `Keycaps`, `DriveGauge`, `CpuWarnValue`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`?**
  _High betweenness centrality (0.174) - this node is a cross-community bridge._
- **Why does `DependencyStatus` connect `DependencyStatus` to `.OfferDependency`, `MainWindow`, `Dependency`?**
  _High betweenness centrality (0.167) - this node is a cross-community bridge._
- **Are the 11 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 11 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _382 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `SystemMode` be split into smaller, more focused modules?**
  _Cohesion score 0.05067920585161965 - nodes in this community are weakly interconnected._
- **Should `DotNetRuntimeDependency` be split into smaller, more focused modules?**
  _Cohesion score 0.05561105561105561 - nodes in this community are weakly interconnected._