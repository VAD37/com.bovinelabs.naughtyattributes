namespace BovineLabs.NaughtyAttributes.Editor
{
    using System.Reflection;
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;

    [PropertyDrawer(typeof(DisableIfAttribute))]
    public class DisableIfPropertyDrawer : PropertyEnabledCondition<DisableIfAttribute>
    {
        /// <inheritdoc />
        protected override bool IsPropertyEnabled(AttributeWrapper wrapper, DisableIfAttribute attribute)
        {
            bool drawDisabled = false;
            bool validCondition = false;

            var target = wrapper.Target;

            FieldInfo conditionField = ReflectionUtility.GetField(target, attribute.ConditionName);
            if (conditionField != null &&
                conditionField.FieldType == typeof(bool))
            {
                drawDisabled = (bool)conditionField.GetValue(target);
                validCondition = true;
            }

            MethodInfo conditionMethod = ReflectionUtility.GetMethod(target, attribute.ConditionName);
            if (conditionMethod != null &&
                conditionMethod.ReturnType == typeof(bool) &&
                conditionMethod.GetParameters().Length == 0)
            {
                drawDisabled = (bool)conditionMethod.Invoke(target, null);
                validCondition = true;
            }

            if (validCondition)
            {
                return !drawDisabled;
            }
            else
            {
                string warning = attribute.GetType().Name + " needs a valid boolean condition field or method name to work";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, target);
                return true;
            }
        }
    }
}
