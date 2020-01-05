using AutoMapper;
using Conferences.Application.ViewModels;
using Conferences.Domain.Conferences.Commands;
using Conferences.Domain.Organizers.Commands;
using System;

namespace Conferences.Application.AutoMapper
{
    internal class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ConferenceViewModel, RegisterConferenceCommand>()
                .ConstructUsing(c => new RegisterConferenceCommand(
                    c.Id,
                    c.Name,
                    c.ShortDescription,
                    c.LongDescription,
                    c.StartDate,
                    c.EndDate,
                    c.Free,
                    c.Value,
                    c.Online,
                    c.CompanyName,
                    c.OrganizerId,
                    c.CategoryId,
                    new AddAddressConferenceCommand(
                        c.Address.Id, 
                        c.Address.Address1, 
                        c.Address.Address2, 
                        c.Address.Address3, 
                        c.Address.Number, 
                        c.Address.Postcode,                         
                        c.Address.City, 
                        c.Address.County, 
                        c.Id)));

            CreateMap<ConferenceViewModel, UpdateConferenceCommand>()
                .ConstructUsing(c => new UpdateConferenceCommand(
                    c.Id,
                    c.Name,
                    c.ShortDescription,
                    c.LongDescription,
                    c.StartDate,
                    c.EndDate,
                    c.Free,
                    c.Value,
                    c.Online,
                    c.CompanyName,
                    c.OrganizerId,
                    c.CategoryId));

            CreateMap<ConferenceViewModel, ExcludeConferenceCommand>()
                .ConstructUsing(c => new ExcludeConferenceCommand(c.Id));

            CreateMap<AddressViewModel, AddAddressConferenceCommand>()
                .ConstructUsing(c => new AddAddressConferenceCommand(
                    c.Id, 
                    c.Address1,
                    c.Address2,
                    c.Address3,
                    c.Number,
                    c.Postcode,
                    c.City,
                    c.County,
                    c.ConferenceId));

            CreateMap<AddressViewModel, UpdateAddressConferenceCommand>()
                .ConstructUsing(c => new UpdateAddressConferenceCommand(
                    c.Id,
                    c.Address1,
                    c.Address2,
                    c.Address3,
                    c.Number,
                    c.Postcode,
                    c.City,
                    c.County,
                    c.ConferenceId));


            // Organizer
            CreateMap<OrganizerViewModel, RegisterOrganizerCommand>()
                .ConstructUsing(c => new RegisterOrganizerCommand(c.Id, c.Name, c.DocumentId, c.Email));
        }
    }
}