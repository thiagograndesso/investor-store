using System;
using System.Threading.Tasks;
using InvestorStore.Catalog.Domain.Events;
using InvestorStore.Core.Communication.Mediator;
using InvestorStore.Core.DomainObjects.Dtos;
using InvestorStore.Core.Messages.CommonMessages.Notifications;

namespace InvestorStore.Catalog.Domain
{
    public class InventoryService : IInventoryService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public InventoryService(IProductRepository productRepository, IMediatorHandler mediatorHandler)
        {
            _productRepository = productRepository;
            _mediatorHandler = mediatorHandler;
        }
        
        public async Task<bool> DebitInventory(Guid productId, int amount)
        {
            if (!await DebitInventoryItem(productId, amount))
            {
                return false;
            }
            
            return await _productRepository.UnitOfWork.Commit();
        }

        public async Task<bool> DebitOrderProducts(OrderProducts orderProducts)
        {
            foreach (var item in orderProducts.Items)
            {
                if (!await DebitInventoryItem(item.Id, item.Quantity))
                {
                    return false;
                }
            }

            return await _productRepository.UnitOfWork.Commit();
        }
        
        private async Task<bool> DebitInventoryItem(Guid productId, int amount)
        {
            var product = await _productRepository.GetById(productId);

            if (product is null)
            {
                return false;
            }

            if (!product.ContainsInventory(amount))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Inventory", $"Product - {product.Name} without inventory"));
                return false;
            }

            product.DebitInventory(amount);

            // TODO: configure the inventory amount through settings
            if (product.InventoryAmount < 10)
            {
                await _mediatorHandler.PublishDomainEvent(new ProductBelowInventoryEvent(product.Id, product.InventoryAmount));
            }

            _productRepository.Update(product);
            return true;
        }

        public async Task<bool> RefillInventory(Guid productId, int amount)
        {
            if (!await RefillInventoryItem(productId, amount))
            {
                return false;
            }
            
            return await _productRepository.UnitOfWork.Commit();
        }

        private async Task<bool> RefillInventoryItem(Guid productId, int amount)
        {
            var product = await _productRepository.GetById(productId);

            if (product is null)
            {
                return false;
            }

            if (!product.ContainsInventory(amount))
            {
                return false;
            }

            product.RefillInventory(amount);

            _productRepository.Update(product);
            return true;
        }

        public async Task<bool> RefillOrderProducts(OrderProducts orderProducts)
        {
            foreach (var item in orderProducts.Items)
            {
                if (!await RefillInventoryItem(item.Id, item.Quantity))
                {
                    return false;
                }
            }
            
            return await _productRepository.UnitOfWork.Commit();
        }

        public void Dispose()
        {
            _productRepository.Dispose();
        }
    }
}