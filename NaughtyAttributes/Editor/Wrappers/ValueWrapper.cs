// <copyright file="ValueWrapper.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor.Wrappers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using BovineLabs.NaughtyAttributes.Editor.Database;
    using BovineLabs.NaughtyAttributes.Editor.Editors;
    using BovineLabs.NaughtyAttributes.Editor.PropertyDrawers;
    using BovineLabs.NaughtyAttributes.Editor.PropertyMetas;
    using BovineLabs.NaughtyAttributes.Editor.Utility;
    using UnityEditor;
    using UnityEngine;

    public abstract class ValueWrapper : AttributeWrapper
    {
        protected MemberInfo MemberInfo { get; }

        private readonly Drawer drawer;
        private bool foldout;

        protected ValueWrapper(object target, MemberInfo memberInfo)
            : base(target)
        {
            this.MemberInfo = memberInfo;

            if (EditorDrawUtility.IsDrawable(this.Type))
            {
                var info = this.Type.GetTypeInfo();

                if (!info.IsArray)
                {
                    this.drawer = new Drawer(this.GetValue());
                }
            }
        }

        public string Name => this.MemberInfo.Name;

        public string DisplayName => this.Name;

        public abstract Type Type { get; }

        public abstract object GetValue();

        public abstract void SetValue(object value);

        public abstract void ApplyModifications();

        public override void ValidateAndDrawField()
        {
            if (!this.CanDraw())
            {
                return;
            }

            this.ValidateField();
            this.ApplyFieldMeta();
            this.DrawField();
        }

        public void DrawPropertyField()
        {
            if (this.drawer != null)
            {
                if (!this.drawer.HasElement)
                {
                    return;
                }

                this.foldout = EditorGUILayout.Foldout(this.foldout, this.DisplayName);

                if (this.foldout)
                {
                    EditorGUI.indentLevel += 1;
                    this.drawer.OnInspectorGUI();
                    EditorGUI.indentLevel -= 1;
                }
            }
            else
            {
                if (this.Type.IsArray)
                {
                    this.foldout = EditorGUILayout.Foldout(this.foldout, this.DisplayName);

                    if (this.foldout)
                    {
                        ListPropertyDrawer.Instance.DrawArray(this);
                    }
                }
                else
                {
                    this.SetValue(EditorDrawUtility.DrawPropertyField(this.GetValue(), this.Type, this.DisplayName));
                }
            }
        }

        /// <inheritdoc />
        public sealed override IEnumerable<T> GetCustomAttributes<T>()
        {
            return this.MemberInfo.GetCustomAttributes<T>(true);
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
            var isPropertyEnabled = this.IsEnabled();

            // Draw the field
            EditorGUI.BeginChangeCheck();
            GUI.enabled = isPropertyEnabled;

            bool customDrawer = false;
            var drawerAttributes = this.GetCustomAttributes<DrawerAttribute>().ToArray();
            if (drawerAttributes.Length > 0)
            {
                var attribute = drawerAttributes[0];
                var d = PropertyDrawerDatabase.GetDrawerForAttribute(attribute.GetType());
                if (d != null)
                {
                    d.Run(this, attribute);
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
    }
}