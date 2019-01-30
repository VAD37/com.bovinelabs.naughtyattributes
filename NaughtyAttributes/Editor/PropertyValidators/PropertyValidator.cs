namespace BovineLabs.NaughtyAttributes.Editor
{
    using UnityEngine.Assertions;

    public abstract class PropertyValidator<T> : AttributeRunner
        where T : NaughtyAttribute
    {
        /// <inheritdoc />
        public sealed override void Run(AttributeWrapper wrapper, NaughtyAttribute attribute)
        {
            Assert.IsTrue(attribute is T);

            this.ValidateProperty(wrapper, (T)attribute);
        }

        protected abstract void ValidateProperty(AttributeWrapper wrapper, T attribute);
    }
}
