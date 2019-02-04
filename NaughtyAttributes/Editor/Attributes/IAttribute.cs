namespace BovineLabs.NaughtyAttributes.Editor.Attributes
{
    using System;

    public interface IAttribute
    {
        Type TargetAttributeType { get; }
    }
}
