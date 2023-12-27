using System.Collections.Generic;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface IAreaSlotPresenter : IPresenter<AreaSlotRm>
{
    AreaSlotRm Add(AreaSlotRm entity);
    IEnumerable<AreaSlotRm> GetList();
    AreaSlotRm Update(AreaSlotRm entity);
}