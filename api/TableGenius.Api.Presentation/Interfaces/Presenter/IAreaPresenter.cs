using System.Collections.Generic;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface IAreaPresenter : IPresenter<AreaRm>
{
    AreaRm Add(AreaRm entity);
    IEnumerable<AreaRm> GetList();
    AreaRm Update(AreaRm entity);
}