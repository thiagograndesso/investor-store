namespace InvestorStore.Payments.Business
{
    public interface ICreditCardPaymentFacade
    {
        Transaction Pay(Order order, Payment payment);
    }
}