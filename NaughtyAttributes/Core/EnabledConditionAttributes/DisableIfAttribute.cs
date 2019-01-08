using System;

namespace NaughtyAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class DisableIfAttribute : EnabledConditionAttribute
    {
        public string ConditionName { get; private set; }

        public DisableIfAttribute(string conditionName)
        {
            this.ConditionName = conditionName;
        }
    }
}
