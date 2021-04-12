using Albelli.Domain.Products;

namespace Albelli.Domain.Orders
{
    public class OrderProductData
    {
        public OrderProductData(ProductId productId, int quantity, double packageWidth, ProductType productType)
        {
            ProductId = productId;
            Quantity = quantity;
            PackageWidth = packageWidth;
            ProductType = productType;

        }

        public ProductId ProductId { get; }

        public int Quantity { get; }

        public double PackageWidth { get; }

        public ProductType ProductType { get; }


    }
}