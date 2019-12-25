using Conferences.Domain.Core.Commands;
using Conferences.Domain.Core.Events;
using System.Threading.Tasks;

namespace Conferences.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T @event) where T : Event;
        Task SendCommand<T>(T command) where T : Command;
    }
}
