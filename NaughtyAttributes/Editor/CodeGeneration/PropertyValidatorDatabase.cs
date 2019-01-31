// This class is auto generated

namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Collections.Generic;
    using BovineLabs.NaughtyAttributes;

    public static class PropertyValidatorDatabase
    {
        private static readonly Dictionary<Type, ValueRunner> validatorsByAttributeType;

        static PropertyValidatorDatabase()
        {
            validatorsByAttributeType = new Dictionary<Type, ValueRunner>
            {
                [typeof(MaxValueAttribute)] = new MaxValuePropertyValidator(),
                [typeof(MinValueAttribute)] = new MinValuePropertyValidator(),
                [typeof(RequiredAttribute)] = new RequiredPropertyValidator(),
                [typeof(ValidateInputAttribute)] = new ValidateInputPropertyValidator()
            };
        }

        public static ValueRunner GetValidatorForAttribute(Type attributeType)
        {
            return validatorsByAttributeType.TryGetValue(attributeType, out var validator) ? validator : null;
        }
    }
}

