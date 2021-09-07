using Microsoft.AspNetCore.Identity;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Constants;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Contracts;
using ProjetoBase.Infrastructure.CrossCutting.Common.Validations.Notifications;
using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.Entities
{
    public class ApplicationRole : IdentityRole<Guid>, IApplicationEntity
    {
        protected ApplicationRole()
        {
        }

        public ApplicationRole(string name, string description)
            : base(name)
        {
            Description = description;
            Active = true;
        }

        public string Description { get; private set; }
        public bool Active { get; private set; }

        public void Activate() => Active = true;
        public void Deactivate() => Active = false;

        public void ChangeName(string name) => Name = name;
        public void ChangeDescription(string description) => Description = description;

        public NotificationResult Validate()
        {
            var result = new NotificationResult();

            result.AddNotifications(new NotificationContract()
                .IsntNullOrWhiteSpace(Name, ValidationMessages.PapelNomeDeveSerPreenchido)
                .HasMaxLength(Name, 250, ValidationMessages.PapelNomeDeveTerUmTamanhoMaximo)

                .IsntNullOrWhiteSpace(Description, ValidationMessages.PapelDescricaoDeveSerPreenchido)
                .HasMaxLength(Description, 900, ValidationMessages.PapelDescricaoDeveTerUmTamanhoMaximo));

            return result;
        }
    }
}
