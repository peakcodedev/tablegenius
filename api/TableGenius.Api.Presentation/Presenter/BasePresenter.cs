using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TableGenius.Api.Entities.Default;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public abstract class BasePresenter<TEntityRm, TEntity> : IPresenter<TEntityRm>
    where TEntityRm : BaseRm where TEntity : Base
{
    // Logger
    private readonly IDatabaseService<TEntity> _entityService;
    private readonly IMapper _mapper;

    protected BasePresenter(IDatabaseService<TEntity> entityService, IMapper mapper)
    {
        _entityService = entityService;
        _mapper = mapper;
    }

    public virtual List<TEntityRm> GetAll()
    {
        var all = _entityService.GetAllAsNoTracking().ToList();
        var returnMap = _mapper.Map<IEnumerable<TEntity>, List<TEntityRm>>(all);
        return returnMap;
    }

    public abstract TEntityRm GetBlank();

    public virtual TEntityRm GetById(Guid id)
    {
        var entity = _entityService.GetById(id);
        var model = _mapper.Map<TEntity, TEntityRm>(entity);
        UpdateBlank(model);
        return model;
    }

    public virtual bool DeleteById(Guid id, bool removeFromDb = false)
    {
        return _entityService.DeleteById(id, removeFromDb);
    }

    public abstract void UpdateBlank(TEntityRm entity);
}