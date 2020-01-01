using AutoMapper;
using Conferences.Application.Interfaces;
using Conferences.Application.ViewModels;
using Conferences.Domain.Conferences.Commands;
using Conferences.Domain.Conferences.Repository;
using Conferences.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Conferences.Application.AppServies
{
    public class ConferenceService : IConferenceService
    {        
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IConferenceRepository _conferenceRepository;

        public ConferenceService( 
            IMapper mapper, 
            IMediatorHandler mediatorHandler,
            IConferenceRepository conferenceRepository)
        {
            _conferenceRepository = conferenceRepository;
            _mapper = mapper;
            _mediatorHandler = mediatorHandler;
        }

        public IEnumerable<ConferenceViewModel> GetAll()
        {
            var conferences = _conferenceRepository.GetAll();
            return _mapper.Map<IEnumerable<ConferenceViewModel>>(conferences);
        }       

        public IEnumerable<CategoryViewModel> GetCategories()
        {
            var categories = _conferenceRepository.GetCategories();
            return _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }

        public IEnumerable<ConferenceViewModel> GetMyConferences(Guid id, Guid organizerId)
        {
            var conferences = _conferenceRepository.GetMyConferenceById(id, organizerId);
            return _mapper.Map<IEnumerable<ConferenceViewModel>>(conferences);
        }

        public ConferenceViewModel GetById(Guid id)
        {
            var conference = _conferenceRepository.GetById(id);
            return _mapper.Map<ConferenceViewModel>(conference);
        }

        public RegisterConferenceCommand RegisterConference(ConferenceViewModel conferenceViewModel)
        {
            var conference = _mapper.Map<RegisterConferenceCommand>(conferenceViewModel);
            _mediatorHandler.SendCommand(conference);
            return conference;
        }

        public UpdateConferenceCommand UpdateConference(ConferenceViewModel conferenceViewModel)
        {
            var conference = _mapper.Map<UpdateConferenceCommand>(conferenceViewModel);
            
            _mediatorHandler.SendCommand(conference);
            return conference;
        }

        public ExcludeConferenceCommand DeleteConference(Guid id)
        {
            var conferenceViewModel = new ConferenceViewModel { Id = id };
            var conference = _mapper.Map<ExcludeConferenceCommand>(conferenceViewModel);
            
            _mediatorHandler.SendCommand(conference);
            return conference;
        }

        public AddAddressConferenceCommand AddAddress(AddressViewModel addressViewModel)
        {
            var address = _mapper.Map<AddAddressConferenceCommand>(addressViewModel);

            _mediatorHandler.SendCommand(address);
            return address;
        }

        public UpdateAddressConferenceCommand UpdateAddress(AddressViewModel addressViewModel)
        {
            var address = _mapper.Map<UpdateAddressConferenceCommand>(addressViewModel);

            _mediatorHandler.SendCommand(address);
            return address;
        }

    }
}
