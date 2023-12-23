using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TableGenius.Api.Entities.Default;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public abstract class BasePresenter<TEntityRm, TEntity>(IDatabaseService<TEntity> entityService, IMapper mapper)
    : IPresenter<TEntityRm>
    where TEntityRm : BaseRm
    where TEntity : Base
{
    // Logger

    public virtual List<TEntityRm> GetAll()
    {
        var all = entityService.GetAllAsNoTracking().ToList();
        var returnMap = mapper.Map<IEnumerable<TEntity>, List<TEntityRm>>(all);
        return returnMap;
    }

    public virtual TEntityRm GetById(Guid id)
    {
        var entity = entityService.GetById(id);
        var model = mapper.Map<TEntity, TEntityRm>(entity);
        return model;
    }

    public virtual bool DeleteById(Guid id, bool removeFromDb = false)
    {
        return entityService.DeleteById(id, removeFromDb);
    }
}