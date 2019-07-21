using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace WizardsCode.Tools.DocGen
{
    public class DocumentationGenerator
    {
        bool includeMonoBehaviours = true;
        bool includeScriptableObjects = true;
        string typeFilterRegex = "";
        string outputDirectory = "";

        Dictionary<Type, List<FieldRecord>> fields = new Dictionary<Type, List<FieldRecord>>();

        public DocumentationGenerator(bool includeMonoBehaviours, bool includeScriptableObjects, string typeFilterRegex, string outputDirectory)
        {
            this.includeMonoBehaviours = includeMonoBehaviours;
            this.includeScriptableObjects = includeScriptableObjects;
            this.typeFilterRegex = typeFilterRegex;
            this.outputDirectory = outputDirectory;
        }

        /// <summary>
        /// Generates documentation for an Assembly.
        /// </summary>
        public void Generate(Assembly assembly)
        {

            if (includeMonoBehaviours)
            {
                Generate(typeof(MonoBehaviour), assembly, typeFilterRegex, outputDirectory);
            }

            if (includeScriptableObjects)
            {
                Generate(typeof(ScriptableObject), assembly, typeFilterRegex, outputDirectory);
            }
        }

        /// <summary>
        /// Generates documentation for all classes that implement or extend a type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="assembly"></param>
        private void Generate(Type type, Assembly assembly, string typeFilterRegex, string outputDirectory)
        {
            var regex = new Regex(typeFilterRegex, RegexOptions.IgnoreCase);
            fields.Clear();

            IEnumerable<Type> types = assembly.GetTypes().Where(t => type.IsAssignableFrom(t));
            foreach (Type t in types)
            {
                if (regex.IsMatch(t.Name))
                {
                    ExtractAllEditorAccessibleFields(t);
                }
            }

            Directory.CreateDirectory(outputDirectory);
            foreach (KeyValuePair<Type, List<FieldRecord>> entries in fields)
            {
                string filename = entries.Key + ".md";
                string path = outputDirectory + "/" + filename;

                StreamWriter writer = new StreamWriter(path);
                writer.Write("# " + entries.Key + "\n\n");

                foreach (FieldRecord entry in entries.Value)
                {
                    writer.WriteLine(entry.ToMarkdown());
                }
                writer.Close();

                AssetDatabase.ImportAsset(path);
            }
        }

        /// <summary>
        /// Searches for public and serialized fields that do not have a tooltip and reports on them.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private void ExtractAllEditorAccessibleFields(Type type)
        {

            GameObject defaultsGo = new GameObject();

            IEnumerable<FieldInfo> publicFields = type.GetFields();

            foreach (FieldInfo info in publicFields)
            {
                ReportField(new FieldRecord(info, defaultsGo));
            }

            GameObject.DestroyImmediate(defaultsGo);

            IEnumerable<FieldInfo> privateFields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(prop => Attribute.IsDefined(prop, typeof(SerializeField)));

            foreach (FieldInfo info in privateFields)
            {
                ReportField(new FieldRecord(info, defaultsGo));
            }
        }

        private void ReportField(FieldRecord field)
        {
            Type key = field.info.ReflectedType;
            List<FieldRecord> entries;
            if (fields.TryGetValue(key, out entries))
            {
                entries.Add(field);
                fields[key] = entries;
            }
            else
            {
                entries = new List<FieldRecord>();
                entries.Add(field);
                fields.Add(key, entries);
            }

            if (field.tooltip == null)
            {
                Debug.LogWarning(field.info.Name + " in " + field.info.ReflectedType + " needs a ToolTip.");
            }
        }

        /// <summary>
        /// The details of field for documentation.
        /// </summary>
        internal class FieldRecord
        {
            internal FieldInfo info;
            internal TooltipAttribute tooltip;
            internal RangeAttribute range;
            internal object defaultValue;

            internal FieldRecord(FieldInfo info, GameObject defaultsGo)
            {
                this.info = info;

                if (info.ReflectedType == typeof(MonoBehaviour))
                {
                    UnityEngine.Object obj = defaultsGo.AddComponent(info.ReflectedType);
                    this.defaultValue = info.GetValue(obj);
                }
                else
                {
                    ScriptableObject instance = ScriptableObject.CreateInstance(info.ReflectedType);
                    this.defaultValue = info.GetValue(instance);
                    ScriptableObject.DestroyImmediate(instance);
                }

                object[] attributes = info.GetCustomAttributes(true);
                foreach (object attr in attributes)
                {

                    if (attr is SerializeField)
                    {
                        // we don't need to record that it is serialized as we are only processing serialized fields.
                        continue;
                    }

                    if (attr is TooltipAttribute)
                    {
                        tooltip = attr as TooltipAttribute;
                        continue;
                    }

                    if (attr is RangeAttribute)
                    {
                        range = attr as RangeAttribute;
                        continue;
                    }

                    Debug.LogWarning("Unable to document attribute type " + attr.GetType());
                }
            }

            public string ExpandedName
            {
                get
                {
                    string name = Regex.Replace(info.Name, "([A-Z])", " $1").Trim();
                    return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(name);
                }
            }

            public string ToMarkdown()
            {
                string doc = "\n## " + ExpandedName;

                doc += " (" + info.FieldType.Name + ")";

                if (tooltip != null)
                {
                    doc += "\n\n" + tooltip.tooltip;
                }
                else
                {
                    doc += "\n\n" + "No tooltip provided.";
                }

                if (defaultValue != null)
                {
                    doc += "\n\n";
                    if (info.FieldType == typeof(string))
                    {
                        doc += "Default Value     : \"" + defaultValue + "\"";
                    }
                    else
                    {
                        doc += "Default Value     : " + defaultValue;
                    }
                }

                if (range != null)
                {
                    doc += "\n";
                    doc += "Range             : " + range.min + " and " + range.max;
                }

                doc += "\n";

                return doc;
            }
        }
    }
}

