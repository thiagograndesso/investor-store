using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InvestorStore.Core.Messages;
using InvestorStore.Sales.Domain;
using MediatR;

namespace InvestorStore.Sales.Application.Commands
{
    public class OrderCommandHandler : 
        IRequestHandler<AddOrderItemCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;

        public OrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        
        public async Task<bool> Handle(AddOrderItemCommand command, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(command))
            {
                return false;
            }

            var order = await _orderRepository.GetOrderDraftByCustomerId(command.CustomerId);
            var item = new OrderItem(command.ProductId, command.Name, command.Quantity, command.Amount);

            if (order is null)
            {
                order = Order.OrderFactory.NewDraftOrder(command.CustomerId);
                order.AddOrderItem(item);

                _orderRepository.Add(order);
            }
            else
            {
                var itemExists = order.OrderItemExists(item);
                order.AddOrderItem(item);

                if (itemExists)
                {
                    _orderRepository.UpdateItem(order.OrderItems.FirstOrDefault(o => o.ProductId == item.ProductId));
                }
                else
                {
                    _orderRepository.AddItem(item);
                }
            }
            
            return await _orderRepository.UnitOfWork.Commit();
        }

        private bool ValidateCommand(Command command)
        {
            if (command.IsValid())
            {
                return true;
            }

            foreach (var error in command.ValidationResult.Errors)
            {
                // Throw event error
            }

            return false;
        }
    }
}