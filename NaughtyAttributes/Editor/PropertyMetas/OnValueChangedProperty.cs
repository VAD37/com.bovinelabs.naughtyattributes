namespace BovineLabs.NaughtyAttributes.Editor.PropertyMetas
{
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.Utility;
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEditor;

    public class OnValueChangedProperty
    {
        private static OnValueChangedProperty instance;
        
        private OnValueChangedProperty()
        {

        }

        public static OnValueChangedProperty Instance => instance ?? (instance = new OnValueChangedProperty());

        public void ApplyPropertyMeta(SerializedPropertyAttributeWrapper wrapper, OnValueChangedAttribute attribute)
        {
            var target = wrapper.Target;

            var callbackMethod = ReflectionUtility.GetMethod(target, attribute.CallbackName);
            if (callbackMethod != null &&
                callbackMethod.ReturnType == typeof(void) &&
                callbackMethod.GetParameters().Length == 0)
            {
                // We must apply modifications so that the callback can be invoked with up-to-date data
                wrapper.Property.serializedObject.ApplyModifiedProperties();

                callbackMethod.Invoke(target, null);
            }
            else
            {
                string warning = attribute.GetType().Name + " can invoke only action methods - with void return type and no parameters";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning);
            }
        }
    }
}
