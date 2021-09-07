using Microsoft.AspNetCore.Identity;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Constants;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Entities
{
    public class ApplicationUser : IdentityUser<Guid>, IApplicationEntity
    {
        protected ApplicationUser()
        {
        }

        public ApplicationUser(string name, string email, string phoneNumber)
            : base(email)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Active = true;
        }

        public ApplicationUser(string name, string email, string phoneNumber, bool administrator)
            : this(name, email, phoneNumber)
        {
            Administrator = administrator;
        }

        public string Name { get; private set; }
        public bool Administrator { get; private set; }
        public bool Active { get; private set; }

        private void ChangeUserName(string userName) => UserName = userName;

        public void Activate() => Active = true;
        public void Deactivate() => Active = false;

        public void ChangeName(string name) => Name = name;
        public void ChangeEmail(string email) => ChangeUserName(Email = email);
        public void ChangePhoneNumber(string phoneNumber) => PhoneNumber = phoneNumber;

        public NotificationResult Validate()
        {
            var result = new NotificationResult();

            result.AddNotifications(new NotificationContract()
                .IsntNullOrWhiteSpace(Name, ValidationMessages.UsuarioNomeDeveSerPreenchido)
                .HasMaxLength(Name, 250, ValidationMessages.UsuarioNomeDeveTerUmTamanhoMaximo)

                .IsntNullOrWhiteSpace(Email, ValidationMessages.UsuarioEmailDeveSerPreenchido)
                .HasMaxLength(Email, 250, ValidationMessages.UsuarioEmailDeveTerUmTamanhoMaximo)
                .IsEmail(Email, ValidationMessages.UsuarioEmailDeveSerValido)

                .IsntNullOrWhiteSpace(PhoneNumber, ValidationMessages.UsuarioCelularDeveSerPreenchido)
                .IsPhone(PhoneNumber, ValidationMessages.UsuarioCelularDeveSerValido));

            return result;
        }
    }
}
