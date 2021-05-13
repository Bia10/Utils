using System.Runtime.InteropServices;
using Utils.Console;
using Utils.Device.Display.Enums;
using Utils.Device.Display.Structures;
using Utils.Types;
using Utils.Win32;

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
            if (string.IsNullOrEmpty(DevMode.dmDeviceName))
                return;

            var structType = typeof(DeviceMode);
            var fields = structType.GetFields();

            foreach (var field in fields)
                ConsoleExtensions.Log($"{field.Name} {field.GetValue(DevMode)}", "info");
        }
    }
}