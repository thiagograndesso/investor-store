using System;
using InvestorStore.Core.DomainObjects;
using InvestorStore.Core.Messages.CommonMessages.DomainEvents;

namespace InvestorStore.Catalog.Domain.Events
{
    public class ProductBelowInventoryEvent : DomainEvent
    {
        public int InventoryAmount { get; }
        
        public ProductBelowInventoryEvent(Guid aggregateId, int inventoryAmount) : base(aggregateId)
        {
            InventoryAmount = inventoryAmount;
        }
    }
}