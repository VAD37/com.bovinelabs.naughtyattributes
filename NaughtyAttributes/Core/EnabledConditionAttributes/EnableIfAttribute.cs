namespace BovineLabs.NaughtyAttributes
{
    using System;

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class EnableIfAttribute : EnabledConditionAttribute
    {
        public string ConditionName { get; private set; }

        public EnableIfAttribute(string conditionName)
        {
            this.ConditionName = conditionName;
        }
    }
}
