using System;

namespace NaughtyAttributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public class FoldoutGroupAttribute : GroupAttribute
    {
        public FoldoutGroupAttribute(string name = "", bool open = true)
            : base(name)
        {
        }
    }
}
