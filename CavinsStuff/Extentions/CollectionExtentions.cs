﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace CavinsStuff.Extentions
{ 
	/// <summary>
	/// Contains all collection extentions.
	/// </summary>
	public static class CollectionExtentions
	{
		/// <summary>
		/// Convert the contents of a <see cref="DataTable"/> to an IEnumerable Object.
		/// </summary>
		/// <typeparam name="T">Type to cast <see cref="DataTable"/>'s rows to</typeparam>
		public static IEnumerable<T> ToEnumerable<T>(this DataTable table) where T : class, new()
		{
			DataTable a = new DataTable();

			CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
			//Change Thread culture to standard
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
			IDictionary<Type, IEnumerable<PropertyInfo>> _Properties =
			new Dictionary<Type, IEnumerable<PropertyInfo>>();

			try
			{
				var objType = typeof(T);
				IEnumerable<PropertyInfo> properties;

				lock (_Properties)
				{
					if (!_Properties.TryGetValue(objType, out properties))
					{
						properties = objType.GetProperties().Where(property => property.CanWrite);
						_Properties.Add(objType, properties);
					}
				}

				var list = new List<T>(table.Rows.Count);

				foreach (var row in table.AsEnumerable())
				{
					var obj = new T();

					foreach (var prop in properties)
					{
						try
						{
							if (row.Table.Columns.Contains(prop.Name))
							{
								prop.SetValue(obj, Convert.ChangeType(row[prop.Name], prop.PropertyType), null);
							}
							else
							{
								var attribute = prop.GetCustomAttribute<DescriptionAttribute>(false);
								var description = ((DescriptionAttribute)attribute);
								if (description != null)
								{
									prop.SetValue(obj, Convert.ChangeType(row[description.Description], prop.PropertyType), null);
								}
							}
						}
						catch (Exception)
						{

						}
					}

					list.Add(obj);
				}

				return list;
			}
			catch
			{
				return Enumerable.Empty<T>();
			}
			finally
			{
				//Reset back to original
				Thread.CurrentThread.CurrentCulture = originalCulture;
			}
		}
		
		/// <summary>
		/// Creates an IEnumerable containing this object
		/// </summary>
		/// <typeparam name="T">Type of IEnumerable</typeparam>
		/// <param name="obj">Object to add to the IEnumerable</param>
		/// <returns></returns>
		public static IEnumerable<T> ToEnumerable<T>(this T obj)
		{
			return new T[] { obj };	
		}

		/// <summary>
		/// Creates an ICollection containing this object
		/// </summary>
		/// <typeparam name="T">Type of ICollection</typeparam>
		/// <param name="obj">Object to add to the IEnumerable</param>
		/// <returns></returns>
		public static ICollection<T> ToCollection<T>(this T obj)
		{
			return new T[] { obj };
		}
	}
}
