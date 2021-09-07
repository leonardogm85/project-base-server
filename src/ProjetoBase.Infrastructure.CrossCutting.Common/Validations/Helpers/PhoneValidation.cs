using ProjetoBase.Infrastructure.CrossCutting.Common.Extensions;
using System.Text.RegularExpressions;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Helpers
{
    public static class PhoneValidation
    {
        public const string Pattern = @"^\([\d]{2}\)\s[\d]{4,5}\-[\d]{4}$";

        public static bool Valid(string value) => !string.IsNullOrEmpty(value) && Regex.IsMatch(value, Pattern);

        public static string RemoveMask(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            var onlyNumbers = value.OnlyNumbers();

            return onlyNumbers.Length == 10 || onlyNumbers.Length == 11 ?
                onlyNumbers :
                string.Empty;
        }

        public static string AddMask(string value)
        {
            var withoutMask = RemoveMask(value);

            return string.IsNullOrEmpty(withoutMask) ?
                string.Empty :
                Regex.Replace(withoutMask, @"(\d{2})(\d{4,5})(\d{4})", "($1) $2-$3");
        }
    }
}
