namespace BovineLabs.NaughtyAttributes.Editor
{
    using UnityEditor;

    public abstract class PropertyEnabledCondition
    {
        public abstract bool IsPropertyEnabled(SerializedProperty property);
    }
}
