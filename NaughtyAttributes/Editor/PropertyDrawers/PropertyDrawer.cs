namespace BovineLabs.NaughtyAttributes.Editor.PropertyDrawers
{
    using BovineLabs.NaughtyAttributes.Editor.Utility;
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEngine.Assertions;

    public abstract class PropertyDrawer : ValueRunner
    {

    }

    public abstract class PropertyDrawer<T> : PropertyDrawer
        where T : NaughtyAttribute
    {
        /// <inheritdoc />
        public override void Run(NonSerializedAttributeWrapper wrapper, NaughtyAttribute attribute)
        {
            Assert.IsTrue(attribute is T);

            EditorDrawUtility.DrawHeader(wrapper);

            this.DrawProperty(wrapper, (T)attribute);
        }

        /// <inheritdoc />
        public override void Run(SerializedPropertyAttributeWrapper wrapper, NaughtyAttribute attribute)
        {
            Assert.IsTrue(attribute is T);

            EditorDrawUtility.DrawHeader(wrapper);

            this.DrawProperty(wrapper, (T)attribute);
        }

        protected abstract void DrawProperty(NonSerializedAttributeWrapper wrapper, T attribute);
        protected abstract void DrawProperty(SerializedPropertyAttributeWrapper wrapper, T attribute);
    }
}
