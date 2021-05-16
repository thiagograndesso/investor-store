using System;
using FluentValidation;
using InvestorStore.Core.Messages;

namespace InvestorStore.Sales.Application.Commands
{
    public class UpdateOrderItemCommand : Command
    {
        public Guid CustomerId { get; }
        public Guid ProductId { get; }
        public Guid OrderId { get; }
        public int Quantity { get; }

        public UpdateOrderItemCommand(Guid customerId, Guid productId, Guid orderId, int quantity)
        {
            CustomerId = customerId;
            ProductId = productId;
            OrderId = orderId;
            Quantity = quantity;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class UpdateOrderItemValidation : AbstractValidator<UpdateOrderItemCommand>
    {
        public UpdateOrderItemValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("CustomerId is invalid");

            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("ProductId is invalid");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Minimum quantity is 1");

            RuleFor(c => c.Quantity)
                .LessThan(15)
                .WithMessage("Maximum quantity is 15");
        }
    }
}