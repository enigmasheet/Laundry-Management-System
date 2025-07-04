using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Laundry.Shared
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns the Display Name defined on an enum value using the [Display(Name="...")] attribute.
        /// Falls back to enum name if no DisplayAttribute is found.
        /// </summary>
        public static string GetDisplayName(this Enum enumValue)
        {
            if (enumValue == null)
                return string.Empty;

            var memberInfo = enumValue.GetType().GetMember(enumValue.ToString());

            if (memberInfo.Length == 0)
                return enumValue.ToString();

            var displayAttr = memberInfo[0].GetCustomAttribute<DisplayAttribute>();

            return displayAttr?.Name ?? enumValue.ToString();
        }

        /// <summary>
        /// Returns all enum values and their display names as (int value, string name).
        /// Useful for dropdown binding.
        /// </summary>
        public static List<(int Value, string Name)> GetEnumDisplayList<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e => (Convert.ToInt32(e), (e as Enum).GetDisplayName()))
                .ToList();
        }
    }
}
