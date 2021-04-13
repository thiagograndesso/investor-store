using System.Threading.Tasks;

namespace InvestorStore.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}