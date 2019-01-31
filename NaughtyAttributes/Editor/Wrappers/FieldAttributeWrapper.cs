// <copyright file="FieldAttributeWrapper.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Reflection;
    using UnityEditor;

    public class FieldAttributeWrapper : ValueWrapper
    {
        private FieldInfo FieldInfo => (FieldInfo)this.MemberInfo;
        
        public FieldAttributeWrapper(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        /// <inheritdoc />
        public sealed override Type Type => this.FieldInfo.FieldType;

        /// <inheritdoc />
        public sealed override object GetValue()
        {
            return this.FieldInfo.GetValue(this.Target);
        }

        /// <inheritdoc />
        public sealed override void SetValue(object value)
        {
            this.FieldInfo.SetValue(this.Target, value);
        }

        /// <inheritdoc />
        public override void ApplyModifications()
        {
        }
    }
}