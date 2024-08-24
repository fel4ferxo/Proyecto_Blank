
namespace Core.Dominio.Entidades
{
    using Core.Dominio.Entidades;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public class PropertysEntity<TEntity>
        where TEntity : Entity
    {
        static IDictionary<string, Expression<Func<TEntity, bool>>> properties =
            new Dictionary<string, Expression<Func<TEntity, bool>>>();

        public PropertysEntity()
        {
            
        }
        protected static void addProperty(string key, Expression<Func<TEntity, bool>> value)
        {
            properties.Add(key, value);
        }
        public static Expression<Func<TEntity, bool>> getProperty(string key)
        {
            Expression<Func<TEntity, bool>> expr;
            properties.TryGetValue(key, out expr);

            return expr;
        }
    }
}
