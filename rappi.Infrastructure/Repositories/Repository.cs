using Microsoft.EntityFrameworkCore;
using rappi.Application.Interfaces;
using rappi.Infrastructure.Data;
using System.Linq.Expressions;

namespace rappi.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _db;
    private readonly DbSet<T> _set;

    public Repository(AppDbContext db)
    {
        _db = db;
        _set = _db.Set<T>();
    }

    // Obtener todos
    public async Task<List<T>> GetAllAsync() => await _set.ToListAsync();

    // Obtener por id
    public async Task<T?> GetByIdAsync(int id) => await _set.FindAsync(id);

    // Crear
    public async Task<T> AddAsync(T entity)
    {
        await _set.AddAsync(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    // Actualizar
    public async Task<bool> UpdateAsync(T entity)
    {
        _set.Update(entity);
        return await _db.SaveChangesAsync() > 0;
    }

    // Eliminar
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _set.FindAsync(id);
        if (entity == null) return false;
        _set.Remove(entity);
        return await _db.SaveChangesAsync() > 0;
    }

    // Buscar con filtro
    public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate) =>
        await _set.Where(predicate).ToListAsync();
    
    public async Task<List<T>> GetAllIncludingAsync(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> q = _set;
        foreach (var inc in includes) q = q.Include(inc);
        return await q.ToListAsync();
    }

    public async Task<T?> GetByIdIncludingAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> q = _set;
        foreach (var inc in includes) q = q.Include(inc);
        return await q.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
    }

}