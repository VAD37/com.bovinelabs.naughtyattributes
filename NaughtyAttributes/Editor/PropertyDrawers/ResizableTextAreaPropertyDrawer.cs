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
        protected override void DrawProperty(ValueWrapper wrapper, ResizableTextAreaAttribute attribute)
        {
            EditorDrawUtility.DrawHeader(wrapper);

            if (wrapper.Type == typeof(string))
            {
                EditorGUILayout.LabelField(wrapper.DisplayName);

                EditorGUI.BeginChangeCheck();

                string textAreaValue = EditorGUILayout.TextArea((string)wrapper.GetValue(), GUILayout.MinHeight(EditorGUIUtility.singleLineHeight * 3f));

                if (EditorGUI.EndChangeCheck())
                {
                    wrapper.SetValue(textAreaValue);
                }
            }
            else
            {
                string warning = attribute.GetType().Name + " can only be used on string fields";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, wrapper);
                wrapper.DrawPropertyField();
            }
        }
    }
}
