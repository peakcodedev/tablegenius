using System.Collections.Generic;
using TableGenius.Api.Entities.Place;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;

namespace TableGenius.Api.Services.Services;

public class LocationService : DatabaseServiceBase<Location>, ILocationService
{
    private readonly ILocationAssignmentRepository _locationAssignmentRepository;

    public LocationService(ILocationRepository locationRepository, IApplicationLogger logger,
        ILocationAssignmentRepository locationAssignmentRepository) :
        base(logger)
    {
        _repository = locationRepository;
        _locationAssignmentRepository = locationAssignmentRepository;
    }

    public IEnumerable<Location> GetAllLocationsByMailAsNoTracking(string mail)
    {
        return _locationAssignmentRepository.GetAllLocationsByMailAsNoTracking(mail);
    }
}