namespace BovineLabs.NaughtyAttributes.Editor.Utility
{
    using System.Collections.Generic;
    using System.Reflection;
    using UnityEditor;

    internal static class SerializedPropertyHelpers
    {
        public static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty property)
        {
            if (!property.hasChildren)
                yield break;

            // Go to first child.
            var iter = property.Copy();
            if (!iter.Next(true))
                yield break; // Shouldn't happen; we've already established we have children.

            // Iterate over children.
            while (true)
            {
                yield return iter;
                if (!iter.Next(false))
                    break;
            }
        }

        public static FieldInfo GetField(this SerializedProperty property)
        {
            var objectType = property.serializedObject.targetObject.GetType();
            var currentSerializableType = objectType;
            var pathComponents = property.propertyPath.Split('.');

            FieldInfo result = null;
            foreach (var component in pathComponents)
            {
                result = currentSerializableType.GetField(component,
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy);
                if (result == null)
                    return null;
                currentSerializableType = result.FieldType;
            }

            return result;
        }
    }
}