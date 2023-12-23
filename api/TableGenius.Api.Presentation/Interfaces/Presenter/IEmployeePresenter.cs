using System;
using System.Collections.Generic;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface IEmployeePresenter : IPresenter<EmployeeRm>
{
    EmployeeRm Add(EmployeeModel entity);
    IEnumerable<EmployeeRm> GetList(Guid companyId);
    IEnumerable<EmployeeRm> GetAllNonDeletedEntries();
}