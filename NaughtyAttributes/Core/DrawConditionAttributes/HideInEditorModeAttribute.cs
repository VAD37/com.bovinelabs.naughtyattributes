using System;

namespace NaughtyAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class HideInEditorModeAttribute : DrawConditionAttribute
    {
    }
}
