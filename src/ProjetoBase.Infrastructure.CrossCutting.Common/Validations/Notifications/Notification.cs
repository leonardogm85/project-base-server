namespace ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications
{
    public class Notification
    {
        public Notification(string message) => Message = message;

        public string Message { get; private set; }
    }
}
