using System.Threading.Tasks;
using InvestorStore.Core.Messages;

namespace InvestorStore.Core.Bus
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
        Task<bool> SendCommand<T>(T command) where T : Command;
    }
}