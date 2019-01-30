namespace BovineLabs.NaughtyAttributes.Editor
{
    using System.Reflection;

    public abstract class MethodDrawer
    {
        public abstract void DrawMethod(UnityEngine.Object target, MethodInfo methodInfo);
    }
}
