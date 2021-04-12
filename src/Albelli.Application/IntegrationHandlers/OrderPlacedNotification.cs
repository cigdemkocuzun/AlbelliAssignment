using Newtonsoft.Json;
using Albelli.Application.Configuration.DomainEvents;
using Albelli.Domain.Customers;
using Albelli.Application.Configuration.Emails;
using System.Threading;
using System.Threading.Tasks;
using Albelli.Application.Configuration.Data;
using MediatR;
using Dapper;
using Albelli.Domain.Orders.Events;
using Albelli.Domain.Orders;

namespace Albelli.Application.IntegrationHandlers
{
    public class OrderPlacedNotification : DomainNotificationBase<OrderPlacedEvent>
    {
        public OrderId OrderId { get; }
        public CustomerId CustomerId { get; }

        public OrderPlacedNotification(OrderPlacedEvent domainEvent) : base(domainEvent)
        {
            this.OrderId = domainEvent.OrderId;
            this.CustomerId = domainEvent.CustomerId;
        }

        [JsonConstructor]
        public OrderPlacedNotification(OrderId orderId, CustomerId customerId) : base(null)
        {
            this.OrderId = orderId;
            this.CustomerId = customerId;
        }
    }

    public class OrderPlacedNotificationHandler : INotificationHandler<OrderPlacedNotification>
    {
        private readonly IEmailSender _emailSender;
        private readonly EmailsSettings _emailsSettings;
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public OrderPlacedNotificationHandler(
            IEmailSender emailSender,
            EmailsSettings emailsSettings,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _emailSender = emailSender;
            _emailsSettings = emailsSettings;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task Handle(OrderPlacedNotification request, CancellationToken cancellationToken)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();

            const string sql = "SELECT [Customer].[Email] " +
                               "FROM [orders].[Customers] AS [Customer] " +
                               "WHERE [Customer].[Id] = @Id";

            var customerEmail = await connection.QueryFirstAsync<string>(sql,
                new
                {
                    Id = request.CustomerId.Value
                });

            var emailMessage = new EmailMessage(
                _emailsSettings.FromAddressEmail,
                customerEmail,
                OrderNotificationsService.GetOrderEmailConfirmationDescription(request.OrderId));

            await _emailSender.SendEmailAsync(emailMessage);
        }
    }
}