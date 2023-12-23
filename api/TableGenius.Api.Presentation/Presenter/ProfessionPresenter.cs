using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TableGenius.Api.Entities.General;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class ProfessionPresenter : BasePresenter<ProfessionRm, Profession>, IProfessionPresenter
{
    private readonly IMapper _mapper;
    private readonly IProfessionService _professionService;

    public ProfessionPresenter(IMapper mapper, IProfessionService professionService) : base(
        professionService, mapper)
    {
        _professionService = professionService;
        _mapper = mapper;
    }

    public override ProfessionRm GetBlank()
    {
        return new ProfessionRm();
    }

    public override void UpdateBlank(ProfessionRm entity)
    {
        // NOTHING TO DO HERE
    }

    public IEnumerable<ProfessionRm> GetList()
    {
        var all = _professionService.GetAllAsNoTracking().ToList();
        var returnMap = _mapper.Map<IEnumerable<Profession>, List<ProfessionRm>>(all);
        return returnMap;
    }

    public ProfessionRm Update(ProfessionRm entity)
    {
        var db = _mapper.Map<ProfessionRm, Profession>(entity);
        var elem = _professionService.Update(db);
        return _mapper.Map<Profession, ProfessionRm>(elem);
    }

    public ProfessionRm Add(ProfessionRm entity)
    {
        var model = _mapper.Map<Profession>(entity);
        var result = _professionService.Add(model);
        return _mapper.Map<ProfessionRm>(result);
    }
}