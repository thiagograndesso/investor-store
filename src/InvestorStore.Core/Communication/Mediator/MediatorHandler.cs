using System.Threading.Tasks;
using InvestorStore.Core.Messages;
using InvestorStore.Core.Messages.CommonMessages.Notifications;
using MediatR;

namespace InvestorStore.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public async Task PublishEvent<T>(T @event) where T : Event
        {
            await _mediator.Publish(@event);
        }

        public async Task PublishNotification<T>(T notification) where T : DomainNotification
        {
            await _mediator.Publish(notification);
        }

        public async Task<bool> SendCommand<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }
    }
}