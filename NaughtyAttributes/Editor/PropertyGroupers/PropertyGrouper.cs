namespace BovineLabs.NaughtyAttributes.Editor.PropertyGroupers
{
    using BovineLabs.NaughtyAttributes;

    public abstract class PropertyGrouper
    {
        public abstract void BeginGroup(GroupAttribute attribute);

        public abstract void EndGroup();
    }
}
