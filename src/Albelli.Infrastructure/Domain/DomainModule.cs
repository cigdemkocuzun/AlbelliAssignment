using Autofac;
using Albelli.Domain.Customers;
using Albelli.Domain.ForeignExchange;
using Albelli.Infrastructure.Domain.ForeignExchanges;
using Albelli.Application.Extensions;

namespace Albelli.Infrastructure.Domain
{
    public class DomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerUniquenessChecker>()
                .As<ICustomerUniquenessChecker>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ForeignExchange>()
                .As<IForeignExchange>()
                .InstancePerLifetimeScope();
        }
    }
}