namespace ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts
{
    public partial class NotificationContract
    {
        public NotificationContract IsNull(decimal? value, string message) => IsFalse(value.HasValue, message);

        public NotificationContract IsntNull(decimal? value, string message) => IsTrue(value.HasValue, message);

        public NotificationContract AreEquals(decimal value, decimal comparer, string message) => IsTrue(value == comparer, message);

        public NotificationContract ArentEquals(decimal value, decimal comparer, string message) => IsFalse(value == comparer, message);

        public NotificationContract IsBetween(decimal value, decimal from, decimal to, string message) => IsTrue(value >= from && value <= to, message);

        public NotificationContract IsGreaterThan(decimal value, decimal comparer, string message) => IsTrue(value > comparer, message);

        public NotificationContract IsGreaterOrEqualsThan(decimal value, decimal comparer, string message) => IsTrue(value >= comparer, message);

        public NotificationContract IsLowerThan(decimal value, decimal comparer, string message) => IsTrue(value < comparer, message);

        public NotificationContract IsLowerOrEqualsThan(decimal value, decimal comparer, string message) => IsTrue(value <= comparer, message);
    }
}
