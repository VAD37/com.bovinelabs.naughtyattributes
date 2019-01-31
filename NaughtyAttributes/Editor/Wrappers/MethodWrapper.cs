// <copyright file="MethodWrapper.cs" company="Timothy Raines">
//     Copyright (c) Timothy Raines. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Object = UnityEngine.Object;

    /// <summary>
    /// The MethodWrapper.
    /// </summary>
    public class MethodWrapper : AttributeWrapper
    {
        private Object target;
        private MethodInfo methodInfo;

        public MethodWrapper(Object target, MethodInfo methodInfo) 
            : base(target)
        {
            this.target = target;
            this.methodInfo = methodInfo;
        }

        /// <inheritdoc />
        public sealed override string Name => this.methodInfo.Name;

        /// <inheritdoc />
        public override string DisplayName => this.Name;

        /// <inheritdoc />
        public override void ValidateAndDrawField()
        {
        }

        /// <inheritdoc />
        public override IEnumerable<T> GetCustomAttributes<T>()
        {
            return this.methodInfo.GetCustomAttributes<T>(true);
        }
    }
}