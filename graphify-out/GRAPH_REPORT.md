# Graph Report - nextcalibur-control-center  (2026-10-13)

## Corpus Check
- 170 files · ~222,121 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2865 nodes · 6211 edges · 179 communities (148 shown, 31 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 334 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `41aa9950`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- RunawayWatchTests
- GpuClockReader
- IShellLinkW
- .Get
- FnShortcut
- PlanTuning
- Window
- RadioButton
- .CheckProgram
- UserPresence
- TrayPresence
- SystemInfo
- AppSettings
- CpuPowerReader
- .ApplyFnShortcuts
- Button
- WindowsFaults
- Nextcalibur.Core.Hardware
- Fact
- .OnGpuModeChanged
- WindowsFaultsTests
- NetworkCostTests
- UpdateService
- ProcessPresence
- xunit
- .Main
- CoolingPolicyTests
- ProtectedStore
- .RequestRestart
- SystemModeService
- .Info
- .Get
- .InstallAsync
- ProgramRule
- Elevation
- Fact
- .PlaceTourStep
- LedState
- Dependency
- MachineScanRecord
- Fact
- .SignatureIsValid
- Program
- Authenticode
- Nextcalibur.Core.Security
- .Check
- MainWindow.FnKeys.cs
- PowerOverlayService
- .Ask
- ToggleButton
- .Read
- DotNetRuntimeDependency
- .Execute
- Nextcalibur.App
- .OfferDependency
- Nextcalibur.Core.csproj
- RestorePoint
- BacklightKeyWatcher
- analyse-autopsy.py
- MemoryTrimmer
- system_diagnostics
- ResourceDictionary
- .MayStartWithoutPrompt
- RuntimeDependencyTests
- SystemMode
- .Read
- ColourWheel
- Nextcalibur.Core.Configuration
- EcMailbox
- CoreLoad
- LedController
- FanGauge
- DevicePowerState
- HostileFilesTests
- DonutGauge
- SegmentedBar
- ProcessMetrics
- LedEffect
- .HasAccess
- .Record
- LogInjectionTests
- Grid
- .WireSettingsPage
- InstallFolderGuard
- BatteryModePolicy
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur Control Center
- Strings
- PendingRestartInfo
- Hardware Protocol
- Nextcalibur 0.5.3
- Nextcalibur 0.5.4
- .Sanitised
- SupportVerdict
- Border
- .RefreshProgramRuleRows
- ThemeService
- NvidiaDriverState
- .CalculateThreadShare
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- StackPanel
- .Migrate
- PawnIoDependency
- .IsSystemFileSignedBy
- ProfileFiles
- Probe
- .GetAsync
- Nextcalibur 0.5.1
- IntegrityKind
- ProtectedStoreElevatedTests
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
- MachineWideGate
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
- Nextcalibur 0.6.1
- .ArrangeOverride
- .TryParseRgb
- .Modules_are_the_released_ones
- Tools
- 0.5.7.md
- CpuWarnValue
- DialogCheck
- ProgramRuleMode
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
1. `MainWindow` - 308 edges
2. `Window` - 175 edges
3. `AppSettings` - 73 edges
4. `Nextcalibur.Core.Hardware` - 53 edges
5. `WindowsFaultsTests` - 40 edges
6. `RadioButton` - 39 edges
7. `TextBlock` - 36 edges
8. `Nextcalibur.Core.Configuration` - 36 edges
9. `SystemMode` - 34 edges
10. `WindowsFaults` - 33 edges

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

## Communities (179 total, 31 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.04
Nodes (47): CheckBox, ComboBox, DeferredOverheat, HwndSource, ObservableCollection, Queue, MainWindow, DateTime (+39 more)

### Community 1 - "RunawayWatchTests"
Cohesion: 0.06
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 2 - "GpuClockReader"
Cohesion: 0.08
Nodes (22): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+14 more)

### Community 3 - "IShellLinkW"
Cohesion: 0.06
Nodes (16): IPropertyStore, PropertyKey, PropVariant, Slider, DllImport, Guid, IEnumerable, IntPtr (+8 more)

### Community 4 - ".Get"
Cohesion: 0.08
Nodes (7): AssemblyInformationalVersionAttribute, SolidColorBrush, Func, MessageBoxButton, MessageBoxResult, Task, TextBlock

### Community 5 - "FnShortcut"
Cohesion: 0.08
Nodes (25): DllImport, IEnumerable, IntPtr, List, StringBuilder, FnAction, FanTurbo, KeyboardBacklight (+17 more)

### Community 6 - "PlanTuning"
Cohesion: 0.13
Nodes (18): AcDc, Floor, Marker, Original, Setting, Action, DllImport, Guid (+10 more)

### Community 7 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 8 - "RadioButton"
Cohesion: 0.07
Nodes (37): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeGaming, ModeOffice (+29 more)

### Community 9 - ".CheckProgram"
Cohesion: 0.10
Nodes (15): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvmlPath, IntegrityFinding (+7 more)

### Community 10 - "UserPresence"
Cohesion: 0.10
Nodes (20): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr, MarshalAs (+12 more)

### Community 11 - "TrayPresence"
Cohesion: 0.10
Nodes (17): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, ProcessInformation, List, Ms (+9 more)

### Community 12 - "SystemInfo"
Cohesion: 0.08
Nodes (21): MemoryStatusEx, DriveUse, INotifyPropertyChanged, DriveRow, Name, Percent, PercentText, DllImport (+13 more)

### Community 13 - "AppSettings"
Cohesion: 0.05
Nodes (38): FnShortcut, JsonElement, ProgramRule, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoCooling, AutoFanTurbo (+30 more)

### Community 14 - "CpuPowerReader"
Cohesion: 0.11
Nodes (14): Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, DllImport, IntPtr, SafeFileHandle (+6 more)

### Community 15 - ".ApplyFnShortcuts"
Cohesion: 0.09
Nodes (14): Problem, Detail, Dictionary, DllImport, IntPtr, KeyEventArgs, ShortcutProblem, Duplicate (+6 more)

### Community 16 - "Button"
Cohesion: 0.07
Nodes (25): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, HideToTrayButton, MinimiseButton (+17 more)

### Community 17 - "WindowsFaults"
Cohesion: 0.11
Nodes (21): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, DateTime, DllImport, Func (+13 more)

### Community 18 - "Nextcalibur.Core.Hardware"
Cohesion: 0.12
Nodes (8): Nextcalibur.Core.Power, Nextcalibur.Core.Hardware, microsoft_win32, microsoft_win32_safehandles, system_management, system_reflection, system_runtime_interopservices, system_security

### Community 19 - "Fact"
Cohesion: 0.15
Nodes (9): Func, IEnumerable, IReadOnlyList, RetirementEntry, Trace, Version, Fact, FootprintTests (+1 more)

### Community 20 - ".OnGpuModeChanged"
Cohesion: 0.10
Nodes (12): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary, IReadOnlyDictionary, Task (+4 more)

### Community 21 - "WindowsFaultsTests"
Cohesion: 0.18
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 22 - "NetworkCostTests"
Cohesion: 0.14
Nodes (19): Bytes, Count, HttpMessageHandler, CancellationToken, Dictionary, Fact, HttpRequestMessage, HttpResponseMessage (+11 more)

### Community 23 - "UpdateService"
Cohesion: 0.09
Nodes (15): CancelEventArgs, EventArgs, DateTime, DispatcherTimer, HashSet, IProgress, Task, TimeSpan (+7 more)

### Community 24 - "ProcessPresence"
Cohesion: 0.12
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 25 - "xunit"
Cohesion: 0.12
Nodes (4): Nextcalibur.Core.Tests, system_text_regularexpressions, system_xml_linq, xunit

### Community 26 - ".Main"
Cohesion: 0.12
Nodes (8): The threat model, and what the code does about it, Application, DllImport, Mutex, App, RegistryKey, Footprint, STAThread

### Community 27 - "CoolingPolicyTests"
Cohesion: 0.12
Nodes (16): At, Margin, DateTime, List, TimeSpan, CoolingPolicy, DegreesPerMinute, Engaged (+8 more)

### Community 28 - "ProtectedStore"
Cohesion: 0.13
Nodes (13): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+5 more)

### Community 29 - ".RequestRestart"
Cohesion: 0.12
Nodes (10): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, IntPtr, MarshalAs (+2 more)

### Community 30 - "SystemModeService"
Cohesion: 0.15
Nodes (11): InvalidOperationException, Ours, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList (+3 more)

### Community 31 - ".Info"
Cohesion: 0.13
Nodes (4): Exception, Log, Folder, NduFix

### Community 32 - ".Get"
Cohesion: 0.13
Nodes (13): ArgumentNullException, FirmwareModeReading, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma, GpuModeService (+5 more)

### Community 33 - ".InstallAsync"
Cohesion: 0.09
Nodes (20): FileStream, Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList (+12 more)

### Community 34 - "ProgramRule"
Cohesion: 0.14
Nodes (16): GeneratedRegex, DateTime, IEnumerable, IReadOnlyDictionary, Regex, ProgramRule, Enabled, Mode (+8 more)

### Community 35 - "Elevation"
Cohesion: 0.14
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 36 - "Fact"
Cohesion: 0.17
Nodes (7): IList, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 37 - ".PlaceTourStep"
Cohesion: 0.09
Nodes (12): SizeChangedEventArgs, FrameworkElement, KeyEventArgs, Point, RoutedEventArgs, Size, TourStep, Body (+4 more)

### Community 38 - "LedState"
Cohesion: 0.15
Nodes (15): B, G, R, LedState, ActiveProfile, BrightnessPercent, Current, Effect (+7 more)

### Community 39 - "Dependency"
Cohesion: 0.11
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 40 - "MachineScanRecord"
Cohesion: 0.11
Nodes (18): DateTime, JsonSerializerOptions, List, MachineScan, RecordPath, MachineScanRecord, BiosVendor, BiosVersion (+10 more)

### Community 41 - "Fact"
Cohesion: 0.10
Nodes (15): IReadOnlyList, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact (+7 more)

### Community 42 - ".SignatureIsValid"
Cohesion: 0.15
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 43 - "Program"
Cohesion: 0.15
Nodes (5): ConsoleColor, Program, DateTimeOffset, ThermalReader, ThermalSample

### Community 44 - "Authenticode"
Cohesion: 0.23
Nodes (15): CatalogInfo, Security, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle (+7 more)

### Community 45 - "Nextcalibur.Core.Security"
Cohesion: 0.16
Nodes (8): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_security_cryptography_x509certificates, system_text_json

### Community 46 - ".Check"
Cohesion: 0.14
Nodes (9): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported, IdempotenceTests (+1 more)

### Community 47 - "MainWindow.FnKeys.cs"
Cohesion: 0.19
Nodes (13): Nextcalibur.App.Controls, system_windows, system_windows_controls, system_windows_controls_button, system_windows_controls_checkbox, system_windows_controls_combobox, system_windows_controls_primitives, system_windows_input (+5 more)

### Community 48 - "PowerOverlayService"
Cohesion: 0.17
Nodes (9): Option, Button, IEnumerable, DllImport, Guid, PowerModeOption, PowerOverlays, All (+1 more)

### Community 49 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 50 - "ToggleButton"
Cohesion: 0.09
Nodes (19): LedPower, OverheatWarningToggle, SelectAll, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost (+11 more)

### Community 51 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 52 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 53 - ".Execute"
Cohesion: 0.18
Nodes (7): ArgumentException, Func, SmiCommand, Fact, InlineData, Theory, SmiCommandTests

### Community 54 - "Nextcalibur.App"
Cohesion: 0.11
Nodes (13): Nextcalibur.App, system_componentmodel, system_io, system_io_directory, system_io_ioexception, system_io_path, system_linq, system_runtime_compilerservices (+5 more)

### Community 55 - ".OfferDependency"
Cohesion: 0.15
Nodes (7): Asynchronous dispatcher & UI thread hardening, Action, Toasts, DependencyStatus, Missing, NeedsAction, Outdated

### Community 56 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.12), System.Management (10.0.12), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 57 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 58 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 59 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 60 - "MemoryTrimmer"
Cohesion: 0.14
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 61 - "system_diagnostics"
Cohesion: 0.17
Nodes (7): Nextcalibur.Cli, Nextcalibur.Core.Input, system_diagnostics, system_security_accesscontrol, system_security_cryptography, system_security_principal, system_text

### Community 62 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 63 - ".MayStartWithoutPrompt"
Cohesion: 0.22
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 64 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 65 - "SystemMode"
Cohesion: 0.24
Nodes (5): PowerModeChangedEventArgs, SystemMode, Gaming, Office, Performance

### Community 66 - ".Read"
Cohesion: 0.30
Nodes (3): Fact, List, ProtectedStoreTests

### Community 67 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 68 - "Nextcalibur.Core.Configuration"
Cohesion: 0.14
Nodes (9): Nextcalibur.Core, Nextcalibur.Core.Configuration, Theme, Dark, Light, system_drawing, system_globalization, system_text_json_serialization (+1 more)

### Community 69 - "EcMailbox"
Cohesion: 0.21
Nodes (8): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, EcMailbox, EcMailboxUnavailableException, Held

### Community 70 - "CoreLoad"
Cohesion: 0.13
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 71 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 72 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 73 - "DevicePowerState"
Cohesion: 0.28
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 74 - "HostileFilesTests"
Cohesion: 0.27
Nodes (5): IOException, Fact, InlineData, Theory, HostileFilesTests

### Community 75 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 76 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 77 - "ProcessMetrics"
Cohesion: 0.15
Nodes (13): Share, Dictionary, TimeSpan, FaultEvidence, ProcessMetrics, Name, PageFaultCount, SessionId (+5 more)

### Community 78 - "LedEffect"
Cohesion: 0.14
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 79 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 80 - ".Record"
Cohesion: 0.20
Nodes (7): IReadOnlyDictionary, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Fact, AuditLogTests

### Community 81 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 82 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 83 - ".WireSettingsPage"
Cohesion: 0.23
Nodes (3): Action, ToggleButton, EventArgs

### Community 84 - "InstallFolderGuard"
Cohesion: 0.33
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 85 - "BatteryModePolicy"
Cohesion: 0.42
Nodes (4): BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

### Community 86 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 87 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 88 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 89 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 90 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 91 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 92 - "PendingRestartInfo"
Cohesion: 0.20
Nodes (7): DateTime, Dictionary, List, PendingRestartInfo, BootTimeUtc, ReasonArguments, Reasons

### Community 93 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 94 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 95 - "Nextcalibur 0.5.4"
Cohesion: 0.18
Nodes (10): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, the second pass, Smaller, Tested on, The language row, The .NET runtime is now kept current like everything else (+2 more)

### Community 96 - ".Sanitised"
Cohesion: 0.29
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 97 - "SupportVerdict"
Cohesion: 0.20
Nodes (10): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+2 more)

### Community 98 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 100 - "ThemeService"
Cohesion: 0.20
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 101 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 102 - ".CalculateThreadShare"
Cohesion: 0.27
Nodes (4): IReadOnlyDictionary, Stable, Dictionary, TopSharePercent

### Community 103 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 104 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 105 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 106 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 107 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 108 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 109 - "StackPanel"
Cohesion: 0.22
Nodes (9): BannerActions, DialogActions, DriveTextStack, FnShortcutRows, PanelWindowsFaultsSubOptions, ProfileRow, ProgramRuleRows, SettingUpdatesAutomatic (+1 more)

### Community 111 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 112 - ".IsSystemFileSignedBy"
Cohesion: 0.39
Nodes (3): Fact, SystemFileTrustTests, Cmd

### Community 114 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 115 - ".GetAsync"
Cohesion: 0.29
Nodes (7): Answer, ConcurrentDictionary, CancellationToken, HttpClient, Task, Answer, Conditional

### Community 116 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 117 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 118 - "ProtectedStoreElevatedTests"
Cohesion: 0.32
Nodes (4): Fact, SecurityIdentifier, ProtectedStoreElevatedTests, Elevated

### Community 119 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 120 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 121 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 122 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 123 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 124 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 125 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 126 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 127 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 128 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 129 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 130 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 131 - ".ForegroundProcess"
Cohesion: 0.47
Nodes (3): DllImport, IntPtr, Name

### Community 132 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 133 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 134 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 135 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 136 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 138 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 140 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 141 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 142 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 143 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 144 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 145 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 146 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 148 - ".TryParseRgb"
Cohesion: 0.50
Nodes (3): B, G, R

### Community 149 - ".Modules_are_the_released_ones"
Cohesion: 0.50
Nodes (3): InlineData, Theory, PawnIoModuleTests

### Community 150 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 153 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 154 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

### Community 155 - "ProgramRuleMode"
Cohesion: 0.67
Nodes (3): ProgramRuleMode, ProgramRuleName, ComboBox

## Knowledge Gaps
- **466 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+461 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 892 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **31 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `RunawayWatchTests`, `.OnBrightnessChanged`, `.ForegroundProcess`, `.Get`, `IShellLinkW`, `PlanTuning`, `GpuClockReader`, `RadioButton`, `LedZone`, `UserPresence`, `TrayPresence`, `SystemInfo`, `AppSettings`, `CpuPowerReader`, `.ApplyFnShortcuts`, `Button`, `WindowsFaults`, `.CheckProgram`, `Nextcalibur.Core.Hardware`, `.OnGpuModeChanged`, `UpdateService`, `ProcessPresence`, `FnShortcut`, `CoolingPolicyTests`, `.RequestRestart`, `SystemModeService`, `.Info`, `.Get`, `ProgramRule`, `Window`, `.PlaceTourStep`, `Program`, `MainWindow.FnKeys.cs`, `PowerOverlayService`, `ToggleButton`, `.Read`, `Nextcalibur.App`, `.OfferDependency`, `BacklightKeyWatcher`, `SystemMode`, `EcMailbox`, `LedController`, `LedEffect`, `Grid`, `.WireSettingsPage`, `BatteryModePolicy`, `Strings`, `SupportVerdict`, `.RefreshProgramRuleRows`, `ThemeService`, `NvidiaDriverState`, `TourPage`?**
  _High betweenness centrality (0.470) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `.OnBrightnessChanged`, `RadioButton`, `Keycaps`, `DriveGauge`, `Button`, `.OnGpuModeChanged`, `CpuWarnValue`, `DialogCheck`, `ProgramRuleMode`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `ToggleButton`, `Grid`, `Border`, `StackPanel`, `Text`?**
  _High betweenness centrality (0.117) - this node is a cross-community bridge._
- **Why does `AppSettings` connect `AppSettings` to `MainWindow`, `SystemMode`, `.Sanitised`, `.Read`, `Nextcalibur.Core.Configuration`, `ThemeService`, `HostileFilesTests`, `TrayPresence`, `.Migrate`, `Fact`, `WindowsFaultsTests`, `UpdateService`, `Strings`, `PendingRestartInfo`, `.Info`?**
  _High betweenness centrality (0.080) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _466 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.03878942881500426 - nodes in this community are weakly interconnected._
- **Should `RunawayWatchTests` be split into smaller, more focused modules?**
  _Cohesion score 0.06493506493506493 - nodes in this community are weakly interconnected._