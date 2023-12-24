﻿using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TableGenius.Api.Entities.Place;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class TablePresenter : BasePresenter<TableRm, Table>, ITablePresenter
{
    private readonly IMapper _mapper;
    private readonly ITableService _tableService;

    public TablePresenter(IMapper mapper, ITableService tableService) : base(
        tableService, mapper)
    {
        _tableService = tableService;
        _mapper = mapper;
    }

    public TableRm Add(TableRm entity)
    {
        var model = _mapper.Map<Table>(entity);
        var result = _tableService.Add(model);
        return _mapper.Map<TableRm>(result);
    }

    public IEnumerable<TableRm> GetList()
    {
        var all = _tableService.GetAllAsNoTracking().ToList();
        var returnMap = _mapper.Map<IEnumerable<Table>, List<TableRm>>(all);
        return returnMap;
    }

    public TableRm Update(TableRm entity)
    {
        var db = _mapper.Map<TableRm, Table>(entity);
        var elem = _tableService.Update(db);
        return _mapper.Map<Table, TableRm>(elem);
    }
}