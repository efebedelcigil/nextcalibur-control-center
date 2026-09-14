# Graph Report - nextcalibur-control-center  (2026-09-14)

## Corpus Check
- 131 files · ~186,119 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2173 nodes · 4495 edges · 145 communities (114 shown, 20 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 250 edges (avg confidence: 0.82)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `01eb1966`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- DotNetRuntimeDependency
- Nextcalibur.Core.Hardware
- Dependency
- MainWindow
- GpuClockReader
- UserPresence
- IShellLinkW
- Window
- SystemInfo
- WindowsFaults
- RadioButton
- .Get
- WindowsFaultsTests
- EcMailbox
- Button
- .Get
- AppSettings
- CpuPowerReader
- .Survey
- ColourWheel
- .PlaceTourStep
- .Info
- .Warn
- SystemMode
- InvalidOperationException
- Program
- Elevation
- DllImport
- .OnSystemModeChanged
- Fact
- .Check
- MemoryTrimmer
- PowerOverlayService
- .CalculateThreadShare
- .InstallAsync
- Nextcalibur.Core.csproj
- .OnLoaded
- SegmentedBar
- .PowerModeControls
- .RefreshBanner
- UpdateService
- Fact
- ResourceDictionary
- ToggleButton
- LogInjectionTests
- .GetColour
- .IsSignedBy
- Nextcalibur.Core.Dependencies
- Unelevated
- LedController
- LedState
- HostileFilesTests
- FanGauge
- DonutGauge
- ThemeService
- StringsDictionaryTests
- DevicePowerState
- CoreLoad
- .StartSlowTimer
- .HasAccess
- BatteryModePolicy
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- Nextcalibur Control Center
- Strings
- Grid
- .WatchTheEssentialDrivers
- PendingRestartInfo
- .Migrate
- SmiCommandTests
- RpmToDoubleConverter
- Hardware Protocol
- Nextcalibur 0.5.3
- DllImport
- LedBrightness
- .SignatureIsValid
- IntPtr
- InstallFolderGuard
- Log
- SupportVerdict
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- Border
- LedEffect
- SystemTools
- Nextcalibur 0.5.1
- Nextcalibur 0.5.5
- AuthenticodeTests
- analyse-autopsy.py
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Releasing, and signing
- Security
- .Pick
- TourPage
- PowerSource
- .Values_are_the_ones_measured_from_the_vendor
- Log-Graphics.ps1
- Privacy
- .OnBrightnessChanged
- StackPanel
- LedZone
- MachineWideGate
- NduFix
- Notice
- SmiSubsystem
- build_icon.py
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Nextcalibur 0.5.6
- .TryParseRgb
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
1. `MainWindow` - 207 edges
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
- `CleanMachineTests` --references--> `OverlayDiagnosis`  [EXTRACTED]
  tests/Nextcalibur.Core.Tests/PolicyTests.cs → src/Nextcalibur.Core/Power/PowerOverlay.cs
- `MainWindow` --inherits--> `Window`  [EXTRACTED]
  src/Nextcalibur.App/MainWindow.Settings.cs → src/Nextcalibur.Core/Hardware/WindowsFaults.cs
- `Probe` --inherits--> `Dependency`  [EXTRACTED]
  tests/Nextcalibur.Core.Tests/Attacks/HostileAnswerTests.cs → src/Nextcalibur.Core/Dependencies/Dependency.cs
- `GpuModeTests` --references--> `GpuConfiguration`  [EXTRACTED]
  tests/Nextcalibur.Core.Tests/GpuModeTests.cs → src/Nextcalibur.Core/Hardware/GpuModeService.cs
- `MainWindow` --defines--> `_gpuPendingRestart`  [EXTRACTED]
  src/Nextcalibur.App/MainWindow.Settings.cs → src/Nextcalibur.App/MainWindow.xaml.cs

## Import Cycles
- None detected.

## Communities (145 total, 20 thin omitted)

### Community 0 - "DotNetRuntimeDependency"
Cohesion: 0.06
Nodes (43): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+35 more)

### Community 1 - "Nextcalibur.Core.Hardware"
Cohesion: 0.06
Nodes (24): Nextcalibur.App, Nextcalibur.Cli, Nextcalibur.Core.Tests, Nextcalibur.Core, Nextcalibur.Core.Configuration, Nextcalibur.Core.Power, Nextcalibur.Core.Hardware, DllImport (+16 more)

### Community 2 - "Dependency"
Cohesion: 0.06
Nodes (44): HttpStatusCode, CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner (+36 more)

### Community 3 - "MainWindow"
Cohesion: 0.05
Nodes (34): DeferredOverheat, ObservableCollection, Queue, Slider, SolidColorBrush, LedPower, Button, Grid (+26 more)

### Community 4 - "GpuClockReader"
Cohesion: 0.09
Nodes (18): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+10 more)

### Community 5 - "UserPresence"
Cohesion: 0.08
Nodes (22): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, Action, Toasts, DateTime, DllImport (+14 more)

### Community 6 - "IShellLinkW"
Cohesion: 0.07
Nodes (13): PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder, AppIdentity (+5 more)

### Community 7 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 8 - "SystemInfo"
Cohesion: 0.07
Nodes (22): MemoryStatusEx, DriveUse, INotifyPropertyChanged, DriveRow, Detail, Name, Percent, PercentText (+14 more)

### Community 9 - "WindowsFaults"
Cohesion: 0.08
Nodes (31): Process, ProcessMetrics, Share, DateTime, Dictionary, Func, IReadOnlyList, List (+23 more)

### Community 10 - "RadioButton"
Cohesion: 0.07
Nodes (34): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, NavDisplay, NavLighting (+26 more)

### Community 11 - ".Get"
Cohesion: 0.10
Nodes (13): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, MessageBoxButton, MessageBoxResult, EventArgs (+5 more)

### Community 13 - "EcMailbox"
Cohesion: 0.13
Nodes (13): Exception, FirmwareModeReading, IDisposable, ManagementObject, Func, EcMailbox, EcMailboxUnavailableException, Held (+5 more)

### Community 14 - "Button"
Cohesion: 0.07
Nodes (24): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, HideToTrayButton, MinimiseButton (+16 more)

### Community 15 - ".Get"
Cohesion: 0.13
Nodes (9): IList, GpuLoad, GpuConfiguration, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests (+1 more)

### Community 16 - "AppSettings"
Cohesion: 0.07
Nodes (27): JsonElement, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoInstallUpdates, CompensateWindowsFaults, CpuWarningTemperatureC, DisableNdu (+19 more)

### Community 17 - "CpuPowerReader"
Cohesion: 0.15
Nodes (9): SafeFileHandle, DllImport, IntPtr, CpuPowerReader, DriverInstalled, Fact, InlineData, Theory (+1 more)

### Community 18 - ".Survey"
Cohesion: 0.13
Nodes (9): Func, IEnumerable, IReadOnlyList, Version, Footprint, SettingsPath, RetirementEntry, Trace (+1 more)

### Community 19 - "ColourWheel"
Cohesion: 0.11
Nodes (18): BitmapSource, Control, DependencyObject, DependencyPropertyChangedEventArgs, Ellipse, Hue, Saturation, Canvas (+10 more)

### Community 20 - ".PlaceTourStep"
Cohesion: 0.10
Nodes (12): CancelEventArgs, KeyEventArgs, SizeChangedEventArgs, Point, RoutedEventArgs, Size, TourStep, Body (+4 more)

### Community 21 - ".Info"
Cohesion: 0.17
Nodes (5): Application, DllImport, Mutex, App, STAThread

### Community 22 - ".Warn"
Cohesion: 0.12
Nodes (8): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, FixButton, Action, ToggleButton

### Community 23 - "SystemMode"
Cohesion: 0.17
Nodes (12): Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, SystemMode, Gaming (+4 more)

### Community 24 - "InvalidOperationException"
Cohesion: 0.14
Nodes (13): ArgumentNullException, InvalidOperationException, ModeDiscrete, ModeHybrid, ModeUma, FirmwareModeReading, GpuMode, Discrete (+5 more)

### Community 25 - "Program"
Cohesion: 0.16
Nodes (5): ConsoleColor, Program, DateTimeOffset, ThermalReader, ThermalSample

### Community 26 - "Elevation"
Cohesion: 0.16
Nodes (3): Encoding, Elevation, CardSwitchTasks

### Community 27 - "DllImport"
Cohesion: 0.14
Nodes (6): DllImport, EventArgs, IntPtr, MarshalAs, TokenPrivileges, TokenPrivileges

### Community 28 - ".OnSystemModeChanged"
Cohesion: 0.16
Nodes (4): PowerModeChangedEventArgs, ModeGaming, ModeOffice, ModePerformance

### Community 29 - "Fact"
Cohesion: 0.12
Nodes (11): IReadOnlyList, RepairOutcome, Empty, Fact, Task, CleanMachineTests, Guarded, Unguarded (+3 more)

### Community 30 - ".Check"
Cohesion: 0.18
Nodes (8): ManagementScope, RegistryKey, RawSecurityDescriptor, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported

### Community 31 - "MemoryTrimmer"
Cohesion: 0.14
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 32 - "PowerOverlayService"
Cohesion: 0.18
Nodes (11): DllImport, Guid, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, PowerModeOption, PowerOverlays (+3 more)

### Community 33 - ".CalculateThreadShare"
Cohesion: 0.15
Nodes (9): IReadOnlyDictionary, Stable, Dictionary, Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests (+1 more)

### Community 34 - ".InstallAsync"
Cohesion: 0.16
Nodes (13): FileStream, IOException, Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress (+5 more)

### Community 35 - "Nextcalibur.Core.csproj"
Cohesion: 0.12
Nodes (13): net8.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.0), System.Management (8.0.0), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk, net8.0-windows (+5 more)

### Community 36 - ".OnLoaded"
Cohesion: 0.17
Nodes (5): AssemblyInformationalVersionAttribute, DependencyStatus, Missing, NeedsAction, Outdated

### Community 37 - "SegmentedBar"
Cohesion: 0.11
Nodes (13): Nextcalibur.App.Controls, FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush (+5 more)

### Community 38 - ".PowerModeControls"
Cohesion: 0.12
Nodes (7): EventArrivedEventArgs, ManagementEventWatcher, Option, Button, IEnumerable, TextBlock, BacklightKeyWatcher

### Community 39 - ".RefreshBanner"
Cohesion: 0.19
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 40 - "UpdateService"
Cohesion: 0.14
Nodes (13): DispatcherTimer, Func, HashSet, IProgress, Task, TimeSpan, UpdateService, AutomaticChecksEnabled (+5 more)

### Community 41 - "Fact"
Cohesion: 0.16
Nodes (5): Fact, ForwardCompatibilityTests, MailboxAccessTests, SettingsTests, StartupPreferenceTests

### Community 42 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 43 - "ToggleButton"
Cohesion: 0.12
Nodes (16): OverheatWarningToggle, SelectAll, SettingAutoCheckUpdates, SettingAutoInstallUpdates, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost, SettingFixWidgets (+8 more)

### Community 44 - "LogInjectionTests"
Cohesion: 0.19
Nodes (7): Exception, Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 45 - ".GetColour"
Cohesion: 0.24
Nodes (7): B, G, R, Fact, InlineData, Theory, LedStateTests

### Community 46 - ".IsSignedBy"
Cohesion: 0.21
Nodes (7): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft

### Community 47 - "Nextcalibur.Core.Dependencies"
Cohesion: 0.16
Nodes (3): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks

### Community 48 - "Unelevated"
Cohesion: 0.33
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 49 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 50 - "LedState"
Cohesion: 0.15
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedState, ActiveProfile, BrightnessPercent (+5 more)

### Community 51 - "HostileFilesTests"
Cohesion: 0.28
Nodes (4): IEnumerable, ProfileFiles, Fact, HostileFilesTests

### Community 52 - "FanGauge"
Cohesion: 0.16
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 53 - "DonutGauge"
Cohesion: 0.17
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 54 - "ThemeService"
Cohesion: 0.15
Nodes (11): Theme, Theme, Dark, Light, ThemePreference, Dark, Light, System (+3 more)

### Community 55 - "StringsDictionaryTests"
Cohesion: 0.24
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 56 - "DevicePowerState"
Cohesion: 0.28
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 57 - "CoreLoad"
Cohesion: 0.17
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 58 - ".StartSlowTimer"
Cohesion: 0.17
Nodes (3): Func, Task, UninstallCommand

### Community 59 - ".HasAccess"
Cohesion: 0.28
Nodes (5): SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 60 - "BatteryModePolicy"
Cohesion: 0.42
Nodes (4): BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

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

### Community 65 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 66 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 67 - ".WatchTheEssentialDrivers"
Cohesion: 0.24
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 68 - "PendingRestartInfo"
Cohesion: 0.18
Nodes (8): Dictionary, List, PendingRestartInfo, BootTimeUtc, ReasonArguments, Reasons, StartupRegistration, IsEnabled

### Community 69 - ".Migrate"
Cohesion: 0.24
Nodes (3): DateTime, InlineData, Theory

### Community 70 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 71 - "RpmToDoubleConverter"
Cohesion: 0.33
Nodes (6): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, Type

### Community 72 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 73 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 74 - "DllImport"
Cohesion: 0.29
Nodes (5): EnumWindowsProc, IO_COUNTERS, PROCESS_MEMORY_COUNTERS, DllImport, IntPtr

### Community 75 - "LedBrightness"
Cohesion: 0.18
Nodes (8): LedBrightness, Full, Half, Off, Fact, InlineData, Theory, BacklightKeyTests

### Community 76 - ".SignatureIsValid"
Cohesion: 0.31
Nodes (9): DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, Authenticode, WinTrustData, WinTrustFileInfo, WinTrustData (+1 more)

### Community 77 - "IntPtr"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 78 - "InstallFolderGuard"
Cohesion: 0.33
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 80 - "SupportVerdict"
Cohesion: 0.22
Nodes (9): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+1 more)

### Community 81 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 82 - "README.md"
Cohesion: 0.33
Nodes (3): How it works, Questions people ask, Using it

### Community 83 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 84 - "Border"
Cohesion: 0.22
Nodes (9): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, ThemeSwitch, TourCard (+1 more)

### Community 85 - "LedEffect"
Cohesion: 0.22
Nodes (8): LedEffect, Blink, Breathing, ColourCycle, Heartbeat, Off, Static, Wave

### Community 86 - "SystemTools"
Cohesion: 0.22
Nodes (7): SystemTools, Cmd, Explorer, Pnputil, Powercfg, PowerShell, Schtasks

### Community 87 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 88 - "Nextcalibur 0.5.5"
Cohesion: 0.25
Nodes (7): Asynchronous dispatcher & UI thread hardening, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture

### Community 90 - "analyse-autopsy.py"
Cohesion: 0.43
Nodes (7): describe_file(), load(), main(), Path, Compare autopsy snapshots taken before, during and after an install. python…, section(), vendorish()

### Community 91 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 92 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 93 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 94 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 95 - "Security"
Cohesion: 0.29
Nodes (7): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, The threat model, and what the code does about it, What counts

### Community 96 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 97 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 98 - "PowerSource"
Cohesion: 0.33
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 99 - ".Values_are_the_ones_measured_from_the_vendor"
Cohesion: 0.33
Nodes (4): Fact, InlineData, Theory, ThermalProfileTests

### Community 100 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 101 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 102 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 103 - "StackPanel"
Cohesion: 0.33
Nodes (6): BannerActions, DialogActions, DriveTextStack, PanelWindowsFaultsSubOptions, ProfileRow, StackPanel

### Community 104 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 105 - "MachineWideGate"
Cohesion: 0.40
Nodes (3): Mutex, TimeSpan, MachineWideGate

### Community 107 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 108 - "SmiSubsystem"
Cohesion: 0.40
Nodes (5): SmiSubsystem, DisplayMode, Led, Profile, Thermal

### Community 109 - "build_icon.py"
Cohesion: 0.50
Nodes (4): main(), Image, Build the Windows application icon from the brand mark. The mark is supplied as…, render()

### Community 110 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 112 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 113 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 114 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 115 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 116 - ".TryParseRgb"
Cohesion: 0.50
Nodes (3): B, G, R

### Community 117 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 120 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

## Knowledge Gaps
- **379 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+374 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 653 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **20 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `Nextcalibur.Core.Hardware`, `GpuClockReader`, `UserPresence`, `Window`, `SystemInfo`, `WindowsFaults`, `RadioButton`, `.Get`, `EcMailbox`, `Button`, `.Get`, `AppSettings`, `CpuPowerReader`, `.PlaceTourStep`, `.Warn`, `SystemMode`, `InvalidOperationException`, `Program`, `DllImport`, `.OnSystemModeChanged`, `PowerOverlayService`, `.OnLoaded`, `.PowerModeControls`, `.RefreshBanner`, `UpdateService`, `ToggleButton`, `LogInjectionTests`, `LedController`, `ThemeService`, `.StartSlowTimer`, `BatteryModePolicy`, `Strings`, `Grid`, `.WatchTheEssentialDrivers`, `SupportVerdict`, `LedEffect`, `TourPage`, `.OnBrightnessChanged`, `LedZone`?**
  _High betweenness centrality (0.492) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `PART_ContentHost`, `TourCanvas`, `Wheel`, `MainWindow`, `RadioButton`, `Button`, `.Warn`, `InvalidOperationException`, `.OnSystemModeChanged`, `ToggleButton`, `Grid`, `Border`, `Text`, `.OnBrightnessChanged`, `StackPanel`, `Keycaps`, `DriveGauge`, `CpuWarnValue`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`?**
  _High betweenness centrality (0.175) - this node is a cross-community bridge._
- **Why does `DependencyStatus` connect `.OnLoaded` to `Dependency`, `.InstallAsync`, `MainWindow`?**
  _High betweenness centrality (0.156) - this node is a cross-community bridge._
- **Are the 11 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 11 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _379 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DotNetRuntimeDependency` be split into smaller, more focused modules?**
  _Cohesion score 0.05673274094326726 - nodes in this community are weakly interconnected._
- **Should `Nextcalibur.Core.Hardware` be split into smaller, more focused modules?**
  _Cohesion score 0.05641025641025641 - nodes in this community are weakly interconnected._