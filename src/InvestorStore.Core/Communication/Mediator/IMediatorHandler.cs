using System.Threading.Tasks;
using InvestorStore.Core.Messages;
using InvestorStore.Core.Messages.CommonMessages.Notifications;

namespace InvestorStore.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event; 
        Task PublishNotification<T>(T notification) where T : DomainNotification;
        Task<bool> SendCommand<T>(T command) where T : Command;
    }
}