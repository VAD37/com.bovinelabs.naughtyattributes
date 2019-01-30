// <copyright file="ValidateInputPropertyValidator.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Linq;
    using UnityEditor;

    [PropertyValidator(typeof(ValidateInputAttribute))]
    public class ValidateInputPropertyValidator : PropertyValidator
    {
        public override void ValidateProperty(AttributeWrapper wrapper)
        {
            var validateInputAttribute = wrapper.GetCustomAttributes<ValidateInputAttribute>().First();

            var validationCallback = ReflectionUtility.GetMethod(wrapper.Target, validateInputAttribute.CallbackName);

            if (validationCallback != null &&
                validationCallback.ReturnType == typeof(bool) &&
                validationCallback.GetParameters().Length == 1)
            {
                Type fieldType = wrapper.Type;
                Type parameterType = validationCallback.GetParameters()[0].ParameterType;

                if (fieldType == parameterType)
                {
                    if (!(bool)validationCallback.Invoke(wrapper.Target, new[] { wrapper.GetValue() }))
                    {
                        if (string.IsNullOrEmpty(validateInputAttribute.Message))
                        {
                            EditorDrawUtility.DrawHelpBox(wrapper.Name + " is not valid", MessageType.Error, true,
                                wrapper.Target);
                        }
                        else
                        {
                            EditorDrawUtility.DrawHelpBox(validateInputAttribute.Message, MessageType.Error, true,
                                wrapper.Target);
                        }
                    }
                }
                else
                {
                    var warning = "The field type is not the same as the callback's parameter type";
                    EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, wrapper.Target);
                }
            }
            else
            {
                var warning = validateInputAttribute.GetType().Name +
                              " needs a callback with boolean return type and a single parameter of the same type as the field";

                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, wrapper.Target);
            }
        }
    }
}