namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Linq;
    using UnityEditor;
    using UnityEngine;
    using Object = UnityEngine.Object;

    public static class EditorDrawUtility
    {
        public static void DrawHeader(string header)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(header, EditorStyles.boldLabel);
        }

        public static bool DrawHeader(AttributeWrapper wrapper)
        {
            HeaderAttribute headerAttr = wrapper.GetCustomAttributes<HeaderAttribute>().FirstOrDefault();
            if (headerAttr != null)
            {
                DrawHeader(headerAttr.header);
                return true;
            }

            return false;
        }

        public static void DrawHelpBox(string message, MessageType type, bool logToConsole = false, UnityEngine.Object context = null)
        {
            EditorGUILayout.HelpBox(message, type);

            if (logToConsole)
            {
                switch (type)
                {
                    case MessageType.None:
                    case MessageType.Info:
                        Debug.Log(message, context);
                        break;
                    case MessageType.Warning:
                        Debug.LogWarning(message, context);
                        break;
                    case MessageType.Error:
                        Debug.LogError(message, context);
                        break;
                }
            }
        }

        public static void DrawPropertyField(SerializedProperty property, bool includeChildren = true)
        {
            EditorGUILayout.PropertyField(property, includeChildren);
        }

        public static object DrawPropertyField(object value, Type type, string label)
        {
            if (type == typeof(bool))
            {
                return EditorGUILayout.Toggle(label, (bool)value);
            }
            if (type == typeof(int))
            {
                return EditorGUILayout.IntField(label, (int)value);
            }
            if (type == typeof(long))
            {
                return EditorGUILayout.LongField(label, (long)value);
            }
            if (type == typeof(float))
            {
                return EditorGUILayout.FloatField(label, (float)value);
            }
            if (type == typeof(double))
            {
                return EditorGUILayout.DoubleField(label, (double)value);
            }
            if (type == typeof(string))
            {
                return EditorGUILayout.TextField(label, (string)value);
            }
            if (type == typeof(Vector2))
            {
                return EditorGUILayout.Vector2Field(label, (Vector2)value);
            }
            if (type == typeof(Vector3))
            {
                return EditorGUILayout.Vector3Field(label, (Vector3)value);
            }
            if (type == typeof(Vector4))
            {
                return EditorGUILayout.Vector4Field(label, (Vector4)value);
            }
            if (type == typeof(Color))
            {
                return EditorGUILayout.ColorField(label, (Color)value);
            }
            if (type == typeof(Bounds))
            {
                return EditorGUILayout.BoundsField(label, (Bounds)value);
            }
            if (type == typeof(Rect))
            {
                return EditorGUILayout.RectField(label, (Rect)value);
            }
            if (value is Object o)
            {
                return EditorGUILayout.ObjectField(label, o, type, true);
            }

            string warning = $"DrawLayoutField doesn't support {type} types";
            DrawHelpBox(warning, MessageType.Warning, true);
            return value;
        }

        public static object TryDrawLayoutField(object value, string label, out bool success)
        {
            Type valueType = value.GetType();

            success = true;

            if (valueType == typeof(bool))
            {
                return EditorGUILayout.Toggle(label, (bool)value);
            }

            if (valueType == typeof(int))
            {
                return EditorGUILayout.IntField(label, (int)value);
            }

            if (valueType == typeof(long))
            {
                return EditorGUILayout.LongField(label, (long)value);
            }

            if (valueType == typeof(float))
            {
                return EditorGUILayout.FloatField(label, (float)value);
            }

            if (valueType == typeof(double))
            {
                return EditorGUILayout.DoubleField(label, (double)value);
            }

            if (valueType == typeof(string))
            {
                return EditorGUILayout.TextField(label, (string)value);
            }

            if (valueType == typeof(Vector2))
            {
                return EditorGUILayout.Vector2Field(label, (Vector2)value);
            }

            if (valueType == typeof(Vector3))
            {
                return EditorGUILayout.Vector3Field(label, (Vector3)value);
            }

            if (valueType == typeof(Vector4))
            {
                return EditorGUILayout.Vector4Field(label, (Vector4)value);
            }

            if (valueType == typeof(Color))
            {
                return EditorGUILayout.ColorField(label, (Color)value);
            }

            if (valueType == typeof(Bounds))
            {
                return EditorGUILayout.BoundsField(label, (Bounds)value);
            }

            if (valueType == typeof(Rect))
            {
                return EditorGUILayout.RectField(label, (Rect)value);
            }

            if (value is Object o)
            {
                return EditorGUILayout.ObjectField(label, o, valueType, true);
            }

            success = false;
            return null;
        }
    }
}
