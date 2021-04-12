using System.Collections.Generic;
using Albelli.Application.DTOs;
using Albelli.Application.Orders;

namespace Albelli.API.Orders
{
    public class CustomerOrderRequest
    {
        public List<ProductDto> Products { get; set; }

        public string Currency { get; set; }
    }
}