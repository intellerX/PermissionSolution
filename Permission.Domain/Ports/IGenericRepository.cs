﻿using System.Linq.Expressions;

namespace Permission.Domain.Ports
{
    public interface IGenericRepository<E> : IDisposable
       where E : Permission.Domain.Entities.BaseEntity
    {
        Task<IEnumerable<E>> GetAsync(Expression<Func<E, bool>>? filter = null,
                    Func<IQueryable<E>, IOrderedQueryable<E>>? orderBy = null, string includeStringProperties = "",
                    bool isTracking = false);

        Task<IEnumerable<E>> GetAsync(Expression<Func<E, bool>>? filter = null,
            Func<IQueryable<E>, IOrderedQueryable<E>>? orderBy = null,
             bool isTracking = false, params Expression<Func<E, object>>[] includeObjectProperties);

        Task<E> GetByIdAsync(object id);
        Task<E> AddAsync(E entity);
        Task UpdateAsync(E entity);
        Task DeleteAsync(E entity);
    }
}
