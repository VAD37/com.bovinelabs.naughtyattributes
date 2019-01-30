// <copyright file="PropertyMeta.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor
{
    using UnityEngine.Assertions;

    public abstract class PropertyMeta<T> : AttributeRunner
        where T : MetaAttribute
    {
        /// <inheritdoc />
        public sealed override void Run(AttributeWrapper wrapper, NaughtyAttribute attribute)
        {
            Assert.IsTrue(attribute is T);

            this.ApplyPropertyMeta(wrapper, (T)attribute);
        }

        protected abstract void ApplyPropertyMeta(AttributeWrapper wrapper, T attribute);
    }
}
