// This class is auto generated

namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Collections.Generic;
    using BovineLabs.NaughtyAttributes;

    public static class PropertyDrawConditionDatabase
    {
        private static readonly Dictionary<Type, PropertyDrawCondition> drawConditionsByAttributeType;

        static PropertyDrawConditionDatabase()
        {
            drawConditionsByAttributeType = new Dictionary<Type, PropertyDrawCondition>
            {
                [typeof(HideIfAttribute)] = new HideIfPropertyDrawCondition(),
                [typeof(HideInEditorModeAttribute)] = new HideInEditorModePropertyDrawCondition(),
                [typeof(HideInPlayModeAttribute)] = new HideInPlayModePropertyDrawCondition(),
                [typeof(ShowIfAttribute)] = new ShowIfPropertyDrawCondition()
            };
        }

        public static PropertyDrawCondition GetDrawConditionForAttribute(Type attributeType)
        {
            if (drawConditionsByAttributeType.TryGetValue(attributeType, out var drawCondition))
            {
                return drawCondition;
            }

            return null;
        }
    }
}