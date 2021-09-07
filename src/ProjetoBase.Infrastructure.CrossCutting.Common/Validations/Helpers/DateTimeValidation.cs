using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Helpers
{
    public static class DateTimeValidation
    {
        public static readonly DateTime DbMinValue = new DateTime(1753, 1, 1);
    }
}
