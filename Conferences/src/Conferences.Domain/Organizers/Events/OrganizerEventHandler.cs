using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Conferences.Domain.Organizers.Events
{
    public class OrganizerEventHandler :
        INotificationHandler<OrganizerRegisteredEvent>
    {
        public Task Handle(OrganizerRegisteredEvent message, CancellationToken cancellationToken)
        {
            // TODO: Send an email?

            return Task.CompletedTask;
        }
    }
}
