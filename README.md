# Graph Report - nextcalibur-control-center  (2026-09-23)

## Corpus Check
- 141 files · ~196,056 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2400 nodes · 5090 edges · 145 communities (115 shown, 30 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 252 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `1e17207b`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- DotNetRuntimeDependency
- SystemModeService
- Authenticode
- UserPresence
- AppSettings
- GpuClockReader
- MainWindow
- ProtectedStore
- TrayPresence
- .CheckProgram
- Window
- IShellLinkW
- SystemInfo
- CpuPowerReader
- RadioButton
- .Survey
- .Main
- Nextcalibur.App
- WindowsFaults
- .OnLoaded
- xunit
- Nextcalibur.Core.Hardware
- SystemTools
- WindowsFaultsTests
- Button
- .RequestRestart
- ProcessPresence
- .PlaceTourStep
- InstallFolderGuard
- LedState
- EcMailbox
- .Get
- .Check
- Fact
- Program
- SystemMode
- Elevation
- .Warn
- .OnGpuModeChanged
- Nextcalibur.Core.Configuration
- Nextcalibur.Core.csproj
- DependencyStatus
- analyse-autopsy.py
- ResourceDictionary
- UpdateService
- BacklightKeyWatcher
- .Get
- ToggleButton
- ColourWheel
- .Ask
- Unelevated
- CoreLoad
- LedController
- .Info
- FanGauge
- ProcessMetrics
- DonutGauge
- Nextcalibur.Core.Dependencies
- SegmentedBar
- LedEffect
- LogInjectionTests
- .OnEffectChecked
- PawnIoDependency
- .HasAccess
- BatteryModePolicy
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
- .WireSettingsPage
- ReleaseFile
- .Check
- Border
- ThemeService
- .Sanitised
- NvidiaDriverState
- .CalculateThreadShare
- WordsInCodeTests
- IntPtr
- AppIdentity.cs
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- Probe
- Nextcalibur 0.5.1
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Releasing, and signing
- .Pick
- TourPage
- Log-Graphics.ps1
- Privacy
- Security
- StackPanel
- LedZone
- MachineWideGate
- PowerSource
- Notice
- .Set
- SmiSubsystem
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Nextcalibur 0.5.6
- .ArrangeOverride
- BrightnessSlider
- .TryParseRgb
- .SendAsync
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
1. `MainWindow` - 222 edges
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
- `Zero-bottleneck gaming & heavy workload architecture` --references--> `MemoryTrimmer`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.Core/Hardware/MemoryTrimmer.cs
- `Zero-bottleneck gaming & heavy workload architecture` --references--> `WindowsFaults`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.Core/Hardware/WindowsFaults.cs
- `Fix GPU Switch Restart Prompt & Windows Privilege Adjustment` --references--> `TokenPrivileges`  [INFERRED]
  docs/releases/0.5.8.md → src/Nextcalibur.App/MainWindow.xaml.cs

## Import Cycles
- None detected.

## Communities (145 total, 30 thin omitted)

### Community 0 - "DotNetRuntimeDependency"
Cohesion: 0.06
Nodes (43): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+35 more)

### Community 1 - "SystemModeService"
Cohesion: 0.05
Nodes (33): InvalidOperationException, Option, Button, IEnumerable, DllImport, Guid, IReadOnlyList, OverlayDiagnosis (+25 more)

### Community 2 - "Authenticode"
Cohesion: 0.06
Nodes (38): CatalogInfo, Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on (+30 more)

### Community 3 - "UserPresence"
Cohesion: 0.05
Nodes (37): MEMORYSTATUSEX, MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, Action, Toasts, DateTime (+29 more)

### Community 4 - "AppSettings"
Cohesion: 0.05
Nodes (40): DateTime, Dictionary, JsonElement, List, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoInstallUpdates (+32 more)

### Community 5 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 6 - "MainWindow"
Cohesion: 0.05
Nodes (35): DeferredOverheat, ObservableCollection, Queue, RoutedPropertyChangedEventArgs, Slider, DateTime, HashSet, List (+27 more)

### Community 7 - "ProtectedStore"
Cohesion: 0.09
Nodes (20): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+12 more)

### Community 8 - "TrayPresence"
Cohesion: 0.06
Nodes (21): ContextMenuStrip, DevPropKey, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, EventHandler (+13 more)

### Community 9 - ".CheckProgram"
Cohesion: 0.07
Nodes (23): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvmlPath, IntegrityFinding (+15 more)

### Community 10 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 11 - "IShellLinkW"
Cohesion: 0.07
Nodes (13): PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder, AppIdentity (+5 more)

### Community 12 - "SystemInfo"
Cohesion: 0.08
Nodes (22): MemoryStatusEx, DriveUse, INotifyPropertyChanged, DriveRow, Detail, Name, Percent, PercentText (+14 more)

### Community 13 - "CpuPowerReader"
Cohesion: 0.09
Nodes (21): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+13 more)

### Community 14 - "RadioButton"
Cohesion: 0.08
Nodes (31): ModeGaming, ModeOffice, ModePerformance, NavDisplay, NavLighting, NavPower, NavSettings, NavSystem (+23 more)

### Community 15 - ".Survey"
Cohesion: 0.10
Nodes (8): Func, IEnumerable, IReadOnlyList, Version, RetirementEntry, Trace, NduFix, FootprintTests

### Community 16 - ".Main"
Cohesion: 0.11
Nodes (8): The threat model, and what the code does about it, Application, DllImport, Mutex, App, RegistryKey, Footprint, STAThread

### Community 17 - "Nextcalibur.App"
Cohesion: 0.11
Nodes (19): Nextcalibur.App, Nextcalibur.App.Controls, system_componentmodel, system_drawing, system_io, system_linq, system_runtime_compilerservices, system_windows (+11 more)

### Community 18 - "WindowsFaults"
Cohesion: 0.11
Nodes (21): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, DateTime, DllImport, Func (+13 more)

### Community 19 - ".OnLoaded"
Cohesion: 0.11
Nodes (9): AssemblyInformationalVersionAttribute, Asynchronous dispatcher & UI thread hardening, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, BannerRestartButton (+1 more)

### Community 20 - "xunit"
Cohesion: 0.13
Nodes (8): Nextcalibur.Core.Tests, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_security_accesscontrol, system_security_cryptography_x509certificates, system_text_regularexpressions, system_xml_linq, xunit

### Community 21 - "Nextcalibur.Core.Hardware"
Cohesion: 0.10
Nodes (11): Nextcalibur.Core.Power, Nextcalibur.Core.Hardware, microsoft_win32, microsoft_win32_safehandles, Theme, Dark, Light, system_management (+3 more)

### Community 22 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 23 - "WindowsFaultsTests"
Cohesion: 0.20
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 24 - "Button"
Cohesion: 0.08
Nodes (22): IsChecked, BannerDismissButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, HideToTrayButton, MinimiseButton, OpenLogButton (+14 more)

### Community 25 - ".RequestRestart"
Cohesion: 0.12
Nodes (11): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr (+3 more)

### Community 26 - "ProcessPresence"
Cohesion: 0.14
Nodes (11): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+3 more)

### Community 27 - ".PlaceTourStep"
Cohesion: 0.09
Nodes (12): KeyEventArgs, SizeChangedEventArgs, FrameworkElement, Point, RoutedEventArgs, Size, TourStep, Body (+4 more)

### Community 28 - "InstallFolderGuard"
Cohesion: 0.14
Nodes (10): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Fact, InlineData, Theory, ElevationPolicyTests (+2 more)

### Community 29 - "LedState"
Cohesion: 0.15
Nodes (15): B, G, R, LedState, ActiveProfile, BrightnessPercent, Current, Effect (+7 more)

### Community 30 - "EcMailbox"
Cohesion: 0.16
Nodes (10): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+2 more)

### Community 31 - ".Get"
Cohesion: 0.17
Nodes (9): ArgumentNullException, GpuLoad, GpuConfiguration, GpuModeService, SwitchOutcome, Func, Words, Resolver (+1 more)

### Community 32 - ".Check"
Cohesion: 0.13
Nodes (10): CommonAce, RawSecurityDescriptor, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported (+2 more)

### Community 33 - "Fact"
Cohesion: 0.17
Nodes (7): IList, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 34 - "Program"
Cohesion: 0.16
Nodes (5): ConsoleColor, Program, DateTimeOffset, ThermalReader, ThermalSample

### Community 35 - "SystemMode"
Cohesion: 0.15
Nodes (12): SmiFamily, Read, Write, ThermalProfile, SystemMode, Gaming, Office, Performance (+4 more)

### Community 36 - "Elevation"
Cohesion: 0.19
Nodes (3): Encoding, Elevation, CardSwitchTasks

### Community 37 - ".Warn"
Cohesion: 0.13
Nodes (5): PowerModeChangedEventArgs, Func, MessageBoxButton, MessageBoxResult, UninstallCommand

### Community 38 - ".OnGpuModeChanged"
Cohesion: 0.11
Nodes (10): ModeDiscrete, ModeHybrid, ModeUma, RadioButton, Task, FirmwareModeReading, GpuMode, Discrete (+2 more)

### Community 39 - "Nextcalibur.Core.Configuration"
Cohesion: 0.17
Nodes (7): Nextcalibur.Cli, Nextcalibur.Core.Configuration, system_diagnostics, system_security_cryptography, system_security_principal, system_text_json, system_text_json_serialization

### Community 40 - "Nextcalibur.Core.csproj"
Cohesion: 0.12
Nodes (13): net8.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Management (8.0.0), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk, net8.0-windows (+5 more)

### Community 41 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 42 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 43 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 44 - "UpdateService"
Cohesion: 0.15
Nodes (13): DispatcherTimer, Func, HashSet, IProgress, Task, TimeSpan, UpdateService, AutomaticChecksEnabled (+5 more)

### Community 45 - "BacklightKeyWatcher"
Cohesion: 0.14
Nodes (11): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, LedBrightness, Full, Half, Off, Fact (+3 more)

### Community 47 - "ToggleButton"
Cohesion: 0.12
Nodes (16): OverheatWarningToggle, SelectAll, SettingAutoCheckUpdates, SettingAutoInstallUpdates, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost, SettingFixWidgets (+8 more)

### Community 48 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 49 - ".Ask"
Cohesion: 0.35
Nodes (7): HttpStatusCode, Fact, InlineData, Task, Theory, Answer, HostileAnswerTests

### Community 50 - "Unelevated"
Cohesion: 0.33
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 51 - "CoreLoad"
Cohesion: 0.13
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 52 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 53 - ".Info"
Cohesion: 0.22
Nodes (4): CancelEventArgs, Exception, Log, Folder

### Community 54 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 55 - "ProcessMetrics"
Cohesion: 0.15
Nodes (14): Share, Dictionary, TimeSpan, FaultEvidence, ProcessMetrics, Name, PageFaultCount, Pid (+6 more)

### Community 56 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 57 - "Nextcalibur.Core.Dependencies"
Cohesion: 0.25
Nodes (5): Nextcalibur.Core.Dependencies, system_collections_concurrent, system_net, system_net_http, system_text

### Community 58 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 59 - "LedEffect"
Cohesion: 0.14
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 60 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 61 - ".OnEffectChecked"
Cohesion: 0.17
Nodes (8): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, LedPower, ReloadButton

### Community 62 - "PawnIoDependency"
Cohesion: 0.15
Nodes (11): CancellationToken, HttpClient, Task, Version, PawnIoDependency, ExpectedSigner, Id, Name (+3 more)

### Community 63 - ".HasAccess"
Cohesion: 0.28
Nodes (5): SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 64 - "BatteryModePolicy"
Cohesion: 0.42
Nodes (4): BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

### Community 65 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 66 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 67 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 68 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 69 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 70 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 71 - "Dependency"
Cohesion: 0.17
Nodes (9): Version, Dependency, ExpectedSigner, Id, MayBeRemoved, Name, Purpose, SilentInstallArguments (+1 more)

### Community 72 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 73 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 74 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 76 - "ReleaseFile"
Cohesion: 0.33
Nodes (6): CancellationToken, HttpClient, Task, Uri, ReleaseFile, HttpClient

### Community 77 - ".Check"
Cohesion: 0.22
Nodes (9): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+1 more)

### Community 78 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 79 - "ThemeService"
Cohesion: 0.20
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 80 - ".Sanitised"
Cohesion: 0.33
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 81 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 82 - ".CalculateThreadShare"
Cohesion: 0.27
Nodes (4): IReadOnlyDictionary, Stable, Dictionary, TopSharePercent

### Community 83 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 84 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 85 - "AppIdentity.cs"
Cohesion: 0.22
Nodes (6): Nextcalibur.Core, system_globalization, system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 86 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 87 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 88 - "README.md"
Cohesion: 0.33
Nodes (3): How it works, Questions people ask, Using it

### Community 89 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 90 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 91 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

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

### Community 98 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 99 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 100 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 101 - "StackPanel"
Cohesion: 0.33
Nodes (6): BannerActions, DialogActions, DriveTextStack, PanelWindowsFaultsSubOptions, ProfileRow, StackPanel

### Community 102 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 103 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 104 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 105 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 107 - "SmiSubsystem"
Cohesion: 0.40
Nodes (5): SmiSubsystem, DisplayMode, Led, Profile, Thermal

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

### Community 115 - "BrightnessSlider"
Cohesion: 0.50
Nodes (4): BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 116 - ".TryParseRgb"
Cohesion: 0.50
Nodes (3): B, G, R

### Community 117 - ".SendAsync"
Cohesion: 0.50
Nodes (3): CancellationToken, HttpRequestMessage, HttpResponseMessage

### Community 118 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 121 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

## Knowledge Gaps
- **387 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+382 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 734 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **30 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `SystemModeService`, `UserPresence`, `AppSettings`, `GpuClockReader`, `TrayPresence`, `.CheckProgram`, `Window`, `SystemInfo`, `CpuPowerReader`, `RadioButton`, `Nextcalibur.App`, `WindowsFaults`, `.OnLoaded`, `Button`, `.RequestRestart`, `ProcessPresence`, `.PlaceTourStep`, `EcMailbox`, `.Get`, `Program`, `SystemMode`, `.Warn`, `.OnGpuModeChanged`, `DependencyStatus`, `UpdateService`, `BacklightKeyWatcher`, `.Get`, `ToggleButton`, `LedController`, `.Info`, `LedEffect`, `.OnEffectChecked`, `BatteryModePolicy`, `Strings`, `Grid`, `.WireSettingsPage`, `.Check`, `ThemeService`, `NvidiaDriverState`, `TourPage`, `LedZone`?**
  _High betweenness centrality (0.465) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `MainWindow`, `RadioButton`, `.OnLoaded`, `Button`, `.OnGpuModeChanged`, `ToggleButton`, `.OnEffectChecked`, `Grid`, `Border`, `Text`, `StackPanel`, `Keycaps`, `DriveGauge`, `BrightnessSlider`, `CpuWarnValue`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`?**
  _High betweenness centrality (0.159) - this node is a cross-community bridge._
- **Why does `DependencyStatus` connect `DependencyStatus` to `MainWindow`, `Dependency`, `ReleaseFile`, `.OnLoaded`, `Nextcalibur.Core.Dependencies`?**
  _High betweenness centrality (0.083) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _387 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DotNetRuntimeDependency` be split into smaller, more focused modules?**
  _Cohesion score 0.05673274094326726 - nodes in this community are weakly interconnected._
- **Should `SystemModeService` be split into smaller, more focused modules?**
  _Cohesion score 0.05189189189189189 - nodes in this community are weakly interconnected._