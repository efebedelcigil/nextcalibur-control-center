using System.Runtime.InteropServices;

// Every native library this assembly imports - nvml.dll from the driver,
// pdh, cfgmgr32, powrprof, wintrust from Windows - comes from the system
// folder and from nowhere else: not the current directory, not PATH, both
// of which the account can put a file in, and the process is elevated.
[assembly: DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
