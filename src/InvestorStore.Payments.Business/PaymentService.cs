using System.Threading.Tasks;
using InvestorStore.Core.Communication.Mediator;
using InvestorStore.Core.DomainObjects.Dtos;
using InvestorStore.Core.Messages.CommonMessages.IntegrationEvents;
using InvestorStore.Core.Messages.CommonMessages.Notifications;

namespace InvestorStore.Payments.Business
{
    public class PaymentService : IPaymentService
    {
        private readonly ICreditCardPaymentFacade _creditCardPaymentFacade;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public PaymentService(ICreditCardPaymentFacade creditCardPaymentFacade,
                              IPaymentRepository paymentRepository, 
                              IMediatorHandler mediatorHandler)
        {
            _creditCardPaymentFacade = creditCardPaymentFacade;
            _paymentRepository = paymentRepository;
            _mediatorHandler = mediatorHandler;
        }

        public async Task<Transaction> Pay(OrderPayment orderPayment)
        {
            var order = new Order
            {
                Id = orderPayment.OrderId,
                Total = orderPayment.Total
            };

            var payment = new Payment
            {
                Amount = orderPayment.Total,
                CardName = orderPayment.CardName,
                CardNumber = orderPayment.CardNumber,
                ExpiryDate = orderPayment.ExpiryDate,
                CvvCode = orderPayment.CvvCode,
                OrderId = orderPayment.OrderId
            };

            var transaction = _creditCardPaymentFacade.Pay(order, payment);

            if (transaction.TransactionStatus == TransactionStatus.Paid)
            {
                payment.AddEvent(new OrderPaymentConfirmedEvent(order.Id, orderPayment.CustomerId, transaction.PaymentId, transaction.Id, order.Total));

                _paymentRepository.Add(payment);
                _paymentRepository.AddTransaction(transaction);

                await _paymentRepository.UnitOfWork.Commit();
                return transaction;
            }

            await _mediatorHandler.PublishNotification(new DomainNotification("Payment","The provider has denied payment"));
            await _mediatorHandler.PublishEvent(new OrderPaymentRejectedEvent(order.Id, orderPayment.CustomerId, transaction.PaymentId, transaction.Id, order.Total));

            return transaction;
        }
    }
}