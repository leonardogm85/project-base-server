using Microsoft.EntityFrameworkCore;
using ProjetoBase.Domain.Interfaces.UnitOfWork;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Constants;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using ProjetoBase.Infrastructure.Data.Context;
using System;

namespace ProjetoBase.Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjetoBaseContext _context;

        public UnitOfWork(ProjetoBaseContext context) => _context = context;

        public NotificationResult Commit()
        {
            var result = new NotificationResult();

            try
            {
                var committed = _context.SaveChanges();

                result.AddNotifications(new NotificationContract().IsGreaterThan(committed, 0, ValidationMessages.RegistroFalhouAoExecutarComando));
            }
            catch (DbUpdateConcurrencyException)
            {
                result.AddNotification(ValidationMessages.RegistroConflitouAoExecutarComando);
            }
            catch (DbUpdateException)
            {
                result.AddNotification(ValidationMessages.RegistroFalhouAoExecutarComando);
            }

            return result;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
