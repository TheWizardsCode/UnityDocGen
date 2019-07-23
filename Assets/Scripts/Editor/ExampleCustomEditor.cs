using UnityEditor;

namespace WizardsCode.Tools.DocGen
{
    /// <summary>
    /// This is a simple example of a custom editor. Note how the `OnInspectorGUI` method calls `this.DrawDocGenAttributes` before drawing the editor GUI.
    /// </summary>
    [CustomEditor(typeof(ExampleMonoBehaviour), true)]
    public class ExampleCustomEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            this.DrawDocGenAttributes();

            // Your customer inspector code goes here
            DrawDefaultInspector();
        }
    }
}