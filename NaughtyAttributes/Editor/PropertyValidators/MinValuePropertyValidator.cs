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
        /// <inheritdoc />
        protected override void ValidateProperty(NonSerializedAttributeWrapper wrapper, MinValueAttribute attribute)
        {
        }

        /// <inheritdoc />
        protected override void ValidateProperty(SerializedPropertyAttributeWrapper wrapper, MinValueAttribute attribute)
        {
            var property = wrapper.Property;

            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    if (property.intValue < attribute.MinValue)
                    {
                        property.intValue = (int)attribute.MinValue;
                    }
                    break;
                case SerializedPropertyType.Float:
                    if (property.floatValue < attribute.MinValue)
                    {
                        property.floatValue = attribute.MinValue;
                    }
                    break;
                default:
                    NotIntFloat();
                    break;
            }
        }

        private static void NotIntFloat()
        {
            string warning = typeof(MinValueAttribute).Name + " can only be used on int or float fields";
            EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning);
        }
    }
}
