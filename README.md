# Graph Report - nextcalibur-control-center  (2026-10-11)

## Corpus Check
- 166 files · ~218,321 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2792 nodes · 6021 edges · 170 communities (136 shown, 34 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 317 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `f22a476c`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- .CheckProgram
- Authenticode
- RunawayWatchTests
- MainWindow
- GpuClockReader
- UpdateService
- IShellLinkW
- WindowsFaults
- FnShortcut
- TrayPresence
- Window
- PlanTuning
- CpuPowerReader
- SystemInfo
- RadioButton
- AppSettings
- UserPresence
- xunit
- .Warn
- .RequestRestart
- .OfferDependency
- WindowsFaultsTests
- Fact
- Button
- NetworkCostTests
- Nextcalibur.Core.Hardware
- .Get
- ProcessPresence
- SystemModeService
- ProtectedStore
- CoolingPolicyTests
- Nextcalibur.Core.Configuration
- .WarnAboutNewConflicts
- .Info
- Elevation
- Fact
- .PlaceTourStep
- LedState
- MachineScanRecord
- Program
- EcMailbox
- GpuModeService
- Dependency
- .Check
- .OfferUpdate
- DotNetRuntimeDependency
- Fact
- MainWindow.FnKeys.cs
- Nextcalibur.Core.csproj
- SystemMode
- RestorePoint
- BacklightKeyWatcher
- .Ask
- MemoryTrimmer
- .CalculateThreadShare
- analyse-autopsy.py
- system_text_json
- ToggleButton
- ResourceDictionary
- RuntimeDependencyTests
- PowerOverlayService
- .Read
- .OnLoaded
- ColourWheel
- DevicePowerState
- .PowerModeControls
- LedController
- FanGauge
- Nextcalibur.Core.Power
- HostileFilesTests
- .OnPowerSourceMayHaveChanged
- DonutGauge
- SegmentedBar
- LedEffect
- SettingsTests.cs
- .HasAccess
- .Record
- CoreLoad
- ThemeService
- PendingRestartInfo
- PawnIoDependency
- StringsDictionaryTests
- ValueConverters.cs
- Nextcalibur 0.5.2
- Nextcalibur Control Center
- Strings
- .RunLighting
- Grid
- .Check
- SmiCommandTests
- Hardware Protocol
- Nextcalibur 0.5.3
- .WireSettingsPage
- .Sanitised
- MachineWideGate
- .RefreshBanner
- Border
- NvidiaDriverState
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- .Migrate
- ProfileFiles
- Probe
- .GetAsync
- Nextcalibur 0.5.1
- StackPanel
- .OnThemeChanged
- NotificationState
- ProtectedStoreElevatedTests
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- .Pick
- TourPage
- NduFix
- Log-Graphics.ps1
- Privacy
- Security
- .OnBrightnessChanged
- LedZone
- PowerSource
- Notice
- Nextcalibur 0.5.11
- AppIdentity.cs
- .Set
- SmiSubsystem
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- .ArrangeOverride
- .TryParseRgb
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

## Communities (170 total, 34 thin omitted)

### Community 0 - ".CheckProgram"
Cohesion: 0.05
Nodes (32): Lazy, ProcessModule, FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Dictionary, HashSet (+24 more)

### Community 1 - "Authenticode"
Cohesion: 0.06
Nodes (38): CatalogInfo, Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on (+30 more)

### Community 2 - "RunawayWatchTests"
Cohesion: 0.06
Nodes (38): Cpu, CpuMs, DllImport, IntPtr, Name, DateTime, Dictionary, Func (+30 more)

### Community 3 - "MainWindow"
Cohesion: 0.04
Nodes (39): CheckBox, DeferredOverheat, HwndSource, ObservableCollection, Queue, MainWindow, DateTime, CoolingWantsReadings (+31 more)

### Community 4 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 5 - "UpdateService"
Cohesion: 0.05
Nodes (36): FileStream, Message, Ok, RestartRequired, DateTime, DispatcherTimer, HashSet, Task (+28 more)

### Community 6 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 7 - "WindowsFaults"
Cohesion: 0.08
Nodes (34): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+26 more)

### Community 8 - "FnShortcut"
Cohesion: 0.08
Nodes (25): DllImport, IEnumerable, IntPtr, List, StringBuilder, FnAction, FanTurbo, KeyboardBacklight (+17 more)

### Community 9 - "TrayPresence"
Cohesion: 0.08
Nodes (18): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, ProcessInformation, EventArgs, List (+10 more)

### Community 10 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 11 - "PlanTuning"
Cohesion: 0.13
Nodes (16): AcDc, Floor, Original, Plan, Action, DllImport, Guid, IntPtr (+8 more)

### Community 12 - "CpuPowerReader"
Cohesion: 0.09
Nodes (21): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+13 more)

### Community 13 - "SystemInfo"
Cohesion: 0.08
Nodes (21): MemoryStatusEx, DriveUse, INotifyPropertyChanged, DriveRow, Name, Percent, PercentText, DllImport (+13 more)

### Community 14 - "RadioButton"
Cohesion: 0.07
Nodes (34): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeGaming, ModeOffice (+26 more)

### Community 15 - "AppSettings"
Cohesion: 0.05
Nodes (35): FnShortcut, JsonElement, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoCooling, AutoFanTurbo, AutoInstallUpdates (+27 more)

### Community 16 - "UserPresence"
Cohesion: 0.13
Nodes (12): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr, MarshalAs (+4 more)

### Community 17 - "xunit"
Cohesion: 0.12
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Security, system_security_cryptography_x509certificates, system_text_regularexpressions, system_xml_linq, xunit

### Community 18 - ".Warn"
Cohesion: 0.12
Nodes (8): The threat model, and what the code does about it, Application, DllImport, Mutex, App, RegistryKey, Footprint, STAThread

### Community 19 - ".RequestRestart"
Cohesion: 0.09
Nodes (12): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, BannerRestartButton, DllImport, EventArgs (+4 more)

### Community 20 - ".OfferDependency"
Cohesion: 0.11
Nodes (12): CancelEventArgs, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary, IReadOnlyDictionary (+4 more)

### Community 21 - "WindowsFaultsTests"
Cohesion: 0.17
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 22 - "Fact"
Cohesion: 0.16
Nodes (9): Func, IEnumerable, IReadOnlyList, RetirementEntry, Trace, Version, Fact, FootprintTests (+1 more)

### Community 23 - "Button"
Cohesion: 0.06
Nodes (23): IsChecked, BannerDismissButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, HideToTrayButton, MinimiseButton (+15 more)

### Community 24 - "NetworkCostTests"
Cohesion: 0.14
Nodes (19): Bytes, Count, HttpMessageHandler, CancellationToken, Dictionary, Fact, HttpRequestMessage, HttpResponseMessage (+11 more)

### Community 25 - "Nextcalibur.Core.Hardware"
Cohesion: 0.12
Nodes (8): Nextcalibur.Cli, Nextcalibur.Core.Input, Nextcalibur.Core.Hardware, microsoft_win32_safehandles, system_diagnostics, system_management, system_runtime_interopservices, system_text

### Community 26 - ".Get"
Cohesion: 0.11
Nodes (3): Func, MessageBoxButton, MessageBoxResult

### Community 27 - "ProcessPresence"
Cohesion: 0.12
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 28 - "SystemModeService"
Cohesion: 0.14
Nodes (12): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, SystemModeService (+4 more)

### Community 29 - "ProtectedStore"
Cohesion: 0.13
Nodes (13): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+5 more)

### Community 30 - "CoolingPolicyTests"
Cohesion: 0.12
Nodes (16): At, Margin, DateTime, List, TimeSpan, CoolingPolicy, DegreesPerMinute, Engaged (+8 more)

### Community 31 - "Nextcalibur.Core.Configuration"
Cohesion: 0.10
Nodes (14): Nextcalibur.App, Nextcalibur.Core, Nextcalibur.Core.Configuration, system_componentmodel, system_drawing, system_globalization, system_io, system_linq (+6 more)

### Community 32 - ".WarnAboutNewConflicts"
Cohesion: 0.11
Nodes (16): Problem, Detail, Dictionary, DllImport, IntPtr, KeyEventArgs, Action, Toasts (+8 more)

### Community 33 - ".Info"
Cohesion: 0.14
Nodes (9): Exception, Log, Folder, Action, Fact, InlineData, Regex, Theory (+1 more)

### Community 34 - "Elevation"
Cohesion: 0.14
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 35 - "Fact"
Cohesion: 0.17
Nodes (8): IList, GpuLoad, GpuConfiguration, Fact, GpuModeTests, Discrete, Hybrid, HardwareSupportTests

### Community 36 - ".PlaceTourStep"
Cohesion: 0.09
Nodes (12): SizeChangedEventArgs, FrameworkElement, KeyEventArgs, Point, RoutedEventArgs, Size, TourStep, Body (+4 more)

### Community 37 - "LedState"
Cohesion: 0.15
Nodes (15): B, G, R, LedState, ActiveProfile, BrightnessPercent, Current, Effect (+7 more)

### Community 38 - "MachineScanRecord"
Cohesion: 0.11
Nodes (18): DateTime, JsonSerializerOptions, List, MachineScan, RecordPath, MachineScanRecord, BiosVendor, BiosVersion (+10 more)

### Community 39 - "Program"
Cohesion: 0.15
Nodes (5): ConsoleColor, Program, DateTimeOffset, ThermalReader, ThermalSample

### Community 40 - "EcMailbox"
Cohesion: 0.17
Nodes (10): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+2 more)

### Community 41 - "GpuModeService"
Cohesion: 0.16
Nodes (10): ArgumentNullException, FirmwareModeReading, GpuMode, Discrete, Hybrid, Uma, GpuModeService, SwitchOutcome (+2 more)

### Community 42 - "Dependency"
Cohesion: 0.13
Nodes (15): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+7 more)

### Community 43 - ".Check"
Cohesion: 0.15
Nodes (8): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported, MailboxAccessTests

### Community 44 - ".OfferUpdate"
Cohesion: 0.13
Nodes (7): Asynchronous dispatcher & UI thread hardening, Deep memory & resource leak eradication, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Task, IProgress

### Community 45 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 46 - "Fact"
Cohesion: 0.12
Nodes (13): IReadOnlyList, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact (+5 more)

### Community 47 - "MainWindow.FnKeys.cs"
Cohesion: 0.20
Nodes (12): Nextcalibur.App.Controls, system_windows, system_windows_controls, system_windows_controls_button, system_windows_controls_checkbox, system_windows_controls_primitives, system_windows_input, system_windows_input_keyeventargs (+4 more)

### Community 48 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.12), System.Management (10.0.12), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk (+6 more)

### Community 49 - "SystemMode"
Cohesion: 0.16
Nodes (12): SmiFamily, Read, Write, ThermalProfile, SystemMode, Gaming, Office, Performance (+4 more)

### Community 50 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 51 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 52 - ".Ask"
Cohesion: 0.25
Nodes (10): HttpStatusCode, CancellationToken, Fact, HttpRequestMessage, HttpResponseMessage, InlineData, Task, Theory (+2 more)

### Community 53 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 54 - ".CalculateThreadShare"
Cohesion: 0.15
Nodes (9): IReadOnlyDictionary, Stable, Dictionary, Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests (+1 more)

### Community 55 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 56 - "system_text_json"
Cohesion: 0.21
Nodes (7): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_text_json, system_text_json_serialization

### Community 57 - "ToggleButton"
Cohesion: 0.11
Nodes (17): OverheatWarningToggle, SelectAll, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost, SettingFixWidgets (+9 more)

### Community 58 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 59 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 60 - "PowerOverlayService"
Cohesion: 0.23
Nodes (7): DllImport, Guid, PowerModeOption, PowerOverlays, All, PowerOverlayService, IdempotenceTests

### Community 61 - ".Read"
Cohesion: 0.30
Nodes (3): Fact, List, ProtectedStoreTests

### Community 63 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 64 - "DevicePowerState"
Cohesion: 0.25
Nodes (6): DevPropKey, Discrete GPU sleep protection in Hybrid mode, DllImport, Guid, DevicePowerState, DevPropKey

### Community 65 - ".PowerModeControls"
Cohesion: 0.14
Nodes (5): Option, Button, IEnumerable, RadioButton, TextBlock

### Community 66 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 67 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 68 - "Nextcalibur.Core.Power"
Cohesion: 0.16
Nodes (6): Nextcalibur.Core.Power, microsoft_win32, Theme, Dark, Light, system_security

### Community 69 - "HostileFilesTests"
Cohesion: 0.27
Nodes (5): IOException, Fact, InlineData, Theory, HostileFilesTests

### Community 70 - ".OnPowerSourceMayHaveChanged"
Cohesion: 0.34
Nodes (5): PowerModeChangedEventArgs, BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

### Community 71 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 72 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 73 - "LedEffect"
Cohesion: 0.14
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 74 - "SettingsTests.cs"
Cohesion: 0.20
Nodes (7): system_reflection, system_security_accesscontrol, system_security_cryptography, system_security_principal, InlineData, Theory, PawnIoModuleTests

### Community 75 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 76 - ".Record"
Cohesion: 0.20
Nodes (7): IReadOnlyDictionary, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Fact, AuditLogTests

### Community 77 - "CoreLoad"
Cohesion: 0.17
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 78 - "ThemeService"
Cohesion: 0.17
Nodes (5): Color, Theme, ThemeService, Preference, Resolved

### Community 79 - "PendingRestartInfo"
Cohesion: 0.19
Nodes (7): DateTime, Dictionary, List, PendingRestartInfo, BootTimeUtc, ReasonArguments, Reasons

### Community 80 - "PawnIoDependency"
Cohesion: 0.15
Nodes (11): CancellationToken, HttpClient, Task, Version, PawnIoDependency, ExpectedSigner, Id, Name (+3 more)

### Community 81 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 82 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 83 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 84 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 85 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 86 - ".RunLighting"
Cohesion: 0.20
Nodes (4): Slider, LedPower, Action, TextBox

### Community 87 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 88 - ".Check"
Cohesion: 0.20
Nodes (10): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+2 more)

### Community 89 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 90 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 91 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 93 - ".Sanitised"
Cohesion: 0.29
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 94 - "MachineWideGate"
Cohesion: 0.20
Nodes (6): Mutex, TimeSpan, MachineWideGate, system_threading, Task, MachineWideGateTests

### Community 96 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 97 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 98 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 99 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 100 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 101 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 102 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 105 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 106 - ".GetAsync"
Cohesion: 0.29
Nodes (7): Answer, ConcurrentDictionary, CancellationToken, HttpClient, Task, Answer, Conditional

### Community 107 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 108 - "StackPanel"
Cohesion: 0.25
Nodes (8): BannerActions, DialogActions, DriveTextStack, FnShortcutRows, PanelWindowsFaultsSubOptions, ProfileRow, SettingUpdatesAutomatic, StackPanel

### Community 109 - ".OnThemeChanged"
Cohesion: 0.25
Nodes (7): ThemeDark, ThemeLight, ThemeSystem, ThemePreference, Dark, Light, System

### Community 110 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 111 - "ProtectedStoreElevatedTests"
Cohesion: 0.32
Nodes (4): Fact, SecurityIdentifier, ProtectedStoreElevatedTests, Elevated

### Community 112 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 113 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 114 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 115 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 116 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 117 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 118 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 119 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 121 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 122 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 123 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 124 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 125 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 126 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 127 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 128 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 129 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 131 - "SmiSubsystem"
Cohesion: 0.40
Nodes (5): SmiSubsystem, DisplayMode, Led, Profile, Thermal

### Community 132 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 134 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 135 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 136 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 137 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 138 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 139 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 141 - ".TryParseRgb"
Cohesion: 0.50
Nodes (3): B, G, R

### Community 142 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 145 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 146 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

## Knowledge Gaps
- **459 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+454 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 867 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **34 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `.CheckProgram`, `RunawayWatchTests`, `GpuClockReader`, `UpdateService`, `IShellLinkW`, `WindowsFaults`, `FnShortcut`, `TrayPresence`, `Window`, `PlanTuning`, `CpuPowerReader`, `SystemInfo`, `RadioButton`, `AppSettings`, `UserPresence`, `.RequestRestart`, `.OfferDependency`, `Button`, `.Get`, `ProcessPresence`, `SystemModeService`, `CoolingPolicyTests`, `Nextcalibur.Core.Configuration`, `.WarnAboutNewConflicts`, `.Info`, `Fact`, `.PlaceTourStep`, `Program`, `EcMailbox`, `GpuModeService`, `.OfferUpdate`, `MainWindow.FnKeys.cs`, `SystemMode`, `BacklightKeyWatcher`, `ToggleButton`, `PowerOverlayService`, `.OnLoaded`, `.PowerModeControls`, `LedController`, `.OnPowerSourceMayHaveChanged`, `LedEffect`, `ThemeService`, `Strings`, `.RunLighting`, `Grid`, `.Check`, `.WireSettingsPage`, `.RefreshBanner`, `NvidiaDriverState`, `.OnThemeChanged`, `TourPage`, `.OnBrightnessChanged`, `LedZone`?**
  _High betweenness centrality (0.465) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Keycaps`, `DriveGauge`, `RadioButton`, `CpuWarnValue`, `DialogCheck`, `.RequestRestart`, `.OfferDependency`, `ColGauge`, `DialogProgress`, `Button`, `DriveDot`, `EffectPanel`, `DriveList`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `ToggleButton`, `.RunLighting`, `Grid`, `Border`, `StackPanel`, `.OnThemeChanged`, `Text`, `.OnBrightnessChanged`?**
  _High betweenness centrality (0.113) - this node is a cross-community bridge._
- **Why does `AppSettings` connect `AppSettings` to `MainWindow`, `HostileFilesTests`, `.Migrate`, `TrayPresence`, `.OnThemeChanged`, `ThemeService`, `PendingRestartInfo`, `SystemMode`, `.Read`, `Strings`, `Fact`, `WindowsFaultsTests`, `system_text_json`, `NduFix`, `.Sanitised`?**
  _High betweenness centrality (0.081) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _459 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `.CheckProgram` be split into smaller, more focused modules?**
  _Cohesion score 0.05017543859649123 - nodes in this community are weakly interconnected._
- **Should `Authenticode` be split into smaller, more focused modules?**
  _Cohesion score 0.060764587525150904 - nodes in this community are weakly interconnected._