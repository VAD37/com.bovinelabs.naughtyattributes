// <copyright file="NonSerializedAttributeWrapper.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor.Wrappers
{
    using System;
    using System.Reflection;
    using BovineLabs.NaughtyAttributes.Editor.Editors;
    using BovineLabs.NaughtyAttributes.Editor.PropertyDrawers;
    using BovineLabs.NaughtyAttributes.Editor.Utility;
    using UnityEditor;
    using UnityEngine;
    using PropertyDrawer = PropertyDrawers.PropertyDrawer;

    /// <summary>
    /// The NonSerializedAttributeWrapper.
    /// </summary>
    public class NonSerializedAttributeWrapper : ValueWrapper
    {
        private readonly Drawer childDrawer;

        private bool isValid = true;
        private bool isArray;
        private bool isClass;

        /// <inheritdoc />
        public NonSerializedAttributeWrapper(object target, MemberInfo memberInfo) 
            : base(target, memberInfo)
        {
            var type = this.Type;
            Debug.Log(type);
            var info = type.GetTypeInfo();
            this.isArray = info.IsArray;
            this.isClass = info.IsClass;

            /*if (this.isArray)
            {
                return;
            }

            if (EditorDrawUtility.IsDrawable(type))
            {
                return;
            }

            var v = this.GetValue();

            if (v == null && this.isClass)
            {
                try
                {
                    v = Activator.CreateInstance(this.Type);
                }

                catch(Exception)
                {
                    this.isValid = false;
                    return;
                }

                this.SetValue(v);
            }*/
        }

        /// <inheritdoc />
        public override string DisplayName => this.MemberInfo.Name;

        /// <inheritdoc />
        protected override bool HasChildren { get; } = false;
        /// <inheritdoc />
        protected override bool IsArray => false; // we don't allow non serialized array drawing at the moment

        /// <inheritdoc />
        protected override void ValidateField(ValueRunner validator, ValidatorAttribute attribute)
        {
            validator.Run(this, attribute);
        }

        /// <inheritdoc />
        protected override void DrawPropertyField(PropertyDrawer drawer, DrawerAttribute attribute)
        {
            if (!this.isValid)
            {
                return;
            }

            if (this.isArray)
            {
                return;
            }

            if (this.HasChildren)
            {
                this.childDrawer.OnInspectorGUI();
            }
            else
            {
                if (drawer == null || attribute == null)
                {
                    this.DrawDefaultField();
                }
                else
                {
                    drawer.Run(this, attribute);
                }
            }
        }

        /// <inheritdoc />
        public override void DrawDefaultField()
        {
            var value = this.GetValue();
            value = EditorDrawUtility.DrawPropertyField(value, this.Type, this.DisplayName);
            this.SetValue(value);
        }
    }
}