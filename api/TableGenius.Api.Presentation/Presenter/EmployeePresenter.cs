using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TableGenius.Api.Entities.Company;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class EmployeePresenter : BasePresenter<EmployeeRm, Employee>,
    IEmployeePresenter
{
    private readonly IEmployeeService _employeeService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    public EmployeePresenter(IMapper mapper, IUserService userService,
        IEmployeeService employeeService) : base(
        employeeService, mapper)
    {
        _userService = userService;
        _employeeService = employeeService;
        _mapper = mapper;
    }

    public IEnumerable<EmployeeRm> GetList(Guid companyId)
    {
        var all = _employeeService.GetAllAsNoTracking().Where(pc => pc.CompanyId == companyId).ToList();

        return (from employee in all
            let user = _userService.GetById(employee.UserId)
            select new EmployeeRm
            {
                Id = employee.Id,
                CompanyId = employee.CompanyId,
                UserId = employee.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Mail = user.Mail
            }).ToList();
    }

    public override EmployeeRm GetBlank()
    {
        return new EmployeeRm();
    }

    public override void UpdateBlank(EmployeeRm entity)
    {
        // NOTHING TO DO HERE
    }

    public EmployeeRm Add(EmployeeModel entity)
    {
        var model = _mapper.Map<Employee>(entity);
        var existingUser = _userService.GetIdByMail(entity.Mail);
        if (existingUser == Guid.Empty) return null;
        model.UserId = existingUser;
        FindAndDeleteExistingEmployee(model);
        var result = _employeeService.Add(model);
        var user = _userService.GetById(result.UserId);
        if (user == null) return null;
        var rm = new EmployeeRm
        {
            Id = result.Id,
            CompanyId = result.CompanyId,
            UserId = result.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Mail = user.Mail
        };
        return rm;
    }

    private void FindAndDeleteExistingEmployee(Employee employee)
    {
        var employees = _employeeService.GetAllAsNoTracking().Where(x =>
            x.UserId == employee.UserId).ToArray();
        if (!employees.Any()) return;
        foreach (var pc in employees)
            _employeeService.DeleteById(pc.Id, true);
    }

    public IEnumerable<EmployeeRm> GetAllNonDeletedEntries()
    {
        var all = _employeeService.GetAllAsNoTracking().ToList();
        var returnMap = _mapper.Map<IEnumerable<Employee>, List<EmployeeRm>>(all);
        return returnMap;
    }
}