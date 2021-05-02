using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvestorStore.Core.Data;

namespace InvestorStore.Sales.Domain
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetById(Guid id);
        Task<IEnumerable<Order>> GetListByCustomerId(Guid customerId);
        Task<Order> GetOrderDraftByCustomerId(Guid customerId);
        void Add(Order order);
        void Update(Order order);

        Task<OrderItem> GetItemById(Guid id);
        Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId);
        void AddItem(OrderItem orderItem);
        void UpdateItem(OrderItem orderItem);
        void RemoveItem(OrderItem orderItem);

        Task<Voucher> GetVoucherByCode(string code);
    }
}