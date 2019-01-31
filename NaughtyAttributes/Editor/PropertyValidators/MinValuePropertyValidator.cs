namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes;
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
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, wrapper);
            }
        }
    }
}
