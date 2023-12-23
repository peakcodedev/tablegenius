using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TableGenius.Api.Entities.Place;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class LocationPresenter: BasePresenter<LocationRM, Location>, ILocationPresenter
{
    private readonly ILocationService _locationService;
    private readonly IMapper _mapper;

    public LocationPresenter(IMapper mapper, ILocationService locationService) : base(
        locationService, mapper)
    {
        _locationService = locationService;
        _mapper = mapper;
    }

    public LocationRM Add(LocationRM entity)
    {
        var model = _mapper.Map<Location>(entity);
        var result = _locationService.Add(model);
        return _mapper.Map<LocationRM>(result);
    }
    
    public IEnumerable<LocationRM> GetList()
    {
        var all = _locationService.GetAllAsNoTracking().ToList();
        var returnMap = _mapper.Map<IEnumerable<Location>, List<LocationRM>>(all);
        return returnMap;
    }
    
    public LocationRM Update(LocationRM entity)
    {
        var db = _mapper.Map<LocationRM, Location>(entity);
        var elem = _locationService.Update(db);
        return _mapper.Map<Location, LocationRM>(elem);
    }

}