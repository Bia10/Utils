using System;
using System.ComponentModel;
using System.Reflection;

namespace Utils.Reflection
{
    public static class ReflectionExtensions
    {
        public static string GetDescriptionAttribute<T>(this T systemType)
        {
            Type type = systemType.GetType();
            if (type == null) 
                throw new InvalidOperationException("Failed to parse system type!");

            FieldInfo fieldInfo = type.GetField(systemType.ToString());
            if (fieldInfo == null) 
                throw new InvalidOperationException("Failed to obtain fieldInfo!");

            DescriptionAttribute[] descAttributes = 
                (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (descAttributes != null && descAttributes.Length > 0) 
                return descAttributes[0].Description;
            else 
                return systemType.ToString();
        }
    }
}
