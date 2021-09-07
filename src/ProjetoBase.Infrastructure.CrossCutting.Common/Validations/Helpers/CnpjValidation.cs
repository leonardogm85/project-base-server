using ProjetoBase.Infrastructure.CrossCutting.Common.Extensions;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Helpers
{
    public static class CnpjValidation
    {
        public static bool Valid(string value)
        {
            var withoutMask = RemoveMask(value);

            if (string.IsNullOrEmpty(withoutMask))
            {
                return false;
            }

            var invalid = new string[]
            {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };

            if (invalid.Any(i => i == withoutMask))
            {
                return false;
            }

            var length = withoutMask.Length - 2;
            var numbers = withoutMask.Substring(0, length);
            var digits = withoutMask.Substring(length);
            var add = 0;
            var position = length - 7;

            for (var i = length; i >= 1; i--)
            {
                add += int.Parse(numbers[length - i].ToString()) * position--;

                if (position < 2)
                {
                    position = 9;
                }
            }

            var rev = ((add % 11) < 2) ?
                0 :
                (11 - (add % 11));

            if (rev != int.Parse(digits[0].ToString()))
            {
                return false;
            }

            length++;
            numbers = withoutMask.Substring(0, length);
            add = 0;
            position = length - 7;

            for (var i = length; i >= 1; i--)
            {
                add += int.Parse(numbers[length - i].ToString()) * position--;

                if (position < 2)
                {
                    position = 9;
                }
            }

            rev = ((add % 11) < 2) ?
                0 :
                (11 - (add % 11));

            if (rev != int.Parse(digits[1].ToString()))
            {
                return false;
            }

            return true;
        }

        public static string RemoveMask(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            var onlyNumbers = value.OnlyNumbers();

            return onlyNumbers.Length == 14 ?
                onlyNumbers :
                string.Empty;
        }

        public static string AddMask(string value)
        {
            var withoutMask = RemoveMask(value);

            return string.IsNullOrEmpty(withoutMask) ?
                string.Empty :
                Regex.Replace(withoutMask, @"(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})", @"$1.$2.$3/$4-$5");
        }
    }
}
