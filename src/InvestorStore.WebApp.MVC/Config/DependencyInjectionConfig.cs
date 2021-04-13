using InvestorStore.Catalog.Application.Services;
using InvestorStore.Catalog.Data;
using InvestorStore.Catalog.Data.Repositories;
using InvestorStore.Catalog.Domain;
using InvestorStore.Core.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace InvestorStore.WebApp.MVC.Config
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatrHandler, MediatrHandler>();
            
            // Catalog Domain
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<CatalogContext>();
        }
    }
}