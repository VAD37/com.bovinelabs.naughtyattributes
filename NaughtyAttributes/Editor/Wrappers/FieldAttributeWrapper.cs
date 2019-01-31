// <copyright file="FieldAttributeWrapper.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Reflection;
    using UnityEditor;
    using Object = UnityEngine.Object;

    public abstract class FieldAttributeWrapper : ValueWrapper
    {
        protected FieldInfo FieldInfo { get; }

        public FieldAttributeWrapper(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
            this.FieldInfo = fieldInfo;
        }

        /// <inheritdoc />
        public override Type Type => this.FieldInfo.FieldType;

        /// <inheritdoc />
        public override object GetValue()
        {
            return this.FieldInfo.GetValue(this.Target);
        }

        /// <inheritdoc />
        public override void SetValue(object value)
        {
            this.FieldInfo.SetValue(this.Target, value);
        }
    }

    public class SerializedFieldAttributeWrapper : FieldAttributeWrapper
    {
        public SerializedFieldAttributeWrapper(SerializedProperty serializedProperty, Object target,
            FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
            this.SerializedProperty = serializedProperty;
        }

        public SerializedProperty SerializedProperty { get; }

        /// <inheritdoc />
        public override string DisplayName => this.SerializedProperty.displayName;

        /// <inheritdoc />
        public override void ApplyModifications()
        {
            this.SerializedProperty.serializedObject.ApplyModifiedProperties();
        }

        /// <inheritdoc />
        public override void DrawPropertyField()
        {
            EditorDrawUtility.DrawPropertyField(this.SerializedProperty);
        }
    }

    public class NonSerializedFieldAttributeWrapper : FieldAttributeWrapper
    {
        public NonSerializedFieldAttributeWrapper(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }

        /// <inheritdoc />
        public override void ApplyModifications()
        {
        }

        /// <inheritdoc />
        public override void DrawPropertyField()
        {
            var result = EditorDrawUtility.DrawPropertyField(this.GetValue(), this.Type, this.DisplayName);
            this.SetValue(result);
        }
    }
}