﻿using System;
using Microsoft.AspNetCore.Authorization;

namespace TableGenius.Api.Web.Auth;

public class HasScopeRequirement : IAuthorizationRequirement
{
    public HasScopeRequirement(string scope)
    {
        Scope = scope ?? throw new ArgumentNullException(nameof(scope));
    }

    public string Scope { get; }
}