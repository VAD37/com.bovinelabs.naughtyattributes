namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Collections.Generic;
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;

    [PropertyDrawer(typeof(ReorderableListAttribute))]
    public class ReorderableListPropertyDrawer : PropertyDrawer<ReorderableListAttribute>
    {
        private Dictionary<string, ReorderableList> reorderableListsByPropertyName = new Dictionary<string, ReorderableList>();

        protected override void DrawProperty(ValueWrapper wrapper, ReorderableListAttribute attribute)
        {
            EditorDrawUtility.DrawHeader(wrapper);

            throw new NotImplementedException();

            /*if (wrapper is SerializedFieldAttributeWrapper serializedWrapper)
            {
                var property = serializedWrapper.SerializedProperty;

                if (property.isArray)
                {
                    if (!this.reorderableListsByPropertyName.ContainsKey(wrapper.Name))
                    {
                        ReorderableList reorderableList = new ReorderableList(property.serializedObject, property, true, true, true, true)
                            {
                                drawHeaderCallback = (rect) =>
                                {
                                    EditorGUI.LabelField(rect,
                                        string.Format("{0}: {1}", wrapper.DisplayName, property.arraySize),
                                        EditorStyles.label);
                                },

                                drawElementCallback = (rect, index, isActive, isFocused) =>
                                {
                                    var element = property.GetArrayElementAtIndex(index);
                                    rect.y += 2f;

                                    EditorGUI.PropertyField(
                                        new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                                        element);
                                }
                            };

                        this.reorderableListsByPropertyName[property.name] = reorderableList;
                    }

                    this.reorderableListsByPropertyName[property.name].DoLayoutList();
                }
                else
                {
                    string warning = typeof(ReorderableListAttribute).Name + " can be used only on arrays or lists";
                    EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning,  true, wrapper);

                    wrapper.DrawPropertyField();
                }
            }
            else
            {
                string warning = typeof(ReorderableListAttribute).Name + " can be used only on serialized fields.";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning,  true, wrapper);

                wrapper.DrawPropertyField();
            }*/

        }

        public override void ClearCache()
        {
            this.reorderableListsByPropertyName.Clear();
        }
    }
}
