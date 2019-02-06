// <copyright file="Drawer.cs" company="Timothy Raines">
//     Copyright (c) Timothy Raines. All rights reserved.
// </copyright>

namespace BovineLabs.NaughtyAttributes.Editor.Editors
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using BovineLabs.NaughtyAttributes.Editor.Database;
    using BovineLabs.NaughtyAttributes.Editor.PropertyGroupers;
    using BovineLabs.NaughtyAttributes.Editor.Utility;
    using BovineLabs.NaughtyAttributes.Editor.Wrappers;
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

        public Drawer(SerializedObject serializedObject, object target)
        {
            // todo better infinite loop support
            var type = target.GetType();
            var fields = ReflectionUtility.GetAllFields(target).Where(f => f.GetType() != type);

            foreach (var field in fields)
            {
                var property = serializedObject.FindProperty(field.Name);

                if (property != null)
                {
                    this.members.Add(new SerializedPropertyAttributeWrapper(serializedObject, target, property, field));
                }

                /*else if (field.GetCustomAttribute<ShowNonSerializedFieldAttribute>() != null)
                {
                    this.members.Add(new FieldAttributeWrapper(target, field));
                }*/
            }

            this.MethodsPropertiesAndGrouping(target);
        }

        public Drawer(SerializedObject serializedObject, object target, SerializedProperty serializedProperty)
        {
            // todo infinite loop support
            var type = target.GetType();
            //var fields = ReflectionUtility.GetAllFields(target).Where(f => f.GetType() != type);

            foreach (var property in serializedProperty.GetChildren())
            {
                this.members.Add(new SerializedPropertyAttributeWrapper(serializedObject, target, property, property.GetField()));
            }

            /*foreach (var field in fields)
            {


                var property = serializedObject.FindProperty(field.Name);

                if (property != null)
                {
                    this.members.Add(new SerializedPropertyAttributeWrapper(property));
                }

                /*else if (field.GetCustomAttribute<ShowNonSerializedFieldAttribute>() != null)
                {
                    this.members.Add(new FieldAttributeWrapper(target, field));
                }*/
            //}

            this.MethodsPropertiesAndGrouping(target);
        }

        /*public Drawer(object target)
        {
            var type = target.GetType();
            var publicFields = ReflectionUtility.GetAllFieldsPublic(target).Where(f => f.GetType() != type);

            foreach (var field in publicFields)
            {
                this.members.Add(new FieldAttributeWrapper(target, field));
            }

            var privateFields = ReflectionUtility.GetAllFieldsPrivate(target).Where(f => f.GetType() != type);

            foreach (var field in privateFields)
            {
                if (field.GetCustomAttribute<SerializeField>() != null || field.GetCustomAttribute<ShowNonSerializedFieldAttribute>() != null)
                {
                    this.members.Add(new FieldAttributeWrapper(target, field));
                }
            }

            this.MethodsPropertiesAndGrouping(target);
        }*/

        public bool HasElement => this.members.Count > 0;

        private void MethodsPropertiesAndGrouping(object target)
        {
            /*this.members.AddRange(ReflectionUtility.GetAllProperties(target)
                .Where(p => p.GetCustomAttribute<ShowNonSerializedFieldAttribute>() != null)
                .Select(p => new PropertyAttributeWrapper(target, p)));*/

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