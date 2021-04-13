using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InvestorStore.Catalog.Application.Dtos;
using InvestorStore.Catalog.Domain;
using InvestorStore.Core.DomainObjects;

namespace InvestorStore.Catalog.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;

        public ProductAppService(IProductRepository productRepository, 
                                 IInventoryService inventoryService, 
                                 IMapper mapper)
        {
            _productRepository = productRepository;
            _inventoryService = inventoryService;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<ProductDto>> GetByCategory(int code)
        {
            return _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.GetByCategory(code));
        }

        public async Task<ProductDto> GetById(Guid id)
        {
            return _mapper.Map<ProductDto>(await _productRepository.GetById(id));
        }

        public async Task<IEnumerable<ProductDto>> GetAll()
        {
            return _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.GetAll());
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoryDto>>(await _productRepository.GetCategories());
        }

        public async Task AddProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _productRepository.Add(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public async Task UpdateProduct(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            _productRepository.Update(product);

            await _productRepository.UnitOfWork.Commit();
        }

        public async Task<ProductDto> DebitInventory(Guid id, int amount)
        {
            if (!await _inventoryService.DebitInventory(id, amount))
            {
                throw new DomainException("Failed to debit inventory");
            }

            return _mapper.Map<ProductDto>(await _productRepository.GetById(id));
        }

        public async Task<ProductDto> RefillInventory(Guid id, int amount)
        {
            if (!await _inventoryService.RefillInventory(id, amount))
            {
                throw new DomainException("Failed to refill inventory");
            }

            return _mapper.Map<ProductDto>(await _productRepository.GetById(id));
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
            _inventoryService?.Dispose();
        }
    }
}