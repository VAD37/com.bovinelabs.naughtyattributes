using UnityEditor;

namespace NaughtyAttributes.Editor
{
    [PropertyDrawCondition(typeof(HideInPlayModeAttribute))]
    public class HideInPlayModePropertyDrawCondition : PropertyDrawCondition
    {
        public override bool CanDrawProperty(SerializedProperty property)
        {
            return !EditorApplication.isPlaying;
        }
    }
}
