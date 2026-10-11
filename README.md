# Graph Report - nextcalibur-control-center  (2026-10-11)

## Corpus Check
- 165 files · ~216,924 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2785 nodes · 6014 edges · 163 communities (133 shown, 30 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 317 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `7ba99e5b`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- Authenticode
- .Get
- AppSettings
- RunawayWatchTests
- Fact
- GpuClockReader
- CpuPowerReader
- .CheckProgram
- IShellLinkW
- WindowsFaults
- ProcessPresence
- FnShortcut
- .InstallAsync
- .Warn
- WindowsFaultsTests
- Window
- RadioButton
- PlanTuning
- TrayPresence
- SystemInfo
- MachineScanRecord
- UserPresence
- Fact
- .Check
- Nextcalibur.Core.Hardware
- .Retirements
- NetworkCostTests
- .RequestRestart
- EcMailbox
- ProtectedStore
- .RunLighting
- CoolingPolicyTests
- xunit
- UpdateService
- .StartSlowTimer
- LedState
- Button
- .PlaceTourStep
- Nextcalibur.App
- SystemMode
- Dependency
- Program
- .OnSystemModeChanged
- PowerOverlayService
- DotNetRuntimeDependency
- MainWindow.FnKeys.cs
- Nextcalibur.Core.csproj
- .Get
- RestorePoint
- .Ask
- MemoryTrimmer
- SystemModeService
- analyse-autopsy.py
- .WarnAboutNewConflicts
- ResourceDictionary
- RuntimeDependencyTests
- Nextcalibur.Core.Power
- .Read
- ColourWheel
- Nextcalibur.Core.Configuration
- ToggleButton
- LedController
- FanGauge
- DonutGauge
- .Record
- InstallFolderGuard
- SegmentedBar
- LedEffect
- ElevationPolicyTests
- LogInjectionTests
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
- SmiCommandTests
- Nextcalibur.Core.Dependencies
- Hardware Protocol
- Nextcalibur 0.5.3
- .Sanitised
- .PowerModeControls
- Border
- ThemeService
- NvidiaDriverState
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- Log
- Probe
- .GetAsync
- Nextcalibur 0.5.1
- StackPanel
- DependencyStatus
- NotificationState
- ProtectedStoreElevatedTests
- Test-Ui.ps1
- Text
- Strings.cs
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Releasing, and signing
- .Pick
- .ChangeSetting
- TourPage
- Log-Graphics.ps1
- Privacy
- Security
- .OnBrightnessChanged
- LedZone
- PowerSource
- Notice
- Nextcalibur 0.5.11
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
- .Modules_are_the_released_ones
- Tools
- 0.5.7.md
- CpuWarnValue
- DialogCheck
- .ShowOverlayDialog
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

## Communities (163 total, 30 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.04
Nodes (45): AssemblyInformationalVersionAttribute, CheckBox, DeferredOverheat, HwndSource, ObservableCollection, Queue, MainWindow, DateTime (+37 more)

### Community 1 - "Authenticode"
Cohesion: 0.06
Nodes (38): CatalogInfo, Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on (+30 more)

### Community 2 - ".Get"
Cohesion: 0.08
Nodes (14): Asynchronous dispatcher & UI thread hardening, SolidColorBrush, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary (+6 more)

### Community 3 - "AppSettings"
Cohesion: 0.05
Nodes (44): DateTime, Dictionary, FnShortcut, JsonElement, List, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates (+36 more)

### Community 4 - "RunawayWatchTests"
Cohesion: 0.06
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 5 - "Fact"
Cohesion: 0.06
Nodes (23): EventArrivedEventArgs, IList, ManagementEventWatcher, BacklightKeyWatcher, GpuLoad, FanTurbo, GpuConfiguration, LedBrightness (+15 more)

### Community 6 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 7 - "CpuPowerReader"
Cohesion: 0.08
Nodes (19): DevPropKey, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, DllImport, IntPtr (+11 more)

### Community 8 - ".CheckProgram"
Cohesion: 0.07
Nodes (23): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvmlPath, IntegrityFinding (+15 more)

### Community 9 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 10 - "WindowsFaults"
Cohesion: 0.08
Nodes (34): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+26 more)

### Community 11 - "ProcessPresence"
Cohesion: 0.07
Nodes (21): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+13 more)

### Community 12 - "FnShortcut"
Cohesion: 0.08
Nodes (25): DllImport, IEnumerable, IntPtr, List, StringBuilder, FnAction, FanTurbo, KeyboardBacklight (+17 more)

### Community 13 - ".InstallAsync"
Cohesion: 0.07
Nodes (25): FileStream, IOException, Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress (+17 more)

### Community 14 - ".Warn"
Cohesion: 0.09
Nodes (11): The threat model, and what the code does about it, Application, DllImport, Mutex, App, IReadOnlyList, RegistryKey, Footprint (+3 more)

### Community 15 - "WindowsFaultsTests"
Cohesion: 0.12
Nodes (7): IReadOnlyDictionary, FaultEvaluation, Stable, Fact, WindowsFaultsTests, Dictionary, TopSharePercent

### Community 16 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 17 - "RadioButton"
Cohesion: 0.07
Nodes (37): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeDiscrete, ModeHybrid (+29 more)

### Community 18 - "PlanTuning"
Cohesion: 0.13
Nodes (16): AcDc, Floor, Original, Plan, Action, DllImport, Guid, IntPtr (+8 more)

### Community 19 - "TrayPresence"
Cohesion: 0.10
Nodes (17): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, ProcessInformation, List, Ms (+9 more)

### Community 20 - "SystemInfo"
Cohesion: 0.08
Nodes (21): MemoryStatusEx, DriveUse, INotifyPropertyChanged, DriveRow, Name, Percent, PercentText, DllImport (+13 more)

### Community 21 - "MachineScanRecord"
Cohesion: 0.07
Nodes (28): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+20 more)

### Community 22 - "UserPresence"
Cohesion: 0.14
Nodes (12): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr, MarshalAs (+4 more)

### Community 23 - "Fact"
Cohesion: 0.12
Nodes (9): Func, IEnumerable, RetirementEntry, Version, Fact, FootprintTests, ForwardCompatibilityTests, MailboxAccessTests (+1 more)

### Community 24 - ".Check"
Cohesion: 0.11
Nodes (14): CommonAce, RawSecurityDescriptor, RegistryKey, SecurityIdentifier, MailboxAccess, MailboxAvailability, AccessNotGranted, Available (+6 more)

### Community 25 - "Nextcalibur.Core.Hardware"
Cohesion: 0.11
Nodes (10): Nextcalibur.Cli, Nextcalibur.Core.Input, Nextcalibur.Core.Hardware, microsoft_win32_safehandles, system_diagnostics, system_management, system_reflection, system_runtime_interopservices (+2 more)

### Community 26 - ".Retirements"
Cohesion: 0.12
Nodes (8): dynamic, Encoding, StartupRegistration, IsEnabled, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 27 - "NetworkCostTests"
Cohesion: 0.14
Nodes (19): Bytes, Count, HttpMessageHandler, CancellationToken, Dictionary, Fact, HttpRequestMessage, HttpResponseMessage (+11 more)

### Community 28 - ".RequestRestart"
Cohesion: 0.11
Nodes (11): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, IntPtr, MarshalAs (+3 more)

### Community 29 - "EcMailbox"
Cohesion: 0.13
Nodes (13): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+5 more)

### Community 30 - "ProtectedStore"
Cohesion: 0.13
Nodes (13): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+5 more)

### Community 31 - ".RunLighting"
Cohesion: 0.08
Nodes (8): Slider, LedPower, ReloadButton, SelectAll, Action, Color, RadioButton, TextBox

### Community 32 - "CoolingPolicyTests"
Cohesion: 0.12
Nodes (16): At, Margin, DateTime, List, TimeSpan, CoolingPolicy, DegreesPerMinute, Engaged (+8 more)

### Community 33 - "xunit"
Cohesion: 0.14
Nodes (3): Nextcalibur.Core.Tests, system_xml_linq, xunit

### Community 34 - "UpdateService"
Cohesion: 0.10
Nodes (14): CancelEventArgs, EventArgs, DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService (+6 more)

### Community 35 - ".StartSlowTimer"
Cohesion: 0.12
Nodes (6): DllImport, IntPtr, Name, Func, Action, Toasts

### Community 36 - "LedState"
Cohesion: 0.15
Nodes (15): B, G, R, LedState, ActiveProfile, BrightnessPercent, Current, Effect (+7 more)

### Community 37 - "Button"
Cohesion: 0.08
Nodes (21): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, MinimiseButton (+13 more)

### Community 38 - ".PlaceTourStep"
Cohesion: 0.09
Nodes (11): SizeChangedEventArgs, KeyEventArgs, Point, RoutedEventArgs, Size, TourStep, Body, SectionName (+3 more)

### Community 39 - "Nextcalibur.App"
Cohesion: 0.09
Nodes (15): Nextcalibur.App, system_componentmodel, system_drawing, system_io, system_io_directory, system_io_ioexception, system_io_path, system_linq (+7 more)

### Community 40 - "SystemMode"
Cohesion: 0.15
Nodes (12): SmiFamily, Read, Write, ThermalProfile, SystemMode, Gaming, Office, Performance (+4 more)

### Community 41 - "Dependency"
Cohesion: 0.13
Nodes (15): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+7 more)

### Community 42 - "Program"
Cohesion: 0.16
Nodes (3): ConsoleColor, Program, ThermalReader

### Community 43 - ".OnSystemModeChanged"
Cohesion: 0.13
Nodes (5): PowerModeChangedEventArgs, ModeGaming, ModeOffice, ModePerformance, UninstallCommand

### Community 44 - "PowerOverlayService"
Cohesion: 0.16
Nodes (11): DllImport, Guid, IReadOnlyList, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, PowerModeOption (+3 more)

### Community 45 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 46 - "MainWindow.FnKeys.cs"
Cohesion: 0.20
Nodes (12): Nextcalibur.App.Controls, system_windows, system_windows_controls, system_windows_controls_button, system_windows_controls_checkbox, system_windows_controls_primitives, system_windows_input, system_windows_input_keyeventargs (+4 more)

### Community 47 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.12), System.Management (10.0.12), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk (+6 more)

### Community 48 - ".Get"
Cohesion: 0.20
Nodes (9): ArgumentNullException, FirmwareModeReading, GpuMode, Discrete, Hybrid, Uma, GpuModeService, SwitchOutcome (+1 more)

### Community 49 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 50 - ".Ask"
Cohesion: 0.25
Nodes (10): HttpStatusCode, CancellationToken, Fact, HttpRequestMessage, HttpResponseMessage, InlineData, Task, Theory (+2 more)

### Community 51 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 52 - "SystemModeService"
Cohesion: 0.23
Nodes (7): Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, SystemModeService

### Community 53 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 54 - ".WarnAboutNewConflicts"
Cohesion: 0.17
Nodes (12): Problem, Detail, Dictionary, KeyEventArgs, ShortcutProblem, Duplicate, NotAKey, ReservedBySystem (+4 more)

### Community 55 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 56 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 57 - "Nextcalibur.Core.Power"
Cohesion: 0.14
Nodes (8): Nextcalibur.Core.Power, InvalidOperationException, microsoft_win32, Theme, Dark, Light, UnsafeModeException, system_security

### Community 58 - ".Read"
Cohesion: 0.30
Nodes (3): Fact, List, ProtectedStoreTests

### Community 59 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 60 - "Nextcalibur.Core.Configuration"
Cohesion: 0.18
Nodes (5): Nextcalibur.Core.Configuration, Nextcalibur.Core.Tests.Attacks, system_text_json, system_text_json_serialization, system_text_regularexpressions

### Community 61 - "ToggleButton"
Cohesion: 0.12
Nodes (16): OverheatWarningToggle, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost, SettingFixWidgets, SettingFnShortcuts (+8 more)

### Community 62 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 63 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 64 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 65 - ".Record"
Cohesion: 0.18
Nodes (7): IReadOnlyDictionary, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Fact, AuditLogTests

### Community 66 - "InstallFolderGuard"
Cohesion: 0.29
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 67 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 68 - "LedEffect"
Cohesion: 0.14
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 69 - "ElevationPolicyTests"
Cohesion: 0.23
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 70 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 71 - "Nextcalibur.Core.Security"
Cohesion: 0.27
Nodes (5): Nextcalibur.Core.Security, system_security_accesscontrol, system_security_cryptography, system_security_cryptography_x509certificates, system_security_principal

### Community 72 - "CoreLoad"
Cohesion: 0.17
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 73 - "PawnIoDependency"
Cohesion: 0.15
Nodes (11): CancellationToken, HttpClient, Task, Version, PawnIoDependency, ExpectedSigner, Id, Name (+3 more)

### Community 74 - "BatteryModePolicy"
Cohesion: 0.42
Nodes (4): BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

### Community 75 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 76 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 77 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 78 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 79 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 80 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 81 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 82 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 83 - "Nextcalibur.Core.Dependencies"
Cohesion: 0.35
Nodes (4): Nextcalibur.Core.Dependencies, system_collections_concurrent, system_net, system_net_http

### Community 84 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 85 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 86 - ".Sanitised"
Cohesion: 0.29
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 87 - ".PowerModeControls"
Cohesion: 0.22
Nodes (4): Option, Button, IEnumerable, TextBlock

### Community 88 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 89 - "ThemeService"
Cohesion: 0.20
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 90 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 91 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

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
Cohesion: 0.33
Nodes (3): How it works, Questions people ask, Using it

### Community 96 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 98 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 99 - ".GetAsync"
Cohesion: 0.29
Nodes (7): Answer, ConcurrentDictionary, CancellationToken, HttpClient, Task, Answer, Conditional

### Community 100 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 101 - "StackPanel"
Cohesion: 0.25
Nodes (8): BannerActions, DialogActions, DriveTextStack, FnShortcutRows, PanelWindowsFaultsSubOptions, ProfileRow, SettingUpdatesAutomatic, StackPanel

### Community 102 - "DependencyStatus"
Cohesion: 0.29
Nodes (4): DependencyStatus, Missing, NeedsAction, Outdated

### Community 103 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 104 - "ProtectedStoreElevatedTests"
Cohesion: 0.32
Nodes (4): Fact, SecurityIdentifier, ProtectedStoreElevatedTests, Elevated

### Community 105 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 106 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 107 - "Strings.cs"
Cohesion: 0.33
Nodes (5): Nextcalibur.Core, Func, Words, Resolver, system_globalization

### Community 108 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

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

### Community 117 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 118 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 119 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 120 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 121 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 122 - "SmiSubsystem"
Cohesion: 0.40
Nodes (5): SmiSubsystem, DisplayMode, Led, Profile, Thermal

### Community 123 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 125 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 126 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 127 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 128 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 129 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 130 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 132 - ".TryParseRgb"
Cohesion: 0.50
Nodes (3): B, G, R

### Community 133 - ".Modules_are_the_released_ones"
Cohesion: 0.50
Nodes (3): InlineData, Theory, PawnIoModuleTests

### Community 134 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 137 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 138 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

## Knowledge Gaps
- **454 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+449 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 861 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **30 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `.Get`, `AppSettings`, `RunawayWatchTests`, `Fact`, `GpuClockReader`, `CpuPowerReader`, `.CheckProgram`, `IShellLinkW`, `WindowsFaults`, `ProcessPresence`, `.ShowOverlayDialog`, `FnShortcut`, `Window`, `RadioButton`, `PlanTuning`, `TrayPresence`, `SystemInfo`, `MachineScanRecord`, `UserPresence`, `Nextcalibur.Core.Hardware`, `.RequestRestart`, `EcMailbox`, `.RunLighting`, `CoolingPolicyTests`, `UpdateService`, `.StartSlowTimer`, `Button`, `.PlaceTourStep`, `Nextcalibur.App`, `SystemMode`, `Program`, `.OnSystemModeChanged`, `PowerOverlayService`, `MainWindow.FnKeys.cs`, `.Get`, `SystemModeService`, `.WarnAboutNewConflicts`, `LedController`, `.Record`, `LedEffect`, `BatteryModePolicy`, `Strings`, `Grid`, `.PowerModeControls`, `ThemeService`, `NvidiaDriverState`, `DependencyStatus`, `.ChangeSetting`, `TourPage`, `.OnBrightnessChanged`, `LedZone`?**
  _High betweenness centrality (0.466) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `.Get`, `CpuWarnValue`, `DialogCheck`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `RadioButton`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `.RunLighting`, `Button`, `.OnSystemModeChanged`, `ToggleButton`, `Grid`, `Border`, `StackPanel`, `Text`, `.OnBrightnessChanged`, `Keycaps`, `DriveGauge`?**
  _High betweenness centrality (0.114) - this node is a cross-community bridge._
- **Why does `AppSettings` connect `AppSettings` to `MainWindow`, `UpdateService`, `SystemMode`, `.Warn`, `WindowsFaultsTests`, `Strings`, `TrayPresence`, `.Sanitised`, `Fact`, `ThemeService`, `.Read`, `Nextcalibur.Core.Configuration`?**
  _High betweenness centrality (0.078) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _454 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.03759398496240601 - nodes in this community are weakly interconnected._
- **Should `Authenticode` be split into smaller, more focused modules?**
  _Cohesion score 0.060764587525150904 - nodes in this community are weakly interconnected._