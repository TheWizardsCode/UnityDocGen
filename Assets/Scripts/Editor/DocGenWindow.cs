using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WizardsCode.Tools.DocGen
{
    public class DocGenWindow : EditorWindow
    {
        MonoBehaviour baseType;
        bool includeMonoBehaviours = true;
        bool includeScriptableObjects = true;
        bool generateTooltipToDoItems = true;
        string includeRegex = "";
        string excludeRegex = ".*DocGen.*";
        string outputDirectory = "Assets/Documentation/Generated";

        [MenuItem("Window/Wizards Code/Documentation Generator")]
        static void Init()
        {
            DocGenWindow window = (DocGenWindow)EditorWindow.GetWindow<DocGenWindow>("DocGen");
            window.Show();
        }

        void OnGUI()
        {
            GUILayout.Label("Base Settings", EditorStyles.boldLabel);

            baseType = EditorGUILayout.ObjectField("Object containing a MonoBehaviour in your project", baseType, typeof(MonoBehaviour)) as MonoBehaviour;
            includeMonoBehaviours = EditorGUILayout.Toggle("Include MonoBehaviours", includeMonoBehaviours);
            includeScriptableObjects = EditorGUILayout.Toggle("Include Scriptable Objects", includeScriptableObjects);
            generateTooltipToDoItems = EditorGUILayout.Toggle("Generate Todo for missing Tooltips", generateTooltipToDoItems);
            outputDirectory = EditorGUILayout.TextField("Output Directory", outputDirectory);
            includeRegex = EditorGUILayout.TextField("Include filter (Regex)", includeRegex);
            excludeRegex = EditorGUILayout.TextField("Exclude filter (Regex)", excludeRegex);

            if (GUILayout.Button("Generate"))
            {
                DocumentationGenerator generator = new DocumentationGenerator(includeMonoBehaviours, includeScriptableObjects, generateTooltipToDoItems, includeRegex, excludeRegex, outputDirectory);
                generator.Generate(baseType.GetType().Assembly);
            }
        }

    }
}
