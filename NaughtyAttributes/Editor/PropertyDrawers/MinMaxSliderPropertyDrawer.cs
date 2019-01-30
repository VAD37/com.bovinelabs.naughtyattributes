namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;
    using UnityEngine;

    [PropertyDrawer(typeof(MinMaxSliderAttribute))]
    public class MinMaxSliderPropertyDrawer : PropertyDrawer<MinMaxSliderAttribute>
    {
        protected override void DrawProperty(AttributeWrapper wrapper, MinMaxSliderAttribute attribute)
        {
            EditorDrawUtility.DrawHeader(wrapper);

            if (wrapper.Type == typeof(Vector2))
            {
                Rect controlRect = EditorGUILayout.GetControlRect();
                float labelWidth = EditorGUIUtility.labelWidth;
                float floatFieldWidth = EditorGUIUtility.fieldWidth;
                float sliderWidth = controlRect.width - labelWidth - 2f * floatFieldWidth;
                float sliderPadding = 5f;

                Rect labelRect = new Rect(
                    controlRect.x,
                    controlRect.y,
                    labelWidth,
                    controlRect.height);

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
                EditorGUI.LabelField(labelRect, wrapper.DisplayName);

                // Draw the slider
                EditorGUI.BeginChangeCheck();

                Vector2 sliderValue = (Vector2)wrapper.GetValue();
                EditorGUI.MinMaxSlider(sliderRect, ref sliderValue.x, ref sliderValue.y, attribute.MinValue, attribute.MaxValue);

                sliderValue.x = EditorGUI.FloatField(minFloatFieldRect, sliderValue.x);
                sliderValue.x = Mathf.Clamp(sliderValue.x, attribute.MinValue, Mathf.Min(attribute.MaxValue, sliderValue.y));

                sliderValue.y = EditorGUI.FloatField(maxFloatFieldRect, sliderValue.y);
                sliderValue.y = Mathf.Clamp(sliderValue.y, Mathf.Max(attribute.MinValue, sliderValue.x), attribute.MaxValue);

                if (EditorGUI.EndChangeCheck())
                {
                    wrapper.SetValue(sliderValue);
                }
            }
            else
            {
                string warning = attribute.GetType().Name + " can be used only on Vector2 fields";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, wrapper.Target);
                wrapper.DrawPropertyField();
            }
        }
    }
}
