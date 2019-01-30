namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;

    [PropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyPropertyDrawer : PropertyEnabledCondition
    {
        /// <inheritdoc />
        public override bool IsPropertyEnabled(SerializedProperty property)
        {
            return false;
        }
    }
}
