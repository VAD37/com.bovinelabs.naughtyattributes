namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;

    [PropertyDrawCondition(typeof(HideInPlayModeAttribute))]
    public class HideInPlayModePropertyDrawCondition : PropertyDrawCondition
    {
        public override bool CanDrawProperty(SerializedProperty property)
        {
            return !EditorApplication.isPlaying;
        }
    }
}
