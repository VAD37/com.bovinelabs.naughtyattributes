namespace NaughtyAttributes
{
    using JetBrains.Annotations;
    using System;

    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : DrawerAttribute
    {
        public string Text { get; private set; }

        public ButtonAttribute(string text = null)
        {
            this.Text = text;
        }
    }
}
