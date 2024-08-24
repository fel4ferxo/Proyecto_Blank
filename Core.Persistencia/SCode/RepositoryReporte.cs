
namespace Core.Persistencia
{
    using Core.Dominio;
    using Core.Dominio.Entidades;
    using Core.Dominio.Specification;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;
    using System.Threading.Tasks;
    using System.Linq.Expressions;

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class RepositoryReporte<TEntity> : IRepositoryReporte<TEntity>
        where TEntity : EntityReporte
    {
        #region Members

        IQueryableUnitOfWork _UnitOfWork;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor que crea una instancia de un repositorio
        /// </summary>
        /// <param name="unitOfWork">Asociado a una Unidad de Trabajo</param>
        public RepositoryReporte(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == (IUnitOfWork)null)
                throw new ArgumentNullException("unitOfWork");

            _UnitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository Members

        /// <summary>
        /// 
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _UnitOfWork;
            }
        }

        /// <summary>
        /// Obtener una Entidad
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity Get(Guid id)
        {
            if (id != null && id != Guid.Empty)
            {
                var rpta = GetSet().AsNoTracking().Where(e => e.ESTADO == true).SingleOrDefault(s => s.Id == id);
                if (rpta == null) throw new KeyNotFoundException("El Identificador enviado no existe actualmente"
                    + id.ToString());
                return rpta;
            }
            else
                throw new ArgumentNullException("Parametro null");
        }
        /// <summary>
        /// Obtener una Entidad de forma asincrona
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            if (id != null && id != Guid.Empty)
            {
                var rpta = await GetSet().AsNoTracking().Where(e => e.ESTADO == true).SingleOrDefaultAsync(s => s.Id == id);
                if (rpta == null) throw new KeyNotFoundException("El Identificador enviado no existe actualmente"
                    + id.ToString());
                return rpta;
            }
            else
                throw new ArgumentNullException("Parametro null");
        }

        /// <summary>
        /// Obtener una lista de todas las actividades
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return GetSet().AsNoTracking().Where(e => e.ESTADO == true);
        }

        /// <summary>
        /// Obtener una lista de todas las actividades de forma asincrona
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await GetSet().AsNoTracking().Where(e => e.ESTADO == true).ToListAsync();
        }

        /// <summary>
        /// Buscar un elemento
        /// </summary>
        /// <param name="match">Expresion linq para filtrar</param>
        /// <returns></returns>
        public virtual TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return GetSet().AsNoTracking().Where(e => e.ESTADO == true).SingleOrDefault(match);
        }

        /// <summary>
        /// Buscar un elemento
        /// </summary>
        /// <param name="match">Expresion linq para filtrar</param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await GetSet().AsNoTracking().Where(e => e.ESTADO == true).SingleOrDefaultAsync(match);
        }

        /// <summary>
        /// Busca todos los elementos que coincidan con el filtro
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter)
        {
            return GetSet().AsNoTracking().Where(e => e.ESTADO == true).Where(filter);
        }

        /// <summary>
        /// Busca todos los elementos que coincidan con el filtro de forma asincrona
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await GetSet().AsNoTracking().Where(e => e.ESTADO == true).Where(filter).ToListAsync();
        }

        /// <summary>
        /// Busca todos los elementos que coincidan con el filtro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter)
        {
            return GetSet().AsNoTracking().Where(e => e.ESTADO == true).Where(filter);
        }

        /// <summary>
        /// Busca todos los elementos que coincidan con el filtro
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await GetSet().AsNoTracking().Where(e => e.ESTADO == true).Where(filter).ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetFiltered(IEnumerable<Expression<Func<TEntity, bool>>> filters)
        {
            var rpta = GetSet().AsNoTracking().Where(e => e.ESTADO == true);
            foreach (var filter in filters)
            {
                rpta = rpta.Where(filter);
            }
            return rpta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetFilteredAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters)
        {
            var rpta = GetSet().AsNoTracking().Where(e => e.ESTADO == true);
            foreach (var filter in filters)
            {
                rpta = rpta.Where(filter);
            }
            return await rpta.ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetAllByFilter(FilterInfo filter)
        {
            var queryable = GetSet().AsNoTracking().Where(e => e.ESTADO == true); //_repository.GetStudents();

            var predicate = SimpleHandler<TEntity>.BuildPredicateFilter(filter);
            if (predicate != null)
                return queryable.Where(predicate);
            else
                return Enumerable.Empty<TEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllByFilterAsync(FilterInfo filter)
        {
            var queryable = GetSet().AsNoTracking().Where(e => e.ESTADO == true); //_repository.GetStudents();

            var predicate = SimpleHandler<TEntity>.BuildPredicateFilter(filter);
            if (predicate != null)
                return await queryable.Where(predicate).ToListAsync();
            else
                return Enumerable.Empty<TEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> AllMatching(ISpecification<TEntity> specification)
        {
            return GetSet().AsNoTracking().Where(specification.SatisfiedBy());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> AllMatchingAsync(ISpecification<TEntity> specification)
        {
            return await GetSet().AsNoTracking().Where(specification.SatisfiedBy()).ToListAsync();
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="orders"></param>
        /// <param name="index"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetPaged(IList<FilterInfo> filters, IList<OrderInfo> orders,
            int skip, int pageCount)
        {
            var queryable = GetSet().AsNoTracking().Where(e => e.ESTADO == true); //_repository.GetStudents();

            var predicate = SimpleHandler<TEntity>.BuildPredicate(filters);
            var filtered = queryable;
            if (filters.Count > 0)
                if (predicate != null)
                    filtered = queryable.Where(predicate);
                else
                    return Enumerable.Empty<TEntity>();

            //if (orders.Count > 0)
            var ordered = OrderHelper.GetOrderedQueryable<TEntity>(filtered, orders);

            var pagination = ordered.Skip(skip).Take(pageCount);
            return pagination;
        }

        public virtual async Task<IEnumerable<TEntity>> GetPagedAsync(IList<FilterInfo> filters, IList<OrderInfo> orders,
            int skip, int pageCount)
        {
            var queryable = GetSet().AsNoTracking().Where(e => e.ESTADO == true); //_repository.GetStudents();

            var predicate = SimpleHandler<TEntity>.BuildPredicate(filters);
            var filtered = queryable;
            if (filters.Count > 0)
                if (predicate != null)
                    filtered = queryable.Where(predicate);
                else
                    return Enumerable.Empty<TEntity>();

            //if (orders.Count > 0)
            var ordered = await OrderHelper.GetOrderedQueryable<TEntity>(filtered, orders).ToListAsync();

            var pagination = ordered.Skip(skip).Take(pageCount);
            return pagination;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="KProperty"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageCount"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> GetPaged<KProperty>(int pageIndex, int pageCount, int skip,
            System.Linq.Expressions.Expression<Func<TEntity, KProperty>> orderByExpression, bool ascending,
            GridFilters filter)
        {
            var set = GetSet().AsNoTracking().Where(e => e.ESTADO == true);

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                          //.Skip(pageCount * pageIndex)
                          .Skip(skip)
                          .Take(pageCount);
            }
            else
            {
                return set.OrderByDescending(orderByExpression)
                          //.Skip(pageCount * pageIndex)
                          .Skip(skip)
                          .Take(pageCount);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public virtual int CountAll(IList<FilterInfo> filters)
        {
            var queryable = GetSet().AsNoTracking().Where(e => e.ESTADO == true); //_repository.GetStudents();

            var predicate = SimpleHandler<TEntity>.BuildPredicate(filters);
            var filtered = queryable;
            if (filters.Count > 0)
                if (predicate != null)
                    filtered = queryable.Where(predicate);
                else
                    return -1;

            return filtered.Count();

        }

        public virtual async Task<int> CountAllAsync(IList<FilterInfo> filters)
        {
            var queryable = GetSet().AsNoTracking().Where(e => e.ESTADO == true); //_repository.GetStudents();

            var predicate = SimpleHandler<TEntity>.BuildPredicate(filters);
            var filtered = queryable;
            if (filters.Count > 0)
                if (predicate != null)
                    filtered = queryable.Where(predicate);
                else
                    return -1;

            return await filtered.CountAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual int CountAll(Expression<Func<TEntity, bool>> filter)
        {
            return GetSet().AsNoTracking().Where(e => e.ESTADO == true).Where(filter).Count();
        }

        public virtual async Task<int> CountAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await GetSet().AsNoTracking().Where(e => e.ESTADO == true).Where(filter).CountAsync();
        }

        /// <summary>
        /// Cuenta todas las entidades
        /// </summary>
        /// <returns></returns>
        public virtual int CountAll()
        {
            return GetSet().AsNoTracking().Where(e => e.ESTADO == true).Count();
        }

        /// <summary>
        /// Cuenta todas las entidades
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> CountAllAsync()
        {
            return await GetSet().AsNoTracking().Where(e => e.ESTADO == true).CountAsync();
        }


        #endregion

        #region IDisposable Members

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (_UnitOfWork != null)
                _UnitOfWork.Dispose();
        }

        #endregion

        #region Private Methods

        DbSet<TEntity> GetSet()
        {
            return _UnitOfWork.CreateSet<TEntity>();
        }

        #endregion



    }
}
