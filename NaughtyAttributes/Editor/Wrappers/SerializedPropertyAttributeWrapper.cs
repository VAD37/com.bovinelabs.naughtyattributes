namespace BovineLabs.NaughtyAttributes.Editor.Wrappers
{
    using System.Reflection;
    using BovineLabs.NaughtyAttributes.Editor.Editors;
    using UnityEditor;
    using PropertyDrawer = PropertyDrawers.PropertyDrawer;

    public class SerializedPropertyAttributeWrapper : ValueWrapper
    {
        private readonly Drawer childDrawer;

        public SerializedPropertyAttributeWrapper(SerializedObject rootObject, object target,
            SerializedProperty property, FieldInfo fieldInfo)        
            : base(rootObject, target, fieldInfo)
        {
            this.Property = property;

            this.IsArray = this.Property.isArray;
            this.HasChildren = !this.IsArray && this.Property.hasChildren &&
                               this.Property.propertyType == SerializedPropertyType.Generic;

            if (this.HasChildren)
            {
                this.childDrawer = new Drawer(rootObject, this.GetValue(), property);
            }
        }

        public SerializedProperty Property { get; }

        /// <inheritdoc />
        protected sealed override bool HasChildren { get; }

        /// <inheritdoc />
        protected sealed override bool IsArray { get; }

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
            if (this.HasChildren)
            {
                this.childDrawer.OnInspectorGUI();
            }
            else if (this.IsArray)
            {
                //ListPropertyDrawer.Instance.DrawArray(this);
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