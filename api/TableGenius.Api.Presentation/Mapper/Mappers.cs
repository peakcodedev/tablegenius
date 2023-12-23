using AutoMapper;
using TableGenius.Api.Entities.Place;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Mapper;

public class Mappers : Profile
{
    public Mappers()
    {
        CreateMap<Location, LocationRM>();
        CreateMap<LocationRM, Location>();
    }
}