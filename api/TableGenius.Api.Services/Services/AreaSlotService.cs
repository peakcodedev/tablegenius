﻿using TableGenius.Api.Entities.Place;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Services.Services;

public class AreaSlotService : DatabaseServiceTenantBase<AreaSlot>, IAreaSlotService
{
    public AreaSlotService(IAreaSlotRepository areaSlotRepository, IApplicationLogger logger) :
        base(logger)
    {
        Repository = areaSlotRepository;
    }
}