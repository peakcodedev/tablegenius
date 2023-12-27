using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TableGenius.Api.Entities.Place;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class AreaSlotPresenter : BasePresenter<AreaSlotRm, AreaSlot>, IAreaSlotPresenter
{
    private readonly IAreaSlotService _areaSlotService;
    private readonly IMapper _mapper;

    public AreaSlotPresenter(IMapper mapper, IAreaSlotService areaSlotService) : base(
        areaSlotService, mapper)
    {
        _areaSlotService = areaSlotService;
        _mapper = mapper;
    }

    public AreaSlotRm Add(AreaSlotRm entity)
    {
        var model = _mapper.Map<AreaSlot>(entity);
        var result = _areaSlotService.Add(model);
        return _mapper.Map<AreaSlotRm>(result);
    }

    public IEnumerable<AreaSlotRm> GetList()
    {
        var all = _areaSlotService.GetAllAsNoTracking().ToList();
        var returnMap = _mapper.Map<IEnumerable<AreaSlot>, List<AreaSlotRm>>(all);
        return returnMap;
    }

    public AreaSlotRm Update(AreaSlotRm entity)
    {
        var db = _mapper.Map<AreaSlotRm, AreaSlot>(entity);
        var elem = _areaSlotService.Update(db);
        return _mapper.Map<AreaSlot, AreaSlotRm>(elem);
    }
}