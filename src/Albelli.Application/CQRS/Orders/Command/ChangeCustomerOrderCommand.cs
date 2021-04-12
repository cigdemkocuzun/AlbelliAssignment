using System;
using System.Collections.Generic;
using MediatR;
using Albelli.Application.Configuration.Commands;
using Albelli.Domain.Products;
using Albelli.Application.DTOs;
using Albelli.Domain.Customers.Orders;
using Albelli.Domain.ForeignExchange;
using Albelli.Application.Configuration.Data;
using System.Threading.Tasks;
using System.Threading;
using Albelli.Domain.Customers;
using System.Linq;
using Albelli.Application.CQRS.Products.Queries;
using Albelli.Domain.Orders;

namespace Albelli.Application.CQRS.Orders.Command
{
    public class ChangeCustomerOrderCommand : CommandBase<Unit>
    {
        public Guid CustomerId { get; }

        public Guid OrderId { get; }

        public string Currency { get; }

        public List<ProductDto> Products { get; }

        public ChangeCustomerOrderCommand(
            Guid customerId,
            Guid orderId,
            List<ProductDto> products,
            string currency)
        {
            this.CustomerId = customerId;
            this.OrderId = orderId;
            this.Currency = currency;
            this.Products = products;
        }
    }

    public class ChangeCustomerOrderCommandHandler : ICommandHandler<ChangeCustomerOrderCommand, Unit>
    {
        private readonly ICustomerRepository _customerRepository;

        private readonly IProductRepository _productRepository;

        private readonly IForeignExchange _foreignExchange;

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        internal ChangeCustomerOrderCommandHandler(
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IForeignExchange foreignExchange,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            this._customerRepository = customerRepository;
            this._productRepository = productRepository;
            _foreignExchange = foreignExchange;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Unit> Handle(ChangeCustomerOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await this._customerRepository.GetByIdAsync(new CustomerId(request.CustomerId));

            var orderId = new OrderId(request.OrderId);

            var conversionRates = this._foreignExchange.GetConversionRates();

            List<ProductId> productIdList = new List<ProductId>();
            foreach (var item in request.Products)
            {
                productIdList.Add(new ProductId(item.Id));
            }

            var products = await _productRepository.GetByIdsAsync(productIdList);

            var orderProducts = request
                    .Products
                    .Select(x => new OrderProductData(new ProductId(x.Id), x.Quantity, products.Where(i => i.Id == new ProductId(x.Id)).FirstOrDefault().PackageWidth, products.Where(i => i.Id == new ProductId(x.Id)).FirstOrDefault().ProductType))
                    .ToList();

            var allProductPrices =
                await ProductPriceProvider.GetAllProductPrices(_sqlConnectionFactory.GetOpenConnection());

            customer.ChangeOrder(
                orderId,
                allProductPrices,
                orderProducts,
                conversionRates,
                request.Currency);

            return Unit.Value;
        }
    }
}
