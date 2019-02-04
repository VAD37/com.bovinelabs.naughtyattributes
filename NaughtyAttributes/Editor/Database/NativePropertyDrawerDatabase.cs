// This class is auto generated

namespace BovineLabs.NaughtyAttributes.Editor.Database
{
    using System;
    using System.Collections.Generic;
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.NativePropertyDrawers;

    public static class NativePropertyDrawerDatabase
    {
        private static Dictionary<Type, ValueRunner> drawersByAttributeType;

        static NativePropertyDrawerDatabase()
        {
            drawersByAttributeType = new Dictionary<Type, ValueRunner>();
            drawersByAttributeType[typeof(ShowNativePropertyAttribute)] = new ShowNativePropertyNativePropertyDrawer();

        }

        public static ValueRunner GetDrawerForAttribute(Type attributeType)
        {
            ValueRunner drawer;
            if (drawersByAttributeType.TryGetValue(attributeType, out drawer))
            {
                return drawer;
            }

            return null;
        }
    }
}

