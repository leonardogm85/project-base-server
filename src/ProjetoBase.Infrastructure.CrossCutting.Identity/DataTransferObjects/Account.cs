namespace ProjetoBase.Infrastructure.CrossCutting.Identity.DataTransferObjects
{
    public class Account : DataTransferObject
    {
        public Account(string concurrencyStamp, string name, string email, string phoneNumber)
        {
            ConcurrencyStamp = concurrencyStamp;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public string ConcurrencyStamp { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
    }
}
