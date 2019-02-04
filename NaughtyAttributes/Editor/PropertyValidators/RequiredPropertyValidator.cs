namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;
    using UnityEngine;

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
