namespace BovineLabs.NaughtyAttributes.Editor.Editors
{
    using BovineLabs.NaughtyAttributes.Editor.Database;
    using BovineLabs.NaughtyAttributes.Editor.PropertyDrawers;
    using UnityEditor;
    using UnityEngine;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(Object), true, isFallback = true)]
    public class InspectorEditor : Editor
    {
        private SerializedProperty script;
        private Drawer drawer;

        public override void OnInspectorGUI()
        {
            this.serializedObject.Update();

            if (this.drawer != null)
            {
                if (this.script != null)
                {
                    GUI.enabled = false;
                    EditorGUILayout.PropertyField(this.script);
                    GUI.enabled = true;
                }

                this.drawer.OnInspectorGUI();
            }
            else
            {
                this.DrawDefaultInspector();
            }

            this.serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            if (this.target is MonoBehaviour || this.target is ScriptableObject)
            {
                this.script = this.serializedObject.FindProperty("m_Script");
                this.drawer = new Drawer(this.serializedObject, this.target);
            }
        }

        private void OnDisable()
        {
            ListPropertyDrawer.Instance.ClearCache();
        }
    }
}
