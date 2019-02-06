namespace BovineLabs.NaughtyAttributes.Editor.PropertyValidators
{
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEngine.Assertions;

    public abstract  class PropertyValidator<T> : ValueRunner
        where T : NaughtyAttribute
    {
        /// <inheritdoc />
        public override void Run(NonSerializedAttributeWrapper wrapper, NaughtyAttribute attribute)
        {
            Assert.IsTrue(attribute is T);

            this.ValidateProperty(wrapper, (T)attribute);
        }

        /// <inheritdoc />
        public override void Run(SerializedPropertyAttributeWrapper wrapper, NaughtyAttribute attribute)
        {
            Assert.IsTrue(attribute is T);

            this.ValidateProperty(wrapper, (T)attribute);
        }

        protected abstract void ValidateProperty(NonSerializedAttributeWrapper wrapper, T attribute);
        protected abstract void ValidateProperty(SerializedPropertyAttributeWrapper wrapper, T attribute);
    }
}
