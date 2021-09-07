using System.Text.RegularExpressions;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Helpers
{
    public static class EmailValidation
    {
        public const string Pattern = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        public static bool Valid(string value) => !string.IsNullOrEmpty(value) && Regex.IsMatch(value, Pattern);
    }
}
