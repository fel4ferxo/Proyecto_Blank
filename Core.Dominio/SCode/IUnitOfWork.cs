
namespace Core.Dominio
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Interfaz para implementar el patron Unidad de Trabajo
    /// </summary>
    public interface IUnitOfWork
        : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        void Attach<TEntity>(TEntity item) where TEntity : class;

        /// <summary>
        /// Commit todos los cambios
        /// </summary>
        int Commit();

        /// <summary>
        /// Commit todos los cambios de forma asincrona
        /// </summary>
        Task<int> CommitAsync();
        /// <summary>
        /// 
        /// </summary>
        void CommitAndRefreshChanges();


        /// <summary>
        /// Rollback cambios
        /// </summary>
        void RollbackChanges();

        /// <summary>
        /// Execute specific query with underliying persistence store
        /// </summary>
        /// <typeparam name="TEntity">Entity type to map query results</typeparam>
        /// <param name="sqlQuery">
        /// Dialect Query 
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
        /// </example>
        /// </param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>
        /// Enumerable results 
        /// </returns>
        IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters);

        /// <summary>
        /// Execute arbitrary command into underliying persistence store
        /// </summary>
        /// <param name="sqlCommand">
        /// Command to execute
        /// <example>
        /// SELECT idCustomer,Name FROM dbo.[Customers] WHERE idCustomer > {0}
        /// </example>
        ///</param>
        /// <param name="parameters">A vector of parameters values</param>
        /// <returns>The number of affected records</returns>
        int ExecuteCommand(string sqlCommand, params object[] parameters);
    }
}
