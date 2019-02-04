namespace BovineLabs.NaughtyAttributes.Editor.PropertyEnabledConditions
{
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.Attributes;
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
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
