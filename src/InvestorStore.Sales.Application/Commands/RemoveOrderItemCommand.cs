using System;
using FluentValidation;
using InvestorStore.Core.Messages;

namespace InvestorStore.Sales.Application.Commands
{
    public class RemoveOrderItemCommand : Command
    {
        public Guid CustomerId { get; }
        public Guid ProductId { get; }
        public Guid OrderId { get; }

        public RemoveOrderItemCommand(Guid customerId, Guid productId, Guid orderId)
        {
            CustomerId = customerId;
            ProductId = productId;
            OrderId = orderId;
        }
        
        public override bool IsValid()
        {
            ValidationResult = new RemoveOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    
    public class RemoveOrderItemValidation : AbstractValidator<RemoveOrderItemCommand>
    {
        public RemoveOrderItemValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("CustomerId is invalid");

            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("ProductId is invalid");
        }
    }
}