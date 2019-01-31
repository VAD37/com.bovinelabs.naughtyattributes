namespace BovineLabs.NaughtyAttributes.Editor
{
    using System.Reflection;
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;

    [PropertyMeta(typeof(InfoBoxAttribute))]
    public class InfoBoxPropertyMeta : PropertyMeta<InfoBoxAttribute>
    {
        /// <inheritdoc />
        protected override void ApplyPropertyMeta(ValueWrapper wrapper, InfoBoxAttribute attribute)
        {
            var target = wrapper.Target;

            if (!string.IsNullOrEmpty(attribute.VisibleIf))
            {
                FieldInfo conditionField = ReflectionUtility.GetField(target, attribute.VisibleIf);
                if (conditionField != null &&
                    conditionField.FieldType == typeof(bool))
                {
                    if ((bool)conditionField.GetValue(target))
                    {
                        this.DrawInfoBox(attribute.Text, attribute.Type);
                    }

                    return;
                }

                MethodInfo conditionMethod = ReflectionUtility.GetMethod(target, attribute.VisibleIf);
                if (conditionMethod != null &&
                    conditionMethod.ReturnType == typeof(bool) &&
                    conditionMethod.GetParameters().Length == 0)
                {
                    if ((bool)conditionMethod.Invoke(target, null))
                    {
                        this.DrawInfoBox(attribute.Text, attribute.Type);
                    }

                    return;
                }

                string warning = attribute.GetType().Name + " needs a valid boolean condition field or method name to work";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, wrapper);
            }
            else
            {
                this.DrawInfoBox(attribute.Text, attribute.Type);
            }
        }

        private void DrawInfoBox(string infoText, InfoBoxType infoBoxType)
        {
            switch (infoBoxType)
            {
                case InfoBoxType.Normal:
                    EditorGUILayout.HelpBox(infoText, MessageType.Info);
                    break;

                case InfoBoxType.Warning:
                    EditorGUILayout.HelpBox(infoText, MessageType.Warning);
                    break;

                case InfoBoxType.Error:
                    EditorGUILayout.HelpBox(infoText, MessageType.Error);
                    break;
            }
        }
    }
}
