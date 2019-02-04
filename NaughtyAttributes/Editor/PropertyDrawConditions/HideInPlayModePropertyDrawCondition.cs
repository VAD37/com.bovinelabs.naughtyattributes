namespace BovineLabs.NaughtyAttributes.Editor.PropertyDrawConditions
{
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.Attributes;
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEditor;

    [PropertyDrawCondition(typeof(HideInPlayModeAttribute))]
    public class HideInPlayModePropertyDrawCondition : PropertyDrawCondition<HideInPlayModeAttribute>
    {
        /// <inheritdoc />
        protected override bool CanDrawProperty(AttributeWrapper wrapper, HideInPlayModeAttribute attribute)
        {
            return !EditorApplication.isPlaying;
        }
    }
}
