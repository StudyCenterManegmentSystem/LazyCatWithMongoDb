using Domain.Entities.Entity;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly IMongoCollection<TEntity> _collection;

        public Repository(IMongoCollection<TEntity> collection)
        {
            _collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public async Task AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, entity.Id);
            await _collection.DeleteOneAsync(filter);
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            var entities = await _collection.FindAsync(_ => true);
            return entities.ToEnumerable().AsQueryable();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var filter = Builders<TEntity>.Filter.Eq(doc => doc.Id, entity.Id);
            await _collection.ReplaceOneAsync(filter, entity);
        }
    }
}
