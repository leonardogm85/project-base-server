using System.Collections.Generic;
using System.Linq;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts
{
    public partial class NotificationContract
    {
        public NotificationContract IsEmpty<T>(IEnumerable<T> value, string message) where T : class => IsFalse(value.Any(), message);

        public NotificationContract IsntEmpty<T>(IEnumerable<T> value, string message) where T : class => IsTrue(value.Any(), message);
    }
}
