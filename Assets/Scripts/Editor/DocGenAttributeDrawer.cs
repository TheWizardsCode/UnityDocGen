using UnityEditor;
using UnityEngine;

namespace WizardsCode.Tools.DocGen
{
    [CustomPropertyDrawer(typeof(DocGenAttribute), true)]
    public class DocGenAttributeDrawer : PropertyDrawer
    {
        private float helpHeight = 30;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = base.GetPropertyHeight(property, label);
            if (property.isExpanded)
            {
                height += helpHeight;
            }
            return height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DocGenAttribute docgen = attribute as DocGenAttribute;
            Rect fieldRect = new Rect(position);

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(fieldRect, property, label, true);
            property.isExpanded = EditorGUI.Foldout(fieldRect, property.isExpanded, GUIContent.none, true);

            if (property.isExpanded)
            {
                Rect helpRect = fieldRect;
                helpRect.y = fieldRect.y + EditorGUIUtility.singleLineHeight;
                helpRect.height = helpHeight;
                
                GUIStyle helpStyle = GUI.skin.GetStyle("HelpBox");
                helpStyle.richText = true;
                EditorGUI.TextArea(helpRect, docgen.Description, helpStyle);
                helpHeight = helpStyle.CalcHeight(new GUIContent(docgen.Description), EditorGUIUtility.currentViewWidth);
            }

            EditorGUI.EndProperty();
        }
    }
}