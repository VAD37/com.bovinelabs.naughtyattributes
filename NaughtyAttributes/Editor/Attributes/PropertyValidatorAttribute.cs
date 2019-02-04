namespace BovineLabs.NaughtyAttributes.Editor.Attributes
{
    using System;

    public class PropertyValidatorAttribute : BaseAttribute
    {
        public PropertyValidatorAttribute(Type targetAttributeType) : base(targetAttributeType)
        {
        }
    }
}
