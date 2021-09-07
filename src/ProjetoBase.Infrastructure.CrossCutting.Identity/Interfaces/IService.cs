using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects;
using System;
using System.Threading.Tasks;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Interfaces
{
    public interface IService<TDataTransferObject> : IDisposable
        where TDataTransferObject : DataTransferObject
    {
        Task<NotificationResult> AddAsync(TDataTransferObject dataTransferObject);
        Task<NotificationResult> UpdateAsync(TDataTransferObject dataTransferObject);
        Task<NotificationResult> RemoveAsync(Guid id);
        Task<NotificationResult> ActivateAsync(Guid id);
        Task<NotificationResult> DeactivateAsync(Guid id);
        Task<TDataTransferObject> GetByIdAsync(Guid id);
    }
}
