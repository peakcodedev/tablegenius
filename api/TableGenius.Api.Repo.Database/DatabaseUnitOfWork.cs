using System;
using System.Linq;
using TableGenius.Api.Entities.Admin;
using TableGenius.Api.Entities.Company;
using TableGenius.Api.Entities.Course;
using TableGenius.Api.Entities.General;
using TableGenius.Api.Entities.Project;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace TableGenius.Api.Repo.Database;

public class DatabaseUnitOfWork : Disposable, IDatabaseUnitOfWork
{
    private readonly IOptions<DatabaseOptions> _databaseOptions;
    private BaseRepository<Company> _companyRepository;
    private BaseRepository<Course> _courseRepository;
    private RepositoryContext _dataContext;
    private BaseRepository<Employee> _employeeRepository;
    private BaseRepository<Profession> _professionRepository;
    private BaseRepository<ProjectBooth> _projectBoothRepository;
    private BaseRepository<ProjectCollaboration> _projectCollaborationRepository;
    private BaseRepository<ProjectExpert> _projectExpertRepository;
    private BaseRepository<ProjectImage> _projectImageRepository;
    private BaseRepository<ProjectRating> _projectRatingRepository;
    private BaseRepository<Project> _projectRepository;
    private BaseRepository<ProjectUserRating> _projectUserRatingRepository;
    private BaseRepository<User> _userRepository;

    public DatabaseUnitOfWork(IOptions<DatabaseOptions> options)
    {
        UniqueId = Guid.NewGuid();
        _databaseOptions = options;
    }

    public DatabaseUnitOfWork(bool IsNew)
    {
        if (IsNew) _dataContext = new RepositoryContext(_databaseOptions);
    }

    public Guid UniqueId { get; set; }

    public void Detach<T>(T domainObj) where T : class
    {
        _dataContext.Entry(domainObj).State = EntityState.Detached;
    }

    public IDatabaseUnitOfWork GetCurrent()
    {
        _dataContext ??= _dataContext = new RepositoryContext(_databaseOptions);
        return this;
    }

    public void Refresh<T>(T domainObj) where T : class
    {
        _dataContext.Entry(domainObj).Reload();
    }

    public void RefreshContext()
    {
        var refreshableObjects = _dataContext.ChangeTracker.Entries().Select(c => c.Entity).ToList();
        foreach (var o in refreshableObjects) _dataContext.Entry(o).Reload();
    }

    public void Rollback()
    {
        _dataContext.Database.CurrentTransaction.Rollback();
    }

    public void Save()
    {
        _dataContext.SaveChanges();
    }

    public IBaseRepository<Project> ProjectRepository()
    {
        _projectRepository ??= new BaseRepository<Project>(_dataContext);
        return _projectRepository;
    }

    public IBaseRepository<ProjectImage> ProjectImageRepository()
    {
        _projectImageRepository ??= new BaseRepository<ProjectImage>(_dataContext);
        return _projectImageRepository;
    }


    public IBaseRepository<ProjectRating> ProjectRatingRepository()
    {
        _projectRatingRepository ??= new BaseRepository<ProjectRating>(_dataContext);
        return _projectRatingRepository;
    }

    public IBaseRepository<ProjectExpert> ProjectExpertRepository()
    {
        _projectExpertRepository ??= new BaseRepository<ProjectExpert>(_dataContext);
        return _projectExpertRepository;
    }


    public IBaseRepository<ProjectBooth> ProjectBoothRepository()
    {
        _projectBoothRepository ??= new BaseRepository<ProjectBooth>(_dataContext);
        return _projectBoothRepository;
    }

    public IBaseRepository<Company> CompanyRepository()
    {
        _companyRepository ??= new BaseRepository<Company>(_dataContext);
        return _companyRepository;
    }

    public IBaseRepository<User> UserRepository()
    {
        _userRepository ??= new BaseRepository<User>(_dataContext);
        return _userRepository;
    }

    public IBaseRepository<Profession> ProfessionRepository()
    {
        _professionRepository ??= new BaseRepository<Profession>(_dataContext);
        return _professionRepository;
    }

    public IBaseRepository<Employee> EmployeeRepository()
    {
        _employeeRepository ??= new BaseRepository<Employee>(_dataContext);
        return _employeeRepository;
    }

    public IBaseRepository<ProjectCollaboration> ProjectCollaborationRepository()
    {
        _projectCollaborationRepository ??= new BaseRepository<ProjectCollaboration>(_dataContext);
        return _projectCollaborationRepository;
    }

    public IBaseRepository<Course> CourseRepository()
    {
        _courseRepository ??= new BaseRepository<Course>(_dataContext);
        return _courseRepository;
    }

    public IBaseRepository<ProjectUserRating> ProjectUserRatingRepository()
    {
        _projectUserRatingRepository ??= new BaseRepository<ProjectUserRating>(_dataContext);
        return _projectUserRatingRepository;
    }
}