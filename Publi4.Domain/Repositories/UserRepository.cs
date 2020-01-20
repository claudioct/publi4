using Microsoft.EntityFrameworkCore;
using Publi4.Domain.Entities;
using Publi4.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Publi4.Domain.Repositories
{
    public class UserRepository : GenericRepository<Publi4User>, IUserRepository
    {
        public UserRepository(Publi4DbContext context) : base(context)
        {
        }

        public async Task<List<Publi4User>> GetAllWithRoles()
        {
            return await Context
                .Users
                .Include(u => u.Roles)
                    .ThenInclude(x => x.Role)
                .Include(u => u.Company)
                .AsNoTracking()
                .ToListAsync();

        }
    }
}
