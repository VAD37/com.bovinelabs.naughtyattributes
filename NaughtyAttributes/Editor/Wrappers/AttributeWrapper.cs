// <copyright file="AttributeWrapper.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// The AttributeWrapper.
    /// </summary>
    public abstract class AttributeWrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeWrapper"/> class.
        /// </summary>
        /// <param name="target">The target object.</param>
        protected AttributeWrapper(object target)
        {
            this.Target = target;
        }

        public object Target { get; }

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

    public abstract class ValueWrapper : AttributeWrapper
    {
        private readonly MemberInfo memberInfo;

        protected ValueWrapper(object target, MemberInfo memberInfo)
            : base(target)
        {
            this.memberInfo = memberInfo;
        }

        public string Name => this.memberInfo.Name;
        public string DisplayName => this.Name;

        public abstract Type Type { get; }

        public abstract object GetValue();
        public abstract void SetValue(object value);

        public override void ValidateAndDrawField()
        {
            this.ValidateField();
            this.ApplyFieldMeta();
            this.DrawField();
        }

        private void ValidateField()
        {
            var validatorAttributes = this.GetCustomAttributes<ValidatorAttribute>().ToArray();

            foreach (var attribute in validatorAttributes)
            {
                var validator = PropertyValidatorDatabase.GetValidatorForAttribute(attribute.GetType());
                validator?.Run(this, attribute);
            }
        }

        private void ApplyFieldMeta()
        {
            // Apply custom meta attributes
            MetaAttribute[] metaAttributes = this.GetCustomAttributes<MetaAttribute>().ToArray();

            Array.Sort(metaAttributes, (x, y) => x.Order - y.Order);

            foreach (var attribute in metaAttributes)
            {
                var meta = PropertyMetaDatabase.GetMetaForAttribute(attribute.GetType());
                meta?.Run(this, attribute);
            }
        }

        private void DrawField()
        {
            if (!this.CanDraw())
            {
                return;
            }

            var isPropertyEnabled = this.IsEnabled();

            // Draw the field
            EditorGUI.BeginChangeCheck();
            GUI.enabled = isPropertyEnabled;

            bool customDrawer = false;
            var drawerAttributes = this.GetCustomAttributes<DrawerAttribute>().ToArray();
            if (drawerAttributes.Length > 0)
            {
                var attribute = drawerAttributes[0];
                var drawer = PropertyDrawerDatabase.GetDrawerForAttribute(attribute.GetType());
                if (drawer != null)
                {
                    drawer.Run(this, attribute);
                    customDrawer = true;
                }
            }

            if (!customDrawer)
            {
                this.DrawPropertyField();
            }

            GUI.enabled = true;

            if (EditorGUI.EndChangeCheck())
            {
                var onValueChangedAttributes = this.GetCustomAttributes<OnValueChangedAttribute>();
                foreach (var onValueChangedAttribute in onValueChangedAttributes)
                {
                    OnValueChangedProperty.Instance.ApplyPropertyMeta(this, onValueChangedAttribute);
                }
            }
        }

        public abstract void ApplyModifications();
        public abstract void DrawPropertyField();

        /// <inheritdoc />
        public sealed override IEnumerable<T> GetCustomAttributes<T>()
        {
            return this.memberInfo.GetCustomAttributes<T>(true);
        }
    }
}