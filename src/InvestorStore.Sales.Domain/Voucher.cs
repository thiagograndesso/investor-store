using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace InvestorStore.Sales.Domain
{
    public class Voucher
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
    }
}