using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TableGenius.Api.Entities.Place;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class AreaPresenter : BasePresenter<AreaRm, Area>, IAreaPresenter
{
    private readonly IAreaService _areaService;
    private readonly IMapper _mapper;

    public AreaPresenter(IMapper mapper, IAreaService areaService) : base(
        areaService, mapper)
    {
        _areaService = areaService;
        _mapper = mapper;
    }

    public AreaRm Add(AreaRm entity)
    {
        var model = _mapper.Map<Area>(entity);
        var result = _areaService.Add(model);
        return _mapper.Map<AreaRm>(result);
    }

    public IEnumerable<AreaRm> GetList()
    {
        var all = _areaService.GetAllAsNoTracking().ToList();
        var returnMap = _mapper.Map<IEnumerable<Area>, List<AreaRm>>(all);
        return returnMap;
    }

    public AreaRm Update(AreaRm entity)
    {
        var db = _mapper.Map<AreaRm, Area>(entity);
        var elem = _areaService.Update(db);
        return _mapper.Map<Area, AreaRm>(elem);
    }
}