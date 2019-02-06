namespace BovineLabs.NaughtyAttributes.Editor.PropertyValidators
{
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.Attributes;
    using BovineLabs.NaughtyAttributes.Editor.Utility;
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEditor;

    [PropertyValidator(typeof(RequiredAttribute))]
    public class RequiredPropertyValidator : PropertyValidator<RequiredAttribute>
    {
        /// <inheritdoc />
        protected override void ValidateProperty(NonSerializedAttributeWrapper wrapper, RequiredAttribute attribute)
        {
        }

        /// <inheritdoc />
        protected override void ValidateProperty(SerializedPropertyAttributeWrapper wrapper, RequiredAttribute attribute)
        {
            var property = wrapper.Property;

            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                NotObject();
                return;
            }

            if (property.objectReferenceValue == null)
            {
                DrawRequiredBox(wrapper, attribute);
            }
        }

        private static void DrawRequiredBox(SerializedPropertyAttributeWrapper wrapper, RequiredAttribute attribute)
        {
            string errorMessage = wrapper.DisplayName + " is required";
            if (!string.IsNullOrEmpty(attribute.Message))
            {
                errorMessage = attribute.Message;
            }

            EditorDrawUtility.DrawHelpBox(errorMessage, MessageType.Error);
        }

        private static void NotObject()
        {
            string warning = typeof(RequiredAttribute).Name + " only works on Object fields";
            EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning);
        }
    }
}
