
namespace Core.Dominio
{
    using Core.Dominio.Entidades;
    using Core.Dominio.Specification;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface para implementar el Patron Repositorio
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IDisposable
        where TEntity : Entity
    {
        /// <summary>
        /// Unidad de Trabajo
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(Guid id);

        /// <summary>
        /// Obtener una Entidad de forma asincrona
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Guid id);

        /// <summary>
        /// Obtener una lista de todas las actividades
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Obtener una lista de todas las actividades de forma asincrona
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Buscar un elemento
        /// </summary>
        /// <param name="match">Expresion linq para filtrar</param>
        /// <returns></returns>
        TEntity Find(Expression<Func<TEntity, bool>> match);

        /// <summary>
        /// Buscar un elemento
        /// </summary>
        /// <param name="match">Expresion linq para filtrar</param>
        /// <returns></returns>
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);

        /// <summary>
        /// Busca todos los elementos que coincidan con el filtro
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Busca todos los elementos que coincidan con el filtro de forma asincrona
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Busca todos los elementos que coincidan con el filtro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter);

        IEnumerable<TEntity> GetFilteredWithoutStatus(Expression<Func<TEntity, bool>> filter);


        /// <summary>
        /// Busca todos los elementos que coincidan con el filtro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetFiltered(IEnumerable<Expression<Func<TEntity, bool>>> filters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetFilteredAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAllByFilter(FilterInfo filter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllByFilterAsync(FilterInfo filter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        IEnumerable<TEntity> AllMatching(ISpecification<TEntity> specification);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> AllMatchingAsync(ISpecification<TEntity> specification);

        /// <summary>
        /// Anyade una actividad al Repositorio
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        TEntity Add(TEntity item);

        /// <summary>
        /// Anyade una actividad al Repositorio
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        TEntity Add(TEntity item, string userCreacion);

        /// <summary>
        /// Guardar una lista de entidades
        /// </summary>
        /// <param name="tList"></param>
        /// <returns></returns>
        IEnumerable<TEntity> SavedAll(IEnumerable<TEntity> items);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"> </param>
        TEntity Remove(TEntity item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        IEnumerable<TEntity> RemoveAll(IEnumerable<TEntity> items);

        /// <summary>
        /// FUNCION QUE PONE EN "0" EL ESTADO PARA ELIMINAR UNA ENTIDAD
        /// </summary>
        /// <param name="item"></param>
        TEntity RemoveEstado(TEntity item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"> </param>
        void TrackItem(TEntity item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"> </param>
        bool isDetached(TEntity item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        TEntity ModifyAttach(TEntity item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        TEntity ModifyWithoutAttach(TEntity item);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="properties"></param>
        TEntity ModifyAttach(TEntity item, /*Expression<Func<TEntity, object>>[]*/
            IList<string> properties);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="properties"></param>
        TEntity ModifyWithoutAttach(TEntity item, /*Expression<Func<TEntity, object>>[]*/
            IList<string> properties);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="orders"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetPaged(IList<FilterInfo> filters, IList<OrderInfo> orders,
            int skip, int pageCount);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="orders"></param>
        /// <param name="skip"></param>
        /// <param name="pageCount"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetPagedAsync(IList<FilterInfo> filters, IList<OrderInfo> orders,
            int skip, int pageCount);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="KProperty"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount, int skip,
            System.Linq.Expressions.Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending,
            GridFilters filter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        int CountAll(IList<FilterInfo> filters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        Task<int> CountAllAsync(IList<FilterInfo> filters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        int CountAll(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<int> CountAllAsync(Expression<Func<TEntity, bool>> filter);

        /// <summary>
        /// Cuenta todas las entidades
        /// </summary>
        /// <returns></returns>
        int CountAll();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<int> CountAllAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="persisted"></param>
        /// <param name="current"></param>
        void Merge(TEntity persisted, TEntity current);

        /// <summary>
        /// Cuenta todas las entidades
        /// </summary>
        /// <returns></returns>
        int Commit();

    }
}
