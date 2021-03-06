using System;
using InvestorStore.Core.DomainObjects.Dtos;

namespace InvestorStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderInventoryConfirmedEvent : IntegrationEvent
    {
        public Guid OrderId { get; }
        public Guid CustomerId { get; }
        public decimal Total { get; }
        public OrderProducts OrderProducts { get; }
        public string CardName { get; }
        public string CardNumber { get; }
        public string ExpiryDate { get; }
        public string CvvCode { get; }

        public OrderInventoryConfirmedEvent(Guid orderId, Guid customerId, decimal total, OrderProducts orderProducts, string cardName, string cardNumber, string expiryDate, string cvvCode)
        {
            OrderId = orderId;
            CustomerId = customerId;
            Total = total;
            OrderProducts = orderProducts;
            CardName = cardName;
            CardNumber = cardNumber;
            ExpiryDate = expiryDate;
            CvvCode = cvvCode;
        }
    }
}