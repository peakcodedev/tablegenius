using System.IO;
using System.Threading.Tasks;

namespace TableGenius.Api.Services.Interfaces;

public interface IFileUploader
{
    Task<string> UploadFile(string fileName, string name, MemoryStream memoryStream, string contentType);
}