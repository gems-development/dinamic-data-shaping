using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Gems.DataShaping.Demo2.Helpers
{
	public static class CustomTypeBuilder
	{
		private static readonly AssemblyName assemblyName = new AssemblyName() { Name = "CustomDynamicTypes" };
		private static readonly ModuleBuilder moduleBuilder = null;
		private static readonly Dictionary<string, Type> constractedTypes = new Dictionary<string, Type>();

		static CustomTypeBuilder()
		{
			var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
			moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
		}
		
		public static Type GetDynamicType(Dictionary<string, Type> fields)
		{
			if (null == fields)
				throw new ArgumentNullException(nameof(fields));
			if (!fields.Any())
				throw new ArgumentOutOfRangeException(nameof(fields), "fields must have at least 1 field definition");


			var className = MakeClassName(fields);

			if (constractedTypes.ContainsKey(className))
				return constractedTypes[className];

			TypeBuilder typeBuilder = moduleBuilder.DefineType(className, TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Serializable);

			foreach (var field in fields)
				typeBuilder.DefineField(field.Key, field.Value, FieldAttributes.Public);

			constractedTypes[className] = typeBuilder.CreateTypeInfo().AsType();

			return constractedTypes[className];

		}

		private static string MakeClassName(Dictionary<string, Type> fields)
		{
			string className = string.Empty;
			foreach (var field in fields)
				className += $"<{field.Key}_{field.Value.Name}>";
			return className;
		}
	}
}
