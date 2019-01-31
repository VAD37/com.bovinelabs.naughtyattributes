namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;

    [PropertyValidator(typeof(RequiredAttribute))]
    public class RequiredPropertyValidator : PropertyValidator<RequiredAttribute>
    {
        protected override void ValidateProperty(ValueWrapper wrapper, RequiredAttribute attribute)
        {
            var value = wrapper.GetValue();

            if (value is UnityEngine.Object obj)
            {
                if (obj == null)
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
