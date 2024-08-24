
namespace Core.Dominio
{
    using Core.Dominio.Entidades;
    using Core.Dominio.Specification;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IService<TEntity> where TEntity : Entity
    {
        TEntity Get(Guid id);

        Task<TEntity> GetAsync(Guid id);

        IEnumerable<TEntity> GetAll();

        Task<IEnumerable<TEntity>> GetAllAsync();

        TEntity Find(Expression<Func<TEntity, bool>> match);

        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);

        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter);

        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter);

        IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter);

        Task<IEnumerable<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> filter);

        IEnumerable<TEntity> GetFiltered(IEnumerable<Expression<Func<TEntity, bool>>> filters);

        Task<IEnumerable<TEntity>> GetFilteredAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters);

        IEnumerable<TEntity> GetAllByFilter(FilterInfo filter);

        Task<IEnumerable<TEntity>> GetAllByFilterAsync(FilterInfo filter);

        IEnumerable<TEntity> AllMatching(ISpecification<TEntity> specification);

        Task<IEnumerable<TEntity>> AllMatchingAsync(ISpecification<TEntity> specification);

        TEntity Insert(TEntity entity);

        Task<TEntity> InsertAsync(TEntity entity);

        IEnumerable<TEntity> SavedAll(IList<TEntity> items);

        Task<IEnumerable<TEntity>> SavedAllAsync(IList<TEntity> items);

        TEntity Delete(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);

        IEnumerable<TEntity> DeleteAll(IList<TEntity> items);

        Task<IEnumerable<TEntity>> DeleteAllAsync(IList<TEntity> items);

        TEntity DeleteEstado(TEntity entity);

        Task<TEntity> DeleteEstadoAsync(TEntity entity);

        Guid DeleteVirtualById(Guid id);

        Task<Guid> DeleteVirtualByIdAsync(Guid id);

        TEntity UpdateAttach(TEntity entity);

        Task<TEntity> UpdateAttachAsync(TEntity entity);

        TEntity UpdateWithoutAttach(TEntity entity);

        Task<TEntity> UpdateWithoutAttachAsync(TEntity entity);

        TEntity UpdateAttach(TEntity entity, /*Expression<Func<TEntity, object>>[]*/
            IList<string> properties);

        Task<TEntity> UpdateAttachAsync(TEntity entity, /*Expression<Func<TEntity, object>>[]*/
            IList<string> properties);

        TEntity UpdateWithoutAttach(TEntity entity, /*Expression<Func<TEntity, object>>[]*/
            IList<string> properties);

        Task<TEntity> UpdateWithoutAttachAsync(TEntity entity, /*Expression<Func<TEntity, object>>[]*/
            IList<string> properties);

        IEnumerable<TEntity> GetPaged(IList<FilterInfo> filters, IList<OrderInfo> orders,
            int skip, int pageCount);

        Task<IEnumerable<TEntity>> GetPagedAsync(IList<FilterInfo> filters, IList<OrderInfo> orders,
            int skip, int pageCount);

        int CountAll(IList<FilterInfo> filters);

        Task<int> CountAllAsync(IList<FilterInfo> filters);

        int CountAll(Expression<Func<TEntity, bool>> filter);

        Task<int> CountAllAsync(Expression<Func<TEntity, bool>> filter);

        int CountAll();

        Task<int> CountAllAsync();

        IEnumerable<TEntityStoreProcedure>
            GetAllStoreProcedure<TEntityStoreProcedure, TEntityFilter>
            (string storeProcedureName, TEntityFilter parameters)
            where TEntityStoreProcedure : Entity
            where TEntityFilter : FilterEntidad;

        IEnumerable<TEntityStoreProcedure>
            GetAllStoreProcedure<TEntityStoreProcedure>(string storeProcedureName)
            where TEntityStoreProcedure : Entity;

        void commit();
    }
}
