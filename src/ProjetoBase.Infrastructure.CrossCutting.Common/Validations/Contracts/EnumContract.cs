using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts
{
    public partial class NotificationContract
    {
        public NotificationContract IsDefined(Enum value, string message) => IsTrue(Enum.IsDefined(value.GetType(), value), message);

        public NotificationContract IsntDefined(Enum value, string message) => IsFalse(Enum.IsDefined(value.GetType(), value), message);

        public NotificationContract AreEquals(Enum value, Enum comparer, string message) => IsTrue(value == comparer, message);

        public NotificationContract ArentEquals(Enum value, Enum comparer, string message) => IsFalse(value == comparer, message);
    }
}
