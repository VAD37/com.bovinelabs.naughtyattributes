namespace BovineLabs.NaughtyAttributes.Editor.PropertyDrawConditions
{
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.Attributes;
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEditor;

    [PropertyDrawCondition(typeof(HideInEditorModeAttribute))]
    public class HideInEditorModePropertyDrawCondition : PropertyDrawCondition<HideInEditorModeAttribute>
    {
        /// <inheritdoc />
        protected override bool CanDrawProperty(AttributeWrapper wrapper, HideInEditorModeAttribute attribute)
        {
            return EditorApplication.isPlaying;
        }
    }
}
