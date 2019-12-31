using Conferences.Domain.Core.Events;
using Conferences.Domain.Interfaces;
using Conferences.Infra.Data.Repository.EventSourcing;
using Newtonsoft.Json;

namespace Conferences.Infra.Data.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IUser _user;

        public SqlEventStore(IEventStoreRepository eventStoreRepository, IUser user)
        {
            _eventStoreRepository = eventStoreRepository;
            _user = user;
        }


        public void SaveEvent<T>(T @event) where T : Event
        {
            var serializedData = JsonConvert.SerializeObject(@event);

            var storedEvent = new StoredEvent(
                @event, 
                serializedData, 
                _user.GetUserId().ToString());

            _eventStoreRepository.Store(storedEvent);
        }
    }
}
