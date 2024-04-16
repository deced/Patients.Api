using System.Linq.Expressions;
using MongoDB.Driver;
using Patients.Api.Data.Entities;

namespace Patients.Api.Data.Repository;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    Task InsertOneAsync(TEntity entity);
    Task InsertManyAsync(ICollection<TEntity> entities);

    Task<TEntity?> GetByIdAsync(Guid guid);
    Task<List<TEntity>> FilterByAsync(FilterDefinition<TEntity> filterDefinition, Expression<Func<TEntity, object>> sortOrder);

    Task UpdateOneAsync(TEntity entity);

    Task<TEntity?> DeleteOneByIdAsync(Guid guid);
}