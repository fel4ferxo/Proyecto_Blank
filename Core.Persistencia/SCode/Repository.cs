
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
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        #region Members

        IQueryableUnitOfWork _UnitOfWork;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor que crea una instancia de un repositorio
        /// </summary>
        /// <param name="unitOfWork">Asociado a una Unidad de Trabajo</param>
        public Repository(IQueryableUnitOfWork unitOfWork)
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
        public virtual TEntity Get(Guid Id)
        {
            if (Id != null && Id != Guid.Empty)
            {
                var rpta = GetSet().AsNoTracking().Where(e => e.ESTADO == true).SingleOrDefault(s => s.Id == Id);
                if (rpta == null) throw new KeyNotFoundException("El Identificador enviado no existe actualmente"
                    + Id.ToString());
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
        public virtual async Task<TEntity> GetAsync(Guid Id)
        {
            if (Id != null && Id != Guid.Empty)
            {
                var rpta = await GetSet().AsNoTracking().Where(e => e.ESTADO == true).SingleOrDefaultAsync(s => s.Id == Id);
                if (rpta == null) throw new KeyNotFoundException("El Identificador enviado no existe actualmente"
                    + Id.ToString());
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

        public virtual IEnumerable<TEntity> GetFilteredWithoutStatus(Expression<Func<TEntity, bool>> filter)
        {
            return GetSet().AsNoTracking().Where(filter);
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
        /// Anyade una actividad al Repositorio
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual TEntity Add(TEntity item)
        {
            if (item != (TEntity)null)
            {
                var seter = GetSet();
                TEntity aa = seter.Add(item);
                return aa;
            }
            else
                throw new ArgumentNullException("Item para guardar null");
        }

        /// <summary>
        /// Anyade una actividad al Repositorio
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual TEntity Add(TEntity item, string userCreacion)
        {
            if (item != (TEntity)null)
            {
                item.GenerateNewIdentity();
                item.AsNew(userCreacion);
                var seter = GetSet();
                TEntity aa = seter.Add(item);
                return aa;
            }
            else
                throw new ArgumentNullException("Item para guardar null");
        }

        /// <summary>
        /// Guardar una lista de entidades
        /// </summary>
        /// <param name="tList"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> SavedAll(IEnumerable<TEntity> items)
        {
            var itemsSaved = GetSet().AddRange(items);
            return itemsSaved;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"> </param>
        public virtual TEntity Remove(TEntity item)
        {
            if (item != (TEntity)null)
            {
                //attach item if not exist
                _UnitOfWork.Attach(item);

                //eliminando
                return GetSet().Remove(item);
            }
            else
                throw new ArgumentNullException("Item para eliminar null");
        }

        public virtual IEnumerable<TEntity> RemoveAll(IEnumerable<TEntity> items)
        {
            if (items != (IEnumerable<TEntity>)null && items.Count() > 0)
            {
                //attach item if not exist
                _UnitOfWork.Attach<IEnumerable<TEntity>>(items);

                //eliminando
                return GetSet().RemoveRange(items);
            }
            else
                throw new ArgumentNullException("Item para eliminar null");
        }

        /// <summary>
        /// FUNCION QUE PONE EN "0" EL ESTADO PARA ELIMINAR UNA ENTIDAD
        /// </summary>
        /// <param name="item"></param>
        public virtual TEntity RemoveEstado(TEntity item)
        {
            IList<string> lista = new List<string>();
            item.ESTADO = false;
            lista.Add("ESTADO");
            if (item != (TEntity)null)
            {
                _UnitOfWork.Attach<TEntity>(item);
                _UnitOfWork.SetModified(item, lista);
                return item;
            }
            else
                throw new ArgumentNullException("Item para eliminar null");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"> </param>
        public virtual void TrackItem(TEntity item)
        {
            if (item != (TEntity)null)
                _UnitOfWork.Attach<TEntity>(item);
            else
                throw new ArgumentNullException("Item null");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"> </param>
        public virtual bool isDetached(TEntity item)
        {
            if (item != (TEntity)null)
                return _UnitOfWork.isDetached<TEntity>(item);
            else
                throw new ArgumentNullException("Item null");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public virtual TEntity ModifyAttach(TEntity item)
        {
            if (item != (TEntity)null)
            {
                _UnitOfWork.Attach<TEntity>(item);
                _UnitOfWork.SetModified(item);
                return item;
            }
            else
                throw new ArgumentNullException("Item para modificar null");
        }

        public virtual TEntity ModifyWithoutAttach(TEntity item)
        {
            if (item != (TEntity)null)
            {
                _UnitOfWork.SetModified(item);
                return item;
            }
            else
                throw new ArgumentNullException("Item para modificar null");
        }

        public virtual TEntity ModifyAttach(TEntity item, /*Expression<Func<TEntity, object>>[]*/
            IList<string> properties)
        {
            if (item != (TEntity)null)
            {
                _UnitOfWork.Attach<TEntity>(item);
                _UnitOfWork.SetModified(item, properties);
                return item;
            }
            else
                throw new ArgumentNullException("Item para modificar null");
        }

        public virtual TEntity ModifyWithoutAttach(TEntity item, /*Expression<Func<TEntity, object>>[]*/
            IList<string> properties)
        {
            if (item != (TEntity)null)
            {
                _UnitOfWork.SetModified(item, properties);
                return item;
            }
            else
                throw new ArgumentNullException("Item para modificar null");
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

            var ordered = filtered;
            if (orders.Count > 0)
                ordered = OrderHelper.GetOrderedQueryable<TEntity>(filtered, orders);
            else
            {
                OrderInfo orderId = new OrderInfo();
                orderId.OrderType = OrderType.DESC;
                orderId.Property = "Id";
                orderId.Index = 1;

                IList<OrderInfo> ordersNew = new List<OrderInfo>();
                ordersNew.Add(orderId);

                ordered = OrderHelper.GetOrderedQueryable<TEntity>(filtered, ordersNew);
            }
            

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="persisted"></param>
        /// <param name="current"></param>
        public virtual void Merge(TEntity persisted, TEntity current)
        {
            _UnitOfWork.ApplyCurrentValues(persisted, current);
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

        public int Commit()
        {
            return _UnitOfWork.Commit();
        }

        #endregion



    }
}
