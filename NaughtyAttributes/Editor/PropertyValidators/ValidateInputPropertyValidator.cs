// <copyright file="ValidateInputPropertyValidator.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor.PropertyValidators
{
    using System;
    using System.Reflection;
    using BovineLabs.NaughtyAttributes.Editor.Attributes;
    using BovineLabs.NaughtyAttributes.Editor.Utility;
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEditor;

    [PropertyValidator(typeof(ValidateInputAttribute))]
    public class ValidateInputPropertyValidator : PropertyValidator<ValidateInputAttribute>
    {
        /// <inheritdoc />
        protected override void ValidateProperty(NonSerializedAttributeWrapper wrapper, ValidateInputAttribute attribute)
        {
            ValidateProperty(wrapper, attribute);
        }

        /// <inheritdoc />
        protected override void ValidateProperty(SerializedPropertyAttributeWrapper wrapper,
            ValidateInputAttribute attribute)
        {
            ValidateProperty(wrapper, attribute);
        }

        private static void ValidateProperty(ValueWrapper wrapper, ValidateInputAttribute attribute)
        {
            var validationCallback = ReflectionUtility.GetMethod(wrapper.Target, attribute.CallbackName);

            if (validationCallback == null || validationCallback.ReturnType != typeof(bool) || validationCallback.GetParameters().Length != 1)
            {
                WrongType();
                return;
            }

            var fieldType = wrapper.Type;
            Type parameterType = validationCallback.GetParameters()[0].ParameterType;
            if (fieldType != parameterType)
            {
                FieldMismatch();
                return;
            }

            if (!(bool)validationCallback.Invoke(wrapper.Target, new[] { wrapper.GetValue() }))
            {
                if (string.IsNullOrEmpty(attribute.Message))
                {
                    EditorDrawUtility.DrawHelpBox(wrapper.DisplayName + " is not valid", MessageType.Error);
                }
                else
                {
                    EditorDrawUtility.DrawHelpBox(attribute.Message, MessageType.Error);
                }
            }
        }

        private static void WrongType()
        {
            var warning = $"{typeof(ValidateInputAttribute).Name} needs a callback with boolean return type and a single parameter of the same type as the field";
            EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning);
        }

        private static void FieldMismatch()
        {
            const string warning = "The field type is not the same as the callback's parameter type";
            EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning);
        }
    }
}