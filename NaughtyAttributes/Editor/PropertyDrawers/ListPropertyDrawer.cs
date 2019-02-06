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
        private const int MaxArrayLength = 100;

        private static ListPropertyDrawer instance;

        private readonly Dictionary<ValueWrapper, Stash> reorderableListsByPropertyName = new Dictionary<ValueWrapper, Stash>();

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

        /// <summary>
        /// Draw the array.
        /// </summary>
        /// <param name="wrapper">The value wrapper.</param>
        [SuppressMessage("ReSharper", "ImplicitlyCapturedClosure", Justification = "Intended")]
        public void DrawArray(ValueWrapper wrapper)
        {
            EditorDrawUtility.DrawHeader(wrapper);

            if (wrapper.GetValue() is IList list && HeuristicallyDetermineType(list, out var elementType))
            {
                if (list.Count > MaxArrayLength)
                {
                    EditorDrawUtility.DrawHelpBox($"Array.Length > {MaxArrayLength}", MessageType.None);
                    return;
                }

                Stash stash;

                if (!this.reorderableListsByPropertyName.ContainsKey(wrapper))
                {
                    IList internalList;

                    stash = new Stash();

                    if (list is Array)
                    {
                        var d1 = typeof(List<>);
                        var typeArgs = new[] { elementType };
                        var newList = (IList)Activator.CreateInstance(d1.MakeGenericType(typeArgs));

                        foreach (var l in list)
                        {
                            newList.Add(l);
                        }

                        stash.IsArray = true;
                        internalList = newList;
                    }
                    else
                    {
                        internalList = list;
                    }

                    stash.List = internalList;

                    var reorderableList = new ReorderableList(internalList, elementType, true, true, true, true)
                    {
                        drawHeaderCallback = rect =>
                        {
                            var label = $"{wrapper.DisplayName}: {internalList.Count}";
                            EditorGUI.LabelField(rect, label, EditorStyles.label);
                        },
                        drawElementCallback = (rect, index, isActive, isFocused) =>
                        {
                            object element = internalList[index];
                            rect.y += 2f;
                            var newValue = EditorDrawUtility.DrawPropertyField(
                                new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                                element,
                                elementType,
                                index.ToString());

                            if (newValue != element)
                            {
                                internalList[index] = newValue;
                                stash.NeedsUpdate = true;
                            }
                        },
                        onAddCallback = l =>
                        {
                            l.list.Add(elementType.GetTypeInfo().IsClass
                                ? null
                                : Activator.CreateInstance(elementType));

                            stash.NeedsUpdate = true;
                        },
                        onRemoveCallback = l =>
                        {
                            l.list.RemoveAt(l.index);
                            if (l.index >= l.count)
                            {
                                l.index = l.count - 1;
                            }

                            stash.NeedsUpdate = true;
                        },
                        onChangedCallback = l => { stash.NeedsUpdate = true; },
                    };

                    stash.Reorderable = reorderableList;
                    this.reorderableListsByPropertyName[wrapper] = stash;
                }
                else
                {
                    stash = this.reorderableListsByPropertyName[wrapper];
                }

                stash.Reorderable.DoLayoutList();

                if (stash.NeedsUpdate && stash.IsArray)
                {
                    stash.NeedsUpdate = false;

                    var ass = Assembly.GetAssembly(typeof(Mesh)); // any class in UnityEngine
                    var type = ass.GetType("UnityEngine.NoAllocHelpers");

                    var methodInfo = type.GetMethod("ExtractArrayFromList", BindingFlags.Static | BindingFlags.Public);

                    if (methodInfo == null)
                    {
                        throw new Exception("ExtractArrayFromListT signature changed.");
                    }

                    var array = (Array)methodInfo.Invoke(null, new object[] { stash.List });

                    array = Resize(array, stash.List.Count, elementType);
                    wrapper.SetValue(array);
                }
            }
            else
            {
                string warning = typeof(ListAttribute).Name + " can be used only on arrays or lists";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning);

                wrapper.DrawDefaultField();
            }
        }

        private static Array Resize(Array array, int size, Type elementType)
        {
            Array newArray = Array.CreateInstance(elementType, size);
            Array.Copy(array, newArray, Math.Min(array.Length, newArray.Length));
            return newArray;
        }

        private static bool HeuristicallyDetermineType(IList myList, out Type elementType)
        {
            elementType = null;

            var enumerableType =
                myList.GetType()
                    .GetInterfaces()
                    .Where(i => i.IsGenericType && i.GenericTypeArguments.Length == 1)
                    .FirstOrDefault(i => i.GetGenericTypeDefinition() == typeof(IEnumerable<>));

            if (enumerableType != null)
            {
                elementType = enumerableType.GenericTypeArguments[0];
            }

            return elementType != null;
        }

        private class Stash
        {
            public ReorderableList Reorderable { get; set; }

            public IList List { get; set; }

            public bool IsArray { get; set; }

            public bool NeedsUpdate { get; set; }
        }
    }
}