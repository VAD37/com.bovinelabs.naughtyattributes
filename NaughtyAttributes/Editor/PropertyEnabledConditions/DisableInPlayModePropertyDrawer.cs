namespace BovineLabs.NaughtyAttributes.Editor.PropertyEnabledConditions
{
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.Attributes;
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEditor;

    [PropertyDrawer(typeof(DisableInPlayModeAttribute))]
    public class DisableInPlayModePropertyDrawer : PropertyEnabledCondition<DisableInPlayModeAttribute>
    {
        /// <inheritdoc />
        protected override bool IsPropertyEnabled(AttributeWrapper wrapper, DisableInPlayModeAttribute attribute)
        {
            return !EditorApplication.isPlaying;
        }
    }
}
