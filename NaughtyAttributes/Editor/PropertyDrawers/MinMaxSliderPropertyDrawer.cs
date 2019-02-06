namespace BovineLabs.NaughtyAttributes.Editor.PropertyDrawers
{
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.Attributes;
    using BovineLabs.NaughtyAttributes.Editor.Utility;
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEditor;
    using UnityEngine;

    [PropertyDrawer(typeof(MinMaxSliderAttribute))]
    public class MinMaxSliderPropertyDrawer : PropertyDrawer<MinMaxSliderAttribute>
    {
        /// <inheritdoc />
        protected override void DrawProperty(NonSerializedAttributeWrapper wrapper, MinMaxSliderAttribute attribute)
        {
        }

        /// <inheritdoc />
        protected override void DrawProperty(SerializedPropertyAttributeWrapper wrapper, MinMaxSliderAttribute attribute)
        {
            var property = wrapper.Property;

            if (property.propertyType != SerializedPropertyType.Vector2)
            {
                NotVector2Field(wrapper);
                return;
            }

            var sliderValue = property.vector2Value;

            if (this.DrawProperty(wrapper.DisplayName, ref sliderValue, attribute))
            {
                property.vector2Value = sliderValue;
            }
        }

        private bool DrawProperty(string label, ref Vector2 sliderValue, MinMaxSliderAttribute attribute)
        {
            Rect controlRect = EditorGUILayout.GetControlRect();
            float labelWidth = EditorGUIUtility.labelWidth;
            float floatFieldWidth = EditorGUIUtility.fieldWidth;
            float sliderWidth = controlRect.width - labelWidth - 2f * floatFieldWidth;
            float sliderPadding = 5f;

            Rect labelRect = new Rect(controlRect.x, controlRect.y, labelWidth, controlRect.height);

            Rect sliderRect = new Rect(
                controlRect.x + labelWidth + floatFieldWidth + sliderPadding,
                controlRect.y,
                sliderWidth - 2f * sliderPadding,
                controlRect.height);

            Rect minFloatFieldRect = new Rect(
                controlRect.x + labelWidth,
                controlRect.y,
                floatFieldWidth,
                controlRect.height);

            Rect maxFloatFieldRect = new Rect(
                controlRect.x + labelWidth + floatFieldWidth + sliderWidth,
                controlRect.y,
                floatFieldWidth,
                controlRect.height);

            // Draw the label
            EditorGUI.LabelField(labelRect, label);

            // Draw the slider
            EditorGUI.BeginChangeCheck();

            EditorGUI.MinMaxSlider(sliderRect, ref sliderValue.x, ref sliderValue.y, attribute.MinValue, attribute.MaxValue);

            sliderValue.x = EditorGUI.FloatField(minFloatFieldRect, sliderValue.x);
            sliderValue.x = Mathf.Clamp(sliderValue.x, attribute.MinValue, Mathf.Min(attribute.MaxValue, sliderValue.y));

            sliderValue.y = EditorGUI.FloatField(maxFloatFieldRect, sliderValue.y);
            sliderValue.y = Mathf.Clamp(sliderValue.y, Mathf.Max(attribute.MinValue, sliderValue.x), attribute.MaxValue);

            return EditorGUI.EndChangeCheck();
        }

        private static void NotVector2Field(ValueWrapper wrapper)
        {
            string warning = typeof(MinMaxSliderAttribute) + " can be used only on Vector2 fields";
            EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning);
            wrapper.DrawDefaultField();
        }
    }
}
