namespace ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts
{
    public partial class NotificationContract
    {
        public NotificationContract IsNull(object value, string message) => IsTrue(value == null, message);

        public NotificationContract IsntNull(object value, string message) => IsFalse(value == null, message);

        public NotificationContract AreEquals(object value, object comparer, string message) => IsTrue(value.Equals(comparer), message);

        public NotificationContract ArentEquals(object value, object comparer, string message) => IsFalse(value.Equals(comparer), message);
    }
}
