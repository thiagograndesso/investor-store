using System;

namespace InvestorStore.Core.DomainObjects.Dtos
{
    public class OrderPayment
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Total { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CvvCode { get; set; }
    }
}