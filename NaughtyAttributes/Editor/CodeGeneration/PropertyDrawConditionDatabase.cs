// This class is auto generated

namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Collections.Generic;
    using BovineLabs.NaughtyAttributes;

    public static class PropertyDrawConditionDatabase
    {
        private static Dictionary<Type, PropertyDrawCondition> drawConditionsByAttributeType;

        static PropertyDrawConditionDatabase()
        {
            drawConditionsByAttributeType = new Dictionary<Type, PropertyDrawCondition>();
            drawConditionsByAttributeType[typeof(HideIfAttribute)] = new HideIfPropertyDrawCondition();
drawConditionsByAttributeType[typeof(HideInEditorModeAttribute)] = new HideInEditorModePropertyDrawCondition();
drawConditionsByAttributeType[typeof(HideInPlayModeAttribute)] = new HideInPlayModePropertyDrawCondition();
drawConditionsByAttributeType[typeof(ShowIfAttribute)] = new ShowIfPropertyDrawCondition();

        }

        public static PropertyDrawCondition GetDrawConditionForAttribute(Type attributeType)
        {
            PropertyDrawCondition drawCondition;
            if (drawConditionsByAttributeType.TryGetValue(attributeType, out drawCondition))
            {
                return drawCondition;
            }
            else
            {
                return null;
            }
        }
    }
}

