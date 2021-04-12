using System;
using MediatR;
using Albelli.Application.Configuration.Queries;
using Albelli.Application.DTOs;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

using Albelli.Application.Configuration.Data;
using Albelli.Domain.Orders;

namespace Albelli.Application.Orders.Queries
{
    public class GetCustomerOrderDetailsQuery : IQuery<OrderDetailsDto>
    {
        public Guid OrderId { get; }

        public GetCustomerOrderDetailsQuery(Guid orderId)
        {
            this.OrderId = orderId;
        }
    }

    public class GetCustomerOrderDetailsQueryHandler : IQueryHandler<GetCustomerOrderDetailsQuery, OrderDetailsDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal GetCustomerOrderDetailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            this._sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<OrderDetailsDto> Handle(GetCustomerOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var connection = this._sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT " +
                               "[Order].[Id], " +
                               "[Order].[IsRemoved], " +
                               "[Order].[Value], " +
                               "[Order].[Currency] " +
                               "FROM orders.Orders AS [Order] " +
                               "WHERE [Order].Id = @OrderId";
            var order = await connection.QuerySingleOrDefaultAsync<OrderDetailsDto>(sql, new { request.OrderId });

            const string sqlProducts = "SELECT " +
                               "[Order].[ProductId] AS [Id], " +
                               "[Order].[Quantity], " +
                               "[Order].[Name], " +
                               "[Order].[Value], " +
                               "[Order].[Currency] " +
                               "FROM orders.OrderProducts AS [Order] " +
                               "INNER JOIN orders.Products AS [Product] ON [Order].ProductId = [Product].[Id] " +
                               "WHERE [Order].OrderId = @OrderId";

            var products = await connection.QueryAsync<OrderProductData>(sqlProducts, new { request.OrderId });

           order.Products = products.AsList();

            return order;
        }
    }
}