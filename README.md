# Graph Report - nextcalibur-control-center  (2026-09-23)

## Corpus Check
- 144 files · ~197,929 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2421 nodes · 5119 edges · 158 communities (125 shown, 33 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 252 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `75c7cc67`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- DotNetRuntimeDependency
- .CheckProgram
- Authenticode
- ProtectedStore
- GpuClockReader
- TrayPresence
- MainWindow
- RadioButton
- Window
- IShellLinkW
- CpuPowerReader
- UserPresence
- Fact
- WindowsFaults
- WindowsFaultsTests
- SystemInfo
- .Main
- AppSettings
- ProcessPresence
- .Info
- Nextcalibur.Core.Hardware
- Elevation
- .RequestRestart
- Fact
- SystemModeService
- .Get
- .OnLoaded
- EcMailbox
- LedState
- Fact
- Nextcalibur.App
- .Warn
- GpuModeService
- Button
- .Check
- MemoryTrimmer
- RoutedEventArgs
- .Read
- .Migrate
- system_runtime_interopservices
- Nextcalibur.Core.csproj
- DependencyStatus
- Dependency
- .CalculateThreadShare
- analyse-autopsy.py
- UpdateService
- Program
- PowerOverlayService
- ResourceDictionary
- MainWindow.xaml.cs
- ToggleButton
- ColourWheel
- system_diagnostics
- BacklightKeyWatcher
- .Ask
- LedController
- FanGauge
- HostileFilesTests
- BatteryModePolicy
- Unelevated
- ProcessMetrics
- DonutGauge
- SystemMode
- .IsUnderProgramFiles
- Nextcalibur.Core.Tests.Attacks
- Nextcalibur.Core.Configuration
- SegmentedBar
- LedEffect
- LogInjectionTests
- SystemTools
- PawnIoDependency
- .HasAccess
- StringsDictionaryTests
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- CoreLoad
- Nextcalibur Control Center
- Strings
- Grid
- SmiCommandTests
- Hardware Protocol
- Nextcalibur 0.5.3
- ThemeService
- .Sanitised
- RpmToDoubleConverter
- Border
- NvidiaDriverState
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- ThermalSample
- Log
- SupportVerdict
- ProfileFiles
- Probe
- ValueConverters.cs
- Nextcalibur 0.5.1
- NduFix
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Releasing, and signing
- DriveRow
- .Pick
- .OnThresholdMoved
- TourPage
- Log-Graphics.ps1
- Privacy
- Security
- .PowerModeControls
- .OnBrightnessChanged
- StackPanel
- LedZone
- MachineWideGate
- PowerSource
- .LatestAsync
- Notice
- .Set
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Nextcalibur 0.5.10
- Nextcalibur 0.5.6
- .ArrangeOverride
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
- `Zero-bottleneck gaming & heavy workload architecture` --references--> `MemoryTrimmer`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.Core/Hardware/MemoryTrimmer.cs
- `Zero-bottleneck gaming & heavy workload architecture` --references--> `WindowsFaults`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.Core/Hardware/WindowsFaults.cs
- `Fix GPU Switch Restart Prompt & Windows Privilege Adjustment` --references--> `TokenPrivileges`  [INFERRED]
  docs/releases/0.5.8.md → src/Nextcalibur.App/MainWindow.xaml.cs

## Import Cycles
- None detected.

## Communities (158 total, 33 thin omitted)

### Community 0 - "DotNetRuntimeDependency"
Cohesion: 0.05
Nodes (46): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+38 more)

### Community 1 - ".CheckProgram"
Cohesion: 0.06
Nodes (27): Lazy, ProcessModule, FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Dictionary, HashSet (+19 more)

### Community 2 - "Authenticode"
Cohesion: 0.08
Nodes (27): CatalogInfo, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle, X509Certificate2 (+19 more)

### Community 3 - "ProtectedStore"
Cohesion: 0.09
Nodes (20): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+12 more)

### Community 4 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 5 - "TrayPresence"
Cohesion: 0.06
Nodes (21): ContextMenuStrip, DevPropKey, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, EventHandler (+13 more)

### Community 6 - "MainWindow"
Cohesion: 0.06
Nodes (33): DeferredOverheat, ObservableCollection, Queue, HideToTrayButton, DateTime, HashSet, List, TimeSpan (+25 more)

### Community 7 - "RadioButton"
Cohesion: 0.06
Nodes (40): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeDiscrete, ModeGaming (+32 more)

### Community 8 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 9 - "IShellLinkW"
Cohesion: 0.07
Nodes (13): PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder, AppIdentity (+5 more)

### Community 10 - "CpuPowerReader"
Cohesion: 0.09
Nodes (21): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+13 more)

### Community 11 - "UserPresence"
Cohesion: 0.10
Nodes (20): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr, MarshalAs (+12 more)

### Community 12 - "Fact"
Cohesion: 0.13
Nodes (10): Func, IEnumerable, IReadOnlyList, Version, Footprint, RetirementEntry, Trace, Fact (+2 more)

### Community 13 - "WindowsFaults"
Cohesion: 0.11
Nodes (21): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, DateTime, DllImport, Func (+13 more)

### Community 14 - "WindowsFaultsTests"
Cohesion: 0.17
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 15 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 16 - ".Main"
Cohesion: 0.13
Nodes (7): The threat model, and what the code does about it, Application, DllImport, Mutex, App, RegistryKey, STAThread

### Community 17 - "AppSettings"
Cohesion: 0.07
Nodes (28): JsonElement, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoInstallUpdates, CompensateWindowsFaults, CpuWarningTemperatureC, DisableNdu (+20 more)

### Community 18 - "ProcessPresence"
Cohesion: 0.13
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 19 - ".Info"
Cohesion: 0.09
Nodes (12): CancelEventArgs, KeyEventArgs, SizeChangedEventArgs, Point, RoutedEventArgs, Size, TourStep, Body (+4 more)

### Community 20 - "Nextcalibur.Core.Hardware"
Cohesion: 0.16
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Security, Nextcalibur.Core.Hardware, system_security_cryptography_x509certificates, system_xml_linq, xunit

### Community 21 - "Elevation"
Cohesion: 0.13
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 22 - ".RequestRestart"
Cohesion: 0.13
Nodes (10): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, IntPtr, MarshalAs (+2 more)

### Community 23 - "Fact"
Cohesion: 0.16
Nodes (8): IList, HardwareSupport, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 24 - "SystemModeService"
Cohesion: 0.17
Nodes (9): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, SystemModeService (+1 more)

### Community 25 - ".Get"
Cohesion: 0.12
Nodes (4): SolidColorBrush, Func, MessageBoxButton, MessageBoxResult

### Community 26 - ".OnLoaded"
Cohesion: 0.14
Nodes (6): AssemblyInformationalVersionAttribute, Asynchronous dispatcher & UI thread hardening, Task, Action, Toasts, Exception

### Community 27 - "EcMailbox"
Cohesion: 0.16
Nodes (10): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+2 more)

### Community 28 - "LedState"
Cohesion: 0.15
Nodes (15): B, G, R, LedState, ActiveProfile, BrightnessPercent, Current, Effect (+7 more)

### Community 29 - "Fact"
Cohesion: 0.11
Nodes (14): OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact, Task (+6 more)

### Community 30 - "Nextcalibur.App"
Cohesion: 0.09
Nodes (15): Nextcalibur.App, system_componentmodel, system_drawing, system_io, system_io_directory, system_io_ioexception, system_io_path, system_linq (+7 more)

### Community 31 - ".Warn"
Cohesion: 0.13
Nodes (7): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Action, ToggleButton

### Community 32 - "GpuModeService"
Cohesion: 0.16
Nodes (11): ArgumentNullException, GpuLoad, FirmwareModeReading, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma (+3 more)

### Community 33 - "Button"
Cohesion: 0.09
Nodes (23): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, MinimiseButton (+15 more)

### Community 34 - ".Check"
Cohesion: 0.14
Nodes (9): CommonAce, RawSecurityDescriptor, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported (+1 more)

### Community 35 - "MemoryTrimmer"
Cohesion: 0.12
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 36 - "RoutedEventArgs"
Cohesion: 0.12
Nodes (4): Action, Color, RadioButton, RoutedEventArgs

### Community 37 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 38 - ".Migrate"
Cohesion: 0.13
Nodes (8): DateTime, Dictionary, List, PendingRestartInfo, BootTimeUtc, ReasonArguments, Reasons, SettingsTests

### Community 39 - "system_runtime_interopservices"
Cohesion: 0.11
Nodes (5): Nextcalibur.Core.Power, microsoft_win32_safehandles, system_management, system_reflection, system_runtime_interopservices

### Community 40 - "Nextcalibur.Core.csproj"
Cohesion: 0.12
Nodes (13): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Management (10.0.12), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk, net10.0-windows (+5 more)

### Community 41 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 42 - "Dependency"
Cohesion: 0.16
Nodes (14): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+6 more)

### Community 43 - ".CalculateThreadShare"
Cohesion: 0.15
Nodes (9): IReadOnlyDictionary, Stable, Dictionary, Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests (+1 more)

### Community 44 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 45 - "UpdateService"
Cohesion: 0.14
Nodes (13): DispatcherTimer, Func, HashSet, IProgress, Task, TimeSpan, UpdateService, AutomaticChecksEnabled (+5 more)

### Community 46 - "Program"
Cohesion: 0.18
Nodes (5): ConsoleColor, B, G, R, Program

### Community 47 - "PowerOverlayService"
Cohesion: 0.22
Nodes (7): DllImport, Guid, IReadOnlyList, PowerOverlays, All, PowerOverlayService, IdempotenceTests

### Community 49 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 50 - "MainWindow.xaml.cs"
Cohesion: 0.24
Nodes (10): Nextcalibur.App.Controls, system_windows, system_windows_controls, system_windows_controls_button, system_windows_controls_primitives, system_windows_input, system_windows_input_keyeventargs, system_windows_media (+2 more)

### Community 51 - "ToggleButton"
Cohesion: 0.12
Nodes (17): LedPower, OverheatWarningToggle, SelectAll, SettingAutoCheckUpdates, SettingAutoInstallUpdates, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost (+9 more)

### Community 52 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 53 - "system_diagnostics"
Cohesion: 0.19
Nodes (6): Nextcalibur.Cli, system_diagnostics, system_security_accesscontrol, system_security_cryptography, system_security_principal, system_text

### Community 54 - "BacklightKeyWatcher"
Cohesion: 0.15
Nodes (11): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, LedBrightness, Full, Half, Off, Fact (+3 more)

### Community 55 - ".Ask"
Cohesion: 0.35
Nodes (7): HttpStatusCode, Fact, InlineData, Task, Theory, Answer, HostileAnswerTests

### Community 56 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 57 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 58 - "HostileFilesTests"
Cohesion: 0.27
Nodes (5): IOException, Fact, InlineData, Theory, HostileFilesTests

### Community 59 - "BatteryModePolicy"
Cohesion: 0.34
Nodes (5): PowerModeChangedEventArgs, BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

### Community 60 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 61 - "ProcessMetrics"
Cohesion: 0.15
Nodes (14): Share, Dictionary, TimeSpan, FaultEvidence, ProcessMetrics, Name, PageFaultCount, Pid (+6 more)

### Community 62 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 63 - "SystemMode"
Cohesion: 0.26
Nodes (4): SystemMode, Gaming, Office, Performance

### Community 64 - ".IsUnderProgramFiles"
Cohesion: 0.21
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 65 - "Nextcalibur.Core.Tests.Attacks"
Cohesion: 0.30
Nodes (6): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_text_json

### Community 66 - "Nextcalibur.Core.Configuration"
Cohesion: 0.19
Nodes (7): Nextcalibur.Core.Configuration, microsoft_win32, Theme, Dark, Light, system_security, system_text_json_serialization

### Community 67 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 68 - "LedEffect"
Cohesion: 0.14
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 69 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 70 - "SystemTools"
Cohesion: 0.17
Nodes (9): FileStream, SystemTools, Cmd, Explorer, Pnputil, Powercfg, PowerShell, Schtasks (+1 more)

### Community 71 - "PawnIoDependency"
Cohesion: 0.15
Nodes (11): CancellationToken, HttpClient, Task, Version, PawnIoDependency, ExpectedSigner, Id, Name (+3 more)

### Community 72 - ".HasAccess"
Cohesion: 0.28
Nodes (5): SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 73 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 74 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 75 - "Nextcalibur 0.5.4"
Cohesion: 0.17
Nodes (11): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+3 more)

### Community 76 - "CoreLoad"
Cohesion: 0.18
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 77 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 78 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 79 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 80 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 81 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 82 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 83 - "ThemeService"
Cohesion: 0.18
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 84 - ".Sanitised"
Cohesion: 0.29
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 85 - "RpmToDoubleConverter"
Cohesion: 0.36
Nodes (6): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, Type

### Community 86 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 87 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 88 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 89 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 90 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 91 - "README.md"
Cohesion: 0.33
Nodes (3): How it works, Questions people ask, Using it

### Community 92 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 93 - "ThermalSample"
Cohesion: 0.33
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 95 - "SupportVerdict"
Cohesion: 0.25
Nodes (8): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites

### Community 97 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 98 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (4): Nextcalibur.Core, system_globalization, system_text_regularexpressions, system_windows_data

### Community 99 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

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

### Community 113 - ".PowerModeControls"
Cohesion: 0.33
Nodes (5): Option, Button, IEnumerable, TextBlock, PowerModeOption

### Community 114 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 115 - "StackPanel"
Cohesion: 0.33
Nodes (6): BannerActions, DialogActions, DriveTextStack, PanelWindowsFaultsSubOptions, ProfileRow, StackPanel

### Community 116 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 117 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 118 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 119 - ".LatestAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, HttpClient, HttpRequestMessage, HttpResponseMessage

### Community 120 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 122 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 124 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 125 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 126 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 127 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 128 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 130 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 133 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 134 - "Words"
Cohesion: 0.67
Nodes (3): Func, Words, Resolver

## Knowledge Gaps
- **396 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+391 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 748 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **33 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `.CheckProgram`, `GpuClockReader`, `TrayPresence`, `RadioButton`, `Window`, `CpuPowerReader`, `UserPresence`, `WindowsFaults`, `AppSettings`, `.Info`, `.RequestRestart`, `SystemModeService`, `.Get`, `.OnLoaded`, `EcMailbox`, `Nextcalibur.App`, `.Warn`, `GpuModeService`, `RoutedEventArgs`, `.Read`, `DependencyStatus`, `UpdateService`, `PowerOverlayService`, `.StartSlowTimer`, `MainWindow.xaml.cs`, `BacklightKeyWatcher`, `LedController`, `BatteryModePolicy`, `SystemMode`, `LedEffect`, `Strings`, `Grid`, `ThemeService`, `NvidiaDriverState`, `ThermalSample`, `SupportVerdict`, `.OnThresholdMoved`, `TourPage`, `.PowerModeControls`, `.OnBrightnessChanged`, `LedZone`?**
  _High betweenness centrality (0.445) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `CpuWarnValue`, `MainWindow`, `RadioButton`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `Button`, `ToggleButton`, `Grid`, `Border`, `Text`, `.OnBrightnessChanged`, `StackPanel`, `Keycaps`, `DriveGauge`?**
  _High betweenness centrality (0.157) - this node is a cross-community bridge._
- **Why does `DependencyStatus` connect `DependencyStatus` to `Nextcalibur.Core.Tests.Attacks`, `.OnLoaded`, `Dependency`, `MainWindow`?**
  _High betweenness centrality (0.098) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _396 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DotNetRuntimeDependency` be split into smaller, more focused modules?**
  _Cohesion score 0.05348101265822785 - nodes in this community are weakly interconnected._
- **Should `.CheckProgram` be split into smaller, more focused modules?**
  _Cohesion score 0.061955965181771634 - nodes in this community are weakly interconnected._