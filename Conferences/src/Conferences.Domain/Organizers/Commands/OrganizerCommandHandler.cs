using Conferences.Domain.Core.Notifications;
using Conferences.Domain.Handlers;
using Conferences.Domain.Interfaces;
using Conferences.Domain.Organizers.Events;
using Conferences.Domain.Organizers.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Conferences.Domain.Organizers.Commands
{
    public class OrganizerCommandHandler : CommandHandler,
        IRequestHandler<RegisterOrganizerCommand>
    {
        private readonly IMediatorHandler _mediator;
        private readonly IOrganizerRepository _organizerRepository;

        public OrganizerCommandHandler(
            IUnitOfWork uow, 
            IMediatorHandler mediator,
            IOrganizerRepository organizerRepository,
            INotificationHandler<DomainNotification> notifications) : base(uow, mediator, notifications)
        {
            _organizerRepository = organizerRepository;
            _mediator = mediator;
        }

        public Task<Unit> Handle(RegisterOrganizerCommand message, CancellationToken cancellationToken)
        {
            var organizer = new Organizer(message.Id, message.Name, message.Email, message.DocumentId);

            if (!organizer.IsValid())
            {
                NotifyValidationError(organizer.ValidationResult);
                return Task.FromResult(Unit.Value);
            }

            var organizerExists = _organizerRepository.Search(o => o.DocumentId == organizer.DocumentId || o.Email == organizer.Email);
            if (organizerExists.Any())
            {
                _mediator.PublishEvent(new DomainNotification(message.MessageType, "Document ID or Email already registered"));
            }

            _organizerRepository.Add(organizer);

            if (Commit())
            {
                _mediator.PublishEvent(new OrganizerRegisteredEvent(organizer.Id, organizer.Name, organizer.Email, organizer.DocumentId));
            }

            return Task.FromResult(Unit.Value);
        }

    }
}
