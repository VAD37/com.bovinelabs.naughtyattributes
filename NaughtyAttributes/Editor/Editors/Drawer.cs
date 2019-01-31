// <copyright file="Drawer.cs" company="Timothy Raines">
//     Copyright (c) Timothy Raines. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// The Drawer.
    /// </summary>
    public class Drawer
    {
        private readonly List<AttributeWrapper> members = new List<AttributeWrapper>();
        private readonly HashSet<AttributeWrapper> group = new HashSet<AttributeWrapper>();
        private readonly Dictionary<string, List<AttributeWrapper>> groupedByName = new Dictionary<string, List<AttributeWrapper>>();

        public Drawer(Object target, SerializedObject serializedObject)
        {
            foreach (var field in ReflectionUtility.GetAllFields(target))
            {
                var serializedProperty = serializedObject.FindProperty(field.Name);

                if (serializedProperty != null)
                {
                    this.members.Add(new FieldAttributeWrapper(target, field));
                }
                else if (field.GetCustomAttribute<ShowNonSerializedFieldAttribute>() != null)
                {
                    this.members.Add(new FieldAttributeWrapper(target, field));
                }
            }
        }

        public bool HasElement => this.members.Count > 0;

        public Drawer(object target)
        {
            var publicFields = ReflectionUtility.GetAllFieldsPublic(target).ToArray();

            foreach (var field in publicFields)
            {
                this.members.Add(new FieldAttributeWrapper(target, field));
            }

            foreach (var field in ReflectionUtility.GetAllFieldsPrivate(target))
            {
                if (field.GetCustomAttribute<SerializeField>() != null || field.GetCustomAttribute<ShowNonSerializedFieldAttribute>() != null)
                {
                    this.members.Add(new FieldAttributeWrapper(target, field));
                }
            }
            
            this.MethodsPropertiesAndGrouping(target);
        }

        private void MethodsPropertiesAndGrouping(object target)
        {
            this.members.AddRange(ReflectionUtility.GetAllProperties(target)
                .Where(p => p.GetCustomAttribute<ShowNonSerializedFieldAttribute>() != null)
                .Select(p => new PropertyAttributeWrapper(target, p)));

            this.members.AddRange(ReflectionUtility
                .GetAllMethods(target, p => p.GetCustomAttribute<MethodAttribute>() != null)
                .Select(p => new MethodWrapper(target, p)));


            this.@group.UnionWith(this.members.Where(f => f.GetCustomAttributes<GroupAttribute>().Any()));

            foreach (var element in this.@group)
            {
                var groupName = element.GetCustomAttributes<GroupAttribute>().First().Name;

                if (!this.groupedByName.TryGetValue(groupName, out var list))
                {
                    list = this.groupedByName[groupName] = new List<AttributeWrapper>();
                }

                list.Add(element);
            }
        }

        public void OnInspectorGUI()
        {
            var drawnGroups = new HashSet<string>();
            foreach (var member in this.members)
            {
                if (this.group.Contains(member))
                {
                    // Draw grouped fields
                    var attribute = member.GetCustomAttributes<GroupAttribute>().First();

                    string groupName = attribute.Name;
                    if (!drawnGroups.Contains(groupName))
                    {
                        drawnGroups.Add(groupName);

                        PropertyGrouper grouper = GetPropertyGrouperForField(member);
                        if (grouper != null)
                        {
                            grouper.BeginGroup(attribute);

                            this.ValidateAndDrawFields(this.groupedByName[groupName]);

                            grouper.EndGroup();
                        }
                        else
                        {
                            this.ValidateAndDrawFields(this.groupedByName[groupName]);
                        }
                    }
                }
                else
                {
                    // Draw non-grouped field
                    member.ValidateAndDrawField();
                }
            }
        }

        private static PropertyGrouper GetPropertyGrouperForField(AttributeWrapper wrapper)
        {
            var groupAttribute = wrapper.GetCustomAttributes<GroupAttribute>().FirstOrDefault();

            if (groupAttribute == null)
            {
                return null;
            }

            return PropertyGrouperDatabase.GetGrouperForAttribute(groupAttribute.GetType());
        }

        private void ValidateAndDrawFields(IEnumerable<AttributeWrapper> wrappers)
        {
            foreach (var wrapper in wrappers)
            {
                wrapper.ValidateAndDrawField();
            }
        }
    }
}