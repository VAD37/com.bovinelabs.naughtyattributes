// <copyright file="AttributeWrapper.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor.Wrappers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BovineLabs.NaughtyAttributes.Editor.Database;
    using UnityEngine;

    /// <summary>
    /// The AttributeWrapper.
    /// </summary>
    public abstract class AttributeWrapper
    {
        public abstract object Target { get; }

        public abstract void ValidateAndDrawField();

        public abstract IEnumerable<T> GetCustomAttributes<T>()
            where T : Attribute;

        protected bool CanDraw()
        {
            // Check if the field has draw conditions
            var drawConditionAttributes = this.GetCustomAttributes<DrawConditionAttribute>().ToArray();
            if (drawConditionAttributes.Length > 0)
            {
                var attribute = drawConditionAttributes[0];
                var drawCondition = PropertyDrawConditionDatabase.GetDrawConditionForAttribute(attribute.GetType());
                if (!drawCondition.CanDrawProperty(this, attribute))
                {
                    return false;
                }
            }

            // Check if the field has HideInInspectorAttribute
            HideInInspector[] hideInInspectorAttributes = (HideInInspector[])this.GetCustomAttributes<HideInInspector>();
            if (hideInInspectorAttributes.Length > 0)
            {
                return false;
            }

            return true;
        }

        protected bool IsEnabled()
        {
            var enabledConditionAttributes = this.GetCustomAttributes<EnabledConditionAttribute>().ToArray();
            if (enabledConditionAttributes.Length > 0)
            {
                var attribute = enabledConditionAttributes[0];
                var drawCondition = PropertyEnabledConditionDatabase.GetEnabledConditionForAttribute(attribute.GetType());
                return drawCondition.IsPropertyEnabled(this, attribute);
            }

            return true;
        }
    }
}