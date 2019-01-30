namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;

    [PropertyDrawer(typeof(DisableInPlayModeAttribute))]
    public class DisableInEditorModePropertyDrawer : PropertyEnabledCondition<DisableInPlayModeAttribute>
    {
        /// <inheritdoc />
        protected override bool IsPropertyEnabled(AttributeWrapper wrapper, DisableInPlayModeAttribute attribute)
        {
            return EditorApplication.isPlaying;
        }
    }
}
