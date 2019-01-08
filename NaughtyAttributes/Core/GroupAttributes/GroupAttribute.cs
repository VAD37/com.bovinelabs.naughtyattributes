using System;

namespace NaughtyAttributes
{
    public abstract class GroupAttribute : NaughtyAttribute
    {
        public string Name { get; private set; }

        public bool ShowName { get; private set; }

        public GroupAttribute(string name, bool showName = true)
        {
            this.Name = name;
            this.ShowName = showName;
        }
    }
}
