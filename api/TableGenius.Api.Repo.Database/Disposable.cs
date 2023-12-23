using System;

namespace TableGenius.Api.Repo.Database;

public class Disposable : IDisposable
{
    private bool isDisposed;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public void Dispose(bool disposing)
    {
        if (!isDisposed && disposing) DisposeCore();
        isDisposed = true;
    }

    protected virtual void DisposeCore()
    {
    }
}