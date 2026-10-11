# Graph Report - nextcalibur-control-center  (2026-10-11)

## Corpus Check
- 165 files · ~216,308 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2777 nodes · 5985 edges · 168 communities (135 shown, 33 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 315 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `729e5e4c`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- DotNetRuntimeDependency
- Authenticode
- MainWindow
- GpuClockReader
- RunawayWatchTests
- .CheckProgram
- CpuPowerReader
- WindowsFaults
- FnShortcut
- Window
- IShellLinkW
- RadioButton
- WindowsFaultsTests
- PlanTuning
- TrayPresence
- SystemInfo
- .Info
- .Get
- AppSettings
- xunit
- .WireSettingsPage
- .Warn
- UserPresence
- Nextcalibur.Core.Hardware
- .ApplyFnShortcuts
- Fact
- Button
- .RequestRestart
- ProcessPresence
- Elevation
- CoolingPolicyTests
- SystemMode
- UpdateService
- Fact
- SystemModeService
- .PlaceTourStep
- EcMailbox
- Fact
- LedState
- MachineScanRecord
- .Check
- PowerOverlayService
- InstallFolderGuard
- Nextcalibur.App
- Nextcalibur.Core.Configuration
- ProtectedStore
- .Read
- .Migrate
- Nextcalibur.Core.csproj
- ToggleButton
- MainWindow.FnKeys.cs
- BacklightKeyWatcher
- MemoryTrimmer
- DependencyStatus
- analyse-autopsy.py
- ValueConverters.cs
- ResourceDictionary
- SmiCommand
- RestorePoint
- ColourWheel
- .Ask
- LedController
- ProtectedStoreTests
- GpuModeService
- FanGauge
- HostileFilesTests
- DonutGauge
- .Record
- SettingsTests.cs
- ReleaseFile
- Program
- SegmentedBar
- LedEffect
- .HasAccess
- LogInjectionTests
- SystemTools
- CoreLoad
- StringsDictionaryTests
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur Control Center
- Strings
- Grid
- Dependency
- Nextcalibur.Core.Dependencies
- Hardware Protocol
- Nextcalibur 0.5.3
- .Sanitised
- Border
- GpuMode
- ThermalSample
- ThemeService
- NvidiaDriverState
- SupportVerdict
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- Log
- PawnIoDependency
- ProfileFiles
- Probe
- .Secure
- Nextcalibur 0.5.1
- StackPanel
- NduFix
- NotificationState
- ProtectedStoreElevatedTests
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Releasing, and signing
- .Pick
- TourPage
- Log-Graphics.ps1
- Privacy
- Security
- .OnBrightnessChanged
- .ForegroundProcess
- .Sample
- LedZone
- PowerSource
- Notice
- Nextcalibur 0.5.11
- .Set
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- .ArrangeOverride
- .TryParseRgb
- .SendAsync
- Tools
- 0.5.7.md
- CpuWarnValue
- DialogCheck
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
- `Zero-bottleneck gaming & heavy workload architecture` --references--> `MemoryTrimmer`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.Core/Hardware/MemoryTrimmer.cs
- `Zero-bottleneck gaming & heavy workload architecture` --references--> `WindowsFaults`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.Core/Hardware/WindowsFaults.cs
- `Fix GPU Switch Restart Prompt & Windows Privilege Adjustment` --references--> `TokenPrivileges`  [INFERRED]
  docs/releases/0.5.8.md → src/Nextcalibur.App/MainWindow.xaml.cs
- `Deep memory & resource leak eradication` --references--> `CpuPowerReader`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.Core/Hardware/CpuPowerReader.cs

## Import Cycles
- None detected.

## Communities (168 total, 33 thin omitted)

### Community 0 - "DotNetRuntimeDependency"
Cohesion: 0.05
Nodes (46): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+38 more)

### Community 1 - "Authenticode"
Cohesion: 0.06
Nodes (38): CatalogInfo, Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on (+30 more)

### Community 2 - "MainWindow"
Cohesion: 0.04
Nodes (44): CheckBox, DeferredOverheat, HwndSource, ObservableCollection, Queue, Slider, MainWindow, DateTime (+36 more)

### Community 3 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 4 - "RunawayWatchTests"
Cohesion: 0.06
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 5 - ".CheckProgram"
Cohesion: 0.07
Nodes (23): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvmlPath, IntegrityFinding (+15 more)

### Community 6 - "CpuPowerReader"
Cohesion: 0.08
Nodes (19): DevPropKey, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, DllImport, IntPtr (+11 more)

### Community 7 - "WindowsFaults"
Cohesion: 0.08
Nodes (34): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+26 more)

### Community 8 - "FnShortcut"
Cohesion: 0.09
Nodes (25): DllImport, IEnumerable, IntPtr, List, StringBuilder, FnAction, FanTurbo, KeyboardBacklight (+17 more)

### Community 9 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 10 - "IShellLinkW"
Cohesion: 0.07
Nodes (13): PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder, AppIdentity (+5 more)

### Community 11 - "RadioButton"
Cohesion: 0.07
Nodes (37): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeGaming, ModeOffice (+29 more)

### Community 12 - "WindowsFaultsTests"
Cohesion: 0.12
Nodes (7): IReadOnlyDictionary, FaultEvaluation, Stable, Fact, WindowsFaultsTests, Dictionary, TopSharePercent

### Community 13 - "PlanTuning"
Cohesion: 0.13
Nodes (16): AcDc, Floor, Original, Plan, Action, DllImport, Guid, IntPtr (+8 more)

### Community 14 - "TrayPresence"
Cohesion: 0.10
Nodes (17): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, ProcessInformation, List, Ms (+9 more)

### Community 15 - "SystemInfo"
Cohesion: 0.08
Nodes (21): MemoryStatusEx, DriveUse, INotifyPropertyChanged, DriveRow, Name, Percent, PercentText, DllImport (+13 more)

### Community 16 - ".Info"
Cohesion: 0.10
Nodes (9): AssemblyInformationalVersionAttribute, Asynchronous dispatcher & UI thread hardening, KeyEventArgs, ModeDiscrete, ModeHybrid, ModeUma, Task, IProgress (+1 more)

### Community 17 - ".Get"
Cohesion: 0.09
Nodes (6): SolidColorBrush, Func, MessageBoxButton, MessageBoxResult, Action, Toasts

### Community 18 - "AppSettings"
Cohesion: 0.05
Nodes (35): FnShortcut, JsonElement, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoCooling, AutoFanTurbo, AutoInstallUpdates (+27 more)

### Community 19 - "xunit"
Cohesion: 0.12
Nodes (8): Nextcalibur.Core.Tests, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, Nextcalibur.Core.Power, system_security_cryptography_x509certificates, system_text_json, system_xml_linq, xunit

### Community 20 - ".WireSettingsPage"
Cohesion: 0.09
Nodes (11): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary, IReadOnlyDictionary, Task (+3 more)

### Community 21 - ".Warn"
Cohesion: 0.13
Nodes (8): The threat model, and what the code does about it, Application, DllImport, Mutex, App, RegistryKey, Footprint, STAThread

### Community 22 - "UserPresence"
Cohesion: 0.14
Nodes (12): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr, MarshalAs (+4 more)

### Community 23 - "Nextcalibur.Core.Hardware"
Cohesion: 0.11
Nodes (8): Nextcalibur.Cli, Nextcalibur.Core.Input, Nextcalibur.Core.Hardware, microsoft_win32_safehandles, system_diagnostics, system_management, system_runtime_interopservices, system_text

### Community 24 - ".ApplyFnShortcuts"
Cohesion: 0.09
Nodes (13): Problem, Detail, Dictionary, DllImport, IntPtr, ShortcutProblem, Duplicate, NotAKey (+5 more)

### Community 25 - "Fact"
Cohesion: 0.15
Nodes (9): Func, IEnumerable, IReadOnlyList, RetirementEntry, Trace, Version, Fact, FootprintTests (+1 more)

### Community 26 - "Button"
Cohesion: 0.07
Nodes (24): IsChecked, BannerDismissButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, HideToTrayButton, MinimiseButton (+16 more)

### Community 27 - ".RequestRestart"
Cohesion: 0.11
Nodes (11): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr (+3 more)

### Community 28 - "ProcessPresence"
Cohesion: 0.12
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 29 - "Elevation"
Cohesion: 0.13
Nodes (7): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, Fact, TaskFolderTests

### Community 30 - "CoolingPolicyTests"
Cohesion: 0.12
Nodes (16): At, Margin, DateTime, List, TimeSpan, CoolingPolicy, DegreesPerMinute, Engaged (+8 more)

### Community 31 - "SystemMode"
Cohesion: 0.18
Nodes (9): PowerModeChangedEventArgs, BatteryModePolicy, Remembered, SystemMode, Gaming, Office, Performance, Fact (+1 more)

### Community 32 - "UpdateService"
Cohesion: 0.10
Nodes (14): CancelEventArgs, EventArgs, DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService (+6 more)

### Community 33 - "Fact"
Cohesion: 0.16
Nodes (8): IList, HardwareSupport, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 34 - "SystemModeService"
Cohesion: 0.17
Nodes (9): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, SystemModeService (+1 more)

### Community 35 - ".PlaceTourStep"
Cohesion: 0.09
Nodes (12): SizeChangedEventArgs, FrameworkElement, KeyEventArgs, Point, RoutedEventArgs, Size, TourStep, Body (+4 more)

### Community 36 - "EcMailbox"
Cohesion: 0.13
Nodes (14): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+6 more)

### Community 37 - "Fact"
Cohesion: 0.10
Nodes (14): OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact, Task (+6 more)

### Community 38 - "LedState"
Cohesion: 0.15
Nodes (15): B, G, R, LedState, ActiveProfile, BrightnessPercent, Current, Effect (+7 more)

### Community 39 - "MachineScanRecord"
Cohesion: 0.11
Nodes (18): DateTime, JsonSerializerOptions, List, MachineScan, RecordPath, MachineScanRecord, BiosVendor, BiosVersion (+10 more)

### Community 40 - ".Check"
Cohesion: 0.13
Nodes (9): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported, IdempotenceTests (+1 more)

### Community 41 - "PowerOverlayService"
Cohesion: 0.16
Nodes (10): Option, Button, IEnumerable, DllImport, Guid, IReadOnlyList, PowerModeOption, PowerOverlays (+2 more)

### Community 42 - "InstallFolderGuard"
Cohesion: 0.16
Nodes (9): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, InlineData, Theory, ElevationPolicyTests, Profile (+1 more)

### Community 43 - "Nextcalibur.App"
Cohesion: 0.10
Nodes (13): Nextcalibur.App, system_componentmodel, system_io, system_io_directory, system_io_ioexception, system_io_path, system_linq, system_runtime_compilerservices (+5 more)

### Community 44 - "Nextcalibur.Core.Configuration"
Cohesion: 0.11
Nodes (9): Nextcalibur.Core.Configuration, microsoft_win32, Theme, Dark, Light, system_drawing, system_security, system_text_json_serialization (+1 more)

### Community 45 - "ProtectedStore"
Cohesion: 0.18
Nodes (9): Content, Hash, Dictionary, IReadOnlyList, List, ProtectedStore, Folder, ReportedSoFar (+1 more)

### Community 46 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 47 - ".Migrate"
Cohesion: 0.12
Nodes (8): DateTime, Dictionary, List, PendingRestartInfo, BootTimeUtc, ReasonArguments, Reasons, SettingsTests

### Community 48 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.12), System.Management (10.0.12), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk (+6 more)

### Community 49 - "ToggleButton"
Cohesion: 0.10
Nodes (18): LedPower, OverheatWarningToggle, SelectAll, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost (+10 more)

### Community 50 - "MainWindow.FnKeys.cs"
Cohesion: 0.21
Nodes (12): Nextcalibur.App.Controls, system_windows, system_windows_controls, system_windows_controls_button, system_windows_controls_checkbox, system_windows_controls_primitives, system_windows_input, system_windows_input_keyeventargs (+4 more)

### Community 51 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 52 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 53 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 54 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 55 - "ValueConverters.cs"
Cohesion: 0.17
Nodes (10): Nextcalibur.Core, CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_globalization, system_text_regularexpressions (+2 more)

### Community 56 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 57 - "SmiCommand"
Cohesion: 0.21
Nodes (6): ArgumentException, SmiCommand, Fact, InlineData, Theory, SmiCommandTests

### Community 58 - "RestorePoint"
Cohesion: 0.18
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 59 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 60 - ".Ask"
Cohesion: 0.35
Nodes (7): HttpStatusCode, Fact, InlineData, Task, Theory, Answer, HostileAnswerTests

### Community 61 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 62 - "ProtectedStoreTests"
Cohesion: 0.29
Nodes (3): Fact, List, ProtectedStoreTests

### Community 63 - "GpuModeService"
Cohesion: 0.28
Nodes (4): ArgumentNullException, GpuModeService, SwitchOutcome, SwitchOutcome

### Community 64 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 65 - "HostileFilesTests"
Cohesion: 0.27
Nodes (5): IOException, Fact, InlineData, Theory, HostileFilesTests

### Community 66 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 67 - ".Record"
Cohesion: 0.18
Nodes (7): IReadOnlyDictionary, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Fact, AuditLogTests

### Community 68 - "SettingsTests.cs"
Cohesion: 0.19
Nodes (7): system_reflection, system_security_accesscontrol, system_security_cryptography, system_security_principal, InlineData, Theory, PawnIoModuleTests

### Community 69 - "ReleaseFile"
Cohesion: 0.22
Nodes (9): CancellationToken, HttpClient, Task, Uri, ReleaseFile, CancellationToken, HttpClient, Task (+1 more)

### Community 71 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 72 - "LedEffect"
Cohesion: 0.14
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 73 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 74 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 75 - "SystemTools"
Cohesion: 0.17
Nodes (9): FileStream, SystemTools, Cmd, Explorer, Pnputil, Powercfg, PowerShell, Schtasks (+1 more)

### Community 76 - "CoreLoad"
Cohesion: 0.17
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 77 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 78 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 79 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 80 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 81 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 82 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 83 - "Dependency"
Cohesion: 0.17
Nodes (9): Version, Dependency, ExpectedSigner, Id, MayBeRemoved, Name, Purpose, SilentInstallArguments (+1 more)

### Community 84 - "Nextcalibur.Core.Dependencies"
Cohesion: 0.35
Nodes (4): Nextcalibur.Core.Dependencies, system_collections_concurrent, system_net, system_net_http

### Community 85 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 86 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 87 - ".Sanitised"
Cohesion: 0.29
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 88 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 89 - "GpuMode"
Cohesion: 0.27
Nodes (6): GpuLoad, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma

### Community 90 - "ThermalSample"
Cohesion: 0.29
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 91 - "ThemeService"
Cohesion: 0.20
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 92 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 93 - "SupportVerdict"
Cohesion: 0.22
Nodes (9): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites (+1 more)

### Community 94 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 95 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 96 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 97 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 98 - "README.md"
Cohesion: 0.33
Nodes (3): How it works, Questions people ask, Using it

### Community 99 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 101 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 103 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 104 - ".Secure"
Cohesion: 0.36
Nodes (4): DirectorySecurity, FileSystemAccessRule, FileSystemRights, SecurityIdentifier

### Community 105 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 106 - "StackPanel"
Cohesion: 0.25
Nodes (8): BannerActions, DialogActions, DriveTextStack, FnShortcutRows, PanelWindowsFaultsSubOptions, ProfileRow, SettingUpdatesAutomatic, StackPanel

### Community 108 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 109 - "ProtectedStoreElevatedTests"
Cohesion: 0.32
Nodes (4): Fact, SecurityIdentifier, ProtectedStoreElevatedTests, Elevated

### Community 110 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 111 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 112 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 113 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 114 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 115 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 116 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 117 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 118 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 119 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 120 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 121 - ".ForegroundProcess"
Cohesion: 0.47
Nodes (3): DllImport, IntPtr, Name

### Community 123 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 124 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 125 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 126 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 128 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 130 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 131 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 132 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 133 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 134 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 135 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 137 - ".TryParseRgb"
Cohesion: 0.50
Nodes (3): B, G, R

### Community 138 - ".SendAsync"
Cohesion: 0.50
Nodes (3): CancellationToken, HttpRequestMessage, HttpResponseMessage

### Community 139 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 142 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 143 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

### Community 144 - "Words"
Cohesion: 0.67
Nodes (3): Func, Words, Resolver

## Knowledge Gaps
- **454 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+449 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 861 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **33 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `GpuClockReader`, `RunawayWatchTests`, `.CheckProgram`, `CpuPowerReader`, `WindowsFaults`, `FnShortcut`, `Window`, `RadioButton`, `PlanTuning`, `TrayPresence`, `SystemInfo`, `.Info`, `.Get`, `AppSettings`, `.WireSettingsPage`, `UserPresence`, `.ApplyFnShortcuts`, `Button`, `.RequestRestart`, `ProcessPresence`, `CoolingPolicyTests`, `SystemMode`, `UpdateService`, `SystemModeService`, `.PlaceTourStep`, `EcMailbox`, `Fact`, `PowerOverlayService`, `Nextcalibur.App`, `.Read`, `ToggleButton`, `MainWindow.FnKeys.cs`, `BacklightKeyWatcher`, `DependencyStatus`, `LedController`, `GpuModeService`, `.Record`, `LedEffect`, `Strings`, `Grid`, `GpuMode`, `ThermalSample`, `ThemeService`, `NvidiaDriverState`, `SupportVerdict`, `TourPage`, `.OnBrightnessChanged`, `.ForegroundProcess`, `.Sample`, `LedZone`?**
  _High betweenness centrality (0.468) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Keycaps`, `DriveGauge`, `RadioButton`, `CpuWarnValue`, `DialogCheck`, `.Info`, `ColGauge`, `.WireSettingsPage`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `Button`, `TourCanvas`, `Wheel`, `ToggleButton`, `Grid`, `Border`, `StackPanel`, `Text`, `.OnBrightnessChanged`?**
  _High betweenness centrality (0.120) - this node is a cross-community bridge._
- **Why does `AppSettings` connect `AppSettings` to `UpdateService`, `HostileFilesTests`, `MainWindow`, `NduFix`, `Nextcalibur.Core.Configuration`, `WindowsFaultsTests`, `TrayPresence`, `.Migrate`, `Strings`, `.Sanitised`, `Fact`, `ThemeService`, `ProtectedStoreTests`, `SystemMode`?**
  _High betweenness centrality (0.082) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _454 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DotNetRuntimeDependency` be split into smaller, more focused modules?**
  _Cohesion score 0.05348101265822785 - nodes in this community are weakly interconnected._
- **Should `Authenticode` be split into smaller, more focused modules?**
  _Cohesion score 0.062111801242236024 - nodes in this community are weakly interconnected._