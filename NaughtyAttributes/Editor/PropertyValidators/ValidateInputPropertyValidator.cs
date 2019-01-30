// <copyright file="ValidateInputPropertyValidator.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Linq;
    using UnityEditor;

    [PropertyValidator(typeof(ValidateInputAttribute))]
    public class ValidateInputPropertyValidator : PropertyValidator<ValidateInputAttribute>
    {
        protected override void ValidateProperty(AttributeWrapper wrapper, ValidateInputAttribute attribute)
        {
            var validationCallback = ReflectionUtility.GetMethod(wrapper.Target, attribute.CallbackName);

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
                        if (string.IsNullOrEmpty(attribute.Message))
                        {
                            EditorDrawUtility.DrawHelpBox(wrapper.Name + " is not valid", MessageType.Error, true,
                                wrapper.Target);
                        }
                        else
                        {
                            EditorDrawUtility.DrawHelpBox(attribute.Message, MessageType.Error, true,
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
                var warning = attribute.GetType().Name +
                              " needs a callback with boolean return type and a single parameter of the same type as the field";

                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, wrapper.Target);
            }
        }
    }
}