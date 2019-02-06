// <copyright file="ValueWrapper.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor.Wrappers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using BovineLabs.NaughtyAttributes.Editor.Database;
    using BovineLabs.NaughtyAttributes.Editor.PropertyMetas;
    using UnityEditor;
    using UnityEngine;
    using PropertyDrawer = PropertyDrawers.PropertyDrawer;

    public abstract class ValueWrapper : AttributeWrapper
    {
        public MemberInfo MemberInfo { get; }

        //private readonly Drawer drawer;
        private bool foldout;

        protected ValueWrapper(object target, MemberInfo memberInfo)
        {
            this.Target = target;
            this.MemberInfo = memberInfo;

            /*if (EditorDrawUtility.IsDrawable(this.Type))
            {
                var info = this.Type.GetTypeInfo();

                if (!info.IsArray)
                {
                    var array = this.GetValue();
                    if (array == null)
                    {
                        array = Activator.CreateInstance(this.Type);
                        this.SetValue(array);
                    }

                    this.drawer = new Drawer(array);
                }
            }*/
        }

        public sealed override object Target { get; }

        public abstract string DisplayName { get; }

        protected abstract bool HasChildren { get; }
        protected abstract bool IsArray { get; }

        protected abstract void ValidateField(ValueRunner validator, ValidatorAttribute attribute);
        protected abstract void DrawPropertyField(PropertyDrawer drawer, DrawerAttribute attribute);

        public Type Type
        {
            get
            {
                if (this.MemberInfo is FieldInfo fieldInfo)
                {
                    return fieldInfo.FieldType;
                }

                if (this.MemberInfo is PropertyInfo propertyInfo)
                {
                    return propertyInfo.PropertyType;
                }

                return null;
            }
        }

        public abstract void DrawDefaultField();

        public override void ValidateAndDrawField()
        {
            if (!this.CanDraw())
            {
                return;
            }

            this.ValidateField();
            this.ApplyFieldMeta();
            this.DrawField();
        }

        public object GetValue()
        {
            if (this.MemberInfo is FieldInfo fieldInfo)
            {
                return fieldInfo.GetValue(this.Target);
            }

            if (this.MemberInfo is PropertyInfo propertyInfo)
            {
                return propertyInfo.GetValue(this.Target);
            }

            return null;
        }

        public void SetValue(object value)
        {
            if (this.MemberInfo is FieldInfo fieldInfo)
            {
                fieldInfo.SetValue(this.Target, value);
            }
            else if (this.MemberInfo is PropertyInfo propertyInfo)
            {
                propertyInfo.SetValue(this.Target, value);
            }
        }

        /*public void DrawPropertyField()
        {
            if (this.drawer != null)
            {
                if (!this.drawer.HasElement)
                {
                    return;
                }

                this.foldout = EditorGUILayout.Foldout(this.foldout, this.DisplayName);

                if (this.foldout)
                {
                    EditorGUI.indentLevel += 1;
                    this.drawer.OnInspectorGUI();
                    EditorGUI.indentLevel -= 1;
                }
            }
            else
            {
                if (this.Type.IsArray)
                {
                    this.foldout = EditorGUILayout.Foldout(this.foldout, this.DisplayName);

                    if (this.foldout)
                    {
                        ListPropertyDrawer.Instance.DrawArray(this);
                    }
                }
                else
                {
                    this.SetValue(EditorDrawUtility.DrawPropertyField(this.GetValue(), this.Type, this.DisplayName));
                }
            }
        }*/

        /// <inheritdoc />
        public sealed override IEnumerable<T> GetCustomAttributes<T>()
        {
            return this.MemberInfo.GetCustomAttributes<T>(true);
        }

        private void ValidateField()
        {
            var validatorAttributes = this.GetCustomAttributes<ValidatorAttribute>().ToArray();

            foreach (var attribute in validatorAttributes)
            {
                var validator = PropertyValidatorDatabase.GetValidatorForAttribute(attribute.GetType());
                if (validator != null)
                {
                    this.ValidateField(validator, attribute);
                }
            }
        }

        private void ApplyFieldMeta()
        {
            // Apply custom meta attributes
            MetaAttribute[] metaAttributes = this.GetCustomAttributes<MetaAttribute>().ToArray();

            Array.Sort(metaAttributes, (x, y) => x.Order - y.Order);

            foreach (var attribute in metaAttributes)
            {
                var meta = PropertyMetaDatabase.GetMetaForAttribute(attribute.GetType());
                meta?.Run(this, attribute);
            }
        }

        private void DrawField()
        {
            var isPropertyEnabled = this.IsEnabled();

            // Draw the field
            EditorGUI.BeginChangeCheck();
            GUI.enabled = isPropertyEnabled;

            PropertyDrawer drawer = null;
            DrawerAttribute attribute = null;

            var drawerAttributes = this.GetCustomAttributes<DrawerAttribute>().ToArray();
            if (drawerAttributes.Length > 0)
            {
                attribute = drawerAttributes[0];
                drawer = PropertyDrawerDatabase.GetDrawerForAttribute(attribute.GetType());
            }

            if (this.HasChildren)
            {
                this.foldout = EditorGUILayout.Foldout(this.foldout, this.DisplayName);

                if (this.foldout)
                {
                    EditorGUI.indentLevel += 1;
                    this.DrawPropertyField(drawer, attribute);
                    EditorGUI.indentLevel -= 1;
                }
            }
            else if (this.IsArray)
            {
                this.foldout = EditorGUILayout.Foldout(this.foldout, this.DisplayName);

                if (this.foldout)
                {
                    this.DrawPropertyField(drawer, attribute);
                }
            }
            else
            {
                this.DrawPropertyField(drawer, attribute);
            }

            GUI.enabled = true;

            if (EditorGUI.EndChangeCheck())
            {
                this.OnEndChangeCheck();
            }
        }

        protected virtual void OnEndChangeCheck() { }
    }
}