

namespace Core.Persistencia.UnidadDeTrabajo
{
    using Core.Persistencia;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class UnitOfWorkReporte
        : DbContext, IQueryableUnitOfWork
    {
        public UnitOfWorkReporte(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }


        #region IDbSet Miembros

        #endregion

        #region IQueryableUnitOfWork Miembros

        public DbSet<TEntity> CreateSet<TEntity>()
            where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void Attach<TEntity>(TEntity item)
            where TEntity : class
        {
            //attach and set as unchanged
            base.Entry<TEntity>(item).State = EntityState.Unchanged;
        }

        public bool isDetached<TEntity>(TEntity item)
            where TEntity : class
        {
            //attach and set as unchanged
            return base.Entry<TEntity>(item).State == System.Data.Entity.EntityState.Detached;
        }

        public void SetModified<TEntity>(TEntity item)
            where TEntity : class
        {
            //this operation also attach item in object state manager
            base.Entry<TEntity>(item).State = System.Data.Entity.EntityState.Modified;
            base.Entry(item).Property("USER_CREACION").IsModified = false;
            //base.Entry(item).Property(x => x.).IsModified = false;
        }

        public void SetModified<TEntity>(TEntity item,
            /*System.Linq.Expressions.Expression<Func<TEntity, object>>[]*/IList<string> properties)
            where TEntity : class
        {
            foreach (var property in properties)
            {
                //var propertyName = System.Web.Mvc.ExpressionHelper.GetExpressionText(property);
                base.Entry(item).Property(property).IsModified = true;
            }

            //base.Entry<TEntity>(item).State = System.Data.EntityState.Modified;
        }

        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity current)
            where TEntity : class
        {
            //if it is not attached, attach original and set current values
            base.Entry<TEntity>(original).CurrentValues.SetValues(current);
        }

        public int Commit()
        {
            return base.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await base.SaveChangesAsync();
        }


        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    base.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry =>
                              {
                                  entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                              });

                }
            } while (saveFailed);
        }

        public void RollbackChanges()
        {
            // set all entities in change tracker
            // as "unchanged state"
            base.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = System.Data.Entity.EntityState.Unchanged);
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, params object[] parameters)
        {
            return base.Database.SqlQuery<TEntity>(sqlQuery, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        #endregion
        
    }
}

