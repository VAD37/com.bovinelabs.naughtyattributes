namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;
    using UnityEngine;

    // Stub mimics box group until we write this
    [PropertyGrouper(typeof(FoldoutGroupAttribute))]
    public class FoldoutGroupPropertyGrouper : PropertyGrouper
    {
        public override void BeginGroup(GroupAttribute attribute)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);

            if (attribute.ShowName && !string.IsNullOrEmpty(attribute.Name))
            {
                EditorGUILayout.LabelField(attribute.Name, EditorStyles.boldLabel);
            }

            EditorGUI.indentLevel += 1;
        }

        public override void EndGroup()
        {
            EditorGUI.indentLevel -= 1;
            EditorGUILayout.EndVertical();
        }
    }
}
