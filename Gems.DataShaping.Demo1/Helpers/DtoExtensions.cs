using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Gems.DataShaping.Demo1.Helpers
{
	public static class DtoExtensions
	{
		public static ExpandoObject ShapeTo<T>(this T dto, string fields)
		{
			if (dto == null)
				throw new ArgumentNullException(nameof(dto));

			var propertyInfos = new List<PropertyInfo>();
			var shapedDto = new ExpandoObject();

			if (string.IsNullOrWhiteSpace(fields))
			{
				propertyInfos.AddRange(
					typeof(T).GetProperties(BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance));
			}
			else
			{
				var splitedFields = fields.Split(',').Select(f => f.Trim());

				foreach (var propertyName in splitedFields)
				{
					var propertyInfo =
						typeof(T).GetProperty(propertyName,
							BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

					if (propertyInfo == null)
						throw new Exception($"Property not found on {typeof(T)}");

					propertyInfos.Add(propertyInfo);
				}
			}

			foreach (var propertyInfo in propertyInfos)
			{
				var propertyValue = propertyInfo.GetValue(dto);

				((IDictionary<string, object>)shapedDto)
					.Add(propertyInfo.Name, propertyValue);
			}
			return shapedDto;
		}

	}
}
