using System.Collections.Generic;
using System.Data;
using AutoMapper;
using Domain;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Bike, Bike>();
        }
    }
}