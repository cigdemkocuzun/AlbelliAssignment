using Albelli.Domain.Orders;
using Albelli.Domain.Products;
using Albelli.Domain.SharedKernel;
using Mapster;
using System;
using System.Collections.Generic;

namespace Albelli.Application.DTOs
{
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }

        public MoneyValue Value { get; set; }

        public string Currency { get; set; }

        public double RequiredBinWidth { get; set; }

        public List<OrderProductData> Products { get; set; }
    }

}