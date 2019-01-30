namespace BovineLabs.NaughtyAttributes.Editor
{
    using UnityEngine.Assertions;

    public abstract class PropertyEnabledCondition
    {
        public abstract bool IsPropertyEnabled(AttributeWrapper wrapper, NaughtyAttribute attribute);
    }

    public abstract class PropertyEnabledCondition<T> : PropertyEnabledCondition
        where T : NaughtyAttribute
    {
        public sealed override bool IsPropertyEnabled(AttributeWrapper wrapper, NaughtyAttribute attribute)
        {
            Assert.IsTrue(attribute is T);

            return this.IsPropertyEnabled(wrapper, (T)attribute);
        }

        protected abstract bool IsPropertyEnabled(AttributeWrapper wrapper, T attribute);
    }
}
