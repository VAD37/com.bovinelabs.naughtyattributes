namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;
    using UnityEngine;

    [PropertyDrawer(typeof(DropdownAttribute))]
    public class DropdownPropertyDrawer : PropertyDrawer<DropdownAttribute>
    {
        /// <inheritdoc />
        protected override void DrawProperty(ValueWrapper wrapper, DropdownAttribute attribute)
        {
            EditorDrawUtility.DrawHeader(wrapper);

            var target = wrapper.Target;

            //FieldInfo fieldInfo = ReflectionUtility.GetField(target, attribute.name);
            FieldInfo valuesFieldInfo = ReflectionUtility.GetField(target, attribute.ValuesFieldName);

            if (valuesFieldInfo == null)
            {
                this.DrawWarningBox($"{attribute.GetType().Name} cannot find a values field with name \"{attribute.ValuesFieldName}\"");
                wrapper.DrawPropertyField();
            }
            else if (valuesFieldInfo.GetValue(target) is IList && wrapper.Type == this.GetElementType(valuesFieldInfo))
            {
                // Selected value
                object selectedValue = wrapper.GetValue();

                // Values and display options
                IList valuesList = (IList)valuesFieldInfo.GetValue(target);
                object[] values = new object[valuesList.Count];
                string[] displayOptions = new string[valuesList.Count];

                for (int i = 0; i < values.Length; i++)
                {
                    object value = valuesList[i];
                    values[i] = value;
                    displayOptions[i] = value.ToString();
                }

                // Selected value index
                int selectedValueIndex = Array.IndexOf(values, selectedValue);
                if (selectedValueIndex < 0)
                {
                    selectedValueIndex = 0;
                }

                // Draw the dropdown
                this.DrawDropdown(target, wrapper, selectedValueIndex, values, displayOptions);
            }
            else if (valuesFieldInfo.GetValue(target) is IDropdownList)
            {
                // Current value
                object selectedValue = wrapper.GetValue();

                // Current value index, values and display options
                IDropdownList dropdown = (IDropdownList)valuesFieldInfo.GetValue(target);
                IEnumerator<KeyValuePair<string, object>> dropdownEnumerator = dropdown.GetEnumerator();

                int index = -1;
                int selectedValueIndex = -1;
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

                // Draw the dropdown
                this.DrawDropdown(target, wrapper, selectedValueIndex, values.ToArray(), displayOptions.ToArray());
            }
            else
            {
                this.DrawWarningBox(typeof(DropdownAttribute).Name + " works only when the type of the field is equal to the element type of the array");
                wrapper.DrawPropertyField();
            }
        }

        private Type GetElementType(FieldInfo listFieldInfo)
        {
            if (listFieldInfo.FieldType.IsGenericType)
            {
                return listFieldInfo.FieldType.GetGenericArguments()[0];
            }

            return listFieldInfo.FieldType.GetElementType();
        }

        private void DrawDropdown(UnityEngine.Object target, ValueWrapper wrapper, int selectedValueIndex, object[] values, string[] displayOptions)
        {
            EditorGUI.BeginChangeCheck();

            int newIndex = EditorGUILayout.Popup(wrapper.DisplayName, selectedValueIndex, displayOptions);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Dropdown");
                wrapper.SetValue(values[newIndex]);
            }
        }

        private void DrawWarningBox(string message)
        {
            EditorGUILayout.HelpBox(message, MessageType.Warning);
            Debug.LogWarning(message);
        }
    }
}
