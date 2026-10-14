# Graph Report - nextcalibur-control-center  (2026-10-14)

## Corpus Check
- 180 files · ~228,124 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3004 nodes · 6537 edges · 180 communities (146 shown, 34 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 350 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `364655a0`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- AppSettings
- FnShortcut
- PlanTuning
- ProtectedStore
- .Get
- GpuClockReader
- RunawayWatchTests
- IShellLinkW
- WindowsFaults
- ProgramRule
- RadioButton
- Window
- EcMailbox
- Nextcalibur.App
- .RequestRestart
- .Info
- TrayPresence
- Nextcalibur.Core.Hardware
- .Main
- UserPresence
- .Survey
- SystemInfo
- .Check
- WindowsFaultsTests
- NetworkCostTests
- SystemModeService
- .PowerModeControls
- ProcessPresence
- Program
- CoolingPolicyTests
- .RemoveMachineTraces
- .StartSlowTimer
- CpuPowerReader
- Fact
- InstallFolderGuard
- .SignatureIsValid
- LedState
- MachineScanRecord
- Button
- .OnClosing
- Authenticode
- microsoft_win32
- Dependency
- GpuModeService
- Nextcalibur.Core.Configuration
- DotNetRuntimeDependency
- RestorePoint
- BacklightKeyWatcher
- Nextcalibur.Core.csproj
- Integrity
- .CheckProgram
- .Ask
- .For
- MemoryTrimmer
- DependencyStatus
- ToggleButton
- .Dictionary
- analyse-autopsy.py
- UpdateService
- CoreLoad
- ResourceDictionary
- RuntimeDependencyTests
- SystemMode
- .ApplyFnShortcuts
- LedController
- ColourWheel
- DevicePowerState
- PowerOverlayService
- FanGauge
- .GameTick
- Unelevated
- DonutGauge
- ThemeService
- SegmentedBar
- LogInjectionTests
- Integrity.cs
- SystemTools
- Grid
- LedEffect
- PawnIoDependency
- BatteryModePolicy
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- Nextcalibur Control Center
- Strings
- .RefreshProgramRuleRows
- .IdleChosen
- SmiCommandTests
- Nextcalibur.Core.Security
- Hardware Protocol
- Nextcalibur 0.5.3
- NvidiaDriverState
- .Values_are_the_ones_measured_from_the_vendor
- IntPtr
- HostileFilesTests
- Border
- StackPanel
- .Sanitised
- SupportVerdict
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
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
- .ChangeSetting
- TourPage
- .Current
- Log-Graphics.ps1
- Privacy
- Nextcalibur 0.6.2
- Security
- LedZone
- MachineWideGate
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
- Strings.cs
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
1. `MainWindow` - 346 edges
2. `Window` - 178 edges
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

## Communities (180 total, 34 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.03
Nodes (64): AssemblyInformationalVersionAttribute, CheckBox, DeferredOverheat, GameEnd, HwndSource, ObservableCollection, PluggedIn, Queue (+56 more)

### Community 1 - "AppSettings"
Cohesion: 0.04
Nodes (53): DateTime, Dictionary, FnShortcut, JsonElement, List, ProgramRule, AppSettings, AcceptedVendorSoftware (+45 more)

### Community 2 - "FnShortcut"
Cohesion: 0.05
Nodes (43): Nextcalibur.Core.Input, INotifyPropertyChanged, Problem, DriveRow, Detail, Name, Percent, PercentText (+35 more)

### Community 3 - "PlanTuning"
Cohesion: 0.08
Nodes (29): Floor, Marker, Original, Action, AcDc, Guid, IReadOnlyList, IdleSetting (+21 more)

### Community 4 - "ProtectedStore"
Cohesion: 0.07
Nodes (26): Content, DirectorySecurity, Hash, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Dictionary (+18 more)

### Community 5 - ".Get"
Cohesion: 0.08
Nodes (12): Asynchronous dispatcher & UI thread hardening, SolidColorBrush, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary (+4 more)

### Community 6 - "GpuClockReader"
Cohesion: 0.08
Nodes (22): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+14 more)

### Community 7 - "RunawayWatchTests"
Cohesion: 0.06
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 8 - "IShellLinkW"
Cohesion: 0.06
Nodes (16): IPropertyStore, PropertyKey, PropVariant, Slider, DllImport, Guid, IEnumerable, IntPtr (+8 more)

### Community 9 - "WindowsFaults"
Cohesion: 0.07
Nodes (35): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+27 more)

### Community 10 - "ProgramRule"
Cohesion: 0.07
Nodes (26): GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, IEnumerable (+18 more)

### Community 11 - "RadioButton"
Cohesion: 0.06
Nodes (40): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeDiscrete, ModeGaming (+32 more)

### Community 12 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 13 - "EcMailbox"
Cohesion: 0.11
Nodes (16): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+8 more)

### Community 14 - "Nextcalibur.App"
Cohesion: 0.11
Nodes (22): Nextcalibur.App, Nextcalibur.App.Controls, system_drawing, system_linq, system_windows, system_windows_controls, system_windows_controls_button, system_windows_controls_checkbox (+14 more)

### Community 15 - ".RequestRestart"
Cohesion: 0.09
Nodes (11): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr (+3 more)

### Community 16 - ".Info"
Cohesion: 0.10
Nodes (7): Func, MessageBoxButton, MessageBoxResult, Exception, Log, Folder, IReadOnlyDictionary

### Community 17 - "TrayPresence"
Cohesion: 0.08
Nodes (16): ContextMenuStrip, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, EventHandler, Icon (+8 more)

### Community 18 - "Nextcalibur.Core.Hardware"
Cohesion: 0.14
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, Nextcalibur.Core.Hardware, nextcalibur_core_power_plantuning_acdc, system_xml_linq, xunit

### Community 19 - ".Main"
Cohesion: 0.10
Nodes (8): The threat model, and what the code does about it, Application, DllImport, Mutex, App, RegistryKey, Footprint, STAThread

### Community 20 - "UserPresence"
Cohesion: 0.12
Nodes (14): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, Point, Size, DateTime, DllImport (+6 more)

### Community 21 - ".Survey"
Cohesion: 0.12
Nodes (8): Func, IEnumerable, IReadOnlyList, RetirementEntry, Trace, Version, NduFix, FootprintTests

### Community 22 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 23 - ".Check"
Cohesion: 0.11
Nodes (13): CommonAce, RawSecurityDescriptor, RegistryKey, SecurityIdentifier, MailboxAccess, MailboxAvailability, AccessNotGranted, Available (+5 more)

### Community 25 - "NetworkCostTests"
Cohesion: 0.14
Nodes (19): Bytes, Count, HttpMessageHandler, CancellationToken, Dictionary, Fact, HttpRequestMessage, HttpResponseMessage (+11 more)

### Community 26 - "SystemModeService"
Cohesion: 0.15
Nodes (11): InvalidOperationException, Ours, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList (+3 more)

### Community 27 - ".PowerModeControls"
Cohesion: 0.09
Nodes (20): Option, Button, IEnumerable, TextBlock, IReadOnlyList, OverlayDiagnosis, GuardMissing, NeedsRepair (+12 more)

### Community 28 - "ProcessPresence"
Cohesion: 0.12
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 29 - "Program"
Cohesion: 0.11
Nodes (8): ConsoleColor, B, G, R, Program, DateTimeOffset, ThermalReader, ThermalSample

### Community 30 - "CoolingPolicyTests"
Cohesion: 0.12
Nodes (16): At, Margin, DateTime, List, TimeSpan, CoolingPolicy, DegreesPerMinute, Engaged (+8 more)

### Community 31 - ".RemoveMachineTraces"
Cohesion: 0.13
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 32 - ".StartSlowTimer"
Cohesion: 0.09
Nodes (4): IReadOnlyList, GameEnd, Action, Toasts

### Community 33 - "CpuPowerReader"
Cohesion: 0.16
Nodes (9): DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled, Fact, InlineData, Theory (+1 more)

### Community 34 - "Fact"
Cohesion: 0.16
Nodes (8): IList, HardwareSupport, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 35 - "InstallFolderGuard"
Cohesion: 0.14
Nodes (10): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Fact, InlineData, Theory, ElevationPolicyTests (+2 more)

### Community 36 - ".SignatureIsValid"
Cohesion: 0.14
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 37 - "LedState"
Cohesion: 0.15
Nodes (15): B, G, R, LedState, ActiveProfile, BrightnessPercent, Current, Effect (+7 more)

### Community 38 - "MachineScanRecord"
Cohesion: 0.11
Nodes (18): DateTime, JsonSerializerOptions, List, MachineScan, RecordPath, MachineScanRecord, BiosVendor, BiosVersion (+10 more)

### Community 39 - "Button"
Cohesion: 0.08
Nodes (24): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, MinimiseButton (+16 more)

### Community 40 - ".OnClosing"
Cohesion: 0.10
Nodes (9): CancelEventArgs, SizeChangedEventArgs, KeyEventArgs, RoutedEventArgs, TourStep, Body, SectionName, Title (+1 more)

### Community 41 - "Authenticode"
Cohesion: 0.25
Nodes (14): CatalogInfo, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle, Authenticode (+6 more)

### Community 42 - "microsoft_win32"
Cohesion: 0.15
Nodes (9): Nextcalibur.Core.Dependencies, microsoft_win32, system_collections_concurrent, system_net, system_net_http, system_security, system_text, system_text_json (+1 more)

### Community 43 - "Dependency"
Cohesion: 0.13
Nodes (15): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+7 more)

### Community 44 - "GpuModeService"
Cohesion: 0.18
Nodes (9): ArgumentNullException, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma, GpuModeService, SwitchOutcome (+1 more)

### Community 45 - "Nextcalibur.Core.Configuration"
Cohesion: 0.15
Nodes (8): Nextcalibur.Core.Configuration, microsoft_win32_safehandles, system_componentmodel, system_diagnostics, system_io, system_reflection, system_runtime_compilerservices, system_runtime_interopservices

### Community 46 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 47 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 48 - "BacklightKeyWatcher"
Cohesion: 0.11
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 49 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 50 - "Integrity"
Cohesion: 0.14
Nodes (9): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvmlPath, InlineData (+1 more)

### Community 51 - ".CheckProgram"
Cohesion: 0.20
Nodes (5): Fact, IntegrityTests, Current, Exe, Manifest

### Community 52 - ".Ask"
Cohesion: 0.25
Nodes (10): HttpStatusCode, CancellationToken, Fact, HttpRequestMessage, HttpResponseMessage, InlineData, Task, Theory (+2 more)

### Community 53 - ".For"
Cohesion: 0.16
Nodes (12): MemberData, AcDc, Guid, IReadOnlyDictionary, IReadOnlyList, ModeProfile, Settings, Fact (+4 more)

### Community 54 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 55 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 56 - "ToggleButton"
Cohesion: 0.10
Nodes (20): LedPower, OverheatWarningToggle, SelectAll, SettingAskGameMode, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice (+12 more)

### Community 57 - ".Dictionary"
Cohesion: 0.15
Nodes (9): IReadOnlyDictionary, Stable, Dictionary, Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests (+1 more)

### Community 58 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 59 - "UpdateService"
Cohesion: 0.13
Nodes (12): DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService, Available, AvailableVersion (+4 more)

### Community 60 - "CoreLoad"
Cohesion: 0.14
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 61 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 62 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 63 - "SystemMode"
Cohesion: 0.24
Nodes (5): PowerModeChangedEventArgs, SystemMode, Gaming, Office, Performance

### Community 65 - "LedController"
Cohesion: 0.24
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 66 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 67 - "DevicePowerState"
Cohesion: 0.25
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 68 - "PowerOverlayService"
Cohesion: 0.26
Nodes (4): DllImport, Guid, PowerOverlayService, IdempotenceTests

### Community 69 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 70 - ".GameTick"
Cohesion: 0.15
Nodes (11): GameSession, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc, Looks (+3 more)

### Community 71 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 72 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 73 - "ThemeService"
Cohesion: 0.14
Nodes (11): Theme, Theme, Dark, Light, ThemePreference, Dark, Light, System (+3 more)

### Community 74 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 75 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 76 - "Integrity.cs"
Cohesion: 0.22
Nodes (5): Nextcalibur.Cli, system_management, system_security_accesscontrol, system_security_cryptography, system_security_principal

### Community 77 - "SystemTools"
Cohesion: 0.17
Nodes (9): FileStream, SystemTools, Cmd, Explorer, Pnputil, Powercfg, PowerShell, Schtasks (+1 more)

### Community 78 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 79 - "LedEffect"
Cohesion: 0.15
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 80 - "PawnIoDependency"
Cohesion: 0.15
Nodes (11): CancellationToken, HttpClient, Task, Version, PawnIoDependency, ExpectedSigner, Id, Name (+3 more)

### Community 81 - "BatteryModePolicy"
Cohesion: 0.42
Nodes (4): BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

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

### Community 86 - "Nextcalibur 0.5.4"
Cohesion: 0.17
Nodes (11): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+3 more)

### Community 87 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 88 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 90 - ".IdleChosen"
Cohesion: 0.26
Nodes (3): ComboBox, Grid, Guid

### Community 91 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 92 - "Nextcalibur.Core.Security"
Cohesion: 0.25
Nodes (4): Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_security_cryptography_x509certificates, system_text_regularexpressions

### Community 93 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 94 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 95 - "NvidiaDriverState"
Cohesion: 0.27
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 96 - ".Values_are_the_ones_measured_from_the_vendor"
Cohesion: 0.24
Nodes (5): ThermalProfile, Fact, InlineData, Theory, ThermalProfileTests

### Community 97 - "IntPtr"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 98 - "HostileFilesTests"
Cohesion: 0.42
Nodes (3): IOException, Fact, HostileFilesTests

### Community 99 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 100 - "StackPanel"
Cohesion: 0.20
Nodes (10): BannerActions, DialogActions, DriveTextStack, FnShortcutRows, IdleRows, PanelWindowsFaultsSubOptions, ProfileRow, ProgramRuleRows (+2 more)

### Community 101 - ".Sanitised"
Cohesion: 0.33
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 102 - "SupportVerdict"
Cohesion: 0.22
Nodes (9): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites (+1 more)

### Community 103 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 104 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 105 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 106 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 107 - ".IsSystemFileSignedBy"
Cohesion: 0.39
Nodes (3): Fact, SystemFileTrustTests, Cmd

### Community 109 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 110 - ".GetAsync"
Cohesion: 0.29
Nodes (7): Answer, ConcurrentDictionary, CancellationToken, HttpClient, Task, Answer, Conditional

### Community 111 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 112 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 113 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 114 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 115 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 116 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 117 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 118 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 119 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 120 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 121 - ".ForegroundProcess"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Name, Pid

### Community 123 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 124 - ".Current"
Cohesion: 0.48
Nodes (3): DllImport, IntPtr, ScreenSaver

### Community 125 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 126 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 127 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 128 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 129 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 130 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 131 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 132 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 133 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 134 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 136 - "SmiSubsystem"
Cohesion: 0.40
Nodes (5): SmiSubsystem, DisplayMode, Led, Profile, Thermal

### Community 137 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 139 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 140 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 141 - "DriveGauge"
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

### Community 148 - "BrightnessSlider"
Cohesion: 0.50
Nodes (4): BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 149 - "ComboBox"
Cohesion: 0.50
Nodes (4): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, ComboBox

### Community 150 - ".Modules_are_the_released_ones"
Cohesion: 0.50
Nodes (3): InlineData, Theory, PawnIoModuleTests

### Community 151 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 154 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 155 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

### Community 156 - "Words"
Cohesion: 0.67
Nodes (3): Func, Words, Resolver

## Knowledge Gaps
- **481 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+476 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 931 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **34 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `AppSettings`, `FnShortcut`, `PlanTuning`, `LedZone`, `.Get`, `GpuClockReader`, `RunawayWatchTests`, `IShellLinkW`, `WindowsFaults`, `ProgramRule`, `RadioButton`, `Window`, `EcMailbox`, `Nextcalibur.App`, `.RequestRestart`, `.Info`, `TrayPresence`, `UserPresence`, `SystemInfo`, `SystemModeService`, `.PowerModeControls`, `Program`, `CoolingPolicyTests`, `.StartSlowTimer`, `CpuPowerReader`, `.OnClosing`, `GpuModeService`, `Nextcalibur.Core.Configuration`, `BacklightKeyWatcher`, `DependencyStatus`, `UpdateService`, `SystemMode`, `.ApplyFnShortcuts`, `LedController`, `PowerOverlayService`, `.GameTick`, `ThemeService`, `Grid`, `LedEffect`, `BatteryModePolicy`, `Strings`, `.RefreshProgramRuleRows`, `.IdleChosen`, `Nextcalibur.Core.Security`, `NvidiaDriverState`, `SupportVerdict`, `.ForegroundProcess`, `.ChangeSetting`, `TourPage`?**
  _High betweenness centrality (0.522) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `RadioButton`, `Keycaps`, `DriveGauge`, `BrightnessSlider`, `ComboBox`, `CpuWarnValue`, `DialogCheck`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `Button`, `ToggleButton`, `Grid`, `Border`, `StackPanel`, `Text`?**
  _High betweenness centrality (0.095) - this node is a cross-community bridge._
- **Why does `DependencyStatus` connect `DependencyStatus` to `MainWindow`, `microsoft_win32`, `Dependency`, `.Get`?**
  _High betweenness centrality (0.083) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _481 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.02792079207920792 - nodes in this community are weakly interconnected._
- **Should `AppSettings` be split into smaller, more focused modules?**
  _Cohesion score 0.039272963323596234 - nodes in this community are weakly interconnected._