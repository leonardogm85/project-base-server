using System.Threading.Tasks;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
