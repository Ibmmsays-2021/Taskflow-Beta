using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces;

public interface IUnitOfWork: IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
