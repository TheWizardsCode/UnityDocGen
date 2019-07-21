using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsCode.Tools.DocGen
{
    [DocGen("The DocGen attribute also works for ScriptableObjects.")]
    public class ExampleScriptableObject : ScriptableObject
    {
        public string PublicNoTooltip = "No tooltip";

        [Tooltip("This is a property with a tooltip")]
        [DocGen("The DocGen attribute also works for ScriptableObjects.")]
        public string PublicWithTooltip = "With tooltip";

        [SerializeField]
        private string privateWithSerializeFieldShouldAppear = "Private fields with SerializeField attribute should have a tooltip";

        private string privateWithoutSerializeFieldShouldNotAppear = "Private fields without SerializeField attribute don't have a tooltip";
    }
}
