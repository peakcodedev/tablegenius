using System;
using System.IO;
using System.Threading.Tasks;
using TableGenius.Api.Entities.Company;

namespace TableGenius.Api.Services.Interfaces.Database;

public interface ICompanyService : IDatabaseService<Company>
{
    Task<string> UploadCompanyPicture(Guid companyId, MemoryStream companyPictureStream, string contentType);
    Company GetByUserId(Guid userId);
}