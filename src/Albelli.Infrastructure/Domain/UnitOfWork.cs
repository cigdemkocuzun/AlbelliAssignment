using System.Threading;
using System.Threading.Tasks;
using Albelli.Domain.SeedWork;
using Albelli.Infrastructure.Database;
using Albelli.Infrastructure.Processing;

namespace Albelli.Infrastructure.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrdersContext _ordersContext;
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;

        public UnitOfWork(
            OrdersContext ordersContext, 
            IDomainEventsDispatcher domainEventsDispatcher)
        {
            this._ordersContext = ordersContext;
            this._domainEventsDispatcher = domainEventsDispatcher;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await this._domainEventsDispatcher.DispatchEventsAsync();
            return await this._ordersContext.SaveChangesAsync(cancellationToken);
        }
    }
}