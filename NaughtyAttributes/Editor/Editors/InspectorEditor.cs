namespace BovineLabs.NaughtyAttributes.Editor
{
    using UnityEditor;
    using UnityEngine;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(Object), true)]
    public class InspectorEditor : Editor
    {
        private SerializedProperty script;
        private Drawer drawer;

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();

            if (this.script != null)
            {
                GUI.enabled = false;
                EditorGUILayout.PropertyField(this.script);
                GUI.enabled = true;
            }

            this.drawer.OnInspectorGUI();

            this.serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            this.script = this.serializedObject.FindProperty("m_Script");

            this.drawer = new Drawer(this.target, this.serializedObject);
        }

        private void OnDisable()
        {
            PropertyDrawerDatabase.ClearCache();
        }
    }
}
