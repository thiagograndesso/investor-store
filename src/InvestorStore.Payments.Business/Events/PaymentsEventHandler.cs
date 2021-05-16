using System.Threading;
using System.Threading.Tasks;
using InvestorStore.Core.DomainObjects.Dtos;
using InvestorStore.Core.Messages.CommonMessages.IntegrationEvents;
using MediatR;

namespace InvestorStore.Payments.Business.Events
{
    public class PaymentsEventHandler : INotificationHandler<OrderInventoryConfirmedEvent>
    {
        private readonly IPaymentService _paymentService;

        public PaymentsEventHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task Handle(OrderInventoryConfirmedEvent message, CancellationToken cancellationToken)
        {
            var paymentOrder = new OrderPayment
            {
                OrderId = message.OrderId,
                CustomerId = message.CustomerId,
                Total = message.Total,
                CardName = message.CardName,
                CardNumber = message.CardNumber,
                ExpiryDate = message.ExpiryDate,
                CvvCode = message.CvvCode
            };

            await _paymentService.Pay(paymentOrder);
        }
    }
}