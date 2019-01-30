// <copyright file="PropertyAttributeWrapper.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Reflection;
    using Object = UnityEngine.Object;

    public class PropertyAttributeWrapper : MemberInfoWrapper
    {
        private readonly PropertyInfo propertyInfo;

        public PropertyAttributeWrapper(Object target, PropertyInfo propertyInfo)
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
    }
}