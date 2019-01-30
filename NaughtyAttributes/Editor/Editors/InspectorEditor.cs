namespace BovineLabs.NaughtyAttributes.Editor
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using BovineLabs.NaughtyAttributes;
    using UnityEditor;
    using UnityEngine;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class InspectorEditor : UnityEditor.Editor
    {
        private SerializedProperty script;

        /*private FieldInfo[] fields;
        private HashSet<FieldInfo> groupedFields;
        private Dictionary<string, List<FieldInfo>> groupedFieldsByGroupName;
        private FieldInfo[] nonSerializedFields;
        private PropertyInfo[] nativeProperties;
        private MethodInfo[] methods;

        private Dictionary<string, SerializedProperty> serializedPropertiesByFieldName;*/

        private bool useDefaultInspector;
        private readonly List<AttributeWrapper> members = new List<AttributeWrapper>();
        private readonly HashSet<AttributeWrapper> group = new HashSet<AttributeWrapper>();
        private readonly Dictionary<string, List<AttributeWrapper>> groupedByName = new Dictionary<string, List<AttributeWrapper>>();

        public override void OnInspectorGUI()
        {
            if (this.useDefaultInspector)
            {
                this.DrawDefaultInspector();
                return;
            }

            this.serializedObject.Update();

            if (this.script != null)
            {
                GUI.enabled = false;
                EditorGUILayout.PropertyField(this.script);
                GUI.enabled = true;
            }

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
                    //this.ValidateAndDrawField(field);
                }
            }

            // Draw fields
           /* HashSet<string> drawnGroups = new HashSet<string>();
            foreach (var field in this.fields)
            {
                if (this.groupedFields.Contains(field))
                {
                    // Draw grouped fields
                    var attribute = (GroupAttribute)field.GetCustomAttributes(typeof(GroupAttribute), true)[0];

                    string groupName = attribute.Name;
                    if (!drawnGroups.Contains(groupName))
                    {
                        drawnGroups.Add(groupName);

                        PropertyGrouper grouper = this.GetPropertyGrouperForField(field);
                        if (grouper != null)
                        {
                            grouper.BeginGroup(attribute);

                            this.ValidateAndDrawFields(this.groupedFieldsByGroupName[groupName]);

                            grouper.EndGroup();
                        }
                        else
                        {
                            this.ValidateAndDrawFields(this.groupedFieldsByGroupName[groupName]);
                        }
                    }
                }
                else
                {
                    // Draw non-grouped field
                    this.ValidateAndDrawField(field);
                }
            }

            this.serializedObject.ApplyModifiedProperties();*/

            /*if (this.useDefaultInspector)
            {
                this.DrawDefaultInspector();
            }
            else
            {
                this.serializedObject.Update();

                if (this.script != null)
                {
                    GUI.enabled = false;
                    EditorGUILayout.PropertyField(this.script);
                    GUI.enabled = true;
                }

                // Draw fields
                HashSet<string> drawnGroups = new HashSet<string>();
                foreach (var field in this.fields)
                {
                    if (this.groupedFields.Contains(field))
                    {
                        // Draw grouped fields
                        var attribute = (GroupAttribute)field.GetCustomAttributes(typeof(GroupAttribute), true)[0];

                        string groupName = attribute.Name;
                        if (!drawnGroups.Contains(groupName))
                        {
                            drawnGroups.Add(groupName);

                            PropertyGrouper grouper = this.GetPropertyGrouperForField(field);
                            if (grouper != null)
                            {
                                grouper.BeginGroup(attribute);

                                this.ValidateAndDrawFields(this.groupedFieldsByGroupName[groupName]);

                                grouper.EndGroup();
                            }
                            else
                            {
                                this.ValidateAndDrawFields(this.groupedFieldsByGroupName[groupName]);
                            }
                        }
                    }
                    else
                    {
                        // Draw non-grouped field
                        this.ValidateAndDrawField(field);
                    }
                }

                this.serializedObject.ApplyModifiedProperties();
            }

            // Draw non-serialized fields
            foreach (var field in this.nonSerializedFields)
            {
                DrawerAttribute drawerAttribute =
                    (DrawerAttribute)field.GetCustomAttributes(typeof(DrawerAttribute), true)[0];
                FieldDrawer drawer = FieldDrawerDatabase.GetDrawerForAttribute(drawerAttribute.GetType());
                if (drawer != null)
                {
                    drawer.DrawField(this.target, field);
                }
            }

            // Draw native properties
            foreach (var property in this.nativeProperties)
            {
                DrawerAttribute drawerAttribute = (DrawerAttribute)property.GetCustomAttributes(typeof(DrawerAttribute), true)[0];
                NativePropertyDrawer drawer = NativePropertyDrawerDatabase.GetDrawerForAttribute(drawerAttribute.GetType());
                if (drawer != null)
                {
                    drawer.DrawNativeProperty(this.target, property);
                }
            }

            // Draw methods
            foreach (var method in this.methods)
            {
                PropertyEnabledCondition enabledCondition = this.GetPropertyEnabledConditionForMember(method);
                bool isPropertyEnabled = true;
                if (enabledCondition != null)
                {
                    isPropertyEnabled = enabledCondition.IsPropertyEnabled(this.serializedPropertiesByFieldName[method.Name]);
                }

                GUI.enabled = isPropertyEnabled;

                DrawerAttribute drawerAttribute = (DrawerAttribute)method.GetCustomAttributes(typeof(DrawerAttribute), true)[0];
                MethodDrawer methodDrawer = MethodDrawerDatabase.GetDrawerForAttribute(drawerAttribute.GetType());
                if (methodDrawer != null)
                {
                    methodDrawer.DrawMethod(this.target, method);
                }

                GUI.enabled = true;
            }*/
        }

        private void OnEnable()
        {
            this.members.Clear();

            this.script = this.serializedObject.FindProperty("m_Script");
            this.group.Clear();
            this.groupedByName.Clear();

            var fields = ReflectionUtility.GetAllFields(this.target);

            foreach (var field in fields)
            {
                var serializedProperty = this.serializedObject.FindProperty(field.Name);

                if (serializedProperty != null)
                {
                    this.members.Add(new SerializedFieldAttributeWrapper(serializedProperty, this.target, field));
                }
                else if (field.GetCustomAttribute<ShowNonSerializedFieldAttribute>() != null)
                {
                    this.members.Add(new NonSerializedFieldAttributeWrapper(this.target, field));
                }
            }

            this.members.AddRange(ReflectionUtility.GetAllProperties(this.target)
                .Where(p => p.GetCustomAttribute<ShowNonSerializedFieldAttribute>() != null)
                .Select(p => new PropertyAttributeWrapper(this.target, p)));

            if (this.members.Count == 0)
            {
                this.useDefaultInspector = true;
            }
            else
            {
                this.useDefaultInspector = false;

                this.group.UnionWith(this.members.Where(f => f.GetCustomAttributes<GroupAttribute>().Any()));

                foreach (var element in this.group)
                {
                    var groupName = element.GetCustomAttributes<GroupAttribute>().First().Name;

                    if (!this.groupedByName.TryGetValue(groupName, out var list))
                    {
                        list = this.groupedByName[groupName] = new List<AttributeWrapper>();
                    }

                    list.Add(element);
                }
            }

            // Cache serialized fields
            /*this.fields = ReflectionUtility.GetAllFields(this.target, f => this.serializedObject.FindProperty(f.Name) != null).ToArray();

            // Cache methods with DrawerAttribute
            this.methods = ReflectionUtility.GetAllMethods(
                this.target, m => m.GetCustomAttributes(typeof(DrawerAttribute), true).Length > 0).ToArray();

            // Cache non-serialized fields
            this.nonSerializedFields = ReflectionUtility.GetAllFields(
                this.target, f => f.GetCustomAttributes(typeof(DrawerAttribute), true).Length > 0 && this.serializedObject.FindProperty(f.Name) == null).ToArray();

            // Cache the native properties
            this.nativeProperties = ReflectionUtility.GetAllProperties(
                this.target, p => p.GetCustomAttributes(typeof(DrawerAttribute), true).Length > 0).ToArray();

            // Cache serialized properties by field name
            this.serializedPropertiesByFieldName = new Dictionary<string, SerializedProperty>();

            // If there are no NaughtyAttributes use default inspector
            if (this.fields.All(f => f.GetCustomAttributes(typeof(NaughtyAttribute), true).Length == 0) &&
                this.methods.Length == 0 &&
                this.nonSerializedFields.Length == 0 &&
                this.nativeProperties.Length == 0)
            {
                this.useDefaultInspector = true;
            }
            else
            {
                this.useDefaultInspector = false;

                // Cache grouped fields
                this.groupedFields = new HashSet<FieldInfo>(this.fields.Where(f =>
                    f.GetCustomAttributes(typeof(GroupAttribute), true).Length > 0));

                // Cache grouped fields by group name
                this.groupedFieldsByGroupName = new Dictionary<string, List<FieldInfo>>();
                foreach (var groupedField in this.groupedFields)
                {
                    string groupName = (groupedField.GetCustomAttributes(typeof(GroupAttribute), true)[0] as GroupAttribute).Name;

                    if (this.groupedFieldsByGroupName.ContainsKey(groupName))
                    {
                        this.groupedFieldsByGroupName[groupName].Add(groupedField);
                    }
                    else
                    {
                        this.groupedFieldsByGroupName[groupName] = new List<FieldInfo>
                        {
                            groupedField,
                        };
                    }
                }

                foreach (var field in this.fields)
                {
                    this.serializedPropertiesByFieldName[field.Name] = this.serializedObject.FindProperty(field.Name);
                }

                foreach (var method in this.methods)
                {
                    this.serializedPropertiesByFieldName[method.Name] = this.serializedObject.FindProperty(method.Name);
                }
            }*/
        }

        private void OnDisable()
        {
            PropertyDrawerDatabase.ClearCache();
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


        /*private void ValidateAndDrawField(AttributeWrapper wrapper)
        {
            ValidateField(wrapper);
            this.ApplyFieldMeta(wrapper);
            this.DrawField(wrapper);
        }

        private static void ValidateField(AttributeWrapper wrapper)
        {
            var validatorAttributes = wrapper.GetCustomAttributes<ValidatorAttribute>().ToArray();

            if (validatorAttributes.Length > 0 && wrapper.SerializedProperty == null)
            {
                string warning = wrapper.GetType().Name + " has a property validator but is not a serializable property";
                EditorDrawUtility.DrawHelpBox(warning, MessageType.Warning, true);
                return;
            }

            foreach (var attribute in validatorAttributes)
            {
                PropertyValidator validator = PropertyValidatorDatabase.GetValidatorForAttribute(attribute.GetType());

                validator?.ValidateProperty(wrapper.SerializedProperty);
            }
        }

        private void ApplyFieldMeta(FieldInfo field)
        {
            // Apply custom meta attributes
            MetaAttribute[] metaAttributes = field
                .GetCustomAttributes(typeof(MetaAttribute), true)
                .Where(attr => attr.GetType() != typeof(OnValueChangedAttribute))
                .Select(obj => obj as MetaAttribute)
                .ToArray();

            Array.Sort(metaAttributes, (x, y) =>
            {
                return x.Order - y.Order;
            });

            foreach (var metaAttribute in metaAttributes)
            {
                PropertyMeta meta = PropertyMetaDatabase.GetMetaForAttribute(metaAttribute.GetType());
                if (meta != null)
                {
                    meta.ApplyPropertyMeta(this.serializedPropertiesByFieldName[field.Name], metaAttribute);
                }
            }
        }

        private void DrawField(FieldInfo field)
        {
            // Check if the field has draw conditions
            PropertyDrawCondition drawCondition = this.GetPropertyDrawConditionForField(field);
            if (drawCondition != null)
            {
                bool canDrawProperty = drawCondition.CanDrawProperty(this.serializedPropertiesByFieldName[field.Name]);
                if (!canDrawProperty)
                {
                    return;
                }
            }

            // Check if the field has HideInInspectorAttribute
            HideInInspector[] hideInInspectorAttributes = (HideInInspector[])field.GetCustomAttributes(typeof(HideInInspector), true);
            if (hideInInspectorAttributes.Length > 0)
            {
                return;
            }

            PropertyEnabledCondition enabledCondition = this.GetPropertyEnabledConditionForMember(field);
            bool isPropertyEnabled = true;
            if (enabledCondition != null)
            {
                isPropertyEnabled = enabledCondition.IsPropertyEnabled(this.serializedPropertiesByFieldName[field.Name]);
            }

            // Draw the field
            EditorGUI.BeginChangeCheck();
            GUI.enabled = isPropertyEnabled;
            PropertyDrawer drawer = this.GetPropertyDrawerForField(field);
            if (drawer != null)
            {
                drawer.DrawProperty(this.serializedPropertiesByFieldName[field.Name]);
            }
            else
            {
                EditorDrawUtility.DrawPropertyField(this.serializedPropertiesByFieldName[field.Name]);
            }

            GUI.enabled = true;

            if (EditorGUI.EndChangeCheck())
            {
                OnValueChangedAttribute[] onValueChangedAttributes = (OnValueChangedAttribute[])field.GetCustomAttributes(typeof(OnValueChangedAttribute), true);
                foreach (var onValueChangedAttribute in onValueChangedAttributes)
                {
                    PropertyMeta meta = PropertyMetaDatabase.GetMetaForAttribute(onValueChangedAttribute.GetType());
                    if (meta != null)
                    {
                        meta.ApplyPropertyMeta(this.serializedPropertiesByFieldName[field.Name], onValueChangedAttribute);
                    }
                }
            }
        }

        private PropertyDrawer GetPropertyDrawerForField(FieldInfo field)
        {
            DrawerAttribute[] drawerAttributes = (DrawerAttribute[])field.GetCustomAttributes(typeof(DrawerAttribute), true);
            if (drawerAttributes.Length > 0)
            {
                PropertyDrawer drawer = PropertyDrawerDatabase.GetDrawerForAttribute(drawerAttributes[0].GetType());
                return drawer;
            }
            else
            {
                return null;
            }
        }*/

        /*private static PropertyGrouper GetPropertyGrouperForField(AttributeWrapper wrapper)
        {
            var groupAttribute = wrapper.GetCustomAttributes<GroupAttribute>().FirstOrDefault();

            if (groupAttribute == null)
            {
                return null;
            }

            return PropertyGrouperDatabase.GetGrouperForAttribute(groupAttribute.GetType());
        }

        private PropertyDrawCondition GetPropertyDrawConditionForField(FieldInfo field)
        {
            DrawConditionAttribute[] drawConditionAttributes = (DrawConditionAttribute[])field.GetCustomAttributes(typeof(DrawConditionAttribute), true);
            if (drawConditionAttributes.Length > 0)
            {
                PropertyDrawCondition drawCondition = PropertyDrawConditionDatabase.GetDrawConditionForAttribute(drawConditionAttributes[0].GetType());
                return drawCondition;
            }
            else
            {
                return null;
            }
        }

        private PropertyEnabledCondition GetPropertyEnabledConditionForMember(MemberInfo memberInfo)
        {
            EnabledConditionAttribute[] drawConditionAttributes = (EnabledConditionAttribute[])memberInfo.GetCustomAttributes(typeof(EnabledConditionAttribute), true);
            if (drawConditionAttributes.Length > 0)
            {
                PropertyEnabledCondition drawCondition = PropertyEnabledConditionDatabase.GetEnabledConditionForAttribute(drawConditionAttributes[0].GetType());
                return drawCondition;
            }
            else
            {
                return null;
            }
        }*/
    }
}
