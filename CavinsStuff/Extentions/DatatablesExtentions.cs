using CavinsStuff.Extentions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CavinsStuff.Extentions
{
	/// <summary>
	/// Contains useful extentions with using the datatables plugin.
	/// </summary>
	public static class DatatablesExtentions
	{
		/// <summary>
		/// Handles the order of the items from the datatable parameters sent in the ajax post.
		/// </summary>
		/// <param name="items">Items displayed in the datatable.</param>
		/// <param name="datatableParameters">Datatable parameters set from ajax post.</param>
		/// <returns>Ordered items</returns>
		public static IOrderedQueryable<TEntity> OrderRecords<TEntity>(this IQueryable<TEntity> items, DataTableParameters datatableParameters) where TEntity : class
		{
			//Handle First Item
			var firstOrder = datatableParameters.Order[0];
			var firstDirAsc = firstOrder.Dir == "asc";
			var returnModel = items.OrderByPropertyName(datatableParameters.Columns[firstOrder.Column].Name, firstDirAsc);

			//Order By using modelOrdered
			for (int i = 1; i < datatableParameters.Order.Count; i++)
			{
				var currentOrder = datatableParameters.Order[i];
				var currentDirAsc = currentOrder.Dir == "asc";
				returnModel = returnModel.ThenByPropertyName(datatableParameters.Columns[currentOrder.Column].Name, currentDirAsc);
			}

			return returnModel;
		}

		/// <summary>
		/// Searchs the items using the datatable parameters sent in the ajax post.
		/// </summary>
		/// <param name="items">Items displayed in the datatable.</param>
		/// <param name="datatableParameters">Datatable parameters set from ajax post.</param>
		/// <returns>Filtered items</returns>
		public static IQueryable<TEntity> SearchRecords<TEntity>(this IQueryable<TEntity> items, DataTableParameters datatableParameters) where TEntity : class
		{
			if (!string.IsNullOrWhiteSpace(datatableParameters.Search.Value))
			{
				items = items.SingleSearch(datatableParameters.Search.Value);
			}

			foreach (var column in datatableParameters.Columns.Where(s => !string.IsNullOrWhiteSpace(s.Search.Value)))
			{
				items = items.ApplyColumnSearch(column.Name, column.Search.Value);
			}

			return items;
		}

		#region Private Methods

		#region Michael Lucas
		private static Func<T, object> BuildUntypedGetter<T>(PropertyInfo propertyInfo)
		{
			var targetType = propertyInfo.DeclaringType;
			var methodInfo = propertyInfo.GetGetMethod();
			var returnType = methodInfo.ReturnType;

			var exTarget = Expression.Parameter(targetType, "t");
			var exBody = Expression.Call(exTarget, methodInfo);
			var exBody2 = Expression.Convert(exBody, typeof(object));

			var lambda = Expression.Lambda<Func<T, object>>(exBody2, exTarget);

			var action = lambda.Compile();
			return action;
		}

		private static Type GetListType<T>(IQueryable<T> someList)
		{
			return typeof(T);
		}

		private static IQueryable<T> SingleSearch<T>(this IQueryable<T> objectList, string searchTerm)
		{
			Type listType = GetListType(objectList);
			List<T> filteredList = new List<T>();
			var getterList = new List<Func<T, object>>();
			foreach (PropertyInfo property in listType.GetProperties())
			{
				getterList.Add(BuildUntypedGetter<T>(property));
			}

			var getters = getterList.ToArray();
			foreach (var item in objectList)
			{
				for (int i = 0; i < getters.Length; i++)
				{
					if (getters[i](item) != null)
					{
						if (getters[i](item).ToString().IndexOf(searchTerm) >= 0)
						{
							if (!filteredList.Contains(item))
							{
								filteredList.Add(item);
							}
						}
					}
				}
			}

			return filteredList.AsQueryable<T>();
		}

		#endregion

		private static IOrderedQueryable<T> OrderByPropertyName<T>(this IQueryable<T> source, string propertyName, bool isDescending = false)
		{
			string OrderByText = isDescending ? "OrderByDescending" : "OrderBy";
			return ApplyOrder(source, propertyName, OrderByText);
		}

		private static IOrderedQueryable<T> ThenByPropertyName<T>(this IOrderedQueryable<T> source, string propertyName, bool isDescending = false)
		{
			string OrderByText = isDescending ? "ThenByDescending" : "ThenBy";
			return ApplyOrder(source, propertyName, OrderByText);
		}

		private static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
		{
			var type = typeof(T);
			var arg = Expression.Parameter(type, "x");
			Expression expr = arg;

			var propertyinfos = type.GetProperties();


			var pi = propertyinfos.First(p => p.Name == property);

			expr = Expression.Property(expr, pi);
			type = pi.PropertyType;

			var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
			var lambda = Expression.Lambda(delegateType, expr, arg);

			var result = typeof(Queryable).GetMethods().Single(
					method => method.Name == methodName
							&& method.IsGenericMethodDefinition
							&& method.GetGenericArguments().Length == 2
							&& method.GetParameters().Length == 2)
					.MakeGenericMethod(typeof(T), type)
					.Invoke(null, new object[] { source, lambda });
			return (IOrderedQueryable<T>)result;
		}

		private static IQueryable<TEntity> ApplyColumnSearch<TEntity>(this IQueryable<TEntity> items, string name, string term)
		{
			var typeProp = typeof(TEntity).GetProperty(name);
			var getter = BuildUntypedGetter<TEntity>(typeProp);

			var returnList = new List<TEntity>();

			return items.Where(s => getter(s).ToString().IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0);
		}

		#endregion
	}
}