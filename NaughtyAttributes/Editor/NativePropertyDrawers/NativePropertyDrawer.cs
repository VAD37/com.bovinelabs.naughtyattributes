namespace BovineLabs.NaughtyAttributes.Editor
{
    using System.Reflection;

    public abstract class NativePropertyDrawer
    {
        public abstract void DrawNativeProperty(UnityEngine.Object target, PropertyInfo property);
    }
}
