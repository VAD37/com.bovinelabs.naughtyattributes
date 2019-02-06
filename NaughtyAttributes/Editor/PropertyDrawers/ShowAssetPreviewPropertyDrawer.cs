namespace BovineLabs.NaughtyAttributes.Editor.PropertyDrawers
{
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.Attributes;
    using BovineLabs.NaughtyAttributes.Editor.Utility;
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
    using UnityEditor;
    using UnityEngine;

    [PropertyDrawer(typeof(ShowAssetPreviewAttribute))]
    public class ShowAssetPreviewPropertyDrawer : PropertyDrawer<ShowAssetPreviewAttribute>
    {
        /// <inheritdoc />
        protected override void DrawProperty(NonSerializedAttributeWrapper wrapper, ShowAssetPreviewAttribute attribute)
        {
            if (!typeof(Object).IsAssignableFrom(wrapper.Type))
            {
                NotObject(wrapper);
                return;
            }

            wrapper.DrawDefaultField();

            DrawPreview((Object)wrapper.GetValue(), attribute);
        }

        /// <inheritdoc />
        protected override void DrawProperty(SerializedPropertyAttributeWrapper wrapper, ShowAssetPreviewAttribute attribute)
        {
            var property = wrapper.Property;

            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                NotObject(wrapper);
                return;
            }

            wrapper.DrawDefaultField();

            DrawPreview(property.objectReferenceValue, attribute);
        }

        private static void DrawPreview(Object value, ShowAssetPreviewAttribute attribute)
        {
            if (value == null)
            {
                return;
            }

            Texture2D previewTexture = AssetPreview.GetAssetPreview(value);
            if (previewTexture != null)
            {
                int width = Mathf.Clamp(attribute.Width, 0, previewTexture.width);
                int height = Mathf.Clamp(attribute.Height, 0, previewTexture.height);
                GUILayout.Label(previewTexture, GUILayout.MaxWidth(width), GUILayout.MaxHeight(height));
            }
            else
            {
                GUILayout.Label(string.Empty, GUILayout.MaxWidth(attribute.Width), GUILayout.MaxHeight(attribute.Height));
            }
        }

        private static void NotObject(ValueWrapper wrapper)
        {
            string warning = typeof(ShowAssetPreviewAttribute).Name + " can only be used on Object fields";
            EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning);
            wrapper.DrawDefaultField();
        }
    }
}
