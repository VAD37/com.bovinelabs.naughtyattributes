// This class is auto generated

namespace BovineLabs.NaughtyAttributes.Editor.Database
{
    using System;
    using System.Collections.Generic;
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.PropertyGroupers;

    public static class PropertyGrouperDatabase
    {
        private static Dictionary<Type, PropertyGrouper> groupersByAttributeType;

        static PropertyGrouperDatabase()
        {
            groupersByAttributeType = new Dictionary<Type, PropertyGrouper>();
            groupersByAttributeType[typeof(BoxGroupAttribute)] = new BoxGroupPropertyGrouper();
groupersByAttributeType[typeof(FoldoutGroupAttribute)] = new FoldoutGroupPropertyGrouper();

        }

        public static PropertyGrouper GetGrouperForAttribute(Type attributeType)
        {
            PropertyGrouper grouper;
            if (groupersByAttributeType.TryGetValue(attributeType, out grouper))
            {
                return grouper;
            }
            else
            {
                return null;
            }
        }
    }
}

