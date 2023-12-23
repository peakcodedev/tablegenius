using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface ICompanyPresenter : IPresenter<CompanyRm>
{
    CompanyRm Update(CompanyRm entity);
    CompanyRm Add(CompanyRm entity);
    EmployeeRm AddEmployeeToCompany(Guid userId, Guid companyId);
    IEnumerable<CompanyRm> GetList();
    CompanyRm GetCompanyByUserId(Guid userId);
    Task<string> UploadCompanyPicture(Guid companyId, MemoryStream companyImageStream, string contentType);
}