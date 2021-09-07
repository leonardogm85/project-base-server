using ProjetoBase.Infrastructure.CrossCutting.Common.Extensions;
using System.Linq;
using System.Text.RegularExpressions;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Helpers
{
    public static class CpfValidation
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
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };

            if (invalid.Any(i => i == withoutMask))
            {
                return false;
            }

            var add = 0;

            for (var i = 0; i < 9; i++)
            {
                add += int.Parse(withoutMask[i].ToString()) * (10 - i);
            }

            var rev = 11 - (add % 11);

            if (rev == 10 || rev == 11)
            {
                rev = 0;
            }

            if (rev != int.Parse(withoutMask[9].ToString()))
            {
                return false;
            }

            add = 0;

            for (var i = 0; i < 10; i++)
            {
                add += int.Parse(withoutMask[i].ToString()) * (11 - i);
            }

            rev = 11 - (add % 11);

            if (rev == 10 || rev == 11)
            {
                rev = 0;
            }

            if (rev != int.Parse(withoutMask[10].ToString()))
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

            return onlyNumbers.Length == 11 ?
                onlyNumbers :
                string.Empty;
        }

        public static string AddMask(string value)
        {
            var withoutMask = RemoveMask(value);

            return string.IsNullOrEmpty(withoutMask) ?
                string.Empty :
                Regex.Replace(withoutMask, @"(\d{3})(\d{3})(\d{3})(\d{2})", @"$1.$2.$3-$4");
        }
    }
}
