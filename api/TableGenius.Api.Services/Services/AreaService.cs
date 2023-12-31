﻿using TableGenius.Api.Entities.Place;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Services.Services;

public class AreaService : DatabaseServiceTenantBase<Area>, IAreaService
{
    public AreaService(IAreaRepository areaRepository, IApplicationLogger logger) :
        base(logger)
    {
        Repository = areaRepository;
    }
}