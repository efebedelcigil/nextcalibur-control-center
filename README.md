# Graph Report - nextcalibur-control-center  (2026-10-11)

## Corpus Check
- 158 files · ~211,449 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2699 nodes · 5781 edges · 172 communities (136 shown, 36 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 291 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `643cb5de`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- DotNetRuntimeDependency
- MainWindow
- GpuClockReader
- FnShortcut
- RunawayWatchTests
- ProtectedStore
- TrayPresence
- Window
- IShellLinkW
- .CheckProgram
- UserPresence
- PlanTuning
- CpuPowerReader
- Nextcalibur.App
- RadioButton
- .Get
- .OnLoaded
- AppSettings
- Nextcalibur.Core.Configuration
- WindowsFaults
- SystemInfo
- WindowsFaultsTests
- Fact
- .RequestRestart
- SystemMode
- Program
- RoutedEventArgs
- ProcessPresence
- Nextcalibur.Core.Hardware
- EcMailbox
- CoolingPolicyTests
- .Get
- .PlaceTourStep
- .SignatureIsValid
- Fact
- Authenticode
- Button
- microsoft_win32
- Elevation
- .Warn
- PowerOverlayService
- .Read
- Fact
- MemoryTrimmer
- .Migrate
- Nextcalibur.Core.csproj
- DependencyStatus
- .ApplyFnShortcuts
- analyse-autopsy.py
- .Check
- .Main
- ToggleButton
- ResourceDictionary
- UpdateService
- .MayStartWithoutPrompt
- LedBrightness
- .GetColour
- ColourWheel
- .Ask
- .OnSystemModeChanged
- LedController
- LedState
- FanGauge
- HostileFilesTests
- Unelevated
- DonutGauge
- ReleaseFile
- SegmentedBar
- ProcessMetrics
- LogInjectionTests
- SystemTools
- CoreLoad
- .OnGpuModeChanged
- SettingsTests.cs
- InstallFolderGuard
- .HasAccess
- BatteryModePolicy
- StringsDictionaryTests
- ValueConverters.cs
- Nextcalibur 0.5.2
- Nextcalibur Control Center
- Strings
- Grid
- .WireSettingsPage
- Dependency
- SmiCommandTests
- Hardware Protocol
- Nextcalibur 0.5.3
- Nextcalibur 0.5.4
- ThemeService
- .Sanitised
- SupportVerdict
- AppIdentity.cs
- Border
- NvidiaDriverState
- .CalculateThreadShare
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- LedEffect
- Log
- PawnIoDependency
- .IsSystemFileSignedBy
- ProfileFiles
- Probe
- Nextcalibur 0.5.1
- StackPanel
- IntegrityKind
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Releasing, and signing
- DriveRow
- .Pick
- TourPage
- Log-Graphics.ps1
- Nextcalibur.Core.Power
- Privacy
- Security
- .PowerModeControls
- .OnThresholdMoved
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
- BacklightKeyWatcher
- .ArrangeOverride
- BrightnessSlider
- .SendAsync
- Tools
- 0.5.7.md
- CpuWarnValue
- ColGauge
- DialogProgress
- DriveDot
- DriveList
- EffectPanel
- FnSelectAll
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
1. `MainWindow` - 278 edges
2. `Window` - 168 edges
3. `AppSettings` - 70 edges
4. `Nextcalibur.Core.Hardware` - 49 edges
5. `WindowsFaultsTests` - 40 edges
6. `RadioButton` - 39 edges
7. `TextBlock` - 36 edges
8. `WindowsFaults` - 33 edges
9. `Nextcalibur.Core.Configuration` - 32 edges
10. `GpuClockReader` - 31 edges

## Surprising Connections (you probably didn't know these)
- `Deep memory & resource leak eradication` --references--> `MainWindow`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.App/MainWindow.Cooling.cs
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

## Communities (172 total, 36 thin omitted)

### Community 0 - "DotNetRuntimeDependency"
Cohesion: 0.05
Nodes (46): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+38 more)

### Community 1 - "MainWindow"
Cohesion: 0.04
Nodes (42): CheckBox, DeferredOverheat, HwndSource, ObservableCollection, Problem, Queue, DateTime, MainWindow (+34 more)

### Community 2 - "GpuClockReader"
Cohesion: 0.08
Nodes (22): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+14 more)

### Community 3 - "FnShortcut"
Cohesion: 0.07
Nodes (33): DllImport, IEnumerable, IntPtr, List, StringBuilder, FnAction, FanTurbo, KeyboardBacklight (+25 more)

### Community 4 - "RunawayWatchTests"
Cohesion: 0.07
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 5 - "ProtectedStore"
Cohesion: 0.09
Nodes (20): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+12 more)

### Community 6 - "TrayPresence"
Cohesion: 0.06
Nodes (21): ContextMenuStrip, DevPropKey, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, EventHandler (+13 more)

### Community 7 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 8 - "IShellLinkW"
Cohesion: 0.07
Nodes (13): PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder, AppIdentity (+5 more)

### Community 9 - ".CheckProgram"
Cohesion: 0.10
Nodes (15): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvmlPath, IntegrityFinding (+7 more)

### Community 10 - "UserPresence"
Cohesion: 0.10
Nodes (20): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr, MarshalAs (+12 more)

### Community 11 - "PlanTuning"
Cohesion: 0.13
Nodes (16): AcDc, Floor, Original, Plan, Action, DllImport, Guid, IntPtr (+8 more)

### Community 12 - "CpuPowerReader"
Cohesion: 0.09
Nodes (21): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+13 more)

### Community 13 - "Nextcalibur.App"
Cohesion: 0.09
Nodes (23): Nextcalibur.App, Nextcalibur.App.Controls, system_componentmodel, system_drawing, system_io, system_linq, system_runtime_compilerservices, system_windows (+15 more)

### Community 14 - "RadioButton"
Cohesion: 0.07
Nodes (36): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeDiscrete, ModeGaming (+28 more)

### Community 15 - ".Get"
Cohesion: 0.09
Nodes (6): SolidColorBrush, Func, MessageBoxButton, MessageBoxResult, Action, Toasts

### Community 16 - ".OnLoaded"
Cohesion: 0.11
Nodes (6): AssemblyInformationalVersionAttribute, CancelEventArgs, Asynchronous dispatcher & UI thread hardening, Task, IProgress, Exception

### Community 17 - "AppSettings"
Cohesion: 0.05
Nodes (35): FnShortcut, JsonElement, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoCooling, AutoFanTurbo, AutoInstallUpdates (+27 more)

### Community 18 - "Nextcalibur.Core.Configuration"
Cohesion: 0.12
Nodes (10): Nextcalibur.Core.Tests, Nextcalibur.Core.Security, Nextcalibur.Core.Configuration, Nextcalibur.Core.Tests.Attacks, system_security_cryptography_x509certificates, system_text_regularexpressions, system_xml_linq, Fact (+2 more)

### Community 19 - "WindowsFaults"
Cohesion: 0.11
Nodes (21): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, DateTime, DllImport, Func (+13 more)

### Community 20 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 21 - "WindowsFaultsTests"
Cohesion: 0.17
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 22 - "Fact"
Cohesion: 0.14
Nodes (9): Func, IEnumerable, IReadOnlyList, Version, RetirementEntry, Trace, Fact, FootprintTests (+1 more)

### Community 23 - ".RequestRestart"
Cohesion: 0.11
Nodes (11): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr (+3 more)

### Community 24 - "SystemMode"
Cohesion: 0.15
Nodes (13): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, SystemMode (+5 more)

### Community 25 - "Program"
Cohesion: 0.12
Nodes (8): ConsoleColor, B, G, R, Program, DateTimeOffset, ThermalReader, ThermalSample

### Community 26 - "RoutedEventArgs"
Cohesion: 0.09
Nodes (9): RoutedPropertyChangedEventArgs, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, HideToTrayButton, Action (+1 more)

### Community 27 - "ProcessPresence"
Cohesion: 0.13
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 28 - "Nextcalibur.Core.Hardware"
Cohesion: 0.12
Nodes (8): Nextcalibur.Cli, Nextcalibur.Core.Input, Nextcalibur.Core.Hardware, microsoft_win32_safehandles, system_diagnostics, system_reflection, system_runtime_interopservices, system_text

### Community 29 - "EcMailbox"
Cohesion: 0.16
Nodes (10): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+2 more)

### Community 30 - "CoolingPolicyTests"
Cohesion: 0.12
Nodes (16): At, Margin, DateTime, List, TimeSpan, CoolingPolicy, DegreesPerMinute, Engaged (+8 more)

### Community 31 - ".Get"
Cohesion: 0.13
Nodes (13): ArgumentNullException, FirmwareModeReading, GpuMode, Discrete, Hybrid, Uma, GpuModeService, SwitchOutcome (+5 more)

### Community 32 - ".PlaceTourStep"
Cohesion: 0.09
Nodes (12): SizeChangedEventArgs, FrameworkElement, KeyEventArgs, Point, RoutedEventArgs, Size, TourStep, Body (+4 more)

### Community 33 - ".SignatureIsValid"
Cohesion: 0.15
Nodes (10): X509Certificate2, Fact, InlineData, Theory, X509Certificate2, SignatureForgeryTests, SignedByMicrosoft, Fact (+2 more)

### Community 34 - "Fact"
Cohesion: 0.18
Nodes (7): IList, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 35 - "Authenticode"
Cohesion: 0.23
Nodes (15): CatalogInfo, Security, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle (+7 more)

### Community 36 - "Button"
Cohesion: 0.09
Nodes (23): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, MinimiseButton (+15 more)

### Community 37 - "microsoft_win32"
Cohesion: 0.14
Nodes (10): Nextcalibur.Core.Dependencies, microsoft_win32, Theme, Dark, Light, system_collections_concurrent, system_net, system_net_http (+2 more)

### Community 38 - "Elevation"
Cohesion: 0.17
Nodes (4): dynamic, Encoding, Elevation, CardSwitchTasks

### Community 39 - ".Warn"
Cohesion: 0.15
Nodes (3): RegistryKey, Footprint, NduFix

### Community 40 - "PowerOverlayService"
Cohesion: 0.16
Nodes (11): DllImport, Guid, IReadOnlyList, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, PowerOverlays (+3 more)

### Community 41 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 42 - "Fact"
Cohesion: 0.13
Nodes (10): RepairOutcome, Empty, Fact, Task, CleanMachineTests, Guarded, Unguarded, CoexistencePolicyTests (+2 more)

### Community 43 - "MemoryTrimmer"
Cohesion: 0.14
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 44 - ".Migrate"
Cohesion: 0.13
Nodes (8): DateTime, Dictionary, List, PendingRestartInfo, BootTimeUtc, ReasonArguments, Reasons, SettingsTests

### Community 45 - "Nextcalibur.Core.csproj"
Cohesion: 0.12
Nodes (13): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Management (10.0.12), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk, net10.0-windows (+5 more)

### Community 46 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 47 - ".ApplyFnShortcuts"
Cohesion: 0.13
Nodes (3): DllImport, IntPtr, KeyEventArgs

### Community 48 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 49 - ".Check"
Cohesion: 0.19
Nodes (5): CommonAce, RawSecurityDescriptor, RegistryKey, MailboxAccess, MailboxAccessTests

### Community 50 - ".Main"
Cohesion: 0.19
Nodes (6): The threat model, and what the code does about it, Application, DllImport, Mutex, App, STAThread

### Community 51 - "ToggleButton"
Cohesion: 0.11
Nodes (18): LedPower, OverheatWarningToggle, SelectAll, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost (+10 more)

### Community 52 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 53 - "UpdateService"
Cohesion: 0.14
Nodes (12): DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService, Available, AvailableVersion (+4 more)

### Community 54 - ".MayStartWithoutPrompt"
Cohesion: 0.22
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 55 - "LedBrightness"
Cohesion: 0.13
Nodes (11): EventArrivedEventArgs, FanTurbo, Known, LedBrightness, Full, Half, Off, Fact (+3 more)

### Community 56 - ".GetColour"
Cohesion: 0.24
Nodes (7): B, G, R, Fact, InlineData, Theory, LedStateTests

### Community 57 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 58 - ".Ask"
Cohesion: 0.35
Nodes (7): HttpStatusCode, Fact, InlineData, Task, Theory, Answer, HostileAnswerTests

### Community 59 - ".OnSystemModeChanged"
Cohesion: 0.23
Nodes (5): PowerModeChangedEventArgs, PowerModeBalanced, PowerModeBest, PowerModeBetter, PowerModeEfficiency

### Community 60 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 61 - "LedState"
Cohesion: 0.14
Nodes (14): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedState, ActiveProfile, BrightnessPercent (+6 more)

### Community 62 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 63 - "HostileFilesTests"
Cohesion: 0.27
Nodes (5): IOException, Fact, InlineData, Theory, HostileFilesTests

### Community 64 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 65 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 66 - "ReleaseFile"
Cohesion: 0.22
Nodes (9): CancellationToken, HttpClient, Task, Uri, ReleaseFile, CancellationToken, HttpClient, Task (+1 more)

### Community 67 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 68 - "ProcessMetrics"
Cohesion: 0.15
Nodes (13): Share, Dictionary, TimeSpan, FaultEvidence, ProcessMetrics, Name, PageFaultCount, SessionId (+5 more)

### Community 69 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 70 - "SystemTools"
Cohesion: 0.17
Nodes (9): FileStream, SystemTools, Cmd, Explorer, Pnputil, Powercfg, PowerShell, Schtasks (+1 more)

### Community 71 - "CoreLoad"
Cohesion: 0.17
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 73 - "SettingsTests.cs"
Cohesion: 0.23
Nodes (7): MailboxAvailability, AccessNotGranted, Available, NotSupported, system_security_accesscontrol, system_security_cryptography, system_security_principal

### Community 74 - "InstallFolderGuard"
Cohesion: 0.33
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 75 - ".HasAccess"
Cohesion: 0.28
Nodes (5): SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 76 - "BatteryModePolicy"
Cohesion: 0.42
Nodes (4): BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

### Community 77 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 78 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 79 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 80 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 81 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 82 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 84 - "Dependency"
Cohesion: 0.17
Nodes (9): Version, Dependency, ExpectedSigner, Id, MayBeRemoved, Name, Purpose, SilentInstallArguments (+1 more)

### Community 85 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 86 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 87 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 88 - "Nextcalibur 0.5.4"
Cohesion: 0.18
Nodes (10): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, the second pass, Smaller, Tested on, The language row, The .NET runtime is now kept current like everything else (+2 more)

### Community 89 - "ThemeService"
Cohesion: 0.18
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 90 - ".Sanitised"
Cohesion: 0.29
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 91 - "SupportVerdict"
Cohesion: 0.20
Nodes (10): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+2 more)

### Community 92 - "AppIdentity.cs"
Cohesion: 0.20
Nodes (6): Nextcalibur.Core, system_globalization, system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 93 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 94 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 95 - ".CalculateThreadShare"
Cohesion: 0.27
Nodes (4): IReadOnlyDictionary, Stable, Dictionary, TopSharePercent

### Community 96 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 97 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 98 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 99 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 100 - "README.md"
Cohesion: 0.33
Nodes (3): How it works, Questions people ask, Using it

### Community 101 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 102 - "LedEffect"
Cohesion: 0.22
Nodes (8): LedEffect, Blink, Breathing, ColourCycle, Heartbeat, Off, Static, Wave

### Community 104 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 105 - ".IsSystemFileSignedBy"
Cohesion: 0.39
Nodes (3): Fact, SystemFileTrustTests, Cmd

### Community 107 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 108 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 109 - "StackPanel"
Cohesion: 0.25
Nodes (8): BannerActions, DialogActions, DriveTextStack, FnShortcutRows, PanelWindowsFaultsSubOptions, ProfileRow, SettingUpdatesAutomatic, StackPanel

### Community 110 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 111 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 112 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 113 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 114 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 115 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 116 - "DriveRow"
Cohesion: 0.29
Nodes (6): INotifyPropertyChanged, DriveRow, Detail, Name, Percent, PercentText

### Community 117 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 118 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 119 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 121 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 122 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 123 - ".PowerModeControls"
Cohesion: 0.33
Nodes (5): Option, Button, IEnumerable, TextBlock, PowerModeOption

### Community 125 - ".ForegroundProcess"
Cohesion: 0.47
Nodes (3): DllImport, IntPtr, Name

### Community 126 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 127 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 128 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 129 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 130 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

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

### Community 142 - "BrightnessSlider"
Cohesion: 0.50
Nodes (4): BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 143 - ".SendAsync"
Cohesion: 0.50
Nodes (3): CancellationToken, HttpRequestMessage, HttpResponseMessage

### Community 144 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 147 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

## Knowledge Gaps
- **441 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+436 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 833 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **36 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `GpuClockReader`, `FnShortcut`, `RunawayWatchTests`, `TrayPresence`, `Window`, `.CheckProgram`, `UserPresence`, `PlanTuning`, `BacklightKeyWatcher`, `CpuPowerReader`, `RadioButton`, `.Get`, `.OnLoaded`, `AppSettings`, `Nextcalibur.App`, `WindowsFaults`, `SystemInfo`, `.RequestRestart`, `SystemMode`, `Program`, `RoutedEventArgs`, `Nextcalibur.Core.Hardware`, `EcMailbox`, `CoolingPolicyTests`, `.Get`, `.PlaceTourStep`, `PowerOverlayService`, `.Read`, `DependencyStatus`, `.ApplyFnShortcuts`, `UpdateService`, `.OnSystemModeChanged`, `LedController`, `.OnGpuModeChanged`, `BatteryModePolicy`, `Strings`, `Grid`, `.WireSettingsPage`, `ThemeService`, `SupportVerdict`, `NvidiaDriverState`, `LedEffect`, `DriveRow`, `TourPage`, `.PowerModeControls`, `.OnThresholdMoved`, `.ForegroundProcess`, `LedZone`?**
  _High betweenness centrality (0.501) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Keycaps`, `DriveGauge`, `BrightnessSlider`, `RadioButton`, `CpuWarnValue`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `RoutedEventArgs`, `EffectPanel`, `FnSelectAll`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `Button`, `ToggleButton`, `.OnSystemModeChanged`, `Grid`, `Border`, `StackPanel`, `Text`?**
  _High betweenness centrality (0.123) - this node is a cross-community bridge._
- **Why does `AppSettings` connect `AppSettings` to `MainWindow`, `microsoft_win32`, `TrayPresence`, `.Warn`, `ProtectedStore`, `.Migrate`, `.OnLoaded`, `Strings`, `WindowsFaultsTests`, `Fact`, `SystemMode`, `ThemeService`, `.Sanitised`, `HostileFilesTests`?**
  _High betweenness centrality (0.069) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _441 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DotNetRuntimeDependency` be split into smaller, more focused modules?**
  _Cohesion score 0.05348101265822785 - nodes in this community are weakly interconnected._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.041826923076923074 - nodes in this community are weakly interconnected._