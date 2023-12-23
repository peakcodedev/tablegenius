using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TableGenius.Api.Entities.Admin;
using TableGenius.Api.Infrastructure.Interfaces;
using TableGenius.Api.Repo.Database.Interfaces;
using TableGenius.Api.Services.Interfaces;
using TableGenius.Api.Services.Interfaces.Database;
using TableGenius.Api.Services.Queries;

namespace TableGenius.Api.Services.Services;

public class UserService : DatabaseServiceBase<User>, IUserService
{
    private readonly IFileUploader _fileUploader;

    public UserService(IDatabaseUnitOfWorkFactory unitOfWorkFactory, IApplicationLogger logger,
        IFileUploader fileUploader) : base(logger)
    {
        DatabaseUnitOfWork = unitOfWorkFactory.Create();
        Querier = new UserQueries(DatabaseUnitOfWork);
        _fileUploader = fileUploader;
    }

    public User GetByMail(string mail)
    {
        return GetAllAsNoTracking().SingleOrDefault(x => x.Mail == mail);
    }

    public Guid GetIdByMail(string mail)
    {
        var user = GetAllAsNoTracking().SingleOrDefault(x => x.Mail == mail);
        return user?.Id ?? Guid.Empty;
    }

    public new User Add(User user)
    {
        return base.Add(user);
    }

    public async Task<string> UploadUserProfilePicture(Guid userId, MemoryStream profilePictureStream,
        string contentType)
    {
        var user = GetById(userId);
        var fileName = user.Id.ToString("N");
        const string folder = "profileImage";
        var profileImageUrl =
            await _fileUploader.UploadFile(folder, fileName, profilePictureStream, contentType);
        user.ProfileImage = fileName;
        Update(user);
        return profileImageUrl;
    }
}