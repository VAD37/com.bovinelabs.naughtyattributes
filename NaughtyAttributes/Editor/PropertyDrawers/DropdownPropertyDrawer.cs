namespace BovineLabs.NaughtyAttributes.Editor.PropertyDrawers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.Attributes;
    using BovineLabs.NaughtyAttributes.Editor.Utility;
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEditor;

    [PropertyDrawer(typeof(DropdownAttribute))]
    public class DropdownPropertyDrawer : PropertyDrawer<DropdownAttribute>
    {
        private void DrawDropdown(ValueWrapper wrapper, int selectedValueIndex, object[] values, string[] displayOptions)
        {
            EditorGUI.BeginChangeCheck();

            int newIndex = EditorGUILayout.Popup(wrapper.DisplayName, selectedValueIndex, displayOptions);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(wrapper.SerializedObject.targetObject, "Dropdown");
                wrapper.SetValue(values[newIndex]);
            }
        }

        /// <inheritdoc />
        protected override void DrawProperty(NonSerializedAttributeWrapper wrapper, DropdownAttribute attribute)
        {
            this.DrawProperty(wrapper, attribute);
        }

        /// <inheritdoc />
        protected override void DrawProperty(SerializedPropertyAttributeWrapper wrapper, DropdownAttribute attribute)
        {
            this.DrawProperty(wrapper, attribute);
        }

        private void DrawProperty(ValueWrapper wrapper, DropdownAttribute attribute)
        {
            var target = wrapper.Target;

            //FieldInfo fieldInfo = ReflectionUtility.GetField(target, attribute.name);
            FieldInfo valuesFieldInfo = ReflectionUtility.GetField(target, attribute.ValuesFieldName);

            if (valuesFieldInfo == null)
            {
                FieldNull(wrapper, attribute);
                return;
            }

            if (valuesFieldInfo.GetValue(target) is IList && wrapper.Type == GetElementType(valuesFieldInfo))
            {
                // Selected value
                object selectedValue = wrapper.GetValue();

                // Values and display options
                IList valuesList = (IList)valuesFieldInfo.GetValue(target);
                var values = new object[valuesList.Count];
                var displayOptions = new string[valuesList.Count];

                for (int i = 0; i < values.Length; i++)
                {
                    object value = valuesList[i];
                    values[i] = value;
                    displayOptions[i] = value.ToString();
                }

                // Selected value index
                var selectedValueIndex = Array.IndexOf(values, selectedValue);
                if (selectedValueIndex < 0)
                {
                    selectedValueIndex = 0;
                }

                this.DrawDropdown(wrapper, selectedValueIndex, values, displayOptions);
            }

            if (valuesFieldInfo.GetValue(target) is IDropdownList)
            {
                // Current value
                object selectedValue = wrapper.GetValue();

                // Current value index, values and display options
                IDropdownList dropdown = (IDropdownList)valuesFieldInfo.GetValue(target);
                IEnumerator<KeyValuePair<string, object>> dropdownEnumerator = dropdown.GetEnumerator();

                int index = -1;
                var selectedValueIndex = -1;
                List<object> values = new List<object>();
                List<string> displayOptions = new List<string>();

                while (dropdownEnumerator.MoveNext())
                {
                    index++;

                    KeyValuePair<string, object> current = dropdownEnumerator.Current;
                    if (current.Value.Equals(selectedValue))
                    {
                        selectedValueIndex = index;
                    }

                    values.Add(current.Value);
                    displayOptions.Add(current.Key);
                }

                dropdownEnumerator.Dispose();

                if (selectedValueIndex < 0)
                {
                    selectedValueIndex = 0;
                }

                this.DrawDropdown(wrapper, selectedValueIndex, values.ToArray(), displayOptions.ToArray());
            }

            NotArray(wrapper);
        }

        private static Type GetElementType(FieldInfo listFieldInfo)
        {
            if (listFieldInfo.FieldType.IsGenericType)
            {
                return listFieldInfo.FieldType.GetGenericArguments()[0];
            }

            return listFieldInfo.FieldType.GetElementType();
        }

        private static void FieldNull(ValueWrapper valueWrapper, DropdownAttribute attribute)
        {
            var message = $"{typeof(DropdownAttribute).Name} cannot find a values field with name \"{attribute.ValuesFieldName}\"";
            EditorGUILayout.HelpBox(message, MessageType.Warning);
            valueWrapper.DrawDefaultField();
        }

        private static void NotArray(ValueWrapper valueWrapper)
        {
            var message = typeof(DropdownAttribute).Name + " works only when the type of the field is equal to the element type of the array";
            EditorGUILayout.HelpBox(message, MessageType.Warning);
            valueWrapper.DrawDefaultField();
        }
    }
}
