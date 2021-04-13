using System;
using System.Threading.Tasks;

namespace InvestorStore.Catalog.Domain
{
    public interface IInventoryService : IDisposable
    {
        Task<bool> DebitInventory(Guid productId, int amount);
        Task<bool> RefillInventory(Guid productId, int amount);
    }
}