using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects
{
    public class UserTable : DataTransferObject
    {
        public UserTable(Guid id, string name, string email, string phoneNumber, bool administrator, bool emailConfirmed, bool active)
        {
            Id = id;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Administrator = administrator;
            EmailConfirmed = emailConfirmed;
            Active = active;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool Administrator { get; private set; }
        public bool EmailConfirmed { get; private set; }
        public bool Active { get; private set; }
    }
}
