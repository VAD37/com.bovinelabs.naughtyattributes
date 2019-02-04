namespace BovineLabs.NaughtyAttributes.Editor.PropertyDrawConditions
{
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEngine.Assertions;

    public abstract class PropertyDrawCondition
    {
        public abstract bool CanDrawProperty(AttributeWrapper wrapper, NaughtyAttribute attribute);
    }

    public abstract class PropertyDrawCondition<T> : PropertyDrawCondition
        where T : NaughtyAttribute
    {
        public sealed override bool CanDrawProperty(AttributeWrapper wrapper, NaughtyAttribute attribute)
        {
            Assert.IsTrue(attribute is T);

            return this.CanDrawProperty(wrapper, (T)attribute);
        }

        protected abstract bool CanDrawProperty(AttributeWrapper wrapper, T attribute);
    }

}
