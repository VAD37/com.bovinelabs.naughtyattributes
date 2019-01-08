using UnityEditor;

namespace NaughtyAttributes.Editor
{
    [PropertyDrawer(typeof(DisableInPlayModeAttribute))]
    public class DisableInEditorModePropertyDrawer : PropertyEnabledCondition
    {
        /// <inheritdoc />
        public override bool IsPropertyEnabled(SerializedProperty property)
        {
            return EditorApplication.isPlaying;
        }
    }
}
