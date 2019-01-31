// <copyright file="MethodWrapper.cs" company="Timothy Raines">
//     Copyright (c) Timothy Raines. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEngine;
    using Object = UnityEngine.Object;

    /// <summary>
    /// The MethodWrapper.
    /// </summary>
    public class MethodWrapper : AttributeWrapper
    {
        public MethodInfo MethodInfo { get; }

        public MethodWrapper(Object target, MethodInfo methodInfo) 
            : base(target)
        {
            this.MethodInfo = methodInfo;
        }

        /// <inheritdoc />
        public sealed override string Name => this.MethodInfo.Name;

        /// <inheritdoc />
        public override string DisplayName => this.Name;

        /// <inheritdoc />
        public override void ValidateAndDrawField()
        {
            if (!this.CanDraw())
            {
                return;
            }

            var isPropertyEnabled = this.IsEnabled();

            GUI.enabled = isPropertyEnabled;

            var drawerAttributes = this.GetCustomAttributes<MethodAttribute>().ToArray();
            if (drawerAttributes.Length > 0)
            {
                var attribute = drawerAttributes[0];
                var drawer = MethodDrawerDatabase.GetDrawerForAttribute(attribute.GetType());
                drawer?.DrawMethod(this, attribute);
            }

            GUI.enabled = true;
        }

        /// <inheritdoc />
        public override IEnumerable<T> GetCustomAttributes<T>()
        {
            return this.MethodInfo.GetCustomAttributes<T>(true);
        }
    }
}