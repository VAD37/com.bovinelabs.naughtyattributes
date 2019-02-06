namespace BovineLabs.NaughtyAttributes.Editor.PropertyDrawers
{
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.Attributes;
    using BovineLabs.NaughtyAttributes.Editor.Utility;
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEditor;
    using UnityEngine;

    [PropertyDrawer(typeof(ResizableTextAreaAttribute))]
    public class ResizableTextAreaPropertyDrawer : PropertyDrawer<ResizableTextAreaAttribute>
    {
        /// <inheritdoc />
        protected override void DrawProperty(NonSerializedAttributeWrapper wrapper, ResizableTextAreaAttribute attribute)
        {

        }

        /// <inheritdoc />
        protected override void DrawProperty(SerializedPropertyAttributeWrapper wrapper, ResizableTextAreaAttribute attribute)
        {

            var property = wrapper.Property;

            if (property.propertyType != SerializedPropertyType.String)
            {
                NotString(wrapper);
                return;
            }

            if (DrawField(wrapper.DisplayName, property.stringValue, out var textAreaValue))
            {
                property.stringValue = textAreaValue;
            }
        }

        private static bool DrawField(string displayName, string text, out string textAreaValue)
        {
            EditorGUILayout.LabelField(displayName);

            EditorGUI.BeginChangeCheck();

            textAreaValue = EditorGUILayout.TextArea(text, GUILayout.MinHeight(EditorGUIUtility.singleLineHeight * 3f));

            if (EditorGUI.EndChangeCheck())
            {
                return true;
            }

            return false;
        }

        private static void NotString(ValueWrapper wrapper)
        {
            string warning = typeof(ResizableTextAreaAttribute).Name + " can only be used on string fields";
            EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning);
            wrapper.DrawDefaultField();
        }
    }
}
