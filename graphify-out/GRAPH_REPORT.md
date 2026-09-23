# Graph Report - nextcalibur-control-center  (2026-09-23)

## Corpus Check
- 143 files · ~197,482 words
- Verdict: corpus is large enough that graph structure adds value.
- Unclassified: 9 file(s) not represented in the graph (top: (none) 3, .wsb 2, .iss 1)

## Summary
- 2416 nodes · 5109 edges · 141 communities (111 shown, 30 thin omitted)
- Extraction: 95% EXTRACTED · 5% INFERRED · 0% AMBIGUOUS · INFERRED: 252 edges (avg confidence: 0.83)
- Token cost: 0 input · 0 output

## Graph Freshness
- Built from commit: `000dada1`
- Run `git rev-parse HEAD` and compare to check if the graph is stale.
- Run `graphify update .` after code changes (no API cost).

## Community Hubs (Navigation)
- Window
- DotNetRuntimeDependency
- Authenticode
- MainWindow
- Dependency
- ProtectedStore
- .CheckProgram
- UpdateService
- GpuClockReader
- CpuPowerReader
- IShellLinkW
- SystemInfo
- UserPresence
- PowerOverlayService
- .Get
- RadioButton
- Fact
- SystemMode
- Nextcalibur.App
- .Main
- WindowsFaults
- Button
- .RequestRestart
- WindowsFaultsTests
- Fact
- Nextcalibur.Core.Configuration
- AppSettings
- ProcessPresence
- .Check
- Nextcalibur.Core.Hardware
- Elevation
- .Warn
- LedState
- Program
- MemoryTrimmer
- .Info
- .PowerModeControls
- .Read
- .Migrate
- .OnLoaded
- Nextcalibur.Core.csproj
- .CalculateThreadShare
- analyse-autopsy.py
- EcMailbox
- .OnGpuModeChanged
- SmiCommand
- BacklightKeyWatcher
- ResourceDictionary
- system_text_json
- .OfferDependency
- ColourWheel
- Unelevated
- LedController
- FanGauge
- HostileFilesTests
- ProcessMetrics
- DonutGauge
- .IsUnderProgramFiles
- SegmentedBar
- .StartSlowTimer
- LedEffect
- GpuModeService
- Nextcalibur.Core.Power
- InstallFolderGuard
- BatteryModePolicy
- StringsDictionaryTests
- GPU mode ("Display Mode")
- Nextcalibur 0.5.2
- CoreLoad
- Nextcalibur Control Center
- Strings
- Grid
- SettingsTests.cs
- Hardware Protocol
- Nextcalibur 0.5.3
- .Sanitised
- IntPtr
- RpmToDoubleConverter
- ThemeService
- NvidiaDriverState
- SupportVerdict
- .ToHsv
- Nextcalibur on a machine that has never had the vendor software
- README.md
- Nextcalibur 0.5.0
- ProfileFiles
- Nextcalibur 0.5.1
- NduFix
- Test-Ui.ps1
- 4. LED — `a1 = 0x0100`
- Nextcalibur 0.5.9
- Releasing, and signing
- .Pick
- TourPage
- Log-Graphics.ps1
- ValueConverters.cs
- Privacy
- Security
- .OnBrightnessChanged
- .OnThresholdMoved
- TourStep
- .Sample
- LedZone
- MachineWideGate
- PowerSource
- Notice
- AppIdentity.cs
- .Set
- Grant-MailboxAccess.ps1
- Trace-ModeSwitch.ps1
- Keycaps
- CpuFanGauge
- DriveGauge
- Nextcalibur 0.5.6
- .ArrangeOverride
- .TryParseRgb
- Tools
- 0.5.7.md
- ColGauge
- DialogProgress
- DriveDot
- DriveList
- EffectPanel
- Wheel
- Icons.xaml
- Palette.xaml
- Palette.Dark.xaml
- Palette.Light.xaml
- Strings.en.xaml
- Strings.tr.xaml
- Typography.xaml

## God Nodes (most connected - your core abstractions)
1. `MainWindow` - 222 edges
2. `Window` - 164 edges
3. `AppSettings` - 61 edges
4. `Nextcalibur.Core.Hardware` - 42 edges
5. `WindowsFaultsTests` - 40 edges
6. `RadioButton` - 39 edges
7. `TextBlock` - 36 edges
8. `TrayPresence` - 33 edges
9. `WindowsFaults` - 33 edges
10. `GpuClockReader` - 31 edges

## Surprising Connections (you probably didn't know these)
- `Deep memory & resource leak eradication` --references--> `MainWindow`  [INFERRED]
  docs/releases/0.5.5.md → src/Nextcalibur.App/MainWindow.Integrity.cs
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

## Communities (141 total, 30 thin omitted)

### Community 0 - "Window"
Cohesion: 0.04
Nodes (84): TemperatureToDoubleConverter, Detail, Foreground, IsMouseOver, ItemsSource.Count, Name, PercentText, Text (+76 more)

### Community 1 - "DotNetRuntimeDependency"
Cohesion: 0.05
Nodes (46): Answer, Bytes, ConcurrentDictionary, Count, HttpMessageHandler, CancellationToken, HttpClient, Task (+38 more)

### Community 2 - "Authenticode"
Cohesion: 0.06
Nodes (38): CatalogInfo, Nextcalibur 0.5.4, Privacy, Quieter in the background, Security, Security, the second pass, Smaller, Tested on (+30 more)

### Community 3 - "MainWindow"
Cohesion: 0.05
Nodes (38): DeferredOverheat, KeyEventArgs, ObservableCollection, Queue, SizeChangedEventArgs, SolidColorBrush, DateTime, HashSet (+30 more)

### Community 4 - "Dependency"
Cohesion: 0.05
Nodes (44): HttpStatusCode, CancellationToken, HttpClient, Task, Uri, Version, Dependency, ExpectedSigner (+36 more)

### Community 5 - "ProtectedStore"
Cohesion: 0.09
Nodes (20): Content, DirectorySecurity, Hash, Dictionary, FileSystemAccessRule, FileSystemRights, IReadOnlyList, List (+12 more)

### Community 6 - ".CheckProgram"
Cohesion: 0.07
Nodes (23): Lazy, ProcessModule, Dictionary, HashSet, IReadOnlyList, Integrity, NvmlPath, IntegrityFinding (+15 more)

### Community 7 - "UpdateService"
Cohesion: 0.05
Nodes (37): FileStream, Message, Ok, RestartRequired, DispatcherTimer, Func, HashSet, IProgress (+29 more)

### Community 8 - "GpuClockReader"
Cohesion: 0.09
Nodes (18): NvmlProcessInfo, NvmlUtilisation, PdhFmtCounterValue, DateTime, DllImport, Func, IntPtr, IReadOnlyList (+10 more)

### Community 9 - "CpuPowerReader"
Cohesion: 0.08
Nodes (19): DevPropKey, Deep memory & resource leak eradication, Discrete GPU sleep protection in Hybrid mode, Nextcalibur 0.5.5, Startup preferences & installer accuracy, Tested on, DllImport, IntPtr (+11 more)

### Community 10 - "IShellLinkW"
Cohesion: 0.07
Nodes (13): PropertyKey, PropVariant, DllImport, Guid, IEnumerable, IntPtr, StringBuilder, AppIdentity (+5 more)

### Community 11 - "SystemInfo"
Cohesion: 0.07
Nodes (22): MemoryStatusEx, DriveUse, INotifyPropertyChanged, DriveRow, Detail, Name, Percent, PercentText (+14 more)

### Community 12 - "UserPresence"
Cohesion: 0.10
Nodes (20): MonitorInfo, NotificationState, Rect, SessionSwitchEventArgs, DateTime, DllImport, IntPtr, MarshalAs (+12 more)

### Community 13 - "PowerOverlayService"
Cohesion: 0.08
Nodes (22): DllImport, Guid, IReadOnlyList, OverlayDiagnosis, GuardMissing, NeedsRepair, OverlayIsStuck, PowerModeOption (+14 more)

### Community 14 - ".Get"
Cohesion: 0.09
Nodes (14): CancelEventArgs, ContextMenuStrip, EventHandler, Icon, Item, NotifyIcon, MessageBoxButton, MessageBoxResult (+6 more)

### Community 15 - "RadioButton"
Cohesion: 0.08
Nodes (33): FxBlink, FxBreathing, FxCycle, FxHeartbeat, FxStatic, FxWave, ModeGaming, ModeOffice (+25 more)

### Community 16 - "Fact"
Cohesion: 0.11
Nodes (10): Func, IEnumerable, IReadOnlyList, Version, RetirementEntry, Trace, Fact, FootprintTests (+2 more)

### Community 17 - "SystemMode"
Cohesion: 0.12
Nodes (16): InvalidOperationException, Dictionary, DllImport, Guid, IntPtr, IReadOnlyDictionary, IReadOnlyList, SystemMode (+8 more)

### Community 18 - "Nextcalibur.App"
Cohesion: 0.10
Nodes (21): Nextcalibur.App, Nextcalibur.App.Controls, system_componentmodel, system_drawing, system_io, system_linq, system_runtime_compilerservices, system_windows (+13 more)

### Community 19 - ".Main"
Cohesion: 0.13
Nodes (8): The threat model, and what the code does about it, Application, DllImport, Mutex, App, RegistryKey, Footprint, STAThread

### Community 20 - "WindowsFaults"
Cohesion: 0.11
Nodes (21): EnumWindowsProc, IO_COUNTERS, Process, PROCESS_MEMORY_COUNTERS, ProcessMetrics, DateTime, DllImport, Func (+13 more)

### Community 21 - "Button"
Cohesion: 0.07
Nodes (24): IsChecked, BannerDismissButton, CloseButton, DialogButtonPrimary, DialogButtonSecondary, HideToTrayButton, MinimiseButton, OpenLogButton (+16 more)

### Community 22 - ".RequestRestart"
Cohesion: 0.10
Nodes (12): Zero-bottleneck gaming & heavy workload architecture, Fix GPU Switch Restart Prompt & Windows Privilege Adjustment, Nextcalibur 0.5.8, Tested on, Luid, BannerRestartButton, DllImport, EventArgs (+4 more)

### Community 23 - "WindowsFaultsTests"
Cohesion: 0.17
Nodes (3): FaultEvaluation, Fact, WindowsFaultsTests

### Community 24 - "Fact"
Cohesion: 0.13
Nodes (11): IList, GpuLoad, Fact, InlineData, Theory, GpuAwakeFaultConditionTests, GpuModeTests, Discrete (+3 more)

### Community 25 - "Nextcalibur.Core.Configuration"
Cohesion: 0.14
Nodes (9): Nextcalibur.Core.Tests, Nextcalibur.Core.Security, Nextcalibur.Core.Configuration, Nextcalibur.Core.Tests.Attacks, system_security_cryptography_x509certificates, system_text_regularexpressions, system_xml_linq, TaskFolderTests (+1 more)

### Community 26 - "AppSettings"
Cohesion: 0.07
Nodes (28): JsonElement, AppSettings, AcceptedVendorSoftware, AutoCheckForUpdates, AutoInstallUpdates, CompensateWindowsFaults, CpuWarningTemperatureC, DisableNdu (+20 more)

### Community 27 - "ProcessPresence"
Cohesion: 0.13
Nodes (11): DllImport, HashSet, IntPtr, StringBuilder, ProcessPresence, DateTime, TimeSpan, VendorSoftware (+3 more)

### Community 28 - ".Check"
Cohesion: 0.13
Nodes (9): CommonAce, RawSecurityDescriptor, RegistryKey, SecurityIdentifier, MailboxAccess, RawSecurityDescriptor, SecurityIdentifier, MailboxAccessCoverageTests (+1 more)

### Community 29 - "Nextcalibur.Core.Hardware"
Cohesion: 0.13
Nodes (7): Nextcalibur.Cli, Nextcalibur.Core.Hardware, microsoft_win32_safehandles, system_diagnostics, system_management, system_reflection, system_runtime_interopservices

### Community 30 - "Elevation"
Cohesion: 0.15
Nodes (5): dynamic, Encoding, Elevation, CardSwitchTasks, Fact

### Community 31 - ".Warn"
Cohesion: 0.13
Nodes (8): MessageBoxButton, MessageBoxResult, Window, Dialogs, Owner, FixButton, Action, ToggleButton

### Community 32 - "LedState"
Cohesion: 0.15
Nodes (15): B, G, R, LedState, ActiveProfile, BrightnessPercent, Current, Effect (+7 more)

### Community 33 - "Program"
Cohesion: 0.17
Nodes (5): ConsoleColor, Program, DateTimeOffset, ThermalReader, ThermalSample

### Community 34 - "MemoryTrimmer"
Cohesion: 0.12
Nodes (13): MEMORYSTATUSEX, DateTime, Dictionary, DllImport, IntPtr, MarshalAs, MEMORYSTATUSEX, MemoryTrimmer (+5 more)

### Community 35 - ".Info"
Cohesion: 0.17
Nodes (8): Log, Folder, Action, Fact, InlineData, Regex, Theory, LogInjectionTests

### Community 36 - ".PowerModeControls"
Cohesion: 0.11
Nodes (7): Option, LedPower, Action, Button, Color, IEnumerable, RadioButton

### Community 37 - ".Read"
Cohesion: 0.13
Nodes (13): SmiFamily, Read, Write, SmiSubsystem, DisplayMode, Led, Profile, Thermal (+5 more)

### Community 38 - ".Migrate"
Cohesion: 0.13
Nodes (8): DateTime, Dictionary, List, PendingRestartInfo, BootTimeUtc, ReasonArguments, Reasons, SettingsTests

### Community 39 - ".OnLoaded"
Cohesion: 0.18
Nodes (6): AssemblyInformationalVersionAttribute, PowerModeChangedEventArgs, PowerModeBalanced, PowerModeBest, PowerModeBetter, PowerModeEfficiency

### Community 40 - "Nextcalibur.Core.csproj"
Cohesion: 0.12
Nodes (13): net10.0-windows10.0.19041.0, Microsoft.NET.Test.Sdk (18.10.1), System.Management (10.0.12), Velopack (1.2.0), xunit (2.9.3), xunit.runner.visualstudio (4.0.0), Microsoft.NET.Sdk, net10.0-windows (+5 more)

### Community 41 - ".CalculateThreadShare"
Cohesion: 0.15
Nodes (9): IReadOnlyDictionary, Stable, Dictionary, Fact, IEnumerable, Regex, XNamespace, WordsInCodeTests (+1 more)

### Community 42 - "analyse-autopsy.py"
Cohesion: 0.14
Nodes (17): collections, io, json, pathlib, pil, sys, describe_file(), load() (+9 more)

### Community 43 - "EcMailbox"
Cohesion: 0.20
Nodes (9): Exception, FirmwareModeReading, IDisposable, ManagementObject, ManagementScope, Func, EcMailbox, EcMailboxUnavailableException (+1 more)

### Community 44 - ".OnGpuModeChanged"
Cohesion: 0.15
Nodes (9): ModeDiscrete, ModeHybrid, ModeUma, FirmwareModeReading, GpuConfiguration, GpuMode, Discrete, Hybrid (+1 more)

### Community 45 - "SmiCommand"
Cohesion: 0.20
Nodes (6): ArgumentException, SmiCommand, Fact, InlineData, Theory, SmiCommandTests

### Community 46 - "BacklightKeyWatcher"
Cohesion: 0.13
Nodes (11): EventArrivedEventArgs, ManagementEventWatcher, BacklightKeyWatcher, LedBrightness, Full, Half, Off, Fact (+3 more)

### Community 47 - "ResourceDictionary"
Cohesion: 0.14
Nodes (17): Bg, CardBorder, Indicator, Knob, PART_Indicator, PART_Track, ResourceDictionary, RootGrid (+9 more)

### Community 48 - "system_text_json"
Cohesion: 0.22
Nodes (7): Nextcalibur.Core.Dependencies, system_collections_concurrent, system_net, system_net_http, system_text, system_text_json, system_text_json_serialization

### Community 49 - ".OfferDependency"
Cohesion: 0.19
Nodes (4): Asynchronous dispatcher & UI thread hardening, Action, Toasts, Exception

### Community 50 - "ColourWheel"
Cohesion: 0.15
Nodes (11): BitmapSource, Control, Ellipse, Canvas, DependencyProperty, Image, ColourWheel, Diameter (+3 more)

### Community 51 - "Unelevated"
Cohesion: 0.33
Nodes (7): ProcessInformation, DllImport, IntPtr, ProcessInformation, StartupInfo, Unelevated, StartupInfo

### Community 52 - "LedController"
Cohesion: 0.26
Nodes (4): LedController, EffectiveBrightnessPercent, HardwareLevel, State

### Community 53 - "FanGauge"
Cohesion: 0.15
Nodes (13): ContentControl, Brush, DependencyProperty, DrawingContext, Pen, FanGauge, ArcBrush, BladeBrush (+5 more)

### Community 54 - "HostileFilesTests"
Cohesion: 0.27
Nodes (5): IOException, Fact, InlineData, Theory, HostileFilesTests

### Community 55 - "ProcessMetrics"
Cohesion: 0.15
Nodes (14): Share, Dictionary, TimeSpan, FaultEvidence, ProcessMetrics, Name, PageFaultCount, Pid (+6 more)

### Community 56 - "DonutGauge"
Cohesion: 0.15
Nodes (12): Brush, DependencyProperty, DrawingContext, Pen, Size, DonutGauge, ArcBrush, BracketBrush (+4 more)

### Community 57 - ".IsUnderProgramFiles"
Cohesion: 0.23
Nodes (6): Fact, InlineData, Theory, ElevationPolicyTests, Profile, ProgramFiles

### Community 58 - "SegmentedBar"
Cohesion: 0.14
Nodes (12): FrameworkElement, Brush, DependencyProperty, DrawingContext, Size, SegmentedBar, LitBrush, Maximum (+4 more)

### Community 59 - ".StartSlowTimer"
Cohesion: 0.18
Nodes (3): Func, Task, UninstallCommand

### Community 60 - "LedEffect"
Cohesion: 0.14
Nodes (13): Dictionary, LedProfile, BrightnessPercent, Colours, Effect, LedEffect, Blink, Breathing (+5 more)

### Community 61 - "GpuModeService"
Cohesion: 0.32
Nodes (4): ArgumentNullException, GpuModeService, SwitchOutcome, SwitchOutcome

### Community 62 - "Nextcalibur.Core.Power"
Cohesion: 0.17
Nodes (6): Nextcalibur.Core.Power, microsoft_win32, Theme, Dark, Light, system_security

### Community 63 - "InstallFolderGuard"
Cohesion: 0.31
Nodes (4): FileSystemAccessRule, FileSystemRights, SecurityIdentifier, InstallFolderGuard

### Community 64 - "BatteryModePolicy"
Cohesion: 0.42
Nodes (4): BatteryModePolicy, Remembered, Fact, BatteryModePolicyTests

### Community 65 - "StringsDictionaryTests"
Cohesion: 0.27
Nodes (6): Dictionary, Fact, InlineData, Theory, XNamespace, StringsDictionaryTests

### Community 66 - "GPU mode ("Display Mode")"
Cohesion: 0.17
Nodes (12): 5. Other interfaces (no mailbox involved), Corrected: the software *can* switch, through its kernel driver, Found: the switch is one mailbox write, GPU mode ("Display Mode"), GPU sensors, Measured: the two buttons work by entirely different means, Out of scope: the refresh rate, Power management (+4 more)

### Community 67 - "Nextcalibur 0.5.2"
Cohesion: 0.17
Nodes (11): A log of its own, A proper installer, A restart you can cancel, CPU power, Every drive, It runs as administrator now, Nextcalibur 0.5.2, Power Mode within the System mode (+3 more)

### Community 68 - "CoreLoad"
Cohesion: 0.18
Nodes (7): ProcessorPerformance, DefaultDllImportSearchPaths, DllImport, IntPtr, CoreLoad, Count, ProcessorPerformance

### Community 69 - "Nextcalibur Control Center"
Cohesion: 0.17
Nodes (12): Building, Documents, Download, Hardware, Legal, Nextcalibur Control Center, Privacy, Rules of the project (+4 more)

### Community 70 - "Strings"
Cohesion: 0.27
Nodes (6): ResourceDictionary, Strings, Current, UiLanguage, English, Turkish

### Community 71 - "Grid"
Cohesion: 0.17
Nodes (11): DriveRowGrid, ModalDialogOverlay, PageDisplay, PageLighting, PagePower, PageSettings, PageSystem, TitleBar (+3 more)

### Community 72 - "SettingsTests.cs"
Cohesion: 0.24
Nodes (7): MailboxAvailability, AccessNotGranted, Available, NotSupported, system_security_accesscontrol, system_security_cryptography, system_security_principal

### Community 73 - "Hardware Protocol"
Cohesion: 0.18
Nodes (11): 1. Transport — ACPI-WMI mailbox, 2. Command structure, 3. Thermal / fan — `a1 = 0x0200`, 6. Safety rules, 7. Not supported, Call sequence, Command families (`a0`), Hardware Protocol (+3 more)

### Community 74 - "Nextcalibur 0.5.3"
Cohesion: 0.18
Nodes (10): A guided tour, A Settings page, Also, Cheaper, Everything opened for you opens as you, Installed under Program Files, Nextcalibur 0.5.3, Tested on (+2 more)

### Community 75 - ".Sanitised"
Cohesion: 0.29
Nodes (4): Fact, InlineData, Theory, HostileSettingsTests

### Community 76 - "IntPtr"
Cohesion: 0.38
Nodes (4): DllImport, IntPtr, Program, SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX

### Community 77 - "RpmToDoubleConverter"
Cohesion: 0.36
Nodes (6): CultureInfo, IValueConverter, Regex, RpmToDoubleConverter, TemperatureToDoubleConverter, Type

### Community 78 - "ThemeService"
Cohesion: 0.20
Nodes (8): Theme, ThemePreference, Dark, Light, System, ThemeService, Preference, Resolved

### Community 79 - "NvidiaDriverState"
Cohesion: 0.29
Nodes (6): DllImport, EssentialDrivers, NvidiaDriverState, Missing, NoCard, Present

### Community 80 - "SupportVerdict"
Cohesion: 0.22
Nodes (9): IReadOnlyList, HardwareSupport, SupportLevel, ReadOnly, Supported, Unsupported, SupportVerdict, AllowsReads (+1 more)

### Community 81 - ".ToHsv"
Cohesion: 0.25
Nodes (6): DependencyObject, DependencyPropertyChangedEventArgs, Hue, Saturation, Color, Value

### Community 82 - "Nextcalibur on a machine that has never had the vendor software"
Cohesion: 0.22
Nodes (8): Machine-wide modifications, Nextcalibur on a machine that has never had the vendor software, The mailbox is firmware; reaching it is a matter of rights, The vendor's settings are never touched, Verified, What degrades, and how, What the application depends on, What the vendor's uninstaller does to a running Nextcalibur

### Community 83 - "README.md"
Cohesion: 0.33
Nodes (3): How it works, Questions people ask, Using it

### Community 84 - "Nextcalibur 0.5.0"
Cohesion: 0.22
Nodes (8): A machine that is not this one gets nothing to click, Graphics mode, all three, Install, uninstall, start, Keyboard backlight and Fn+Space, Living beside, and after, the vendor's software, Nextcalibur 0.5.0, System mode is now the whole mode, Under the hood

### Community 86 - "Nextcalibur 0.5.1"
Cohesion: 0.25
Nodes (7): Display Mode, Nextcalibur 0.5.1, Overheat warning, per chip, Small things, The readings, on every page, The window's own dialogues, Updates, on your terms

### Community 88 - "Test-Ui.ps1"
Cohesion: 0.39
Nodes (5): Enabled(), Find(), Invoke(), Say(), Text()

### Community 89 - "4. LED — `a1 = 0x0100`"
Cohesion: 0.29
Nodes (7): 4. LED — `a1 = 0x0100`, A sweep of the registers nobody uses (read only, 11 September 2026), Brightness (`B`), Devices (`a2`), Effects (`E`), Read (`a0 = 0xFA00`), Write (`a0 = 0xFB00`)

### Community 90 - "Nextcalibur 0.5.9"
Cohesion: 0.29
Nodes (6): Corrections to the 0.5.5 notes, Fixed, .NET 10, Nextcalibur 0.5.9, Nothing outside the application changes it, Tested on

### Community 91 - "Releasing, and signing"
Cohesion: 0.29
Nodes (7): How a release happens, Releasing, and signing, Repository settings that matter, Signing: what it is and what it buys, The history rewrite of 13 September 2026, The routes, Wiring it into the workflow

### Community 92 - ".Pick"
Cohesion: 0.29
Nodes (3): MouseEventArgs, MouseButtonEventArgs, Point

### Community 93 - "TourPage"
Cohesion: 0.29
Nodes (7): TourPage, Any, Display, Lighting, Power, Settings, System

### Community 94 - "Log-Graphics.ps1"
Cohesion: 0.48
Nodes (5): Get-BootStamp(), Get-Nvidia(), Get-RegGpuMode(), Sample(), Write-Header()

### Community 95 - "ValueConverters.cs"
Cohesion: 0.40
Nodes (3): Nextcalibur.Core, system_globalization, system_windows_data

### Community 96 - "Privacy"
Cohesion: 0.33
Nodes (6): Changes, Children of the application, Privacy, What it reads, What leaves the machine, What stays on the machine

### Community 97 - "Security"
Cohesion: 0.33
Nodes (6): Attacks that were tried, How releases are made, Reporting, Security, Supported versions, What counts

### Community 98 - ".OnBrightnessChanged"
Cohesion: 0.33
Nodes (5): RoutedPropertyChangedEventArgs, BrightnessSlider, CpuWarnSlider, GpuWarnSlider, Slider

### Community 100 - "TourStep"
Cohesion: 0.33
Nodes (5): TourStep, Body, SectionName, Title, TourPage

### Community 102 - "LedZone"
Cohesion: 0.33
Nodes (6): LedZone, AllKeyboard, Everything, Left, Middle, Right

### Community 103 - "MachineWideGate"
Cohesion: 0.33
Nodes (4): Mutex, TimeSpan, MachineWideGate, system_threading

### Community 104 - "PowerSource"
Cohesion: 0.40
Nodes (4): DllImport, PowerSource, SystemPowerStatus, SystemPowerStatus

### Community 105 - "Notice"
Cohesion: 0.40
Nodes (4): Interoperability, Notice, Third-party components, What this repository does not contain

### Community 106 - "AppIdentity.cs"
Cohesion: 0.40
Nodes (4): system_io_directory, system_io_ioexception, system_io_path, system_runtime_interopservices_comtypes

### Community 108 - "Grant-MailboxAccess.ps1"
Cohesion: 0.60
Nodes (3): Get-CurrentDescriptor(), Show-State(), Test-CanReadSecurityKey()

### Community 110 - "Trace-ModeSwitch.ps1"
Cohesion: 0.70
Nodes (4): Get-Drivers(), Get-RegistryValues(), Get-State(), Get-VendorFiles()

### Community 111 - "Keycaps"
Cohesion: 0.50
Nodes (4): Background, Keycaps, TourShade, Path

### Community 112 - "CpuFanGauge"
Cohesion: 0.67
Nodes (4): RpmToDoubleConverter, CpuFanGauge, GpuFanGauge, FanGauge

### Community 113 - "DriveGauge"
Cohesion: 0.50
Nodes (4): Percent, DriveGauge, RamGauge, DonutGauge

### Community 114 - "Nextcalibur 0.5.6"
Cohesion: 0.50
Nodes (3): Nextcalibur 0.5.6, Tested on, Windows Installed Apps & Control Panel registration

### Community 116 - ".TryParseRgb"
Cohesion: 0.50
Nodes (3): B, G, R

### Community 117 - "Tools"
Cohesion: 0.50
Nodes (3): Captures (`trace/`), Still worth running, Tools

## Knowledge Gaps
- **394 isolated node(s):** `SelectedColour`, `VisualChildrenCount`, `Diameter`, `Maximum`, `ArcBrush` (+389 more)
  These have ≤1 connection - possible missing edges or undocumented components. (Counts symbols only; 745 node(s) total have ≤1 connection when file, concept and rationale nodes are included.)
- **30 thin communities (<3 nodes) omitted from report** — run `graphify query` to explore isolated nodes.

## Suggested Questions
_Questions this graph is uniquely positioned to answer:_

- **Why does `MainWindow` connect `MainWindow` to `Window`, `.CheckProgram`, `UpdateService`, `GpuClockReader`, `CpuPowerReader`, `SystemInfo`, `UserPresence`, `PowerOverlayService`, `.Get`, `RadioButton`, `SystemMode`, `Nextcalibur.App`, `WindowsFaults`, `Button`, `.RequestRestart`, `AppSettings`, `ProcessPresence`, `.Warn`, `Program`, `.PowerModeControls`, `.Read`, `.OnLoaded`, `EcMailbox`, `.OnGpuModeChanged`, `BacklightKeyWatcher`, `.OfferDependency`, `LedController`, `.StartSlowTimer`, `LedEffect`, `GpuModeService`, `BatteryModePolicy`, `Strings`, `Grid`, `ThemeService`, `NvidiaDriverState`, `SupportVerdict`, `TourPage`, `.OnBrightnessChanged`, `.OnThresholdMoved`, `TourStep`, `.Sample`, `LedZone`?**
  _High betweenness centrality (0.450) - this node is a cross-community bridge._
- **Why does `Window` connect `Window` to `MainWindow`, `RadioButton`, `Button`, `.RequestRestart`, `.Warn`, `.PowerModeControls`, `.OnLoaded`, `.OnGpuModeChanged`, `Grid`, `.OnBrightnessChanged`, `Keycaps`, `CpuFanGauge`, `DriveGauge`, `ColGauge`, `DialogProgress`, `DriveDot`, `DriveList`, `EffectPanel`, `Wheel`?**
  _High betweenness centrality (0.159) - this node is a cross-community bridge._
- **Why does `DependencyStatus` connect `UpdateService` to `system_text_json`, `.OfferDependency`, `MainWindow`, `Dependency`?**
  _High betweenness centrality (0.095) - this node is a cross-community bridge._
- **Are the 12 inferred relationships involving `AppSettings` (e.g. with `.A_polling_interval_from_the_file_is_a_sane_one()` and `.A_warning_threshold_from_the_file_is_a_sane_one()`) actually correct?**
  _`AppSettings` has 12 INFERRED edges - model-reasoned connections that need verification._
- **What connects `SelectedColour`, `VisualChildrenCount`, `Diameter` to the rest of the system?**
  _394 weakly-connected nodes found - possible documentation gaps or missing edges._
- **Should `Window` be split into smaller, more focused modules?**
  _Cohesion score 0.041176470588235294 - nodes in this community are weakly interconnected._
- **Should `DotNetRuntimeDependency` be split into smaller, more focused modules?**
  _Cohesion score 0.054527750730282376 - nodes in this community are weakly interconnected._