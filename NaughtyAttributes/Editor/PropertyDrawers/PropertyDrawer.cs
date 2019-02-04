namespace BovineLabs.NaughtyAttributes.Editor.PropertyDrawers
{
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEngine.Assertions;

    public abstract class PropertyDrawer : ValueRunner
    {

    }

    public abstract class PropertyDrawer<T> : PropertyDrawer
        where T : NaughtyAttribute
    {
        /// <inheritdoc />
        public override void Run(ValueWrapper wrapper, NaughtyAttribute attribute)
        {
            Assert.IsTrue(attribute is T);

            this.DrawProperty(wrapper, (T)attribute);
        }

        protected abstract void DrawProperty(ValueWrapper wrapper, T attribute);


    }
}
