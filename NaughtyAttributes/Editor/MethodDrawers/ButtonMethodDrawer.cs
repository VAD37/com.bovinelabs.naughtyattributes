namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;
    using UnityEngine;

    [MethodDrawer(typeof(ButtonAttribute))]
    public class ButtonMethodDrawer : MethodDrawer<ButtonAttribute>
    {
        protected override void DrawMethod(MethodWrapper wrapper, ButtonAttribute attribute)
        {
            if (wrapper.MethodInfo.GetParameters().Length == 0)
            {
                string buttonText = string.IsNullOrEmpty(attribute.Text) ? wrapper.Name : attribute.Text;

                if (GUILayout.Button(buttonText))
                {
                    wrapper.MethodInfo.Invoke(wrapper.Target, null);
                }
            }
            else
            {
                string warning = typeof(ButtonAttribute).Name + " works only on methods with no parameters";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, wrapper);
            }
        }
    }
}
