using Conferences.Domain.Conferences.Events;
using Conferences.Domain.Conferences.Repository;
using Conferences.Domain.Core.Notifications;
using Conferences.Domain.Handlers;
using Conferences.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Conferences.Domain.Conferences.Commands
{
    public class ConferenceCommandHandler : CommandHandler,
        IRequestHandler<RegisterConferenceCommand>,
        IRequestHandler<UpdateConferenceCommand>,
        IRequestHandler<ExcludeConferenceCommand>,
        IRequestHandler<AddAddressConferenceCommand>,
        IRequestHandler<UpdateAddressConferenceCommand>
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly IUser _user;
        private readonly IMediatorHandler _mediator;

        public ConferenceCommandHandler(
            IUser user,
            IUnitOfWork uow, 
            IMediatorHandler mediator,
            IConferenceRepository conferenceRepository,
            INotificationHandler<DomainNotification> notifications) : base(uow, mediator, notifications)
        {
            _user = user;
            _mediator = mediator;
            _conferenceRepository = conferenceRepository;
        }

        public Task<Unit> Handle(RegisterConferenceCommand message, CancellationToken cancellationToken)
        {
            var address = new Address(message.Address.Id, message.Address.Address1, message.Address.Address2, message.Address.Address3,
                message.Address.Number, message.Address.Postcode, message.Address.City, message.Address.County, message.Address.ConferenceId.Value);

            var conference = Conference.ConferenceFactory.NewConferenceComplety(message.Id, message.Name, message.ShortDescription,
                message.LongDescription, message.StartDate, message.EndDate, message.Free, message.Value,
                message.Online, message.CompanyName, _user.GetUserId(), address, message.CategoryId);

            if (!IsConferenceValid(conference)) return Task.FromResult(Unit.Value);

            // TODO:
            // Validate business
            // Organizer can register conference?

            _conferenceRepository.Add(conference);

            if (Commit())
            {
                _mediator.PublishEvent(new ConferenceRegisteredEvent(conference.Id, conference.Name, conference.StartDate, conference.EndDate,
                    conference.Free, conference.Value, conference.Online, conference.CompanyName));
            }

            return Task.FromResult(Unit.Value);
        }        

        public Task<Unit> Handle(UpdateConferenceCommand message, CancellationToken cancellationToken)
        {
            if (!ConferenceExists(message.Id, message.MessageType)) return Task.FromResult(Unit.Value);

            var currentConference = _conferenceRepository.GetById(message.Id);
            if (currentConference.OrganizerId != _user.GetUserId())
            {
                _mediator.PublishEvent(new DomainNotification(message.MessageType, "The conference not belong to the organizer"));
                return Task.FromResult(Unit.Value);
            }

            var conference = Conference.ConferenceFactory.NewConferenceComplety(message.Id, message.Name, message.ShortDescription,
                message.LongDescription, message.StartDate, message.EndDate, message.Free, message.Value,
                message.Online, message.CompanyName, _user.GetUserId(), currentConference.Address, message.CategoryId);

            if (!conference.Online && conference.Address == null)
            {
                _mediator.PublishEvent(new DomainNotification(message.MessageType, "Is not possible to update a conference without an address"));
                return Task.FromResult(Unit.Value);
            }

            if (!IsConferenceValid(conference)) return Task.FromResult(Unit.Value);

            _conferenceRepository.Update(conference);

            if (Commit())
            {
                _mediator.PublishEvent(new ConferenceUpdatedEvent(conference.Id, conference.Name, conference.StartDate, conference.EndDate,
                    conference.Free, conference.Value, conference.Online, conference.CompanyName));                
            }

            return Task.FromResult(Unit.Value);
        }               

        public Task<Unit> Handle(ExcludeConferenceCommand message, CancellationToken cancellationToken)
        {
            if (!ConferenceExists(message.Id, message.MessageType)) return Task.FromResult(Unit.Value);

            var currentConference = _conferenceRepository.GetById(message.Id);
            if (currentConference.OrganizerId != _user.GetUserId())
            {
                _mediator.PublishEvent(new DomainNotification(message.MessageType, "The conference not belong to the organizer"));
                return Task.FromResult(Unit.Value);
            }

            // TODO: validation business

            currentConference.ExcludeConference();

            _conferenceRepository.Update(currentConference);

            if (Commit())
            {
                _mediator.PublishEvent(new ConferenceExcludedEvent(message.Id));
            }

            return Task.FromResult(Unit.Value);
        }

        public Task<Unit> Handle(AddAddressConferenceCommand message, CancellationToken cancellationToken)
        {
            var address = new Address(message.Id, message.Address1, message.Address2, message.Address3,
                message.Number, message.Postcode, message.City, message.County, message.ConferenceId.Value);

            if (!address.IsValid())
            {
                NotifyValidationError(address.ValidationResult);
                return Task.FromResult(Unit.Value);
            }

            var conference = _conferenceRepository.GetById(address.ConferenceId.Value);
            conference.SwitchToPresencial();

            _conferenceRepository.Update(conference);
            _conferenceRepository.AddAddress(address);

            if (Commit())
            {
                _mediator.PublishEvent(new AddressConferenceAddedEvent(address.Id, address.Address1, address.Address2, address.Address3,
                        address.Number, address.Postcode, address.City, address.County, address.ConferenceId.Value));
            }

            return Task.FromResult(Unit.Value);
        }

        public Task<Unit> Handle(UpdateAddressConferenceCommand message, CancellationToken cancellationToken)
        {
            var address = new Address(message.Id, message.Address1, message.Address2, message.Address3,
                message.Number, message.Postcode, message.City, message.County, message.ConferenceId.Value);

            if (!address.IsValid())
            {
                NotifyValidationError(address.ValidationResult);
                return Task.FromResult(Unit.Value);
            }

            _conferenceRepository.UpdateAddress(address);

            if (Commit())
            {
                _mediator.PublishEvent(new AddressConferenceUpdatedEvent(address.Id, address.Address1, address.Address2, address.Address3,
                        address.Number, address.Postcode, address.City, address.County, address.ConferenceId.Value));
            }

            return Task.FromResult(Unit.Value);
        }

        private bool IsConferenceValid(Conference conference)
        {
            if (conference.IsValid()) return true;

            NotifyValidationError(conference.ValidationResult);
            return false;
        }

        private bool ConferenceExists(Guid id, string messageType)
        {
            var conference = _conferenceRepository.GetById(id);

            if (conference != null) return true;

            _mediator.PublishEvent(new DomainNotification(messageType, "Conference not found"));
            return false;
        }
    }
}
