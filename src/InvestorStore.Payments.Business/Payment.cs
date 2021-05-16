using System;
using InvestorStore.Core.DomainObjects;

namespace InvestorStore.Payments.Business
{
    public class Payment : Entity, IAggregateRoot
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }

        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public string CvvCode { get; set; }

        // EF. Rel.
        public Transaction Transaction { get; set; }
    }
}
