namespace BovineLabs.NaughtyAttributes.Editor.Wrappers
{
    using System.Reflection;
    using BovineLabs.NaughtyAttributes.Editor.Editors;
    using BovineLabs.NaughtyAttributes.Editor.PropertyDrawers;
    using BovineLabs.NaughtyAttributes.Editor.PropertyMetas;
    using UnityEditor;
    using PropertyDrawer = PropertyDrawers.PropertyDrawer;

    public class SerializedPropertyAttributeWrapper : ValueWrapper
    {
        private readonly Drawer childDrawer;

        public SerializedPropertyAttributeWrapper(object target,
            SerializedProperty property, FieldInfo fieldInfo)        
            : base( target, fieldInfo)
        {
            this.Property = property;

            this.IsArray = this.Property.isArray;
            if (!this.IsArray && this.Property.hasChildren &&
                this.Property.propertyType == SerializedPropertyType.Generic)
            {
                this.childDrawer = new Drawer(this.GetValue(), property);
                if (this.childDrawer.HasElement)
                {
                    this.HasChildren = true;
                }
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
                ListPropertyDrawer.Instance.DrawArray(this.Property);
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

        /// <inheritdoc />
        protected override void OnEndChangeCheck()
        {
            var onValueChangedAttributes = this.GetCustomAttributes<OnValueChangedAttribute>();
            foreach (var onValueChangedAttribute in onValueChangedAttributes)
            {
                OnValueChangedProperty.Instance.ApplyPropertyMeta(this, onValueChangedAttribute);
            }
        }
    }
}