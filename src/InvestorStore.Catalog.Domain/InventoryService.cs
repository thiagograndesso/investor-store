using System;
using System.Threading.Tasks;

namespace InvestorStore.Catalog.Domain
{
    public class InventoryService : IInventoryService
    {
        private readonly IProductRepository _repository;

        public InventoryService(IProductRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<bool> DebitInventory(Guid productId, int amount)
        {
            var product = await _repository.GetById(productId);

            if (product is null)
            {
                return false;
            }

            if (!product.ContainsInventory(amount))
            {
                return false;
            }
            
            product.DebitInventory(amount);
            
            _repository.Update(product);
            return await _repository.UnitOfWork.Commit();
        }

        public async Task<bool> RefillInventory(Guid productId, int amount)
        {
            var product = await _repository.GetById(productId);

            if (product is null)
            {
                return false;
            }

            if (!product.ContainsInventory(amount))
            {
                return false;
            }
            
            product.RefillInventory(amount);
            
            _repository.Update(product);
            return await _repository.UnitOfWork.Commit();        
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}