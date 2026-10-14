# Graph Report - nextcalibur-control-center  (2026-10-14)

## Corpus Check
- 197 files · ~246,746 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3348 nodes · 7445 edges · 190 communities (156 shown, 34 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 437 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `19f09c09`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- AppSettings
- .Get
- FnShortcut
- Authenticode
- .CheckProgram
- GpuClockReader
- RunawayWatchTests
- CpuLimits
- ProtectedStore
- WindowsFaults
- IShellLinkW
- Window
- .Warn
- RadioButton
- Nextcalibur.Core.Hardware
- PlanTuning
- .Info
- CoolingPolicyTests
- SystemInfo
- NetworkCostTests
- UserPresence
- .StartSlowTimer
- Nextcalibur.App
- .Check
- Button
- Fact
- SystemMode
- WindowsFaultsTests
- SystemTools
- Api
- .RequestRestart
- .LoadSystemMode
- CeilingTestWindow
- TrayPresence
- Nextcalibur.Core.Security
- LedState
- system_runtime_interopservices
- .Survey
- .Get
- Dependency
- MachineScanRecord
- EcMailbox
- MemoryTrimmer
- Elevation
- PowerOverlayService
- .For
- DevicePowerSaving
- ToggleButton
- .ShouldAsk
- CpuPowerReader
- .Ask
- DotNetRuntimeDependency
- RestorePoint
- Nextcalibur.Core.csproj
- ProgramRule
- DependencyStatus
- .RefreshBanner
- analyse-autopsy.py
- ModeProfileTests
- UpdateService
- ProcessPresence
- .Dictionary
- Fact
- SmiCommand
- .Execute
- IDisposable
- .Save
- ResourceDictionary
- RuntimeDependencyTests
- CeilingStep
- DevicePowerState
- .Scan
- LedController
- .GetColour
- ColourWheel
- .LoadGpuMode
- FanGauge
- Nextcalibur.Core.Configuration
- Unelevated
- DonutGauge
- MachineHistory
- BatteryModePolicy
- IdleSetting
- SegmentedBar
- Nextcalibur Control Center
- ThemeService
- .Record
- LogInjectionTests
- .Capture
- AcDc
- Grid
- .RefreshIdleRows
- LedBrightness
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Strings
- LedEffect
- PackageTemperatureTests
- SystemModes
- Hardware Protocol
- Nextcalibur 0.5.3
- StackPanel
- NvidiaDriverState
- SupportVerdict
- IntPtr
- Integrity.cs
- Border
- .RunLighting
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- BacklightKeyWatcher
- VendorSoftware
- PawnIoDependency
- Probe
- CeilingTests
- Nextcalibur 0.5.1
- .ScreenSaverChosen
- NotificationState
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- .Pick
- .ForegroundProcess
- TourPage
- NduFix
- BatteryModePolicy.cs
- Log-Graphics.ps1
- Privacy
- Nextcalibur 0.6.2
- Security
- .OnBrightnessChanged
- ComboBox
- LedZone
- MachineWideGate
- RunningKind
- Notice
- Nextcalibur 0.5.11
- AppIdentity.cs
- TourStep
- .Set
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- Nextcalibur 0.6.1
- ProgramRules
- .ArrangeOverride
- .Modules_are_the_released_ones
- Tools
- 0.5.7.md
- CpuWarnValue
- DialogCheck
- Toasts.cs
- .Work_that_must_not_be_interrupted
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
1. `MainWindow` - 394 edges
2. `Window` - 188 edges
3. `AppSettings` - 85 edges
4. `Nextcalibur.Core.Hardware` - 67 edges
5. `SystemMode` - 66 edges
6. `RadioButton` - 41 edges
7. `Nextcalibur.Core.Configuration` - 41 edges
8. `Nextcalibur.Core.Power` - 41 edges
9. `WindowsFaultsTests` - 40 edges
10. `TextBlock` - 39 edges

## Surprising Connections (you probably didn't know these)
- `Deep memory & resource leak eradication` --references--> `MainWindow`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.App/MainWindow.Audit.cs
- `Deep memory & resource leak eradication` --references--> `CpuPowerReader`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.Core/Hardware/CpuPowerReader.cs
- `Zero-bottleneck gaming & heavy workload architecture` --references--> `WindowsFaults`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.Core/Hardware/WindowsFaults.cs
- `Fix GPU Switch Restart Prompt & Windows Privilege Adjustment` --references--> `TokenPrivileges`  [INFERRED]
  docs/releases/0.5.8.md → src/Nextcalibur.App/MainWindow.xaml.cs
- `Zero-bottleneck gaming & heavy workload architecture` --references--> `MemoryTrimmer`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.Core/Hardware/MemoryTrimmer.cs

## Import Cycles
- None detected.

## Communities (190 total, 34 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.03
Nodes (68): DeferredOverheat, GameSession, HwndSource, ObservableCollection, PluggedIn, Queue, SizeChangedEventArgs, MainWindow (+60 more)

### Community 1 - "AppSettings"
Cohesion: 0.03
Nodes (63): DateTime, Dictionary, FnShortcut, JsonElement, Limits, List, ProgramRule, SystemMode (+55 more)

### Community 2 - ".Get"
Cohesion: 0.06
Nodes (21): AssemblyInformationalVersionAttribute, Asynchronous dispatcher & UI thread hardening, GameEnd, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner (+13 more)

### Community 3 - "FnShortcut"
Cohesion: 0.05
Nodes (39): Nextcalibur.Core.Input, Problem, Detail, Dictionary, DllImport, IntPtr, DllImport, IEnumerable (+31 more)

### Community 4 - "Authenticode"
Cohesion: 0.06
Nodes (38): CatalogInfo, Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on (+30 more)

### Community 5 - ".CheckProgram"
Cohesion: 0.06
Nodes (28): Lazy, ProcessModule, FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Dictionary, HashSet (+20 more)

### Community 6 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 7 - "RunawayWatchTests"
Cohesion: 0.06
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 8 - "CpuLimits"
Cohesion: 0.06
Nodes (31): Bit, IReadOnlySet, Now, Offset, PowerInfo, PowerLimits, Ratio, SinceBoot (+23 more)

### Community 9 - "ProtectedStore"
Cohesion: 0.09
Nodes (20): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+12 more)

### Community 10 - "WindowsFaults"
Cohesion: 0.06
Nodes (35): Process, ProcessorPerformance, Share, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count (+27 more)

### Community 11 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 12 - "Window"
Cohesion: 0.08
Nodes (46): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+38 more)

### Community 13 - ".Warn"
Cohesion: 0.09
Nodes (11): The threat model, and what the code does about it, Application, DllImport, Mutex, App, OverlayRepairPending, RegistryKey, Footprint (+3 more)

### Community 14 - "RadioButton"
Cohesion: 0.06
Nodes (39): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeBalanced, ModeGaming (+31 more)

### Community 15 - "Nextcalibur.Core.Hardware"
Cohesion: 0.12
Nodes (7): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, Nextcalibur.Core.Hardware, nextcalibur_core_power_plantuning_acdc, system_text_regularexpressions, system_xml_linq, xunit

### Community 16 - "PlanTuning"
Cohesion: 0.13
Nodes (18): Floor, Marker, Original, Action, AcDc, DllImport, Guid, IEnumerable (+10 more)

### Community 17 - ".Info"
Cohesion: 0.10
Nodes (6): CancelEventArgs, Guid, Ours, Plan, List, ModeChoice

### Community 18 - "CoolingPolicyTests"
Cohesion: 0.08
Nodes (26): At, Margin, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc (+18 more)

### Community 19 - "SystemInfo"
Cohesion: 0.08
Nodes (21): MemoryStatusEx, DriveUse, INotifyPropertyChanged, DriveRow, Name, Percent, PercentText, DllImport (+13 more)

### Community 20 - "NetworkCostTests"
Cohesion: 0.11
Nodes (26): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+18 more)

### Community 21 - "UserPresence"
Cohesion: 0.11
Nodes (14): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, Point, Size, DateTime, DllImport (+6 more)

### Community 22 - ".StartSlowTimer"
Cohesion: 0.07
Nodes (7): SolidColorBrush, IReadOnlyList, GameEnd, Func, Action, Toasts, UninstallCommand

### Community 23 - "Nextcalibur.App"
Cohesion: 0.13
Nodes (20): Nextcalibur.App, Nextcalibur.App.Controls, system_linq, system_windows, system_windows_controls, system_windows_controls_button, system_windows_controls_checkbox, system_windows_controls_combobox (+12 more)

### Community 24 - ".Check"
Cohesion: 0.11
Nodes (14): CommonAce, RawSecurityDescriptor, RegistryKey, SecurityIdentifier, MailboxAccess, MailboxAvailability, AccessNotGranted, Available (+6 more)

### Community 25 - "Button"
Cohesion: 0.07
Nodes (25): IsChecked, BannerDismissButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, HideToTrayButton, MinimiseButton, OpenLogButton (+17 more)

### Community 26 - "Fact"
Cohesion: 0.13
Nodes (10): IList, GpuLoad, DateTimeOffset, ThermalSample, Fact, GpuModeTests, Discrete, Hybrid (+2 more)

### Community 27 - "SystemMode"
Cohesion: 0.14
Nodes (15): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, Ours, Plan (+7 more)

### Community 29 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 30 - "Api"
Cohesion: 0.10
Nodes (23): Api, GetSettingFn, InitializeFn, ProfileOutFn, SessionFn, SessionOutFn, Set, SetSettingFn (+15 more)

### Community 31 - ".RequestRestart"
Cohesion: 0.11
Nodes (10): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, IntPtr, MarshalAs, TokenPrivileges (+2 more)

### Community 32 - ".LoadSystemMode"
Cohesion: 0.10
Nodes (9): Option, PowerModeChangedEventArgs, Button, EventArgs, Guid, IEnumerable, Ours, Plan (+1 more)

### Community 33 - "CeilingTestWindow"
Cohesion: 0.14
Nodes (13): CancellationTokenSource, Button, CancellationToken, CheckBox, Func, List, StackPanel, Task (+5 more)

### Community 34 - "TrayPresence"
Cohesion: 0.11
Nodes (12): ContextMenuStrip, Deep memory & resource leak eradication, EventHandler, Icon, Item, NotifyIcon, List, Ms (+4 more)

### Community 35 - "Nextcalibur.Core.Security"
Cohesion: 0.13
Nodes (9): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_security_cryptography_x509certificates, system_text (+1 more)

### Community 36 - "LedState"
Cohesion: 0.11
Nodes (19): Dictionary, LedBrightness, LedProfile, BrightnessPercent, Colours, Effect, LedState, ActiveProfile (+11 more)

### Community 37 - "system_runtime_interopservices"
Cohesion: 0.11
Nodes (9): microsoft_win32, microsoft_win32_safehandles, system_componentmodel, system_diagnostics, system_io, system_management, system_reflection, system_runtime_compilerservices (+1 more)

### Community 38 - ".Survey"
Cohesion: 0.15
Nodes (7): Func, IEnumerable, IReadOnlyList, RetirementEntry, Trace, Version, FootprintTests

### Community 39 - ".Get"
Cohesion: 0.14
Nodes (13): ArgumentNullException, FirmwareModeReading, FirmwareModeReading, GpuMode, Discrete, Hybrid, Uma, GpuModeService (+5 more)

### Community 40 - "Dependency"
Cohesion: 0.11
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 41 - "MachineScanRecord"
Cohesion: 0.11
Nodes (18): DateTime, JsonSerializerOptions, List, MachineScan, RecordPath, MachineScanRecord, BiosVendor, BiosVersion (+10 more)

### Community 42 - "EcMailbox"
Cohesion: 0.16
Nodes (5): ConsoleColor, ManagementScope, Program, EcMailbox, ThermalReader

### Community 43 - "MemoryTrimmer"
Cohesion: 0.11
Nodes (17): Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture, MEMORYSTATUSEX, DateTime, Dictionary, DllImport (+9 more)

### Community 44 - "Elevation"
Cohesion: 0.14
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 45 - "PowerOverlayService"
Cohesion: 0.15
Nodes (11): DllImport, Guid, IReadOnlyList, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, PowerModeOption (+3 more)

### Community 46 - ".For"
Cohesion: 0.15
Nodes (13): AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, Baseline (+5 more)

### Community 47 - "DevicePowerSaving"
Cohesion: 0.17
Nodes (12): TextBlock, ToggleButton, AcDc, Guid, IEnumerable, DevicePowerSaving, Off, DeviceSaving (+4 more)

### Community 48 - "ToggleButton"
Cohesion: 0.08
Nodes (21): LedPower, OverheatWarningToggle, SelectAll, SettingAskGameMode, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice (+13 more)

### Community 49 - ".ShouldAsk"
Cohesion: 0.16
Nodes (10): HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, Fact, InlineData, Theory (+2 more)

### Community 50 - "CpuPowerReader"
Cohesion: 0.21
Nodes (6): ArgumentOutOfRangeException, DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 51 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 52 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 53 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 54 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 55 - "ProgramRule"
Cohesion: 0.20
Nodes (12): DateTime, IEnumerable, IReadOnlyDictionary, ProgramRule, Enabled, Process, DateTime, Dictionary (+4 more)

### Community 56 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 57 - ".RefreshBanner"
Cohesion: 0.19
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 58 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 59 - "ModeProfileTests"
Cohesion: 0.22
Nodes (7): MemberData, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 60 - "UpdateService"
Cohesion: 0.14
Nodes (13): DateTime, DispatcherTimer, HashSet, IProgress, Task, TimeSpan, UpdateService, Available (+5 more)

### Community 61 - "ProcessPresence"
Cohesion: 0.23
Nodes (8): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, Fact, ProcessPresenceTests, Self

### Community 62 - ".Dictionary"
Cohesion: 0.15
Nodes (9): IReadOnlyDictionary, Stable, Dictionary, Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests (+1 more)

### Community 63 - "Fact"
Cohesion: 0.14
Nodes (10): RepairOutcome, Empty, Fact, Task, CleanMachineTests, Guarded, Unguarded, CoexistencePolicyTests (+2 more)

### Community 64 - "SmiCommand"
Cohesion: 0.20
Nodes (6): ArgumentException, SmiCommand, Fact, InlineData, Theory, SmiCommandTests

### Community 65 - ".Execute"
Cohesion: 0.14
Nodes (12): Exception, ManagementObject, Func, EcMailboxUnavailableException, SmiFamily, Read, Write, SmiSubsystem (+4 more)

### Community 66 - "IDisposable"
Cohesion: 0.18
Nodes (7): IDisposable, Held, ThermalProfile, Fact, InlineData, Theory, ThermalProfileTests

### Community 67 - ".Save"
Cohesion: 0.13
Nodes (5): Slider, Action, ToggleButton, EventArgs, TextBox

### Community 68 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 69 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 70 - "CeilingStep"
Cohesion: 0.21
Nodes (7): IEnumerable, IReadOnlyList, TimeSpan, CeilingStep, CeilingTest, Fact, CeilingTestTests

### Community 71 - "DevicePowerState"
Cohesion: 0.23
Nodes (6): DevPropKey, Discrete GPU sleep protection in Hybrid mode, DllImport, Guid, DevicePowerState, DevPropKey

### Community 72 - ".Scan"
Cohesion: 0.18
Nodes (10): Display, IoCounters, ProcessLook, Dictionary, DllImport, IntPtr, TimeSpan, IoCounters (+2 more)

### Community 73 - "LedController"
Cohesion: 0.24
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 74 - ".GetColour"
Cohesion: 0.24
Nodes (7): B, G, R, Fact, InlineData, Theory, LedStateTests

### Community 75 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 77 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 78 - "Nextcalibur.Core.Configuration"
Cohesion: 0.15
Nodes (7): Nextcalibur.Core, Nextcalibur.Core.Configuration, system_drawing, system_globalization, system_security, system_text_json_serialization, system_windows_forms

### Community 79 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 80 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 81 - "MachineHistory"
Cohesion: 0.28
Nodes (6): DateTime, IReadOnlyList, MachineHistory, DateTime, Fact, MachineHistoryTests

### Community 82 - "BatteryModePolicy"
Cohesion: 0.35
Nodes (5): BatteryModePolicy, QuietMode, Remembered, Fact, BatteryModePolicyTests

### Community 83 - "IdleSetting"
Cohesion: 0.24
Nodes (8): AcDc, Guid, IReadOnlyList, IdleSetting, DisplayOff, Sleep, IdleTimeouts, Timeouts

### Community 84 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 85 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 86 - "ThemeService"
Cohesion: 0.15
Nodes (11): Theme, Theme, Dark, Light, ThemePreference, Dark, Light, System (+3 more)

### Community 87 - ".Record"
Cohesion: 0.20
Nodes (7): IReadOnlyDictionary, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Fact, AuditLogTests

### Community 88 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 89 - ".Capture"
Cohesion: 0.26
Nodes (6): EnumWindowsProc, IO_COUNTERS, PROCESS_MEMORY_COUNTERS, ProcessMetrics, DllImport, IntPtr

### Community 90 - "AcDc"
Cohesion: 0.26
Nodes (5): AcDc, InlineData, Theory, Fact, IdleTimeoutsTests

### Community 91 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 92 - ".RefreshIdleRows"
Cohesion: 0.24
Nodes (3): ComboBox, Grid, Guid

### Community 93 - "LedBrightness"
Cohesion: 0.18
Nodes (8): LedBrightness, Full, Half, Off, Fact, InlineData, Theory, BacklightKeyTests

### Community 94 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 95 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 96 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 97 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 98 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 99 - "LedEffect"
Cohesion: 0.15
Nodes (11): B, G, R, LedEffect, Blink, Breathing, ColourCycle, Heartbeat (+3 more)

### Community 100 - "PackageTemperatureTests"
Cohesion: 0.41
Nodes (4): Fact, InlineData, Theory, PackageTemperatureTests

### Community 101 - "SystemModes"
Cohesion: 0.23
Nodes (5): IReadOnlyList, SystemModes, InOrder, Fact, SystemModesTests

### Community 102 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 103 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 104 - "StackPanel"
Cohesion: 0.18
Nodes (11): BannerActions, CeilingRows, DialogActions, DriveTextStack, FnShortcutRows, IdleRows, PanelWindowsFaultsSubOptions, ProfileRow (+3 more)

### Community 105 - "NvidiaDriverState"
Cohesion: 0.27
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 106 - "SupportVerdict"
Cohesion: 0.20
Nodes (10): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+2 more)

### Community 107 - "IntPtr"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 108 - "Integrity.cs"
Cohesion: 0.31
Nodes (4): Nextcalibur.Cli, system_security_accesscontrol, system_security_cryptography, system_security_principal

### Community 109 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 111 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 112 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 113 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 114 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 115 - "BacklightKeyWatcher"
Cohesion: 0.22
Nodes (4): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo

### Community 116 - "VendorSoftware"
Cohesion: 0.31
Nodes (3): DateTime, TimeSpan, VendorSoftware

### Community 117 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 118 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 119 - "CeilingTests"
Cohesion: 0.31
Nodes (5): Baseline, Fact, InlineData, Theory, CeilingTests

### Community 120 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 121 - ".ScreenSaverChosen"
Cohesion: 0.43
Nodes (3): DllImport, IntPtr, ScreenSaver

### Community 122 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 123 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 124 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 125 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 126 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 127 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 128 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 129 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 130 - ".ForegroundProcess"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Name, Pid

### Community 131 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 133 - "BatteryModePolicy.cs"
Cohesion: 0.33
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 134 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 135 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 136 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 137 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 138 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 139 - "ComboBox"
Cohesion: 0.33
Nodes (6): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, SettingNvPower, ComboBox

### Community 140 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 141 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 142 - "RunningKind"
Cohesion: 0.33
Nodes (6): RunningItem, RunningKind, App, Background, Service, Transfer

### Community 143 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 144 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 145 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 146 - "TourStep"
Cohesion: 0.40
Nodes (5): TourStep, Body, SectionName, Title, TourPage

### Community 148 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 150 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 151 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 152 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 153 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 154 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 155 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 156 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 157 - "ProgramRules"
Cohesion: 0.50
Nodes (3): GeneratedRegex, Regex, ProgramRules

### Community 159 - ".Modules_are_the_released_ones"
Cohesion: 0.50
Nodes (3): InlineData, Theory, PawnIoModuleTests

### Community 160 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 163 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 164 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

## Knowledge Gaps
- **521 isolated node(s):** `OverlayRepairPending`, `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum` (+516 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1025 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **34 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `AppSettings`, `.Get`, `FnShortcut`, `.ForegroundProcess`, `TourPage`, `GpuClockReader`, `RunawayWatchTests`, `.CheckProgram`, `.OnBrightnessChanged`, `IShellLinkW`, `LedZone`, `Window`, `RadioButton`, `PlanTuning`, `.Info`, `CoolingPolicyTests`, `TourStep`, `SystemInfo`, `UserPresence`, `.StartSlowTimer`, `Nextcalibur.App`, `Button`, `Fact`, `SystemMode`, `.RequestRestart`, `.LoadSystemMode`, `CeilingTestWindow`, `TrayPresence`, `Nextcalibur.Core.Security`, `.Get`, `EcMailbox`, `PowerOverlayService`, `.For`, `DevicePowerSaving`, `ToggleButton`, `CpuPowerReader`, `ProgramRule`, `DependencyStatus`, `.RefreshBanner`, `UpdateService`, `IDisposable`, `.Save`, `LedController`, `.LoadGpuMode`, `Nextcalibur.Core.Configuration`, `BatteryModePolicy`, `IdleSetting`, `ThemeService`, `AcDc`, `Grid`, `.RefreshIdleRows`, `LedBrightness`, `Strings`, `LedEffect`, `NvidiaDriverState`, `SupportVerdict`, `.RunLighting`, `BacklightKeyWatcher`, `VendorSoftware`, `.ScreenSaverChosen`?**
  _High betweenness centrality (0.464) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `.Get`, `.OnBrightnessChanged`, `ComboBox`, `RadioButton`, `Keycaps`, `DriveGauge`, `Button`, `CpuWarnValue`, `DialogCheck`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `ToggleButton`, `Wheel`, `Grid`, `StackPanel`, `Border`, `Text`?**
  _High betweenness centrality (0.095) - this node is a cross-community bridge._
- **Why does `Nextcalibur.Core.Hardware` connect `Nextcalibur.Core.Hardware` to `RunawayWatchTests`, `CpuLimits`, `WindowsFaults`, `MachineWideGate`, `UserPresence`, `Nextcalibur.App`, `Fact`, `Api`, `Nextcalibur.Core.Security`, `system_runtime_interopservices`, `.Get`, `Elevation`, `.Execute`, `DevicePowerState`, `Nextcalibur.Core.Configuration`, `LedBrightness`, `NvidiaDriverState`, `SupportVerdict`, `Integrity.cs`, `BacklightKeyWatcher`?**
  _High betweenness centrality (0.078) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `OverlayRepairPending`, `SelectedColour`, `VisualChildrenCount` to the rest of the system?**
  _521 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.028403113822848727 - nodes in this community are weakly interconnected._
- **Should `AppSettings` be split into smaller, more focused modules?**
  _Cohesion score 0.033458177278401995 - nodes in this community are weakly interconnected._