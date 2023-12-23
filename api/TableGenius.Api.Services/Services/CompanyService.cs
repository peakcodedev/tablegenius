using System;
using System.IO;
using System.Threading.Tasks;
using TableGenius.Api.Entities.Company;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;
using TableGenius.Api.Services.Queries;

namespace TableGenius.Api.Services.Services;

public class CompanyService : DatabaseServiceBase<Company>, ICompanyService
{
    private readonly IEmployeeService _employeeService;
    private readonly IFileUploader _fileUploader;

    public CompanyService(IDatabaseUnitOfWorkFactory unitOfWorkFactory, IApplicationLogger logger,
        IFileUploader fileUploader, IEmployeeService employeeService) : base(logger)
    {
        DatabaseUnitOfWork = unitOfWorkFactory.Create();
        Querier = new CompanyQueries(DatabaseUnitOfWork);
        _fileUploader = fileUploader;
        _employeeService = employeeService;
    }

    public async Task<string> UploadCompanyPicture(Guid companyId, MemoryStream companyPictureStream,
        string contentType)
    {
        var company = GetById(companyId);
        const string folder = "companyImage";
        var fileName = company.Id.ToString("N");
        var companyImageUrl = await _fileUploader.UploadFile(folder, fileName, companyPictureStream, contentType);
        company.Image = fileName;
        Update(company);
        return companyImageUrl;
    }

    public Company GetByUserId(Guid userId)
    {
        var employee = _employeeService.GetByUserId(userId);
        return employee == null ? null : GetById(employee.CompanyId);
    }
}