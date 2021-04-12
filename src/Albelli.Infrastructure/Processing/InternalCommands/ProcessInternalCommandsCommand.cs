using MediatR;
using Albelli.Application;
using Albelli.Application.Configuration.Commands;
using Albelli.Infrastructure.Processing.Outbox;

namespace Albelli.Infrastructure.Processing.InternalCommands
{
    internal class ProcessInternalCommandsCommand : CommandBase<Unit>, IRecurringCommand
    {

    }
}