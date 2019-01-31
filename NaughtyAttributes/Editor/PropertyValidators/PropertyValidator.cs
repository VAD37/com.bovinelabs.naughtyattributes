namespace BovineLabs.NaughtyAttributes.Editor
{
    using UnityEngine.Assertions;

    public abstract class PropertyValidator<T> : ValueRunner
        where T : NaughtyAttribute
    {
        /// <inheritdoc />
        public sealed override void Run(ValueWrapper wrapper, NaughtyAttribute attribute)
        {
            Assert.IsTrue(attribute is T);

            this.ValidateProperty(wrapper, (T)attribute);
        }

        protected abstract void ValidateProperty(ValueWrapper wrapper, T attribute);
    }
}
