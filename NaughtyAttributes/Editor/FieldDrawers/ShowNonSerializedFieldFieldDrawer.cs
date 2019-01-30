/*namespace BovineLabs.NaughtyAttributes.Editor
{
    using System.Reflection;
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;

    [FieldDrawer(typeof(ShowNonSerializedFieldAttribute))]
    public class ShowNonSerializedFieldFieldDrawer : FieldDrawer<ShowNonSerializedFieldAttribute>
    {
        /// <inheritdoc />
        protected override void DrawField(AttributeWrapper wrapper, ShowNonSerializedFieldAttribute attribute)
        {
            object value = field.GetValue(target);

            if (value == null)
            {
                var warning = $"{typeof(ShowNonSerializedFieldFieldDrawer).Name} doesn't support Reference types";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, target);
                return;
            }

            var result = EditorDrawUtility.TryDrawLayoutField(value, field.Name, out var success);

            if (!success)
            {
                var warning = $"{typeof(ShowNonSerializedFieldFieldDrawer).Name} doesn't support {field.FieldType.Name} types";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, target);
            }

            field.SetValue(target, result);
        }
    }
}
*/