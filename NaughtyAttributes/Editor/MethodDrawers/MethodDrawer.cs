namespace BovineLabs.NaughtyAttributes.Editor.MethodDrawers
{
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEngine.Assertions;

    public abstract class MethodDrawer
    {
        public abstract void DrawMethod(MethodWrapper wrapper, NaughtyAttribute attribute);
    }

    public abstract class MethodDrawer<T> : MethodDrawer
        where T : MethodAttribute
    {
        /// <inheritdoc />
        public sealed override void DrawMethod(MethodWrapper wrapper, NaughtyAttribute attribute)
        {
            Assert.IsTrue(attribute is T);

            this.DrawMethod(wrapper, (T)attribute);
        }

        protected abstract void DrawMethod(MethodWrapper wrapper, T attribute);
    }
}
