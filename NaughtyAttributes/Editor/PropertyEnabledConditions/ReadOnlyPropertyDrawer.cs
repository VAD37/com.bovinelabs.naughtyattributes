namespace BovineLabs.NaughtyAttributes.Editor.PropertyEnabledConditions
{
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.Attributes;
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;

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
