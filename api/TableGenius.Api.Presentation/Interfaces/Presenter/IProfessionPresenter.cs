using System.Collections.Generic;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface IProfessionPresenter : IPresenter<ProfessionRm>
{
    ProfessionRm Update(ProfessionRm entity);
    ProfessionRm Add(ProfessionRm entity);
    IEnumerable<ProfessionRm> GetList();
}