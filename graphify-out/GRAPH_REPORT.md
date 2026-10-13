# Graph Report - nextcalibur-control-center  (2026-10-13)

## Corpus Check
- 180 files · ~228,370 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3013 nodes · 6568 edges · 172 communities (141 shown, 31 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 353 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `089e5333`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- PlanTuning
- AppSettings
- LedState
- ProtectedStore
- RunawayWatchTests
- GpuClockReader
- ProcessPresence
- TrayPresence
- ProgramRule
- IShellLinkW
- .Get
- WindowsFaults
- FnShortcut
- RadioButton
- Window
- .CheckProgram
- Nextcalibur.App
- CpuPowerReader
- .OnLoaded
- Nextcalibur.Core.Hardware
- .WireSettingsPage
- .ApplyFnShortcuts
- EcMailbox
- .Survey
- xunit
- SystemInfo
- UserPresence
- WindowsFaultsTests
- .Check
- SystemMode
- .Info
- NetworkCostTests
- .RequestRestart
- InstallFolderGuard
- CoolingPolicyTests
- .Get
- .RemoveMachineTraces
- Button
- Dependency
- MachineScanRecord
- .SignatureIsValid
- Nextcalibur.Core.Security
- Authenticode
- Fact
- PowerOverlayService
- .Main
- .Ask
- DotNetRuntimeDependency
- Nextcalibur.Core.csproj
- RestorePoint
- BacklightKeyWatcher
- .For
- MemoryTrimmer
- DependencyStatus
- .Dictionary
- analyse-autopsy.py
- Program
- UpdateService
- ToggleButton
- RuntimeDependencyTests
- CoreLoad
- ColourWheel
- ResourceDictionary
- FanGauge
- .RefreshProgramRuleRows
- .OnSystemModeChanged
- Unelevated
- DonutGauge
- Nextcalibur.Core.Configuration
- SegmentedBar
- .RunLighting
- LogInjectionTests
- Nextcalibur.Core.Power
- SystemTools
- Grid
- Footprint
- .Check
- BatteryModePolicy
- StringsDictionaryTests
- ValueConverters.cs
- Nextcalibur 0.5.2
- Nextcalibur Control Center
- Strings
- SmiCommandTests
- Hardware Protocol
- Nextcalibur 0.5.3
- Nextcalibur 0.5.4
- HostileFilesTests
- Border
- ThemeService
- NvidiaDriverState
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- StackPanel
- ComboBox
- ThermalSample
- SettingsTests.cs
- PawnIoDependency
- .IsSystemFileSignedBy
- ProfileFiles
- Probe
- .GetAsync
- Nextcalibur 0.5.1
- NotificationState
- IntegrityKind
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- .Pick
- .ForegroundProcess
- GameSession
- TourPage
- .Values_are_the_ones_measured_from_the_vendor
- Log-Graphics.ps1
- Privacy
- Security
- .Identify
- DriveRow
- .PowerModeControls
- MachineWideGate
- PowerSource
- Notice
- Nextcalibur 0.5.11
- Nextcalibur 0.6.2
- AppIdentity.cs
- TourStep
- SmiSubsystem
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- Nextcalibur 0.6.1
- .ArrangeOverride
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
1. `MainWindow` - 341 edges
2. `Window` - 182 edges
3. `AppSettings` - 75 edges
4. `Nextcalibur.Core.Hardware` - 54 edges
5. `SystemMode` - 43 edges
6. `WindowsFaultsTests` - 40 edges
7. `RadioButton` - 39 edges
8. `Nextcalibur.Core.Configuration` - 38 edges
9. `TextBlock` - 36 edges
10. `WindowsFaults` - 33 edges

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

## Communities (172 total, 31 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.03
Nodes (60): CheckBox, DeferredOverheat, HwndSource, ObservableCollection, Queue, SizeChangedEventArgs, MainWindow, DateTime (+52 more)

### Community 1 - "PlanTuning"
Cohesion: 0.06
Nodes (37): Floor, Marker, Original, PluggedIn, Seconds, Action, DllImport, IntPtr (+29 more)

### Community 2 - "AppSettings"
Cohesion: 0.04
Nodes (53): DateTime, Dictionary, FnShortcut, JsonElement, List, ProgramRule, AppSettings, AcceptedVendorSoftware (+45 more)

### Community 3 - "LedState"
Cohesion: 0.05
Nodes (42): B, Dictionary, G, R, LedProfile, BrightnessPercent, Colours, Effect (+34 more)

### Community 4 - "ProtectedStore"
Cohesion: 0.07
Nodes (26): Content, DirectorySecurity, Hash, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Dictionary (+18 more)

### Community 5 - "RunawayWatchTests"
Cohesion: 0.06
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 6 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 7 - "ProcessPresence"
Cohesion: 0.06
Nodes (23): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+15 more)

### Community 8 - "TrayPresence"
Cohesion: 0.06
Nodes (21): ContextMenuStrip, DevPropKey, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, EventHandler (+13 more)

### Community 9 - "ProgramRule"
Cohesion: 0.07
Nodes (26): GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, IEnumerable (+18 more)

### Community 10 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 11 - ".Get"
Cohesion: 0.07
Nodes (6): SolidColorBrush, IReadOnlyList, GameEnd, Func, Task, TextBlock

### Community 12 - "WindowsFaults"
Cohesion: 0.08
Nodes (34): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+26 more)

### Community 13 - "FnShortcut"
Cohesion: 0.08
Nodes (25): DllImport, IEnumerable, IntPtr, List, StringBuilder, FnAction, FanTurbo, KeyboardBacklight (+17 more)

### Community 14 - "RadioButton"
Cohesion: 0.06
Nodes (40): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeDiscrete, ModeGaming (+32 more)

### Community 15 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 16 - ".CheckProgram"
Cohesion: 0.10
Nodes (15): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvmlPath, IntegrityFinding (+7 more)

### Community 17 - "Nextcalibur.App"
Cohesion: 0.10
Nodes (23): Nextcalibur.App, Nextcalibur.App.Controls, system_componentmodel, system_io, system_linq, system_runtime_compilerservices, system_windows, system_windows_controls (+15 more)

### Community 18 - "CpuPowerReader"
Cohesion: 0.09
Nodes (21): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+13 more)

### Community 19 - ".OnLoaded"
Cohesion: 0.09
Nodes (8): AssemblyInformationalVersionAttribute, Asynchronous dispatcher & UI thread hardening, Slider, Action, Toasts, IProgress, Exception, TextBox

### Community 20 - "Nextcalibur.Core.Hardware"
Cohesion: 0.09
Nodes (12): Nextcalibur.Cli, Nextcalibur.Core.Input, Nextcalibur.Core.Hardware, microsoft_win32_safehandles, system_diagnostics, system_management, system_reflection, system_runtime_interopservices (+4 more)

### Community 21 - ".WireSettingsPage"
Cohesion: 0.10
Nodes (12): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary, IReadOnlyDictionary, Task (+4 more)

### Community 22 - ".ApplyFnShortcuts"
Cohesion: 0.08
Nodes (15): CancelEventArgs, Problem, Detail, Dictionary, DllImport, IntPtr, KeyEventArgs, ShortcutProblem (+7 more)

### Community 23 - "EcMailbox"
Cohesion: 0.13
Nodes (14): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+6 more)

### Community 24 - ".Survey"
Cohesion: 0.12
Nodes (8): Func, IEnumerable, IReadOnlyList, RetirementEntry, Trace, Version, NduFix, FootprintTests

### Community 25 - "xunit"
Cohesion: 0.11
Nodes (5): Nextcalibur.Core.Tests, nextcalibur_core_power_plantuning_acdc, system_text_regularexpressions, system_xml_linq, xunit

### Community 26 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 27 - "UserPresence"
Cohesion: 0.13
Nodes (14): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, Point, Size, DateTime, DllImport (+6 more)

### Community 28 - "WindowsFaultsTests"
Cohesion: 0.17
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 29 - ".Check"
Cohesion: 0.11
Nodes (13): CommonAce, RawSecurityDescriptor, RegistryKey, SecurityIdentifier, MailboxAccess, MailboxAvailability, AccessNotGranted, Available (+5 more)

### Community 30 - "SystemMode"
Cohesion: 0.15
Nodes (15): InvalidOperationException, Ours, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList (+7 more)

### Community 31 - ".Info"
Cohesion: 0.11
Nodes (5): ComboBox, MessageBoxButton, MessageBoxResult, Log, Folder

### Community 32 - "NetworkCostTests"
Cohesion: 0.14
Nodes (19): Bytes, Count, HttpMessageHandler, CancellationToken, Dictionary, Fact, HttpRequestMessage, HttpResponseMessage (+11 more)

### Community 33 - ".RequestRestart"
Cohesion: 0.11
Nodes (11): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr (+3 more)

### Community 34 - "InstallFolderGuard"
Cohesion: 0.14
Nodes (10): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Fact, InlineData, Theory, ElevationPolicyTests (+2 more)

### Community 35 - "CoolingPolicyTests"
Cohesion: 0.12
Nodes (16): At, Margin, DateTime, List, TimeSpan, CoolingPolicy, DegreesPerMinute, Engaged (+8 more)

### Community 36 - ".Get"
Cohesion: 0.13
Nodes (14): ArgumentNullException, GpuLoad, FirmwareModeReading, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma (+6 more)

### Community 37 - ".RemoveMachineTraces"
Cohesion: 0.13
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 38 - "Button"
Cohesion: 0.08
Nodes (23): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, MinimiseButton (+15 more)

### Community 39 - "Dependency"
Cohesion: 0.11
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 40 - "MachineScanRecord"
Cohesion: 0.11
Nodes (18): DateTime, JsonSerializerOptions, List, MachineScan, RecordPath, MachineScanRecord, BiosVendor, BiosVersion (+10 more)

### Community 41 - ".SignatureIsValid"
Cohesion: 0.15
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 42 - "Nextcalibur.Core.Security"
Cohesion: 0.15
Nodes (8): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_security_cryptography_x509certificates, system_text_json

### Community 43 - "Authenticode"
Cohesion: 0.23
Nodes (15): CatalogInfo, Security, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle (+7 more)

### Community 44 - "Fact"
Cohesion: 0.18
Nodes (7): IList, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 45 - "PowerOverlayService"
Cohesion: 0.16
Nodes (11): DllImport, Guid, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, PowerModeOption, PowerOverlays (+3 more)

### Community 46 - ".Main"
Cohesion: 0.15
Nodes (8): The threat model, and what the code does about it, Application, DllImport, Mutex, App, StartupRegistration, IsEnabled, STAThread

### Community 47 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 48 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 49 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.12), System.Management (10.0.12), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 50 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 51 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 52 - ".For"
Cohesion: 0.16
Nodes (12): MemberData, AcDc, Guid, IReadOnlyDictionary, IReadOnlyList, ModeProfile, Settings, Fact (+4 more)

### Community 53 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 54 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 55 - ".Dictionary"
Cohesion: 0.15
Nodes (9): IReadOnlyDictionary, Stable, Dictionary, Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests (+1 more)

### Community 56 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 57 - "Program"
Cohesion: 0.18
Nodes (5): ConsoleColor, B, G, R, Program

### Community 58 - "UpdateService"
Cohesion: 0.13
Nodes (12): DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService, Available, AvailableVersion (+4 more)

### Community 59 - "ToggleButton"
Cohesion: 0.11
Nodes (18): OverheatWarningToggle, SettingAskGameMode, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost, SettingFixWidgets (+10 more)

### Community 60 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 61 - "CoreLoad"
Cohesion: 0.15
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 62 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 63 - "ResourceDictionary"
Cohesion: 0.17
Nodes (15): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+7 more)

### Community 64 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 67 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 68 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 69 - "Nextcalibur.Core.Configuration"
Cohesion: 0.16
Nodes (7): Nextcalibur.Core, Nextcalibur.Core.Configuration, system_drawing, system_globalization, system_security, system_text_json_serialization, system_windows_forms

### Community 70 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 71 - ".RunLighting"
Cohesion: 0.15
Nodes (8): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, LedPower, ReloadButton, Action, Slider

### Community 72 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 73 - "Nextcalibur.Core.Power"
Cohesion: 0.21
Nodes (5): Nextcalibur.Core.Power, microsoft_win32, Theme, Dark, Light

### Community 74 - "SystemTools"
Cohesion: 0.17
Nodes (9): FileStream, SystemTools, Cmd, Explorer, Pnputil, Powercfg, PowerShell, Schtasks (+1 more)

### Community 75 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 77 - ".Check"
Cohesion: 0.18
Nodes (10): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+2 more)

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

### Community 82 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 83 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 84 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 85 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 86 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 87 - "Nextcalibur 0.5.4"
Cohesion: 0.18
Nodes (10): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, the second pass, Smaller, Tested on, The language row, The .NET runtime is now kept current like everything else (+2 more)

### Community 88 - "HostileFilesTests"
Cohesion: 0.42
Nodes (3): IOException, Fact, HostileFilesTests

### Community 89 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 90 - "ThemeService"
Cohesion: 0.20
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 91 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 92 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 93 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 94 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 95 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 96 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 97 - "StackPanel"
Cohesion: 0.22
Nodes (9): BannerActions, DialogActions, DriveTextStack, FnShortcutRows, PanelWindowsFaultsSubOptions, ProfileRow, ProgramRuleRows, SettingUpdatesAutomatic (+1 more)

### Community 98 - "ComboBox"
Cohesion: 0.22
Nodes (9): IdleDisplayAc, IdleDisplayDc, IdleMode, IdleScreenSaver, IdleSleepAc, IdleSleepDc, ProgramRuleMode, ProgramRuleName (+1 more)

### Community 99 - "ThermalSample"
Cohesion: 0.33
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 100 - "SettingsTests.cs"
Cohesion: 0.39
Nodes (3): system_security_accesscontrol, system_security_cryptography, system_security_principal

### Community 101 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 102 - ".IsSystemFileSignedBy"
Cohesion: 0.39
Nodes (3): Fact, SystemFileTrustTests, Cmd

### Community 104 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 105 - ".GetAsync"
Cohesion: 0.29
Nodes (7): Answer, ConcurrentDictionary, CancellationToken, HttpClient, Task, Answer, Conditional

### Community 106 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 107 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 108 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 109 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 110 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 111 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 112 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 113 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 114 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 115 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 116 - ".ForegroundProcess"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Name, Pid

### Community 117 - "GameSession"
Cohesion: 0.29
Nodes (7): Dictionary, GameSession, FirstSeenUtc, Looks, Name, Pid, ProcessStart

### Community 118 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 119 - ".Values_are_the_ones_measured_from_the_vendor"
Cohesion: 0.33
Nodes (4): Fact, InlineData, Theory, ThermalProfileTests

### Community 120 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 121 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 122 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 123 - ".Identify"
Cohesion: 0.33
Nodes (4): GameSession, DateTime, Name, Pid

### Community 124 - "DriveRow"
Cohesion: 0.33
Nodes (5): INotifyPropertyChanged, DriveRow, Name, Percent, PercentText

### Community 125 - ".PowerModeControls"
Cohesion: 0.33
Nodes (5): Option, Button, IEnumerable, Text, TextBlock

### Community 126 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 127 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 128 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 129 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 130 - "Nextcalibur 0.6.2"
Cohesion: 0.40
Nodes (4): Fixed, New, Nextcalibur 0.6.2, Tested on

### Community 131 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 132 - "TourStep"
Cohesion: 0.40
Nodes (5): TourStep, Body, SectionName, Title, TourPage

### Community 133 - "SmiSubsystem"
Cohesion: 0.40
Nodes (5): SmiSubsystem, DisplayMode, Led, Profile, Thermal

### Community 134 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 136 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 137 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 138 - "DriveGauge"
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

### Community 144 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 147 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 148 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

## Knowledge Gaps
- **480 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+475 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 932 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **31 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `PlanTuning`, `AppSettings`, `LedState`, `TourStep`, `RunawayWatchTests`, `GpuClockReader`, `ProcessPresence`, `TrayPresence`, `ProgramRule`, `IShellLinkW`, `.Get`, `WindowsFaults`, `FnShortcut`, `RadioButton`, `Window`, `.CheckProgram`, `Nextcalibur.App`, `CpuPowerReader`, `.OnLoaded`, `Nextcalibur.Core.Hardware`, `.WireSettingsPage`, `.ApplyFnShortcuts`, `EcMailbox`, `SystemInfo`, `UserPresence`, `SystemMode`, `.Info`, `.RequestRestart`, `CoolingPolicyTests`, `.Get`, `Button`, `Nextcalibur.Core.Security`, `PowerOverlayService`, `BacklightKeyWatcher`, `DependencyStatus`, `UpdateService`, `.RefreshProgramRuleRows`, `.OnSystemModeChanged`, `.RunLighting`, `Grid`, `.Check`, `BatteryModePolicy`, `Strings`, `ThemeService`, `NvidiaDriverState`, `ThermalSample`, `.ForegroundProcess`, `GameSession`, `TourPage`, `.Identify`, `.PowerModeControls`?**
  _High betweenness centrality (0.498) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Keycaps`, `DriveGauge`, `RadioButton`, `CpuWarnValue`, `DialogCheck`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `Button`, `ToggleButton`, `.RunLighting`, `Grid`, `Border`, `StackPanel`, `ComboBox`, `Text`?**
  _High betweenness centrality (0.092) - this node is a cross-community bridge._
- **Why does `AppSettings` connect `AppSettings` to `MainWindow`, `LedState`, `ProtectedStore`, `Nextcalibur.Core.Configuration`, `TrayPresence`, `Footprint`, `Strings`, `.OnLoaded`, `.Survey`, `ThemeService`, `WindowsFaultsTests`, `SystemMode`?**
  _High betweenness centrality (0.088) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _480 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.030154277699859747 - nodes in this community are weakly interconnected._
- **Should `PlanTuning` be split into smaller, more focused modules?**
  _Cohesion score 0.058254963427377224 - nodes in this community are weakly interconnected._