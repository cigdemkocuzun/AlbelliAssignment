using System;
using System.Collections.Generic;
using System.Linq;
using Albelli.Domain.Orders;
using Albelli.Domain.SeedWork;
using Albelli.Domain.SharedKernel;

namespace Albelli.Domain.Products
{
    public class Product : Entity, IAggregateRoot
    {
        public ProductId Id { get; private set; }

        public string Name { get; private set; }

        public ProductType ProductType { get; private set; }

        public double PackageWidth { get; private set; }

        private List<ProductPrice> _prices;

        private Product()
        {

        }
    }
}