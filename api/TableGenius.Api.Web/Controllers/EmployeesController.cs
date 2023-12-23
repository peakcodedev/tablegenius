using System;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;
using Microsoft.AspNetCore.Mvc;

namespace TableGenius.Api.Web.Controllers;

public class EmployeesController : DefaultController
{
    private readonly IEmployeePresenter _employeePresenter;

    public EmployeesController(IEmployeePresenter employeePresenter)
    {
        _employeePresenter = employeePresenter;
    }

    [HttpGet("{companyId}")]
    public JsonResult GetEmployeesOfCompany(Guid companyId)
    {
        var res = _employeePresenter.GetList(companyId);
        return Json(
            new DataJsonResult<EmployeeRm>(200, "employees successfully returned", res));
    }


    [HttpPost("{companyId}")]
    public JsonResult AddEmployee([FromBody] EmployeeModel employee, [FromRoute] Guid companyId)
    {
        employee.CompanyId = companyId;
        var res = _employeePresenter.Add(employee);
        return Json(res != null
            ? new SingleDataJsonResult<EmployeeRm>(200, "successfully added employee", res)
            : new InfoJsonResult(500, "Error on adding employee"));
    }

    [HttpDelete("{id}")]
    public JsonResult DeleteEmployee(Guid id)
    {
        var success = _employeePresenter.DeleteById(id, true);
        return Json(success
            ? new InfoJsonResult(200, "successfully deleted employee")
            : new InfoJsonResult(500, "Error on deleting employee"));
    }
}