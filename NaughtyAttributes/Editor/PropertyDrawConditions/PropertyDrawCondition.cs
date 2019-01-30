namespace BovineLabs.NaughtyAttributes.Editor
{
    using UnityEditor;

    public abstract class PropertyDrawCondition
    {
        public abstract bool CanDrawProperty(SerializedProperty property);
    }
}
