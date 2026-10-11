# Graph Report - nextcalibur-control-center  (2026-10-11)

## Corpus Check
- 165 files · ~216,640 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2781 nodes · 6000 edges · 169 communities (136 shown, 33 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 317 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `830092e9`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- Authenticode
- AppSettings
- ProtectedStore
- FnShortcut
- IShellLinkW
- .CheckProgram
- GpuClockReader
- .InstallAsync
- xunit
- .Get
- Window
- RunawayWatchTests
- Nextcalibur.Core.Configuration
- UserPresence
- PlanTuning
- TrayPresence
- RadioButton
- .Info
- Button
- .Warn
- CpuPowerReader
- WindowsFaults
- MachineScanRecord
- .RequestRestart
- Fact
- WindowsFaultsTests
- NetworkCostTests
- Nextcalibur.Core.Hardware
- Fact
- CoolingPolicyTests
- ProcessPresence
- UpdateService
- SettingsTests.cs
- SystemModeService
- Program
- .PlaceTourStep
- LedState
- Dependency
- EcMailbox
- Elevation
- .StartSlowTimer
- PowerOverlayService
- .Ask
- DotNetRuntimeDependency
- Fact
- Nextcalibur.Core.csproj
- MemoryTrimmer
- .WarnAboutNewConflicts
- .CalculateThreadShare
- SmiCommand
- analyse-autopsy.py
- .OnSystemModeChanged
- RestorePoint
- ToggleButton
- ResourceDictionary
- RuntimeDependencyTests
- BacklightKeyWatcher
- .RefreshPreview
- .Get
- ColourWheel
- LedController
- .IsUnderProgramFiles
- FanGauge
- .Measure
- DevicePowerState
- DonutGauge
- GpuMode
- .Capture
- SegmentedBar
- CoreLoad
- LedEffect
- ThemeService
- .HasAccess
- LogInjectionTests
- ValueConverters.cs
- SystemMode
- InstallFolderGuard
- BatteryModePolicy
- StringsDictionaryTests
- SystemInfo
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- .PowerModeControls
- Nextcalibur Control Center
- Strings
- Grid
- Hardware Protocol
- Nextcalibur 0.5.3
- LedZone
- IntPtr
- system_diagnostics
- StorageUse
- Border
- .Sanitised
- Log
- NvidiaDriverState
- SystemInfoTests.cs
- WordsInCodeTests
- Nextcalibur.Core.Power
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- PawnIoDependency
- Probe
- .GetAsync
- Nextcalibur 0.5.1
- DriveRow
- StackPanel
- Test-Ui.ps1
- Text
- Strings.cs
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Releasing, and signing
- .Pick
- TourPage
- NduFix
- BatteryModePolicy.cs
- Log-Graphics.ps1
- Privacy
- Security
- PawnIoModuleTests.cs
- Notice
- Nextcalibur 0.5.11
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
- BrightnessSlider
- .TryParseRgb
- TaskFolderTests.cs
- Tools
- 0.5.7.md
- CpuWarnValue
- DialogCheck
- .Trademark_noise_and_the_clock_speed_are_removed
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

## Communities (169 total, 33 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.04
Nodes (41): CheckBox, DeferredOverheat, HwndSource, ObservableCollection, Queue, SolidColorBrush, MainWindow, DateTime (+33 more)

### Community 1 - "Authenticode"
Cohesion: 0.06
Nodes (38): CatalogInfo, Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on (+30 more)

### Community 2 - "AppSettings"
Cohesion: 0.05
Nodes (44): DateTime, Dictionary, FnShortcut, JsonElement, List, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates (+36 more)

### Community 3 - "ProtectedStore"
Cohesion: 0.09
Nodes (20): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+12 more)

### Community 4 - "FnShortcut"
Cohesion: 0.08
Nodes (26): Nextcalibur.Core.Input, DllImport, IEnumerable, IntPtr, List, StringBuilder, FnAction, FanTurbo (+18 more)

### Community 5 - "IShellLinkW"
Cohesion: 0.06
Nodes (18): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+10 more)

### Community 6 - ".CheckProgram"
Cohesion: 0.07
Nodes (23): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvmlPath, IntegrityFinding (+15 more)

### Community 7 - "GpuClockReader"
Cohesion: 0.09
Nodes (18): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+10 more)

### Community 8 - ".InstallAsync"
Cohesion: 0.07
Nodes (25): FileStream, IOException, Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress (+17 more)

### Community 9 - "xunit"
Cohesion: 0.09
Nodes (14): Nextcalibur.Core.Tests, Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_security_cryptography_x509certificates (+6 more)

### Community 10 - ".Get"
Cohesion: 0.09
Nodes (14): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary, IReadOnlyDictionary, Task (+6 more)

### Community 11 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 12 - "RunawayWatchTests"
Cohesion: 0.09
Nodes (27): CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet, TimeSpan (+19 more)

### Community 13 - "Nextcalibur.Core.Configuration"
Cohesion: 0.09
Nodes (24): Nextcalibur.App, Nextcalibur.App.Controls, Nextcalibur.Core.Configuration, system_componentmodel, system_drawing, system_io, system_linq, system_text_json_serialization (+16 more)

### Community 14 - "UserPresence"
Cohesion: 0.10
Nodes (20): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr, MarshalAs (+12 more)

### Community 15 - "PlanTuning"
Cohesion: 0.13
Nodes (16): AcDc, Floor, Original, Plan, Action, DllImport, Guid, IntPtr (+8 more)

### Community 16 - "TrayPresence"
Cohesion: 0.10
Nodes (17): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, ProcessInformation, List, Ms (+9 more)

### Community 17 - "RadioButton"
Cohesion: 0.07
Nodes (34): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, NavDisplay, NavLighting (+26 more)

### Community 18 - ".Info"
Cohesion: 0.10
Nodes (10): AssemblyInformationalVersionAttribute, Asynchronous dispatcher & UI thread hardening, KeyEventArgs, Task, IProgress, Exception, DependencyStatus, Missing (+2 more)

### Community 19 - "Button"
Cohesion: 0.06
Nodes (26): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, HideToTrayButton (+18 more)

### Community 20 - ".Warn"
Cohesion: 0.11
Nodes (9): The threat model, and what the code does about it, Application, DllImport, Mutex, App, MessageBoxButton, MessageBoxResult, RegistryKey (+1 more)

### Community 21 - "CpuPowerReader"
Cohesion: 0.11
Nodes (14): Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, DllImport, IntPtr, SafeFileHandle (+6 more)

### Community 22 - "WindowsFaults"
Cohesion: 0.09
Nodes (28): Share, DateTime, Dictionary, Func, IReadOnlyList, List, TimeSpan, FaultEvaluation (+20 more)

### Community 23 - "MachineScanRecord"
Cohesion: 0.08
Nodes (26): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites (+18 more)

### Community 24 - ".RequestRestart"
Cohesion: 0.10
Nodes (11): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr (+3 more)

### Community 25 - "Fact"
Cohesion: 0.15
Nodes (9): Func, IEnumerable, IReadOnlyList, Footprint, RetirementEntry, Version, Fact, FootprintTests (+1 more)

### Community 27 - "NetworkCostTests"
Cohesion: 0.14
Nodes (19): Bytes, Count, HttpMessageHandler, CancellationToken, Dictionary, Fact, HttpRequestMessage, HttpResponseMessage (+11 more)

### Community 28 - "Nextcalibur.Core.Hardware"
Cohesion: 0.11
Nodes (11): Nextcalibur.Core.Hardware, microsoft_win32, microsoft_win32_safehandles, Trace, SmiFamily, Read, Write, system_management (+3 more)

### Community 29 - "Fact"
Cohesion: 0.14
Nodes (10): IList, Fact, InlineData, Theory, GpuAwakeFaultConditionTests, GpuModeTests, Discrete, Hybrid (+2 more)

### Community 30 - "CoolingPolicyTests"
Cohesion: 0.12
Nodes (16): At, Margin, DateTime, List, TimeSpan, CoolingPolicy, DegreesPerMinute, Engaged (+8 more)

### Community 31 - "ProcessPresence"
Cohesion: 0.14
Nodes (11): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+3 more)

### Community 32 - "UpdateService"
Cohesion: 0.10
Nodes (14): CancelEventArgs, EventArgs, DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService (+6 more)

### Community 33 - "SettingsTests.cs"
Cohesion: 0.12
Nodes (10): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported, ForwardCompatibilityTests (+2 more)

### Community 34 - "SystemModeService"
Cohesion: 0.17
Nodes (9): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, SystemModeService (+1 more)

### Community 35 - "Program"
Cohesion: 0.14
Nodes (6): ConsoleColor, Program, HardwareSupport, DateTimeOffset, ThermalReader, ThermalSample

### Community 36 - ".PlaceTourStep"
Cohesion: 0.09
Nodes (12): SizeChangedEventArgs, FrameworkElement, KeyEventArgs, Point, RoutedEventArgs, Size, TourStep, Body (+4 more)

### Community 37 - "LedState"
Cohesion: 0.15
Nodes (15): B, G, R, LedState, ActiveProfile, BrightnessPercent, Current, Effect (+7 more)

### Community 38 - "Dependency"
Cohesion: 0.11
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 39 - "EcMailbox"
Cohesion: 0.14
Nodes (13): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+5 more)

### Community 40 - "Elevation"
Cohesion: 0.17
Nodes (4): dynamic, Encoding, Elevation, CardSwitchTasks

### Community 41 - ".StartSlowTimer"
Cohesion: 0.12
Nodes (5): DllImport, IntPtr, Name, Action, Toasts

### Community 42 - "PowerOverlayService"
Cohesion: 0.17
Nodes (11): DllImport, Guid, IReadOnlyList, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, PowerModeOption (+3 more)

### Community 43 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 44 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 45 - "Fact"
Cohesion: 0.13
Nodes (10): RepairOutcome, Empty, Fact, Task, CleanMachineTests, Guarded, Unguarded, CoexistencePolicyTests (+2 more)

### Community 46 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.12), System.Management (10.0.12), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk (+6 more)

### Community 47 - "MemoryTrimmer"
Cohesion: 0.14
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 48 - ".WarnAboutNewConflicts"
Cohesion: 0.14
Nodes (13): Problem, Detail, Dictionary, DllImport, IntPtr, ShortcutProblem, Duplicate, NotAKey (+5 more)

### Community 49 - ".CalculateThreadShare"
Cohesion: 0.13
Nodes (9): IReadOnlyDictionary, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Stable, Fact, Dictionary (+1 more)

### Community 50 - "SmiCommand"
Cohesion: 0.19
Nodes (6): ArgumentException, SmiCommand, Fact, InlineData, Theory, SmiCommandTests

### Community 51 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 52 - ".OnSystemModeChanged"
Cohesion: 0.17
Nodes (5): PowerModeChangedEventArgs, ModeGaming, ModeOffice, ModePerformance, UninstallCommand

### Community 53 - "RestorePoint"
Cohesion: 0.18
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 54 - "ToggleButton"
Cohesion: 0.11
Nodes (17): OverheatWarningToggle, SelectAll, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost, SettingFixWidgets (+9 more)

### Community 55 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 56 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 57 - "BacklightKeyWatcher"
Cohesion: 0.13
Nodes (8): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, Fact, InlineData, Theory, BacklightKeyTests

### Community 58 - ".RefreshPreview"
Cohesion: 0.13
Nodes (5): RoutedPropertyChangedEventArgs, Slider, Action, Color, TextBox

### Community 59 - ".Get"
Cohesion: 0.28
Nodes (4): ArgumentNullException, GpuModeService, SwitchOutcome, SwitchOutcome

### Community 60 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 61 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 62 - ".IsUnderProgramFiles"
Cohesion: 0.21
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 63 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 64 - ".Measure"
Cohesion: 0.20
Nodes (8): Cpu, Dictionary, IReadOnlyList, TimeSpan, ProcessLoad, TopProcesses, Fact, TopProcessesTests

### Community 65 - "DevicePowerState"
Cohesion: 0.28
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 66 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 67 - "GpuMode"
Cohesion: 0.19
Nodes (7): GpuLoad, FirmwareModeReading, GpuConfiguration, GpuMode, Discrete, Hybrid, Uma

### Community 68 - ".Capture"
Cohesion: 0.23
Nodes (7): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, DllImport, IntPtr

### Community 69 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 70 - "CoreLoad"
Cohesion: 0.15
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 71 - "LedEffect"
Cohesion: 0.14
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 72 - "ThemeService"
Cohesion: 0.15
Nodes (11): Theme, Theme, Dark, Light, ThemePreference, Dark, Light, System (+3 more)

### Community 73 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 74 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 75 - "ValueConverters.cs"
Cohesion: 0.26
Nodes (8): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_text_regularexpressions, system_windows_data, Type

### Community 76 - "SystemMode"
Cohesion: 0.28
Nodes (7): ThermalProfile, SystemMode, Gaming, Office, Performance, InlineData, Theory

### Community 77 - "InstallFolderGuard"
Cohesion: 0.33
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 78 - "BatteryModePolicy"
Cohesion: 0.42
Nodes (4): BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

### Community 79 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 80 - "SystemInfo"
Cohesion: 0.27
Nodes (5): MemoryStatusEx, DllImport, MarshalAs, MemoryStatusEx, SystemInfo

### Community 81 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 82 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 83 - ".PowerModeControls"
Cohesion: 0.18
Nodes (5): Option, Button, IEnumerable, RadioButton, TextBlock

### Community 84 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 85 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 86 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 87 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 88 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 89 - "LedZone"
Cohesion: 0.18
Nodes (10): LedBrightness, Full, Half, Off, LedZone, AllKeyboard, Everything, Left (+2 more)

### Community 90 - "IntPtr"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 91 - "system_diagnostics"
Cohesion: 0.31
Nodes (4): Nextcalibur.Cli, system_diagnostics, system_security_accesscontrol, system_security_principal

### Community 92 - "StorageUse"
Cohesion: 0.22
Nodes (6): DriveUse, IReadOnlyList, DriveUse, Name, StorageUse, Percent

### Community 93 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 94 - ".Sanitised"
Cohesion: 0.33
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 96 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 97 - "SystemInfoTests.cs"
Cohesion: 0.31
Nodes (3): Fact, ProcessorNameTests, StorageUseTests

### Community 98 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 99 - "Nextcalibur.Core.Power"
Cohesion: 0.25
Nodes (3): Nextcalibur.Core.Power, Fact, ThermalProfileTests

### Community 100 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 101 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 102 - "README.md"
Cohesion: 0.33
Nodes (3): How it works, Questions people ask, Using it

### Community 103 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 104 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 105 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 106 - ".GetAsync"
Cohesion: 0.29
Nodes (7): Answer, ConcurrentDictionary, CancellationToken, HttpClient, Task, Answer, Conditional

### Community 107 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 108 - "DriveRow"
Cohesion: 0.25
Nodes (6): INotifyPropertyChanged, DriveRow, Name, Percent, PercentText, system_runtime_compilerservices

### Community 109 - "StackPanel"
Cohesion: 0.25
Nodes (8): BannerActions, DialogActions, DriveTextStack, FnShortcutRows, PanelWindowsFaultsSubOptions, ProfileRow, SettingUpdatesAutomatic, StackPanel

### Community 110 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 111 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 112 - "Strings.cs"
Cohesion: 0.33
Nodes (5): Nextcalibur.Core, Func, Words, Resolver, system_globalization

### Community 113 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 114 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 115 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 116 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 117 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 119 - "BatteryModePolicy.cs"
Cohesion: 0.33
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 120 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 121 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 122 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 123 - "PawnIoModuleTests.cs"
Cohesion: 0.33
Nodes (4): system_security_cryptography, InlineData, Theory, PawnIoModuleTests

### Community 124 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 125 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 127 - "SmiSubsystem"
Cohesion: 0.40
Nodes (5): SmiSubsystem, DisplayMode, Led, Profile, Thermal

### Community 128 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 130 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 131 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 132 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 133 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 134 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 135 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 137 - "BrightnessSlider"
Cohesion: 0.50
Nodes (4): BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 138 - ".TryParseRgb"
Cohesion: 0.50
Nodes (3): B, G, R

### Community 140 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 143 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 144 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

## Knowledge Gaps
- **454 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+449 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 861 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **33 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `AppSettings`, `FnShortcut`, `.CheckProgram`, `GpuClockReader`, `.Get`, `Window`, `RunawayWatchTests`, `Nextcalibur.Core.Configuration`, `UserPresence`, `PlanTuning`, `TrayPresence`, `RadioButton`, `.Info`, `Button`, `.Warn`, `CpuPowerReader`, `WindowsFaults`, `MachineScanRecord`, `.RequestRestart`, `CoolingPolicyTests`, `ProcessPresence`, `UpdateService`, `SystemModeService`, `Program`, `.PlaceTourStep`, `EcMailbox`, `.StartSlowTimer`, `PowerOverlayService`, `.WarnAboutNewConflicts`, `.OnSystemModeChanged`, `ToggleButton`, `BacklightKeyWatcher`, `.RefreshPreview`, `.Get`, `LedController`, `GpuMode`, `LedEffect`, `ThemeService`, `SystemMode`, `BatteryModePolicy`, `SystemInfo`, `.PowerModeControls`, `Strings`, `Grid`, `LedZone`, `StorageUse`, `NvidiaDriverState`, `TourPage`?**
  _High betweenness centrality (0.466) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Keycaps`, `DriveGauge`, `BrightnessSlider`, `.Get`, `CpuWarnValue`, `DialogCheck`, `RadioButton`, `Button`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `.OnSystemModeChanged`, `ToggleButton`, `Grid`, `Border`, `StackPanel`, `Text`?**
  _High betweenness centrality (0.114) - this node is a cross-community bridge._
- **Why does `AppSettings` connect `AppSettings` to `MainWindow`, `UpdateService`, `SettingsTests.cs`, `ProtectedStore`, `ThemeService`, `SystemMode`, `Nextcalibur.Core.Configuration`, `TrayPresence`, `.Warn`, `Strings`, `NduFix`, `Fact`, `WindowsFaultsTests`, `.Sanitised`?**
  _High betweenness centrality (0.075) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _454 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.04225352112676056 - nodes in this community are weakly interconnected._
- **Should `Authenticode` be split into smaller, more focused modules?**
  _Cohesion score 0.062111801242236024 - nodes in this community are weakly interconnected._