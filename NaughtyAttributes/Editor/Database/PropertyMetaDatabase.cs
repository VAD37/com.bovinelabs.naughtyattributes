namespace BovineLabs.NaughtyAttributes.Editor.Database
{
    using System;
    using System.Collections.Generic;
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.PropertyMetas;

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

