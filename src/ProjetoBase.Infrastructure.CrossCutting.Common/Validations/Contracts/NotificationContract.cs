using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts
{
    public partial class NotificationContract
    {
        public NotificationContract() => Notifications = Enumerable.Empty<Notification>();

        public IEnumerable<Notification> Notifications { get; private set; }
    }
}
