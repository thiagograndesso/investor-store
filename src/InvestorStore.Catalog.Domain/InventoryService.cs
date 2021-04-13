using System;
using System.Threading.Tasks;
using InvestorStore.Catalog.Domain.Events;
using InvestorStore.Core.Bus;

namespace InvestorStore.Catalog.Domain
{
    public class InventoryService : IInventoryService
    {
        private readonly IProductRepository _repository;
        private readonly IMediatrHandler _bus;

        public InventoryService(IProductRepository repository, IMediatrHandler bus)
        {
            _repository = repository;
            _bus = bus;
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

            // TODO: configure the inventory amount through settings
            if (product.InventoryAmount < 10)
            {
                await _bus.PublishEvent(new ProductBelowInventoryEvent(product.Id, product.InventoryAmount));
            }
            
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