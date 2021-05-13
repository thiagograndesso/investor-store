using System;
using System.Collections.Generic;

namespace InvestorStore.Sales.Application.Queries.Dtos
{
    public class BasketDto
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Discount { get; set; }
        public string VoucherCode { get; set; }

        public List<BasketItemDto> Items { get; set; } = new();
        public BasketPaymentDto Payment { get; set; }
    }
}