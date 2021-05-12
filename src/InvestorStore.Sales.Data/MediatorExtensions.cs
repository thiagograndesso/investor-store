using System.Linq;
using System.Threading.Tasks;
using InvestorStore.Core.Communication.Mediator;
using InvestorStore.Core.DomainObjects;

namespace InvestorStore.Sales.Data
{
    public static class MediatorExtensions
    {
        public static async Task PublishEvents(this IMediatorHandler mediator, SalesContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Events != null && x.Entity.Events.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Events)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => {
                    await mediator.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}