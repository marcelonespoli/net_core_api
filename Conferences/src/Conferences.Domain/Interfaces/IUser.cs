﻿using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Conferences.Domain.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}
