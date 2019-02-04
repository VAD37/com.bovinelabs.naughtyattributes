namespace BovineLabs.NaughtyAttributes.Editor.NativePropertyDrawers
{
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEngine.Assertions;

    public abstract class NativePropertyDrawer<T> : ValueRunner
        where T : NaughtyAttribute
    {
        /// <inheritdoc />
        public override void Run(ValueWrapper wrapper, NaughtyAttribute attribute)
        {
            Assert.IsTrue(attribute is T);

            this.DrawNativeProperty(wrapper, (T)attribute);
        }

        protected abstract void DrawNativeProperty(ValueWrapper wrapper, T attribute);
    }
}
