using System;
using FluentValidation;
using InvestorStore.Core.Messages;

namespace InvestorStore.Sales.Application.Commands
{
    public class AddOrderItemCommand : Command
    {
        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public decimal Amount { get; private set; }

        public AddOrderItemCommand(Guid customerId, Guid productId, string name, int quantity, decimal amount)
        {
            CustomerId = customerId;
            ProductId = productId;
            Name = name;
            Quantity = quantity;
            Amount = amount;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddOrderItemValidation : AbstractValidator<AddOrderItemCommand>
    {
        public AddOrderItemValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Customer id is invalid!");
            
            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Product id is invalid!");
            
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name cannot be empty");
            
            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("The minimum amount of an item is 1");
            
            RuleFor(c => c.Quantity)
                .LessThan(15)
                .WithMessage("The maximum amount of an item is 15");
            
            RuleFor(c => c.Amount)
                .GreaterThan(0)
                .WithMessage("The item amount needs to be greater than 0");
        }
    }
}