using Albelli.Domain.Orders;
using FluentValidation;
using Mapster;
using System;

namespace Albelli.Application.DTOs
{
    public class OrderProductDto
    {
        public Guid ProductId { get; set; }

        public ProductType ProductType { get; set; }
     
        public double PackageWidth { get; set; }

        public int Quantity { get; set; }

        public decimal Value { get; set; }

        public string Currency { get; set; }

    }

}