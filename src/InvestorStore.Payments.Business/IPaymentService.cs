using System.Threading.Tasks;
using InvestorStore.Core.DomainObjects.Dtos;

namespace InvestorStore.Payments.Business
{
    public interface IPaymentService
    {
        Task<Transaction> Pay(OrderPayment orderPayment);
    }
}