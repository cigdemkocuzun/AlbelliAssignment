using Albelli.Domain.Orders;
using FluentValidation;
using System;

namespace Albelli.Application.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public int Quantity { get; set; }


        public ProductDto()
        {

        }

        public ProductDto(Guid id, int quantity)
        {
            this.Id = id;
            this.Quantity = quantity;
        }
    }

    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            this.RuleFor(x => x.Quantity).GreaterThan(0)
                .WithMessage("At least one product has invalid quantity");
        }
    }
}