using System;
using System.Collections.Generic;
using AutoMapper;
using TableGenius.Api.Entities.Place;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class LocationAssignmentPresenter : BasePresenter<LocationAssignmentRm, LocationAssignment>,
    ILocationAssignmentPresenter
{
    private readonly ILocationAssignmentService _locationAssignmentService;
    private readonly IMapper _mapper;

    public LocationAssignmentPresenter(IMapper mapper, ILocationAssignmentService locationAssignmentService) : base(
        locationAssignmentService, mapper)
    {
        _locationAssignmentService = locationAssignmentService;
        _mapper = mapper;
    }

    public LocationAssignmentRm Add(LocationAssignmentRm entity)
    {
        var model = _mapper.Map<LocationAssignment>(entity);
        var result = _locationAssignmentService.Add(model);
        return _mapper.Map<LocationAssignmentRm>(result);
    }

    public LocationAssignmentRm Update(LocationAssignmentRm entity)
    {
        var db = _mapper.Map<LocationAssignmentRm, LocationAssignment>(entity);
        var elem = _locationAssignmentService.Update(db);
        return _mapper.Map<LocationAssignment, LocationAssignmentRm>(elem);
    }

    public IEnumerable<LocationAssignmentRm> GetListByLocation(Guid locationId)
    {
        var all = _locationAssignmentService.GetAllByLocationIdAsNoTracking(locationId);
        var returnMap = _mapper.Map<IEnumerable<LocationAssignment>, List<LocationAssignmentRm>>(all);
        return returnMap;
    }
}