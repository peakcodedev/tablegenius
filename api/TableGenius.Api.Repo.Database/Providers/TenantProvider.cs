using System;
using Microsoft.AspNetCore.Http;

namespace TableGenius.Api.Repo.Database.Providers;

public sealed class TenantProvider
{
    private const string TenantIdHeaderName = "X-TenantId";

    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetTenantId()
    {
        if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null) return Guid.Empty;
        var header = _httpContextAccessor
            .HttpContext
            .Request
            .Headers[TenantIdHeaderName];
        try
        {
            return string.IsNullOrEmpty(header) ? Guid.Empty : Guid.Parse(header);
        }
        catch
        {
            return Guid.Empty;
        }
    }
}