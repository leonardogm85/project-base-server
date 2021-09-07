using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts
{
    public partial class NotificationContract
    {
        public NotificationContract IsNull(DateTime? value, string message) => IsFalse(value.HasValue, message);

        public NotificationContract IsntNull(DateTime? value, string message) => IsTrue(value.HasValue, message);

        public NotificationContract AreEquals(DateTime value, DateTime comparer, string message) => IsTrue(value == comparer, message);

        public NotificationContract ArentEquals(DateTime value, DateTime comparer, string message) => IsFalse(value == comparer, message);

        public NotificationContract IsBetween(DateTime value, DateTime from, DateTime to, string message) => IsTrue(value >= from && value <= to, message);

        public NotificationContract IsGreaterThan(DateTime value, DateTime comparer, string message) => IsTrue(value > comparer, message);

        public NotificationContract IsGreaterOrEqualsThan(DateTime value, DateTime comparer, string message) => IsTrue(value >= comparer, message);

        public NotificationContract IsLowerThan(DateTime value, DateTime comparer, string message) => IsTrue(value < comparer, message);

        public NotificationContract IsLowerOrEqualsThan(DateTime value, DateTime comparer, string message) => IsTrue(value <= comparer, message);
    }
}
