using System;

namespace InvestorStore.Sales.Application.Queries.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public int OrderStatus { get; set; }
    }
}