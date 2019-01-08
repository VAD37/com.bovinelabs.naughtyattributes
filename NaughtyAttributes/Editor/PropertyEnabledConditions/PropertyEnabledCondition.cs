using UnityEditor;

namespace NaughtyAttributes.Editor
{
    public abstract class PropertyEnabledCondition
    {
        public abstract bool IsPropertyEnabled(SerializedProperty property);
    }
}
