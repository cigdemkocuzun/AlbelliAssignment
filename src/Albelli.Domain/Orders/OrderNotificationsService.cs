namespace Albelli.Domain.Orders
{
    public static class OrderNotificationsService
    {
        public static string GetOrderEmailConfirmationDescription(OrderId orderId)
        {
            return $"Order number: {orderId.Value} placed";
        }
    }
}