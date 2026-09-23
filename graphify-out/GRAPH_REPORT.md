# Graph Report - nextcalibur-control-center  (2026-09-23)

## Corpus Check
- 133 files · ~188,525 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2247 nodes · 4682 edges · 154 communities (119 shown, 35 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 243 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `ad4ce359`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- DotNetRuntimeDependency
- UserPresence
- GpuClockReader
- MainWindow
- TrayPresence
- .SignatureIsValid
- Window
- IShellLinkW
- RadioButton
- CpuPowerReader
- WindowsFaults
- Fact
- SystemInfo
- .Get
- AppSettings
- WindowsFaultsTests
- SystemMode
- .StartSlowTimer
- ProcessPresence
- .RequestRestart
- .Main
- EcMailbox
- Fact
- LedState
- .Retirements
- .Warn
- .OnLoaded
- .Info
- InstallFolderGuard
- .Get
- Button
- Nextcalibur.Core.Hardware
- microsoft_win32
- Dependency
- Nextcalibur.Core.csproj
- DependencyStatus
- analyse-autopsy.py
- Nextcalibur.App
- UpdateService
- PowerOverlay.cs
- .Check
- CoreLoad
- system_runtime_interopservices
- ResourceDictionary
- Fact
- MainWindow.xaml.cs
- BacklightKeyWatcher
- LedController
- ToggleButton
- ColourWheel
- .Ask
- Program
- FanGauge
- Unelevated
- ProcessMetrics
- PowerOverlayService
- DonutGauge
- ThemeService
- .Survey
- SegmentedBar
- LedEffect
- .HasAccess
- LogInjectionTests
- system_diagnostics
- PawnIoDependency
- BatteryModePolicy
- StringsDictionaryTests
- Nextcalibur.Core.Configuration
- ValueConverters.cs
- Nextcalibur 0.5.2
- SystemTools
- Nextcalibur Control Center
- Strings
- Grid
- SmiCommandTests
- Hardware Protocol
- Nextcalibur 0.5.3
- DriveRow
- .Sanitised
- .Values_are_the_ones_measured_from_the_vendor
- HostileFilesTests
- Border
- PendingRestartInfo
- NvidiaDriverState
- .CalculateThreadShare
- WordsInCodeTests
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- ThermalSample
- .Migrate
- Log
- SupportVerdict
- Probe
- IntPtr
- Nextcalibur 0.5.1
- .Open
- ProfileFiles
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Releasing, and signing
- .Pick
- .OnThresholdMoved
- TourPage
- BatteryModePolicy.cs
- Log-Graphics.ps1
- Privacy
- Security
- StackPanel
- TourStep
- .A_polling_interval_from_the_file_is_a_sane_one
- LedZone
- .LatestAsync
- Notice
- .Set
- SmiSubsystem
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Nextcalibur 0.5.6
- .ArrangeOverride
- BrightnessSlider
- .TryParseRgb
- Tools
- 0.5.7.md
- CpuWarnValue
- SmiFamily
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
1. `MainWindow` - 209 edges
2. `Window` - 164 edges
3. `AppSettings` - 60 edges
4. `Nextcalibur.Core.Hardware` - 41 edges
5. `WindowsFaultsTests` - 40 edges
6. `RadioButton` - 39 edges
7. `TextBlock` - 36 edges
8. `TrayPresence` - 33 edges
9. `WindowsFaults` - 33 edges
10. `GpuClockReader` - 31 edges

## Surprising Connections (you probably didn't know these)
- `Deep memory & resource leak eradication` --references--> `MainWindow`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.App/MainWindow.Settings.cs
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

## Communities (154 total, 35 thin omitted)

### Community 0 - "DotNetRuntimeDependency"
Cohesion: 0.06
Nodes (43): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+35 more)

### Community 1 - "UserPresence"
Cohesion: 0.06
Nodes (33): MEMORYSTATUSEX, MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, Dictionary, DllImport (+25 more)

### Community 2 - "GpuClockReader"
Cohesion: 0.08
Nodes (23): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+15 more)

### Community 3 - "MainWindow"
Cohesion: 0.06
Nodes (28): DeferredOverheat, ObservableCollection, Queue, HideToTrayButton, Button, Grid, List, Ms (+20 more)

### Community 4 - "TrayPresence"
Cohesion: 0.06
Nodes (21): ContextMenuStrip, DevPropKey, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, EventHandler (+13 more)

### Community 5 - ".SignatureIsValid"
Cohesion: 0.07
Nodes (29): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+21 more)

### Community 6 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 7 - "IShellLinkW"
Cohesion: 0.07
Nodes (13): PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder, AppIdentity (+5 more)

### Community 8 - "RadioButton"
Cohesion: 0.06
Nodes (40): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeDiscrete, ModeGaming (+32 more)

### Community 9 - "CpuPowerReader"
Cohesion: 0.09
Nodes (21): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+13 more)

### Community 10 - "WindowsFaults"
Cohesion: 0.10
Nodes (22): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, DateTime, DllImport, Func (+14 more)

### Community 11 - "Fact"
Cohesion: 0.12
Nodes (8): Func, IEnumerable, Version, RetirementEntry, Fact, FootprintTests, MailboxAccessTests, StartupPreferenceTests

### Community 12 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 13 - ".Get"
Cohesion: 0.11
Nodes (7): Option, PowerModeChangedEventArgs, Button, IEnumerable, MessageBoxButton, MessageBoxResult, TextBlock

### Community 14 - "AppSettings"
Cohesion: 0.06
Nodes (28): JsonElement, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoInstallUpdates, CompensateWindowsFaults, CpuWarningTemperatureC, DisableNdu (+20 more)

### Community 16 - "SystemMode"
Cohesion: 0.15
Nodes (13): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, SystemMode (+5 more)

### Community 17 - ".StartSlowTimer"
Cohesion: 0.09
Nodes (5): DateTime, Func, Task, TimeSpan, DeferredOverheat

### Community 18 - "ProcessPresence"
Cohesion: 0.13
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 19 - ".RequestRestart"
Cohesion: 0.12
Nodes (11): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr (+3 more)

### Community 20 - ".Main"
Cohesion: 0.15
Nodes (6): The threat model, and what the code does about it, Application, DllImport, Mutex, App, STAThread

### Community 21 - "EcMailbox"
Cohesion: 0.17
Nodes (10): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+2 more)

### Community 22 - "Fact"
Cohesion: 0.16
Nodes (8): IList, HardwareSupport, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests, HardwareSupportTests

### Community 23 - "LedState"
Cohesion: 0.15
Nodes (15): B, G, R, LedState, ActiveProfile, BrightnessPercent, Current, Effect (+7 more)

### Community 24 - ".Retirements"
Cohesion: 0.18
Nodes (3): Encoding, Elevation, CardSwitchTasks

### Community 25 - ".Warn"
Cohesion: 0.13
Nodes (7): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Action, ToggleButton

### Community 26 - ".OnLoaded"
Cohesion: 0.14
Nodes (5): AssemblyInformationalVersionAttribute, Asynchronous dispatcher & UI thread hardening, Action, Toasts, Exception

### Community 27 - ".Info"
Cohesion: 0.11
Nodes (8): CancelEventArgs, KeyEventArgs, SizeChangedEventArgs, FrameworkElement, Point, RoutedEventArgs, Size, TourStep

### Community 28 - "InstallFolderGuard"
Cohesion: 0.14
Nodes (10): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Fact, InlineData, Theory, ElevationPolicyTests (+2 more)

### Community 29 - ".Get"
Cohesion: 0.16
Nodes (12): ArgumentNullException, FirmwareModeReading, GpuMode, Discrete, Hybrid, Uma, GpuModeService, SwitchOutcome (+4 more)

### Community 30 - "Button"
Cohesion: 0.09
Nodes (23): IsChecked, BannerDismissButton, BannerRestartButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, MinimiseButton (+15 more)

### Community 31 - "Nextcalibur.Core.Hardware"
Cohesion: 0.22
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, Nextcalibur.Core.Hardware, system_text_regularexpressions, system_xml_linq, xunit

### Community 32 - "microsoft_win32"
Cohesion: 0.17
Nodes (9): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, microsoft_win32, system_collections_concurrent, system_net, system_net_http, system_security_cryptography_x509certificates (+1 more)

### Community 33 - "Dependency"
Cohesion: 0.15
Nodes (14): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+6 more)

### Community 34 - "Nextcalibur.Core.csproj"
Cohesion: 0.12
Nodes (13): net8.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Management (8.0.0), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk, net8.0-windows (+5 more)

### Community 35 - "DependencyStatus"
Cohesion: 0.14
Nodes (15): Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress, IReadOnlyList, Task (+7 more)

### Community 36 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 37 - "Nextcalibur.App"
Cohesion: 0.11
Nodes (13): Nextcalibur.App, system_drawing, system_io, system_io_directory, system_io_ioexception, system_io_path, system_linq, system_runtime_interopservices_comtypes (+5 more)

### Community 38 - "UpdateService"
Cohesion: 0.14
Nodes (13): DispatcherTimer, Func, HashSet, IProgress, Task, TimeSpan, UpdateService, AutomaticChecksEnabled (+5 more)

### Community 39 - "PowerOverlay.cs"
Cohesion: 0.13
Nodes (13): IReadOnlyList, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, PowerModeOption, PowerOverlays, All (+5 more)

### Community 40 - ".Check"
Cohesion: 0.20
Nodes (7): CommonAce, RegistryKey, MailboxAccess, MailboxAvailability, AccessNotGranted, Available, NotSupported

### Community 41 - "CoreLoad"
Cohesion: 0.12
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 42 - "system_runtime_interopservices"
Cohesion: 0.13
Nodes (4): system_management, system_runtime_interopservices, system_text, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 43 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 44 - "Fact"
Cohesion: 0.14
Nodes (9): Mutex, TimeSpan, MachineWideGate, system_threading, Fact, Task, CoexistencePolicyTests, MachineWideGateTests (+1 more)

### Community 45 - "MainWindow.xaml.cs"
Cohesion: 0.24
Nodes (10): Nextcalibur.App.Controls, system_windows, system_windows_controls, system_windows_controls_button, system_windows_controls_primitives, system_windows_input, system_windows_input_keyeventargs, system_windows_media (+2 more)

### Community 46 - "BacklightKeyWatcher"
Cohesion: 0.14
Nodes (11): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, LedBrightness, Full, Half, Off, Fact (+3 more)

### Community 47 - "LedController"
Cohesion: 0.23
Nodes (5): RoutedPropertyChangedEventArgs, LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 48 - "ToggleButton"
Cohesion: 0.12
Nodes (17): LedPower, OverheatWarningToggle, SelectAll, SettingAutoCheckUpdates, SettingAutoInstallUpdates, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost (+9 more)

### Community 49 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 50 - ".Ask"
Cohesion: 0.35
Nodes (7): HttpStatusCode, Fact, InlineData, Task, Theory, Answer, HostileAnswerTests

### Community 52 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 53 - "Unelevated"
Cohesion: 0.35
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 54 - "ProcessMetrics"
Cohesion: 0.15
Nodes (14): Share, Dictionary, TimeSpan, FaultEvidence, ProcessMetrics, Name, PageFaultCount, Pid (+6 more)

### Community 55 - "PowerOverlayService"
Cohesion: 0.29
Nodes (4): DllImport, Guid, PowerOverlayService, IdempotenceTests

### Community 56 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 57 - "ThemeService"
Cohesion: 0.14
Nodes (11): Theme, Theme, Dark, Light, ThemePreference, Dark, Light, System (+3 more)

### Community 58 - ".Survey"
Cohesion: 0.18
Nodes (5): IReadOnlyList, RegistryKey, Footprint, SettingsPath, NduFix

### Community 59 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 60 - "LedEffect"
Cohesion: 0.14
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 61 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 62 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 63 - "system_diagnostics"
Cohesion: 0.19
Nodes (6): Nextcalibur.Cli, microsoft_win32_safehandles, system_diagnostics, system_reflection, system_security_accesscontrol, system_security_principal

### Community 64 - "PawnIoDependency"
Cohesion: 0.15
Nodes (11): CancellationToken, HttpClient, Task, Version, PawnIoDependency, ExpectedSigner, Id, Name (+3 more)

### Community 65 - "BatteryModePolicy"
Cohesion: 0.42
Nodes (4): BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

### Community 66 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 67 - "Nextcalibur.Core.Configuration"
Cohesion: 0.20
Nodes (6): Nextcalibur.Core, Nextcalibur.Core.Configuration, Trace, system_globalization, system_security, system_text_json_serialization

### Community 68 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 69 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 70 - "SystemTools"
Cohesion: 0.18
Nodes (9): FileStream, SystemTools, Cmd, Explorer, Pnputil, Powercfg, PowerShell, Schtasks (+1 more)

### Community 71 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 72 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 73 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 74 - "SmiCommandTests"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 75 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 76 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 77 - "DriveRow"
Cohesion: 0.18
Nodes (8): INotifyPropertyChanged, DriveRow, Detail, Name, Percent, PercentText, system_componentmodel, system_runtime_compilerservices

### Community 78 - ".Sanitised"
Cohesion: 0.29
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 79 - ".Values_are_the_ones_measured_from_the_vendor"
Cohesion: 0.24
Nodes (5): ThermalProfile, Fact, InlineData, Theory, ThermalProfileTests

### Community 80 - "HostileFilesTests"
Cohesion: 0.42
Nodes (3): IOException, Fact, HostileFilesTests

### Community 81 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 82 - "PendingRestartInfo"
Cohesion: 0.24
Nodes (7): DateTime, Dictionary, List, PendingRestartInfo, BootTimeUtc, ReasonArguments, Reasons

### Community 83 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 84 - ".CalculateThreadShare"
Cohesion: 0.27
Nodes (4): IReadOnlyDictionary, Stable, Dictionary, TopSharePercent

### Community 85 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 86 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 87 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 88 - "README.md"
Cohesion: 0.33
Nodes (3): How it works, Questions people ask, Using it

### Community 89 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 90 - "ThermalSample"
Cohesion: 0.33
Nodes (3): DateTimeOffset, ThermalReader, ThermalSample

### Community 93 - "SupportVerdict"
Cohesion: 0.25
Nodes (8): IReadOnlyList, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads, AllowsWrites

### Community 94 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 95 - "IntPtr"
Cohesion: 0.50
Nodes (3): DllImport, IntPtr, Program

### Community 96 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 99 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 100 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 101 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 102 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 103 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 105 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 106 - "BatteryModePolicy.cs"
Cohesion: 0.33
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 107 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 108 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 109 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 110 - "StackPanel"
Cohesion: 0.33
Nodes (6): BannerActions, DialogActions, DriveTextStack, PanelWindowsFaultsSubOptions, ProfileRow, StackPanel

### Community 111 - "TourStep"
Cohesion: 0.33
Nodes (5): TourStep, Body, SectionName, Title, TourPage

### Community 113 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 114 - ".LatestAsync"
Cohesion: 0.33
Nodes (4): CancellationToken, HttpClient, HttpRequestMessage, HttpResponseMessage

### Community 115 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 117 - "SmiSubsystem"
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

### Community 123 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 125 - "BrightnessSlider"
Cohesion: 0.50
Nodes (4): BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 126 - ".TryParseRgb"
Cohesion: 0.50
Nodes (3): B, G, R

### Community 127 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 130 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 131 - "SmiFamily"
Cohesion: 0.67
Nodes (3): SmiFamily, Read, Write

## Knowledge Gaps
- **372 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+367 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 698 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **35 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `UserPresence`, `GpuClockReader`, `TrayPresence`, `Window`, `RadioButton`, `CpuPowerReader`, `WindowsFaults`, `.Get`, `AppSettings`, `SystemMode`, `.StartSlowTimer`, `.RequestRestart`, `EcMailbox`, `.Warn`, `.OnLoaded`, `.Info`, `.Get`, `DependencyStatus`, `UpdateService`, `MainWindow.xaml.cs`, `BacklightKeyWatcher`, `LedController`, `PowerOverlayService`, `ThemeService`, `LedEffect`, `BatteryModePolicy`, `Strings`, `Grid`, `NvidiaDriverState`, `ThermalSample`, `SupportVerdict`, `.Open`, `.OnThresholdMoved`, `TourPage`, `TourStep`, `LedZone`?**
  _High betweenness centrality (0.521) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `CpuWarnValue`, `MainWindow`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `RadioButton`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `Button`, `ToggleButton`, `Grid`, `Border`, `Text`, `StackPanel`, `Keycaps`, `DriveGauge`, `BrightnessSlider`?**
  _High betweenness centrality (0.174) - this node is a cross-community bridge._
- **Why does `DependencyStatus` connect `DependencyStatus` to `microsoft_win32`, `Dependency`, `.OnLoaded`, `MainWindow`?**
  _High betweenness centrality (0.116) - this node is a cross-community bridge._
- **Are the 11 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 11 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _372 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DotNetRuntimeDependency` be split into smaller, more focused modules?**
  _Cohesion score 0.05789473684210526 - nodes in this community are weakly interconnected._
- **Should `UserPresence` be split into smaller, more focused modules?**
  _Cohesion score 0.061367621274108705 - nodes in this community are weakly interconnected._