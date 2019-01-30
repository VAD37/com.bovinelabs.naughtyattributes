namespace BovineLabs.NaughtyAttributes.Editor
{
    using UnityEditor;

    public abstract class PropertyDrawer
    {
        public abstract void DrawProperty(SerializedProperty property);

        public virtual void ClearCache()
        {

        }
    }
}
