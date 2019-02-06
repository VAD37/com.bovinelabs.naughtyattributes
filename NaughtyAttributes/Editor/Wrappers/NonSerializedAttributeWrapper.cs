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
        private readonly bool isArray;

        /// <inheritdoc />
        public NonSerializedAttributeWrapper(object target, MemberInfo memberInfo) 
            : base(target, memberInfo)
        {
            var type = this.Type;
            var info = type.GetTypeInfo();
            this.isArray = info.IsArray;

            if (this.isArray)
            {
                return;
            }

            if (EditorDrawUtility.IsDrawable(type))
            {
                return;
            }

            var value = this.GetValue();

            if (value == null)
            {
                try
                {
                    value = Activator.CreateInstance(this.Type);
                }
                catch (Exception)
                {
                    return;
                }
            }

            this.childDrawer = new Drawer(value);

            if (this.childDrawer.HasElement)
            {
                this.HasChildren = true;
            }
        }

        /// <inheritdoc />
        public override string DisplayName => this.MemberInfo.Name;

        /// <inheritdoc />
        protected override bool HasChildren { get; }
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