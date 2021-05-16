using System;
using FluentValidation;
using InvestorStore.Core.Messages;

namespace InvestorStore.Sales.Application.Commands
{
    public class ApplyVoucherOrderCommand : Command
    {
        public Guid CustomerId { get; }
        public Guid OrderId { get; }
        public string VoucherCode { get; }

        public ApplyVoucherOrderCommand(Guid customerId, Guid orderId, string voucherCode)
        {
            CustomerId = customerId;
            OrderId = orderId;
            VoucherCode = voucherCode;
        }

        public override bool IsValid()
        {
            ValidationResult = new ApplyVoucherOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
    
    public class ApplyVoucherOrderValidation : AbstractValidator<ApplyVoucherOrderCommand>
    {
        public ApplyVoucherOrderValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("CustomerId is invalid");

            RuleFor(c => c.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("OrderId is invalid");

            RuleFor(c => c.VoucherCode)
                .NotEmpty()
                .WithMessage("Voucher code cannot be empty");
        }
    }
}