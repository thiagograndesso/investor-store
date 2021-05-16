using System;
using InvestorStore.Core.DomainObjects;

namespace InvestorStore.Payments.Business
{
    public class Transaction : Entity
    {
        public Guid OrderId { get; set; }
        public Guid PaymentId { get; set; }
        public decimal Total { get; set; }
        public TransactionStatus TransactionStatus { get; set; }

        // EF. Rel.
        public Payment Payment { get; set; }
    }
}