using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using FluentValidation;
using FluentValidation.Results;
using InvestorStore.Core.DomainObjects;

namespace InvestorStore.Sales.Domain
{
    public class Voucher : Entity
    {
        public string Code { get; set; }
        public decimal? Percentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public int Quantity { get; set; }
        public VoucherType VoucherType { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? UsedAt { get; set; }
        public DateTimeOffset DueDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsUsed { get; set; }
        
        // EF relation
        public ICollection<Order> Orders { get; set; }
        
        internal ValidationResult ValidateIfApplicable()
        {
            return new VoucherApplicableValidation().Validate(this);
        }
    }
    
    public class VoucherApplicableValidation : AbstractValidator<Voucher>
    {

        public VoucherApplicableValidation()
        {
            RuleFor(c => c.DueDate)
                .Must(DueDateGreaterThanNow)
                .WithMessage("This voucher is expired");

            RuleFor(c => c.IsActive)
                .Equal(true)
                .WithMessage("This voucher is not valid anymore");

            RuleFor(c => c.IsUsed)
                .Equal(false)
                .WithMessage("This voucher has been used already");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("This voucher is not available anymore");
        }

        protected static bool DueDateGreaterThanNow(DateTimeOffset dueDate)
        {
            return dueDate >= DateTime.Now;
        }
    }
}