// This class is auto generated

namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Collections.Generic;
    using BovineLabs.NaughtyAttributes;

    public static class PropertyMetaDatabase
    {
        private static Dictionary<Type, AttributeRunner> metasByAttributeType;

        static PropertyMetaDatabase()
        {
            metasByAttributeType = new Dictionary<Type, AttributeRunner>();
            metasByAttributeType[typeof(InfoBoxAttribute)] = new InfoBoxPropertyMeta();
metasByAttributeType[typeof(OnValueChangedAttribute)] = new OnValueChangedPropertyMeta();

        }

        public static AttributeRunner GetMetaForAttribute(Type attributeType)
        {
            AttributeRunner meta;
            if (metasByAttributeType.TryGetValue(attributeType, out meta))
            {
                return meta;
            }
            else
            {
                return null;
            }
        }
    }
}

