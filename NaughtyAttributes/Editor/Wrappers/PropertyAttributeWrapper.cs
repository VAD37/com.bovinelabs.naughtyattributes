// <copyright file="PropertyAttributeWrapper.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor.Wrappers
{
    using System;
    using System.Reflection;

    public class PropertyAttributeWrapper : ValueWrapper
    {
        private PropertyInfo PropertyInfo => (PropertyInfo)this.MemberInfo;

        public PropertyAttributeWrapper(object target, PropertyInfo propertyInfo)
            : base(target, propertyInfo)
        {
        }

        /// <inheritdoc />
        public override Type Type => this.PropertyInfo.PropertyType;

        /// <inheritdoc />
        public override object GetValue()
        {
            return this.PropertyInfo.GetValue(this.Target);
        }

        /// <inheritdoc />
        public override void SetValue(object value)
        {
            this.PropertyInfo.SetValue(this.Target, value);
        }

        /// <inheritdoc />
        public override void ApplyModifications()
        {
        }
    }
}