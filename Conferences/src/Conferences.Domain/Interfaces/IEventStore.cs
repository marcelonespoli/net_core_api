using Conferences.Domain.Core.Events;

namespace Conferences.Domain.Interfaces
{
    public interface IEventStore
    {
        void SaveEvent<T>(T @event) where T : Event;
    }
}
