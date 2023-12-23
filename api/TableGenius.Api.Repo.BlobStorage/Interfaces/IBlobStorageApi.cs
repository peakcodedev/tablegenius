using TableGenius.Api.Settings;
using Minio;

namespace TableGenius.Api.Repo.BlobStorage.Interfaces;

public interface IBlobStorageApi
{
    IMinioClient GetClient();
    BlobStorageSettings GetSettings();
}