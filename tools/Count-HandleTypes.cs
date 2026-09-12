using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

// Counts a process's handles by object type, twice, and prints the change.
// usage: handles <pid> <secondsBetween> <outFile>
var pid = int.Parse(args[0]);
var wait = int.Parse(args[1]);
var outFile = args[2];

var sb = new StringBuilder();
var a = Snapshot(pid);
sb.AppendLine($"{DateTime.Now:HH:mm:ss} first: total {a.Values.Sum()}");
Thread.Sleep(wait * 1000);
var b = Snapshot(pid);
sb.AppendLine($"{DateTime.Now:HH:mm:ss} second: total {b.Values.Sum()}");
foreach (var type in a.Keys.Union(b.Keys).OrderBy(t => t))
{
    a.TryGetValue(type, out var x); b.TryGetValue(type, out var y);
    sb.AppendLine($"{type,-24} {x,6} -> {y,6}  ({y - x:+#;-#;0})");
}
File.WriteAllText(outFile, sb.ToString());

static Dictionary<string, int> Snapshot(int pid)
{
    var result = new Dictionary<string, int>();
    using var target = Process.GetProcessById(pid);
    var hTarget = OpenProcess(0x0040 /* PROCESS_DUP_HANDLE */, false, pid);
    if (hTarget == IntPtr.Zero) throw new Exception("OpenProcess failed: " + Marshal.GetLastWin32Error());

    var size = 1 << 20;
    var buffer = Marshal.AllocHGlobal(size);
    int status;
    while ((status = NtQuerySystemInformation(64, buffer, size, out var needed)) == unchecked((int)0xC0000004))
    {
        size = needed + (1 << 16);
        buffer = Marshal.ReAllocHGlobal(buffer, (IntPtr)size);
    }
    if (status != 0) throw new Exception("NtQuerySystemInformation: " + status.ToString("X"));

    var count = Marshal.ReadInt64(buffer);
    var entry = buffer + 16;
    var entrySize = Marshal.SizeOf<SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX>();
    var typeNames = new Dictionary<ushort, string>();
    for (long i = 0; i < count; i++, entry += entrySize)
    {
        var e = Marshal.PtrToStructure<SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX>(entry);
        if ((int)e.UniqueProcessId != pid) continue;
        if (!typeNames.TryGetValue(e.ObjectTypeIndex, out var name))
        {
            name = "?";
            if (DuplicateHandle(hTarget, e.HandleValue, GetCurrentProcess(), out var dup, 0, false, 2 /* SAME_ACCESS */))
            {
                var tb = Marshal.AllocHGlobal(4096);
                if (NtQueryObject(dup, 2, tb, 4096, out _) == 0)
                    name = Marshal.PtrToStringUni(Marshal.ReadIntPtr(tb + 8), Marshal.ReadInt16(tb) / 2) ?? "?";
                Marshal.FreeHGlobal(tb);
                CloseHandle(dup);
            }
            typeNames[e.ObjectTypeIndex] = name;
        }
        result[name] = result.GetValueOrDefault(name) + 1;
    }
    Marshal.FreeHGlobal(buffer);
    CloseHandle(hTarget);
    return result;
}


[StructLayout(LayoutKind.Sequential)]
struct SYSTEM_HANDLE_TABLE_ENTRY_INFO_EX
{
    public IntPtr Object; public IntPtr UniqueProcessId; public IntPtr HandleValue;
    public uint GrantedAccess; public ushort CreatorBackTraceIndex; public ushort ObjectTypeIndex;
    public uint HandleAttributes; public uint Reserved;
}


static partial class Program
{
[DllImport("ntdll.dll")] static extern int NtQuerySystemInformation(int cls, IntPtr buf, int len, out int needed);
[DllImport("ntdll.dll")] static extern int NtQueryObject(IntPtr h, int cls, IntPtr buf, int len, out int needed);
[DllImport("kernel32.dll", SetLastError = true)] static extern IntPtr OpenProcess(uint access, bool inherit, int pid);
[DllImport("kernel32.dll", SetLastError = true)] static extern bool DuplicateHandle(IntPtr src, IntPtr h, IntPtr dst, out IntPtr dup, uint access, bool inherit, uint options);
[DllImport("kernel32.dll")] static extern IntPtr GetCurrentProcess();
[DllImport("kernel32.dll")] static extern bool CloseHandle(IntPtr h);
}
