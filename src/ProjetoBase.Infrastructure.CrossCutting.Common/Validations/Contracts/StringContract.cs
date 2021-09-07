using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Helpers;
using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts
{
    public partial class NotificationContract
    {
        public NotificationContract IsNullOrWhiteSpace(string value, string message) => IsTrue(string.IsNullOrWhiteSpace(value), message);

        public NotificationContract IsntNullOrWhiteSpace(string value, string message) => IsFalse(string.IsNullOrWhiteSpace(value), message);

        public NotificationContract AreEquals(string value, string comparer, string message) =>
            IsTrue(string.Equals(value, comparer, StringComparison.OrdinalIgnoreCase), message);

        public NotificationContract ArentEquals(string value, string comparer, string message) =>
            IsFalse(string.Equals(value, comparer, StringComparison.OrdinalIgnoreCase), message);

        public NotificationContract HasMinLength(string value, int length, string message) =>
            string.IsNullOrEmpty(value) ? this : IsTrue(value.Length >= length, message);

        public NotificationContract HasMaxLength(string value, int length, string message) =>
            string.IsNullOrEmpty(value) ? this : IsTrue(value.Length <= length, message);

        public NotificationContract HasLength(string value, int length, string message) => string.IsNullOrEmpty(value) ? this : IsTrue(value.Length == length, message);

        public NotificationContract IsEmail(string value, string message) => string.IsNullOrEmpty(value) ? this : IsTrue(EmailValidation.Valid(value), message);

        public NotificationContract IsPhone(string value, string message) => string.IsNullOrEmpty(value) ? this : IsTrue(PhoneValidation.Valid(value), message);

        public NotificationContract IsZipCode(string value, string message) => string.IsNullOrEmpty(value) ? this : IsTrue(ZipCodeValidation.Valid(value), message);

        public NotificationContract IsCpf(string value, string message) => string.IsNullOrEmpty(value) ? this : IsTrue(CpfValidation.Valid(value), message);

        public NotificationContract IsCnpj(string value, string message) => string.IsNullOrEmpty(value) ? this : IsTrue(CnpjValidation.Valid(value), message);

        public NotificationContract IsDocument(string value, string message) => string.IsNullOrEmpty(value) ? this : IsTrue(DocumentValidation.Valid(value), message);
    }
}
