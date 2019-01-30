namespace BovineLabs.NaughtyAttributes
{
    using System;

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ReadOnlyAttribute : EnabledConditionAttribute
    {
    }
}
