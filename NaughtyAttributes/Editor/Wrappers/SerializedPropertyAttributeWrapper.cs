namespace BovineLabs.NaughtyAttributes.Editor.Wrappers
{
    using System;
    using System.Linq;
    using System.Reflection;
    using BovineLabs.NaughtyAttributes.Editor.Editors;
    using BovineLabs.NaughtyAttributes.Editor.PropertyDrawers;
    using BovineLabs.NaughtyAttributes.Editor.PropertyMetas;
    using BovineLabs.NaughtyAttributes.Editor.Utility;
    using UnityEditor;
    using PropertyDrawer = PropertyDrawers.PropertyDrawer;

    public class SerializedPropertyAttributeWrapper : ValueWrapper
    {
        private readonly Drawer[] children;

        public SerializedPropertyAttributeWrapper(SerializedObject serializedObject, object target,
            SerializedProperty property, FieldInfo fieldInfo)
            : base(serializedObject, target, fieldInfo)
        {
            this.Property = property;

            if (!property.hasChildren)
            {
                return;
            }

            var t = this.GetValue();
            //this.children = property.GetChildren().Select(c => new Drawer(serializedObject, t, c)).ToArray();
        }

        public SerializedProperty Property { get; } 
        
        /// <inheritdoc />
        protected override bool HasChildren => this.Property.hasChildren;

        /// <inheritdoc />
        protected override bool IsArray => this.Property.isArray;

        /// <inheritdoc />
        protected override void ValidateField(ValueRunner validator, ValidatorAttribute attribute)
        {
            validator.Run(this, attribute);
        }

        /// <inheritdoc />
        public override string DisplayName => this.Property.displayName;

        /// <inheritdoc />
        protected override void DrawPropertyField(PropertyDrawer drawer, DrawerAttribute attribute)
        {
            if (this.Property.hasChildren)
            {
                return;
                foreach (var child in this.children)
                {
                    child.OnInspectorGUI();
                }
            }
            else if (this.Property.isArray)
            {
                ListPropertyDrawer.Instance.DrawArray(this);
            }
            else
            {
                if (drawer == null || attribute == null)
                {
                    this.DrawDefaultField();
                }
                else
                {
                    drawer.Run(this, attribute);
                }
            }
        }

        /// <inheritdoc />
        public override void DrawDefaultField()
        {
            EditorGUILayout.PropertyField(this.Property, false);
        }
    }
}