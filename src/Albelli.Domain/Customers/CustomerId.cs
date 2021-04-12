using System;
using Albelli.Domain.SeedWork;

namespace Albelli.Domain.Customers
{
    public class CustomerId : TypedIdValueBase
    {
        public CustomerId(Guid value) : base(value)
        {
        }
    }
}