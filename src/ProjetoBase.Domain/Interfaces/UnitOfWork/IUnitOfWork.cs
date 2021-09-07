using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;

namespace ProjetoBase.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        NotificationResult Commit();
    }
}
