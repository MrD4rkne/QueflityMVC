using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    /// <summary>
    /// Add entity
    /// </summary>
    /// <param name="entityToAdd">Entity to be added</param>
    /// <returns>Entity's id</returns>
    /// <exception cref="ArgumentNullException">Entity is null</exception>
    Task<int> AddAsync(T entityToAdd);

    /// <summary>
    /// Delete entity by Id
    /// </summary>
    /// <param name="entityToDeleteId">Entity's id</param>
    Task DeleteAsync(int entityToDeleteId);

    /// <summary>
    /// Delete entity by Entity.Id
    /// </summary>
    /// <param name="entityToDelete">Entity to be deleted</param>
    /// <exception cref="ArgumentException">Entity is null</exception>
    Task DeleteAsync(T entityToDelete);

    /// <summary>
    /// Update entity
    /// </summary>
    /// <param name="entityToUpdate">Entity to be updated</param>
    /// <returns>Updated entity</returns>
    /// <exception cref="ArgumentException">Entity does not exist</exception>
    Task<T> UpdateAsync(T entityToUpdate);

    /// <summary>
    /// Whether Entity exists
    /// </summary>
    /// <param name="entityToCheck">Entity to be checked</param>
    /// <returns>True if exists, otherwise False</returns>
    /// <exception cref="ArgumentNullException">Entity is null</exception>
    Task<bool> ExistsAsync(T entityToCheck);

    /// <summary>
    /// Whether Entity exists
    /// </summary>
    /// <param name="entityId">Entity's to be checked id</param>
    /// <returns>True if exists, otherwise False</returns>
    Task<bool> ExistsAsync(int entityId);

    /// <summary>
    /// Get Entity by Id
    /// </summary>
    /// <param name="entityId">Entity's id</param>
    /// <returns>Entity or null if it doe not exist</returns>
    Task<T?> GetByIdAsync(int entityId);

    /// <summary>
    /// Get all entities of type
    /// </summary>
    /// <returns>All entities</returns>
    IQueryable<T> GetAll();
}