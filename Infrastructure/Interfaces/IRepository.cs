using Domain.Entities.Entity;

namespace Infrastructure.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IQueryable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(string id);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(string id);
}
