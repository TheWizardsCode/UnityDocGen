using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WizardsCode.Tools.DocGen
{
    [CustomEditor(typeof(ExampleMonoBehaviour), true)]
    public class ExampleCustomEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            this.DrawDocGenAttributes();

            // Your customer inspector code goes here
            DrawDefaultInspector();
        }
    }
}