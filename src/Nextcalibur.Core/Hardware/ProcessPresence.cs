using System.Runtime.InteropServices;
using System.Text;

namespace Nextcalibur.Core.Hardware;

/// <summary>
/// Answers "is a process with one of these names running?" cheaply enough
/// to ask every few seconds for the life of the application.
///
/// <c>Process.GetProcessesByName</c> costs about
/// 3 ms of processor time a call on this machine - it asks the kernel for
/// every process <em>with its threads</em> and builds objects for the lot -
/// and asked every four seconds that was most of what the application spent
/// while idle (measured 12 September 2026). This keeps the set of process
/// ids from the last look, which the kernel hands over in a few microseconds,
/// and opens only the ids it has not seen before to read their image name.
/// When nothing has started or stopped, a look costs one small call.
/// </summary>
public sealed class ProcessPresence
{
    private readonly string[] _names;
    private readonly HashSet<uint> _seen = new();
    private readonly HashSet<uint> _matching = new();
    private uint[] _ids = new uint[1024];

    /// <param name="imageNames">Executable names, with extension, compared case-insensitively.</param>
    public ProcessPresence(params string[] imageNames)
    {
        _names = imageNames;
    }

    /// <summary>Whether any process with one of the names is running right now.</summary>
    public bool AnyRunning()
    {
        var count = Enumerate();
        if (count < 0) return false;

        var current = new HashSet<uint>(count);
        for (var i = 0; i < count; i++) current.Add(_ids[i]);

        // Gone: forget. Kept: a matching id is checked again by name, so a
        // number the kernel has reused for something else does not keep
        // the answer true.
        _seen.RemoveWhere(id => !current.Contains(id));
        _matching.RemoveWhere(id => !current.Contains(id) || !Matches(id));

        foreach (var id in current)
        {
            if (!_seen.Add(id)) continue;
            if (Matches(id)) _matching.Add(id);
        }

        return _matching.Count > 0;
    }

    private int Enumerate()
    {
        while (true)
        {
            if (!K32EnumProcesses(_ids, (uint)(_ids.Length * sizeof(uint)), out var bytes)) return -1;
            var count = (int)(bytes / sizeof(uint));
            if (count < _ids.Length) return count;
            _ids = new uint[_ids.Length * 2];
        }
    }

    private bool Matches(uint id)
    {
        // PROCESS_QUERY_LIMITED_INFORMATION opens almost anything, including
        // protected processes; the few it cannot are not the vendor's.
        var handle = OpenProcess(0x1000, false, id);
        if (handle == IntPtr.Zero) return false;
        try
        {
            var buffer = new StringBuilder(260);
            var size = (uint)buffer.Capacity;
            if (!QueryFullProcessImageNameW(handle, 0, buffer, ref size)) return false;
            var path = buffer.ToString(0, (int)size);
            var slash = path.LastIndexOf('\\');
            var name = slash < 0 ? path : path[(slash + 1)..];
            foreach (var wanted in _names)
                if (string.Equals(name, wanted, StringComparison.OrdinalIgnoreCase)) return true;
            return false;
        }
        finally
        {
            CloseHandle(handle);
        }
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool K32EnumProcesses(uint[] ids, uint size, out uint bytesReturned);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(uint access, bool inherit, uint id);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern bool QueryFullProcessImageNameW(IntPtr process, uint flags, StringBuilder name, ref uint size);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CloseHandle(IntPtr handle);
}
