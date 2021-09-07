using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts
{
    public partial class NotificationContract
    {
        public NotificationContract AreEquals(Guid value, Guid comparer, string message) => IsTrue(value == comparer, message);

        public NotificationContract ArentEquals(Guid value, Guid comparer, string message) => IsFalse(value == comparer, message);

        public NotificationContract IsEmpty(Guid value, string message) => IsTrue(value == Guid.Empty, message);

        public NotificationContract IsntEmpty(Guid value, string message) => IsFalse(value == Guid.Empty, message);
    }
}
