using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvestorStore.Catalog.Application.Dtos;

namespace InvestorStore.Catalog.Application.Services
{
    public interface IProductAppService : IDisposable
    {
        Task<IEnumerable<ProductDto>> GetAll();
        Task<ProductDto> GetById(Guid id);
        Task<IEnumerable<ProductDto>> GetByCategory(int code);
        Task<IEnumerable<CategoryDto>> GetCategories();

        Task AddProduct(ProductDto product);
        Task UpdateProduct(ProductDto product);

        Task<ProductDto> DebitInventory(Guid id, int amount);
        Task<ProductDto> RefillInventory(Guid id, int amount);
    }
}