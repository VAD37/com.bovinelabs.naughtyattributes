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
    using Object = UnityEngine.Object;

    /// <summary>
    /// The AttributeWrapper.
    /// </summary>
    public abstract class AttributeWrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeWrapper"/> class.
        /// </summary>
        /// <param name="target">The target object.</param>
        protected AttributeWrapper(Object target)
        {
            this.Target = target;
        }

        public Object Target { get; }

        public abstract Type Type { get; }

        public abstract string Name { get; }

        public abstract string DisplayName { get; }

        public void ValidateAndDrawField()
        {
            this.ValidateField();
            this.ApplyFieldMeta();
            this.DrawField();
        }

        public abstract object GetValue();

        public abstract void SetValue(object value);

        public abstract IEnumerable<T> GetCustomAttributes<T>()
            where T : Attribute;

        public abstract void ApplyModifications();

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
            MetaAttribute[] metaAttributes = this.GetCustomAttributes<MetaAttribute>()
                .Where(attr => attr.GetType() != typeof(OnValueChangedAttribute))
                .ToArray();

            Array.Sort(metaAttributes, (x, y) => x.Order - y.Order);

            foreach (var attribute in metaAttributes)
            {
                var meta = PropertyMetaDatabase.GetMetaForAttribute(attribute.GetType());
                meta?.Run(this, attribute);
            }
        }

        private void DrawField()
        {
            // Check if the field has draw conditions
            var drawConditionAttributes = this.GetCustomAttributes<DrawConditionAttribute>().ToArray();
            if (drawConditionAttributes.Length > 0)
            {
                var attribute = drawConditionAttributes[0];
                var drawCondition = PropertyDrawConditionDatabase.GetDrawConditionForAttribute(attribute.GetType());
                if (!drawCondition.CanDrawProperty(this, attribute))
                {
                    return;
                }
            }

            // Check if the field has HideInInspectorAttribute
            HideInInspector[] hideInInspectorAttributes = (HideInInspector[])this.GetCustomAttributes<HideInInspector>();
            if (hideInInspectorAttributes.Length > 0)
            {
                return;
            }

            bool isPropertyEnabled = true;

            var enabledConditionAttributes = this.GetCustomAttributes<EnabledConditionAttribute>().ToArray();
            if (enabledConditionAttributes.Length > 0)
            {
                var attribute = enabledConditionAttributes[0];
                var drawCondition = PropertyEnabledConditionDatabase.GetEnabledConditionForAttribute(attribute.GetType());
                isPropertyEnabled = drawCondition.IsPropertyEnabled(this, attribute);
            }

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
                    drawer.DrawProperty(this, attribute);
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
                    var meta = PropertyMetaDatabase.GetMetaForAttribute(onValueChangedAttribute.GetType());
                    meta?.Run(this, onValueChangedAttribute);
                }
            }
        }

        public abstract void DrawPropertyField();

    }

    public abstract class MemberInfoWrapper : AttributeWrapper
    {
        private readonly MemberInfo memberInfo;

        protected MemberInfoWrapper(Object target, MemberInfo memberInfo)
            : base(target)
        {
            this.memberInfo = memberInfo;
        }

        /// <inheritdoc />
        public sealed override string Name => this.memberInfo.Name;
        public override string DisplayName => this.Name;
        
        /// <inheritdoc />
        public sealed override IEnumerable<T> GetCustomAttributes<T>()
        {
            return this.memberInfo.GetCustomAttributes<T>(true);
        }
    }
}