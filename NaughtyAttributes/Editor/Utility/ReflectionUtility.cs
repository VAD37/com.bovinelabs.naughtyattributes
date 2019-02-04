namespace BovineLabs.NaughtyAttributes.Editor.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class ReflectionUtility
    {
        public static IEnumerable<FieldInfo> GetAllFieldsPublic(object target)
        {
            return GetAllFields(target, BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
        }

        public static IEnumerable<FieldInfo> GetAllFieldsPrivate(object target)
        {
            return GetAllFields(target, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
        }

        public static IEnumerable<FieldInfo> GetAllFields(object target, BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly)
        {
            var types = new List<Type> { target.GetType() };

            while (types.Last().BaseType != null)
            {
                types.Add(types.Last().BaseType);
            }

            for (var i = types.Count - 1; i >= 0; i--)
            {
                var fieldInfos = types[i]
                    .GetFields(flags);

                foreach (var fieldInfo in fieldInfos)
                {
                    yield return fieldInfo;
                }
            }
        }

        public static IEnumerable<PropertyInfo> GetAllProperties(object target)
        {
            var types = new List<Type> { target.GetType(), };

            while (types.Last().BaseType != null)
            {
                types.Add(types.Last().BaseType);
            }

            for (var i = types.Count - 1; i >= 0; i--)
            {
                IEnumerable<PropertyInfo> propertyInfos = types[i]
                    .GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic |
                                   BindingFlags.Public | BindingFlags.DeclaredOnly);

                foreach (var propertyInfo in propertyInfos)
                {
                    yield return propertyInfo;
                }
            }
        }
        
        private static IEnumerable<FieldInfo> GetAllFields(object target, Func<FieldInfo, bool> predicate)
        {
            var types = new List<Type> { target.GetType() };

            while (types.Last().BaseType != null)
            {
                types.Add(types.Last().BaseType);
            }

            for (int i = types.Count - 1; i >= 0; i--)
            {
                var fieldInfos = types[i]
                    .GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic |
                               BindingFlags.Public | BindingFlags.DeclaredOnly).Where(predicate);

                foreach (var fieldInfo in fieldInfos)
                {
                    yield return fieldInfo;
                }
            }
        }

        private static IEnumerable<PropertyInfo> GetAllProperties(object target, Func<PropertyInfo, bool> predicate)
        {
            List<Type> types = new List<Type>()
            {
                target.GetType(),
            };

            while (types.Last().BaseType != null)
            {
                types.Add(types.Last().BaseType);
            }

            for (int i = types.Count - 1; i >= 0; i--)
            {
                IEnumerable<PropertyInfo> propertyInfos = types[i]
                    .GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly)
                    .Where(predicate);

                foreach (var propertyInfo in propertyInfos)
                {
                    yield return propertyInfo;
                }
            }
        }

        public static IEnumerable<MethodInfo> GetAllMethods(object target, Func<MethodInfo, bool> predicate)
        {
            var list = new List<MethodInfo>();
            GetAllMethods(target.GetType(), list, predicate);
            return list;
        }

        public static FieldInfo GetField(object target, string fieldName)
        {
            return GetAllFields(target, f => f.Name.Equals(fieldName, StringComparison.InvariantCulture)).FirstOrDefault();
        }

        public static PropertyInfo GetProperty(object target, string propertyName)
        {
            return GetAllProperties(target, p => p.Name.Equals(propertyName, StringComparison.InvariantCulture)).FirstOrDefault();
        }

        public static MethodInfo GetMethod(object target, string methodName)
        {
            return GetAllMethods(target, m => m.Name.Equals(methodName, StringComparison.InvariantCulture)).FirstOrDefault();
        }

        private static void GetAllMethods(Type t, ICollection<MethodInfo> methods, Func<MethodInfo, bool> predicate)
        {
            var flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public;

            foreach (var method in t.GetMethods(flags).Where(predicate))
            {
                // Ignore inherited methods.
                if (method.DeclaringType == t)
                {
                    methods.Add(method);
                }
            }

            var baseType = t.BaseType;
            if (baseType != null)
            {
                GetAllMethods(baseType, methods, predicate);
            }
        }
    }
}
