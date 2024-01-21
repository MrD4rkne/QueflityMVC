namespace QueflityMVC.Domain.Common
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Add entity
        /// </summary>
        /// <param name="entityToAdd">Entity to be added</param>
        /// <returns>Entity's id</returns>
        /// <exception cref="ArgumentNullException">Entity is null</exception>
        int Add(T entityToAdd);

        /// <summary>
        /// Delete entity by Id
        /// </summary>
        /// <param name="entityToDeleteId">Entity's id</param>
        void Delete(int entityToDeleteId);

        /// <summary>
        /// Delete entity by Entity.Id
        /// </summary>
        /// <param name="entityToDelete">Entity to be deleted</param>
        /// <exception cref="ArgumentException">Entity is null</exception>
        void Delete(T entityToDelete);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entityToUpdate">Entity to be updated</param>
        /// <returns>Updated entity</returns>
        /// <exception cref="ArgumentException">Entity does not exist</exception>
        T? Update(T entityToUpdate);

        /// <summary>
        /// Whether Entity exists
        /// </summary>
        /// <param name="entityToCheck">Entity to be checked</param>
        /// <returns>True if exists, otherwise False</returns>
        /// <exception cref="ArgumentNullException">Entity is null</exception>
        bool Exists(T entityToCheck);

        /// <summary>
        /// Whether Entity exists
        /// </summary>
        /// <param name="entityId">Entity's to be checked id</param>
        /// <returns>True if exists, otherwise False</returns>
        bool Exists(int entityId);

        /// <summary>
        /// Get Entity by Id
        /// </summary>
        /// <param name="entityId">Entity's id</param>
        /// <returns>Entity or null if it doe not exist</returns>
        T? GetById(int entityId);

        /// <summary>
        /// Get all entities of type
        /// </summary>
        /// <returns>All entities</returns>
        IQueryable<T> GetAll();
    }
}