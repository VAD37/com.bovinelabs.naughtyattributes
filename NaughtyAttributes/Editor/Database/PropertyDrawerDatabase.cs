// This class is auto generated

namespace BovineLabs.NaughtyAttributes.Editor.Database
{
    using System;
    using System.Collections.Generic;
    using BovineLabs.NaughtyAttributes;
    using BovineLabs.NaughtyAttributes.Editor.PropertyDrawers;

    public static class PropertyDrawerDatabase
    {
        private static readonly Dictionary<Type, PropertyDrawer> drawersByAttributeType;

        static PropertyDrawerDatabase()
        {
            drawersByAttributeType = new Dictionary<Type, PropertyDrawer>
            {
                [typeof(DropdownAttribute)] = new DropdownPropertyDrawer(),
                [typeof(MinMaxSliderAttribute)] = new MinMaxSliderPropertyDrawer(),
                [typeof(ProgressBarAttribute)] = new ProgressBarPropertyDrawer(),
                [typeof(ReorderableListAttribute)] = new ReorderableListPropertyDrawer(),
                [typeof(ResizableTextAreaAttribute)] = new ResizableTextAreaPropertyDrawer(),
                [typeof(ShowAssetPreviewAttribute)] = new ShowAssetPreviewPropertyDrawer(),
                [typeof(SliderAttribute)] = new SliderPropertyDrawer()
            };
        }

        public static PropertyDrawer GetDrawerForAttribute(Type attributeType)
        {
            return drawersByAttributeType.TryGetValue(attributeType, out var drawer) ? drawer : null;
        }

        public static void ClearCache()
        {
            foreach (var kvp in drawersByAttributeType)
            {
                kvp.Value.ClearCache();
            }
        }
    }
}

