namespace BovineLabs.NaughtyAttributes.Editor.PropertyValidators
{
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.Attributes;
    using BovineLabs.NaughtyAttributes.Editor.Utility;
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEditor;

    [PropertyValidator(typeof(MaxValueAttribute))]
    public class MaxValuePropertyValidator : PropertyValidator<MaxValueAttribute>
    {
        /// <inheritdoc />
        protected override void ValidateProperty(NonSerializedAttributeWrapper wrapper, MaxValueAttribute attribute)
        {
        }

        /// <inheritdoc />
        protected override void ValidateProperty(SerializedPropertyAttributeWrapper wrapper, MaxValueAttribute attribute)
        {
            var property = wrapper.Property;

            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    if (property.intValue > attribute.MaxValue)
                    {
                        property.intValue = (int)attribute.MaxValue;
                    }
                    break;
                case SerializedPropertyType.Float:
                    if (property.floatValue > attribute.MaxValue)
                    {
                        property.floatValue = attribute.MaxValue;
                    }
                    break;
                default:
                    NotIntFloat();
                    break;
            }
        }

        private static void NotIntFloat()
        {
            string warning = $"{typeof(MaxValueAttribute).Name} can only be used on int or float fields";
            EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning);
        }
    }
}
