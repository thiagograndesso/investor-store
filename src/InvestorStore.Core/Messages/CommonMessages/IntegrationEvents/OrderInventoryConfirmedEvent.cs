using System;
using InvestorStore.Core.DomainObjects.Dtos;

namespace InvestorStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderInventoryConfirmedEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal Total { get; private set; }
        public OrderProducts OrderProducts { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string CardExpiryDate { get; private set; }
        public string CardCvvCode { get; private set; }

        public OrderInventoryConfirmedEvent(Guid orderId, Guid customerId, decimal total, OrderProducts orderProducts, string cardName, string cardNumber, string cardExpiryDate, string cardCvvCode)
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