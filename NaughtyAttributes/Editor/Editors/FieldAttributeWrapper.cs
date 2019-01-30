// <copyright file="AttributeWrapper.cs" company="Timothy Raines">
//     Copyright (c) Timothy Raines. All rights reserved.
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
    }

    public class NonSerializedFieldAttributeWrapper : FieldAttributeWrapper
    {
        public NonSerializedFieldAttributeWrapper(Object target, FieldInfo fieldInfo)
            : base(target, fieldInfo)
        {
        }
    }
}