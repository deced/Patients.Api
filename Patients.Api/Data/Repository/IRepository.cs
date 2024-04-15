using System.Linq.Expressions;
using Patients.Api.Data.Entities;

namespace Patients.Api.Data.Repository;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    Task InsertOneAsync(TEntity entity);
    Task InsertManyAsync(ICollection<TEntity> entities);

    Task<TEntity?> GetByIdAsync(Guid guid);
    Task<List<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> sortOrder);
    Task<List<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> filterExpression, Expression<Func<TEntity, object>> sortOrder, int skip, int take);

    Task UpdateOneAsync(TEntity entity);

    Task<TEntity?> DeleteOneByIdAsync(Guid guid);
}