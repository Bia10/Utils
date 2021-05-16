using System;
using System.Runtime.InteropServices;
using Utils.Console;
using Utils.Device.Display.Enums;
using Utils.Device.Display.Structures;
using Utils.Types.Enum;
using Utils.Types.String;
using Utils.Win32API;

namespace Utils.Device.Display
{
    public class DisplaySettings
    {
        public readonly DeviceMode DevMode;

        public DisplaySettings()
        {
            DevMode = GetCurrentDisplaySettings();
        }

        private static DeviceMode GetCurrentDisplaySettings()
        {
            DeviceMode devMode = default;
            devMode.dmSize = (ushort)Marshal.SizeOf(devMode);

            if (NativeMethods.EnumDisplaySettings(null, DisplayModeNumber.CurrentSettings.ToInt(), ref devMode))
                return devMode;

            ConsoleExtensions.Log("Failed to obtain current display settings!", "error");
            return new DeviceMode();
        }

        public void PrintCurrentDisplaySettings()
        {
            if (!DevMode.dmDeviceName.Valid())
                throw new InvalidOperationException("Display device name is not valid! " +
                                                    $"DeviceName: {DevMode.dmDeviceName}");

            foreach (var field in typeof(DeviceMode).GetFields())
                ConsoleExtensions.Log($"{field.Name} {field.GetValue(DevMode)}", "info");
        }
    }
}