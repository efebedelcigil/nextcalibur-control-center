# Graph Report - nextcalibur-control-center  (2026-09-23)

## Corpus Check
- 132 files · ~187,378 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2235 nodes · 4647 edges · 146 communities (117 shown, 29 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 243 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `1d1ed70c`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- DotNetRuntimeDependency
- MainWindow
- Dependency
- UserPresence
- GpuClockReader
- .SignatureIsValid
- .InstallAsync
- Window
- IShellLinkW
- CpuPowerReader
- RadioButton
- .Get
- WindowsFaults
- SystemInfo
- .Survey
- .OnLoaded
- Button
- WindowsFaultsTests
- Fact
- AppSettings
- system_runtime_interopservices
- .RequestRestart
- ProcessPresence
- EcMailbox
- SystemModeService
- Nextcalibur.Core.Hardware
- Fact
- .Info
- .OnGpuModeChanged
- Program
- .OfferDependency
- .Main
- UpdateService
- .OnSystemModeChanged
- Nextcalibur.App
- Nextcalibur.Core.Dependencies
- Nextcalibur.Core.csproj
- .Get
- analyse-autopsy.py
- .Migrate
- Fact
- PowerOverlayService
- ResourceDictionary
- MainWindow.xaml.cs
- BacklightKeyWatcher
- ToggleButton
- Elevation
- .GetColour
- ColourWheel
- Nextcalibur.Core.Configuration
- Unelevated
- CoreLoad
- LedController
- FanGauge
- ProcessMetrics
- DonutGauge
- LedState
- SystemMode
- SegmentedBar
- .HasAccess
- LogInjectionTests
- .FromBytes
- .Check
- ElevationPolicyTests
- BatteryModePolicy
- StringsDictionaryTests
- ValueConverters.cs
- DevicePowerState
- Nextcalibur 0.5.2
- Nextcalibur Control Center
- Strings
- Grid
- Hardware Protocol
- Nextcalibur 0.5.3
- CardSwitchTasks
- DriveRow
- .WireSettingsPage
- .PowerModeControls
- Border
- ThemeService
- NvidiaDriverState
- SupportVerdict
- .CalculateThreadShare
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- InstallFolderGuard
- LedEffect
- .For
- Nextcalibur 0.5.1
- Nextcalibur 0.5.5
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Releasing, and signing
- .Pick
- TourPage
- Log-Graphics.ps1
- Privacy
- Security
- .OnBrightnessChanged
- StackPanel
- LedZone
- MachineWideGate
- PowerSource
- Notice
- TourStep
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Strings.cs
- Nextcalibur 0.5.6
- .ArrangeOverride
- .TryParseRgb
- MailboxAvailability
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
1. `MainWindow` - 209 edges
2. `Window` - 164 edges
3. `AppSettings` - 59 edges
4. `Nextcalibur.Core.Hardware` - 40 edges
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

## Communities (146 total, 29 thin omitted)

### Community 0 - "DotNetRuntimeDependency"
Cohesion: 0.06
Nodes (43): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+35 more)

### Community 1 - "MainWindow"
Cohesion: 0.05
Nodes (37): DeferredOverheat, KeyEventArgs, ObservableCollection, Queue, SizeChangedEventArgs, Slider, LedPower, Button (+29 more)

### Community 2 - "Dependency"
Cohesion: 0.05
Nodes (44): HttpStatusCode, CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner (+36 more)

### Community 3 - "UserPresence"
Cohesion: 0.06
Nodes (33): MEMORYSTATUSEX, MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, Dictionary, DllImport (+25 more)

### Community 4 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 5 - ".SignatureIsValid"
Cohesion: 0.07
Nodes (29): Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on, The language row (+21 more)

### Community 6 - ".InstallAsync"
Cohesion: 0.07
Nodes (25): FileStream, IOException, Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress (+17 more)

### Community 7 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 8 - "IShellLinkW"
Cohesion: 0.07
Nodes (13): PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder, AppIdentity (+5 more)

### Community 9 - "CpuPowerReader"
Cohesion: 0.09
Nodes (21): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+13 more)

### Community 10 - "RadioButton"
Cohesion: 0.07
Nodes (34): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, NavDisplay, NavLighting (+26 more)

### Community 11 - ".Get"
Cohesion: 0.10
Nodes (13): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, MessageBoxButton, MessageBoxResult, EventArgs (+5 more)

### Community 12 - "WindowsFaults"
Cohesion: 0.11
Nodes (21): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, DateTime, DllImport, Func (+13 more)

### Community 13 - "SystemInfo"
Cohesion: 0.10
Nodes (16): MemoryStatusEx, DriveUse, DllImport, IReadOnlyList, MarshalAs, DriveUse, Name, MemoryStatusEx (+8 more)

### Community 14 - ".Survey"
Cohesion: 0.11
Nodes (10): Func, IEnumerable, IReadOnlyList, RegistryKey, Version, Footprint, SettingsPath, RetirementEntry (+2 more)

### Community 15 - ".OnLoaded"
Cohesion: 0.09
Nodes (6): AssemblyInformationalVersionAttribute, SolidColorBrush, DateTime, Func, Task, DeferredOverheat

### Community 16 - "Button"
Cohesion: 0.07
Nodes (24): IsChecked, BannerDismissButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, HideToTrayButton, MinimiseButton (+16 more)

### Community 17 - "WindowsFaultsTests"
Cohesion: 0.19
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 18 - "Fact"
Cohesion: 0.14
Nodes (9): IList, GpuLoad, GpuConfiguration, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests (+1 more)

### Community 19 - "AppSettings"
Cohesion: 0.07
Nodes (27): JsonElement, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoInstallUpdates, CompensateWindowsFaults, CpuWarningTemperatureC, DisableNdu (+19 more)

### Community 20 - "system_runtime_interopservices"
Cohesion: 0.09
Nodes (9): Nextcalibur.Cli, microsoft_win32_safehandles, system_diagnostics, system_management, system_reflection, system_runtime_interopservices, system_security_accesscontrol, system_security_principal (+1 more)

### Community 21 - ".RequestRestart"
Cohesion: 0.12
Nodes (10): Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr, MarshalAs (+2 more)

### Community 22 - "ProcessPresence"
Cohesion: 0.14
Nodes (11): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+3 more)

### Community 23 - "EcMailbox"
Cohesion: 0.16
Nodes (10): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+2 more)

### Community 24 - "SystemModeService"
Cohesion: 0.17
Nodes (9): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, SystemModeService (+1 more)

### Community 25 - "Nextcalibur.Core.Hardware"
Cohesion: 0.18
Nodes (6): Nextcalibur.Core.Tests, Nextcalibur.Core.Power, Nextcalibur.Core.Hardware, system_text_regularexpressions, system_xml_linq, xunit

### Community 26 - "Fact"
Cohesion: 0.11
Nodes (14): OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, RepairOutcome, Empty, Fact, Task (+6 more)

### Community 27 - ".Info"
Cohesion: 0.16
Nodes (3): Log, Folder, NduFix

### Community 28 - ".OnGpuModeChanged"
Cohesion: 0.12
Nodes (14): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, BannerRestartButton, ModeDiscrete, ModeHybrid (+6 more)

### Community 29 - "Program"
Cohesion: 0.16
Nodes (5): ConsoleColor, Program, DateTimeOffset, ThermalReader, ThermalSample

### Community 30 - ".OfferDependency"
Cohesion: 0.14
Nodes (8): Asynchronous dispatcher & UI thread hardening, Action, Toasts, Exception, DependencyStatus, Missing, NeedsAction, Outdated

### Community 31 - ".Main"
Cohesion: 0.16
Nodes (6): The threat model, and what the code does about it, Application, DllImport, Mutex, App, STAThread

### Community 32 - "UpdateService"
Cohesion: 0.12
Nodes (14): CancelEventArgs, DispatcherTimer, Func, HashSet, IProgress, Task, TimeSpan, UpdateService (+6 more)

### Community 33 - ".OnSystemModeChanged"
Cohesion: 0.15
Nodes (5): PowerModeChangedEventArgs, ModeGaming, ModeOffice, ModePerformance, UninstallCommand

### Community 34 - "Nextcalibur.App"
Cohesion: 0.11
Nodes (13): Nextcalibur.App, system_drawing, system_io, system_io_directory, system_io_ioexception, system_io_path, system_linq, system_runtime_interopservices_comtypes (+5 more)

### Community 35 - "Nextcalibur.Core.Dependencies"
Cohesion: 0.19
Nodes (8): Nextcalibur.Core.Dependencies, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_collections_concurrent, system_net, system_net_http, system_security_cryptography_x509certificates, system_text_json

### Community 36 - "Nextcalibur.Core.csproj"
Cohesion: 0.12
Nodes (13): net8.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Management (8.0.0), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk, net8.0-windows (+5 more)

### Community 37 - ".Get"
Cohesion: 0.20
Nodes (7): ArgumentNullException, GpuModeService, SwitchOutcome, Func, Words, Resolver, SwitchOutcome

### Community 38 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 39 - ".Migrate"
Cohesion: 0.15
Nodes (9): DateTime, Dictionary, List, PendingRestartInfo, BootTimeUtc, ReasonArguments, Reasons, InlineData (+1 more)

### Community 40 - "Fact"
Cohesion: 0.16
Nodes (5): Fact, ForwardCompatibilityTests, MailboxAccessTests, SettingsTests, StartupPreferenceTests

### Community 41 - "PowerOverlayService"
Cohesion: 0.23
Nodes (7): DllImport, Guid, IReadOnlyList, PowerModeOption, PowerOverlays, All, PowerOverlayService

### Community 42 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 43 - "MainWindow.xaml.cs"
Cohesion: 0.24
Nodes (10): Nextcalibur.App.Controls, system_windows, system_windows_controls, system_windows_controls_button, system_windows_controls_primitives, system_windows_input, system_windows_input_keyeventargs, system_windows_media (+2 more)

### Community 44 - "BacklightKeyWatcher"
Cohesion: 0.14
Nodes (11): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, LedBrightness, Full, Half, Off, Fact (+3 more)

### Community 45 - "ToggleButton"
Cohesion: 0.12
Nodes (16): OverheatWarningToggle, SelectAll, SettingAutoCheckUpdates, SettingAutoInstallUpdates, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost, SettingFixWidgets (+8 more)

### Community 46 - "Elevation"
Cohesion: 0.23
Nodes (3): StartupRegistration, IsEnabled, Elevation

### Community 47 - ".GetColour"
Cohesion: 0.24
Nodes (7): B, G, R, Fact, InlineData, Theory, LedStateTests

### Community 48 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 49 - "Nextcalibur.Core.Configuration"
Cohesion: 0.16
Nodes (7): Nextcalibur.Core.Configuration, microsoft_win32, Theme, Dark, Light, system_security, system_text_json_serialization

### Community 50 - "Unelevated"
Cohesion: 0.33
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 51 - "CoreLoad"
Cohesion: 0.13
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 52 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 53 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 54 - "ProcessMetrics"
Cohesion: 0.15
Nodes (14): Share, Dictionary, TimeSpan, FaultEvidence, ProcessMetrics, Name, PageFaultCount, Pid (+6 more)

### Community 55 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 56 - "LedState"
Cohesion: 0.15
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedState, ActiveProfile, BrightnessPercent (+5 more)

### Community 57 - "SystemMode"
Cohesion: 0.20
Nodes (9): ThermalProfile, SystemMode, Gaming, Office, Performance, Fact, InlineData, Theory (+1 more)

### Community 58 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 59 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 60 - "LogInjectionTests"
Cohesion: 0.25
Nodes (6): Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 61 - ".FromBytes"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 62 - ".Check"
Cohesion: 0.31
Nodes (3): RegistryKey, MailboxAccess, IdempotenceTests

### Community 63 - "ElevationPolicyTests"
Cohesion: 0.26
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 64 - "BatteryModePolicy"
Cohesion: 0.42
Nodes (4): BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

### Community 65 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 66 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 67 - "DevicePowerState"
Cohesion: 0.32
Nodes (5): DevPropKey, DllImport, Guid, DevicePowerState, DevPropKey

### Community 68 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 69 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 70 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 71 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 72 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 73 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 75 - "DriveRow"
Cohesion: 0.18
Nodes (8): INotifyPropertyChanged, DriveRow, Detail, Name, Percent, PercentText, system_componentmodel, system_runtime_compilerservices

### Community 77 - ".PowerModeControls"
Cohesion: 0.22
Nodes (4): Option, Button, IEnumerable, TextBlock

### Community 78 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 79 - "ThemeService"
Cohesion: 0.20
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 80 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 81 - "SupportVerdict"
Cohesion: 0.22
Nodes (9): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+1 more)

### Community 82 - ".CalculateThreadShare"
Cohesion: 0.27
Nodes (4): IReadOnlyDictionary, Stable, Dictionary, TopSharePercent

### Community 83 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 84 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 85 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 86 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 87 - "README.md"
Cohesion: 0.33
Nodes (3): How it works, Questions people ask, Using it

### Community 88 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 89 - "InstallFolderGuard"
Cohesion: 0.39
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 90 - "LedEffect"
Cohesion: 0.22
Nodes (8): LedEffect, Blink, Breathing, ColourCycle, Heartbeat, Off, Static, Wave

### Community 91 - ".For"
Cohesion: 0.22
Nodes (8): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal

### Community 92 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 93 - "Nextcalibur 0.5.5"
Cohesion: 0.25
Nodes (6): Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, Zero-bottleneck gaming & heavy workload architecture

### Community 94 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 95 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 96 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 97 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 98 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 99 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 100 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 101 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 102 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 103 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 104 - "StackPanel"
Cohesion: 0.33
Nodes (6): BannerActions, DialogActions, DriveTextStack, PanelWindowsFaultsSubOptions, ProfileRow, StackPanel

### Community 105 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 106 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 107 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 108 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 109 - "TourStep"
Cohesion: 0.40
Nodes (5): TourStep, Body, SectionName, Title, TourPage

### Community 110 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 112 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 113 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 114 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 116 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 118 - ".TryParseRgb"
Cohesion: 0.50
Nodes (3): B, G, R

### Community 119 - "MailboxAvailability"
Cohesion: 0.50
Nodes (4): MailboxAvailability, AccessNotGranted, Available, NotSupported

### Community 120 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 123 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

## Knowledge Gaps
- **372 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+367 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 697 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **29 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `UserPresence`, `GpuClockReader`, `Window`, `CpuPowerReader`, `RadioButton`, `.Get`, `WindowsFaults`, `SystemInfo`, `.OnLoaded`, `Button`, `Fact`, `AppSettings`, `.RequestRestart`, `ProcessPresence`, `EcMailbox`, `SystemModeService`, `.OnGpuModeChanged`, `Program`, `.OfferDependency`, `UpdateService`, `.OnSystemModeChanged`, `.Get`, `PowerOverlayService`, `MainWindow.xaml.cs`, `BacklightKeyWatcher`, `ToggleButton`, `LedController`, `SystemMode`, `BatteryModePolicy`, `Strings`, `Grid`, `.WireSettingsPage`, `.PowerModeControls`, `ThemeService`, `NvidiaDriverState`, `SupportVerdict`, `LedEffect`, `Nextcalibur 0.5.5`, `TourPage`, `.OnBrightnessChanged`, `LedZone`, `TourStep`?**
  _High betweenness centrality (0.521) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `DriveDot`, `DriveList`, `EffectPanel`, `MainWindow`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `RadioButton`, `Button`, `.OnGpuModeChanged`, `.OnSystemModeChanged`, `ToggleButton`, `Grid`, `Border`, `Text`, `.OnBrightnessChanged`, `StackPanel`, `Keycaps`, `DriveGauge`, `CpuWarnValue`, `ColGauge`, `DialogProgress`?**
  _High betweenness centrality (0.173) - this node is a cross-community bridge._
- **Why does `DependencyStatus` connect `.OfferDependency` to `MainWindow`, `Dependency`, `Nextcalibur.Core.Dependencies`, `.InstallAsync`?**
  _High betweenness centrality (0.103) - this node is a cross-community bridge._
- **Are the 11 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 11 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _372 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `DotNetRuntimeDependency` be split into smaller, more focused modules?**
  _Cohesion score 0.05789473684210526 - nodes in this community are weakly interconnected._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.045875251509054325 - nodes in this community are weakly interconnected._