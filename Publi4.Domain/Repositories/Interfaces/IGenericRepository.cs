using Publi4.Domain.Entities;
using System;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Publi4.Domain.Repositories.Interfaces
{
    public interface IGenericRepository<T>
     where T : class
    {
        IQueryable<T> GetAll();
        Task<T> GetById<TKey>(TKey id);
        Task Create(T entity);
        Task Update<TKey>(TKey id, T entity);
        Task Delete<TKey>(TKey id);
    }
}
