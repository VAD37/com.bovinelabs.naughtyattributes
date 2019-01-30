namespace BovineLabs.NaughtyAttributes.Editor
{
    using System.Reflection;

    public abstract class FieldDrawer
    {
        public abstract void DrawField(UnityEngine.Object target, FieldInfo field);
    }
}
