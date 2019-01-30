﻿namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;

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
