# Graph Report - nextcalibur-control-center  (2026-10-11)

## Corpus Check
- 167 files · ~218,624 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2797 nodes · 6027 edges · 175 communities (138 shown, 37 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 318 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `26fb7e9a`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- DotNetRuntimeDependency
- Authenticode
- ProtectedStore
- GpuClockReader
- RunawayWatchTests
- .CheckProgram
- AppSettings
- WindowsFaults
- IShellLinkW
- FnShortcut
- Window
- .Get
- Nextcalibur.App
- .ApplyFnShortcuts
- RadioButton
- PlanTuning
- .WireSettingsPage
- UserPresence
- .Info
- WindowsFaultsTests
- SystemInfo
- Nextcalibur.Core.Hardware
- .RequestRestart
- SystemMode
- ProcessPresence
- CoolingPolicyTests
- CpuPowerReader
- TrayPresence
- Fact
- Button
- LedState
- MachineScanRecord
- Fact
- EcMailbox
- .Survey
- .Get
- .PlaceTourStep
- .Read
- system_runtime_interopservices
- Nextcalibur.Core.csproj
- .Check
- RestorePoint
- .Main
- MemoryTrimmer
- DependencyStatus
- Fact
- analyse-autopsy.py
- Elevation
- BacklightKeyWatcher
- UpdateService
- Nextcalibur.Core.Power
- ToggleButton
- ResourceDictionary
- SmiCommand
- system_text_json
- ColourWheel
- .Ask
- PowerOverlayService
- LedController
- Program
- FanGauge
- Nextcalibur.Core.Configuration
- DevicePowerState
- HostileFilesTests
- Unelevated
- DonutGauge
- ReleaseFile
- SegmentedBar
- LedEffect
- InstallFolderGuard
- .HasAccess
- LogInjectionTests
- Nextcalibur.Core.Security
- SystemTools
- .OnPowerModeChanged
- CoreLoad
- Footprint
- .RootOf
- BatteryModePolicy
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur Control Center
- Strings
- Grid
- Dependency
- Hardware Protocol
- Nextcalibur 0.5.3
- .RemoveMachineTraces
- ThemeService
- .OnSystemModeChanged
- Border
- .Sanitised
- NvidiaDriverState
- SupportVerdict
- .CalculateThreadShare
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- ThermalSample
- PawnIoDependency
- ProfileFiles
- Probe
- Nextcalibur 0.5.1
- Nextcalibur 0.5.5
- StackPanel
- Log
- NduFix
- NotificationState
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- .Pick
- TourPage
- Log-Graphics.ps1
- Privacy
- Security
- .OnBrightnessChanged
- .ForegroundProcess
- LedZone
- PowerSource
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
- Nextcalibur 0.6.1
- .ArrangeOverride
- .TryParseRgb
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

## Communities (175 total, 37 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.04
Nodes (45): CheckBox, DeferredOverheat, HwndSource, ObservableCollection, Queue, Slider, MainWindow, DateTime (+37 more)

### Community 1 - "DotNetRuntimeDependency"
Cohesion: 0.05
Nodes (46): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+38 more)

### Community 2 - "Authenticode"
Cohesion: 0.06
Nodes (38): CatalogInfo, Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on (+30 more)

### Community 3 - "ProtectedStore"
Cohesion: 0.07
Nodes (26): Content, DirectorySecurity, Hash, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Dictionary (+18 more)

### Community 4 - "GpuClockReader"
Cohesion: 0.08
Nodes (23): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+15 more)

### Community 5 - "RunawayWatchTests"
Cohesion: 0.06
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 6 - ".CheckProgram"
Cohesion: 0.07
Nodes (23): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvmlPath, IntegrityFinding (+15 more)

### Community 7 - "AppSettings"
Cohesion: 0.05
Nodes (41): DateTime, Dictionary, FnShortcut, JsonElement, List, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates (+33 more)

### Community 8 - "WindowsFaults"
Cohesion: 0.08
Nodes (34): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+26 more)

### Community 9 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 10 - "FnShortcut"
Cohesion: 0.08
Nodes (25): DllImport, IEnumerable, IntPtr, List, StringBuilder, FnAction, FanTurbo, KeyboardBacklight (+17 more)

### Community 11 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 12 - ".Get"
Cohesion: 0.09
Nodes (6): SolidColorBrush, Func, MessageBoxButton, MessageBoxResult, Action, Toasts

### Community 13 - "Nextcalibur.App"
Cohesion: 0.09
Nodes (23): Nextcalibur.App, Nextcalibur.App.Controls, system_componentmodel, system_drawing, system_io, system_linq, system_runtime_compilerservices, system_windows (+15 more)

### Community 14 - ".ApplyFnShortcuts"
Cohesion: 0.07
Nodes (19): INotifyPropertyChanged, Problem, DriveRow, Detail, Name, Percent, PercentText, Dictionary (+11 more)

### Community 15 - "RadioButton"
Cohesion: 0.07
Nodes (36): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeDiscrete, ModeGaming (+28 more)

### Community 16 - "PlanTuning"
Cohesion: 0.14
Nodes (16): AcDc, Floor, Original, Plan, Action, DllImport, Guid, IntPtr (+8 more)

### Community 17 - ".WireSettingsPage"
Cohesion: 0.09
Nodes (11): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary, IReadOnlyDictionary, Task (+3 more)

### Community 18 - "UserPresence"
Cohesion: 0.12
Nodes (14): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, Point, Size, DateTime, DllImport (+6 more)

### Community 19 - ".Info"
Cohesion: 0.11
Nodes (5): AssemblyInformationalVersionAttribute, CancelEventArgs, Asynchronous dispatcher & UI thread hardening, Task, Exception

### Community 20 - "WindowsFaultsTests"
Cohesion: 0.16
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 21 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 22 - "Nextcalibur.Core.Hardware"
Cohesion: 0.15
Nodes (5): Nextcalibur.Core.Tests, Nextcalibur.Core.Hardware, system_text_regularexpressions, system_xml_linq, xunit

### Community 23 - ".RequestRestart"
Cohesion: 0.11
Nodes (10): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr, MarshalAs (+2 more)

### Community 24 - "SystemMode"
Cohesion: 0.14
Nodes (13): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, SystemMode (+5 more)

### Community 25 - "ProcessPresence"
Cohesion: 0.13
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 26 - "CoolingPolicyTests"
Cohesion: 0.12
Nodes (16): At, Margin, DateTime, List, TimeSpan, CoolingPolicy, DegreesPerMinute, Engaged (+8 more)

### Community 27 - "CpuPowerReader"
Cohesion: 0.16
Nodes (9): DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled, Fact, InlineData, Theory (+1 more)

### Community 28 - "TrayPresence"
Cohesion: 0.11
Nodes (11): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, EventArgs, List, Ms (+3 more)

### Community 29 - "Fact"
Cohesion: 0.16
Nodes (8): IList, HardwareSupport, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 30 - "Button"
Cohesion: 0.08
Nodes (24): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, HideToTrayButton (+16 more)

### Community 31 - "LedState"
Cohesion: 0.15
Nodes (15): B, G, R, LedState, ActiveProfile, BrightnessPercent, Current, Effect (+7 more)

### Community 32 - "MachineScanRecord"
Cohesion: 0.11
Nodes (18): DateTime, JsonSerializerOptions, List, MachineScan, RecordPath, MachineScanRecord, BiosVendor, BiosVersion (+10 more)

### Community 33 - "Fact"
Cohesion: 0.10
Nodes (15): IReadOnlyList, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact (+7 more)

### Community 34 - "EcMailbox"
Cohesion: 0.14
Nodes (13): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+5 more)

### Community 35 - ".Survey"
Cohesion: 0.17
Nodes (7): Func, IEnumerable, IReadOnlyList, RetirementEntry, Trace, Version, FootprintTests

### Community 36 - ".Get"
Cohesion: 0.16
Nodes (12): ArgumentNullException, FirmwareModeReading, GpuMode, Discrete, Hybrid, Uma, GpuModeService, SwitchOutcome (+4 more)

### Community 37 - ".PlaceTourStep"
Cohesion: 0.11
Nodes (10): SizeChangedEventArgs, FrameworkElement, KeyEventArgs, RoutedEventArgs, TourStep, Body, SectionName, Title (+2 more)

### Community 38 - ".Read"
Cohesion: 0.12
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 39 - "system_runtime_interopservices"
Cohesion: 0.10
Nodes (7): Nextcalibur.Core.Input, system_io_directory, system_io_ioexception, system_io_path, system_management, system_runtime_interopservices, system_runtime_interopservices_comtypes

### Community 40 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.12), System.Management (10.0.12), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk (+6 more)

### Community 41 - ".Check"
Cohesion: 0.17
Nodes (8): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported, IdempotenceTests

### Community 42 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 43 - ".Main"
Cohesion: 0.19
Nodes (6): The threat model, and what the code does about it, Application, DllImport, Mutex, App, STAThread

### Community 44 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 45 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 46 - "Fact"
Cohesion: 0.16
Nodes (5): Fact, ForwardCompatibilityTests, MailboxAccessTests, SettingsTests, StartupPreferenceTests

### Community 47 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 48 - "Elevation"
Cohesion: 0.20
Nodes (4): dynamic, Elevation, Fact, TaskFolderTests

### Community 49 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 50 - "UpdateService"
Cohesion: 0.14
Nodes (13): DateTime, DispatcherTimer, HashSet, IProgress, Task, TimeSpan, UpdateService, Available (+5 more)

### Community 51 - "Nextcalibur.Core.Power"
Cohesion: 0.13
Nodes (9): Nextcalibur.Cli, Nextcalibur.Core.Power, microsoft_win32, microsoft_win32_safehandles, Theme, Dark, Light, system_reflection (+1 more)

### Community 52 - "ToggleButton"
Cohesion: 0.11
Nodes (18): LedPower, OverheatWarningToggle, SelectAll, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost (+10 more)

### Community 53 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 54 - "SmiCommand"
Cohesion: 0.21
Nodes (6): ArgumentException, SmiCommand, Fact, InlineData, Theory, SmiCommandTests

### Community 55 - "system_text_json"
Cohesion: 0.22
Nodes (6): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_text_json

### Community 56 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 57 - ".Ask"
Cohesion: 0.35
Nodes (7): HttpStatusCode, Fact, InlineData, Task, Theory, Answer, HostileAnswerTests

### Community 58 - "PowerOverlayService"
Cohesion: 0.25
Nodes (6): DllImport, Guid, PowerModeOption, PowerOverlays, All, PowerOverlayService

### Community 59 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 61 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 62 - "Nextcalibur.Core.Configuration"
Cohesion: 0.18
Nodes (4): Nextcalibur.Core.Configuration, system_diagnostics, system_text, system_text_json_serialization

### Community 63 - "DevicePowerState"
Cohesion: 0.28
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 64 - "HostileFilesTests"
Cohesion: 0.27
Nodes (5): IOException, Fact, InlineData, Theory, HostileFilesTests

### Community 65 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 66 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 67 - "ReleaseFile"
Cohesion: 0.22
Nodes (9): CancellationToken, HttpClient, Task, Uri, ReleaseFile, CancellationToken, HttpClient, Task (+1 more)

### Community 68 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 69 - "LedEffect"
Cohesion: 0.14
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 70 - "InstallFolderGuard"
Cohesion: 0.30
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 71 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 72 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 73 - "Nextcalibur.Core.Security"
Cohesion: 0.27
Nodes (5): Nextcalibur.Core.Security, system_security_accesscontrol, system_security_cryptography, system_security_cryptography_x509certificates, system_security_principal

### Community 74 - "SystemTools"
Cohesion: 0.17
Nodes (9): FileStream, SystemTools, Cmd, Explorer, Pnputil, Powercfg, PowerShell, Schtasks (+1 more)

### Community 75 - ".OnPowerModeChanged"
Cohesion: 0.18
Nodes (8): Option, PowerModeBalanced, PowerModeBest, PowerModeBetter, PowerModeEfficiency, Button, IEnumerable, TextBlock

### Community 76 - "CoreLoad"
Cohesion: 0.17
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 78 - ".RootOf"
Cohesion: 0.24
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 79 - "BatteryModePolicy"
Cohesion: 0.42
Nodes (4): BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

### Community 80 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 81 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 82 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 83 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 84 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 85 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 86 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 87 - "Dependency"
Cohesion: 0.17
Nodes (9): Version, Dependency, ExpectedSigner, Id, MayBeRemoved, Name, Purpose, SilentInstallArguments (+1 more)

### Community 88 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 89 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 91 - "ThemeService"
Cohesion: 0.18
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 93 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 94 - ".Sanitised"
Cohesion: 0.33
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 95 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 96 - "SupportVerdict"
Cohesion: 0.22
Nodes (9): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites (+1 more)

### Community 97 - ".CalculateThreadShare"
Cohesion: 0.27
Nodes (4): IReadOnlyDictionary, Stable, Dictionary, TopSharePercent

### Community 98 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 99 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 100 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 101 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 102 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 103 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 104 - "ThermalSample"
Cohesion: 0.33
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 105 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 107 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 108 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 109 - "Nextcalibur 0.5.5"
Cohesion: 0.25
Nodes (6): Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture

### Community 110 - "StackPanel"
Cohesion: 0.25
Nodes (8): BannerActions, DialogActions, DriveTextStack, FnShortcutRows, PanelWindowsFaultsSubOptions, ProfileRow, SettingUpdatesAutomatic, StackPanel

### Community 114 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 115 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 116 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 117 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 118 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 119 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 120 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 121 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 122 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 123 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 124 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 125 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 126 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 127 - ".ForegroundProcess"
Cohesion: 0.47
Nodes (3): DllImport, IntPtr, Name

### Community 128 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 129 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 130 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 131 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 133 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 135 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 136 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 137 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 139 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 140 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 141 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 142 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 144 - ".TryParseRgb"
Cohesion: 0.50
Nodes (3): B, G, R

### Community 145 - ".SendAsync"
Cohesion: 0.50
Nodes (3): CancellationToken, HttpRequestMessage, HttpResponseMessage

### Community 146 - ".Modules_are_the_released_ones"
Cohesion: 0.50
Nodes (3): InlineData, Theory, PawnIoModuleTests

### Community 147 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 150 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 151 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

## Knowledge Gaps
- **461 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+456 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 870 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **37 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `LedZone`, `GpuClockReader`, `RunawayWatchTests`, `.CheckProgram`, `AppSettings`, `WindowsFaults`, `FnShortcut`, `Window`, `.Get`, `Nextcalibur.App`, `.ApplyFnShortcuts`, `RadioButton`, `PlanTuning`, `.WireSettingsPage`, `UserPresence`, `.Info`, `.RequestRestart`, `SystemMode`, `CoolingPolicyTests`, `CpuPowerReader`, `TrayPresence`, `Button`, `EcMailbox`, `.Get`, `.PlaceTourStep`, `.Read`, `DependencyStatus`, `BacklightKeyWatcher`, `UpdateService`, `PowerOverlayService`, `LedController`, `LedEffect`, `.OnPowerModeChanged`, `BatteryModePolicy`, `Strings`, `Grid`, `ThemeService`, `.OnSystemModeChanged`, `NvidiaDriverState`, `SupportVerdict`, `ThermalSample`, `Nextcalibur 0.5.5`, `.OnGpuModeChanged`, `TourPage`, `.OnBrightnessChanged`, `.ForegroundProcess`?**
  _High betweenness centrality (0.454) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Keycaps`, `DriveGauge`, `RadioButton`, `CpuWarnValue`, `DialogCheck`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `Button`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `ToggleButton`, `.OnPowerModeChanged`, `Grid`, `Border`, `StackPanel`, `Text`, `.OnBrightnessChanged`?**
  _High betweenness centrality (0.112) - this node is a cross-community bridge._
- **Why does `AppSettings` connect `AppSettings` to `MainWindow`, `HostileFilesTests`, `ProtectedStore`, `Footprint`, `Fact`, `NduFix`, `.Info`, `WindowsFaultsTests`, `Strings`, `SystemMode`, `.Sanitised`, `ThemeService`, `TrayPresence`, `Nextcalibur.Core.Configuration`?**
  _High betweenness centrality (0.075) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _461 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.03702615339406406 - nodes in this community are weakly interconnected._
- **Should `DotNetRuntimeDependency` be split into smaller, more focused modules?**
  _Cohesion score 0.054527750730282376 - nodes in this community are weakly interconnected._