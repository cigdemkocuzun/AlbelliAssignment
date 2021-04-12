using System;
using Albelli.Domain.SeedWork;

namespace Albelli.Domain.Products
{
    public class ProductId : TypedIdValueBase
    {
        public ProductId(Guid value) : base(value)
        {
        }
    }
}