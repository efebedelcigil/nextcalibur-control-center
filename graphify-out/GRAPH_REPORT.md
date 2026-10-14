# Graph Report - nextcalibur-control-center  (2026-10-14)

## Corpus Check
- 182 files · ~230,829 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3040 nodes · 6612 edges · 171 communities (141 shown, 30 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 363 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `8bdfa655`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- AppSettings
- Authenticode
- PlanTuning
- CpuPowerReader
- .Get
- RunawayWatchTests
- GpuClockReader
- .CheckProgram
- ProcessPresence
- Nextcalibur.Core.Configuration
- UpdateService
- WindowsFaults
- FnShortcut
- .InstallAsync
- IShellLinkW
- Window
- WindowsFaultsTests
- ProgramRule
- RadioButton
- Button
- SystemInfo
- .OnLoaded
- .Warn
- MachineScanRecord
- Nextcalibur.Core.Hardware
- .RequestRestart
- .Survey
- .ApplyFnShortcuts
- TrayPresence
- EcMailbox
- .Get
- NetworkCostTests
- UserPresence
- Fact
- ProtectedStore
- SystemModeService
- LedState
- CoolingPolicyTests
- .WireSettingsPage
- Elevation
- Nextcalibur.Core.Security
- Dependency
- PowerOverlayService
- system_runtime_interopservices
- .Ask
- DotNetRuntimeDependency
- Program
- BacklightKeyWatcher
- Nextcalibur.Core.csproj
- .IdleChosen
- .Check
- RestorePoint
- .For
- MemoryTrimmer
- LedController
- ToggleButton
- .Execute
- analyse-autopsy.py
- Nextcalibur.Core.Power
- system_diagnostics
- ResourceDictionary
- RuntimeDependencyTests
- CoreLoad
- .Read
- ColourWheel
- .GameTick
- SystemMode
- .IsUnderProgramFiles
- FanGauge
- DevicePowerState
- .OnPowerSourceMayHaveChanged
- Unelevated
- DonutGauge
- Hardware Protocol
- SegmentedBar
- Nextcalibur Control Center
- LedEffect
- .HasAccess
- .Record
- LogInjectionTests
- Grid
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Strings
- Nextcalibur 0.5.3
- .Sanitised
- Border
- StackPanel
- IntegrityKind
- ThemeService
- NvidiaDriverState
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- PawnIoDependency
- ThermalSample
- Probe
- .GetAsync
- Nextcalibur 0.5.1
- Log
- NotificationState
- ProtectedStoreElevatedTests
- Test-Ui.ps1
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- .Pick
- .ForegroundProcess
- TourPage
- MachineWideGate
- .Values_are_the_ones_measured_from_the_vendor
- Log-Graphics.ps1
- Text
- Privacy
- Nextcalibur 0.6.2
- Security
- .OnThresholdMoved
- TourStep
- LedZone
- PowerSource
- Notice
- Nextcalibur 0.5.11
- .Set
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- 2. Command structure
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- Nextcalibur 0.6.1
- .ArrangeOverride
- BrightnessSlider
- ComboBox
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
1. `MainWindow` - 347 edges
2. `Window` - 178 edges
3. `AppSettings` - 75 edges
4. `Nextcalibur.Core.Hardware` - 56 edges
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

## Communities (171 total, 30 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.03
Nodes (65): CheckBox, DeferredOverheat, HwndSource, ObservableCollection, Option, PluggedIn, Queue, SizeChangedEventArgs (+57 more)

### Community 1 - "AppSettings"
Cohesion: 0.04
Nodes (53): DateTime, Dictionary, FnShortcut, JsonElement, List, ProgramRule, AppSettings, AcceptedVendorSoftware (+45 more)

### Community 2 - "Authenticode"
Cohesion: 0.06
Nodes (38): CatalogInfo, Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on (+30 more)

### Community 3 - "PlanTuning"
Cohesion: 0.08
Nodes (29): Floor, Marker, Original, Action, AcDc, Guid, IReadOnlyList, IdleSetting (+21 more)

### Community 4 - "CpuPowerReader"
Cohesion: 0.07
Nodes (24): ArgumentOutOfRangeException, Bit, IReadOnlySet, Now, Offset, PowerLimits, SinceBoot, IReadOnlyList (+16 more)

### Community 5 - ".Get"
Cohesion: 0.07
Nodes (15): CancelEventArgs, GameEnd, GeneratedRegex, Dictionary, IReadOnlyDictionary, Task, KeyEventArgs, IReadOnlyList (+7 more)

### Community 6 - "RunawayWatchTests"
Cohesion: 0.07
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 7 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 8 - ".CheckProgram"
Cohesion: 0.08
Nodes (18): Lazy, ProcessModule, FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Dictionary, HashSet (+10 more)

### Community 9 - "ProcessPresence"
Cohesion: 0.07
Nodes (22): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+14 more)

### Community 10 - "Nextcalibur.Core.Configuration"
Cohesion: 0.09
Nodes (26): Nextcalibur.App, Nextcalibur.App.Controls, Nextcalibur.Core, Nextcalibur.Core.Configuration, system_componentmodel, system_drawing, system_globalization, system_io (+18 more)

### Community 11 - "UpdateService"
Cohesion: 0.07
Nodes (21): Asynchronous dispatcher & UI thread hardening, Task, EventArgs, DateTime, DispatcherTimer, HashSet, IProgress, Task (+13 more)

### Community 12 - "WindowsFaults"
Cohesion: 0.08
Nodes (34): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+26 more)

### Community 13 - "FnShortcut"
Cohesion: 0.08
Nodes (25): DllImport, IEnumerable, IntPtr, List, StringBuilder, FnAction, FanTurbo, KeyboardBacklight (+17 more)

### Community 14 - ".InstallAsync"
Cohesion: 0.07
Nodes (25): FileStream, IOException, Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress (+17 more)

### Community 15 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 16 - "Window"
Cohesion: 0.08
Nodes (44): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+36 more)

### Community 17 - "WindowsFaultsTests"
Cohesion: 0.12
Nodes (7): IReadOnlyDictionary, FaultEvaluation, Stable, Fact, WindowsFaultsTests, Dictionary, TopSharePercent

### Community 18 - "ProgramRule"
Cohesion: 0.08
Nodes (23): HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, IEnumerable, IReadOnlyDictionary (+15 more)

### Community 19 - "RadioButton"
Cohesion: 0.07
Nodes (37): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeGaming, ModeOffice (+29 more)

### Community 20 - "Button"
Cohesion: 0.06
Nodes (28): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, HideToTrayButton (+20 more)

### Community 21 - "SystemInfo"
Cohesion: 0.08
Nodes (21): MemoryStatusEx, DriveUse, INotifyPropertyChanged, DriveRow, Name, Percent, PercentText, DllImport (+13 more)

### Community 22 - ".OnLoaded"
Cohesion: 0.07
Nodes (7): AssemblyInformationalVersionAttribute, SolidColorBrush, Func, Action, Toasts, windows_data_xml_dom, windows_ui_notifications

### Community 23 - ".Warn"
Cohesion: 0.11
Nodes (10): The threat model, and what the code does about it, Application, DllImport, Mutex, App, MessageBoxButton, MessageBoxResult, RegistryKey (+2 more)

### Community 24 - "MachineScanRecord"
Cohesion: 0.07
Nodes (27): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites (+19 more)

### Community 25 - "Nextcalibur.Core.Hardware"
Cohesion: 0.14
Nodes (5): Nextcalibur.Core.Tests, Nextcalibur.Core.Hardware, nextcalibur_core_power_plantuning_acdc, system_xml_linq, xunit

### Community 26 - ".RequestRestart"
Cohesion: 0.10
Nodes (11): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr (+3 more)

### Community 27 - ".Survey"
Cohesion: 0.12
Nodes (8): Func, IEnumerable, IReadOnlyList, RetirementEntry, Trace, Version, NduFix, FootprintTests

### Community 28 - ".ApplyFnShortcuts"
Cohesion: 0.09
Nodes (13): Problem, Detail, Dictionary, DllImport, IntPtr, ShortcutProblem, Duplicate, NotAKey (+5 more)

### Community 29 - "TrayPresence"
Cohesion: 0.09
Nodes (15): ContextMenuStrip, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, EventHandler, Icon (+7 more)

### Community 30 - "EcMailbox"
Cohesion: 0.11
Nodes (17): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, EcMailbox, EcMailboxUnavailableException, Held (+9 more)

### Community 31 - ".Get"
Cohesion: 0.12
Nodes (13): GpuLoad, FirmwareModeReading, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma, GpuModeService (+5 more)

### Community 32 - "NetworkCostTests"
Cohesion: 0.14
Nodes (19): Bytes, Count, HttpMessageHandler, CancellationToken, Dictionary, Fact, HttpRequestMessage, HttpResponseMessage (+11 more)

### Community 33 - "UserPresence"
Cohesion: 0.15
Nodes (12): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr, MarshalAs (+4 more)

### Community 34 - "Fact"
Cohesion: 0.15
Nodes (9): ArgumentNullException, IList, HardwareSupport, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests (+1 more)

### Community 35 - "ProtectedStore"
Cohesion: 0.13
Nodes (13): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+5 more)

### Community 36 - "SystemModeService"
Cohesion: 0.16
Nodes (11): InvalidOperationException, Ours, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList (+3 more)

### Community 37 - "LedState"
Cohesion: 0.14
Nodes (17): B, G, LedBrightness, R, LedState, ActiveProfile, BrightnessPercent, Current (+9 more)

### Community 38 - "CoolingPolicyTests"
Cohesion: 0.13
Nodes (15): At, Margin, DateTime, List, TimeSpan, CoolingPolicy, DegreesPerMinute, Engaged (+7 more)

### Community 39 - ".WireSettingsPage"
Cohesion: 0.12
Nodes (7): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Action, ToggleButton

### Community 40 - "Elevation"
Cohesion: 0.14
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 41 - "Nextcalibur.Core.Security"
Cohesion: 0.14
Nodes (9): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_security_cryptography_x509certificates, system_text_json (+1 more)

### Community 42 - "Dependency"
Cohesion: 0.11
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 43 - "PowerOverlayService"
Cohesion: 0.15
Nodes (11): DllImport, Guid, IReadOnlyList, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, PowerModeOption (+3 more)

### Community 44 - "system_runtime_interopservices"
Cohesion: 0.10
Nodes (8): Nextcalibur.Core.Input, system_io_directory, system_io_ioexception, system_io_path, system_management, system_runtime_interopservices, system_runtime_interopservices_comtypes, system_text

### Community 45 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 46 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 47 - "Program"
Cohesion: 0.17
Nodes (5): ConsoleColor, B, G, R, Program

### Community 48 - "BacklightKeyWatcher"
Cohesion: 0.11
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 49 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 50 - ".IdleChosen"
Cohesion: 0.16
Nodes (6): ComboBox, Grid, Guid, DllImport, IntPtr, ScreenSaver

### Community 51 - ".Check"
Cohesion: 0.17
Nodes (8): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported, IdempotenceTests

### Community 52 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 53 - ".For"
Cohesion: 0.16
Nodes (12): MemberData, AcDc, Guid, IReadOnlyDictionary, IReadOnlyList, ModeProfile, Settings, Fact (+4 more)

### Community 54 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 55 - "LedController"
Cohesion: 0.18
Nodes (6): RoutedPropertyChangedEventArgs, Color, LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 56 - "ToggleButton"
Cohesion: 0.10
Nodes (19): OverheatWarningToggle, SelectAll, SettingAskGameMode, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost (+11 more)

### Community 57 - ".Execute"
Cohesion: 0.19
Nodes (7): ArgumentException, Func, SmiCommand, Fact, InlineData, Theory, SmiCommandTests

### Community 58 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 59 - "Nextcalibur.Core.Power"
Cohesion: 0.12
Nodes (9): Nextcalibur.Core.Power, microsoft_win32, microsoft_win32_safehandles, Theme, Dark, Light, system_reflection, system_security (+1 more)

### Community 60 - "system_diagnostics"
Cohesion: 0.17
Nodes (5): Nextcalibur.Cli, system_diagnostics, system_security_accesscontrol, system_security_cryptography, system_security_principal

### Community 61 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 62 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 63 - "CoreLoad"
Cohesion: 0.15
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 64 - ".Read"
Cohesion: 0.30
Nodes (3): Fact, List, ProtectedStoreTests

### Community 65 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 66 - ".GameTick"
Cohesion: 0.14
Nodes (12): GameSession, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc, Looks (+4 more)

### Community 67 - "SystemMode"
Cohesion: 0.26
Nodes (4): SystemMode, Gaming, Office, Performance

### Community 68 - ".IsUnderProgramFiles"
Cohesion: 0.21
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 69 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 70 - "DevicePowerState"
Cohesion: 0.28
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 71 - ".OnPowerSourceMayHaveChanged"
Cohesion: 0.34
Nodes (5): PowerModeChangedEventArgs, BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

### Community 72 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 73 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 74 - "Hardware Protocol"
Cohesion: 0.14
Nodes (14): 1. Transport — ACPI-WMI mailbox, 3. Thermal / fan — `a1 = 0x0200`, 4. LED — `a1 = 0x0100`, 6. Safety rules, 7. Not supported, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`) (+6 more)

### Community 75 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 76 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 77 - "LedEffect"
Cohesion: 0.14
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 78 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 79 - ".Record"
Cohesion: 0.20
Nodes (7): IReadOnlyDictionary, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Fact, AuditLogTests

### Community 80 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 81 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 82 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 83 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 84 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 85 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 86 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 87 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 88 - ".Sanitised"
Cohesion: 0.29
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 89 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 90 - "StackPanel"
Cohesion: 0.20
Nodes (10): BannerActions, DialogActions, DriveTextStack, FnShortcutRows, IdleRows, PanelWindowsFaultsSubOptions, ProfileRow, ProgramRuleRows (+2 more)

### Community 91 - "IntegrityKind"
Cohesion: 0.20
Nodes (9): IntegrityFinding, IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified (+1 more)

### Community 92 - "ThemeService"
Cohesion: 0.20
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 93 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

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
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 99 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 100 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 101 - "ThermalSample"
Cohesion: 0.31
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 102 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 103 - ".GetAsync"
Cohesion: 0.29
Nodes (7): Answer, ConcurrentDictionary, CancellationToken, HttpClient, Task, Answer, Conditional

### Community 104 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 106 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 107 - "ProtectedStoreElevatedTests"
Cohesion: 0.32
Nodes (4): Fact, SecurityIdentifier, ProtectedStoreElevatedTests, Elevated

### Community 108 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 109 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 110 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 111 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 112 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 113 - ".ForegroundProcess"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Name, Pid

### Community 114 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 115 - "MachineWideGate"
Cohesion: 0.29
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 116 - ".Values_are_the_ones_measured_from_the_vendor"
Cohesion: 0.33
Nodes (4): Fact, InlineData, Theory, ThermalProfileTests

### Community 117 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 118 - "Text"
Cohesion: 0.47
Nodes (6): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, FanGauge

### Community 119 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 120 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 121 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 123 - "TourStep"
Cohesion: 0.33
Nodes (5): TourStep, Body, SectionName, Title, TourPage

### Community 124 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 125 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 126 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 127 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 129 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 131 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 132 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 133 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 134 - "2. Command structure"
Cohesion: 0.50
Nodes (4): 2. Command structure, Call sequence, Command families (`a0`), Subsystems (`a1`)

### Community 135 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 136 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 137 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 138 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 140 - "BrightnessSlider"
Cohesion: 0.50
Nodes (4): BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 141 - "ComboBox"
Cohesion: 0.50
Nodes (4): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, ComboBox

### Community 142 - ".Modules_are_the_released_ones"
Cohesion: 0.50
Nodes (3): InlineData, Theory, PawnIoModuleTests

### Community 143 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 146 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 147 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

## Knowledge Gaps
- **484 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+479 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 945 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **30 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `AppSettings`, `PlanTuning`, `CpuPowerReader`, `.Get`, `RunawayWatchTests`, `GpuClockReader`, `ProcessPresence`, `Nextcalibur.Core.Configuration`, `UpdateService`, `WindowsFaults`, `FnShortcut`, `Window`, `ProgramRule`, `RadioButton`, `Button`, `SystemInfo`, `.OnLoaded`, `.Warn`, `MachineScanRecord`, `.RequestRestart`, `.ApplyFnShortcuts`, `TrayPresence`, `EcMailbox`, `.Get`, `UserPresence`, `SystemModeService`, `CoolingPolicyTests`, `.WireSettingsPage`, `PowerOverlayService`, `BacklightKeyWatcher`, `.IdleChosen`, `LedController`, `ToggleButton`, `.GameTick`, `SystemMode`, `.OnPowerSourceMayHaveChanged`, `LedEffect`, `Grid`, `Strings`, `IntegrityKind`, `ThemeService`, `NvidiaDriverState`, `ThermalSample`, `.ForegroundProcess`, `TourPage`, `.OnThresholdMoved`, `TourStep`, `LedZone`?**
  _High betweenness centrality (0.476) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Keycaps`, `DriveGauge`, `.Get`, `BrightnessSlider`, `ComboBox`, `CpuWarnValue`, `DialogCheck`, `Button`, `RadioButton`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `ToggleButton`, `Grid`, `Border`, `StackPanel`, `Text`?**
  _High betweenness centrality (0.093) - this node is a cross-community bridge._
- **Why does `Nextcalibur.Core.Hardware` connect `Nextcalibur.Core.Hardware` to `CpuPowerReader`, `ThermalSample`, `Nextcalibur.Core.Security`, `Nextcalibur.Core.Configuration`, `system_runtime_interopservices`, `BacklightKeyWatcher`, `MachineWideGate`, `MachineScanRecord`, `Nextcalibur.Core.Power`, `system_diagnostics`, `EcMailbox`, `.Get`?**
  _High betweenness centrality (0.077) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _484 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.029283916723861818 - nodes in this community are weakly interconnected._
- **Should `AppSettings` be split into smaller, more focused modules?**
  _Cohesion score 0.039272963323596234 - nodes in this community are weakly interconnected._