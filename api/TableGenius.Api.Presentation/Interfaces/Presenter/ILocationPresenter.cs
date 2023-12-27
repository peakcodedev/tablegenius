using System.Collections.Generic;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface ILocationPresenter : IPresenter<LocationRm>
{
    LocationRm Add(LocationRm entity);
    IEnumerable<LocationRm> GetList();
    IEnumerable<LocationRm> GetAllMyLocations(string mail);
    LocationRm Update(LocationRm entity);
}