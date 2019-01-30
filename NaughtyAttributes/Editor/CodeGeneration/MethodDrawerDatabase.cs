// This class is auto generated

namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Collections.Generic;
    using BovineLabs.NaughtyAttributes;

    public static class MethodDrawerDatabase
    {
        private static Dictionary<Type, MethodDrawer> drawersByAttributeType;

        static MethodDrawerDatabase()
        {
            drawersByAttributeType = new Dictionary<Type, MethodDrawer>();
            drawersByAttributeType[typeof(ButtonAttribute)] = new ButtonMethodDrawer();

        }

        public static MethodDrawer GetDrawerForAttribute(Type attributeType)
        {
            MethodDrawer drawer;
            if (drawersByAttributeType.TryGetValue(attributeType, out drawer))
            {
                return drawer;
            }
            else
            {
                return null;
            }
        }
    }
}

