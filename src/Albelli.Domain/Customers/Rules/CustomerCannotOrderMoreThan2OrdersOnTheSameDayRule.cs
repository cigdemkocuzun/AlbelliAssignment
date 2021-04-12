using System.Collections.Generic;
using System.Linq;
using Albelli.Domain.Customers.Orders;
using Albelli.Domain.Orders;
using Albelli.Domain.SeedWork;

namespace Albelli.Domain.Customers.Rules
{
    public class CustomerCannotOrderMoreThan2OrdersOnTheSameDayRule : IBusinessRule
    {
        private readonly IList<Order> _orders;

        public CustomerCannotOrderMoreThan2OrdersOnTheSameDayRule(IList<Order> orders)
        {
            _orders = orders;
        }

        public bool IsBroken()
        {
           return _orders.Count(x => x.IsOrderedToday()) >= 2;
        }

        public string Message => "You cannot order more than  orders on the same day.";
    }
}