using System;
using InvestorStore.Core.DomainObjects.Dtos;

namespace InvestorStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderCreatedEvent : IntegrationEvent
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public decimal Total { get; }
        public OrderProducts OrderProducts { get; }
        public string CardName { get; }
        public string CardNumber { get; }
        public string CardExpiryDate { get; }
        public string CardCvvCode { get; }

        public OrderCreatedEvent(Guid orderId, Guid customerId, decimal total, OrderProducts orderProducts, string cardName, string cardNumber, string cardExpiryDate, string cardCvvCode)
        {
            OrderId = orderId;
            CustomerId = customerId;
            Total = total;
            OrderProducts = orderProducts;
            CardName = cardName;
            CardNumber = cardNumber;
            CardExpiryDate = cardExpiryDate;
            CardCvvCode = cardCvvCode;
        }
    }
}