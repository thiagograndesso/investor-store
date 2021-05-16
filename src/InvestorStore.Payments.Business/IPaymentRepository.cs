using InvestorStore.Core.Data;

namespace InvestorStore.Payments.Business
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        void Add(Payment payment);

        void AddTransaction(Transaction transaction);
    }
}