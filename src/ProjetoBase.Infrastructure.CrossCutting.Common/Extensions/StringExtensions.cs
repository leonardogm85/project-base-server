using System.Linq;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Extensions
{
    public static class StringExtensions
    {
        public static string OnlyNumbers(this string value) => string.Concat(value.Where(c => char.IsDigit(c)));

        public static string OnlyLetters(this string value) => string.Concat(value.Where(c => char.IsLetter(c)));
    }
}
