using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace JobMarket.Extensions
{
    public static class CollectionExtensions    
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool asc)
        {
            var type = typeof(T);
            string methodName = asc ? "OrderBy" : "OrderByDescending";
            var property = type.GetProperty(propertyName);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }

        /// <summary>
        /// http://stackoverflow.com/questions/9801116/where-can-i-find-a-good-example-of-using-linq-lambda-expressions-to-generate-d
        /// http://www.codeproject.com/Articles/58357/Using-jqGrid-s-search-toolbar-with-multiple-filter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static IQueryable<T> Where<T>(this IQueryable<T> query,
        string column, object value)
        {
            if (string.IsNullOrEmpty(column))
                return query;

            ParameterExpression parameter = Expression.Parameter(query.ElementType, "p");

            MemberExpression memberAccess = null;
            foreach (var property in column.Split('.'))
                memberAccess = MemberExpression.Property
                   (memberAccess ?? (parameter as Expression), property);

            // http://stackoverflow.com/questions/5305526/can-linq-expression-be-case-insensitive
            var methodInfo = typeof(string).GetMethod("IndexOf", new[] { typeof(string), typeof(StringComparison) });
            var callEx = Expression.Call(memberAccess, methodInfo, Expression.Constant(value), Expression.Constant(StringComparison.OrdinalIgnoreCase));
            Expression condition = Expression.NotEqual(callEx, Expression.Constant(-1));

            /*Expression condition = Expression.Call(memberAccess,
                        typeof(string).GetMethod("Contains"),
                        Expression.Constant(value));*/
            LambdaExpression lambda = Expression.Lambda(condition, parameter);

                MethodCallExpression result = Expression.Call(
                               typeof(Queryable), "Where",
                               new[] { query.ElementType },
                               query.Expression,
                               lambda);

                return query.Provider.CreateQuery<T>(result);
        }



        /*public static IQueryable<T> Like<T>(this IQueryable<T> source, string propertyName, string keyword)
        {
            var type = typeof(T);
            var property = type.GetProperty(propertyName);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var constant = Expression.Constant("%" + keyword + "%");
            MethodCallExpression methodExp = Expression.Call(null, typeof(SqlMethods).GetMethod("Like", new Type[] { typeof(string), typeof(string) }), propertyAccess, constant);
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(methodExp, parameter);
            return source.Where(lambda);
        }*/
    }
}