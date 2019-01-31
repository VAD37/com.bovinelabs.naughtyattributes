namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Collections.Generic;
    using BovineLabs.NaughtyAttributes;

    public static class PropertyMetaDatabase
    {
        private static readonly Dictionary<Type, ValueRunner> metasByAttributeType;

        static PropertyMetaDatabase()
        {
            metasByAttributeType = new Dictionary<Type, ValueRunner>
            {
                [typeof(InfoBoxAttribute)] = new InfoBoxPropertyMeta(),
            };
        }

        public static ValueRunner GetMetaForAttribute(Type attributeType)
        {
            return metasByAttributeType.TryGetValue(attributeType, out var meta) ? meta : null;
        }
    }
}

