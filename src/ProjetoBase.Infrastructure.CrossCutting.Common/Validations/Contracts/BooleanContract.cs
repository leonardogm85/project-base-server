using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System.Linq;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts
{
    public partial class NotificationContract
    {
        public NotificationContract IsTrue(bool value, string message) => IsFalse(!value, message);

        public NotificationContract IsFalse(bool value, string message)
        {
            if (value)
            {
                Notifications = Notifications.Append(new Notification(message));
            }

            return this;
        }
    }
}
