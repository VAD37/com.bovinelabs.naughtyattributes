namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;

    public interface IAttribute
    {
        Type TargetAttributeType { get; }
    }
}
