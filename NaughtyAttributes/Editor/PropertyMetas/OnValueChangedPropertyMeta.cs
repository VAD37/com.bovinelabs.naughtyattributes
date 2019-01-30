namespace BovineLabs.NaughtyAttributes.Editor
{
    using System.Reflection;
    using BovineLabs.NaughtyAttributes;
    using UnityEngine;

    [PropertyMeta(typeof(OnValueChangedAttribute))]
    public class OnValueChangedPropertyMeta : PropertyMeta<OnValueChangedAttribute>
    {
        protected override void ApplyPropertyMeta(AttributeWrapper wrapper, OnValueChangedAttribute attribute)
        {
            var target = wrapper.Target;

            MethodInfo callbackMethod = ReflectionUtility.GetMethod(target, attribute.CallbackName);
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
                Debug.LogWarning(warning, target);
            }
        }
    }
}
