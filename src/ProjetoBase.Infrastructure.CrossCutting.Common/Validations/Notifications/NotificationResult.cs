using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications
{
    public class NotificationResult
    {
        public NotificationResult() => Notifications = Enumerable.Empty<Notification>();

        public IEnumerable<Notification> Notifications { get; private set; }

        public bool Valid => !Invalid;

        public bool Invalid => Notifications.Any();

        public void AddNotification(string message) => AddNotification(new Notification(message));

        public void AddNotification(Notification notification) => Notifications = Notifications.Append(notification);

        public void AddNotifications(NotificationContract contract) => AddNotifications(contract.Notifications);

        public void AddNotifications(IEnumerable<string> notifications) => AddNotifications(notifications.Select(n => new Notification(n)));

        public void AddNotifications(IEnumerable<Notification> notifications) => Notifications = Notifications.Concat(notifications);
    }
}
