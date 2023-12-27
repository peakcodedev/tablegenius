using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TableGenius.Api.Entities.Place;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class LocationPresenter : BasePresenter<LocationRm, Location>, ILocationPresenter
{
    private readonly ILocationService _locationService;
    private readonly IMapper _mapper;

    public LocationPresenter(IMapper mapper, ILocationService locationService) : base(
        locationService, mapper)
    {
        _locationService = locationService;
        _mapper = mapper;
    }

    public LocationRm Add(LocationRm entity)
    {
        var model = _mapper.Map<Location>(entity);
        var result = _locationService.Add(model);
        return _mapper.Map<LocationRm>(result);
    }

    public IEnumerable<LocationRm> GetList()
    {
        var all = _locationService.GetAllAsNoTracking().ToList();
        var returnMap = _mapper.Map<IEnumerable<Location>, List<LocationRm>>(all);
        return returnMap;
    }

    public LocationRm Update(LocationRm entity)
    {
        var db = _mapper.Map<LocationRm, Location>(entity);
        var elem = _locationService.Update(db);
        return _mapper.Map<Location, LocationRm>(elem);
    }

    public IEnumerable<LocationRm> GetAllMyLocations(string mail)
    {
        var all = _locationService.GetAllLocationsByMailAsNoTracking(mail).ToList();
        var returnMap = _mapper.Map<IEnumerable<Location>, List<LocationRm>>(all);
        return returnMap;
    }
}