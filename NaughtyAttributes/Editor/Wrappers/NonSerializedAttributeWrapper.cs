// <copyright file="NonSerializedAttributeWrapper.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor.Wrappers
{
    using System;
    using System.Reflection;
    using UnityEditor;
    using PropertyDrawer = PropertyDrawers.PropertyDrawer;

    /// <summary>
    /// The NonSerializedAttributeWrapper.
    /// </summary>
    public class NonSerializedAttributeWrapper : ValueWrapper
    {
        /// <inheritdoc />
        public NonSerializedAttributeWrapper(SerializedObject rootObject, object target, MemberInfo memberInfo) : base(rootObject, target, memberInfo)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override string DisplayName => this.MemberInfo.Name;

        /// <inheritdoc />
        protected override bool HasChildren { get; }
        /// <inheritdoc />
        protected override bool IsArray { get; }

        /// <inheritdoc />
        protected override void ValidateField(ValueRunner validator, ValidatorAttribute attribute)
        {
        }

        /// <inheritdoc />
        protected override void DrawPropertyField(PropertyDrawer drawer, DrawerAttribute attribute)
        {
        }

        /// <inheritdoc />
        public override void DrawDefaultField()
        {
        }
    }
}