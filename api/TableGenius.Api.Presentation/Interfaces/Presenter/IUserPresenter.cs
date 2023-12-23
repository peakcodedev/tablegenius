using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TableGenius.Api.Presentation.ResourceModel;

namespace TableGenius.Api.Presentation.Interfaces.Presenter;

public interface IUserPresenter : IPresenter<UserRM>
{
    UserRM Update(UserRM entity);
    UserRM Add(UserRM entity);
    UserRM GetByMail(string mail);
    IEnumerable<UserRM> GetList();
    UserRelationsRm GetUserRelations(Guid userId);
    Task<string> UploadProfilePicture(Guid userId, MemoryStream profileImageStream, string contentType);
}