namespace BovineLabs.NaughtyAttributes
{
    using System;

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ProgressBarAttribute : DrawerAttribute
    {
        public string Name { get; private set; }
        public float MaxValue { get; private set; }
        public ProgressBarColor Color { get; private set; }

        public ProgressBarAttribute(string name = "", float maxValue = 100, ProgressBarColor color = ProgressBarColor.Blue)
        {
            this.Name = name;
            this.MaxValue = maxValue;
            this.Color = color;
        }
    }

    public enum ProgressBarColor
    {
        Red,
        Pink,
        Orange,
        Yellow,
        Green,
        Blue,
        Indigo,
        Violet,
        White
    }
}