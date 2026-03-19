using Domain.Interfaces;
using Infrastracture.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastracture.Persistence.Repositories;

public class GenericRepository<TEntity>: IGenericRepository<TEntity> where TEntity :class
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<TEntity> _dbset;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbset = _context.Set<TEntity>();
    } 
    public async Task<TEntity> GetById(object id)
    {
        return await _dbset.FindAsync(id);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbset.ToListAsync();
    }

    public  void Update(TEntity entity) 
    {
        _dbset.Update(entity);
    }
    public async Task AddAsync(TEntity entity,CancellationToken cancellationToken)
    {
       await _dbset.AddAsync(entity, cancellationToken);
    }
    public void Delete(TEntity entity)
    {
        _dbset.Remove(entity);
    }

}
