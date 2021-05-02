using System.Threading.Tasks;
using InvestorStore.Core.Messages;
using MediatR;

namespace InvestorStore.Core.Bus
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
    }
}