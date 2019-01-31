// <copyright file="FieldAttributeWrapper.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Reflection;
    using UnityEditor;

    public class FieldAttributeWrapper : ValueWrapper
    {
        private readonly FieldInfo fieldInfo;
        private readonly Drawer drawer;
        private bool foldout;

        public FieldAttributeWrapper(object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
            this.fieldInfo = fieldInfo;

            if (EditorDrawUtility.IsDrawable(this.Type))
            {
                var info = this.Type.GetTypeInfo();

                if (info.IsArray)
                {

                }
                else
                {
                    this.drawer = new Drawer(this.GetValue());
                }
            }
        }

        /// <inheritdoc />
        public sealed override Type Type => this.fieldInfo.FieldType;

        /// <inheritdoc />
        public sealed override object GetValue()
        {
            return this.fieldInfo.GetValue(this.Target);
        }

        /// <inheritdoc />
        public sealed override void SetValue(object value)
        {
            this.fieldInfo.SetValue(this.Target, value);
        }

        /// <inheritdoc />
        public override void ApplyModifications()
        {
        }

        /// <inheritdoc />
        public override void DrawPropertyField()
        {
            if (this.drawer != null)
            {
                this.foldout = EditorGUILayout.Foldout(this.foldout, this.DisplayName);

                if (this.foldout)
                {
                    this.drawer.OnInspectorGUI();
                }
            }
            else
            {
                this.SetValue(EditorDrawUtility.DrawPropertyField(this.GetValue(), this.Type, this.DisplayName));
            }
        }
    }
}