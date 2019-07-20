using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using WizardsCode.Tools.Editor.Linter;

namespace WizardsCode.Tools.Editor
{
    public class UnityLinterWindow : EditorWindow
    {
        MonoBehaviour baseType;
        bool includeMonoBehaviours = true;
        bool includeScriptableObjects = true;
        
        [MenuItem("Window/Wizards Code/Linter")]
        static void Init()
        {
            UnityLinterWindow window = (UnityLinterWindow)EditorWindow.GetWindow(typeof(UnityLinterWindow));
            window.Show();
        }

        void OnGUI()
        {
            GUILayout.Label("Base Settings", EditorStyles.boldLabel);

            baseType = EditorGUILayout.ObjectField("Object containing a MonoBehaviour in your project", baseType, typeof(MonoBehaviour)) as MonoBehaviour;
            includeScriptableObjects = EditorGUILayout.Toggle("Include MonoBehaviours", includeMonoBehaviours);
            includeScriptableObjects = EditorGUILayout.Toggle("Include Scriptable Objects", includeScriptableObjects);

            if(GUILayout.Button("Analyze"))
            {
                Analyze(baseType.GetType().Assembly);
            }
        }

        /// <summary>
        /// Perform a complete analysis of an Assembly and generate a report of any linting failures.
        /// </summary>
        private void Analyze(Assembly assembly)
        {
            if (includeMonoBehaviours)
            {
                IEnumerable<Type> monoBehaviours = assembly.GetTypes().Where(t => typeof(MonoBehaviour).IsAssignableFrom(t));
                foreach (Type t in monoBehaviours)
                {
                    ReportAllFieldsWithoutTooltip(t);
                }
            }

            if (includeScriptableObjects)
            {
                IEnumerable<Type> monoBehaviours = assembly.GetTypes().Where(t => typeof(ScriptableObject).IsAssignableFrom(t));
                foreach (Type t in monoBehaviours)
                {
                    ReportAllFieldsWithoutTooltip(t);
                }
            }
        }

        /// <summary>
        /// Searches for public and serialized fields that do not have a tooltip and reports on them.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private void ReportAllFieldsWithoutTooltip(Type type)
        {
            IEnumerable<FieldInfo> publicFields = type.GetFields()
                .Where(prop => !Attribute.IsDefined(prop, typeof(TooltipAttribute)));

            foreach (FieldInfo info in publicFields)
            {
                ReportField(new LintError(info.ReflectedType, info.Name + " does not have tooltip"));
            }

            IEnumerable<FieldInfo> privateFields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(prop => Attribute.IsDefined(prop, typeof(SerializeField))
                && !Attribute.IsDefined(prop, typeof(TooltipAttribute)));

            foreach (FieldInfo info in privateFields)
            {
                ReportField(new LintError(info.ReflectedType, info.Name + " does not have tooltip"));
            }
        }


        private static void ReportField(LintError error)
        {
            Debug.LogWarning(error);
        }
    }

    /// <summary>
    /// The details of a Linting error.
    /// </summary>
    internal class LintError
    {
        internal Type type;
        internal String message;

        /// <summary>
        /// Create a new LintError.
        /// </summary>
        /// <param name="t">The Type definition this error appears in.</param>
        /// <param name="msg">A human readable description of the error.</param>
        internal LintError(Type t, String msg)
        {
            message = msg;
            type = t;
        }

        public override string ToString()
        {
            return type.Name + ": " + message;
        }
    }
}
