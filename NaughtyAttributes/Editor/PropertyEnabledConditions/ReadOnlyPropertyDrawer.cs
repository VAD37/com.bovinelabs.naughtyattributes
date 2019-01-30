namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes;

    [PropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyPropertyDrawer : PropertyEnabledCondition<ReadOnlyAttribute>
    {
        /// <inheritdoc />
        protected override bool IsPropertyEnabled(AttributeWrapper wrapper, ReadOnlyAttribute attribute)
        {
            return false;
        }
    }
}
