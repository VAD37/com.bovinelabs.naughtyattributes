// This class is auto generated

namespace BovineLabs.NaughtyAttributes.Editor.Database
{
    using System;
    using System.Collections.Generic;
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.PropertyEnabledConditions;

    public static class PropertyEnabledConditionDatabase
    {
        private static Dictionary<Type, PropertyEnabledCondition> drawConditionsByAttributeType;

        static PropertyEnabledConditionDatabase()
        {
            drawConditionsByAttributeType = new Dictionary<Type, PropertyEnabledCondition>();
            drawConditionsByAttributeType[typeof(DisableIfAttribute)] = new DisableIfPropertyDrawer();
            drawConditionsByAttributeType[typeof(EnableIfAttribute)] = new EnableIfPropertyDrawer();
            drawConditionsByAttributeType[typeof(DisableInEditorModeAttribute)] = new DisableInEditorModePropertyDrawer();
            drawConditionsByAttributeType[typeof(DisableInPlayModeAttribute)] = new DisableInPlayModePropertyDrawer();
            drawConditionsByAttributeType[typeof(ReadOnlyAttribute)] = new ReadOnlyPropertyDrawer();
        }

        public static PropertyEnabledCondition GetEnabledConditionForAttribute(Type attributeType)
        {
            PropertyEnabledCondition enabledCondition;
            if (drawConditionsByAttributeType.TryGetValue(attributeType, out enabledCondition))
            {
                return enabledCondition;
            }
            else
            {
                return null;
            }
        }
    }
}