using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects
{
    public class User : DataTransferObject
    {
        public User(Guid id, bool active, string concurrencyStamp, string name, string email, string phoneNumber)
        {
            Id = id;
            Active = active;
            ConcurrencyStamp = concurrencyStamp;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public User(Guid id, bool active, string concurrencyStamp, string name, string email, string phoneNumber, string password)
            : this(id, active, concurrencyStamp, name, email, phoneNumber)
        {
            Password = password;
        }

        public User(Guid id, bool active, string concurrencyStamp, string name, string email, string phoneNumber, bool administrator)
            : this(id, active, concurrencyStamp, name, email, phoneNumber)
        {
            Administrator = administrator;
        }

        public User(Guid id, bool active, string concurrencyStamp, string name, string email, string phoneNumber, string password, bool administrator)
            : this(id, active, concurrencyStamp, name, email, phoneNumber, password)
        {
            Administrator = administrator;
        }

        public Guid Id { get; private set; }
        public bool Active { get; private set; }
        public string ConcurrencyStamp { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool Administrator { get; private set; }
        public string Password { get; private set; }
    }
}
