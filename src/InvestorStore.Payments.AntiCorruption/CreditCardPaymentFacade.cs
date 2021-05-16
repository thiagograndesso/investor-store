using InvestorStore.Payments.Business;

namespace InvestorStore.Payments.AntiCorruption
{
    public class CreditCardPaymentFacade : ICreditCardPaymentFacade
    {
        private readonly IPayPalGateway _payPalGateway;
        private readonly IConfigurationManager _configManager;

        public CreditCardPaymentFacade(IPayPalGateway payPalGateway, IConfigurationManager configManager)
        {
            _payPalGateway = payPalGateway;
            _configManager = configManager;
        }

        public Transaction Pay(Order order, Payment payment)
        {
            var apiKey = _configManager.GetValue("apiKey");
            var encryptionKey = _configManager.GetValue("encryptionKey");

            var serviceKey = _payPalGateway.GetPayPalServiceKey(apiKey, encryptionKey);
            var cardHashKey = _payPalGateway.GetCardHashKey(serviceKey, payment.CardNumber);

            var result = _payPalGateway.CommitTransaction(cardHashKey, order.Id.ToString(), payment.Amount);

            // TODO: Here the Payment Gateway should return the transaction
            var transaction = new Transaction
            {
                OrderId = order.Id,
                Total = order.Total,
                PaymentId = payment.Id
            };

            if (result)
            {
                transaction.TransactionStatus = TransactionStatus.Paid;
                return transaction;
            }

            transaction.TransactionStatus = TransactionStatus.Rejected;
            return transaction;
        }
    }
}