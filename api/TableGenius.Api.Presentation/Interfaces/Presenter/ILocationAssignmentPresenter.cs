using System;
using System.Collections.Generic;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface ILocationAssignmentPresenter : IPresenter<LocationAssignmentRm>
{
    LocationAssignmentRm Add(LocationAssignmentRm entity);
    IEnumerable<LocationAssignmentRm> GetListByLocation(Guid locationId);
    LocationAssignmentRm Update(LocationAssignmentRm entity);
}