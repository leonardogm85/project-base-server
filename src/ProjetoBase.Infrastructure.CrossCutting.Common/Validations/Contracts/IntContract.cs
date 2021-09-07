namespace ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts
{
    public partial class NotificationContract
    {
        public NotificationContract IsNull(int? value, string message) => IsFalse(value.HasValue, message);

        public NotificationContract IsntNull(int? value, string message) => IsTrue(value.HasValue, message);

        public NotificationContract AreEquals(int value, int comparer, string message) => IsTrue(value == comparer, message);

        public NotificationContract ArentEquals(int value, int comparer, string message) => IsFalse(value == comparer, message);

        public NotificationContract IsBetween(int value, int from, int to, string message) => IsTrue(value >= from && value <= to, message);

        public NotificationContract IsGreaterThan(int value, int comparer, string message) => IsTrue(value > comparer, message);

        public NotificationContract IsGreaterOrEqualsThan(int value, int comparer, string message) => IsTrue(value >= comparer, message);

        public NotificationContract IsLowerThan(int value, int comparer, string message) => IsTrue(value < comparer, message);

        public NotificationContract IsLowerOrEqualsThan(int value, int comparer, string message) => IsTrue(value <= comparer, message);
    }
}
