using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestorStore.Sales.Application.Queries.Dtos;
using InvestorStore.Sales.Domain;

namespace InvestorStore.Sales.Application.Queries
{
    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderRepository _repository;

        public OrderQueries(IOrderRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<BasketDto> GetCustomerBasket(Guid customerId)
        {
            var order = await _repository.GetOrderDraftByCustomerId(customerId);
            if (order == null)
            {
                return null;
            }

            var basket = new BasketDto
            {
                CustomerId = order.CustomerId,
                TotalAmount = order.TotalAmount,
                OrderId = order.Id,
                Discount = order.Discount,
                SubTotal = order.Discount + order.TotalAmount
            };

            if (order.VoucherId != Guid.Empty)
            {
                basket.VoucherCode = order.Voucher.Code;
            }

            foreach (var item in order.OrderItems)
            {
                basket.Items.Add(new BasketItemDto
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Price = item.Amount,
                    TotalAmount = item.Amount * item.Quantity
                });
            }

            return basket;
        }

        public async Task<IEnumerable<OrderDto>> GetCustomerOrders(Guid customerId)
        {
            var orders = await _repository.GetListByCustomerId(customerId);

            orders = orders.Where(p => p.OrderStatus == OrderStatus.Paid || p.OrderStatus == OrderStatus.Cancelled)
                .OrderByDescending(p => p.Code);

            if (!orders.Any())
            {
                return null;
            }

            var orderDtos = new List<OrderDto>();

            foreach (var order in orders)
            {
                orderDtos.Add(new OrderDto
                {
                    Id = order.Id,
                    TotalAmount = order.TotalAmount,
                    OrderStatus = (int)order.OrderStatus,
                    Code = order.Code,
                    CreatedAt = order.CreatedAt
                });
            }

            return orderDtos;
        }
    }
}