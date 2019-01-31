namespace BovineLabs.NaughtyAttributes
{
    using System;
    using JetBrains.Annotations;

    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : MethodAttribute
    {
        public string Text { get; private set; }

        public ButtonAttribute(string text = null)
        {
            this.Text = text;
        }
    }
}
