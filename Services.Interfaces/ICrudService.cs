﻿using Models;

namespace Services.Interfaces
{
    public interface ICrudService<T> where T : Entity
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> ReadAsync();
        Task<T?> ReadAsync(int id);
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}