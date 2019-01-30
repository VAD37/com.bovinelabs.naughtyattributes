namespace BovineLabs.NaughtyAttributes
{
    using System;

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class BoxGroupAttribute : GroupAttribute
    {
        public BoxGroupAttribute(string name = "", bool showName = true)
            : base(name, showName)
        {
        }
    }
}
