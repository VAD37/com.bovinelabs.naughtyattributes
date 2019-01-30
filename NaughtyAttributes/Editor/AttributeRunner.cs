// <copyright file="Validator.cs" company="Timothy Raines">
//     Copyright (c) Timothy Raines. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor
{
    public abstract class AttributeRunner
    {
        public abstract void Run(AttributeWrapper wrapper, NaughtyAttribute attribute);
    }
}