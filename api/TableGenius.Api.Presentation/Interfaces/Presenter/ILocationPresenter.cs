using System.Collections.Generic;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface ILocationPresenter : IPresenter<LocationRM>
{
    LocationRM Add(LocationRM entity);
    IEnumerable<LocationRM> GetList();
    LocationRM Update(LocationRM entity);
}