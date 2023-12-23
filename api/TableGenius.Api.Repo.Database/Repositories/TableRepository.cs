﻿using TableGenius.Api.Entities.Place;
using TableGenius.Api.Repo.Database.Interfaces;

namespace TableGenius.Api.Repo.Database.Repositories;

public class TableRepository(RepositoryContext dataContext) : BaseRepository<Table>(dataContext), ITableRepository
{
}