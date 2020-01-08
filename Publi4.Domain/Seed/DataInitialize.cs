using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Publi4.Domain.Seed
{
    public static class DataInitialize
    {
        public static IHost ExecuteMigration(this IHost host)
        {
            return Seed.ApplicationSeedData.Execute(host);
        }
    }
}
