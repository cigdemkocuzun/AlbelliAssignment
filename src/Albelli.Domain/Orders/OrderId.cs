using System;
using Albelli.Domain.SeedWork;

namespace Albelli.Domain.Orders
{
    public class OrderId : TypedIdValueBase
    {
        public OrderId(Guid value) : base(value)
        {
        }
    }
}