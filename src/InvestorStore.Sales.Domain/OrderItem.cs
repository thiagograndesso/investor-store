using System;
using System.ComponentModel.DataAnnotations.Schema;
using InvestorStore.Core.DomainObjects;

namespace InvestorStore.Sales.Domain
{
    public class OrderItem : Entity
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; }
        public string ProductName { get; }
        public int Quantity { get; private set; }
        public decimal Amount { get; }
        
        // EF relation
        public Order Order { get; set; }
        
        protected OrderItem() {}

        public OrderItem(Guid productId, string productName, int quantity, decimal amount)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            Amount = amount;
        }

        internal void AssociateOrder(Guid orderId)
        {
            OrderId = orderId;
        }

        public decimal CalculateAmount()
        {
            return Quantity * Amount;
        }

        internal void AddQuantity(int quantity)
        {
            Quantity += quantity;
        }

        internal void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}