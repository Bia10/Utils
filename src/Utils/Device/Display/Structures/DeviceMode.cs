using System.Runtime.InteropServices;

namespace Utils.Device.Display.Structures
{
    public struct Constants
    {
        public const int CCHDEVICENAME = 32;
        public const int CCHFORMNAME = 32;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public readonly struct PointLong
    {
        [MarshalAs(UnmanagedType.I4)] private readonly int x;
        [MarshalAs(UnmanagedType.I4)] private readonly int y;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct DeviceMode
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.CCHDEVICENAME)]
        public readonly string dmDeviceName; //BCHAR dmDeviceName[CCHDEVICENAME];
        [MarshalAs(UnmanagedType.U2)]
        public readonly ushort dmSpecVersion;
        [MarshalAs(UnmanagedType.U2)]
        public readonly ushort dmDriverVersion;
        [MarshalAs(UnmanagedType.U2)]
        public ushort dmSize;
        [MarshalAs(UnmanagedType.U2)]
        public readonly ushort dmDriverExtra;
        [MarshalAs(UnmanagedType.U4)]
        public readonly uint dmFields;
        public readonly PointLong dmPosition;
        [MarshalAs(UnmanagedType.U4)]
        public readonly uint dmDisplayOrientation;
        [MarshalAs(UnmanagedType.U4)]
        public readonly uint dmDisplayFixedOutput;
        [MarshalAs(UnmanagedType.I2)]
        public readonly short dmColor;
        [MarshalAs(UnmanagedType.I2)]
        public readonly short dmDuplex;
        [MarshalAs(UnmanagedType.I2)]
        public readonly short dmYResolution;
        [MarshalAs(UnmanagedType.I2)]
        public readonly short dmTTOption;
        [MarshalAs(UnmanagedType.I2)]
        public readonly short dmCollate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = Constants.CCHFORMNAME)]
        public readonly string dmFormName; //BYTE dmFormName[CCHFORMNAME];
        [MarshalAs(UnmanagedType.U2)]
        public readonly ushort dmLogPixels;
        [MarshalAs(UnmanagedType.U4)]
        public readonly uint dmBitsPerPel;
        [MarshalAs(UnmanagedType.U4)]
        public readonly uint dmPelsWidth;
        [MarshalAs(UnmanagedType.U4)]
        public readonly uint dmPelsHeight;
        [MarshalAs(UnmanagedType.U4)]
        public readonly uint dmDisplayFlags;
        [MarshalAs(UnmanagedType.U4)]
        public readonly uint dmDisplayFrequency;
        [MarshalAs(UnmanagedType.U4)]
        public readonly uint dmICMMethod;
        [MarshalAs(UnmanagedType.U4)]
        public readonly uint dmICMIntent;
        [MarshalAs(UnmanagedType.U4)]
        public readonly uint dmMediaType;
        [MarshalAs(UnmanagedType.U4)]
        public readonly uint dmDitherType;
        [MarshalAs(UnmanagedType.U4)]
        public readonly uint dmReserved1;
        [MarshalAs(UnmanagedType.U4)]
        public readonly uint dmReserved2;
        [MarshalAs(UnmanagedType.U4)]
        public readonly uint dmPanningWidth;
        [MarshalAs(UnmanagedType.U4)]
        public readonly uint dmPanningHeight;
    }
}