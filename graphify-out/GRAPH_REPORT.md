# Graph Report - nextcalibur-control-center  (2026-10-15)

## Corpus Check
- 220 files · ~268,265 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3784 nodes · 8428 edges · 205 communities (172 shown, 33 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 502 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `19cad0d2`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- GpuClockReader
- AppSettings
- FnShortcut
- ProtectedStore
- .RuleRow
- GameLibrary
- .Info
- .Get
- RunawayWatchTests
- ProgramRule
- Nextcalibur.App
- UpdateService
- Window
- WindowsFaults
- IShellLinkW
- RadioButton
- xunit
- ProcessPresence
- PlanTuning
- Nextcalibur.Core.Hardware
- .Warn
- CoolingPolicyTests
- Fact
- NetworkCostTests
- CeilingTestWindow
- .RequestRestart
- SystemMode
- .StartSlowTimer
- .Main
- WindowsFaultsTests
- SystemInfo
- .RemoveMachineTraces
- UserPresence
- Button
- CpuLimits
- CrashGuard
- Nextcalibur.Core.Configuration
- InstallFolderGuard
- .Choose
- Api
- ColourWheel
- .Get
- ResourceDictionary
- LedState
- Dependency
- .PlaceTourStep
- .LoadDefinitionsAsync
- .SignatureIsValid
- Fact
- Fact
- TestPreflight
- Integrity
- CpuPowerReader
- .Save
- Authenticode
- .Check
- Program
- ModeProfile
- FirmwareInventory
- .For
- .OnSystemModeChanged
- ToggleButton
- .Read
- DotNetRuntimeDependency
- MachineScanRecord
- .Execute
- Nextcalibur.Core.csproj
- .CheckProgram
- RestorePoint
- .Ask
- MemoryTrimmer
- DependencyStatus
- .Dictionary
- DevicePowerSaving
- analyse-autopsy.py
- BacklightKeyWatcher
- DarkMenuColours
- PowerOverlayService
- TemperatureFusion
- Nextcalibur.Core.Security
- Hardware Protocol
- RuntimeDependencyTests
- CoreLoad
- .GetColour
- DevicePowerState
- EcMailbox
- Unelevated
- LedController
- FanGauge
- TrayPresence
- DonutGauge
- BatteryModePolicy
- Nextcalibur.Core.Tests.Attacks
- SegmentedBar
- .Set
- Nextcalibur Control Center
- .RunAndRecord
- .HasAccess
- .Write
- LogInjectionTests
- Grid
- PackageTemperatureTests
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- Strings
- StackPanel
- .RefreshIdleRows
- AcDc
- SystemModes
- Nextcalibur 0.5.3
- ThemeService
- SupportVerdict
- NoticeGate
- .Pick
- Border
- NvidiaDriverState
- .Parse
- .ClampFrequency
- .IsSystemFileSignedBy
- IntPtr
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- LedEffect
- ThermalSample
- PawnIoDependency
- Probe
- Nextcalibur 0.5.1
- Toasts
- NduFix
- .Recommended
- IntegrityKind
- Test-Ui.ps1
- Text
- .BuildMenu
- Nextcalibur 0.5.5
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- TourPage
- .Current
- Log-Graphics.ps1
- Privacy
- Nextcalibur 0.6.2
- Nextcalibur 0.7.0
- Security
- .OnBrightnessChanged
- ComboBox
- LedZone
- MachineWideGate
- PowerSource
- Notice
- Nextcalibur 0.5.11
- Nextcalibur 0.7.1
- AppIdentity.cs
- PART_Track
- .Read
- .FirmwareChanged
- .A_polling_interval_from_the_file_is_a_sane_one
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- SystemInfoTests.cs
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- Nextcalibur 0.6.1
- Tools
- Toggle
- 0.5.7.md
- CpuWarnValue
- DialogCheck
- EffectPanel
- PART_ContentHost
- .Paint
- .LimitsFor
- .LoadIcon
- ColGauge
- DialogProgress
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
1. `MainWindow` - 449 edges
2. `Window` - 199 edges
3. `AppSettings` - 96 edges
4. `Nextcalibur.Core.Hardware` - 77 edges
5. `SystemMode` - 69 edges
6. `Nextcalibur.Core.Power` - 51 edges
7. `Nextcalibur.Core.Configuration` - 50 edges
8. `Nextcalibur.Core.Tests` - 46 edges
9. `RadioButton` - 41 edges
10. `TextBlock` - 41 edges

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

## Communities (205 total, 33 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.02
Nodes (86): Added, DeferredOverheat, Found, GameSession, HwndSource, ObservableCollection, PluggedIn, Queue (+78 more)

### Community 1 - "GpuClockReader"
Cohesion: 0.06
Nodes (31): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, Detail, Func, Ok, SelfTest, DateTime (+23 more)

### Community 2 - "AppSettings"
Cohesion: 0.03
Nodes (66): DateTime, Dictionary, FnShortcut, JsonElement, List, ProgramRule, AppSettings, AcceptedVendorSoftware (+58 more)

### Community 3 - "FnShortcut"
Cohesion: 0.05
Nodes (42): INotifyPropertyChanged, Problem, DriveRow, Detail, Name, Percent, PercentText, Dictionary (+34 more)

### Community 4 - "ProtectedStore"
Cohesion: 0.07
Nodes (26): Content, DirectorySecurity, Hash, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Dictionary (+18 more)

### Community 5 - ".RuleRow"
Cohesion: 0.06
Nodes (22): AssemblyInformationalVersionAttribute, ItemsControl, Property, DependencyObject, DependencyProperty, Func, List, TextBlock (+14 more)

### Community 6 - "GameLibrary"
Cohesion: 0.07
Nodes (20): Folder, Dictionary, GeneratedRegex, HashSet, IEnumerable, IReadOnlyDictionary, IReadOnlyList, List (+12 more)

### Community 7 - ".Info"
Cohesion: 0.07
Nodes (9): Asynchronous dispatcher & UI thread hardening, Kept, Task, Guid, IReadOnlyList, Plan, Task, IProgress (+1 more)

### Community 8 - ".Get"
Cohesion: 0.08
Nodes (11): GameEnd, SolidColorBrush, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary (+3 more)

### Community 9 - "RunawayWatchTests"
Cohesion: 0.07
Nodes (34): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+26 more)

### Community 10 - "ProgramRule"
Cohesion: 0.06
Nodes (31): HashSet, IEnumerable, IReadOnlyDictionary, TimeSpan, GameSessions, DateTime, GeneratedRegex, IEnumerable (+23 more)

### Community 11 - "Nextcalibur.App"
Cohesion: 0.09
Nodes (25): Nextcalibur.App, Nextcalibur.App.Controls, system_componentmodel, system_drawing, system_io, system_linq, system_runtime_compilerservices, system_windows (+17 more)

### Community 12 - "UpdateService"
Cohesion: 0.06
Nodes (26): FileStream, IOException, DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService (+18 more)

### Community 13 - "Window"
Cohesion: 0.07
Nodes (48): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+40 more)

### Community 14 - "WindowsFaults"
Cohesion: 0.07
Nodes (35): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+27 more)

### Community 15 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 16 - "RadioButton"
Cohesion: 0.06
Nodes (42): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeBalanced, ModeDiscrete (+34 more)

### Community 17 - "xunit"
Cohesion: 0.09
Nodes (10): Nextcalibur.Core.Tests, Nextcalibur.Core.Input, Nextcalibur.Core.Power, nextcalibur_core_power_plantuning_acdc, system_reflection, system_xml_linq, InlineData, Theory (+2 more)

### Community 18 - "ProcessPresence"
Cohesion: 0.09
Nodes (18): DateTime, IReadOnlyList, MachineHistory, DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence (+10 more)

### Community 19 - "PlanTuning"
Cohesion: 0.13
Nodes (18): Floor, Marker, Original, Action, AcDc, DllImport, Guid, IEnumerable (+10 more)

### Community 20 - "Nextcalibur.Core.Hardware"
Cohesion: 0.10
Nodes (7): Nextcalibur.Cli, Nextcalibur.Core.Hardware, microsoft_win32_safehandles, system_diagnostics, system_management, system_runtime_interopservices, system_text

### Community 21 - ".Warn"
Cohesion: 0.08
Nodes (9): MessageBoxButton, MessageBoxResult, Exception, Log, Folder, IReadOnlyList, TimeSpan, Preflight (+1 more)

### Community 22 - "CoolingPolicyTests"
Cohesion: 0.08
Nodes (26): At, Margin, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc (+18 more)

### Community 23 - "Fact"
Cohesion: 0.11
Nodes (10): Func, IEnumerable, IReadOnlyList, Version, RetirementEntry, Trace, Fact, FootprintTests (+2 more)

### Community 24 - "NetworkCostTests"
Cohesion: 0.11
Nodes (26): Answer, Bytes, Count, HttpMessageHandler, CancellationToken, ConcurrentDictionary, HttpClient, Task (+18 more)

### Community 25 - "CeilingTestWindow"
Cohesion: 0.11
Nodes (18): CancellationTokenSource, Button, CancellationToken, CheckBox, Func, List, StackPanel, Task (+10 more)

### Community 26 - ".RequestRestart"
Cohesion: 0.09
Nodes (11): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr, MarshalAs (+3 more)

### Community 27 - "SystemMode"
Cohesion: 0.13
Nodes (15): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, Ours, Plan (+7 more)

### Community 28 - ".StartSlowTimer"
Cohesion: 0.07
Nodes (7): DllImport, IntPtr, Name, Pid, IReadOnlyList, GameEnd, Func

### Community 29 - ".Main"
Cohesion: 0.10
Nodes (9): The threat model, and what the code does about it, Application, DllImport, Mutex, App, OverlayRepairPending, RegistryKey, Footprint (+1 more)

### Community 30 - "WindowsFaultsTests"
Cohesion: 0.16
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 31 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 32 - ".RemoveMachineTraces"
Cohesion: 0.10
Nodes (9): dynamic, Encoding, StartupRegistration, IsEnabled, DllImport, Elevation, CardSwitchTasks, Fact (+1 more)

### Community 33 - "UserPresence"
Cohesion: 0.13
Nodes (14): LastInputInfo, MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr (+6 more)

### Community 34 - "Button"
Cohesion: 0.06
Nodes (30): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, GameScanButton (+22 more)

### Community 35 - "CpuLimits"
Cohesion: 0.09
Nodes (18): Bit, Now, Offset, PowerInfo, PowerLimits, Ratio, SinceBoot, IReadOnlyList (+10 more)

### Community 36 - "CrashGuard"
Cohesion: 0.11
Nodes (19): Session, DateTimeOffset, Exception, List, TimeSpan, CrashGuard, SafeMode, UncleanPrevious (+11 more)

### Community 37 - "Nextcalibur.Core.Configuration"
Cohesion: 0.09
Nodes (9): Nextcalibur.Core.Configuration, microsoft_win32, Theme, Dark, Light, system_security, system_text_json, system_text_json_serialization (+1 more)

### Community 38 - "InstallFolderGuard"
Cohesion: 0.13
Nodes (10): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Fact, InlineData, Theory, ElevationPolicyTests (+2 more)

### Community 39 - ".Choose"
Cohesion: 0.12
Nodes (19): IEnumerable, IReadOnlyList, IdleLoadPicker, RunningItem, RunningKind, App, Background, Service (+11 more)

### Community 40 - "Api"
Cohesion: 0.11
Nodes (23): Api, GetSettingFn, InitializeFn, ProfileOutFn, SessionFn, SessionOutFn, Set, SetSettingFn (+15 more)

### Community 41 - "ColourWheel"
Cohesion: 0.10
Nodes (19): BitmapSource, Control, DependencyPropertyChangedEventArgs, Ellipse, Hue, Saturation, Canvas, Color (+11 more)

### Community 42 - ".Get"
Cohesion: 0.13
Nodes (14): ArgumentNullException, GpuLoad, FirmwareModeReading, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma (+6 more)

### Community 43 - "ResourceDictionary"
Cohesion: 0.10
Nodes (27): ActualWidth, Bar, Bg, Blade, Box, CardBorder, Grip, Guard (+19 more)

### Community 44 - "LedState"
Cohesion: 0.11
Nodes (19): Dictionary, LedBrightness, LedProfile, BrightnessPercent, Colours, Effect, LedState, ActiveProfile (+11 more)

### Community 45 - "Dependency"
Cohesion: 0.11
Nodes (18): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+10 more)

### Community 46 - ".PlaceTourStep"
Cohesion: 0.09
Nodes (11): SizeChangedEventArgs, FrameworkElement, KeyEventArgs, RoutedEventArgs, Size, TourStep, Body, SectionName (+3 more)

### Community 47 - ".LoadDefinitionsAsync"
Cohesion: 0.13
Nodes (15): Grid, Task, GpuPowerLimits, IEnumerable, PowerDefinition, PowerDefinitions, SetBy, Intel (+7 more)

### Community 48 - ".SignatureIsValid"
Cohesion: 0.15
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 49 - "Fact"
Cohesion: 0.18
Nodes (7): IList, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 50 - "Fact"
Cohesion: 0.11
Nodes (14): OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact, Task (+6 more)

### Community 51 - "TestPreflight"
Cohesion: 0.14
Nodes (12): Display, IoCounters, ProcessLook, ConcurrentDictionary, Dictionary, DllImport, IntPtr, IoCounters (+4 more)

### Community 52 - "Integrity"
Cohesion: 0.12
Nodes (11): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvapiPath, NvmlPath (+3 more)

### Community 53 - "CpuPowerReader"
Cohesion: 0.20
Nodes (6): ArgumentOutOfRangeException, DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 54 - ".Save"
Cohesion: 0.09
Nodes (7): CancelEventArgs, Slider, Guid, Ours, Plan, EventArgs, TextBox

### Community 55 - "Authenticode"
Cohesion: 0.25
Nodes (14): CatalogInfo, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle, Authenticode (+6 more)

### Community 56 - ".Check"
Cohesion: 0.14
Nodes (9): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported, IdempotenceTests (+1 more)

### Community 57 - "Program"
Cohesion: 0.15
Nodes (5): ConsoleColor, B, G, R, Program

### Community 58 - "ModeProfile"
Cohesion: 0.14
Nodes (12): AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, Baseline (+4 more)

### Community 59 - "FirmwareInventory"
Cohesion: 0.09
Nodes (22): IReadOnlyList, FirmwareInventory, BatteryChemistry, BatteryDesignMwh, BatteryName, BiosDate, BiosRelease, BiosVendor (+14 more)

### Community 60 - ".For"
Cohesion: 0.22
Nodes (8): MemberData, Baseline, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 61 - ".OnSystemModeChanged"
Cohesion: 0.16
Nodes (7): Option, PowerModeChangedEventArgs, Button, Guid, IEnumerable, Ours, Plan

### Community 62 - "ToggleButton"
Cohesion: 0.09
Nodes (22): OverheatWarningToggle, SelectAll, SettingAskGameMode, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost (+14 more)

### Community 63 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 64 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 65 - "MachineScanRecord"
Cohesion: 0.09
Nodes (22): DateTime, List, MachineScanRecord, BiosDate, BiosRelease, BiosVendor, BiosVersion, BoardProduct (+14 more)

### Community 66 - ".Execute"
Cohesion: 0.18
Nodes (7): ArgumentException, Func, SmiCommand, Fact, InlineData, Theory, SmiCommandTests

### Community 67 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 68 - ".CheckProgram"
Cohesion: 0.20
Nodes (5): Fact, IntegrityTests, Current, Exe, Manifest

### Community 69 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 70 - ".Ask"
Cohesion: 0.25
Nodes (10): HttpStatusCode, CancellationToken, Fact, HttpRequestMessage, HttpResponseMessage, InlineData, Task, Theory (+2 more)

### Community 71 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 72 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Ok, Task (+7 more)

### Community 73 - ".Dictionary"
Cohesion: 0.15
Nodes (9): IReadOnlyDictionary, Stable, Dictionary, Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests (+1 more)

### Community 74 - "DevicePowerSaving"
Cohesion: 0.21
Nodes (10): AcDc, Guid, IEnumerable, DevicePowerSaving, Off, DeviceSaving, Usb, Wifi (+2 more)

### Community 75 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 76 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 77 - "DarkMenuColours"
Cohesion: 0.11
Nodes (19): ProfessionalColorTable, Color, DarkMenuColours, CheckBackground, CheckPressedBackground, CheckSelectedBackground, ImageMarginGradientBegin, ImageMarginGradientEnd (+11 more)

### Community 78 - "PowerOverlayService"
Cohesion: 0.21
Nodes (7): DllImport, Guid, IReadOnlyList, PowerModeOption, PowerOverlays, All, PowerOverlayService

### Community 79 - "TemperatureFusion"
Cohesion: 0.25
Nodes (9): DateTime, TimeSpan, TemperatureFusion, Offset, RegisterSetAside, Value, DateTime, Fact (+1 more)

### Community 80 - "Nextcalibur.Core.Security"
Cohesion: 0.18
Nodes (5): Nextcalibur.Core.Security, system_security_accesscontrol, system_security_cryptography, system_security_cryptography_x509certificates, system_security_principal

### Community 81 - "Hardware Protocol"
Cohesion: 0.11
Nodes (18): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 4. LED — `a1 = 0x0100`, 6. Safety rules, 7. Not supported, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`) (+10 more)

### Community 82 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 83 - "CoreLoad"
Cohesion: 0.15
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 84 - ".GetColour"
Cohesion: 0.24
Nodes (7): B, G, R, Fact, InlineData, Theory, LedStateTests

### Community 85 - "DevicePowerState"
Cohesion: 0.25
Nodes (6): DevPropKey, Discrete GPU sleep protection in Hybrid mode, DllImport, Guid, DevicePowerState, DevPropKey

### Community 86 - "EcMailbox"
Cohesion: 0.21
Nodes (8): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, EcMailbox, EcMailboxUnavailableException, Held

### Community 87 - "Unelevated"
Cohesion: 0.33
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 88 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 89 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 90 - "TrayPresence"
Cohesion: 0.16
Nodes (8): EventHandler, Item, NotifyIcon, List, Ms, Window, TrayPresence, Dark

### Community 91 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 92 - "BatteryModePolicy"
Cohesion: 0.35
Nodes (5): BatteryModePolicy, QuietMode, Remembered, Fact, BatteryModePolicyTests

### Community 93 - "Nextcalibur.Core.Tests.Attacks"
Cohesion: 0.26
Nodes (5): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http

### Community 94 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 95 - ".Set"
Cohesion: 0.30
Nodes (7): PowerThrottlingState, DllImport, IntPtr, EfficiencyMode, PowerThrottlingState, Fact, EfficiencyModeTests

### Community 96 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 97 - ".RunAndRecord"
Cohesion: 0.22
Nodes (6): JsonSerializerOptions, MachineScan, Firmware, RecordPath, Fact, MachineScanTests

### Community 98 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 99 - ".Write"
Cohesion: 0.23
Nodes (7): AcDc, Guid, IReadOnlyList, IdleSetting, DisplayOff, Sleep, IdleTimeouts

### Community 100 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 101 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 102 - "PackageTemperatureTests"
Cohesion: 0.36
Nodes (4): Fact, InlineData, Theory, PackageTemperatureTests

### Community 103 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 104 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 105 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 106 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 107 - "Nextcalibur 0.5.4"
Cohesion: 0.17
Nodes (11): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+3 more)

### Community 108 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 109 - "StackPanel"
Cohesion: 0.17
Nodes (12): BannerActions, CeilingRows, DefinitionRows, DialogActions, DriveTextStack, FnShortcutRows, IdleLoadRows, IdleRows (+4 more)

### Community 110 - ".RefreshIdleRows"
Cohesion: 0.24
Nodes (6): ComboBox, Func, Grid, Guid, TextBlock, ToggleButton

### Community 111 - "AcDc"
Cohesion: 0.29
Nodes (5): AcDc, InlineData, Theory, Fact, IdleTimeoutsTests

### Community 112 - "SystemModes"
Cohesion: 0.23
Nodes (5): IReadOnlyList, SystemModes, InOrder, Fact, SystemModesTests

### Community 113 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 114 - "ThemeService"
Cohesion: 0.18
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 115 - "SupportVerdict"
Cohesion: 0.20
Nodes (10): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+2 more)

### Community 116 - "NoticeGate"
Cohesion: 0.22
Nodes (7): Key, Show, Action, List, NoticeGate, ShouldWait, WaitingCount

### Community 117 - ".Pick"
Cohesion: 0.20
Nodes (6): MouseEventArgs, MouseButtonEventArgs, Arrow, Point, Tick, Path

### Community 118 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 119 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 120 - ".Parse"
Cohesion: 0.29
Nodes (4): List, MemoryModule, Fact, FirmwareInventoryTests

### Community 121 - ".ClampFrequency"
Cohesion: 0.27
Nodes (5): Baseline, Fact, InlineData, Theory, CeilingTests

### Community 122 - ".IsSystemFileSignedBy"
Cohesion: 0.33
Nodes (3): Fact, SystemFileTrustTests, Cmd

### Community 123 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 124 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 125 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 126 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 127 - "LedEffect"
Cohesion: 0.22
Nodes (8): LedEffect, Blink, Breathing, ColourCycle, Heartbeat, Off, Static, Wave

### Community 128 - "ThermalSample"
Cohesion: 0.33
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 129 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 130 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 131 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 132 - "Toasts"
Cohesion: 0.32
Nodes (5): Action, Toasts, Balloon, windows_data_xml_dom, windows_ui_notifications

### Community 134 - ".Recommended"
Cohesion: 0.29
Nodes (5): Min, Fact, InlineData, Theory, IdleRecommendedTests

### Community 135 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 136 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 137 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 139 - "Nextcalibur 0.5.5"
Cohesion: 0.29
Nodes (5): Deep memory & resource leak eradication, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture

### Community 140 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 141 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 142 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 143 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 144 - ".Current"
Cohesion: 0.48
Nodes (3): DllImport, IntPtr, ScreenSaver

### Community 145 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 146 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 147 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 148 - "Nextcalibur 0.7.0"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.7.0, Tested on, Updated

### Community 149 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 150 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 151 - "ComboBox"
Cohesion: 0.33
Nodes (6): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, SettingNvPower, ComboBox

### Community 152 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 153 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 154 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 155 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 156 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 157 - "Nextcalibur 0.7.1"
Cohesion: 0.40
Nodes (4): Fixed, New, Nextcalibur 0.7.1, Tested on

### Community 158 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 159 - "PART_Track"
Cohesion: 0.40
Nodes (5): PART_Track, RootGrid, Sword, Grid, Track

### Community 163 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 165 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 166 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 167 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 169 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 170 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 171 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 172 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 173 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 175 - "Toggle"
Cohesion: 0.67
Nodes (3): IsDropDownOpen, Toggle, ToggleButton

### Community 177 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 178 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

### Community 179 - "EffectPanel"
Cohesion: 0.67
Nodes (3): EffectPanel, ProfileRow, UniformGrid

### Community 180 - "PART_ContentHost"
Cohesion: 0.67
Nodes (3): PART_ContentHost, SettingsScroll, ScrollViewer

## Knowledge Gaps
- **610 isolated node(s):** `OverlayRepairPending`, `Accepted`, `SelectedColour`, `VisualChildrenCount`, `Diameter` (+605 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 1181 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **33 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `ThermalSample`, `GpuClockReader`, `AppSettings`, `FnShortcut`, `.RuleRow`, `.Info`, `.Get`, `RunawayWatchTests`, `ProgramRule`, `Nextcalibur 0.5.5`, `UpdateService`, `Nextcalibur.App`, `WindowsFaults`, `TourPage`, `RadioButton`, `Window`, `PlanTuning`, `Nextcalibur.Core.Hardware`, `.Warn`, `CoolingPolicyTests`, `.OnBrightnessChanged`, `LedZone`, `CeilingTestWindow`, `.RequestRestart`, `SystemMode`, `.StartSlowTimer`, `SystemInfo`, `UserPresence`, `Button`, `Nextcalibur.Core.Configuration`, `.Choose`, `.Get`, `.PlaceTourStep`, `.LoadDefinitionsAsync`, `Integrity`, `CpuPowerReader`, `.Save`, `ModeProfile`, `.OnSystemModeChanged`, `.Read`, `DependencyStatus`, `DevicePowerSaving`, `BacklightKeyWatcher`, `PowerOverlayService`, `TemperatureFusion`, `Nextcalibur.Core.Security`, `CoreLoad`, `EcMailbox`, `LedController`, `TrayPresence`, `BatteryModePolicy`, `.Write`, `Grid`, `Strings`, `.RefreshIdleRows`, `ThemeService`, `SupportVerdict`, `NvidiaDriverState`, `LedEffect`?**
  _High betweenness centrality (0.512) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Text`, `RadioButton`, `.OnBrightnessChanged`, `ComboBox`, `Button`, `Keycaps`, `DriveGauge`, `CpuWarnValue`, `DialogCheck`, `EffectPanel`, `PART_ContentHost`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `ToggleButton`, `TourCanvas`, `Wheel`, `Grid`, `StackPanel`, `Border`?**
  _High betweenness centrality (0.078) - this node is a cross-community bridge._
- **Why does `Nextcalibur.Core.Hardware` connect `Nextcalibur.Core.Hardware` to `ThermalSample`, `CpuLimits`, `Nextcalibur.Core.Configuration`, `PackageTemperatureTests`, `SystemInfoTests.cs`, `RunawayWatchTests`, `Nextcalibur.App`, `BacklightKeyWatcher`, `Nextcalibur.Core.Security`, `xunit`, `SupportVerdict`, `MachineWideGate`, `Nextcalibur.Core.Tests.Attacks`?**
  _High betweenness centrality (0.058) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `OverlayRepairPending`, `Accepted`, `SelectedColour` to the rest of the system?**
  _610 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.021374204983839015 - nodes in this community are weakly interconnected._
- **Should `GpuClockReader` be split into smaller, more focused modules?**
  _Cohesion score 0.058126619770455384 - nodes in this community are weakly interconnected._