using System;

namespace InvestorStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderPaymentConfirmedEvent : IntegrationEvent
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public Guid PaymentId { get; }
        public Guid TransactionId { get; }
        public decimal Total { get; }

        public OrderPaymentConfirmedEvent(Guid orderId, Guid customerId, Guid paymentId, Guid transactionId, decimal total)
        {
            OrderId = orderId;
            CustomerId = customerId;
            PaymentId = paymentId;
            TransactionId = transactionId;
            Total = total;
        }
    }
}