using Publi4.Domain.Entities;
using Publi4.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publi4.Domain.Repositories
{
    public class CompanyRepository : GenericRepository<CompanyEntity>, ICompanyRepository
    {
        public CompanyRepository(Publi4DbContext context) : base(context)
        {
        }
    }
}
