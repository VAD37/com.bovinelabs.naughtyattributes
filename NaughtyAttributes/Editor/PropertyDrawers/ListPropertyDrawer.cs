// <copyright file="ListPropertyDrawer.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor.PropertyDrawers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.Utility;
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;

    /// <summary>
    /// List property drawer.
    /// </summary>
    public class ListPropertyDrawer
    {
        private Dictionary<string, ReorderableList> reorderableListsByPropertyName =
            new Dictionary<string, ReorderableList>();
        private static ListPropertyDrawer instance;

        private ListPropertyDrawer()
        {
        }

        /// <summary>
        /// Gets the singleton instance.
        /// </summary>
        public static ListPropertyDrawer Instance => instance ?? (instance = new ListPropertyDrawer());

        /// <summary>
        /// Clear the cache.
        /// </summary>
        public void ClearCache()
        {
            this.reorderableListsByPropertyName.Clear();
        }

        public void DrawArray(SerializedProperty property)
        {
            EditorDrawUtility.DrawHeader(property.displayName);

            if (property.isArray)
            {
                if (!this.reorderableListsByPropertyName.ContainsKey(property.name))
                {
                    var list =
                        new ReorderableList(property.serializedObject, property, true, true, true, true)
                        {
                            drawHeaderCallback = rect =>
                            {
                                EditorGUI.LabelField(rect, $"{property.displayName}: {property.arraySize}",
                                    EditorStyles.label);
                            },

                            drawElementCallback = (rect, index, isActive, isFocused) =>
                            {
                                var element = property.GetArrayElementAtIndex(index);
                                rect.y += 2f;

                                EditorGUI.PropertyField(
                                    new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element);
                            }
                        };

                    this.reorderableListsByPropertyName[property.name] = list;
                }

                this.reorderableListsByPropertyName[property.name].DoLayoutList();
            }
            else
            {
                string warning = typeof(ListPropertyDrawer).Name + " can be used only on arrays or lists";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning);
                EditorDrawUtility.DrawPropertyField(property);
            }
        }
    }
}