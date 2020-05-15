using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Gems.DataShaping.Demo2.Helpers
{
	public static class QueryableExtension
	{
		public static IQueryable<object> ShapeTo<T>(this IQueryable<T> source, string[] fieldNames)
		{
			if (source == null)
				throw new ArgumentNullException(nameof(source));
			if (!fieldNames.Any())
				return (IQueryable<object>)source;

			Dictionary<string, Type> sourcePropertyTypes =
				fieldNames.ToDictionary(name => name, name => source.ElementType.GetProperty(name).PropertyType);

			var dynamicType = CustomTypeBuilder.GetDynamicType(sourcePropertyTypes);

			// var books = books.Select(b=> new {BookName=b.Name, ISBN=b.Isbn})

			var sourceItem = Expression.Parameter(source.ElementType, "t");
			
			var bindings =
				dynamicType.GetFields().Select(p =>
					Expression.Bind(p, Expression.Property(sourceItem, p.Name))).OfType<MemberBinding>();

			var selector = Expression.Lambda(Expression.MemberInit(
				Expression.New(dynamicType.GetConstructor(Type.EmptyTypes)), bindings), sourceItem);

			return (IQueryable<object>)source.Provider.CreateQuery(
				Expression.Call(typeof(System.Linq.Queryable),
					"Select", new Type[] { source.ElementType, dynamicType },
					source.Expression, selector));

		}

	}
}
