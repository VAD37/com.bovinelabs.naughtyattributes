using UnityEditor;
using UnityEngine;

namespace NaughtyAttributes.Editor
{
    // Stub mimics box group until we write this
    [PropertyGrouper(typeof(FoldoutGroupAttribute))]
    public class FoldoutGroupPropertyGrouper : PropertyGrouper
    {
        public override void BeginGroup(string label)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);

            if (!string.IsNullOrEmpty(label))
            {
                EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
            }
        }

        public override void EndGroup()
        {
            EditorGUILayout.EndVertical();
        }
    }
}
