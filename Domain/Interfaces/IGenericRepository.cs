using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetById(object id);
    Task<List<TEntity>> GetAllAsync();
    void Update(TEntity entity);
    Task AddAsync(TEntity entity, CancellationToken cancellationToken);
    void Delete(TEntity entity);

}
