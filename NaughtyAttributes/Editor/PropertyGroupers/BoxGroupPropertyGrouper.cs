namespace BovineLabs.NaughtyAttributes.Editor.PropertyGroupers
{
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.Attributes;
    using UnityEditor;
    using UnityEngine;

    [PropertyGrouper(typeof(BoxGroupAttribute))]
    public class BoxGroupPropertyGrouper : PropertyGrouper
    {
        public override void BeginGroup(GroupAttribute attribute)
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);

            if (attribute.ShowName && !string.IsNullOrEmpty(attribute.Name))
            {
                EditorGUILayout.LabelField(attribute.Name, EditorStyles.boldLabel);
            }
        }

        public override void EndGroup()
        {
            EditorGUILayout.EndVertical();
        }
    }
}
