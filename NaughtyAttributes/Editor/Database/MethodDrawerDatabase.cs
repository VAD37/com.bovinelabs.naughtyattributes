// This class is auto generated

namespace BovineLabs.NaughtyAttributes.Editor.Database
{
    using System;
    using System.Collections.Generic;
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.MethodDrawers;

    public static class MethodDrawerDatabase
    {
        private static readonly Dictionary<Type, MethodDrawer> drawersByAttributeType;

        static MethodDrawerDatabase()
        {
            drawersByAttributeType = new Dictionary<Type, MethodDrawer>();
            drawersByAttributeType[typeof(ButtonAttribute)] = new ButtonMethodDrawer();

        }

        public static MethodDrawer GetDrawerForAttribute(Type attributeType)
        {
            return drawersByAttributeType.TryGetValue(attributeType, out var drawer) ? drawer : null;
        }
    }
}

