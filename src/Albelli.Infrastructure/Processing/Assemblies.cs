using System.Reflection;
using Albelli.Application.CQRS.Orders.Command;

namespace Albelli.Infrastructure.Processing
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(PlaceCustomerOrderCommand).Assembly;
    }
}