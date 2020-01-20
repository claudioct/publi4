using Publi4.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Publi4.Domain.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<Publi4User>
    {
        Task<List<Publi4User>> GetAllWithRoles();
    }
}
