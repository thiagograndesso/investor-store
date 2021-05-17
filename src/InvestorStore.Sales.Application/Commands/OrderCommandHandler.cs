using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InvestorStore.Core.Communication.Mediator;
using InvestorStore.Core.DomainObjects.Dtos;
using InvestorStore.Core.Extensions;
using InvestorStore.Core.Messages;
using InvestorStore.Core.Messages.CommonMessages.IntegrationEvents;
using InvestorStore.Core.Messages.CommonMessages.Notifications;
using InvestorStore.Sales.Application.Events;
using InvestorStore.Sales.Domain;
using MediatR;

namespace InvestorStore.Sales.Application.Commands
{
    public class OrderCommandHandler : 
        IRequestHandler<AddOrderItemCommand, bool>,
        IRequestHandler<UpdateOrderItemCommand, bool>,
        IRequestHandler<RemoveOrderItemCommand, bool>,
        IRequestHandler<ApplyVoucherOrderCommand, bool>,
        IRequestHandler<OpenOrderCommand, bool>,
        IRequestHandler<CompleteOrderCommand, bool>,
        IRequestHandler<CancelOrderAndRefillInventoryCommand, bool>,
        IRequestHandler<CancelOrderCommand, bool>
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

        public async Task<bool> Handle(UpdateOrderItemCommand command, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(command)) return false;

            var order = await _orderRepository.GetOrderDraftByCustomerId(command.CustomerId);
            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Order", "Order not found!"));
                return false;
            }

            var orderItem = await _orderRepository.GetItemByOrder(order.Id, command.ProductId);
            if (!order.OrderItemExists(orderItem))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Order", "Order item not found!"));
                return false;
            }

            order.UpdateQuantity(orderItem, command.Quantity);
            order.AddEvent(new ProductUpdatedOrderEvent(command.CustomerId, order.Id, command.ProductId, command.Quantity));

            _orderRepository.UpdateItem(orderItem);
            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(RemoveOrderItemCommand command, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(command))
            {
                return false;
            }

            var order = await _orderRepository.GetOrderDraftByCustomerId(command.CustomerId);
            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Order", "Order not found!"));
                return false;
            }

            var orderItem = await _orderRepository.GetItemByOrder(order.Id, command.ProductId);
            if (orderItem != null && !order.OrderItemExists(orderItem))
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Order", "Order item not found!"));
                return false;
            }

            order.RemoveOrderItem(orderItem);
            order.AddEvent(new ProductRemovedOrderEvent(command.CustomerId, order.Id, command.ProductId));

            _orderRepository.RemoveItem(orderItem);
            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(ApplyVoucherOrderCommand command, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(command))
            {
                return false;
            }

            var order = await _orderRepository.GetOrderDraftByCustomerId(command.CustomerId);
            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Order", "Order not found!"));
                return false;
            }

            var voucher = await _orderRepository.GetVoucherByCode(command.VoucherCode);
            if (voucher == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Order", "Voucher not found!"));
                return false;
            }

            var voucherValidation = order.ApplyVoucher(voucher);
            if (!voucherValidation.IsValid)
            {
                foreach (var error in voucherValidation.Errors)
                {
                    await _mediatorHandler.PublishNotification(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return false;
            }

            order.AddEvent(new VoucherAppliedOrderEvent(command.CustomerId, order.Id, voucher.Id));

            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(OpenOrderCommand command, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(command))
            {
                return false;
            }

            var order = await _orderRepository.GetOrderDraftByCustomerId(command.CustomerId);
            order.OpenOrder();

            var items = new List<Item>();
            order.OrderItems.ForEach(i => items.Add(new Item { Id = i.ProductId, Quantity = i.Quantity }));
            var orderProducts = new OrderProducts { OrderId = order.Id, Items = items };

            order.AddEvent(new OrderCreatedEvent(order.Id, order.CustomerId, order.TotalAmount, orderProducts,  command.CardName, command.CardNumber, command.CardExpiryDate, command.CardCvvCode));

            _orderRepository.Update(order);
            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(CompleteOrderCommand command, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(command))
            {
                return false;
            }
            
            var order = await _orderRepository.GetById(command.OrderId);
            if (order is null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Order","Order has not been found!"));
                return false;
            }
            
            order.CompleteOrder();
            
            order.AddEvent(new OrderCompletedEvent(command.OrderId));
            return await _orderRepository.UnitOfWork.Commit();
        }

        public async Task<bool> Handle(CancelOrderAndRefillInventoryCommand command, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(command))
            {
                return false;
            }
            
            var order = await _orderRepository.GetById(command.OrderId);
            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Order", "Order has not been found!"));
                return false;
            }

            var items = new List<Item>();
            order.OrderItems.ForEach(i => items.Add(new Item { Id = i.ProductId, Quantity = i.Quantity }));
            var orderProducts = new OrderProducts { OrderId = order.Id, Items = items };

            order.AddEvent(new OrderCancelledEvent(order.Id, order.CustomerId, orderProducts));
            order.ToDraft();

            return await _orderRepository.UnitOfWork.Commit();
        }
        
        public async Task<bool> Handle(CancelOrderCommand command, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(command))
            {
                return false;
            }
            
            var order = await _orderRepository.GetById(command.OrderId);
            if (order == null)
            {
                await _mediatorHandler.PublishNotification(new DomainNotification("Order", "Order has not been found!"));
                return false;
            }
            
            order.ToDraft();

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