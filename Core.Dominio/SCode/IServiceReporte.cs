
namespace Core.Dominio
{
    using Core.Dominio.Entidades;
    using Core.Dominio.Specification;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IServiceReporte<TEntity> where TEntity : EntityReporte
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
            where TEntityStoreProcedure : EntityReporte
            where TEntityFilter : FilterReporte;

        IEnumerable<TEntityStoreProcedure>
            GetAllStoreProcedureOracle<TEntityStoreProcedure>(string storeProcedureName)
            where TEntityStoreProcedure : EntityReporte;

        IEnumerable<TEntityStoreProcedure>
            GetAllStoreProcedureOracle<TEntityStoreProcedure, TEntityFilter>
            (string storeProcedureName, TEntityFilter parameters)
            where TEntityStoreProcedure : EntityReporte
            where TEntityFilter : FilterReporte;

        IEnumerable<TEntityStoreProcedure>
            GetAllStoreProcedure<TEntityStoreProcedure>(string storeProcedureName)
            where TEntityStoreProcedure : EntityReporte;

        IEnumerable<TEntityStoreProcedure>
            GetAllStoreProcedureEntity<TEntityStoreProcedure, TEntityFilter>
            (string storeProcedureName, TEntityFilter parameters)
            where TEntityStoreProcedure : Entity
            where TEntityFilter : FilterReporte;

        IEnumerable<TEntityStoreProcedure>
            GetAllStoreProcedureEntity<TEntityStoreProcedure>(string storeProcedureName)
            where TEntityStoreProcedure : Entity;

        int
            ExecuteStoredProcedure<TEntityFilter>
            (string storeProcedureName, TEntityFilter parameters)
            where TEntityFilter : FilterReporte;


        //  byte[] GenerarExcel(String titulo, IEnumerable<String> header, IEnumerable<SegComItemReporte> body);

    }
}
