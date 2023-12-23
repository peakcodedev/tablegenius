using System;
using Microsoft.AspNetCore.Mvc;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Web.Default;

namespace TableGenius.Api.Web.Controllers;

public class TablesController : DefaultController
{
    private readonly ITablePresenter _tablePresenter;

    public TablesController(ITablePresenter tablePresenter)
    {
        _tablePresenter = tablePresenter;
    }

    //[Authorize("admin")]
    [HttpGet]
    public JsonResult GetAll()
    {
        var res = _tablePresenter.GetList();
        return Json(new DataJsonResult<TableRm>(200, "tables successfully returned", res));
    }


    [HttpPost]
    public JsonResult Add(TableRm table)
    {
        var res = _tablePresenter.Add(table);
        return Json(res != null
            ? new SingleDataJsonResult<TableRm>(200, "successfully added table", res)
            : new InfoJsonResult(500, "Error on adding table"));
    }

    //[Authorize("admin")]
    [HttpDelete("{id}")]
    public JsonResult Delete(Guid id)
    {
        var success = _tablePresenter.DeleteById(id);
        return Json(success
            ? new InfoJsonResult(200, "successfully deleted table")
            : new InfoJsonResult(500, "Error on deleting table"));
    }

    [HttpPatch("{Id}")]
    public JsonResult Update([FromRoute] Guid id, [FromBody] TableRm tableRm)
    {
        tableRm.Id = id;
        var res = _tablePresenter.Update(tableRm);
        return Json(res != null
            ? new SingleDataJsonResult<TableRm>(200, "table successfully returned", res)
            : new InfoJsonResult(500, "Error on updating table"));
    }

    [HttpGet("{id}")]
    public JsonResult Get(Guid id)
    {
        var res = _tablePresenter.GetById(id);
        return Json(new SingleDataJsonResult<TableRm>(200, "table successfully returned", res));
    }
}