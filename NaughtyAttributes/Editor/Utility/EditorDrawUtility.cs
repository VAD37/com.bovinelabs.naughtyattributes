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

        public static bool DrawLayoutField(object value, string label)
        {
            //GUI.enabled = false;

            bool isDrawn = true;
            Type valueType = value.GetType();

            if (valueType == typeof(bool))
            {
                EditorGUILayout.Toggle(label, (bool)value);
            }
            else if (valueType == typeof(int))
            {
                EditorGUILayout.IntField(label, (int)value);
            }
            else if (valueType == typeof(long))
            {
                EditorGUILayout.LongField(label, (long)value);
            }
            else if (valueType == typeof(float))
            {
                EditorGUILayout.FloatField(label, (float)value);
            }
            else if (valueType == typeof(double))
            {
                EditorGUILayout.DoubleField(label, (double)value);
            }
            else if (valueType == typeof(string))
            {
                EditorGUILayout.TextField(label, (string)value);
            }
            else if (valueType == typeof(Vector2))
            {
                EditorGUILayout.Vector2Field(label, (Vector2)value);
            }
            else if (valueType == typeof(Vector3))
            {
                EditorGUILayout.Vector3Field(label, (Vector3)value);
            }
            else if (valueType == typeof(Vector4))
            {
                EditorGUILayout.Vector4Field(label, (Vector4)value);
            }
            else if (valueType == typeof(Color))
            {
                EditorGUILayout.ColorField(label, (Color)value);
            }
            else if (valueType == typeof(Bounds))
            {
                EditorGUILayout.BoundsField(label, (Bounds)value);
            }
            else if (valueType == typeof(Rect))
            {
                EditorGUILayout.RectField(label, (Rect)value);
            }
            else if (value is Object o)
            {
                EditorGUILayout.ObjectField(label, o, valueType, true);
            }
            else
            {
                isDrawn = false;
            }

            //GUI.enabled = true;

            return isDrawn;
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
