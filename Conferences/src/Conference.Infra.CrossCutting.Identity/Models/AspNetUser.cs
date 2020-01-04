using Conferences.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Conference.Infra.CrossCutting.Identity.Models
{
    public class AspNetUser : IUser
    {
        public string Name => "User test - (user implementation TODO)";

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            throw new NotImplementedException();
        }

        public Guid GetUserId()
        {
            return new Guid("3151A60E-BC14-400F-8BFB-A815B5466961");
        }

        public bool IsAuthenticated()
        {
            return true;
        }
    }
}
