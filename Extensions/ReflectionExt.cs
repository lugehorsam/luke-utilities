namespace Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public static class ReflectionExt
    {
        public static Dictionary<MethodInfo, T[]> GetMethodsWithAttribute<T>() where T : Attribute
        {
            var methodAttributeMap = new Dictionary<MethodInfo, T[]>();
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            for (var i = 0; i < types.Length; i++)
            {
                Type type = types[i];
                MethodInfo[] typeMethods = type.GetMethods();
                for (var j = 0; j < typeMethods.Length; j++)
                {
                    MethodInfo method = typeMethods[j];
                    var methodAttributes = method.GetCustomAttributes(typeof(T), false) as T[];
                    if (methodAttributes.Length > 0)
                    {
                        methodAttributeMap[typeMethods[j]] = methodAttributes;
                    }
                }
            }

            return methodAttributeMap;
        }

        public static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            Type[] interfaceTypes = givenType.GetInterfaces();

            foreach (Type it in interfaceTypes)
            {
                if (it.IsGenericType && (it.GetGenericTypeDefinition() == genericType))
                {
                    return true;
                }
            }

            if (givenType.IsGenericType && (givenType.GetGenericTypeDefinition() == genericType))
            {
                return true;
            }

            Type baseType = givenType.BaseType;
            if (baseType == null)
            {
                return false;
            }

            return IsAssignableToGenericType(baseType, genericType);
        }

        public static IEnumerable<Type> GetParentTypes(this Type type)
        {
            // is there any base type?
            if ((type == null) || (type.BaseType == null))
            {
                yield break;
            }

            // return all implemented or inherited interfaces
            foreach (Type i in type.GetInterfaces())
            {
                yield return i;
            }

            // return all inherited types
            Type currentBaseType = type.BaseType;
            while (currentBaseType != null)
            {
                yield return currentBaseType;

                currentBaseType = currentBaseType.BaseType;
            }
        }

        public static FieldInfo[] GetNonDefaultFields<T>(T @object)
        {
            T defaultObj = default(T);

            FieldInfo[] allFields = typeof(T).GetFields();
            var relevantFields = new List<FieldInfo>();

            foreach (FieldInfo field in allFields)
            {
                if (field.GetValue(@object) != field.GetValue(defaultObj))
                {
                    relevantFields.Add(field);
                }
            }

            return relevantFields.ToArray();
        }
    }
}
