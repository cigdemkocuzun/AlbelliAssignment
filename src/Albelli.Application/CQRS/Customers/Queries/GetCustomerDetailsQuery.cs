using System;
using System.Threading;
using System.Threading.Tasks;
using Albelli.Application.Configuration.Data;
using Albelli.Application.Configuration.Queries;
using Albelli.Application.DTOs;
using Dapper;

namespace Albelli.Application.CQRS.Customers.GetCustomerDetails
{
    public class GetCustomerDetailsQuery : IQuery<CustomerDetailsDto>
    {
        public GetCustomerDetailsQuery(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; }
    }

    public class GetCustomerDetailsQueryHandler : IQueryHandler<GetCustomerDetailsQuery, CustomerDetailsDto>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetCustomerDetailsQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public Task<CustomerDetailsDto> Handle(GetCustomerDetailsQuery request, CancellationToken cancellationToken)
        {
            const string sql = "SELECT " +
                               "[Customer].[Id], " +
                               "[Customer].[Name], " +
                               "[Customer].[Email], " +
                               "[Customer].[WelcomeEmailWasSent] " +
                               "FROM orders.Customers AS [Customer] " +
                               "WHERE [Customer].[Id] = @CustomerId ";

            var connection = _sqlConnectionFactory.GetOpenConnection();

            return connection.QuerySingleAsync<CustomerDetailsDto>(sql, new
            {
                request.CustomerId
            });
        }
    }
}