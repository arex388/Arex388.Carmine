using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Arex388.Carmine {
    internal static class Language2Extensions {
        public static string ToValue<T>(
            this T value)
            where T : struct {
            var type = value.GetType();

            if (!type.IsEnum) {
                return string.Empty;
            }

            var memberInfo = type.GetMember($"{value}");
            var display = memberInfo[0].GetCustomAttribute<DisplayAttribute>();

            return display is null
                ? value.ToString()
                : display.Name;
        }
    }
}