# Graph Report - nextcalibur-control-center  (2026-10-14)

## Corpus Check
- 215 files · ~262,067 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3691 nodes · 8171 edges · 198 communities (169 shown, 29 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 474 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `b9ef26b3`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- FnShortcut
- ProtectedStore
- ProgramRule
- GpuClockReader
- AppSettings
- PowerOverlayService
- WindowsFaults
- Nextcalibur.Core.Hardware
- TrayPresence
- IShellLinkW
- Window
- PlanTuning
- RadioButton
- .Get
- ProcessPresence
- xunit
- Nextcalibur.App
- TextBlock
- CoolingPolicyTests
- RunawayWatchTests
- SystemMode
- GameLibrary
- NetworkCostTests
- .OnLoaded
- .RequestRestart
- .StartSlowTimer
- .Info
- EcMailbox
- Nextcalibur.Core.Configuration
- SystemInfo
- Fact
- UserPresence
- WindowsFaultsTests
- Button
- SystemTools
- CrashGuard
- .ModeName
- Program
- .For
- Fact
- .Choose
- Api
- ColourWheel
- LedState
- ResourceDictionary
- BatteryModePolicy
- Dependency
- .SignatureIsValid
- CeilingTestWindow
- .Check
- .Main
- Integrity
- .ShowResult
- DevicePowerSaving
- .Get
- Authenticode
- TestPreflight
- .PlaceTourStep
- .Read
- FirmwareInventory
- UpdateService
- .Ask
- DotNetRuntimeDependency
- MachineScanRecord
- RestorePoint
- Nextcalibur.Core.csproj
- .OnSystemModeChanged
- .CheckProgram
- IdleSetting
- Elevation
- BacklightKeyWatcher
- MemoryTrimmer
- DependencyStatus
- CpuPowerReader
- .Dictionary
- analyse-autopsy.py
- ModeProfileTests
- .ApplyFnShortcuts
- TemperatureFusion
- CpuLimitsTests
- .LoadDefinitionsAsync
- RuntimeDependencyTests
- CoreLoad
- IntPtr
- CpuLimits
- LedController
- .Migrate
- FanGauge
- Name
- DevicePowerState
- Unelevated
- DonutGauge
- ThemeService
- Hardware Protocol
- SegmentedBar
- .Set
- Nextcalibur Control Center
- AcDc
- LedEffect
- PendingRestartInfo
- InstallFolderGuard
- Log
- .RunAndRecord
- LogInjectionTests
- Grid
- .HasAccess
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- .Pick
- Strings
- StackPanel
- .RefreshIdleRows
- PackageTemperatureTests
- Nextcalibur.Core.Dependencies
- Nextcalibur 0.5.3
- Footprint
- CardSwitchTasks
- .Sanitised
- NvidiaDriverState
- .Parse
- SupportVerdict
- ElevationPolicyTests
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- NoticeGate
- PawnIoDependency
- .IsSystemFileSignedBy
- Probe
- CeilingTests
- Nextcalibur 0.5.1
- Nextcalibur 0.5.5
- Toasts
- NduFix
- IntegrityKind
- Test-Ui.ps1
- Text
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- TourPage
- SetBy
- Log-Graphics.ps1
- Privacy
- Nextcalibur 0.6.2
- Nextcalibur 0.7.0
- Security
- .OnBrightnessChanged
- ComboBox
- LedZone
- PawnIoModuleTests.cs
- Notice
- Nextcalibur 0.5.11
- AppIdentity.cs
- PART_Track
- .Set
- .Read
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Strings.cs
- 2. Command structure
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- Nextcalibur 0.6.1
- Tools
- Toggle
- 0.5.7.md
- DialogCheck
- EffectPanel
- PART_ContentHost
- DriveDot
- DriveList
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
1. `MainWindow` - 432 edges
2. `Window` - 198 edges
3. `AppSettings` - 92 edges
4. `Nextcalibur.Core.Hardware` - 75 edges
5. `SystemMode` - 68 edges
6. `Nextcalibur.Core.Power` - 49 edges
7. `Nextcalibur.Core.Configuration` - 48 edges
8. `Nextcalibur.Core.Tests` - 45 edges
9. `RadioButton` - 41 edges
10. `TextBlock` - 40 edges

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

## Communities (198 total, 29 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.02
Nodes (85): Added, DeferredOverheat, Found, GameEnd, GameSession, HwndSource, ObservableCollection, PluggedIn (+77 more)

### Community 1 - "FnShortcut"
Cohesion: 0.05
Nodes (41): INotifyPropertyChanged, Problem, DriveRow, Detail, Name, Percent, PercentText, Dictionary (+33 more)

### Community 2 - "ProtectedStore"
Cohesion: 0.06
Nodes (26): Content, DirectorySecurity, Hash, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Dictionary (+18 more)

### Community 3 - "ProgramRule"
Cohesion: 0.06
Nodes (33): HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, GeneratedRegex, IEnumerable (+25 more)

### Community 4 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 5 - "AppSettings"
Cohesion: 0.04
Nodes (55): FnShortcut, JsonElement, ProgramRule, AppSettings, AcceptedVendorSoftware, AskModeForGames, AutoCheckForUpdates, AutoCooling (+47 more)

### Community 6 - "PowerOverlayService"
Cohesion: 0.06
Nodes (29): Option, Button, IEnumerable, TextBlock, Mutex, TimeSpan, MachineWideGate, DllImport (+21 more)

### Community 7 - "WindowsFaults"
Cohesion: 0.07
Nodes (36): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+28 more)

### Community 8 - "Nextcalibur.Core.Hardware"
Cohesion: 0.08
Nodes (13): Nextcalibur.Cli, Nextcalibur.Core.Input, Nextcalibur.Core.Hardware, microsoft_win32, microsoft_win32_safehandles, system_componentmodel, system_diagnostics, system_io (+5 more)

### Community 9 - "TrayPresence"
Cohesion: 0.05
Nodes (32): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, ProfessionalColorTable, Color, List (+24 more)

### Community 10 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 11 - "Window"
Cohesion: 0.07
Nodes (46): TemperatureToDoubleConverter, Foreground, IsMouseOver, ItemsSource.Count, Name, Banner, Bd, ColGauge (+38 more)

### Community 12 - "PlanTuning"
Cohesion: 0.12
Nodes (18): Floor, Marker, Original, Action, AcDc, DllImport, Guid, IEnumerable (+10 more)

### Community 13 - "RadioButton"
Cohesion: 0.06
Nodes (42): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeBalanced, ModeDiscrete (+34 more)

### Community 14 - ".Get"
Cohesion: 0.09
Nodes (7): Dictionary, IReadOnlyDictionary, Task, Task, Action, Task, ToggleButton

### Community 15 - "ProcessPresence"
Cohesion: 0.09
Nodes (18): DateTime, IReadOnlyList, MachineHistory, DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence (+10 more)

### Community 16 - "xunit"
Cohesion: 0.11
Nodes (5): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, nextcalibur_core_power_plantuning_acdc, system_xml_linq, xunit

### Community 17 - "Nextcalibur.App"
Cohesion: 0.11
Nodes (22): Nextcalibur.App, Nextcalibur.App.Controls, system_drawing, system_linq, system_windows, system_windows_controls, system_windows_controls_button, system_windows_controls_checkbox (+14 more)

### Community 18 - "TextBlock"
Cohesion: 0.05
Nodes (41): Detail, PercentText, BannerBody, BannerTitle, BrightnessValue, CpuClock, CpuFan, CpuTemp (+33 more)

### Community 19 - "CoolingPolicyTests"
Cohesion: 0.08
Nodes (26): At, Margin, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc (+18 more)

### Community 20 - "RunawayWatchTests"
Cohesion: 0.10
Nodes (25): CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet, Start (+17 more)

### Community 21 - "SystemMode"
Cohesion: 0.12
Nodes (18): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, Ours (+10 more)

### Community 22 - "GameLibrary"
Cohesion: 0.11
Nodes (14): Dictionary, GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, IReadOnlyList, Path, Regex (+6 more)

### Community 23 - "NetworkCostTests"
Cohesion: 0.11
Nodes (26): Answer, Bytes, Count, HttpMessageHandler, CancellationToken, ConcurrentDictionary, HttpClient, Task (+18 more)

### Community 24 - ".OnLoaded"
Cohesion: 0.09
Nodes (8): Asynchronous dispatcher & UI thread hardening, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, EventArgs, IProgress

### Community 25 - ".RequestRestart"
Cohesion: 0.09
Nodes (10): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr, MarshalAs (+2 more)

### Community 26 - ".StartSlowTimer"
Cohesion: 0.08
Nodes (7): SolidColorBrush, DllImport, IntPtr, Name, Pid, Func, Task

### Community 27 - ".Info"
Cohesion: 0.09
Nodes (6): Guid, Ours, Plan, MessageBoxButton, MessageBoxResult, IReadOnlyDictionary

### Community 28 - "EcMailbox"
Cohesion: 0.11
Nodes (15): ArgumentException, Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox (+7 more)

### Community 29 - "Nextcalibur.Core.Configuration"
Cohesion: 0.11
Nodes (10): Nextcalibur.Core.Security, Nextcalibur.Core.Configuration, Nextcalibur.Core.Tests.Attacks, system_security_accesscontrol, system_security_cryptography, system_security_cryptography_x509certificates, system_security_principal, system_text_json (+2 more)

### Community 30 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 31 - "Fact"
Cohesion: 0.13
Nodes (11): IList, GpuLoad, GpuConfiguration, DateTimeOffset, ThermalSample, Fact, GpuModeTests, Discrete (+3 more)

### Community 32 - "UserPresence"
Cohesion: 0.13
Nodes (14): LastInputInfo, MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr (+6 more)

### Community 34 - "Button"
Cohesion: 0.06
Nodes (30): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, GameScanButton (+22 more)

### Community 35 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 36 - "CrashGuard"
Cohesion: 0.11
Nodes (19): Session, DateTimeOffset, Exception, List, TimeSpan, CrashGuard, SafeMode, UncleanPrevious (+11 more)

### Community 37 - ".ModeName"
Cohesion: 0.12
Nodes (7): AssemblyInformationalVersionAttribute, Task, FrameworkElement, Func, Grid, List, ModeChoice

### Community 38 - "Program"
Cohesion: 0.11
Nodes (7): ConsoleColor, B, G, R, Program, HardwareSupport, ThermalReader

### Community 39 - ".For"
Cohesion: 0.13
Nodes (13): AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, Baseline (+5 more)

### Community 40 - "Fact"
Cohesion: 0.15
Nodes (9): Func, IEnumerable, IReadOnlyList, Version, RetirementEntry, Trace, Fact, FootprintTests (+1 more)

### Community 41 - ".Choose"
Cohesion: 0.12
Nodes (19): IEnumerable, IReadOnlyList, IdleLoadPicker, RunningItem, RunningKind, App, Background, Service (+11 more)

### Community 42 - "Api"
Cohesion: 0.11
Nodes (23): Api, GetSettingFn, InitializeFn, ProfileOutFn, SessionFn, SessionOutFn, Set, SetSettingFn (+15 more)

### Community 43 - "ColourWheel"
Cohesion: 0.10
Nodes (19): BitmapSource, Control, DependencyObject, DependencyPropertyChangedEventArgs, Ellipse, Hue, Saturation, Canvas (+11 more)

### Community 44 - "LedState"
Cohesion: 0.14
Nodes (17): B, G, LedBrightness, R, LedState, ActiveProfile, BrightnessPercent, Current (+9 more)

### Community 45 - "ResourceDictionary"
Cohesion: 0.10
Nodes (27): ActualWidth, Bar, Bg, Blade, Box, CardBorder, Grip, Guard (+19 more)

### Community 46 - "BatteryModePolicy"
Cohesion: 0.15
Nodes (11): DllImport, BatteryModePolicy, QuietMode, Remembered, PowerSource, SystemPowerStatus, SystemPowerStatus, Fact (+3 more)

### Community 47 - "Dependency"
Cohesion: 0.11
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 48 - ".SignatureIsValid"
Cohesion: 0.15
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 49 - "CeilingTestWindow"
Cohesion: 0.14
Nodes (12): CancellationTokenSource, Button, CancellationToken, CheckBox, Func, StackPanel, Task, TextBlock (+4 more)

### Community 50 - ".Check"
Cohesion: 0.13
Nodes (10): CommonAce, RawSecurityDescriptor, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported (+2 more)

### Community 51 - ".Main"
Cohesion: 0.16
Nodes (7): The threat model, and what the code does about it, Application, DllImport, Mutex, App, OverlayRepairPending, STAThread

### Community 52 - "Integrity"
Cohesion: 0.12
Nodes (11): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvapiPath, NvmlPath (+3 more)

### Community 53 - ".ShowResult"
Cohesion: 0.15
Nodes (10): List, IEnumerable, IReadOnlyList, TimeSpan, CeilingStep, CeilingTest, Fact, InlineData (+2 more)

### Community 54 - "DevicePowerSaving"
Cohesion: 0.17
Nodes (12): TextBlock, ToggleButton, AcDc, Guid, IEnumerable, DevicePowerSaving, Off, DeviceSaving (+4 more)

### Community 55 - ".Get"
Cohesion: 0.16
Nodes (12): ArgumentNullException, FirmwareModeReading, GpuMode, Discrete, Hybrid, Uma, GpuModeService, SwitchOutcome (+4 more)

### Community 56 - "Authenticode"
Cohesion: 0.25
Nodes (14): CatalogInfo, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle, Authenticode (+6 more)

### Community 57 - "TestPreflight"
Cohesion: 0.17
Nodes (11): Display, IoCounters, ProcessLook, ConcurrentDictionary, Dictionary, DllImport, IntPtr, TimeSpan (+3 more)

### Community 58 - ".PlaceTourStep"
Cohesion: 0.11
Nodes (10): SizeChangedEventArgs, FrameworkElement, KeyEventArgs, RoutedEventArgs, TourStep, Body, SectionName, Title (+2 more)

### Community 59 - ".Read"
Cohesion: 0.12
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 60 - "FirmwareInventory"
Cohesion: 0.09
Nodes (22): IReadOnlyList, FirmwareInventory, BatteryChemistry, BatteryDesignMwh, BatteryName, BiosDate, BiosRelease, BiosVendor (+14 more)

### Community 61 - "UpdateService"
Cohesion: 0.11
Nodes (13): CancelEventArgs, DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService, Available (+5 more)

### Community 62 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 63 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 64 - "MachineScanRecord"
Cohesion: 0.09
Nodes (22): DateTime, List, MachineScanRecord, BiosDate, BiosRelease, BiosVendor, BiosVersion, BoardProduct (+14 more)

### Community 65 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 66 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 67 - ".OnSystemModeChanged"
Cohesion: 0.18
Nodes (4): PowerModeChangedEventArgs, Guid, Ours, Plan

### Community 68 - ".CheckProgram"
Cohesion: 0.20
Nodes (5): Fact, IntegrityTests, Current, Exe, Manifest

### Community 69 - "IdleSetting"
Cohesion: 0.16
Nodes (12): Min, AcDc, Guid, IdleSetting, DisplayOff, Sleep, IdleTimeouts, Timeouts (+4 more)

### Community 70 - "Elevation"
Cohesion: 0.19
Nodes (5): dynamic, Elevation, Fact, Fact, TaskFolderTests

### Community 71 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 72 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 73 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 74 - "CpuPowerReader"
Cohesion: 0.24
Nodes (5): DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 75 - ".Dictionary"
Cohesion: 0.15
Nodes (9): IReadOnlyDictionary, Stable, Dictionary, Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests (+1 more)

### Community 76 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 77 - "ModeProfileTests"
Cohesion: 0.22
Nodes (7): MemberData, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 78 - ".ApplyFnShortcuts"
Cohesion: 0.14
Nodes (3): DllImport, IntPtr, KeyEventArgs

### Community 79 - "TemperatureFusion"
Cohesion: 0.25
Nodes (9): DateTime, TimeSpan, TemperatureFusion, Offset, RegisterSetAside, Value, DateTime, Fact (+1 more)

### Community 80 - "CpuLimitsTests"
Cohesion: 0.14
Nodes (9): ArgumentOutOfRangeException, Now, Offset, SinceBoot, IReadOnlyList, Fact, CpuLimitsTests, ThermFlags (+1 more)

### Community 81 - ".LoadDefinitionsAsync"
Cohesion: 0.22
Nodes (7): Task, GpuPowerLimits, IEnumerable, PowerDefinition, PowerDefinitions, Fact, PowerDefinitionsTests

### Community 82 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 83 - "CoreLoad"
Cohesion: 0.15
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 84 - "IntPtr"
Cohesion: 0.21
Nodes (7): DllImport, IntPtr, ScreenSaver, DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 85 - "CpuLimits"
Cohesion: 0.16
Nodes (10): Bit, PowerInfo, PowerLimits, Ratio, IReadOnlySet, CpuLimits, PowerInfo, PowerLimits (+2 more)

### Community 86 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 87 - ".Migrate"
Cohesion: 0.17
Nodes (5): Limits, SystemMode, InlineData, Theory, SettingsTests

### Community 88 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 89 - "Name"
Cohesion: 0.20
Nodes (9): Cpu, Name, Dictionary, IReadOnlyList, TimeSpan, ProcessLoad, TopProcesses, Fact (+1 more)

### Community 90 - "DevicePowerState"
Cohesion: 0.28
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 91 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 92 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 93 - "ThemeService"
Cohesion: 0.14
Nodes (11): Theme, Theme, Dark, Light, ThemePreference, Dark, Light, System (+3 more)

### Community 94 - "Hardware Protocol"
Cohesion: 0.14
Nodes (14): 1. Transport — ACPI-WMI mailbox, 3. Thermal / fan — `a1 = 0x0200`, 4. LED — `a1 = 0x0100`, 6. Safety rules, 7. Not supported, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`) (+6 more)

### Community 95 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 96 - ".Set"
Cohesion: 0.30
Nodes (7): PowerThrottlingState, DllImport, IntPtr, EfficiencyMode, PowerThrottlingState, Fact, EfficiencyModeTests

### Community 97 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 98 - "AcDc"
Cohesion: 0.21
Nodes (6): IReadOnlyList, AcDc, InlineData, Theory, Fact, IdleTimeoutsTests

### Community 99 - "LedEffect"
Cohesion: 0.14
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 100 - "PendingRestartInfo"
Cohesion: 0.18
Nodes (7): DateTime, Dictionary, List, PendingRestartInfo, BootTimeUtc, ReasonArguments, Reasons

### Community 101 - "InstallFolderGuard"
Cohesion: 0.30
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 102 - "Log"
Cohesion: 0.26
Nodes (3): Exception, Log, Folder

### Community 103 - ".RunAndRecord"
Cohesion: 0.22
Nodes (6): JsonSerializerOptions, MachineScan, Firmware, RecordPath, Fact, MachineScanTests

### Community 104 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 105 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 106 - ".HasAccess"
Cohesion: 0.28
Nodes (5): SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 107 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 108 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 109 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 110 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 111 - "Nextcalibur 0.5.4"
Cohesion: 0.17
Nodes (11): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+3 more)

### Community 112 - ".Pick"
Cohesion: 0.17
Nodes (7): MouseEventArgs, MouseButtonEventArgs, Size, Arrow, Point, Tick, Path

### Community 113 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 114 - "StackPanel"
Cohesion: 0.17
Nodes (12): BannerActions, CeilingRows, DefinitionRows, DialogActions, DriveTextStack, FnShortcutRows, IdleLoadRows, IdleRows (+4 more)

### Community 115 - ".RefreshIdleRows"
Cohesion: 0.27
Nodes (3): ComboBox, Grid, Guid

### Community 116 - "PackageTemperatureTests"
Cohesion: 0.41
Nodes (4): Fact, InlineData, Theory, PackageTemperatureTests

### Community 117 - "Nextcalibur.Core.Dependencies"
Cohesion: 0.35
Nodes (4): Nextcalibur.Core.Dependencies, system_collections_concurrent, system_net, system_net_http

### Community 118 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 121 - ".Sanitised"
Cohesion: 0.33
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 122 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 123 - ".Parse"
Cohesion: 0.29
Nodes (4): List, MemoryModule, Fact, FirmwareInventoryTests

### Community 124 - "SupportVerdict"
Cohesion: 0.22
Nodes (9): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites (+1 more)

### Community 125 - "ElevationPolicyTests"
Cohesion: 0.29
Nodes (5): InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 126 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 127 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 128 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 129 - "NoticeGate"
Cohesion: 0.25
Nodes (7): Key, Show, Action, List, NoticeGate, ShouldWait, WaitingCount

### Community 130 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 131 - ".IsSystemFileSignedBy"
Cohesion: 0.39
Nodes (3): Fact, SystemFileTrustTests, Cmd

### Community 132 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 133 - "CeilingTests"
Cohesion: 0.31
Nodes (5): Baseline, Fact, InlineData, Theory, CeilingTests

### Community 134 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 135 - "Nextcalibur 0.5.5"
Cohesion: 0.25
Nodes (6): Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture

### Community 136 - "Toasts"
Cohesion: 0.32
Nodes (5): Action, Toasts, Balloon, windows_data_xml_dom, windows_ui_notifications

### Community 138 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 139 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 140 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 141 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 142 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 143 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 144 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 145 - "SetBy"
Cohesion: 0.29
Nodes (7): SetBy, Intel, LaptopMaker, Nextcalibur, Nvidia, Windows, You

### Community 146 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 147 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 148 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 149 - "Nextcalibur 0.7.0"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.7.0, Tested on, Updated

### Community 150 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 151 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 152 - "ComboBox"
Cohesion: 0.33
Nodes (6): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, SettingNvPower, ComboBox

### Community 153 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 154 - "PawnIoModuleTests.cs"
Cohesion: 0.33
Nodes (4): system_reflection, InlineData, Theory, PawnIoModuleTests

### Community 155 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 156 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 157 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 158 - "PART_Track"
Cohesion: 0.40
Nodes (5): PART_Track, RootGrid, Sword, Grid, Track

### Community 161 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 163 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 164 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 165 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 167 - "2. Command structure"
Cohesion: 0.50
Nodes (4): 2. Command structure, Call sequence, Command families (`a0`), Subsystems (`a1`)

### Community 168 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 169 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 170 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 171 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 172 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 174 - "Toggle"
Cohesion: 0.67
Nodes (3): IsDropDownOpen, Toggle, ToggleButton

### Community 176 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

### Community 177 - "EffectPanel"
Cohesion: 0.67
Nodes (3): EffectPanel, ProfileRow, UniformGrid

### Community 178 - "PART_ContentHost"
Cohesion: 0.67
Nodes (3): PART_ContentHost, SettingsScroll, ScrollViewer

## Knowledge Gaps
- **599 isolated node(s):** `OverlayRepairPending`, `Accepted`, `SelectedColour`, `VisualChildrenCount`, `Diameter` (+594 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1147 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **29 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `FnShortcut`, `ProgramRule`, `GpuClockReader`, `AppSettings`, `PowerOverlayService`, `Nextcalibur 0.5.5`, `WindowsFaults`, `TrayPresence`, `Nextcalibur.Core.Hardware`, `Window`, `PlanTuning`, `RadioButton`, `.Get`, `TourPage`, `Nextcalibur.App`, `CoolingPolicyTests`, `RunawayWatchTests`, `SystemMode`, `.OnBrightnessChanged`, `.OnLoaded`, `.RequestRestart`, `.StartSlowTimer`, `.Info`, `EcMailbox`, `Nextcalibur.Core.Configuration`, `SystemInfo`, `Fact`, `UserPresence`, `LedZone`, `Button`, `.ModeName`, `Program`, `.For`, `.Choose`, `BatteryModePolicy`, `CeilingTestWindow`, `Integrity`, `DevicePowerSaving`, `.Get`, `.PlaceTourStep`, `.Read`, `UpdateService`, `.OnSystemModeChanged`, `IdleSetting`, `BacklightKeyWatcher`, `DependencyStatus`, `CpuPowerReader`, `.ApplyFnShortcuts`, `TemperatureFusion`, `.LoadDefinitionsAsync`, `CoreLoad`, `LedController`, `ThemeService`, `AcDc`, `LedEffect`, `Grid`, `.Pick`, `Strings`, `.RefreshIdleRows`, `NvidiaDriverState`, `SupportVerdict`?**
  _High betweenness centrality (0.526) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Button`, `Keycaps`, `DriveGauge`, `Grid`, `Text`, `RadioButton`, `DialogCheck`, `EffectPanel`, `TextBlock`, `StackPanel`, `PART_ContentHost`, `DriveDot`, `DriveList`, `.OnBrightnessChanged`, `ComboBox`, `TourCanvas`, `Wheel`?**
  _High betweenness centrality (0.092) - this node is a cross-community bridge._
- **Why does `AppSettings` connect `AppSettings` to `MainWindow`, `WindowsFaultsTests`, `ProtectedStore`, `PendingRestartInfo`, `Fact`, `TrayPresence`, `NduFix`, `Strings`, `ThemeService`, `SystemMode`, `.Migrate`, `.OnLoaded`, `.Sanitised`, `Nextcalibur.Core.Configuration`?**
  _High betweenness centrality (0.084) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `OverlayRepairPending`, `Accepted`, `SelectedColour` to the rest of the system?**
  _599 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.021622297212848394 - nodes in this community are weakly interconnected._
- **Should `FnShortcut` be split into smaller, more focused modules?**
  _Cohesion score 0.0546583850931677 - nodes in this community are weakly interconnected._