using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InvestorStore.Sales.Application.Queries.Dtos;

namespace InvestorStore.Sales.Application.Queries
{
    public interface IOrderQueries
    {
        Task<BasketDto> GetCustomerBasket(Guid customerId);
        Task<IEnumerable<OrderDto>> GetCustomerOrders(Guid customerId);   
    }
}