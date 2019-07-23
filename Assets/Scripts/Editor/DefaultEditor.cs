using UnityEditor;
using UnityEngine;


namespace WizardsCode.Tools.DocGen
{
    [CustomEditor(typeof(UnityEngine.Object), true)]
    public class DefaultEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            this.DrawDocGenAttributes();
            DrawDefaultInspector();
        }
    }
}
