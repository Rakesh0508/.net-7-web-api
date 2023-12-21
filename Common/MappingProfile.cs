using AutoMapper;
using WorldAPI.DTO.Country;
using WorldAPI.DTO.States;
using WorldAPI.Models;

namespace WorldAPI.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country,CreateCountryDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();

            CreateMap<States,CreateStatesDto>().ReverseMap();
            CreateMap<States,StatesDto>().ReverseMap();
            CreateMap<States, UpdateStatesDto>().ReverseMap();
        }
    }

    
}
