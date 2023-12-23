using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TableGenius.Api.Entities.Company;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class CompanyPresenter : BasePresenter<CompanyRm, Company>, ICompanyPresenter
{
    private readonly ICompanyService _companyService;
    private readonly IEmployeeService _employeeService;
    private readonly IMapper _mapper;

    public CompanyPresenter(IMapper mapper, ICompanyService companyService, IEmployeeService employeeService) : base(
        companyService, mapper)
    {
        _companyService = companyService;
        _employeeService = employeeService;
        _mapper = mapper;
    }

    public override CompanyRm GetBlank()
    {
        return new CompanyRm();
    }

    public override void UpdateBlank(CompanyRm entity)
    {
        // NOTHING TO DO HERE
    }

    public IEnumerable<CompanyRm> GetList()
    {
        var all = _companyService.GetAllAsNoTracking().ToList();
        var returnMap = _mapper.Map<IEnumerable<Company>, List<CompanyRm>>(all);
        return returnMap;
    }

    public CompanyRm GetCompanyByUserId(Guid userId)
    {
        var employment = _employeeService.GetAllAsNoTracking().SingleOrDefault(e => e.UserId == userId);
        if (employment == null) return null;
        var company = _companyService.GetAllAsNoTracking().Single(c => employment.CompanyId == c.Id);
        var companyRm = _mapper.Map<Company, CompanyRm>(company);
        return companyRm;
    }

    public CompanyRm Update(CompanyRm entity)
    {
        var existingCompany = _companyService.GetByIdAsNoTracking(entity.Id);
        entity.Image = existingCompany.Image;
        var db = _mapper.Map<CompanyRm, Company>(entity);
        var elem = _companyService.Update(db);
        return _mapper.Map<Company, CompanyRm>(elem);
    }

    public CompanyRm Add(CompanyRm entity)
    {
        var model = _mapper.Map<Company>(entity);
        var result = _companyService.Add(model);
        return _mapper.Map<CompanyRm>(result);
    }

    public EmployeeRm AddEmployeeToCompany(Guid userId, Guid companyId)
    {
        var employee = new Employee
        {
            CompanyId = companyId,
            UserId = userId
        };
        var result = _employeeService.Add(employee);
        return _mapper.Map<EmployeeRm>(result);
    }

    public Task<string> UploadCompanyPicture(Guid companyId, MemoryStream companyImageStream, string contentType)
    {
        return _companyService.UploadCompanyPicture(companyId, companyImageStream, contentType);
    }
}