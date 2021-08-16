using System;
using Utils.Reflection;

namespace Utils.Types.Enum
{
    public static class EnumExtensions
    {
        public static int ToInt<T>(this T @enum) where T : IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            return (int)(IConvertible)@enum;
        }

        public static int CountMembers<T>(this T _) where T : IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            return System.Enum.GetNames(typeof(T)).Length;
        }

        public static string GetDescription<T>(this T @enum) where T : IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            return @enum.GetDescriptionAttribute();
        }
    }
}
