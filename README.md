# Graph Report - nextcalibur-control-center  (2026-10-14)

## Corpus Check
- 185 files · ~235,913 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 3125 nodes · 6866 edges · 189 communities (157 shown, 32 thin omitted)
- Extraction: 94% EXTRACTED · 6% INFERRED · 0% AMBIGUOUS · INFERRED: 388 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `f29d8f63`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- SystemMode
- AppSettings
- ProtectedStore
- .Get
- GpuClockReader
- FnShortcut
- RunawayWatchTests
- ProcessPresence
- Nextcalibur.Core.Configuration
- WindowsFaults
- IShellLinkW
- .Info
- RadioButton
- Window
- PlanTuning
- CoolingPolicyTests
- .OnLoaded
- Nextcalibur.Core.Hardware
- .RequestRestart
- MachineScanRecord
- .WireSettingsPage
- WindowsFaultsTests
- UserPresence
- SystemInfo
- NetworkCostTests
- Fact
- .ApplyFnShortcuts
- InstallFolderGuard
- Program
- Button
- system_runtime_interopservices
- Elevation
- EcMailbox
- Fact
- Dependency
- .SignatureIsValid
- .Get
- TrayPresence
- Nextcalibur.Core.Security
- .Check
- Authenticode
- .For
- .Ask
- Integrity
- .Read
- DotNetRuntimeDependency
- Nextcalibur.Core.csproj
- .CheckProgram
- RestorePoint
- .Main
- MemoryTrimmer
- DependencyStatus
- CpuPowerReader
- .Dictionary
- CpuLimits
- analyse-autopsy.py
- system_diagnostics
- Nextcalibur.Core.Power
- BacklightKeyWatcher
- ToggleButton
- UpdateService
- Footprint
- ResourceDictionary
- RuntimeDependencyTests
- CoreLoad
- .GetColour
- ModeProfile
- ProgramRule
- ColourWheel
- DevicePowerState
- LedController
- LedState
- CpuLimitsTests
- FanGauge
- Unelevated
- DonutGauge
- .Processor
- AcDc
- Hardware Protocol
- SegmentedBar
- Nextcalibur Control Center
- LogInjectionTests
- SystemTools
- Grid
- .RefreshIdleRows
- .HasAccess
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- Strings
- .RunawayTick
- .Sanitised
- PackageTemperatureTests
- .ShouldAsk
- AcDc
- SmiCommandTests
- Nextcalibur 0.5.3
- ThemeService
- GameSessions
- HostileFilesTests
- Border
- StackPanel
- NvidiaDriverState
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- LedEffect
- PawnIoDependency
- .IsSystemFileSignedBy
- ProfileFiles
- Probe
- .GetAsync
- Nextcalibur 0.5.1
- NduFix
- NotificationState
- IntegrityKind
- Test-Ui.ps1
- Text
- Nextcalibur 0.5.5
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- .Pick
- TourPage
- .Current
- SetBy
- Log-Graphics.ps1
- Words
- Privacy
- Nextcalibur 0.6.2
- Security
- DriveRow
- .OnBrightnessChanged
- LedZone
- MachineWideGate
- PowerSource
- Notice
- Nextcalibur 0.5.11
- ComboBox
- TourStep
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
- ProgramRules
- .ArrangeOverride
- .Modules_are_the_released_ones
- .Names_are_normalised
- Tools
- 0.5.7.md
- CpuWarnValue
- DialogCheck
- Toasts.cs
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
1. `MainWindow` - 354 edges
2. `Window` - 182 edges
3. `AppSettings` - 78 edges
4. `Nextcalibur.Core.Hardware` - 59 edges
5. `SystemMode` - 52 edges
6. `RadioButton` - 41 edges
7. `WindowsFaultsTests` - 40 edges
8. `Nextcalibur.Core.Configuration` - 38 edges
9. `TextBlock` - 37 edges
10. `GpuClockReader` - 35 edges

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

## Communities (189 total, 32 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.03
Nodes (64): CheckBox, DeferredOverheat, GameSession, HwndSource, ObservableCollection, PluggedIn, Queue, SizeChangedEventArgs (+56 more)

### Community 1 - "SystemMode"
Cohesion: 0.06
Nodes (36): InvalidOperationException, Ours, BatteryModePolicy, QuietMode, Remembered, DllImport, Guid, IReadOnlyList (+28 more)

### Community 2 - "AppSettings"
Cohesion: 0.04
Nodes (53): DateTime, Dictionary, FnShortcut, JsonElement, List, ProgramRule, AppSettings, AcceptedVendorSoftware (+45 more)

### Community 3 - "ProtectedStore"
Cohesion: 0.07
Nodes (26): Content, DirectorySecurity, Hash, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Dictionary (+18 more)

### Community 4 - ".Get"
Cohesion: 0.06
Nodes (7): GameEnd, SolidColorBrush, IReadOnlyList, GameEnd, Func, MessageBoxButton, MessageBoxResult

### Community 5 - "GpuClockReader"
Cohesion: 0.08
Nodes (22): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+14 more)

### Community 6 - "FnShortcut"
Cohesion: 0.07
Nodes (33): DllImport, IEnumerable, IntPtr, List, StringBuilder, FnAction, FanTurbo, KeyboardBacklight (+25 more)

### Community 7 - "RunawayWatchTests"
Cohesion: 0.07
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 8 - "ProcessPresence"
Cohesion: 0.07
Nodes (22): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+14 more)

### Community 9 - "Nextcalibur.Core.Configuration"
Cohesion: 0.09
Nodes (24): Nextcalibur.App, Nextcalibur.App.Controls, Nextcalibur.Core.Configuration, system_componentmodel, system_drawing, system_io, system_linq, system_runtime_compilerservices (+16 more)

### Community 10 - "WindowsFaults"
Cohesion: 0.08
Nodes (34): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+26 more)

### Community 11 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 12 - ".Info"
Cohesion: 0.10
Nodes (7): AssemblyInformationalVersionAttribute, Option, PowerModeChangedEventArgs, List, Button, IEnumerable, ModeChoice

### Community 13 - "RadioButton"
Cohesion: 0.06
Nodes (42): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeBalanced, ModeDiscrete (+34 more)

### Community 14 - "Window"
Cohesion: 0.08
Nodes (44): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+36 more)

### Community 15 - "PlanTuning"
Cohesion: 0.13
Nodes (16): Floor, Marker, Original, Action, DllImport, Guid, IEnumerable, IntPtr (+8 more)

### Community 16 - "CoolingPolicyTests"
Cohesion: 0.08
Nodes (26): At, Margin, DateTime, Dictionary, Name, Pid, GameSession, FirstSeenUtc (+18 more)

### Community 17 - ".OnLoaded"
Cohesion: 0.09
Nodes (7): Asynchronous dispatcher & UI thread hardening, Slider, Task, EventArgs, IProgress, Exception, TextBox

### Community 18 - "Nextcalibur.Core.Hardware"
Cohesion: 0.13
Nodes (5): Nextcalibur.Core.Tests, Nextcalibur.Core.Hardware, nextcalibur_core_power_plantuning_acdc, system_xml_linq, xunit

### Community 19 - ".RequestRestart"
Cohesion: 0.09
Nodes (11): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr, MarshalAs (+3 more)

### Community 20 - "MachineScanRecord"
Cohesion: 0.07
Nodes (27): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites (+19 more)

### Community 21 - ".WireSettingsPage"
Cohesion: 0.12
Nodes (11): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary, IReadOnlyDictionary, Task (+3 more)

### Community 22 - "WindowsFaultsTests"
Cohesion: 0.17
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 23 - "UserPresence"
Cohesion: 0.13
Nodes (14): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, Point, Size, DateTime, DllImport (+6 more)

### Community 24 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 25 - "NetworkCostTests"
Cohesion: 0.14
Nodes (19): Bytes, Count, HttpMessageHandler, CancellationToken, Dictionary, Fact, HttpRequestMessage, HttpResponseMessage (+11 more)

### Community 26 - "Fact"
Cohesion: 0.16
Nodes (9): Func, IEnumerable, IReadOnlyList, RetirementEntry, Trace, Version, Fact, FootprintTests (+1 more)

### Community 27 - ".ApplyFnShortcuts"
Cohesion: 0.11
Nodes (7): CancelEventArgs, Problem, Detail, Dictionary, DllImport, IntPtr, KeyEventArgs

### Community 28 - "InstallFolderGuard"
Cohesion: 0.14
Nodes (10): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Fact, InlineData, Theory, ElevationPolicyTests (+2 more)

### Community 29 - "Program"
Cohesion: 0.12
Nodes (9): ConsoleColor, B, G, R, Program, HardwareSupport, DateTimeOffset, ThermalReader (+1 more)

### Community 30 - "Button"
Cohesion: 0.07
Nodes (26): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, HideToTrayButton (+18 more)

### Community 31 - "system_runtime_interopservices"
Cohesion: 0.09
Nodes (8): Nextcalibur.Core.Input, system_io_directory, system_io_ioexception, system_io_path, system_management, system_runtime_interopservices, system_runtime_interopservices_comtypes, system_text

### Community 32 - "Elevation"
Cohesion: 0.14
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 33 - "EcMailbox"
Cohesion: 0.16
Nodes (10): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+2 more)

### Community 34 - "Fact"
Cohesion: 0.17
Nodes (7): IList, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 35 - "Dependency"
Cohesion: 0.11
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 36 - ".SignatureIsValid"
Cohesion: 0.15
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 37 - ".Get"
Cohesion: 0.15
Nodes (10): ArgumentNullException, FirmwareModeReading, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma, GpuModeService (+2 more)

### Community 38 - "TrayPresence"
Cohesion: 0.12
Nodes (11): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, List, Ms, Window (+3 more)

### Community 39 - "Nextcalibur.Core.Security"
Cohesion: 0.15
Nodes (9): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_security_cryptography_x509certificates, system_text_json (+1 more)

### Community 40 - ".Check"
Cohesion: 0.13
Nodes (10): CommonAce, RawSecurityDescriptor, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported (+2 more)

### Community 41 - "Authenticode"
Cohesion: 0.25
Nodes (14): CatalogInfo, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle, Authenticode (+6 more)

### Community 42 - ".For"
Cohesion: 0.22
Nodes (8): Baseline, MemberData, Fact, InlineData, Theory, ModeProfileTests, Modes, TheoryData

### Community 43 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 44 - "Integrity"
Cohesion: 0.14
Nodes (10): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvmlPath, IntegrityFinding (+2 more)

### Community 45 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 46 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 47 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.13), System.Management (10.0.13), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 48 - ".CheckProgram"
Cohesion: 0.20
Nodes (5): Fact, IntegrityTests, Current, Exe, Manifest

### Community 49 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 50 - ".Main"
Cohesion: 0.19
Nodes (6): The threat model, and what the code does about it, Application, DllImport, Mutex, App, STAThread

### Community 51 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 52 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 53 - "CpuPowerReader"
Cohesion: 0.24
Nodes (5): DllImport, IntPtr, SafeFileHandle, CpuPowerReader, DriverInstalled

### Community 54 - ".Dictionary"
Cohesion: 0.15
Nodes (9): IReadOnlyDictionary, Stable, Dictionary, Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests (+1 more)

### Community 55 - "CpuLimits"
Cohesion: 0.13
Nodes (13): Bit, IReadOnlySet, Now, PowerInfo, PowerLimits, Ratio, SinceBoot, IReadOnlyList (+5 more)

### Community 56 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 57 - "system_diagnostics"
Cohesion: 0.16
Nodes (5): Nextcalibur.Cli, system_diagnostics, system_security_accesscontrol, system_security_cryptography, system_security_principal

### Community 58 - "Nextcalibur.Core.Power"
Cohesion: 0.12
Nodes (9): Nextcalibur.Core.Power, microsoft_win32, microsoft_win32_safehandles, Theme, Dark, Light, system_reflection, system_security (+1 more)

### Community 59 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 60 - "ToggleButton"
Cohesion: 0.11
Nodes (19): LedPower, OverheatWarningToggle, SelectAll, SettingAskGameMode, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice (+11 more)

### Community 61 - "UpdateService"
Cohesion: 0.13
Nodes (12): DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService, Available, AvailableVersion (+4 more)

### Community 62 - "Footprint"
Cohesion: 0.16
Nodes (4): RegistryKey, Footprint, Log, Folder

### Community 63 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 64 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 65 - "CoreLoad"
Cohesion: 0.15
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 66 - ".GetColour"
Cohesion: 0.24
Nodes (7): B, G, R, Fact, InlineData, Theory, LedStateTests

### Community 67 - "ModeProfile"
Cohesion: 0.20
Nodes (10): AcDc, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, Baseline (+2 more)

### Community 68 - "ProgramRule"
Cohesion: 0.26
Nodes (10): DateTime, IEnumerable, IReadOnlyDictionary, ProgramRule, Enabled, Process, DateTime, Dictionary (+2 more)

### Community 69 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 70 - "DevicePowerState"
Cohesion: 0.25
Nodes (6): DevPropKey, Discrete GPU sleep protection in Hybrid mode, DllImport, Guid, DevicePowerState, DevPropKey

### Community 71 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 72 - "LedState"
Cohesion: 0.13
Nodes (15): Dictionary, LedBrightness, LedProfile, BrightnessPercent, Colours, Effect, LedState, ActiveProfile (+7 more)

### Community 73 - "CpuLimitsTests"
Cohesion: 0.18
Nodes (6): ArgumentOutOfRangeException, Offset, Fact, CpuLimitsTests, ThermFlags, TjMax

### Community 74 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 75 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 76 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 77 - ".Processor"
Cohesion: 0.27
Nodes (6): GpuPowerLimits, IEnumerable, PowerDefinition, PowerDefinitions, Fact, PowerDefinitionsTests

### Community 78 - "AcDc"
Cohesion: 0.21
Nodes (6): IReadOnlyList, AcDc, InlineData, Theory, Fact, IdleTimeoutsTests

### Community 79 - "Hardware Protocol"
Cohesion: 0.14
Nodes (14): 1. Transport — ACPI-WMI mailbox, 3. Thermal / fan — `a1 = 0x0200`, 4. LED — `a1 = 0x0100`, 6. Safety rules, 7. Not supported, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`) (+6 more)

### Community 80 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 81 - "Nextcalibur Control Center"
Cohesion: 0.14
Nodes (14): Building, Documents, Download, Hardware, Is it for your laptop?, Legal, Nextcalibur Control Center, Privacy (+6 more)

### Community 82 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 83 - "SystemTools"
Cohesion: 0.17
Nodes (9): FileStream, SystemTools, Cmd, Explorer, Pnputil, Powercfg, PowerShell, Schtasks (+1 more)

### Community 84 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 85 - ".RefreshIdleRows"
Cohesion: 0.23
Nodes (3): ComboBox, Grid, Guid

### Community 86 - ".HasAccess"
Cohesion: 0.28
Nodes (5): SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 87 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 88 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 89 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 90 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 91 - "Nextcalibur 0.5.4"
Cohesion: 0.17
Nodes (11): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+3 more)

### Community 92 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 93 - ".RunawayTick"
Cohesion: 0.21
Nodes (6): DllImport, IntPtr, Name, Pid, Action, Toasts

### Community 94 - ".Sanitised"
Cohesion: 0.26
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 95 - "PackageTemperatureTests"
Cohesion: 0.41
Nodes (4): Fact, InlineData, Theory, PackageTemperatureTests

### Community 96 - ".ShouldAsk"
Cohesion: 0.33
Nodes (4): IEnumerable, Fact, TimeSpan, GameSessionsTests

### Community 97 - "AcDc"
Cohesion: 0.32
Nodes (7): AcDc, Guid, IdleSetting, DisplayOff, Sleep, IdleTimeouts, Timeouts

### Community 98 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 99 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 100 - "ThemeService"
Cohesion: 0.18
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 101 - "GameSessions"
Cohesion: 0.22
Nodes (6): HashSet, IReadOnlyDictionary, TimeSpan, GameSessions, InlineData, Theory

### Community 102 - "HostileFilesTests"
Cohesion: 0.42
Nodes (3): IOException, Fact, HostileFilesTests

### Community 103 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 104 - "StackPanel"
Cohesion: 0.20
Nodes (10): BannerActions, DialogActions, DriveTextStack, FnShortcutRows, IdleRows, PanelWindowsFaultsSubOptions, ProfileRow, ProgramRuleRows (+2 more)

### Community 105 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 106 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 107 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 108 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 109 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 110 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 111 - "LedEffect"
Cohesion: 0.22
Nodes (8): LedEffect, Blink, Breathing, ColourCycle, Heartbeat, Off, Static, Wave

### Community 112 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 113 - ".IsSystemFileSignedBy"
Cohesion: 0.39
Nodes (3): Fact, SystemFileTrustTests, Cmd

### Community 115 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 116 - ".GetAsync"
Cohesion: 0.29
Nodes (7): Answer, ConcurrentDictionary, CancellationToken, HttpClient, Task, Answer, Conditional

### Community 117 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 119 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 120 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 121 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 122 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 123 - "Nextcalibur 0.5.5"
Cohesion: 0.29
Nodes (5): Deep memory & resource leak eradication, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture

### Community 124 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 125 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 126 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 127 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 128 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 129 - ".Current"
Cohesion: 0.48
Nodes (3): DllImport, IntPtr, ScreenSaver

### Community 130 - "SetBy"
Cohesion: 0.29
Nodes (7): SetBy, Intel, LaptopMaker, Nextcalibur, Nvidia, Windows, You

### Community 131 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 132 - "Words"
Cohesion: 0.33
Nodes (5): Nextcalibur.Core, Func, Words, Resolver, system_globalization

### Community 133 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 134 - "Nextcalibur 0.6.2"
Cohesion: 0.33
Nodes (5): Fixed, New, Nextcalibur 0.6.2, Tested on, Updated

### Community 135 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 136 - "DriveRow"
Cohesion: 0.33
Nodes (5): INotifyPropertyChanged, DriveRow, Name, Percent, PercentText

### Community 137 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 138 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 139 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 140 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 141 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 142 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 143 - "ComboBox"
Cohesion: 0.40
Nodes (5): IdleScreenSaver, ProgramRuleMode, ProgramRuleName, SettingBatteryMode, ComboBox

### Community 144 - "TourStep"
Cohesion: 0.40
Nodes (5): TourStep, Body, SectionName, Title, TourPage

### Community 146 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 148 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 149 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 150 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 151 - "2. Command structure"
Cohesion: 0.50
Nodes (4): 2. Command structure, Call sequence, Command families (`a0`), Subsystems (`a1`)

### Community 152 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 153 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 154 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 155 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 156 - "ProgramRules"
Cohesion: 0.50
Nodes (3): GeneratedRegex, Regex, ProgramRules

### Community 158 - ".Modules_are_the_released_ones"
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
- **496 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+491 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 961 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **32 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `TourPage`, `SystemMode`, `AppSettings`, `.Get`, `GpuClockReader`, `FnShortcut`, `RunawayWatchTests`, `Nextcalibur.Core.Configuration`, `.OnBrightnessChanged`, `LedZone`, `.Info`, `RadioButton`, `WindowsFaults`, `PlanTuning`, `CoolingPolicyTests`, `TourStep`, `.OnLoaded`, `.RequestRestart`, `MachineScanRecord`, `.WireSettingsPage`, `Window`, `UserPresence`, `.ApplyFnShortcuts`, `Program`, `Button`, `EcMailbox`, `.Get`, `TrayPresence`, `Integrity`, `.Read`, `DependencyStatus`, `CpuPowerReader`, `BacklightKeyWatcher`, `UpdateService`, `ProgramRule`, `LedController`, `Grid`, `.RefreshIdleRows`, `Strings`, `.RunawayTick`, `AcDc`, `ThemeService`, `NvidiaDriverState`, `LedEffect`, `Nextcalibur 0.5.5`?**
  _High betweenness centrality (0.493) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `.OnBrightnessChanged`, `RadioButton`, `ComboBox`, `Keycaps`, `DriveGauge`, `Button`, `CpuWarnValue`, `DialogCheck`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `ToggleButton`, `Grid`, `Border`, `StackPanel`, `Text`?**
  _High betweenness centrality (0.091) - this node is a cross-community bridge._
- **Why does `Nextcalibur.Core.Hardware` connect `Nextcalibur.Core.Hardware` to `EcMailbox`, `GpuClockReader`, `Nextcalibur.Core.Security`, `Nextcalibur.Core.Configuration`, `MachineWideGate`, `MachineScanRecord`, `CpuLimits`, `system_diagnostics`, `Nextcalibur.Core.Power`, `BacklightKeyWatcher`, `Program`, `system_runtime_interopservices`?**
  _High betweenness centrality (0.078) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _496 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.02839173405211141 - nodes in this community are weakly interconnected._
- **Should `SystemMode` be split into smaller, more focused modules?**
  _Cohesion score 0.05524537173082574 - nodes in this community are weakly interconnected._