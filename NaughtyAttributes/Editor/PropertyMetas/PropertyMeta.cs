// <copyright file="PropertyMeta.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor.PropertyMetas
{
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEngine.Assertions;

    public abstract class PropertyMeta
    {
        public abstract void Run(ValueWrapper wrapper, NaughtyAttribute attribute);
    }

    public abstract class PropertyMeta<T> : PropertyMeta
        where T : MetaAttribute
    {
        /// <inheritdoc />
        public override void Run(ValueWrapper wrapper, NaughtyAttribute attribute)
        {
            Assert.IsTrue(attribute is T);

            this.ApplyPropertyMeta(wrapper, (T)attribute);
        }

        protected abstract void ApplyPropertyMeta(ValueWrapper wrapper, T attribute);
    }
}
