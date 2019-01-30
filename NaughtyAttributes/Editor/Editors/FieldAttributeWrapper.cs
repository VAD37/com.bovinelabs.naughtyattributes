// <copyright file="FieldAttributeWrapper.cs" company="BovineLabs">
//     Copyright (c) BovineLabs. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Reflection;
    using UnityEditor;
    using Object = UnityEngine.Object;

    public abstract class FieldAttributeWrapper : MemberInfoWrapper
    {
        private readonly FieldInfo fieldInfo;

        public FieldAttributeWrapper(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
            this.fieldInfo = fieldInfo;
        }

        /// <inheritdoc />
        public override Type Type => this.fieldInfo.FieldType;

        /// <inheritdoc />
        public override object GetValue()
        {
            return this.fieldInfo.GetValue(this.Target);
        }

        /// <inheritdoc />
        public override void SetValue(object value)
        {
            this.fieldInfo.SetValue(this.Target, value);
        }
    }

    public class SerializedFieldAttributeWrapper : FieldAttributeWrapper
    {
        private SerializedProperty serializedProperty;

        public SerializedFieldAttributeWrapper(SerializedProperty serializedProperty, Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
            this.serializedProperty = serializedProperty;
        }

        /// <inheritdoc />
        public override void ApplyModifications()
        {
            serializedProperty.serializedObject.ApplyModifiedProperties(); 
        }

        /// <inheritdoc />
        protected override void DrawPropertyField()
        {
            EditorDrawUtility.DrawPropertyField(this.serializedProperty);
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
        protected override void DrawPropertyField()
        {
            throw new NotImplementedException();
        }
    }
}