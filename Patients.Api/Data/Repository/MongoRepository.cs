using System.Linq.Expressions;
using System.Reflection;
using MongoDB.Driver;
using Patients.Api.Attributes;
using Patients.Api.Configuration;
using Patients.Api.Data.Entities;

namespace Patients.Api.Data.Repository;

public class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : EntityBase
{
    private readonly IMongoCollection<TEntity> _collection;
    
    public MongoRepository()
    {
        var connectionString = AppConfiguration.ConnectionString;
        var databaseName = AppConfiguration.DatabaseName;
        var collectionName = GetCollectionName<TEntity>();
        
        var database = new MongoClient(connectionString).GetDatabase(databaseName);
        _collection = database.GetCollection<TEntity>(collectionName);

    }
    
    public Task InsertOneAsync(TEntity entity)
    {
        return _collection.InsertOneAsync(entity);
    }

    public Task InsertManyAsync(ICollection<TEntity> entities)
    {
        if (entities.Count > 0) 
            return _collection.InsertManyAsync(entities);
        
        return Task.CompletedTask;
    }

    public Task<TEntity?> GetByIdAsync(Guid guid)
    {
        return _collection.Find(x => x.Id == guid).FirstOrDefaultAsync()!;
    }
    
    public Task<List<TEntity>> FilterByAsync(FilterDefinition<TEntity> filterDefinition,
        Expression<Func<TEntity, object>> sortOrder)
    {
        return _collection.Find(filterDefinition).SortBy(sortOrder).ToListAsync();
    }

    public Task UpdateOneAsync(TEntity entity)
    {
        return _collection.FindOneAndReplaceAsync(x => x.Id == entity.Id, entity);
    }

    public Task<TEntity?> DeleteOneByIdAsync(Guid guid)
    {
        return _collection.FindOneAndDeleteAsync(x => x.Id == guid)!;
    }
    
    private static string GetCollectionName<T>()
    {
        var type = typeof(T);
        var attribute = type.GetCustomAttribute<MongoCollectionNameAttribute>();
        if (attribute == null)
            throw new Exception($"BsonCollectionAttribute is not specified for entity {type.Name}");

        return attribute.CollectionName;
    }
}