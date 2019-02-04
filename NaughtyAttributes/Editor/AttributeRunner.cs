// <copyright file="Validator.cs" company="Timothy Raines">
//     Copyright (c) Timothy Raines. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;

    public abstract class AttributeRunner
    {
        public abstract void Run(AttributeWrapper wrapper, NaughtyAttribute attribute);
    }

    public abstract class ValueRunner
    {
        public abstract void Run(ValueWrapper wrapper, NaughtyAttribute attribute);
    }
}