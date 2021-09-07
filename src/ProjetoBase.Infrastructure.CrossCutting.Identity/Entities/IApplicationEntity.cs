using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Entities
{
    public interface IApplicationEntity
    {
        bool Active { get; }

        void Activate();
        void Deactivate();

        NotificationResult Validate();
    }
}
