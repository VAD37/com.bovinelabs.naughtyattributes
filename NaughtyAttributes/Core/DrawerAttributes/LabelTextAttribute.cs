using System;

namespace NaughtyAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class LabelTextAttribute : DrawerAttribute
    {
        public string Text { get; private set; }

        public LabelTextAttribute(string text)
        {
            this.Text = text;
        }
    }
}
