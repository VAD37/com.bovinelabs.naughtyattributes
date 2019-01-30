namespace BovineLabs.NaughtyAttributes.Editor
{
    using UnityEngine.Assertions;

    public abstract class PropertyDrawer
    {
        public abstract void DrawProperty(AttributeWrapper wrapper, NaughtyAttribute attribute);

        public virtual void ClearCache()
        {
        }
    }

    public abstract class PropertyDrawer<T> : PropertyDrawer
        where T : NaughtyAttribute
    {
        /// <inheritdoc />
        public sealed override void DrawProperty(AttributeWrapper wrapper, NaughtyAttribute attribute)
        {
            Assert.IsTrue(attribute is T);

            this.DrawProperty(wrapper, (T)attribute);
        }

        protected abstract void DrawProperty(AttributeWrapper wrapper, T attribute);
    }
}
