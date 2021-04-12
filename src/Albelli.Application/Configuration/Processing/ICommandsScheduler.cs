using System.Threading.Tasks;
using MediatR;
using Albelli.Application.Configuration.Commands;

namespace Albelli.Application.Configuration.Processing
{
    public interface ICommandsScheduler
    {
        Task EnqueueAsync<T>(ICommand<T> command);
    }
}