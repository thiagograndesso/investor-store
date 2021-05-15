using System;
using System.Collections.Generic;

namespace InvestorStore.Core.DomainObjects.Dtos
{
    public class OrderProducts
    {
        public Guid OrderId { get; set; }
        public ICollection<Item> Items { get; set; }
    }
    
    public class Item
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}