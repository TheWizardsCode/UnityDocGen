
using System;
using UnityEditor;

namespace WizardsCode.Tools.DocGen
{
    public static class EditorExtensions
    {
        public static void DrawDocGenAttributes(this Editor editor)
        {
            Attribute[] attributes = Attribute.GetCustomAttributes(editor.target.GetType(), typeof(Attribute));

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
        }
    }
}
