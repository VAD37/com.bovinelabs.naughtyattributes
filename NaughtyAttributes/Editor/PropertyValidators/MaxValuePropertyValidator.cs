namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;

    [PropertyValidator(typeof(MaxValueAttribute))]
    public class MaxValuePropertyValidator : PropertyValidator<MaxValueAttribute>
    {
        protected override void ValidateProperty(ValueWrapper wrapper, MaxValueAttribute attribute)
        {
            var value = wrapper.GetValue();

            if (value is int intValue)
            {
                if (intValue > attribute.MaxValue)
                {
                    wrapper.SetValue((int)attribute.MaxValue);
                }
            }
            else if (value is float floatValue)
            {
                if (floatValue > attribute.MaxValue)
                {
                    wrapper.SetValue(attribute.MaxValue);
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
