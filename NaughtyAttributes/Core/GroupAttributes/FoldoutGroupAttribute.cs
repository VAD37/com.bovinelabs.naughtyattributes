namespace BovineLabs.NaughtyAttributes
{
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class FoldoutGroupAttribute : GroupAttribute
    {
        public FoldoutGroupAttribute(string name = "", bool open = true, bool showName = true)
            : base(name, showName)
        {
        }
    }
}
