using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsCode.Tools.DocGen
{
    [DocGen("DocGen adds a DocGen attribute that allows more documentation text to be added to an attribute. This text will be displayed in the inspector via a PropertyDrawer as well as in the generated documentation.")]
    public class ExampleMonoBehaviour : MonoBehaviour
    {
        [Tooltip("This is a public string field with a tooltip (you are reading it now).")]
        [DocGen("Using the DocGen attribute you can add additional documentation to a field that doesn't fit into a ToolTip. This content will only be visible if the field is expanded. The content will also appear in the generated documentation. ")]
        public string publicString = "This is the default value of this string.";
        [SerializeField]
        [Tooltip("This is a private field, but it has the SerializeField attribute. This text comes from the tooltip for the field.")]
        private string privateSerializedString = "This is the default value.";
        [Tooltip("Field with a range.")]
        [Range(0, 1.5f)]
        public float floatField = 0.5f;

        public string publicButUndocumentedString = "This public string does not have a tooltip.";
    }
}
