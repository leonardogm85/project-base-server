using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Attributes
{
    public class AddressAttribute : Attribute
    {
        public AddressAttribute(string address) => Address = address;

        public string Address { get; private set; }
    }
}
