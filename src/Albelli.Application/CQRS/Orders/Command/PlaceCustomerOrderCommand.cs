using System;
using System.Collections.Generic;
using MediatR;
using Albelli.Application.Configuration.Commands;
using Albelli.Application.DTOs;
using Albelli.Domain.Customers.Orders;
using Albelli.Application.Configuration.Data;
using Albelli.Domain.ForeignExchange;
using System.Threading;
using Albelli.Domain.Customers;
using System.Threading.Tasks;
using System.Linq;
using Albelli.Domain.Products;
using FluentValidation;
using Albelli.Application.CQRS.Products.Queries;
using Albelli.Domain.Orders;

namespace Albelli.Application.CQRS.Orders.Command
{
    public class PlaceCustomerOrderCommand : CommandBase<OrderDetailsDto>
    {
        public Guid CustomerId { get; }

        public List<ProductDto> Products { get; }

        public string Currency { get; }

        public PlaceCustomerOrderCommand(
            Guid customerId,
            List<ProductDto> products,
            string currency)
        {
            this.CustomerId = customerId;
            this.Products = products;
            this.Currency = currency;
        }
    }

    public class PlaceCustomerOrderCommandValidator : AbstractValidator<PlaceCustomerOrderCommand>
    {
        public PlaceCustomerOrderCommandValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is empty");
            RuleFor(x => x.Products).NotEmpty().WithMessage("Products list is empty");
            RuleForEach(x => x.Products).SetValidator(new ProductDtoValidator());

            this.RuleFor(x => x.Currency).Must(x => x == "USD" || x == "EUR")
                .WithMessage("At least one product has invalid currency.it must be USD or EUR");
        }
    }

    public class PlaceCustomerOrderCommandHandler : ICommandHandler<PlaceCustomerOrderCommand, OrderDetailsDto>
    {
        private readonly ICustomerRepository _customerRepository;

        private readonly IProductRepository _productRepository;

        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        private readonly IForeignExchange _foreignExchange;

        public PlaceCustomerOrderCommandHandler(
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            IForeignExchange foreignExchange,
            ISqlConnectionFactory sqlConnectionFactory)
        {
            this._customerRepository = customerRepository;
            this._productRepository = productRepository;
            this._foreignExchange = foreignExchange;
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<OrderDetailsDto> Handle(PlaceCustomerOrderCommand command, CancellationToken cancellationToken)
        {
            var customer = await this._customerRepository.GetByIdAsync(new CustomerId(command.CustomerId));

            List<ProductId> productIdList = new List<ProductId>();
            foreach (var item in command.Products)
            {
                productIdList.Add(new ProductId(item.Id));
            }

            var products = await _productRepository.GetByIdsAsync(productIdList);

            var allProductPrices = await ProductPriceProvider.GetAllProductPrices(_sqlConnectionFactory.GetOpenConnection());

            var conversionRates = this._foreignExchange.GetConversionRates();

            var orderProductsData = command
                .Products
                .Select(x => new OrderProductData(new ProductId(x.Id), x.Quantity, products.Where(i=> i.Id == new ProductId(x.Id)).FirstOrDefault().PackageWidth, products.Where(i => i.Id == new ProductId(x.Id)).FirstOrDefault().ProductType))
                .ToList();

            Order order = customer.PlaceOrder(
                orderProductsData,
                allProductPrices,
                command.Currency,
                conversionRates);

            var OrderDetailsDto = new OrderDetailsDto { Id = order.Id.Value, Currency = command.Currency, Products = orderProductsData, Value = order._value, RequiredBinWidth = order._requiredBinWidth };

            return OrderDetailsDto;
        }
    }
}