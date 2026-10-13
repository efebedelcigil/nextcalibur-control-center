# Graph Report - nextcalibur-control-center  (2026-10-13)

## Corpus Check
- 170 files · ~221,624 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2859 nodes · 6189 edges · 168 communities (138 shown, 30 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 334 edges (avg confidence: 0.84)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `759f4893`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- MainWindow
- Authenticode
- FnShortcut
- .CheckProgram
- .OfferDependency
- GpuClockReader
- RunawayWatchTests
- PowerOverlayService
- .Get
- CpuPowerReader
- WindowsFaults
- IShellLinkW
- .InstallAsync
- SystemMode
- Window
- PlanTuning
- RadioButton
- WindowsFaultsTests
- TrayPresence
- SystemInfo
- xunit
- AppSettings
- UserPresence
- RoutedEventArgs
- Nextcalibur.Core.Hardware
- UpdateService
- NetworkCostTests
- .RequestRestart
- Fact
- ProcessPresence
- .Main
- CoolingPolicyTests
- ProtectedStore
- ProgramRule
- .Survey
- Nextcalibur.Core.Configuration
- Elevation
- LedState
- Dependency
- MachineScanRecord
- Button
- Program
- EcMailbox
- .Info
- .Get
- MainWindow.FnKeys.cs
- .Ask
- .Read
- Fact
- DotNetRuntimeDependency
- Nextcalibur.Core.csproj
- RestorePoint
- BacklightKeyWatcher
- MemoryTrimmer
- analyse-autopsy.py
- ResourceDictionary
- .MayStartWithoutPrompt
- RuntimeDependencyTests
- ToggleButton
- LogInjectionTests
- .Read
- ColourWheel
- system_text_json
- LedController
- .Check
- FanGauge
- DonutGauge
- .Record
- Nextcalibur.Core.Power
- SegmentedBar
- LedEffect
- .HasAccess
- .FromBytes
- CoreLoad
- Grid
- StringsDictionaryTests
- ValueConverters.cs
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- Nextcalibur Control Center
- Strings
- .ApplyRuleMode
- PendingRestartInfo
- .Check
- Hardware Protocol
- Nextcalibur 0.5.3
- .Sanitised
- Border
- ThemeService
- NvidiaDriverState
- WordsInCodeTests
- IntPtr
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- StackPanel
- SettingsTests.cs
- PawnIoDependency
- Probe
- .GetAsync
- Nextcalibur 0.5.1
- NotificationState
- ProtectedStoreElevatedTests
- Test-Ui.ps1
- Text
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Nextcalibur 0.6.0
- Releasing, and signing
- .Pick
- TourPage
- Log-Graphics.ps1
- Privacy
- Security
- .OnBrightnessChanged
- .ForegroundProcess
- TourStep
- LedZone
- MachineWideGate
- PowerSource
- Notice
- Nextcalibur 0.5.11
- AppIdentity.cs
- .Set
- .A_polling_interval_from_the_file_is_a_sane_one
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- DriveGauge
- Nextcalibur 0.5.10
- Nextcalibur 0.5.12
- Nextcalibur 0.5.6
- Nextcalibur 0.6.1
- .ArrangeOverride
- .TryParseRgb
- MailboxAvailability
- .Modules_are_the_released_ones
- Tools
- 0.5.7.md
- CpuWarnValue
- DialogCheck
- ProgramRuleMode
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
1. `MainWindow` - 308 edges
2. `Window` - 175 edges
3. `AppSettings` - 73 edges
4. `Nextcalibur.Core.Hardware` - 53 edges
5. `WindowsFaultsTests` - 40 edges
6. `RadioButton` - 39 edges
7. `TextBlock` - 36 edges
8. `Nextcalibur.Core.Configuration` - 36 edges
9. `WindowsFaults` - 33 edges
10. `SystemMode` - 33 edges

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

## Communities (168 total, 30 thin omitted)

### Community 0 - "MainWindow"
Cohesion: 0.04
Nodes (51): CheckBox, ComboBox, DeferredOverheat, HwndSource, ObservableCollection, Queue, SizeChangedEventArgs, MainWindow (+43 more)

### Community 1 - "Authenticode"
Cohesion: 0.06
Nodes (38): CatalogInfo, Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on (+30 more)

### Community 2 - "FnShortcut"
Cohesion: 0.06
Nodes (37): Problem, Detail, Dictionary, KeyEventArgs, DllImport, IEnumerable, IntPtr, List (+29 more)

### Community 3 - ".CheckProgram"
Cohesion: 0.06
Nodes (27): Lazy, ProcessModule, FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard, Dictionary, HashSet (+19 more)

### Community 4 - ".OfferDependency"
Cohesion: 0.07
Nodes (19): Asynchronous dispatcher & UI thread hardening, MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, Dictionary, IReadOnlyDictionary (+11 more)

### Community 5 - "GpuClockReader"
Cohesion: 0.08
Nodes (21): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+13 more)

### Community 6 - "RunawayWatchTests"
Cohesion: 0.07
Nodes (35): Cpu, CpuMs, DateTime, Dictionary, Func, IReadOnlyDictionary, IReadOnlyList, ISet (+27 more)

### Community 7 - "PowerOverlayService"
Cohesion: 0.07
Nodes (25): BatteryModePolicy, Remembered, DllImport, Guid, IReadOnlyList, OverlayDiagnosis, GuardMissing, NeedsRepair (+17 more)

### Community 8 - ".Get"
Cohesion: 0.08
Nodes (8): AssemblyInformationalVersionAttribute, SolidColorBrush, Func, MessageBoxButton, MessageBoxResult, Task, Action, Toasts

### Community 9 - "CpuPowerReader"
Cohesion: 0.08
Nodes (19): DevPropKey, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, DllImport, IntPtr (+11 more)

### Community 10 - "WindowsFaults"
Cohesion: 0.08
Nodes (34): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, Share, DateTime, Dictionary (+26 more)

### Community 11 - "IShellLinkW"
Cohesion: 0.07
Nodes (14): IPropertyStore, PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder (+6 more)

### Community 12 - ".InstallAsync"
Cohesion: 0.07
Nodes (25): FileStream, IOException, Message, Ok, RestartRequired, CancellationToken, HttpClient, IProgress (+17 more)

### Community 13 - "SystemMode"
Cohesion: 0.10
Nodes (14): InvalidOperationException, PowerModeChangedEventArgs, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList (+6 more)

### Community 14 - "Window"
Cohesion: 0.08
Nodes (43): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, BannerBody (+35 more)

### Community 15 - "PlanTuning"
Cohesion: 0.12
Nodes (18): AcDc, Floor, Marker, Original, Plan, Setting, Action, DllImport (+10 more)

### Community 16 - "RadioButton"
Cohesion: 0.07
Nodes (37): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeGaming, ModeOffice (+29 more)

### Community 17 - "WindowsFaultsTests"
Cohesion: 0.12
Nodes (7): IReadOnlyDictionary, FaultEvaluation, Stable, Fact, WindowsFaultsTests, Dictionary, TopSharePercent

### Community 18 - "TrayPresence"
Cohesion: 0.10
Nodes (17): ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, ProcessInformation, List, Ms (+9 more)

### Community 19 - "SystemInfo"
Cohesion: 0.08
Nodes (21): MemoryStatusEx, DriveUse, INotifyPropertyChanged, DriveRow, Name, Percent, PercentText, DllImport (+13 more)

### Community 20 - "xunit"
Cohesion: 0.10
Nodes (7): Nextcalibur.Core.Tests, Nextcalibur.Core.Security, Nextcalibur.Core.Tests.Attacks, system_security_cryptography_x509certificates, system_text_regularexpressions, system_xml_linq, xunit

### Community 21 - "AppSettings"
Cohesion: 0.05
Nodes (37): FnShortcut, JsonElement, ProgramRule, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoCooling, AutoFanTurbo (+29 more)

### Community 22 - "UserPresence"
Cohesion: 0.12
Nodes (14): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, Point, Size, DateTime, DllImport (+6 more)

### Community 23 - "RoutedEventArgs"
Cohesion: 0.06
Nodes (14): Option, BannerRestartButton, HideToTrayButton, LedPower, ReloadButton, SelectAll, UpdateNowButton, Action (+6 more)

### Community 24 - "Nextcalibur.Core.Hardware"
Cohesion: 0.11
Nodes (8): Nextcalibur.Cli, Nextcalibur.Core.Hardware, microsoft_win32_safehandles, system_diagnostics, system_management, system_reflection, system_runtime_interopservices, system_text

### Community 25 - "UpdateService"
Cohesion: 0.08
Nodes (16): CancelEventArgs, Slider, EventArgs, DateTime, DispatcherTimer, HashSet, Task, TimeSpan (+8 more)

### Community 26 - "NetworkCostTests"
Cohesion: 0.14
Nodes (19): Bytes, Count, HttpMessageHandler, CancellationToken, Dictionary, Fact, HttpRequestMessage, HttpResponseMessage (+11 more)

### Community 27 - ".RequestRestart"
Cohesion: 0.11
Nodes (11): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, DllImport, EventArgs, IntPtr (+3 more)

### Community 28 - "Fact"
Cohesion: 0.14
Nodes (9): IList, GpuLoad, GpuConfiguration, Fact, GpuModeTests, Discrete, Hybrid, GpuSwitchProtocolTests (+1 more)

### Community 29 - "ProcessPresence"
Cohesion: 0.12
Nodes (12): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+4 more)

### Community 30 - ".Main"
Cohesion: 0.13
Nodes (8): The threat model, and what the code does about it, Application, DllImport, Mutex, App, RegistryKey, Footprint, STAThread

### Community 31 - "CoolingPolicyTests"
Cohesion: 0.12
Nodes (16): At, Margin, DateTime, List, TimeSpan, CoolingPolicy, DegreesPerMinute, Engaged (+8 more)

### Community 32 - "ProtectedStore"
Cohesion: 0.13
Nodes (13): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+5 more)

### Community 33 - "ProgramRule"
Cohesion: 0.14
Nodes (16): GeneratedRegex, DateTime, IEnumerable, IReadOnlyDictionary, Regex, ProgramRule, Enabled, Mode (+8 more)

### Community 34 - ".Survey"
Cohesion: 0.15
Nodes (7): Func, IEnumerable, IReadOnlyList, RetirementEntry, Trace, Version, FootprintTests

### Community 35 - "Nextcalibur.Core.Configuration"
Cohesion: 0.11
Nodes (14): Nextcalibur.App, Nextcalibur.Core, Nextcalibur.Core.Configuration, system_componentmodel, system_drawing, system_globalization, system_io, system_linq (+6 more)

### Community 36 - "Elevation"
Cohesion: 0.14
Nodes (6): dynamic, Encoding, Elevation, CardSwitchTasks, Fact, TaskFolderTests

### Community 37 - "LedState"
Cohesion: 0.15
Nodes (15): B, G, R, LedState, ActiveProfile, BrightnessPercent, Current, Effect (+7 more)

### Community 38 - "Dependency"
Cohesion: 0.11
Nodes (17): CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner, Id (+9 more)

### Community 39 - "MachineScanRecord"
Cohesion: 0.11
Nodes (18): DateTime, JsonSerializerOptions, List, MachineScan, RecordPath, MachineScanRecord, BiosVendor, BiosVersion (+10 more)

### Community 40 - "Button"
Cohesion: 0.08
Nodes (21): IsChecked, BannerDismissButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, FixButton, MinimiseButton, OpenLogButton (+13 more)

### Community 41 - "Program"
Cohesion: 0.15
Nodes (5): ConsoleColor, Program, DateTimeOffset, ThermalReader, ThermalSample

### Community 42 - "EcMailbox"
Cohesion: 0.16
Nodes (10): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+2 more)

### Community 43 - ".Info"
Cohesion: 0.16
Nodes (3): Log, Folder, NduFix

### Community 44 - ".Get"
Cohesion: 0.16
Nodes (12): ArgumentNullException, FirmwareModeReading, GpuMode, Discrete, Hybrid, Uma, GpuModeService, SwitchOutcome (+4 more)

### Community 45 - "MainWindow.FnKeys.cs"
Cohesion: 0.19
Nodes (14): Nextcalibur.App.Controls, Nextcalibur.Core.Input, system_windows, system_windows_controls, system_windows_controls_button, system_windows_controls_checkbox, system_windows_controls_combobox, system_windows_controls_primitives (+6 more)

### Community 46 - ".Ask"
Cohesion: 0.22
Nodes (11): HttpStatusCode, CancellationToken, Fact, HttpClient, HttpRequestMessage, HttpResponseMessage, InlineData, Task (+3 more)

### Community 47 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 48 - "Fact"
Cohesion: 0.15
Nodes (5): Fact, ForwardCompatibilityTests, MailboxAccessTests, SettingsTests, StartupPreferenceTests

### Community 49 - "DotNetRuntimeDependency"
Cohesion: 0.16
Nodes (15): CancellationToken, HttpClient, JsonElement, Task, Uri, Version, DotNetRuntimeDependency, ExpectedSigner (+7 more)

### Community 50 - "Nextcalibur.Core.csproj"
Cohesion: 0.11
Nodes (14): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Diagnostics.EventLog (10.0.12), System.Management (10.0.12), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.1), Microsoft.NET.Sdk (+6 more)

### Community 51 - "RestorePoint"
Cohesion: 0.17
Nodes (10): CreatedAt, RestorePointInfo, Sequence, DateTime, DllImport, RestorePoint, RestorePointInfo, RestorePointOutcome (+2 more)

### Community 52 - "BacklightKeyWatcher"
Cohesion: 0.12
Nodes (12): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, FanTurbo, LedBrightness, Full, Half, Off (+4 more)

### Community 53 - "MemoryTrimmer"
Cohesion: 0.15
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 54 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 55 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 56 - ".MayStartWithoutPrompt"
Cohesion: 0.22
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 57 - "RuntimeDependencyTests"
Cohesion: 0.24
Nodes (5): Fact, InlineData, JsonElement, Theory, RuntimeDependencyTests

### Community 58 - "ToggleButton"
Cohesion: 0.12
Nodes (17): OverheatWarningToggle, SettingAutoCooling, SettingAutoFanTurbo, SettingDisableNdu, SettingFixCrossDevice, SettingFixTextInputHost, SettingFixWidgets, SettingFnShortcuts (+9 more)

### Community 59 - "LogInjectionTests"
Cohesion: 0.19
Nodes (7): Exception, Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 60 - ".Read"
Cohesion: 0.30
Nodes (3): Fact, List, ProtectedStoreTests

### Community 61 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 62 - "system_text_json"
Cohesion: 0.23
Nodes (6): Nextcalibur.Core.Dependencies, system_collections_concurrent, system_net, system_net_http, system_text_json, system_text_json_serialization

### Community 63 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 64 - ".Check"
Cohesion: 0.26
Nodes (4): CommonAce, RegistryKey, MailboxAccess, IdempotenceTests

### Community 65 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 66 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 67 - ".Record"
Cohesion: 0.18
Nodes (7): IReadOnlyDictionary, IReadOnlyList, JsonSerializerOptions, AuditLog, AppVersion, Fact, AuditLogTests

### Community 68 - "Nextcalibur.Core.Power"
Cohesion: 0.18
Nodes (6): Nextcalibur.Core.Power, microsoft_win32, Theme, Dark, Light, system_security

### Community 69 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 70 - "LedEffect"
Cohesion: 0.14
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 71 - ".HasAccess"
Cohesion: 0.25
Nodes (6): RawSecurityDescriptor, SecurityIdentifier, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests, Me

### Community 72 - ".FromBytes"
Cohesion: 0.27
Nodes (5): ArgumentException, Fact, InlineData, Theory, SmiCommandTests

### Community 73 - "CoreLoad"
Cohesion: 0.17
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 74 - "Grid"
Cohesion: 0.15
Nodes (12): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, ProgramRuleAddRow (+4 more)

### Community 75 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 76 - "ValueConverters.cs"
Cohesion: 0.29
Nodes (7): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, system_windows_data, Type

### Community 77 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 78 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 79 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 80 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 82 - "PendingRestartInfo"
Cohesion: 0.18
Nodes (7): DateTime, Dictionary, List, PendingRestartInfo, BootTimeUtc, ReasonArguments, Reasons

### Community 83 - ".Check"
Cohesion: 0.20
Nodes (10): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+2 more)

### Community 84 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 85 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 86 - ".Sanitised"
Cohesion: 0.29
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 87 - "Border"
Cohesion: 0.20
Nodes (10): Banner, Bd, DriveDivider, PreviewA, PreviewB, PreviewC, SettingStartHow, ThemeSwitch (+2 more)

### Community 88 - "ThemeService"
Cohesion: 0.20
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 89 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 90 - "WordsInCodeTests"
Cohesion: 0.31
Nodes (5): Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests

### Community 91 - "IntPtr"
Cohesion: 0.42
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 92 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 93 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 94 - "README.md"
Cohesion: 0.36
Nodes (3): How it works, Questions people ask, Using it

### Community 95 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 96 - "StackPanel"
Cohesion: 0.22
Nodes (9): BannerActions, DialogActions, DriveTextStack, FnShortcutRows, PanelWindowsFaultsSubOptions, ProfileRow, ProgramRuleRows, SettingUpdatesAutomatic (+1 more)

### Community 97 - "SettingsTests.cs"
Cohesion: 0.39
Nodes (3): system_security_accesscontrol, system_security_cryptography, system_security_principal

### Community 98 - "PawnIoDependency"
Cohesion: 0.22
Nodes (8): Version, PawnIoDependency, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 99 - "Probe"
Cohesion: 0.22
Nodes (8): Version, Probe, ExpectedSigner, Id, Name, Purpose, SilentInstallArguments, UninstallKey

### Community 100 - ".GetAsync"
Cohesion: 0.29
Nodes (7): Answer, ConcurrentDictionary, CancellationToken, HttpClient, Task, Answer, Conditional

### Community 101 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 102 - "NotificationState"
Cohesion: 0.25
Nodes (8): NotificationState, AcceptsNotifications, Busy, NotPresent, PresentationMode, QuietTime, RunningDirect3dFullScreen, RunningWindowsStoreApp

### Community 103 - "ProtectedStoreElevatedTests"
Cohesion: 0.32
Nodes (4): Fact, SecurityIdentifier, ProtectedStoreElevatedTests, Elevated

### Community 104 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 105 - "Text"
Cohesion: 0.38
Nodes (7): RpmToDoubleConverter, Text, CpuFanGauge, CpuName, GpuFanGauge, GpuName, FanGauge

### Community 106 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 107 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 108 - "Nextcalibur 0.6.0"
Cohesion: 0.29
Nodes (6): Heat, Keyboard, Nextcalibur 0.6.0, Safety and records, Smaller, Tested on

### Community 109 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 110 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 111 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 112 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 113 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 114 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 115 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 116 - ".ForegroundProcess"
Cohesion: 0.47
Nodes (3): DllImport, IntPtr, Name

### Community 117 - "TourStep"
Cohesion: 0.33
Nodes (5): TourStep, Body, SectionName, Title, TourPage

### Community 118 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 119 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 120 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 121 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 122 - "Nextcalibur 0.5.11"
Cohesion: 0.40
Nodes (4): Added, Fixed, Nextcalibur 0.5.11, Tested on

### Community 123 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 126 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 128 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 129 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 130 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 131 - "Nextcalibur 0.5.10"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.5.10, Tested on

### Community 132 - "Nextcalibur 0.5.12"
Cohesion: 0.50
Nodes (3): Changed, Nextcalibur 0.5.12, Tested on

### Community 133 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 134 - "Nextcalibur 0.6.1"
Cohesion: 0.50
Nodes (3): Fixed, Nextcalibur 0.6.1, Tested on

### Community 136 - ".TryParseRgb"
Cohesion: 0.50
Nodes (3): B, G, R

### Community 137 - "MailboxAvailability"
Cohesion: 0.50
Nodes (4): MailboxAvailability, AccessNotGranted, Available, NotSupported

### Community 138 - ".Modules_are_the_released_ones"
Cohesion: 0.50
Nodes (3): InlineData, Theory, PawnIoModuleTests

### Community 139 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

### Community 142 - "CpuWarnValue"
Cohesion: 0.67
Nodes (3): CpuWarnValue, GpuWarnValue, TextBox

### Community 143 - "DialogCheck"
Cohesion: 0.67
Nodes (3): DialogCheck, FnSelectAll, CheckBox

### Community 144 - "ProgramRuleMode"
Cohesion: 0.67
Nodes (3): ProgramRuleMode, ProgramRuleName, ComboBox

## Knowledge Gaps
- **466 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+461 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 890 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **30 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `FnShortcut`, `.CheckProgram`, `.OfferDependency`, `GpuClockReader`, `RunawayWatchTests`, `PowerOverlayService`, `.Get`, `CpuPowerReader`, `WindowsFaults`, `SystemMode`, `Window`, `PlanTuning`, `RadioButton`, `TrayPresence`, `SystemInfo`, `AppSettings`, `UserPresence`, `RoutedEventArgs`, `UpdateService`, `.RequestRestart`, `Fact`, `ProcessPresence`, `CoolingPolicyTests`, `ProgramRule`, `Nextcalibur.Core.Configuration`, `Button`, `Program`, `EcMailbox`, `.Info`, `.Get`, `MainWindow.FnKeys.cs`, `.Read`, `BacklightKeyWatcher`, `LogInjectionTests`, `LedController`, `.Record`, `LedEffect`, `Grid`, `Strings`, `.ApplyRuleMode`, `.Check`, `ThemeService`, `NvidiaDriverState`, `TourPage`, `.OnBrightnessChanged`, `.ForegroundProcess`, `TourStep`, `LedZone`?**
  _High betweenness centrality (0.478) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `Keycaps`, `DriveGauge`, `.OfferDependency`, `CpuWarnValue`, `DialogCheck`, `RadioButton`, `ProgramRuleMode`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `RoutedEventArgs`, `EffectPanel`, `PART_ContentHost`, `TourCanvas`, `Wheel`, `Button`, `ToggleButton`, `Grid`, `Border`, `StackPanel`, `Text`, `.OnBrightnessChanged`?**
  _High betweenness centrality (0.118) - this node is a cross-community bridge._
- **Why does `Nextcalibur.Core.Hardware` connect `Nextcalibur.Core.Hardware` to `SettingsTests.cs`, `Nextcalibur.Core.Configuration`, `Nextcalibur.Core.Power`, `Program`, `EcMailbox`, `MainWindow.FnKeys.cs`, `.Check`, `BacklightKeyWatcher`, `xunit`, `MachineWideGate`, `system_text_json`?**
  _High betweenness centrality (0.084) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _466 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `MainWindow` be split into smaller, more focused modules?**
  _Cohesion score 0.03614457831325301 - nodes in this community are weakly interconnected._
- **Should `Authenticode` be split into smaller, more focused modules?**
  _Cohesion score 0.062111801242236024 - nodes in this community are weakly interconnected._