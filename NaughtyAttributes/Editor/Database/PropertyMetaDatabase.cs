namespace BovineLabs.NaughtyAttributes.Editor.Database
{
    using System;
    using System.Collections.Generic;
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.PropertyMetas;

    public static class PropertyMetaDatabase
    {
        private static readonly Dictionary<Type, PropertyMeta> metasByAttributeType;

        static PropertyMetaDatabase()
        {
            metasByAttributeType = new Dictionary<Type, PropertyMeta>
            {
                [typeof(InfoBoxAttribute)] = new InfoBoxPropertyMeta(),
            };
        }

        public static PropertyMeta GetMetaForAttribute(Type attributeType)
        {
            return metasByAttributeType.TryGetValue(attributeType, out var meta) ? meta : null;
        }
    }
}

