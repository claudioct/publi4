using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Publi4.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Publi4.Helpers
{
    public static class UserHelper
    {
        public static string GetUserFirstRole(ClaimsPrincipal userClaimsPrincipal)
        {
            return userClaimsPrincipal.Claims
                           .Where(c => c.Type == ClaimTypes.Role)
                           .Select(c => c.Value)
                           .ToList()[0];
        }
    }
}
