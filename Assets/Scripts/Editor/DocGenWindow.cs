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
        bool generateTooltipToDoitems = true;
        string typeFilter = "";
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
            generateTooltipToDoitems = EditorGUILayout.Toggle("Generate Todo for missing Tooltips", generateTooltipToDoitems);
            outputDirectory = EditorGUILayout.TextField("Output Directory", outputDirectory);
            typeFilter = EditorGUILayout.TextField("Regex to filter types", typeFilter);

            if (GUILayout.Button("Generate"))
            {
                DocumentationGenerator generator = new DocumentationGenerator(includeMonoBehaviours, includeScriptableObjects, typeFilter, outputDirectory);
                generator.Generate(baseType.GetType().Assembly);
            }
        }

    }
}
