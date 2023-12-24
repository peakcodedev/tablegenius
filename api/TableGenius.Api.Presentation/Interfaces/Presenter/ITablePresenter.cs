﻿using System.Collections.Generic;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface ITablePresenter : IPresenter<TableRm>
{
    TableRm Add(TableRm entity);
    IEnumerable<TableRm> GetList();
    TableRm Update(TableRm entity);
}