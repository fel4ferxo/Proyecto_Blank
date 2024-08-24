
namespace Core.Dominio.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public static class SimpleHandler<T>
    {
        public static Expression<Func<T, bool>> BuildPredicateFilter(FilterInfo filter)
        {
            // 
            var predicate = ModPredicateBuilder.Create<T>(item => true);
            return predicate.And(BuildExpression(filter));            

        }

        public static Expression<Func<T, bool>> BuildPredicate(IList<FilterInfo> filters)
        {
            // FilterInfo
            var predicate = ModPredicateBuilder.Create<T>(item => true);

            foreach (var info in filters)
            {
                if (info.Logical == Logical.OR)
                    predicate = predicate.Or(BuildExpression(info));
                else
                    predicate = predicate.And(BuildExpression(info));
            }

            return predicate;
        }

        public static Expression<Func<T, bool>> BuildExpression(FilterInfo info)
        {
            ParameterExpression param = Expression.Parameter(typeof(T), "parm");
            Expression exp = ExpressionBuilder.GetExpression<T>(param, info);
            var lambda = Expression.Lambda<Func<T, bool>>(exp, param);
            return lambda;
        }

        public static Expression<Func<T, bool>> BuildExpressionContains(FilterInfo info)
        {
            // obligar al filtro "info" a trabajar con el operador Contains
            info.Operator = Operator.Contains;

            ParameterExpression param = Expression.Parameter(typeof(T), "parm");
            Expression exp = ExpressionBuilder.GetExpressionContains<T>(param, info);
            var lambda = Expression.Lambda<Func<T, bool>>(exp, param);
            return lambda;
        }
    }
}
