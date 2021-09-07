namespace ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Helpers
{
    public static class DocumentValidation
    {
        public static bool Valid(string value) => CpfValidation.Valid(value) || CnpjValidation.Valid(value);

        public static string RemoveMask(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            var result = CpfValidation.RemoveMask(value);

            return string.IsNullOrEmpty(result) ?
                CnpjValidation.RemoveMask(value) :
                result;
        }

        public static string AddMask(string value)
        {
            var withoutMask = RemoveMask(value);

            if (string.IsNullOrEmpty(withoutMask))
            {
                return string.Empty;
            }

            var result = CpfValidation.AddMask(value);

            return string.IsNullOrEmpty(result) ?
                CnpjValidation.AddMask(value) :
                result;
        }
    }
}
