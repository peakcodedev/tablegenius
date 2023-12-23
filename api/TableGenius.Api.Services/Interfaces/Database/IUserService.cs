using System;
using System.IO;
using System.Threading.Tasks;
using TableGenius.Api.Entities.Admin;

namespace TableGenius.Api.Services.Interfaces.Database;

public interface IUserService : IDatabaseService<User>
{
    User GetByMail(string mail);
    Guid GetIdByMail(string mail);
    new User Add(User user);
    Task<string> UploadUserProfilePicture(Guid userId, MemoryStream profilePictureStream, string contentType);
}