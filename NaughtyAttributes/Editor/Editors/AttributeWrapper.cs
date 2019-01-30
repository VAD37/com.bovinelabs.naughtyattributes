// <copyright file="AttributeWrapper.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
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

        public void ValidateAndDrawField()
        {
            this.ValidateField();
            this.ApplyFieldMeta();
        }

        public abstract object GetValue();

        public abstract void SetValue(object value);

        public abstract IEnumerable<T> GetCustomAttributes<T>()
            where T : Attribute;

        private void ValidateField()
        {
            var validatorAttributes = this.GetCustomAttributes<ValidatorAttribute>().ToArray();

            foreach (var attribute in validatorAttributes)
            {
                var validator = PropertyValidatorDatabase.GetValidatorForAttribute(attribute.GetType());
                validator?.ValidateProperty(this);
            }
        }

        private void ApplyFieldMeta()
        {

        }
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

        /// <inheritdoc />
        public sealed override IEnumerable<T> GetCustomAttributes<T>()
        {
            return this.memberInfo.GetCustomAttributes<T>(true);
        }
    }
}