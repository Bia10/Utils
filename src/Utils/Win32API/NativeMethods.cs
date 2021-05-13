using System.Runtime.InteropServices;
using Utils.Device.Display.Structures;

namespace Utils.Win32API
{
    public static class NativeMethods
    {
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        public static extern bool EnumDisplaySettings(string deviceName, int modeNum, ref DeviceMode devMode);
    }
}