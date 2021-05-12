using System;
using System.ComponentModel;

namespace Utils.Reflection
{
    public static class ReflectionExtensions
    {
        public static string GetDescriptionAttribute<T>(this T systemType)
        {
            var type = systemType.GetType();
            if (type == null) 
                throw new InvalidOperationException("Failed to parse system type!");

            var fieldInfo = type.GetField(systemType.ToString() ?? string.Empty);
            if (fieldInfo == null) 
                throw new InvalidOperationException("Failed to obtain fieldInfo!");

            var descAttributes = 
                (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descAttributes is {Length: > 0} ? descAttributes[0].Description : systemType.ToString();
        }
    }
}
