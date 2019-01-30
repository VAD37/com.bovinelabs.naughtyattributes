namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes;
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
