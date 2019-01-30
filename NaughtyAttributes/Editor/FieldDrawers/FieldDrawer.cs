/*amespace BovineLabs.NaughtyAttributes.Editor
{
    using UnityEngine.Assertions;

    public abstract class FieldDrawer<T> : AttributeRunner
        where T : NaughtyAttribute
    {
        /// <inheritdoc />
        public override void Run(AttributeWrapper wrapper, NaughtyAttribute attribute)
        {
            Assert.IsTrue(attribute is T);

            this.DrawField(wrapper, (T)attribute);
        }

        protected abstract void DrawField(AttributeWrapper wrapper, T attribute);
    }
}
*/