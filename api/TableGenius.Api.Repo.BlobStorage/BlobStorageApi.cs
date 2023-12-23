using System;
using TableGenius.Api.Repo.BlobStorage.Interfaces;
using TableGenius.Api.Settings;
using Microsoft.Extensions.Options;
using Minio;

namespace TableGenius.Api.Repo.BlobStorage;

public class BlobStorageApi : IBlobStorageApi
{
    private readonly BlobStorageSettings _settings;
    private IMinioClient _minioClient;

    public BlobStorageApi(IOptions<BlobStorageSettings> blobStorageSettings)
    {
        _settings = blobStorageSettings.Value;
        TryAndSetService();
    }

    public IMinioClient GetClient()
    {
        return _minioClient ?? null;
    }

    public BlobStorageSettings GetSettings()
    {
        return _settings ?? null;
    }

    private void TryAndSetService()
    {
        try
        {
            _minioClient = new MinioClient().WithEndpoint(_settings.Endpoint).WithSSL()
                .WithCredentials(_settings.User, _settings.Password).Build();
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}