using System;
using System.Threading.Tasks;

namespace Albelli.Infrastructure.Processing
{
    public interface ICommandsDispatcher
    {
        Task DispatchCommandAsync(Guid id);
    }
}
