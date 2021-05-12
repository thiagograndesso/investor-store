using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InvestorStore.Core.Communication.Mediator;
using InvestorStore.Core.Messages;
using InvestorStore.Core.Messages.CommonMessages.Notifications;
using InvestorStore.Sales.Application.Events;
using InvestorStore.Sales.Domain;
using MediatR;

namespace InvestorStore.Sales.Application.Commands
{
    public class OrderCommandHandler : 
        IRequestHandler<AddOrderItemCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public OrderCommandHandler(IOrderRepository orderRepository, IMediatorHandler mediatorHandler)
        {
            _orderRepository = orderRepository;
            _mediatorHandler = mediatorHandler;
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
                order.AddEvent(new DraftOrderCreatedEvent(command.CustomerId, order.Id));
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
                
                order.AddEvent(new OrderUpdatedEvent(order.CustomerId, order.Id, order.TotalAmount));
            }
            
            order.AddEvent(new OrderItemAddedEvent(order.CustomerId, order.Id, command.ProductId, command.Amount, command.Quantity));
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
                _mediatorHandler.PublishNotification(new DomainNotification(command.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}