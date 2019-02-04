namespace BovineLabs.NaughtyAttributes.Editor.PropertyDrawers
{
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.Attributes;
    using BovineLabs.NaughtyAttributes.Editor.Utility;
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEditor;

    [PropertyDrawer(typeof(SliderAttribute))]
    public class SliderPropertyDrawer : PropertyDrawer<SliderAttribute>
    {
        /// <inheritdoc />
        protected override void DrawProperty(ValueWrapper wrapper, SliderAttribute attribute)
        {
            EditorDrawUtility.DrawHeader(wrapper);

            if (wrapper.Type == typeof(int))
            {
                wrapper.SetValue(EditorGUILayout.IntSlider((int)wrapper.GetValue(), (int)attribute.MinValue,
                    (int)attribute.MaxValue));
            }
            else if (wrapper.Type == typeof(float))
            {
                wrapper.SetValue(EditorGUILayout.Slider((float)wrapper.GetValue(), attribute.MinValue,
                    attribute.MaxValue));
            }
            else
            {
                string warning = attribute.GetType().Name + " can be used only on int or float fields";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning);
                wrapper.DrawPropertyField();
            }
        }
    }
}
