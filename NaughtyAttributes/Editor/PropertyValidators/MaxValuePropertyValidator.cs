namespace BovineLabs.NaughtyAttributes.Editor
{
    using System.Linq;
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;

    [PropertyValidator(typeof(MaxValueAttribute))]
    public class MaxValuePropertyValidator : PropertyValidator
    {
        public override void ValidateProperty(AttributeWrapper wrapper)
        {
            var maxValueAttribute = wrapper.GetCustomAttributes<MaxValueAttribute>().First();

            var value = wrapper.GetValue();

            if (value is int intValue)
            {
                if (intValue > maxValueAttribute.MaxValue)
                {
                    wrapper.SetValue((int)maxValueAttribute.MaxValue);
                }
            }
            else if (value is float floatValue)
            {
                if (floatValue > maxValueAttribute.MaxValue)
                {
                    wrapper.SetValue(maxValueAttribute.MaxValue);
                }
            }
            else
            {
                string warning = maxValueAttribute.GetType().Name + " can be used only on int or float fields";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, wrapper.Target);
            }
        }
    }
}
