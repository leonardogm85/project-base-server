using ProjetoBase.Infrastructure.CrossCutting.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Extensions
{
    public static class EnumExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);

            return type.GetField(name).GetCustomAttribute<TAttribute>();
        }

        public static TType GetValue<TType>(this Enum value) where TType : struct => (TType)(object)value;

        public static IEnumerable<TEnum> GetAll<TEnum>(this Type type) where TEnum : Enum => Enum.GetValues(type).Cast<TEnum>();

        public static string GetDescription(this Enum value) => value.GetAttribute<DescriptionAttribute>().Description;
    }
}
