namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;

    [PropertyDrawer(typeof(DisableInEditorModeAttribute))]
    public class DisableInEditorModePropertyDrawer : PropertyEnabledCondition<DisableInEditorModeAttribute>
    {
        /// <inheritdoc />
        protected override bool IsPropertyEnabled(AttributeWrapper wrapper, DisableInEditorModeAttribute attribute)
        {
            return EditorApplication.isPlaying;
        }
    }
}
