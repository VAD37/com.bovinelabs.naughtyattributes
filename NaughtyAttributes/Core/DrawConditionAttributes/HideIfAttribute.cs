namespace BovineLabs.NaughtyAttributes
{
    using System;

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class HideIfAttribute : DrawConditionAttribute
    {
        public string ConditionName { get; private set; }

        public HideIfAttribute(string conditionName)
        {
            this.ConditionName = conditionName;
        }
    }
}
