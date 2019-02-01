namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;

    [PropertyDrawer(typeof(ReorderableListAttribute))]
    public class ReorderableListPropertyDrawer : PropertyDrawer<ReorderableListAttribute>
    {
        private Dictionary<string, Stash> reorderableListsByPropertyName = new Dictionary<string, Stash>();

        private class Stash
        {
            public ReorderableList Reorerable;
            public IList List;
            public bool IsArray;
            public bool NeedsUpdate;
        }

        protected override void DrawProperty(ValueWrapper wrapper, ReorderableListAttribute attribute)
        {
            EditorDrawUtility.DrawHeader(wrapper);

            if(wrapper.GetValue() is IList list && HeuristicallyDetermineType(list, out var elementType))
            {
                if (!this.reorderableListsByPropertyName.ContainsKey(wrapper.Name))
                {
                    IList internalList;

                    var stash = new Stash();
                    
                    if (list is Array)
                    {
                        Type d1 = typeof(List<>);
                        Type[] typeArgs = { elementType };
                        Type makeme = d1.MakeGenericType(typeArgs);
                        var newList = (IList)Activator.CreateInstance(makeme);

                        Debug.Log(newList.GetType());

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

                        Debug.Log(list.GetType());
                    }

                    stash.List = internalList;

                    var reorderableList = new ReorderableList(internalList, elementType, true, true, true, true)
                    {
                        drawHeaderCallback = rect =>
                        {
                            EditorGUI.LabelField(rect, $"{wrapper.DisplayName}: {internalList.Count}",
                                EditorStyles.label);
                        },

                        drawElementCallback = (rect, index, isActive, isFocused) =>
                        {
                            object element = internalList[index];
                            rect.y += 2f;
                            var newValue = EditorDrawUtility.DrawPropertyField(
                                new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element,
                                typeof(int), index.ToString());

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
                        }
                    };

                    stash.Reorerable = reorderableList;
                    this.reorderableListsByPropertyName[wrapper.Name] = stash;
                }

                var s = this.reorderableListsByPropertyName[wrapper.Name];

                s.Reorerable.DoLayoutList();

                if (s.NeedsUpdate && s.IsArray)
                {
                    s.NeedsUpdate = false;

                    var ass = Assembly.GetAssembly(typeof(Mesh)); // any class in UnityEngine
                    var type = ass.GetType("UnityEngine.NoAllocHelpers");

                    var methodInfo = type.GetMethod("ExtractArrayFromList", BindingFlags.Static | BindingFlags.Public);

                    if (methodInfo == null)
                    {
                        throw new Exception("ExtractArrayFromListT signature changed.");
                    }
                    var array = (Array)methodInfo.Invoke(null, new object[] { s.List });

                    array = Resize(array, s.List.Count, elementType);
                    wrapper.SetValue(array);
                }
            }
            else
            {
                string warning = typeof(ReorderableListAttribute).Name + " can be used only on arrays or lists";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, wrapper);

                wrapper.DrawPropertyField();
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

        public override void ClearCache()
        {
            this.reorderableListsByPropertyName.Clear();
        }
    }
}
