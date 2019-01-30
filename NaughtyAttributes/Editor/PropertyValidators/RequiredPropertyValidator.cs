namespace BovineLabs.NaughtyAttributes.Editor
{
    using System.Linq;
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;

    [PropertyValidator(typeof(RequiredAttribute))]
    public class RequiredPropertyValidator : PropertyValidator
    {
        public override void ValidateProperty(AttributeWrapper wrapper)
        {
            RequiredAttribute requiredAttribute = wrapper.GetCustomAttributes<RequiredAttribute>().First();

            var value = wrapper.GetValue();

            if (value is UnityEngine.Object obj)
            {
                if (obj == null)
                {
                    string errorMessage = wrapper.Name + " is required";
                    if (!string.IsNullOrEmpty(requiredAttribute.Message))
                    {
                        errorMessage = requiredAttribute.Message;
                    }

                    EditorDrawUtility.DrawHelpBox(errorMessage, MessageType.Error, true, wrapper.Target);
                }
            }
            else
            {
                string warning = requiredAttribute.GetType().Name + " works only on reference types";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, wrapper.Target);
            }
        }
    }
}
