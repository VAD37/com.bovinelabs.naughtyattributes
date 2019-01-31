namespace BovineLabs.NaughtyAttributes.Editor
{
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

            var conditionField = ReflectionUtility.GetField(target, attribute.ConditionName);
            var conditionMethod = ReflectionUtility.GetMethod(target, attribute.ConditionName);
            var conditionProperty = ReflectionUtility.GetProperty(target, attribute.ConditionName);

            if (conditionField != null && conditionField.FieldType == typeof(bool))
            {
                drawDisabled = (bool)conditionField.GetValue(target);
                validCondition = true;
            }
            else if (conditionMethod != null &&
                     conditionMethod.ReturnType == typeof(bool) &&
                     conditionMethod.GetParameters().Length == 0)
            {
                drawDisabled = (bool)conditionMethod.Invoke(target, null);
                validCondition = true;
            }
            else if (conditionProperty != null &&
                     conditionProperty.PropertyType == typeof(bool))
            {
                drawDisabled = (bool)conditionProperty.GetValue(target);
                validCondition = true;
            }

            if (validCondition)
            {
                return !drawDisabled;
            }

            string warning = attribute.GetType().Name + " needs a valid boolean condition field or method name to work";
            EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, target);
            return true;
        }
    }
}
