
namespace Core.Dominio
{
    using Core.Dominio.Entidades;
    using Core.Dominio.Specification;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading.Tasks;

    public class ServiceAbstract<TEntity> : IService<TEntity> 
        where TEntity : Entity
    {
        #region Members

        private Dominio.IRepository<TEntity> _repository;

        #endregion

        #region Constructor

        //public ServiceAbstract() { }
        public ServiceAbstract(IRepository<TEntity> repository)
        {
            this._repository = repository;
        }

        protected IRepository<TEntity> getRepository()
        {
            return _repository;
        }

        #endregion

        #region funciones

        public virtual TEntity Get(Guid id)
        {
            if (id == null) throw new ArgumentNullException("Identificador null");
            return _repository.Get(id);
        }

        public virtual async Task<TEntity> GetAsync(Guid id)
        {
            if (id == null) throw new ArgumentNullException("Identificador null");
            return await _repository.GetAsync(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return _repository.Find(match);
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await _repository.FindAsync(match);
        }

        public virtual IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> filter)
        {
            return _repository.FindAll(filter);
        }

        public virtual async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _repository.FindAllAsync(filter);
        }

        public virtual IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, bool>> filter)
        {
            return _repository.GetFiltered(filter);
        }

        public virtual async Task<IEnumerable<TEntity>> GetFilteredAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _repository.GetFilteredAsync(filter);
        }

        //
        public virtual IEnumerable<TEntity> GetFiltered(IEnumerable<Expression<Func<TEntity, bool>>> filters)
        {
            return _repository.GetFiltered(filters);
        }
        //

        public virtual async Task<IEnumerable<TEntity>> GetFilteredAsync
            (IEnumerable<Expression<Func<TEntity, bool>>> filters)
        {
            return await _repository.GetFilteredAsync(filters);
        }

        public virtual IEnumerable<TEntity> GetAllByFilter(FilterInfo filter)
        {
            return _repository.GetAllByFilter(filter);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllByFilterAsync(FilterInfo filter)
        {
            return await _repository.GetAllByFilterAsync(filter);
        }

        public virtual IEnumerable<TEntity> AllMatching(ISpecification<TEntity> specification)
        {
            return _repository.AllMatching(specification);
        }

        public virtual async Task<IEnumerable<TEntity>> AllMatchingAsync
            (ISpecification<TEntity> specification)
        {
            return await _repository.AllMatchingAsync(specification);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("Insert");

            entity.GenerateNewIdentity();
            var rpta = _repository.Add(entity);
            _repository.Commit();
            return rpta;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("InsertAsync");

            entity.GenerateNewIdentity();
            var rpta = _repository.Add(entity);
            await _repository.UnitOfWork.CommitAsync();
            return rpta;
        }

        public virtual IEnumerable<TEntity> SavedAll(IList<TEntity> items)
        {
            if (items == null ) throw new ArgumentNullException("SavedAll");

            for(int i = 0; i< items.Count(); i++)
                items[i].GenerateNewIdentity();
            
            var rpta = _repository.SavedAll(items);
            _repository.Commit();
            return rpta;
        }

        public virtual async Task<IEnumerable<TEntity>> SavedAllAsync(IList<TEntity> items)
        {
            if (items == null) throw new ArgumentNullException("SavedAllAsync");

            for (int i = 0; i < items.Count(); i++)
                items[i].GenerateNewIdentity();

            var rpta = _repository.SavedAll(items);
            await _repository.UnitOfWork.CommitAsync();
            return rpta;
        }

        public virtual TEntity Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("Delete");

            var rpta = _repository.Remove(entity);
            _repository.Commit();
            return rpta;
        }

        public virtual async Task<TEntity> DeleteAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("DeleteAsync");

            var rpta = _repository.Remove(entity);
            await _repository.UnitOfWork.CommitAsync();
            return rpta;
        }

        public virtual IEnumerable<TEntity> DeleteAll(IList<TEntity> items)
        {
            if (items == null) throw new ArgumentNullException("DeleteAll");
            
            var rpta = _repository.RemoveAll(items);
            _repository.Commit();
            return rpta;
        }

        public virtual async Task<IEnumerable<TEntity>> DeleteAllAsync(IList<TEntity> items)
        {
            if (items == null) throw new ArgumentNullException("DeleteAllAsync");

            var rpta = _repository.RemoveAll(items);
            await _repository.UnitOfWork.CommitAsync();
            return rpta;
        }

        public virtual TEntity DeleteEstado(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("RemoveEstado");

            _repository.RemoveEstado(entity);
            _repository.Commit();
            return entity;
        }

        public virtual async Task<TEntity> DeleteEstadoAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("DeleteEstadoAsync");

            _repository.RemoveEstado(entity);
            await _repository.UnitOfWork.CommitAsync();
            return entity;
        }

        //

        public virtual Guid DeleteVirtualById(Guid id)
        {
            if (id == null) throw new ArgumentNullException("DeleteById");

            TEntity entity = (TEntity)Activator.CreateInstance(typeof(TEntity), new object[] {});
            if (entity == null) throw new KeyNotFoundException("DeleteById");
            entity.ChangeCurrentIdentity(id);
            _repository.UnitOfWork.Attach(entity);            

            IList<string> estadoProperty = new List<string>();
            estadoProperty.Add("estado");

            _repository.ModifyWithoutAttach(entity, estadoProperty);
            _repository.Commit();
            return id;
        }

        //

        public virtual async Task<Guid> DeleteVirtualByIdAsync(Guid id)
        {
            if (id == null) throw new ArgumentNullException("DeleteById");

            TEntity entity = (TEntity)Activator.CreateInstance(typeof(TEntity), new object[] { });
            if (entity == null) throw new KeyNotFoundException("DeleteById");
            entity.ChangeCurrentIdentity(id);
            _repository.UnitOfWork.Attach(entity);

            IList<string> estadoProperty = new List<string>();
            estadoProperty.Add("estado");

            _repository.ModifyWithoutAttach(entity, estadoProperty);
            await _repository.UnitOfWork.CommitAsync();
            return id;
        }

        public virtual TEntity UpdateAttach(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("UpdateAttach");

            _repository.ModifyAttach(entity);
            _repository.Commit();
            return entity;
        }

        public virtual async Task<TEntity> UpdateAttachAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("UpdateAttachAsync");

            _repository.ModifyAttach(entity);
            await _repository.UnitOfWork.CommitAsync();
            return entity;
        }

        public virtual TEntity UpdateWithoutAttach(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("UpdateWithoutAttach");

            _repository.ModifyWithoutAttach(entity);
            _repository.Commit();
            return entity;
        }

        public virtual async Task<TEntity> UpdateWithoutAttachAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("UpdateWithoutAttachAsync");

            _repository.ModifyWithoutAttach(entity);
            await _repository.UnitOfWork.CommitAsync();
            return entity;
        }

        public virtual TEntity UpdateAttach(TEntity entity, /*Expression<Func<TEntity, object>>[]*/
            IList<string> properties)
        {
            if (entity == null) throw new ArgumentNullException("UpdateAttach");

            _repository.ModifyAttach(entity, properties);
            _repository.Commit();
            return entity;
        }

        public virtual async Task<TEntity> UpdateAttachAsync(TEntity entity, /*Expression<Func<TEntity, object>>[]*/
            IList<string> properties)
        {
            if (entity == null) throw new ArgumentNullException("UpdateAttachAsync");

            _repository.ModifyAttach(entity, properties);
            await _repository.UnitOfWork.CommitAsync();
            return entity;
        }

        public virtual TEntity UpdateWithoutAttach(TEntity entity, /*Expression<Func<TEntity, object>>[]*/
            IList<string> properties)
        {
            if (entity == null) throw new ArgumentNullException("UpdateWithoutAttach");

            _repository.ModifyWithoutAttach(entity, properties);
            _repository.Commit();
            return entity;
        }

        public virtual async Task<TEntity> UpdateWithoutAttachAsync(TEntity entity, /*Expression<Func<TEntity, object>>[]*/
            IList<string> properties)
        {
            if (entity == null) throw new ArgumentNullException("UpdateWithoutAttachAsync");

            _repository.ModifyWithoutAttach(entity, properties);
            await _repository.UnitOfWork.CommitAsync();
            return entity;
        }

        public virtual IEnumerable<TEntity> GetPaged(IList<FilterInfo> filters, IList<OrderInfo> orders,
            int skip, int pageCount)
        {
            if (filters == null || orders == null) throw new ArgumentNullException("GetPaged");
            return _repository.GetPaged(filters, orders, skip, pageCount);
        }

        public virtual async Task<IEnumerable<TEntity>> GetPagedAsync(IList<FilterInfo> filters, IList<OrderInfo> orders,
            int skip, int pageCount)
        {
            if (filters == null || orders == null) throw new ArgumentNullException("GetPaged");
            return await _repository.GetPagedAsync(filters, orders, skip, pageCount);
        }

        public virtual int CountAll(IList<FilterInfo> filters)
        {
            if (filters == null) throw new ArgumentNullException("CountAll");
            return _repository.CountAll(filters);
        }

        public virtual async Task<int> CountAllAsync(IList<FilterInfo> filters)
        {
            if (filters == null) throw new ArgumentNullException("CountAll");
            return await _repository.CountAllAsync(filters);
        }

        public virtual int CountAll(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null) throw new ArgumentNullException("CountAll");
            return _repository.CountAll(filter);
        }

        public virtual async Task<int> CountAllAsync(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null) throw new ArgumentNullException("CountAll");
            return await _repository.CountAllAsync(filter);
        }

        public virtual int CountAll()
        {
            return _repository.CountAll();
        }

        public virtual async Task<int> CountAllAsync()
        {
            return await _repository.CountAllAsync();
        }

        public virtual IEnumerable<TEntityStoreProcedure>
            GetAllStoreProcedure<TEntityStoreProcedure, TEntityFilter>
            (string storeProcedureName, TEntityFilter parameters)
            where TEntityStoreProcedure : Entity
            where TEntityFilter : FilterEntidad
        {
            IList<object> parametersStoreProcedure = new List<object>();

            // Get property array
            var properties = parameters.GetType().GetProperties(); ;

            foreach (var p in properties)
            {
                string name = p.Name;
                var value = p.GetValue(parameters, null);

                var parameterStoreProcedure = new System.Data.SqlClient.SqlParameter
                {
                    ParameterName = name,
                    Value = value
                };

                parametersStoreProcedure.Add(parameterStoreProcedure);

            }

            var rpta = _repository.UnitOfWork.ExecuteQuery<TEntityStoreProcedure>(storeProcedureName,
                parametersStoreProcedure.ToArray());
            return rpta.ToList();
        }

        public IEnumerable<TEntityStoreProcedure>
            GetAllStoreProcedure<TEntityStoreProcedure>(string storeProcedureName)
            where TEntityStoreProcedure : Entity
        {
            IList<object> parametersStoreProcedure = new List<object>();
            var rpta = _repository.UnitOfWork.ExecuteQuery<TEntityStoreProcedure>(storeProcedureName,
                parametersStoreProcedure.ToArray());
            return rpta.ToList();
        }

        #endregion

        public void commit()
        {
            _repository.UnitOfWork.Commit();
        }
    }
}
