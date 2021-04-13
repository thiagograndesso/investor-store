using System.Threading.Tasks;
using InvestorStore.Core.Messages;

namespace InvestorStore.Core.Bus
{
    public interface IMediatrHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
    }
}