using System;
using System.Collections.Generic;
using System.Linq;
using InvestorStore.Core.DomainObjects;

namespace InvestorStore.Sales.Domain
{
    public class Order : Entity, IAggregateRoot
    {
        public int Code { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid VoucherId { get; private set; }
        public bool IsVoucherUsed { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalAmount { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public OrderStatus OrderStatus { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        // EF relation
        public virtual Voucher Voucher { get; set; }

        protected Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public Order(Guid customerId, bool isVoucherUsed, decimal discount, decimal totalAmount)
        {
            CustomerId = customerId;
            IsVoucherUsed = isVoucherUsed;
            Discount = discount;
            TotalAmount = totalAmount;
            _orderItems = new List<OrderItem>();
        }

        public void ApplyVoucher(Voucher voucher)
        {
            Voucher = voucher;
            IsVoucherUsed = true;
            CalculateOrderAmount();
        }

        public void CalculateOrderAmount()
        {
            TotalAmount = OrderItems.Sum(o => o.CalculateAmount());
            CalculateTotalAmountDiscount();
        }

        public void CalculateTotalAmountDiscount()
        {
            if (!IsVoucherUsed)
            {
                return;
            }

            decimal discount = 0;
            var amount = TotalAmount;

            if (Voucher.VoucherType == VoucherType.Percentage)
            {
                if (Voucher.Percentage.HasValue)
                {
                    discount = (amount * Voucher.Percentage.Value) / 100;
                    amount -= discount;
                }
            }
            else
            {
                if (Voucher.DiscountAmount.HasValue)
                {
                    discount = Voucher.DiscountAmount.Value;
                    amount -= discount;
                }
            }

            TotalAmount = amount < 0 ? 0 : amount;
            Discount = discount;
        }

        public bool OrderItemExists(OrderItem item)
        {
            return _orderItems.Any(o => o.ProductId == item.ProductId);
        }

        public void AddOrderItem(OrderItem item)
        {
            if (!item.IsValid())
            {
                return;
            }
            
            item.AssociateOrder(Id);

            if (OrderItemExists(item))
            {
                var existingItem = _orderItems.FirstOrDefault(o => o.ProductId == item.ProductId);
                existingItem?.AddQuantity(item.Quantity);
                item = existingItem;

                _orderItems.Remove(existingItem);
            }

            item?.CalculateAmount();
            _orderItems.Add(item);
            
            CalculateOrderAmount();
        }

        public void RemoveOrderItem(OrderItem item)
        {
            if (!item.IsValid())
            {
                return;
            }

            var existingItem = _orderItems.FirstOrDefault(o => o.ProductId == item.ProductId);
            if (existingItem is null)
            {
                throw new DomainException("The item does not belong to the order!");
            }

            _orderItems.Remove(existingItem);
            
            CalculateOrderAmount();
        }
        
        public void UpdateOrderItem(OrderItem item)
        {
            if (!item.IsValid())
            {
                return;
            }
            
            item.AssociateOrder(Id);

            var existingItem = _orderItems.FirstOrDefault(o => o.ProductId == item.ProductId);
            if (existingItem is null)
            {
                throw new DomainException("The item does not belong to the order!");
            }

            _orderItems.Remove(existingItem);
            _orderItems.Add(item);
            
            CalculateOrderAmount();
        }

        public void UpdateQuantity(OrderItem item, int quantity)
        {
            item.UpdateQuantity(quantity);
            UpdateOrderItem(item);
        }

        public void ToDraft()
        {
            OrderStatus = OrderStatus.Draft;
        }
        
        public void OpenOrder()
        {
            OrderStatus = OrderStatus.Opened;
        }
        
        public void CompleteOrder()
        {
            OrderStatus = OrderStatus.Paid;
        }
        
        public void CancelOrder()
        {
            OrderStatus = OrderStatus.Canceled;
        }
        
        public static class OrderFactory
        {
            public static Order NewDraftOrder(Guid customerId)
            {
                var order = new Order { CustomerId = customerId };
                order.ToDraft();
                return order;
            }
        }
    }
}