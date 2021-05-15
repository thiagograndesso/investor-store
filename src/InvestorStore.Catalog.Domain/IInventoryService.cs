using System;
using System.Threading.Tasks;
using InvestorStore.Core.DomainObjects.Dtos;

namespace InvestorStore.Catalog.Domain
{
    public interface IInventoryService : IDisposable
    {
        Task<bool> DebitInventory(Guid productId, int amount);
        Task<bool> DebitOrderProducts(OrderProducts orderProducts);
        Task<bool> RefillInventory(Guid productId, int amount);
        Task<bool> RefillOrderProducts(OrderProducts orderProducts);
    }
}