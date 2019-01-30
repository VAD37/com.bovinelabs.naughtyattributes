namespace BovineLabs.NaughtyAttributes.Editor
{
    using System.Linq;
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;

    [PropertyValidator(typeof(MinValueAttribute))]
    public class MinValuePropertyValidator : PropertyValidator
    {
        public override void ValidateProperty(AttributeWrapper wrapper)
        {
            var minValueAttribute = wrapper.GetCustomAttributes<MinValueAttribute>().First();

            if (wrapper.Type == typeof(int))
            {
                var value = (int)wrapper.GetValue();

                if (value < minValueAttribute.MinValue)
                {
                    wrapper.SetValue((int)minValueAttribute.MinValue);
                }
            }
            else if (wrapper.Type == typeof(float))
            {
                var value = (float)wrapper.GetValue();

                if (value < minValueAttribute.MinValue)
                {
                    wrapper.SetValue(minValueAttribute.MinValue);
                }
            }
            else
            {
                string warning = minValueAttribute.GetType().Name + " can be used only on int or float fields";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, wrapper.Target);
            }
        }
    }
}
