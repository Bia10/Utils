using System;
using System.ComponentModel;
using System.Reflection;

namespace Utils.Types.Enumeration
{
    public static class EnumExtensions
    {
        public static int ToInt(this Enum @enum)
        {
            return (int)(IConvertible)@enum;
        }

        public static int CountMembers(this Enum _)
        {
            return Enum.GetNames(typeof(Enum)).Length;
        }

        public static string GetDescription(this Enum value)
        {
            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null) return null;
            var attribute = (DescriptionAttribute)fieldInfo.GetCustomAttribute(typeof(DescriptionAttribute));

            return attribute.Description;
        }
    }
}
