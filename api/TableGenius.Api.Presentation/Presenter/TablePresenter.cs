using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TableGenius.Api.Entities.Place;
using TableGenius.Api.Presentation.Interfaces.Presenter;
using TableGenius.Api.Presentation.ResourceModel;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Presentation.Presenter;

public class TablePresenter : BasePresenter<TableRm, Table>, ITablePresenter
{
    private readonly IAreaSlotService _areaSlotService;
    private readonly IMapper _mapper;
    private readonly ITableService _tableService;

    public TablePresenter(IMapper mapper, ITableService tableService, IAreaSlotService areaSlotService) : base(
        tableService, mapper)
    {
        _tableService = tableService;
        _areaSlotService = areaSlotService;
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


    public IEnumerable<TableWithStatusRm> GetAllAssignedTablesByAreaSlotAndCurrentDate(Guid areaSlotId,
        DateTime dateTime)
    {
        var allTakenTables = _tableService
            .GetAllAssignedTablesByAreaSlotAndCurrentDateAsNoTracking(areaSlotId, dateTime).ToList();
        var allTables = _tableService.GetAllAsNoTracking();
        var returnList = new List<TableWithStatusRm>();
        foreach (var table in allTables)
        {
            var tableRm = _mapper.Map<Table, TableWithStatusRm>(table);
            tableRm.Taken = allTakenTables.Any(x => x.Id == table.Id);
            returnList.Add(tableRm);
        }

        return returnList;
    }

    public IEnumerable<TableRm> GetFreeTablesByAreaSlotAndCurrentDate(Guid areaSlotId,
        DateTime dateTime)
    {
        var areaSlot = _areaSlotService.GetByIdAsNoTracking(areaSlotId);
        var allTakenTables = _tableService
            .GetAllAssignedTablesByAreaSlotAndCurrentDateAsNoTracking(areaSlotId, dateTime).ToList();
        var allTables = _tableService.GetAllAsNoTracking().Where(x => x.AreaId == areaSlot.AreaId);
        var allFreeTables = allTables.Where(x => allTakenTables.All(y => x.Id != y.Id)).OrderBy(x => x.TableNumber);
        var returnMap = _mapper.Map<IEnumerable<Table>, List<TableRm>>(allFreeTables);
        return returnMap;
    }
}