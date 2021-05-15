using System;
using FluentValidation;
using InvestorStore.Core.Messages;

namespace InvestorStore.Sales.Application.Commands
{
    public class OpenOrderCommand : Command
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal Total { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string CardExpiryDate { get; private set; }
        public string CardCvvCode { get; private set; }

        public OpenOrderCommand(Guid orderId, Guid customerId, decimal total, string cardName, string cardNumber, string cardExpiryDate, string cardCvvCode)
        {
            OrderId = orderId;
            CustomerId = customerId;
            Total = total;
            CardName = cardName;
            CardNumber = cardNumber;
            CardExpiryDate = cardExpiryDate;
            CardCvvCode = cardCvvCode;
        }
        
        public override bool IsValid()
        {
            ValidationResult = new OpenOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    
    public class OpenOrderValidation : AbstractValidator<OpenOrderCommand>
    {
        public OpenOrderValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid customer id");

            RuleFor(c => c.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid order id");

            RuleFor(c => c.CardName)
                .NotEmpty()
                .WithMessage("Card name has not been provided");

            RuleFor(c => c.CardNumber)
                .CreditCard()
                .WithMessage("Invalid card number");

            RuleFor(c => c.CardExpiryDate)
                .NotEmpty()
                .WithMessage("Expiry date has not been provided");

            RuleFor(c => c.CardCvvCode)
                .Length(3, 4)
                .WithMessage("CVV code is in incorrect format");
        }
    }
}