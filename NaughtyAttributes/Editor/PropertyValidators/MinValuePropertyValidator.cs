namespace BovineLabs.NaughtyAttributes.Editor.PropertyValidators
{
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.Attributes;
    using BovineLabs.NaughtyAttributes.Editor.Utility;
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEditor;

    [PropertyValidator(typeof(MinValueAttribute))]
    public class MinValuePropertyValidator : PropertyValidator<MinValueAttribute>
    {
        protected override void ValidateProperty(ValueWrapper wrapper, MinValueAttribute attribute)
        {
            if (wrapper.Type == typeof(int))
            {
                var value = (int)wrapper.GetValue();

                if (value < attribute.MinValue)
                {
                    wrapper.SetValue((int)attribute.MinValue);
                }
            }
            else if (wrapper.Type == typeof(float))
            {
                var value = (float)wrapper.GetValue();

                if (value < attribute.MinValue)
                {
                    wrapper.SetValue(attribute.MinValue);
                }
            }
            else
            {
                string warning = attribute.GetType().Name + " can be used only on int or float fields";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning);
            }
        }
    }
}
