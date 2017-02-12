using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace TheGodfatherGM.Data.Extensions
{
    public static class EnumExtensions
    {
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());

            return memInfo.First().GetCustomAttributes().OfType<T>().FirstOrDefault();
        }

        public static string GetDisplayName(this Enum enumVal)
        {
            return enumVal.GetAttributeOfType<DisplayAttribute>()?.Name ?? enumVal.ToString();
        }
    }
}
