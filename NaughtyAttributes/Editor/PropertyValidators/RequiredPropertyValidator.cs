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
        protected override void ValidateProperty(ValueWrapper wrapper, RequiredAttribute attribute)
        {
            if (typeof(UnityEngine.Object).IsAssignableFrom(wrapper.Type))
            {
                var value = wrapper.GetValue();

                if (value == null)
                {
                    string errorMessage = wrapper.Name + " is required";
                    if (!string.IsNullOrEmpty(attribute.Message))
                    {
                        errorMessage = attribute.Message;
                    }

                    EditorDrawUtility.DrawHelpBox(errorMessage, MessageType.Error, true, wrapper);
                }
            }
            else
            {
                string warning = attribute.GetType().Name + " works only on reference types";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, wrapper);
            }
        }
    }
}
