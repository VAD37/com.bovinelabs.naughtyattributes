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
        protected override void DrawProperty(NonSerializedAttributeWrapper wrapper, SliderAttribute attribute)
        {
            var type = wrapper.Type;

            if (type == typeof(int))
            {
                wrapper.SetValue(EditorGUILayout.IntSlider((int)wrapper.GetValue(), (int)attribute.MinValue, (int)attribute.MaxValue));
            }
            else if (type == typeof(float))
            {
                wrapper.SetValue(EditorGUILayout.Slider((float)wrapper.GetValue(), attribute.MinValue, attribute.MaxValue));
            }
            else
                NotIntFloat(wrapper);
        }

        /// <inheritdoc />
        protected override void DrawProperty(SerializedPropertyAttributeWrapper wrapper, SliderAttribute attribute)
        {
            var property = wrapper.Property;

            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    property.intValue = EditorGUILayout.IntSlider(property.intValue, (int)attribute.MinValue, (int)attribute.MaxValue);
                    break;
                case SerializedPropertyType.Float:
                    property.floatValue = EditorGUILayout.Slider(property.floatValue, attribute.MinValue, attribute.MaxValue);
                    break;
                default:
                    NotIntFloat(wrapper);
                    break;
            }
        }

        private static void NotIntFloat(ValueWrapper wrapper)
        {
            string warning = typeof(SliderAttribute).Name + " can only be used on int or float fields";
            EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning);
            wrapper.DrawDefaultField();
        }
    }
}
