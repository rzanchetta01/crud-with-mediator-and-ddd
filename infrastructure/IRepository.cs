using System.Linq.Expressions;
using domain.Entities;

namespace infrastructure;

public interface IRepository<T> where T : BaseEntity
{

        /// <summary>
        /// Get async entity by identifier 
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>Entity</returns>
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Get all entities in collection
        /// </summary>
        /// <param name="cancellationToken">CancellationToken</param>
        /// <returns>collection of entities</returns>
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Async Insert entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="cancellationToken">CancellationToken</param>
        Task InsertAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// Async Insert entities
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <param name="cancellationToken">CancellationToken</param>
        Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Async Update entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="cancellationToken">CancellationToken</param>
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        
        /// <summary>
        /// Async Update entities
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <param name="cancellationToken">CancellationToken</param>
        Task UpdateAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

        ///<summary>
        /// Async Delete entity
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="cancellationToken">CancellationToken</param>
        Task DeleteAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// Async Delete entities
        /// </summary>
        /// <param name="entities">Entities</param>
        /// <param name="cancellationToken">CancellationToken</param>
        Task DeleteAsync(IEnumerable<T> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<T> Table();

}