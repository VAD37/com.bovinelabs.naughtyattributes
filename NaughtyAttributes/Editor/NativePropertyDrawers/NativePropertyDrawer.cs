using UnityEngine.Assertions;

namespace BovineLabs.NaughtyAttributes.Editor
{
    public abstract class NativePropertyDrawer<T> : AttributeRunner
        where T : NaughtyAttribute
    {
        /// <inheritdoc />
        public override void Run(AttributeWrapper wrapper, NaughtyAttribute attribute)
        {
            Assert.IsTrue(attribute is T);

            this.DrawNativeProperty(wrapper, (T)attribute);
        }

        protected abstract void DrawNativeProperty(AttributeWrapper wrapper, T attribute);
    }
}
