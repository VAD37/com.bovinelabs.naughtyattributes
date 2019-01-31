namespace BovineLabs.NaughtyAttributes
{
    using System;

    [AttributeUsage(AttributeTargets.Field)]
    public class ShowNonSerializedFieldAttribute : NaughtyAttribute
    {
    }
}