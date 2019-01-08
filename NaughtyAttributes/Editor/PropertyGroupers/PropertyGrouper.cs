using System;

namespace NaughtyAttributes.Editor
{
    public abstract class PropertyGrouper
    {
        public abstract void BeginGroup(GroupAttribute attribute);

        public abstract void EndGroup();
    }
}
