# Graph Report - nextcalibur-control-center  (2026-10-11)

## Corpus Check
- 164 files · ~215,723 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2769 nodes · 5955 edges · 165 communities (132 shown, 33 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 319 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `00ab61da`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- DotNetRuntimeDependency
- Authenticode
- PlanTuning
- ProtectedStore
- MainWindow
- RunawayWatchTests
- GpuClockReader
- AppSettings
- FnShortcut
- .CheckProgram
- WindowsFaults
- RadioButton
- Window
- IShellLinkW
- WindowsFaultsTests
- .Info
- .Get
- Fact
- Nextcalibur.Core.Hardware
- UserPresence
- RoutedEventArgs
- SystemInfo
- .RequestRestart
- .Main
- SystemTools
- .ApplyFnShortcuts
- SystemMode
- Fact
- ProcessPresence
- Elevation
- EcMailbox
- CpuPowerReader
- TrayPresence
- system_runtime_interopservices
- Nextcalibur.Core.Configuration
- .PlaceTourStep
- InstallFolderGuard
- LedState
- MachineScanRecord
- Fact
- .Get
- Button
- .Check
- .Read
- BacklightKeyWatcher
- Nextcalibur.Core.csproj
- UpdateService
- Program
- MainWindow.FnKeys.cs
- MemoryTrimmer
- DependencyStatus
- analyse-autopsy.py
- LedController
- PowerOverlayService
- ToggleButton
- ResourceDictionary
- Nextcalibur.Core.Power
- ColourWheel
- system_text_json
- DevicePowerState
- .Ask
- FanGauge
- Unelevated
- DonutGauge
- SegmentedBar
- .OnGpuModeChanged
- LedEffect
- .HasAccess
- LogInjectionTests
- .Create
- Nextcalibur.Core.Security
- CoreLoad
- PawnIoDependency
- BatteryModePolicy
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur Control Center
- Strings
- Grid
- Dependency
- .Check
- SmiCommandTests
- Hardware Protocol
- Nextcalibur 0.5.3
- .OnSystemModeChanged
- .WireSettingsPage
- ThemeService
- ReleaseFile
- Border
- .Sanitised
- NvidiaDriverState
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- Probe
- Nextcalibur 0.5.1
- StackPanel
- ThermalSample
- NduFix
- NotificationState
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.5
- Nextcalibur 0.5.9
- Releasing, and signing
- .Pick
- .OnThresholdMoved
- TourPage
- Log-Graphics.ps1
- Privacy
- Security
- .ForegroundProcess
- LedZone
- MachineWideGate
- PowerSource
- Notice
- Nextcalibur 0.5.11
- .PowerModeControls
- AppIdentity.cs
- .Set
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- .ArrangeOverride
- BrightnessSlider
- .SendAsync
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
1. `MainWindow` - 284 edges
2. `Window` - 169 edges
3. `AppSettings` - 70 edges
4. `Nextcalibur.Core.Hardware` - 51 edges
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

## Communities (165 total, 33 thin omitted)

### Community 0 - "DotNetRuntimeDependency"
Cohesion: 0.05
Nodes (46): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+38 more)

### Community 1 - "Authenticode"
Cohesion: 0.06
Nodes (38): CatalogInfo, Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on (+30 more)

### Community 2 - "PlanTuning"
Cohesion: 0.07
Nodes (32): AcDc, At, Floor, Margin, Original, Plan, Action, DateTime (+24 more)

### Community 3 - "ProtectedStore"
Cohesion: 0.06
Nodes (27): Content, DirectorySecurity, Hash, IReadOnlyDictionary, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion (+19 more)

### Community 4 - "MainWindow"
Cohesion: 0.04
Nodes (41): CheckBox, DeferredOverheat, HwndSource, ObservableCollection, Queue, MainWindow, DateTime, CoolingWantsReadings (+33 more)

### Community 5 - "RunawayWatchTests"
Cohesion: 0.06
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 6 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 7 - "AppSettings"
Cohesion: 0.04
Nodes (44): Dictionary, FnShortcut, JsonElement, List, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoCooling (+36 more)

### Community 8 - "FnShortcut"
Cohesion: 0.07
Nodes (33): DllImport, IEnumerable, IntPtr, List, StringBuilder, FnAction, FanTurbo, KeyboardBacklight (+25 more)

### Community 9 - ".CheckProgram"
Cohesion: 0.07
Nodes (23): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvmlPath, IntegrityFinding (+15 more)

### Community 10 - "WindowsFaults"
Cohesion: 0.08
Nodes (34): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+26 more)

### Community 11 - "RadioButton"
Cohesion: 0.06
Nodes (40): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeDiscrete, ModeGaming (+32 more)

### Community 12 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 13 - "IShellLinkW"
Cohesion: 0.07
Nodes (13): PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder, AppIdentity (+5 more)

### Community 14 - "WindowsFaultsTests"
Cohesion: 0.12
Nodes (7): IReadOnlyDictionary, FaultEvaluation, Stable, Fact, WindowsFaultsTests, Dictionary, TopSharePercent

### Community 15 - ".Info"
Cohesion: 0.10
Nodes (7): AssemblyInformationalVersionAttribute, Asynchronous dispatcher & UI thread hardening, Task, IProgress, Exception, Log, Folder

### Community 16 - ".Get"
Cohesion: 0.10
Nodes (6): SolidColorBrush, Func, MessageBoxButton, MessageBoxResult, Action, Toasts

### Community 17 - "Fact"
Cohesion: 0.12
Nodes (10): DateTime, Func, IEnumerable, IReadOnlyList, RetirementEntry, Trace, Version, Fact (+2 more)

### Community 18 - "Nextcalibur.Core.Hardware"
Cohesion: 0.13
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Tests.Attacks, Nextcalibur.Core.Hardware, system_text_regularexpressions, system_xml_linq, xunit

### Community 19 - "UserPresence"
Cohesion: 0.14
Nodes (12): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr, MarshalAs (+4 more)

### Community 20 - "RoutedEventArgs"
Cohesion: 0.09
Nodes (10): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary, IReadOnlyDictionary, Task (+2 more)

### Community 21 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 22 - ".RequestRestart"
Cohesion: 0.10
Nodes (10): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr, MarshalAs (+2 more)

### Community 23 - ".Main"
Cohesion: 0.13
Nodes (8): The threat model, and what the code does about it, Application, DllImport, Mutex, App, RegistryKey, Footprint, STAThread

### Community 24 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 25 - ".ApplyFnShortcuts"
Cohesion: 0.08
Nodes (11): INotifyPropertyChanged, Problem, DriveRow, Detail, Name, Percent, PercentText, Dictionary (+3 more)

### Community 26 - "SystemMode"
Cohesion: 0.14
Nodes (13): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, SystemMode (+5 more)

### Community 27 - "Fact"
Cohesion: 0.14
Nodes (9): IList, GpuLoad, GpuConfiguration, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests (+1 more)

### Community 28 - "ProcessPresence"
Cohesion: 0.13
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 29 - "Elevation"
Cohesion: 0.13
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 30 - "EcMailbox"
Cohesion: 0.16
Nodes (10): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+2 more)

### Community 31 - "CpuPowerReader"
Cohesion: 0.16
Nodes (9): DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled, Fact, InlineData, Theory (+1 more)

### Community 32 - "TrayPresence"
Cohesion: 0.11
Nodes (11): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, EventArgs, List, Ms (+3 more)

### Community 33 - "system_runtime_interopservices"
Cohesion: 0.09
Nodes (7): Nextcalibur.Core.Input, microsoft_win32_safehandles, system_diagnostics, system_management, system_reflection, system_runtime_interopservices, system_text

### Community 34 - "Nextcalibur.Core.Configuration"
Cohesion: 0.11
Nodes (14): Nextcalibur.App, Nextcalibur.Core, Nextcalibur.Core.Configuration, system_componentmodel, system_drawing, system_globalization, system_io, system_linq (+6 more)

### Community 35 - ".PlaceTourStep"
Cohesion: 0.09
Nodes (12): SizeChangedEventArgs, FrameworkElement, KeyEventArgs, Point, RoutedEventArgs, Size, TourStep, Body (+4 more)

### Community 36 - "InstallFolderGuard"
Cohesion: 0.14
Nodes (10): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Fact, InlineData, Theory, ElevationPolicyTests (+2 more)

### Community 37 - "LedState"
Cohesion: 0.15
Nodes (15): B, G, R, LedState, ActiveProfile, BrightnessPercent, Current, Effect (+7 more)

### Community 38 - "MachineScanRecord"
Cohesion: 0.11
Nodes (18): DateTime, JsonSerializerOptions, List, MachineScan, RecordPath, MachineScanRecord, BiosVendor, BiosVersion (+10 more)

### Community 39 - "Fact"
Cohesion: 0.11
Nodes (14): OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact, Task (+6 more)

### Community 40 - ".Get"
Cohesion: 0.16
Nodes (12): ArgumentNullException, FirmwareModeReading, GpuMode, Discrete, Hybrid, Uma, GpuModeService, SwitchOutcome (+4 more)

### Community 41 - "Button"
Cohesion: 0.09
Nodes (23): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, MinimiseButton (+15 more)

### Community 42 - ".Check"
Cohesion: 0.14
Nodes (9): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported, IdempotenceTests (+1 more)

### Community 43 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 44 - "BacklightKeyWatcher"
Cohesion: 0.11
Nodes (13): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, Known, LedBrightness, Full, Half (+5 more)

### Community 45 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.12), System.Management (10.0.12), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk (+6 more)

### Community 46 - "UpdateService"
Cohesion: 0.13
Nodes (13): CancelEventArgs, DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService, Available (+5 more)

### Community 47 - "Program"
Cohesion: 0.17
Nodes (5): ConsoleColor, B, G, R, Program

### Community 48 - "MainWindow.FnKeys.cs"
Cohesion: 0.21
Nodes (12): Nextcalibur.App.Controls, system_windows, system_windows_controls, system_windows_controls_button, system_windows_controls_checkbox, system_windows_controls_primitives, system_windows_input, system_windows_input_keyeventargs (+4 more)

### Community 49 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 50 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 51 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 52 - "LedController"
Cohesion: 0.22
Nodes (5): RoutedPropertyChangedEventArgs, LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 53 - "PowerOverlayService"
Cohesion: 0.23
Nodes (7): DllImport, Guid, IReadOnlyList, PowerModeOption, PowerOverlays, All, PowerOverlayService

### Community 54 - "ToggleButton"
Cohesion: 0.11
Nodes (18): LedPower, OverheatWarningToggle, SelectAll, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost (+10 more)

### Community 55 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 56 - "Nextcalibur.Core.Power"
Cohesion: 0.14
Nodes (7): Nextcalibur.Cli, Nextcalibur.Core.Power, microsoft_win32, Theme, Dark, Light, system_security

### Community 57 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 58 - "system_text_json"
Cohesion: 0.23
Nodes (6): Nextcalibur.Core.Dependencies, system_collections_concurrent, system_net, system_net_http, system_text_json, system_text_json_serialization

### Community 59 - "DevicePowerState"
Cohesion: 0.25
Nodes (6): DevPropKey, Discrete GPU sleep protection in Hybrid mode, DllImport, Guid, DevicePowerState, DevPropKey

### Community 60 - ".Ask"
Cohesion: 0.35
Nodes (7): HttpStatusCode, Fact, InlineData, Task, Theory, Answer, HostileAnswerTests

### Community 61 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 62 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 63 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 64 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 66 - "LedEffect"
Cohesion: 0.14
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 67 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 68 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 69 - ".Create"
Cohesion: 0.21
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 70 - "Nextcalibur.Core.Security"
Cohesion: 0.27
Nodes (5): Nextcalibur.Core.Security, system_security_accesscontrol, system_security_cryptography, system_security_cryptography_x509certificates, system_security_principal

### Community 71 - "CoreLoad"
Cohesion: 0.17
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 72 - "PawnIoDependency"
Cohesion: 0.15
Nodes (11): CancellationToken, HttpClient, Task, Version, PawnIoDependency, ExpectedSigner, Id, Name (+3 more)

### Community 73 - "BatteryModePolicy"
Cohesion: 0.42
Nodes (4): BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

### Community 74 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 75 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 76 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 77 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 78 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 79 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 80 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 81 - "Dependency"
Cohesion: 0.17
Nodes (9): Version, Dependency, ExpectedSigner, Id, MayBeRemoved, Name, Purpose, SilentInstallArguments (+1 more)

### Community 82 - ".Check"
Cohesion: 0.20
Nodes (10): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+2 more)

### Community 83 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 84 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 85 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 88 - "ThemeService"
Cohesion: 0.18
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 89 - "ReleaseFile"
Cohesion: 0.33
Nodes (6): CancellationToken, HttpClient, Task, Uri, ReleaseFile, HttpClient

### Community 90 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 91 - ".Sanitised"
Cohesion: 0.33
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 92 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 93 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 94 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 95 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 96 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 97 - "README.md"
Cohesion: 0.33
Nodes (3): How it works, Questions people ask, Using it

### Community 98 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 99 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 100 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 101 - "StackPanel"
Cohesion: 0.25
Nodes (8): BannerActions, DialogActions, DriveTextStack, FnShortcutRows, PanelWindowsFaultsSubOptions, ProfileRow, SettingUpdatesAutomatic, StackPanel

### Community 102 - "ThermalSample"
Cohesion: 0.39
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 104 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 105 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 106 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 107 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 108 - "Nextcalibur 0.5.5"
Cohesion: 0.29
Nodes (5): Deep memory & resource leak eradication, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture

### Community 109 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 110 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 111 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 113 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 114 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 115 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 116 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 117 - ".ForegroundProcess"
Cohesion: 0.47
Nodes (3): DllImport, IntPtr, Name

### Community 118 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 119 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 120 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 121 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 122 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 123 - ".PowerModeControls"
Cohesion: 0.40
Nodes (4): Option, Button, IEnumerable, TextBlock

### Community 124 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 126 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 128 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 129 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 130 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 131 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 132 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 133 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 135 - "BrightnessSlider"
Cohesion: 0.50
Nodes (4): BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 136 - ".SendAsync"
Cohesion: 0.50
Nodes (3): CancellationToken, HttpRequestMessage, HttpResponseMessage

### Community 137 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 140 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 141 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

## Knowledge Gaps
- **455 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+450 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 861 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **33 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `PlanTuning`, `RunawayWatchTests`, `GpuClockReader`, `AppSettings`, `FnShortcut`, `.CheckProgram`, `WindowsFaults`, `RadioButton`, `Window`, `.Info`, `.Get`, `UserPresence`, `RoutedEventArgs`, `SystemInfo`, `.RequestRestart`, `.ApplyFnShortcuts`, `SystemMode`, `Fact`, `EcMailbox`, `CpuPowerReader`, `TrayPresence`, `Nextcalibur.Core.Configuration`, `.PlaceTourStep`, `.Get`, `.Read`, `BacklightKeyWatcher`, `UpdateService`, `MainWindow.FnKeys.cs`, `DependencyStatus`, `LedController`, `PowerOverlayService`, `.OnGpuModeChanged`, `LedEffect`, `BatteryModePolicy`, `Strings`, `Grid`, `.Check`, `.OnSystemModeChanged`, `.WireSettingsPage`, `ThemeService`, `NvidiaDriverState`, `ThermalSample`, `Nextcalibur 0.5.5`, `.OnThresholdMoved`, `TourPage`, `.ForegroundProcess`, `LedZone`, `.PowerModeControls`?**
  _High betweenness centrality (0.487) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `Keycaps`, `DriveGauge`, `MainWindow`, `BrightnessSlider`, `RadioButton`, `CpuWarnValue`, `DialogCheck`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `Button`, `ToggleButton`, `Grid`, `Border`, `StackPanel`, `Text`?**
  _High betweenness centrality (0.135) - this node is a cross-community bridge._
- **Why does `AppSettings` connect `AppSettings` to `TrayPresence`, `SystemMode`, `MainWindow`, `ProtectedStore`, `NduFix`, `WindowsFaultsTests`, `.Info`, `Strings`, `Fact`, `.Main`, `ThemeService`, `system_text_json`, `.Sanitised`?**
  _High betweenness centrality (0.070) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _455 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DotNetRuntimeDependency` be split into smaller, more focused modules?**
  _Cohesion score 0.05348101265822785 - nodes in this community are weakly interconnected._
- **Should `Authenticode` be split into smaller, more focused modules?**
  _Cohesion score 0.062111801242236024 - nodes in this community are weakly interconnected._