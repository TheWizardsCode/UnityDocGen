using System;
using UnityEditor;
using UnityEngine;

namespace WizardsCode.Tools.DocGen
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class DocGenAttributeEditor : Editor
    {
        Attribute[] attributes;

        private void OnEnable()
        {
            attributes = Attribute.GetCustomAttributes(this.target.GetType(), typeof(Attribute));
        }

        public override void OnInspectorGUI()
        {

            //.Cast<CustomAttribute>().ToArray()
            for (int i = 0; i < attributes.Length; i++)
            {
                switch (attributes[i].GetType().Name)
                {
                    case "DocGenAttribute":
                        DocGenAttribute docgen = (DocGenAttribute)attributes[i];
                        EditorGUILayout.HelpBox(docgen.Description, MessageType.Info);
                        break;
                    default:
                        break;
                }
                
            }
            this.DrawDefaultInspector();
        }
    }
}
