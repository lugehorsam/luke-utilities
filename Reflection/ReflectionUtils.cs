using System.Reflection;
using System;
using System.Collections.Generic;


public static class ReflectionUtils {

    public static Dictionary<MethodInfo, T[]> GetMethodsWithAttribute<T> () where T : Attribute
    {
        Dictionary<MethodInfo, T[]> methodAttributeMap = new Dictionary<MethodInfo, T[]> ();
        Type [] types = Assembly.GetExecutingAssembly ().GetTypes ();
        for (int i = 0; i < types.Length; i++) {
            Type type = types [i];
            MethodInfo [] typeMethods = type.GetMethods ();
            for (int j = 0; j < typeMethods.Length; j++) {
                MethodInfo method = typeMethods [j];
                T [] methodAttributes = method.GetCustomAttributes (typeof (T), inherit: false) as T [];
                if (methodAttributes.Length > 0) {
                    methodAttributeMap [typeMethods [j]] = methodAttributes;
                }
            }
        }
        return methodAttributeMap;
    }

    public static bool IsAssignableToGenericType (Type givenType, Type genericType)
    {
        var interfaceTypes = givenType.GetInterfaces ();

        foreach (var it in interfaceTypes) {
            if (it.IsGenericType && it.GetGenericTypeDefinition () == genericType)
                return true;
        }

        if (givenType.IsGenericType && givenType.GetGenericTypeDefinition () == genericType)
            return true;

        Type baseType = givenType.BaseType;
        if (baseType == null) return false;

        return IsAssignableToGenericType (baseType, genericType);
    }
}
