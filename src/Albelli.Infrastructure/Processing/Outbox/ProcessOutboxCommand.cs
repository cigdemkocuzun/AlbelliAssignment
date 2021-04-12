using MediatR;
using Albelli.Application;
using Albelli.Application.Configuration.Commands;

namespace Albelli.Infrastructure.Processing.Outbox
{
    public class ProcessOutboxCommand : CommandBase<Unit>, IRecurringCommand
    {

    }
}