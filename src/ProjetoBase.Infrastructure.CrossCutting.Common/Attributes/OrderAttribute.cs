using System;

namespace ProjetoBase.Infrastructure.CrossCutting.Common.Attributes
{
    public class OrderAttribute : Attribute
    {
        public OrderAttribute(int order) => Order = order;

        public int Order { get; private set; }
    }
}
