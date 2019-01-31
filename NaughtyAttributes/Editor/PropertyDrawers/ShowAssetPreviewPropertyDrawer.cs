namespace BovineLabs.NaughtyAttributes.Editor
{
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;
    using UnityEngine;

    [PropertyDrawer(typeof(ShowAssetPreviewAttribute))]
    public class ShowAssetPreviewPropertyDrawer : PropertyDrawer<ShowAssetPreviewAttribute>
    {
        protected override void DrawProperty(ValueWrapper wrapper, ShowAssetPreviewAttribute attribute)
        {
            wrapper.DrawPropertyField();

            if (typeof(Object).IsAssignableFrom(wrapper.Type))
            {
                var value = (Object)wrapper.GetValue();

                if (value != null)
                {
                    Texture2D previewTexture = AssetPreview.GetAssetPreview(value);
                    if (previewTexture != null)
                    {
                        int width = Mathf.Clamp(attribute.Width, 0, previewTexture.width);
                        int height = Mathf.Clamp(attribute.Height, 0, previewTexture.height);
                        GUILayout.Label(previewTexture, GUILayout.MaxWidth(width), GUILayout.MaxHeight(height));
                    }
                }
            }
            else
            {
                string warning = wrapper.Name + " doesn't have an asset preview";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true, wrapper.Target);
            }
        }
    }
}
