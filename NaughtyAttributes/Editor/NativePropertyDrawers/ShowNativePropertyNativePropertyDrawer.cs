namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;

    [NativePropertyDrawer(typeof(ShowNativePropertyAttribute))]
    public class ShowNativePropertyNativePropertyDrawer : NativePropertyDrawer<ShowNativePropertyAttribute>
    {
        /// <inheritdoc />
        protected override void DrawNativeProperty(ValueWrapper wrapper, ShowNativePropertyAttribute attribute)
        {
            object value = wrapper.GetValue();

            if (value == null)
            {
                string warning = string.Format("{0} doesn't support {1} types", typeof(ShowNativePropertyNativePropertyDrawer).Name, "Reference");
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, wrapper.Target);
            }
            else
            {
                wrapper.DrawPropertyField();
            }
        }
    }
}
