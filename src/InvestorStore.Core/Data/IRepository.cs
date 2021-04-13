using System;
using InvestorStore.Core.DomainObjects;

namespace InvestorStore.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        public IUnitOfWork UnitOfWork { get; }
    }
}