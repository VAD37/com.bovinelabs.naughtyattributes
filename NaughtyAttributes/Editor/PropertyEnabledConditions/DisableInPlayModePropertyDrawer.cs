using UnityEditor;

namespace NaughtyAttributes.Editor
{
    [PropertyDrawer(typeof(DisableInPlayModeAttribute))]
    public class DisableInPlayModePropertyDrawer : PropertyEnabledCondition
    {
        /// <inheritdoc />
        public override bool IsPropertyEnabled(SerializedProperty property)
        {
            return !EditorApplication.isPlaying;
        }
    }
}
