// <copyright file="PropertyAttributeWrapper.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Reflection;

    public class PropertyAttributeWrapper : ValueWrapper
    {
        private readonly PropertyInfo propertyInfo;

        public PropertyAttributeWrapper(object target, PropertyInfo propertyInfo)
            : base(target, propertyInfo)
        {
            this.propertyInfo = propertyInfo;
        }

        /// <inheritdoc />
        public override Type Type => this.propertyInfo.PropertyType;

        /// <inheritdoc />
        public override object GetValue()
        {
            return this.propertyInfo.GetValue(this.Target);
        }

        /// <inheritdoc />
        public override void SetValue(object value)
        {
            this.propertyInfo.SetValue(this.Target, value);
        }

        /// <inheritdoc />
        public override void ApplyModifications()
        {
        }

        /// <inheritdoc />
        public override void DrawPropertyField()
        {
            var result = this.GetValue();


        }
    }
}