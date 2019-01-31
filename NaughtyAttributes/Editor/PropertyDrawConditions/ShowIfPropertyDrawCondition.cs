namespace BovineLabs.NaughtyAttributes.Editor
{
    using System.Reflection;
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;

    [PropertyDrawCondition(typeof(ShowIfAttribute))]
    public class ShowIfPropertyDrawCondition : PropertyDrawCondition<ShowIfAttribute>
    {
        /// <inheritdoc />
        protected override bool CanDrawProperty(AttributeWrapper wrapper, ShowIfAttribute attribute)
        {
            var target = wrapper.Target;

            FieldInfo conditionField = ReflectionUtility.GetField(target, attribute.ConditionName);
            if (conditionField != null &&
                conditionField.FieldType == typeof(bool))
            {
                return (bool)conditionField.GetValue(target);
            }

            MethodInfo conditionMethod = ReflectionUtility.GetMethod(target, attribute.ConditionName);
            if (conditionMethod != null &&
                conditionMethod.ReturnType == typeof(bool) &&
                conditionMethod.GetParameters().Length == 0)
            {
                return (bool)conditionMethod.Invoke(target, null);
            }

            string warning = attribute.GetType().Name + " needs a valid boolean condition field or method name to work";
            EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, wrapper);

            return true;
        }
    }
}
