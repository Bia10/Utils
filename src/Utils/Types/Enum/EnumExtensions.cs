using System;
using Utils.Reflection;

namespace Utils.Types.Enum
{
    public static class EnumExtensions
    {
        public static int ToInt(this System.Enum @enum)
        {
            return (int)(IConvertible)@enum;
        }

        public static int CountMembers<T>(this T _) 
            where T: System.Enum
        {
            return System.Enum.GetNames(typeof(T)).Length;
        }

        public static string GetDescription(this System.Enum value)
        {
            return value.GetDescriptionAttribute();
        }
    }
}
