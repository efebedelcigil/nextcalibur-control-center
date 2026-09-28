# Graph Report - nextcalibur-control-center  (2026-09-28)

## Corpus Check
- 146 files · ~200,627 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2442 nodes · 5163 edges · 158 communities (122 shown, 36 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 257 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `aa607fdb`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- DotNetRuntimeDependency
- SystemModeService
- MainWindow
- UserPresence
- Authenticode
- AppSettings
- ProtectedStore
- .CheckProgram
- GpuClockReader
- TrayPresence
- Window
- IShellLinkW
- RadioButton
- CpuPowerReader
- Nextcalibur.App
- .OnLoaded
- WindowsFaults
- SystemInfo
- SystemTools
- WindowsFaultsTests
- .RequestRestart
- .Get
- ProcessPresence
- Nextcalibur.Core.Hardware
- Fact
- Fact
- EcMailbox
- LedState
- .Warn
- .Get
- Button
- .OnClosing
- Nextcalibur.Core.Security
- .Main
- Program
- .Check
- .Warn
- SystemMode
- Nextcalibur.Core.csproj
- DependencyStatus
- .StartSlowTimer
- analyse-autopsy.py
- .Retirements
- CoreLoad
- system_runtime_interopservices
- ResourceDictionary
- SmiCommand
- system_diagnostics
- Nextcalibur.Core.Power
- .OnGpuModeChanged
- ColourWheel
- BacklightKeyWatcher
- .Ask
- UpdateService
- LedController
- FanGauge
- Unelevated
- ProcessMetrics
- DonutGauge
- ToggleButton
- SegmentedBar
- LedEffect
- .IsUnderProgramFiles
- LogInjectionTests
- PawnIoDependency
- .HasAccess
- BatteryModePolicy
- StringsDictionaryTests
- ValueConverters.cs
- Nextcalibur 0.5.2
- Nextcalibur 0.5.4
- Nextcalibur Control Center
- Strings
- Grid
- Dependency
- Hardware Protocol
- Nextcalibur 0.5.3
- ThemeService
- .Sanitised
- ReleaseFile
- CardSwitchTasks
- Border
- NvidiaDriverState
- SupportVerdict
- .CalculateThreadShare
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- .WatchWindowsItself
- Log
- Probe
- Nextcalibur 0.5.1
- IntegrityKind
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Releasing, and signing
- DriveRow
- .Pick
- .OnThresholdMoved
- StackPanel
- TourPage
- Log-Graphics.ps1
- Privacy
- Security
- .OnBrightnessChanged
- .Sample
- LedZone
- PowerSource
- Notice
- Nextcalibur 0.5.11
- .Set
- SmiSubsystem
- StartupPreferenceTests
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Strings.cs
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- .ArrangeOverride
- .TryParseRgb
- MailboxAvailability
- .SendAsync
- Tools
- 0.5.7.md
- CpuWarnValue
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
1. `MainWindow` - 229 edges
2. `Window` - 163 edges
3. `AppSettings` - 64 edges
4. `Nextcalibur.Core.Hardware` - 42 edges
5. `WindowsFaultsTests` - 40 edges
6. `RadioButton` - 39 edges
7. `TextBlock` - 36 edges
8. `WindowsFaults` - 33 edges
9. `GpuClockReader` - 31 edges
10. `Nextcalibur.Core.Configuration` - 30 edges

## Surprising Connections (you probably didn't know these)
- `Deep memory & resource leak eradication` --references--> `MainWindow`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.App/MainWindow.Integrity.cs
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

## Communities (158 total, 36 thin omitted)

### Community 0 - "DotNetRuntimeDependency"
Cohesion: 0.05
Nodes (46): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+38 more)

### Community 1 - "SystemModeService"
Cohesion: 0.06
Nodes (30): InvalidOperationException, DllImport, Guid, IReadOnlyList, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck (+22 more)

### Community 2 - "MainWindow"
Cohesion: 0.05
Nodes (35): DeferredOverheat, ObservableCollection, Queue, HideToTrayButton, DateTime, HashSet, List, TimeSpan (+27 more)

### Community 3 - "UserPresence"
Cohesion: 0.06
Nodes (33): MEMORYSTATUSEX, MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, Dictionary, DllImport (+25 more)

### Community 4 - "Authenticode"
Cohesion: 0.08
Nodes (27): CatalogInfo, DefaultDllImportSearchPaths, DllImport, Guid, IntPtr, MarshalAs, SafeFileHandle, X509Certificate2 (+19 more)

### Community 5 - "AppSettings"
Cohesion: 0.05
Nodes (39): DateTime, Dictionary, JsonElement, List, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoInstallUpdates (+31 more)

### Community 6 - "ProtectedStore"
Cohesion: 0.09
Nodes (20): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+12 more)

### Community 7 - ".CheckProgram"
Cohesion: 0.08
Nodes (18): Lazy, ProcessModule, FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Dictionary, HashSet (+10 more)

### Community 8 - "GpuClockReader"
Cohesion: 0.09
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 9 - "TrayPresence"
Cohesion: 0.06
Nodes (21): ContextMenuStrip, DevPropKey, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, EventHandler (+13 more)

### Community 10 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 11 - "IShellLinkW"
Cohesion: 0.07
Nodes (13): PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder, AppIdentity (+5 more)

### Community 12 - "RadioButton"
Cohesion: 0.06
Nodes (40): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeDiscrete, ModeGaming (+32 more)

### Community 13 - "CpuPowerReader"
Cohesion: 0.09
Nodes (21): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+13 more)

### Community 14 - "Nextcalibur.App"
Cohesion: 0.10
Nodes (21): Nextcalibur.App, Nextcalibur.App.Controls, system_componentmodel, system_drawing, system_io, system_linq, system_runtime_compilerservices, system_windows (+13 more)

### Community 15 - ".OnLoaded"
Cohesion: 0.12
Nodes (5): AssemblyInformationalVersionAttribute, Asynchronous dispatcher & UI thread hardening, Task, IProgress, Exception

### Community 16 - "WindowsFaults"
Cohesion: 0.11
Nodes (21): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, DateTime, DllImport, Func (+13 more)

### Community 17 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 18 - "SystemTools"
Cohesion: 0.10
Nodes (14): FileStream, IOException, IEnumerable, ProfileFiles, SystemTools, Cmd, Explorer, Pnputil (+6 more)

### Community 19 - "WindowsFaultsTests"
Cohesion: 0.18
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 20 - ".RequestRestart"
Cohesion: 0.11
Nodes (11): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr (+3 more)

### Community 21 - ".Get"
Cohesion: 0.10
Nodes (8): SizeChangedEventArgs, SolidColorBrush, Point, Size, Func, MessageBoxButton, MessageBoxResult, IntegrityFinding

### Community 22 - "ProcessPresence"
Cohesion: 0.13
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 23 - "Nextcalibur.Core.Hardware"
Cohesion: 0.17
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Configuration, Nextcalibur.Core.Hardware, system_text_regularexpressions, system_xml_linq, xunit

### Community 24 - "Fact"
Cohesion: 0.16
Nodes (9): IList, GpuConfiguration, HardwareSupport, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests (+1 more)

### Community 25 - "Fact"
Cohesion: 0.15
Nodes (9): Func, IEnumerable, IReadOnlyList, Version, RetirementEntry, Trace, Fact, FootprintTests (+1 more)

### Community 26 - "EcMailbox"
Cohesion: 0.13
Nodes (13): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+5 more)

### Community 27 - "LedState"
Cohesion: 0.15
Nodes (15): B, G, R, LedState, ActiveProfile, BrightnessPercent, Current, Effect (+7 more)

### Community 28 - ".Warn"
Cohesion: 0.13
Nodes (7): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Action, ToggleButton

### Community 29 - ".Get"
Cohesion: 0.16
Nodes (12): ArgumentNullException, FirmwareModeReading, GpuMode, Discrete, Hybrid, Uma, GpuModeService, SwitchOutcome (+4 more)

### Community 30 - "Button"
Cohesion: 0.09
Nodes (23): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, MinimiseButton (+15 more)

### Community 31 - ".OnClosing"
Cohesion: 0.10
Nodes (10): CancelEventArgs, KeyEventArgs, FrameworkElement, RoutedEventArgs, TourStep, Body, SectionName, Title (+2 more)

### Community 32 - "Nextcalibur.Core.Security"
Cohesion: 0.17
Nodes (9): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_security_cryptography_x509certificates, system_text_json (+1 more)

### Community 33 - ".Main"
Cohesion: 0.16
Nodes (6): The threat model, and what the code does about it, Application, DllImport, Mutex, App, STAThread

### Community 34 - "Program"
Cohesion: 0.17
Nodes (5): ConsoleColor, Program, DateTimeOffset, ThermalReader, ThermalSample

### Community 35 - ".Check"
Cohesion: 0.16
Nodes (6): CommonAce, RawSecurityDescriptor, RegistryKey, MailboxAccess, IdempotenceTests, MailboxAccessTests

### Community 36 - ".Warn"
Cohesion: 0.17
Nodes (3): RegistryKey, Footprint, NduFix

### Community 37 - "SystemMode"
Cohesion: 0.16
Nodes (12): SmiFamily, Read, Write, ThermalProfile, SystemMode, Gaming, Office, Performance (+4 more)

### Community 38 - "Nextcalibur.Core.csproj"
Cohesion: 0.12
Nodes (13): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Management (10.0.12), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk, net10.0-windows (+5 more)

### Community 39 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 40 - ".StartSlowTimer"
Cohesion: 0.17
Nodes (4): Option, PowerModeChangedEventArgs, Button, IEnumerable

### Community 41 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 42 - ".Retirements"
Cohesion: 0.23
Nodes (4): dynamic, Elevation, Fact, TaskFolderTests

### Community 43 - "CoreLoad"
Cohesion: 0.12
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 44 - "system_runtime_interopservices"
Cohesion: 0.12
Nodes (6): system_io_directory, system_io_ioexception, system_io_path, system_management, system_runtime_interopservices, system_runtime_interopservices_comtypes

### Community 45 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 46 - "SmiCommand"
Cohesion: 0.21
Nodes (6): ArgumentException, SmiCommand, Fact, InlineData, Theory, SmiCommandTests

### Community 47 - "system_diagnostics"
Cohesion: 0.19
Nodes (6): Nextcalibur.Cli, system_diagnostics, system_security_accesscontrol, system_security_cryptography, system_security_principal, system_text

### Community 48 - "Nextcalibur.Core.Power"
Cohesion: 0.13
Nodes (8): Nextcalibur.Core.Power, microsoft_win32, microsoft_win32_safehandles, Theme, Dark, Light, system_reflection, system_security

### Community 50 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 51 - "BacklightKeyWatcher"
Cohesion: 0.15
Nodes (11): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, LedBrightness, Full, Half, Off, Fact (+3 more)

### Community 52 - ".Ask"
Cohesion: 0.35
Nodes (7): HttpStatusCode, Fact, InlineData, Task, Theory, Answer, HostileAnswerTests

### Community 53 - "UpdateService"
Cohesion: 0.17
Nodes (12): DateTime, DispatcherTimer, HashSet, Task, TimeSpan, UpdateService, Available, AvailableVersion (+4 more)

### Community 54 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 55 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 56 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 57 - "ProcessMetrics"
Cohesion: 0.15
Nodes (14): Share, Dictionary, TimeSpan, FaultEvidence, ProcessMetrics, Name, PageFaultCount, Pid (+6 more)

### Community 58 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 59 - "ToggleButton"
Cohesion: 0.13
Nodes (15): LedPower, OverheatWarningToggle, SelectAll, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost, SettingFixWidgets, SettingOfficeOnBattery (+7 more)

### Community 60 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 61 - "LedEffect"
Cohesion: 0.14
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 62 - ".IsUnderProgramFiles"
Cohesion: 0.23
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 63 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 64 - "PawnIoDependency"
Cohesion: 0.15
Nodes (11): CancellationToken, HttpClient, Task, Version, PawnIoDependency, ExpectedSigner, Id, Name (+3 more)

### Community 65 - ".HasAccess"
Cohesion: 0.28
Nodes (5): SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 66 - "BatteryModePolicy"
Cohesion: 0.42
Nodes (4): BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

### Community 67 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 68 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 69 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 70 - "Nextcalibur 0.5.4"
Cohesion: 0.17
Nodes (11): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+3 more)

### Community 71 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 72 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 73 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 74 - "Dependency"
Cohesion: 0.17
Nodes (9): Version, Dependency, ExpectedSigner, Id, MayBeRemoved, Name, Purpose, SilentInstallArguments (+1 more)

### Community 75 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 76 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 77 - "ThemeService"
Cohesion: 0.18
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 78 - ".Sanitised"
Cohesion: 0.29
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 79 - "ReleaseFile"
Cohesion: 0.33
Nodes (6): CancellationToken, HttpClient, Task, Uri, ReleaseFile, HttpClient

### Community 81 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 82 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 83 - "SupportVerdict"
Cohesion: 0.22
Nodes (9): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites (+1 more)

### Community 84 - ".CalculateThreadShare"
Cohesion: 0.27
Nodes (4): IReadOnlyDictionary, Stable, Dictionary, TopSharePercent

### Community 85 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 86 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 87 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 88 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 89 - "README.md"
Cohesion: 0.33
Nodes (3): How it works, Questions people ask, Using it

### Community 90 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 93 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 94 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 95 - "IntegrityKind"
Cohesion: 0.25
Nodes (8): IntegrityKind, FolderWritable, ModuleUnsigned, NvmlUntrusted, PawnIoUntrusted, ProgramChanged, ProgramUnverified, RuntimeUntrusted

### Community 96 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 97 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 98 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 99 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 100 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 101 - "DriveRow"
Cohesion: 0.29
Nodes (6): INotifyPropertyChanged, DriveRow, Detail, Name, Percent, PercentText

### Community 102 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 104 - "StackPanel"
Cohesion: 0.29
Nodes (7): BannerActions, DialogActions, DriveTextStack, PanelWindowsFaultsSubOptions, ProfileRow, SettingUpdatesAutomatic, StackPanel

### Community 105 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 106 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 107 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 108 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 109 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 111 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 112 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 113 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 114 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 116 - "SmiSubsystem"
Cohesion: 0.40
Nodes (5): SmiSubsystem, DisplayMode, Led, Profile, Thermal

### Community 118 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 120 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 121 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 122 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 124 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 125 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 126 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 128 - ".TryParseRgb"
Cohesion: 0.50
Nodes (3): B, G, R

### Community 129 - "MailboxAvailability"
Cohesion: 0.50
Nodes (4): MailboxAvailability, AccessNotGranted, Available, NotSupported

### Community 130 - ".SendAsync"
Cohesion: 0.50
Nodes (3): CancellationToken, HttpRequestMessage, HttpResponseMessage

### Community 131 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 134 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

## Knowledge Gaps
- **404 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+399 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 758 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **36 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `SystemModeService`, `UserPresence`, `AppSettings`, `GpuClockReader`, `TrayPresence`, `Window`, `RadioButton`, `CpuPowerReader`, `Nextcalibur.App`, `.OnLoaded`, `WindowsFaults`, `.RequestRestart`, `.Get`, `EcMailbox`, `.Warn`, `.Get`, `.OnClosing`, `Program`, `SystemMode`, `DependencyStatus`, `.StartSlowTimer`, `.OnGpuModeChanged`, `BacklightKeyWatcher`, `UpdateService`, `LedController`, `LedEffect`, `BatteryModePolicy`, `Strings`, `Grid`, `ThemeService`, `NvidiaDriverState`, `SupportVerdict`, `.WatchWindowsItself`, `.OnThresholdMoved`, `TourPage`, `.OnBrightnessChanged`, `.Sample`, `LedZone`?**
  _High betweenness centrality (0.469) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `CpuWarnValue`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `RadioButton`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `Button`, `ToggleButton`, `Grid`, `Border`, `Text`, `StackPanel`, `.OnBrightnessChanged`, `Keycaps`, `DriveGauge`?**
  _High betweenness centrality (0.154) - this node is a cross-community bridge._
- **Why does `DependencyStatus` connect `DependencyStatus` to `Nextcalibur.Core.Security`, `MainWindow`, `Dependency`, `ReleaseFile`, `.OnLoaded`?**
  _High betweenness centrality (0.085) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _404 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DotNetRuntimeDependency` be split into smaller, more focused modules?**
  _Cohesion score 0.05348101265822785 - nodes in this community are weakly interconnected._
- **Should `SystemModeService` be split into smaller, more focused modules?**
  _Cohesion score 0.05711849957374254 - nodes in this community are weakly interconnected._