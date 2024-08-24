
namespace Core.Dominio.Entidades
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Reflection;

    public class ExpressionBuilder
    {
        // Define some of our default filtering options
        private static MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        private static MethodInfo startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
        private static MethodInfo endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });

        public static Expression<Func<T, bool>> GetExpression<T>(List<FilterInfo> filters)
        {
            // No filters passed in 
            if (filters.Count == 0)
                return null;

            // Create the parameter for the ObjectType (typically the "x" in your expression (x => "x")
            // The "parm" string is used strictly for debugging purposes
            ParameterExpression param = Expression.Parameter(typeof(T), "parm");

            // Store the result of a calculated Expression
            Expression exp = null;

            if (filters.Count == 1)
                exp = GetExpression<T>(param, filters[0]); // Create expression from a single instance
            else if (filters.Count == 2)
                exp = GetExpression<T>(param, filters[0], filters[1]); // Create expression that utilizes AndAlso mentality
            else
            {
                // Loop through filters until we have created an expression for each
                while (filters.Count > 0)
                {
                    // Grab initial filters remaining in our List
                    var f1 = filters[0];
                    var f2 = filters[1];

                    // Check if we have already set our Expression
                    if (exp == null)
                        exp = GetExpression<T>(param, filters[0], filters[1]); // First iteration through our filters
                    else
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0], filters[1])); // Add to our existing expression

                    filters.Remove(f1);
                    filters.Remove(f2);

                    // Odd number, handle this seperately
                    if (filters.Count == 1)
                    {
                        // Pass in our existing expression and our newly created expression from our last remaining filter
                        exp = Expression.AndAlso(exp, GetExpression<T>(param, filters[0]));

                        // Remove filter to break out of while loop
                        filters.RemoveAt(0);
                    }
                }
            }

            return Expression.Lambda<Func<T, bool>>(exp, param);
        }

        public static Expression GetExpression<T>(ParameterExpression param, FilterInfo filter)
        {
            // The member you want to evaluate (x => x.FirstName)
            MemberExpression member = Expression.Property(param, filter.PropertyName);

            var propertyType = ((PropertyInfo)member.Member).PropertyType;
            var converter = TypeDescriptor.GetConverter(propertyType); // 1

            if (!converter.CanConvertFrom(typeof(string))) // 2
                throw new NotSupportedException();

            var propertyValue = converter.ConvertFromInvariantString(filter.Value); // 3

            // The value you want to evaluate
            ConstantExpression constant = Expression.Constant(filter.Value);
            var constantOtro = Expression.Constant(propertyValue);
            var valueExpression = Expression.Convert(constantOtro, propertyType); // 4

            // Determine how we want to apply the expression
            switch (filter.Operator)
            {
                case Operator.Equals:
                    //return Expression.Equal(member, constant);
                    return Expression.Equal(member, valueExpression);

                case Operator.NotEqual:
                    //return Expression.Equal(member, constant);
                    return Expression.NotEqual(member, valueExpression);

                case Operator.Contains:
                    //return Expression.Call(member, containsMethod, constant);
                    return Expression.Call(member, containsMethod, valueExpression);

                case Operator.GreaterThan:
                    //return Expression.GreaterThan(member, constant);
                    return Expression.GreaterThan(member, valueExpression);
                case Operator.GreaterThanOrEqual:
                    //return Expression.GreaterThanOrEqual(member, constant);
                    return Expression.GreaterThanOrEqual(member, valueExpression);

                case Operator.LessThan:
                    //return Expression.LessThan(member, constant);
                    return Expression.LessThan(member, valueExpression);

                case Operator.LessThanOrEqualTo:
                    //return Expression.LessThanOrEqual(member, constant);
                    return Expression.LessThanOrEqual(member, valueExpression);

                case Operator.StartsWith:
                    //return Expression.Call(member, startsWithMethod, constant);
                    return Expression.Call(member, startsWithMethod, valueExpression);

                case Operator.EndsWith:
                    //return Expression.Call(member, endsWithMethod, constant);
                    return Expression.Call(member, endsWithMethod, valueExpression);
            }

            return null;
        }

        public static Expression GetExpressionContains<T>(ParameterExpression param, FilterInfo filter)
        {
            // The member you want to evaluate (x => x.FirstName)
            MemberExpression member = Expression.Property(param, filter.PropertyName);

            var propertyType = ((PropertyInfo)member.Member).PropertyType;
            var converter = TypeDescriptor.GetConverter(propertyType); // 1

            if (!converter.CanConvertFrom(typeof(string))) // 2
                throw new NotSupportedException();

            var propertyValue = converter.ConvertFromInvariantString(filter.Value); // 3

            // The value you want to evaluate
            ConstantExpression constant = Expression.Constant(filter.Value);
            var constantOtro = Expression.Constant(propertyValue);
            var valueExpression = Expression.Convert(constantOtro, propertyType); // 4

            // Determine how we want to apply the expression
            switch (filter.Operator)
            {
                case Operator.Contains:
                    //return Expression.Call(member, containsMethod, constant);
                    return Expression.Call(member, containsMethod, valueExpression);
            }

            return null;
        }

        private static BinaryExpression GetExpression<T>(ParameterExpression param, FilterInfo filter1, FilterInfo filter2)
        {
            Expression result1 = GetExpression<T>(param, filter1);
            Expression result2 = GetExpression<T>(param, filter2);
            return Expression.AndAlso(result1, result2);
        }
    }

    public enum Operator
    {
        Contains,               //Contiene
        GreaterThan,            //Mayor que
        GreaterThanOrEqual,     //Mayor o igual que 
        LessThan,               //Menor que
        LessThanOrEqualTo,      //Menor o igual que
        StartsWith,             //Comienza con
        EndsWith,               //Termina con
        Equals,                 //Es igual a
        NotEqual                //No es igual a
    }
}
