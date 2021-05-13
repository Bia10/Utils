using System;
using Utils.Reflection;

namespace Utils.Types
{
    public static class EnumExtensions
    {
        public static int ToInt(this Enum @enum)
        {
            return (int)(IConvertible)@enum;
        }

        public static int CountMembers<T>(this T _) 
            where T: Enum
        {
            return Enum.GetNames(typeof(T)).Length;
        }

        public static string GetDescription(this Enum value)
        {
            return value.GetDescriptionAttribute();
        }
    }
}
