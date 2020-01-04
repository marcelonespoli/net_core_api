using Conferences.Application.ViewModels;
using Conferences.Domain.Conferences.Commands;
using System;
using System.Collections.Generic;

namespace Conferences.Application.Interfaces
{
    public interface IConferenceService
    {
        IEnumerable<ConferenceViewModel> GetAll();        
        IEnumerable<CategoryViewModel> GetCategories();
        IEnumerable<ConferenceViewModel> GetMyConferences(Guid organizerId);
        ConferenceViewModel GetMyConferenceById(Guid id, Guid organizerId);
        ConferenceViewModel GetById(Guid id);
        RegisterConferenceCommand RegisterConference(ConferenceViewModel conferenceViewModel);
        UpdateConferenceCommand UpdateConference(ConferenceViewModel conferenceViewModel);
        ExcludeConferenceCommand DeleteConference(Guid id);
        AddAddressConferenceCommand AddAddress(AddressViewModel addressViewModel);
        UpdateAddressConferenceCommand UpdateAddress(AddressViewModel addressViewModel);
    }
}
