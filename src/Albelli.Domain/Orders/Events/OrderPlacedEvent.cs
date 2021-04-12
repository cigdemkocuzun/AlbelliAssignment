using System;
using Albelli.Domain.Customers;
using Albelli.Domain.SeedWork;
using Albelli.Domain.SharedKernel;

namespace Albelli.Domain.Orders.Events
{
    public class OrderPlacedEvent : DomainEventBase
    {
        public OrderId OrderId { get; }

        public CustomerId CustomerId { get; }

        public MoneyValue Value { get; }

        public OrderPlacedEvent(
            OrderId orderId, 
            CustomerId customerId, 
            MoneyValue value)
        {
            this.OrderId = orderId;
            this.CustomerId = customerId;
            Value = value;
        }
    }
}