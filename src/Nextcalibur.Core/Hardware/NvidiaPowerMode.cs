using System.Runtime.InteropServices;

namespace Nextcalibur.Core.Hardware;

/// <summary>
/// NVIDIA's "Power management mode", the driver's global setting, read and
/// written through NVAPI's driver settings (NVIDIA/nvapi, the public header:
/// PREFERRED_PSTATE_ID 0x1057EB71). The owner's ask of 14 October 2026,
/// after "Prefer maximum performance" was found holding the card at P0 and
/// 17 W with nothing to do. Only the global (base) profile; a game's own
/// profile is the driver's and the game's.
///
/// nvapi64.dll is loaded only when signed as Windows installed it, like
/// nvml.dll. Every failure is quiet: no card, an old driver, a setting the
/// driver does not have - there is simply nothing to show.
/// </summary>
public static class NvidiaPowerMode
{
    public const uint SettingId = 0x1057EB71;

    /// <summary>The values as the header names them.</summary>
    public const uint Adaptive = 0, PreferMaximum = 1, OptimalPower = 5;

    /// <summary>What the driver uses when the profile does not say: OPTIMAL_POWER, the "Normal" of the Control Panel.</summary>
    public const uint DriverDefault = OptimalPower;

    /// <summary>The mode in force on the global profile, and whether the profile sets it (false: the driver's default); null when it cannot be read.</summary>
    public static (uint Value, bool Set)? Read()
    {
        try
        {
            return WithSession((session, profile, api) =>
            {
                var buffer = NewSetting(0);
                try
                {
                    var status = api.GetSetting(session, profile, SettingId, buffer);
                    if (status == SettingNotFound) return (DriverDefault, false);
                    return status == Ok ? ((uint)Marshal.ReadInt32(buffer, CurrentValueOffset), true) : ((uint, bool)?)null;
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                }
            });
        }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException or SEHException or AccessViolationException)
        {
            return null;
        }
    }

    /// <summary>Sets the mode on the global profile and saves it. False when it could not be done.</summary>
    public static bool Write(uint value)
    {
        if (value is not (Adaptive or PreferMaximum or OptimalPower)) return false;
        try
        {
            return WithSession((session, profile, api) =>
            {
                var buffer = NewSetting(value);
                try
                {
                    return api.SetSetting(session, profile, buffer) == Ok && api.SaveSettings(session) == Ok;
                }
                finally
                {
                    Marshal.FreeHGlobal(buffer);
                }
            });
        }
        catch (Exception ex) when (ex is DllNotFoundException or EntryPointNotFoundException or SEHException or AccessViolationException)
        {
            return false;
        }
    }

    // NVDRS_SETTING_V1, laid out as the header has it: version, a 2048-wide
    // name, id, type, location, two flags, then the predefined and the current
    // value as 4100-byte unions.
    private const int SettingSize = 0x3020;
    private const int SettingVersion = SettingSize | (1 << 16);
    private const int IdOffset = 4 + 4096, TypeOffset = IdOffset + 4, CurrentValueOffset = 4120 + 4100;
    private const int Ok = 0, SettingNotFound = -160;

    private static IntPtr NewSetting(uint value)
    {
        var buffer = Marshal.AllocHGlobal(SettingSize);
        Marshal.Copy(new byte[SettingSize], 0, buffer, SettingSize);
        Marshal.WriteInt32(buffer, 0, SettingVersion);
        Marshal.WriteInt32(buffer, IdOffset, unchecked((int)SettingId));
        Marshal.WriteInt32(buffer, TypeOffset, 0); // NVDRS_DWORD_TYPE
        Marshal.WriteInt32(buffer, CurrentValueOffset, unchecked((int)value));
        return buffer;
    }

    private static T? WithSession<T>(Func<IntPtr, IntPtr, Api, T?> work)
    {
        if (!Security.Integrity.NvapiIsTrusted() || Api.Load() is not { } api) return default;
        if (api.Initialize() != Ok || api.CreateSession(out var session) != Ok) return default;
        try
        {
            if (api.LoadSettings(session) != Ok || api.GetBaseProfile(session, out var profile) != Ok) return default;
            return work(session, profile, api);
        }
        finally
        {
            api.DestroySession(session);
        }
    }

    private sealed class Api
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int InitializeFn();
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int SessionOutFn(out IntPtr session);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int SessionFn(IntPtr session);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int ProfileOutFn(IntPtr session, out IntPtr profile);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int GetSettingFn(IntPtr session, IntPtr profile, uint id, IntPtr setting);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int SetSettingFn(IntPtr session, IntPtr profile, IntPtr setting);

        public required InitializeFn Initialize { get; init; }
        public required SessionOutFn CreateSession { get; init; }
        public required SessionFn LoadSettings { get; init; }
        public required ProfileOutFn GetBaseProfile { get; init; }
        public required GetSettingFn GetSetting { get; init; }
        public required SetSettingFn SetSetting { get; init; }
        public required SessionFn SaveSettings { get; init; }
        public required SessionFn DestroySession { get; init; }

        public static Api? Load()
        {
            T? Fn<T>(uint id) where T : Delegate =>
                QueryInterface(id) is var p && p != IntPtr.Zero ? Marshal.GetDelegateForFunctionPointer<T>(p) : null;

            var init = Fn<InitializeFn>(0x0150E828);
            var create = Fn<SessionOutFn>(0x0694D52E);
            var load = Fn<SessionFn>(0x375DBD6B);
            var base_ = Fn<ProfileOutFn>(0xDA8466A0);
            var get = Fn<GetSettingFn>(0x73BF8338);
            var set = Fn<SetSettingFn>(0x577DD202);
            var save = Fn<SessionFn>(0xFCBC7E14);
            var destroy = Fn<SessionFn>(0xDAD9CFF8);
            if (init is null || create is null || load is null || base_ is null || get is null || set is null || save is null || destroy is null) return null;
            return new Api
            {
                Initialize = init, CreateSession = create, LoadSettings = load, GetBaseProfile = base_,
                GetSetting = get, SetSetting = set, SaveSettings = save, DestroySession = destroy,
            };
        }

        [DllImport("nvapi64.dll", EntryPoint = "nvapi_QueryInterface", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr QueryInterface(uint id);
    }
}
