namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
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

        public static void DrawHelpBox(string message, MessageType type, bool logToConsole = false, AttributeWrapper wrapper = null)
        {
            EditorGUILayout.HelpBox(message, type);

            Object context = null;

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

        public static void DrawPropertyField(SerializedProperty property)
        {
            EditorGUILayout.PropertyField(property, true);
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

            if (typeof(Object).IsAssignableFrom(type))
            {
                return EditorGUILayout.ObjectField(label, (Object)value, type, true);
            }

            return value;
        }

        public static object DrawPropertyField(Rect rect, object value, Type type, string label)
        {
            if (type == typeof(bool))
            {
                return EditorGUI.Toggle(rect, label, (bool)value);
            }

            if (type == typeof(int))
            {
                return EditorGUI.IntField(rect, label, (int)value);
            }

            if (type == typeof(long))
            {
                return EditorGUI.LongField(rect, label, (long)value);
            }

            if (type == typeof(float))
            {
                return EditorGUI.FloatField(rect, label, (float)value);
            }

            if (type == typeof(double))
            {
                return EditorGUI.DoubleField(rect, label, (double)value);
            }

            if (type == typeof(string))
            {
                return EditorGUI.TextField(rect, label, (string)value);
            }

            if (type == typeof(Vector2))
            {
                return EditorGUI.Vector2Field(rect, label, (Vector2)value);
            }

            if (type == typeof(Vector3))
            {
                return EditorGUI.Vector3Field(rect, label, (Vector3)value);
            }

            if (type == typeof(Vector4))
            {
                return EditorGUI.Vector4Field(rect, label, (Vector4)value);
            }

            if (type == typeof(Color))
            {
                return EditorGUI.ColorField(rect, label, (Color)value);
            }

            if (type == typeof(Bounds))
            {
                return EditorGUI.BoundsField(rect, label, (Bounds)value);
            }

            if (type == typeof(Rect))
            {
                return EditorGUI.RectField(rect, label, (Rect)value);
            }

            if (type.IsEnum)
            {
                return EditorGUI.EnumPopup(rect, label, (Enum)value);
            }

            if (typeof(Object).IsAssignableFrom(type))
            {
                return EditorGUI.ObjectField(rect, label, (Object)value, type, true);
            }

            return value;
        }

        public static bool IsDrawable(Type type)
        {
            return type != typeof(bool) && type != typeof(int) && type != typeof(long) && type != typeof(float) &&
                   type != typeof(double) && type != typeof(string) && type != typeof(Vector2) &&
                   type != typeof(Vector3) && type != typeof(Vector4) && type != typeof(Color) &&
                   type != typeof(Bounds) && type != typeof(Rect) && !type.IsEnum &&
                !typeof(Object).IsAssignableFrom(type);
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
