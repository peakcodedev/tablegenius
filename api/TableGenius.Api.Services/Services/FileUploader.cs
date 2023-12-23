using System;
using System.IO;
using System.Threading.Tasks;
using TableGenius.Api.Repo.BlobStorage.Interfaces;
using TableGenius.Api.Services.Interfaces;
using TableGenius.Api.Settings;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace TableGenius.Api.Services.Services;

public class FileUploader : IFileUploader
{
    private readonly IMinioClient _client;
    private readonly BlobStorageSettings _settings;

    public FileUploader(IBlobStorageApi blobStorageApi)
    {
        _client = blobStorageApi.GetClient();
        _settings = blobStorageApi.GetSettings();
    }

    public async Task<string> UploadFile(string folder, string name, MemoryStream memoryStream, string contentType)
    {
        memoryStream.Position = 0;
        try
        {
            await EnsureBucketExist();
            var putObjectArgs = new PutObjectArgs()
                .WithBucket(_settings.Bucket)
                .WithObject(folder + "/" + name)
                .WithObjectSize(memoryStream.Length)
                .WithStreamData(memoryStream)
                .WithContentType(contentType);
            await _client.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
            return "https://" + _settings.Endpoint + "/" + _settings.Bucket + "/" + folder + "/" + name;
        }
        catch (MinioException e)
        {
            Console.WriteLine("File Upload Error: {0}", e.Message);
            return "error";
        }
    }

    private async Task EnsureBucketExist()
    {
        var beArgs = new BucketExistsArgs()
            .WithBucket(_settings.Bucket);
        var found = await _client.BucketExistsAsync(beArgs).ConfigureAwait(false);
        if (!found)
        {
            var mbArgs = new MakeBucketArgs()
                .WithBucket(_settings.Bucket);
            await _client.MakeBucketAsync(mbArgs).ConfigureAwait(false);
        }
    }
}