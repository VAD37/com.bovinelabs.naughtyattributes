namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes;

    public abstract class PropertyGrouper
    {
        public abstract void BeginGroup(GroupAttribute attribute);

        public abstract void EndGroup();
    }
}
