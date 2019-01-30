namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;

    [PropertyDrawCondition(typeof(HideInEditorModeAttribute))]
    public class HideInEditorModePropertyDrawCondition : PropertyDrawCondition
    {
        public override bool CanDrawProperty(SerializedProperty property)
        {
            return EditorApplication.isPlaying;
        }
    }
}
