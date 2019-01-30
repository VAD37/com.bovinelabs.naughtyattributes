namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes;
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
