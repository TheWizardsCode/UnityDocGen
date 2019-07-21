using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsCode.Tools.Editor.DocGen
{
    public class ExampleMonoBehaviour : MonoBehaviour
    {
        [Tooltip("This is a public string field with a tooltip (you are reading it now).")]
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
