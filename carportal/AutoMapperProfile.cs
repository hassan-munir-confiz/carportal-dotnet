using AutoMapper;
using carportal.Models;
using carportal.Models.Dtos;

namespace carportal
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Car, GetCarDto>();
            CreateMap<CreateCarDto, Car>();
            
            
        }
        
    }
}