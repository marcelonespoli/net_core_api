using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Conferences.Domain.Conferences.Events
{
    public class ConferenceEventHandler :
        INotificationHandler<ConferenceRegisteredEvent>,
        INotificationHandler<ConferenceUpdatedEvent>,
        INotificationHandler<ConferenceExcludedEvent>,
        INotificationHandler<AddressConferenceAddedEvent>,
        INotificationHandler<AddressConferenceUpdatedEvent>
    {
        public Task Handle(ConferenceRegisteredEvent message, CancellationToken cancellationToken)
        {
            // TODO: Run some action
            return Task.CompletedTask;
        }

        public Task Handle(ConferenceUpdatedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Run some action
            return Task.CompletedTask;
        }

        public Task Handle(ConferenceExcludedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Run some action
            return Task.CompletedTask;
        }

        public Task Handle(AddressConferenceAddedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Run some action
            return Task.CompletedTask;
        }

        public Task Handle(AddressConferenceUpdatedEvent message, CancellationToken cancellationToken)
        {
            // TODO: Run some action
            return Task.CompletedTask;
        }
    }
}
