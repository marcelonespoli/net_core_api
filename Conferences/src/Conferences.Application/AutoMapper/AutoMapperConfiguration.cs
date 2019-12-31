using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conferences.Application.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMapping()
        {
            return new MapperConfiguration((options) =>
            {
                options.AddProfile(new DomainToViewModelMappingProfile());
                options.AddProfile(new ViewModelToDomainMappingProfile());
            });
        }
    }
}
