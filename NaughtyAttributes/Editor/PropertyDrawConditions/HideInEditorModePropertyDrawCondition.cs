using UnityEditor;

namespace NaughtyAttributes.Editor
{
    [PropertyDrawCondition(typeof(HideInEditorModeAttribute))]
    public class HideInEditorModePropertyDrawCondition : PropertyDrawCondition
    {
        public override bool CanDrawProperty(SerializedProperty property)
        {
            return EditorApplication.isPlaying;
        }
    }
}
