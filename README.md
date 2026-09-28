# Graph Report - nextcalibur-control-center  (2026-09-28)

## Corpus Check
- 146 files · ~200,000 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2438 nodes · 5149 edges · 159 communities (122 shown, 37 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 258 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `12132d72`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Authenticode
- MainWindow
- UserPresence
- ProtectedStore
- DependencyStatus
- GpuClockReader
- .CheckProgram
- TrayPresence
- AppSettings
- Window
- IShellLinkW
- RadioButton
- CpuPowerReader
- .Get
- .RequestRestart
- WindowsFaults
- xunit
- SystemInfo
- WindowsFaultsTests
- NetworkCostTests
- SystemMode
- .OnLoaded
- ProcessPresence
- Nextcalibur.App
- .OnClosing
- LedState
- Dependency
- EcMailbox
- Fact
- .Survey
- Fact
- .Get
- .StartSlowTimer
- Button
- Program
- .Ask
- DotNetRuntimeDependency
- .Main
- .Read
- .Check
- Nextcalibur.Core.Hardware
- Nextcalibur.Core.csproj
- .CalculateThreadShare
- analyse-autopsy.py
- Fact
- .OnSystemModeChanged
- PowerOverlayService
- ResourceDictionary
- RuntimeDependencyTests
- ColourWheel
- system_diagnostics
- Nextcalibur.Core.Configuration
- Elevation
- BacklightKeyWatcher
- CoreLoad
- UpdateService
- LedController
- .MayStartWithoutPrompt
- FanGauge
- Unelevated
- ProcessMetrics
- DonutGauge
- ToggleButton
- SegmentedBar
- LedEffect
- InstallFolderGuard
- .HasAccess
- LogInjectionTests
- system_text_json
- .WireSettingsPage
- BatteryModePolicy
- StringsDictionaryTests
- ValueConverters.cs
- Nextcalibur 0.5.2
- Nextcalibur Control Center
- Grid
- SmiCommandTests
- Hardware Protocol
- Nextcalibur 0.5.3
- DriveRow
- Strings
- ThemeService
- .RemoveMachineTraces
- .Sanitised
- SupportVerdict
- CardSwitchTasks
- Border
- NvidiaDriverState
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- Log
- PawnIoDependency
- NduFix
- Probe
- .GetAsync
- Nextcalibur 0.5.1
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Releasing, and signing
- .Pick
- .OnThresholdMoved
- StackPanel
- TourPage
- .TryShow
- BatteryModePolicy.cs
- Log-Graphics.ps1
- Privacy
- Security
- .OnBrightnessChanged
- LedZone
- MachineWideGate
- Notice
- Nextcalibur 0.5.11
- AppIdentity.cs
- GpuConfiguration
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
1. `MainWindow` - 227 edges
2. `Window` - 163 edges
3. `AppSettings` - 63 edges
4. `Nextcalibur.Core.Hardware` - 42 edges
5. `WindowsFaultsTests` - 40 edges
6. `RadioButton` - 39 edges
7. `TextBlock` - 36 edges
8. `WindowsFaults` - 33 edges
9. `GpuClockReader` - 31 edges
10. `Nextcalibur.Core.Configuration` - 30 edges

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

## Communities (159 total, 37 thin omitted)

### Community 0 - "Authenticode"
Cohesion: 0.06
Nodes (38): CatalogInfo, Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on (+30 more)

### Community 1 - "MainWindow"
Cohesion: 0.05
Nodes (37): DeferredOverheat, ObservableCollection, Queue, HideToTrayButton, DateTime, HashSet, List, TimeSpan (+29 more)

### Community 2 - "UserPresence"
Cohesion: 0.06
Nodes (33): MEMORYSTATUSEX, MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, Dictionary, DllImport (+25 more)

### Community 3 - "ProtectedStore"
Cohesion: 0.09
Nodes (20): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+12 more)

### Community 4 - "DependencyStatus"
Cohesion: 0.06
Nodes (29): FileStream, IOException, Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress (+21 more)

### Community 5 - "GpuClockReader"
Cohesion: 0.09
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 6 - ".CheckProgram"
Cohesion: 0.07
Nodes (23): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvmlPath, IntegrityFinding (+15 more)

### Community 7 - "TrayPresence"
Cohesion: 0.06
Nodes (21): ContextMenuStrip, DevPropKey, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, EventHandler (+13 more)

### Community 8 - "AppSettings"
Cohesion: 0.06
Nodes (37): DateTime, Dictionary, JsonElement, List, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoInstallUpdates (+29 more)

### Community 9 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 10 - "IShellLinkW"
Cohesion: 0.07
Nodes (13): PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder, AppIdentity (+5 more)

### Community 11 - "RadioButton"
Cohesion: 0.06
Nodes (40): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeDiscrete, ModeGaming (+32 more)

### Community 12 - "CpuPowerReader"
Cohesion: 0.09
Nodes (21): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+13 more)

### Community 13 - ".Get"
Cohesion: 0.10
Nodes (8): SolidColorBrush, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, MessageBoxButton, MessageBoxResult

### Community 14 - ".RequestRestart"
Cohesion: 0.10
Nodes (11): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr (+3 more)

### Community 15 - "WindowsFaults"
Cohesion: 0.11
Nodes (21): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, DateTime, DllImport, Func (+13 more)

### Community 16 - "xunit"
Cohesion: 0.13
Nodes (8): Nextcalibur.Core.Tests, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, Nextcalibur.Core.Power, system_security_cryptography_x509certificates, system_text_regularexpressions, system_xml_linq, xunit

### Community 17 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 18 - "WindowsFaultsTests"
Cohesion: 0.18
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 19 - "NetworkCostTests"
Cohesion: 0.14
Nodes (19): Bytes, Count, HttpMessageHandler, CancellationToken, Dictionary, Fact, HttpRequestMessage, HttpResponseMessage (+11 more)

### Community 20 - "SystemMode"
Cohesion: 0.15
Nodes (13): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, SystemMode (+5 more)

### Community 21 - ".OnLoaded"
Cohesion: 0.13
Nodes (5): AssemblyInformationalVersionAttribute, Asynchronous dispatcher & UI thread hardening, Task, IProgress, Exception

### Community 22 - "ProcessPresence"
Cohesion: 0.13
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 23 - "Nextcalibur.App"
Cohesion: 0.13
Nodes (17): Nextcalibur.App, Nextcalibur.App.Controls, system_drawing, system_io, system_linq, system_windows, system_windows_controls, system_windows_controls_button (+9 more)

### Community 24 - ".OnClosing"
Cohesion: 0.09
Nodes (11): CancelEventArgs, KeyEventArgs, SizeChangedEventArgs, Point, RoutedEventArgs, Size, TourStep, Body (+3 more)

### Community 25 - "LedState"
Cohesion: 0.15
Nodes (15): B, G, R, LedState, ActiveProfile, BrightnessPercent, Current, Effect (+7 more)

### Community 26 - "Dependency"
Cohesion: 0.11
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 27 - "EcMailbox"
Cohesion: 0.16
Nodes (10): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+2 more)

### Community 28 - "Fact"
Cohesion: 0.18
Nodes (7): IList, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 29 - ".Survey"
Cohesion: 0.15
Nodes (7): Func, IEnumerable, IReadOnlyList, Version, RetirementEntry, Trace, FootprintTests

### Community 30 - "Fact"
Cohesion: 0.11
Nodes (14): OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact, Task (+6 more)

### Community 31 - ".Get"
Cohesion: 0.16
Nodes (12): ArgumentNullException, FirmwareModeReading, GpuMode, Discrete, Hybrid, Uma, GpuModeService, SwitchOutcome (+4 more)

### Community 33 - "Button"
Cohesion: 0.09
Nodes (23): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, MinimiseButton (+15 more)

### Community 34 - "Program"
Cohesion: 0.17
Nodes (5): ConsoleColor, Program, DateTimeOffset, ThermalReader, ThermalSample

### Community 35 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 36 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 37 - ".Main"
Cohesion: 0.20
Nodes (6): The threat model, and what the code does about it, Application, DllImport, Mutex, App, STAThread

### Community 38 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 39 - ".Check"
Cohesion: 0.17
Nodes (8): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported, IdempotenceTests

### Community 40 - "Nextcalibur.Core.Hardware"
Cohesion: 0.17
Nodes (5): Nextcalibur.Core.Hardware, microsoft_win32_safehandles, system_management, system_reflection, system_runtime_interopservices

### Community 41 - "Nextcalibur.Core.csproj"
Cohesion: 0.12
Nodes (13): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Management (10.0.12), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk, net10.0-windows (+5 more)

### Community 42 - ".CalculateThreadShare"
Cohesion: 0.15
Nodes (9): IReadOnlyDictionary, Stable, Dictionary, Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests (+1 more)

### Community 43 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 44 - "Fact"
Cohesion: 0.16
Nodes (5): Fact, ForwardCompatibilityTests, MailboxAccessTests, SettingsTests, StartupPreferenceTests

### Community 45 - ".OnSystemModeChanged"
Cohesion: 0.20
Nodes (4): Option, PowerModeChangedEventArgs, Button, IEnumerable

### Community 46 - "PowerOverlayService"
Cohesion: 0.23
Nodes (7): DllImport, Guid, IReadOnlyList, PowerModeOption, PowerOverlays, All, PowerOverlayService

### Community 47 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 48 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 49 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 50 - "system_diagnostics"
Cohesion: 0.19
Nodes (6): Nextcalibur.Cli, system_diagnostics, system_security_accesscontrol, system_security_cryptography, system_security_principal, system_text

### Community 51 - "Nextcalibur.Core.Configuration"
Cohesion: 0.16
Nodes (7): Nextcalibur.Core.Configuration, microsoft_win32, Theme, Dark, Light, system_security, system_text_json_serialization

### Community 52 - "Elevation"
Cohesion: 0.23
Nodes (4): dynamic, Elevation, Fact, TaskFolderTests

### Community 53 - "BacklightKeyWatcher"
Cohesion: 0.15
Nodes (11): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, LedBrightness, Full, Half, Off, Fact (+3 more)

### Community 54 - "CoreLoad"
Cohesion: 0.13
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 55 - "UpdateService"
Cohesion: 0.17
Nodes (12): DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService, Available, AvailableVersion (+4 more)

### Community 56 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 57 - ".MayStartWithoutPrompt"
Cohesion: 0.23
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 58 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 59 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 60 - "ProcessMetrics"
Cohesion: 0.15
Nodes (14): Share, Dictionary, TimeSpan, FaultEvidence, ProcessMetrics, Name, PageFaultCount, Pid (+6 more)

### Community 61 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 62 - "ToggleButton"
Cohesion: 0.13
Nodes (15): LedPower, OverheatWarningToggle, SelectAll, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost, SettingFixWidgets, SettingOfficeOnBattery (+7 more)

### Community 63 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 64 - "LedEffect"
Cohesion: 0.14
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 65 - "InstallFolderGuard"
Cohesion: 0.30
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 66 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 67 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 68 - "system_text_json"
Cohesion: 0.31
Nodes (5): Nextcalibur.Core.Dependencies, system_collections_concurrent, system_net, system_net_http, system_text_json

### Community 70 - "BatteryModePolicy"
Cohesion: 0.42
Nodes (4): BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

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

### Community 75 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 76 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 77 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 78 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 79 - "DriveRow"
Cohesion: 0.18
Nodes (8): INotifyPropertyChanged, DriveRow, Detail, Name, Percent, PercentText, system_componentmodel, system_runtime_compilerservices

### Community 80 - "Strings"
Cohesion: 0.29
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 81 - "ThemeService"
Cohesion: 0.18
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 83 - ".Sanitised"
Cohesion: 0.29
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 84 - "SupportVerdict"
Cohesion: 0.20
Nodes (10): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+2 more)

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

### Community 94 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 96 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 97 - ".GetAsync"
Cohesion: 0.29
Nodes (7): Answer, ConcurrentDictionary, CancellationToken, HttpClient, Task, Answer, Conditional

### Community 98 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 99 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 100 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 101 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 102 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 103 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 104 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 106 - "StackPanel"
Cohesion: 0.29
Nodes (7): BannerActions, DialogActions, DriveTextStack, PanelWindowsFaultsSubOptions, ProfileRow, SettingUpdatesAutomatic, StackPanel

### Community 107 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 108 - ".TryShow"
Cohesion: 0.33
Nodes (4): Action, Toasts, windows_data_xml_dom, windows_ui_notifications

### Community 109 - "BatteryModePolicy.cs"
Cohesion: 0.33
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

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

### Community 114 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 115 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 116 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 117 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 118 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

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

### Community 127 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 128 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 129 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 131 - ".TryParseRgb"
Cohesion: 0.50
Nodes (3): B, G, R

### Community 132 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 135 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

## Knowledge Gaps
- **403 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+398 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 757 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **37 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `UserPresence`, `DependencyStatus`, `GpuClockReader`, `.CheckProgram`, `TrayPresence`, `AppSettings`, `Window`, `RadioButton`, `CpuPowerReader`, `.Get`, `.RequestRestart`, `WindowsFaults`, `SystemMode`, `.OnLoaded`, `Nextcalibur.App`, `.OnClosing`, `EcMailbox`, `.Get`, `.StartSlowTimer`, `Program`, `.OnSystemModeChanged`, `PowerOverlayService`, `BacklightKeyWatcher`, `UpdateService`, `LedController`, `LedEffect`, `.WireSettingsPage`, `BatteryModePolicy`, `Grid`, `ThemeService`, `SupportVerdict`, `NvidiaDriverState`, `.OnThresholdMoved`, `TourPage`, `.OnBrightnessChanged`, `LedZone`, `GpuConfiguration`?**
  _High betweenness centrality (0.452) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `CpuWarnValue`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `RadioButton`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `Button`, `ToggleButton`, `Grid`, `Border`, `Text`, `StackPanel`, `.OnBrightnessChanged`, `Keycaps`, `DriveGauge`?**
  _High betweenness centrality (0.157) - this node is a cross-community bridge._
- **Why does `DependencyStatus` connect `DependencyStatus` to `MainWindow`, `Dependency`, `system_text_json`, `.OnLoaded`?**
  _High betweenness centrality (0.086) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _403 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Authenticode` be split into smaller, more focused modules?**
  _Cohesion score 0.062111801242236024 - nodes in this community are weakly interconnected._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.048484848484848485 - nodes in this community are weakly interconnected._