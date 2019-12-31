using AutoMapper;
using Conferences.Application.ViewModels;
using Conferences.Domain.Conferences;
using Conferences.Domain.Organizers;

namespace Conferences.Application.AutoMapper
{
    internal class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Conference, ConferenceViewModel>();
            CreateMap<Address, AddressViewModel>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Organizer, OrganizerViewModel>();
        }
    }
}