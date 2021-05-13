using InvestorStore.Catalog.Application.Services;
using InvestorStore.Catalog.Data;
using InvestorStore.Catalog.Data.Repositories;
using InvestorStore.Catalog.Domain;
using InvestorStore.Catalog.Domain.Events;
using InvestorStore.Core.Communication.Mediator;
using InvestorStore.Core.Messages.CommonMessages.Notifications;
using InvestorStore.Sales.Application.Commands;
using InvestorStore.Sales.Application.Events;
using InvestorStore.Sales.Application.Queries;
using InvestorStore.Sales.Data;
using InvestorStore.Sales.Data.Repository;
using InvestorStore.Sales.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace InvestorStore.WebApp.MVC.Config
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Mediator
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            
            // Notifications
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            
            // Catalog Domain
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<CatalogContext>();
            
            // Events
            services.AddScoped<INotificationHandler<ProductBelowInventoryEvent>, ProductEventHandler>();
            services.AddScoped<INotificationHandler<DraftOrderCreatedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderUpdatedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderItemAddedEvent>, OrderEventHandler>();

            // Sales Domain
            services.AddScoped<IRequestHandler<AddOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddScoped<SalesContext>();

        }
    }
}