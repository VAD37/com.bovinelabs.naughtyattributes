// This class is auto generated

namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Collections.Generic;
    using BovineLabs.NaughtyAttributes;

    public static class PropertyValidatorDatabase
    {
        private static Dictionary<Type, AttributeRunner> validatorsByAttributeType;

        static PropertyValidatorDatabase()
        {
            validatorsByAttributeType = new Dictionary<Type, AttributeRunner>();
            validatorsByAttributeType[typeof(MaxValueAttribute)] = new MaxValuePropertyValidator();
            validatorsByAttributeType[typeof(MinValueAttribute)] = new MinValuePropertyValidator();
            validatorsByAttributeType[typeof(RequiredAttribute)] = new RequiredPropertyValidator();
            validatorsByAttributeType[typeof(ValidateInputAttribute)] = new ValidateInputPropertyValidator();
        }

        public static AttributeRunner GetValidatorForAttribute(Type attributeType)
        {
            AttributeRunner validator;
            if (validatorsByAttributeType.TryGetValue(attributeType, out validator))
            {
                return validator;
            }
            else
            {
                return null;
            }
        }
    }
}

