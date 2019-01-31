namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;
    using UnityEngine;

    public class OnValueChangedProperty
    {
        private static OnValueChangedProperty instance;
        public static OnValueChangedProperty Instance => instance ?? (instance = new OnValueChangedProperty());

        public void ApplyPropertyMeta(ValueWrapper wrapper, OnValueChangedAttribute attribute)
        {
            var target = wrapper.Target;

            var callbackMethod = ReflectionUtility.GetMethod(target, attribute.CallbackName);
            if (callbackMethod != null &&
                callbackMethod.ReturnType == typeof(void) &&
                callbackMethod.GetParameters().Length == 0)
            {
                // We must apply modifications so that the callback can be invoked with up-to-date data
                wrapper.ApplyModifications();

                callbackMethod.Invoke(target, null);
            }
            else
            {
                string warning = attribute.GetType().Name + " can invoke only action methods - with void return type and no parameters";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, wrapper);
            }
        }
    }
}