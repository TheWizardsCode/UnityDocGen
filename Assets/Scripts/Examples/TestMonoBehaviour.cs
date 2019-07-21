using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WizardsCode.Tools.DocGen
{
    public class TestMonoBehaviour : MonoBehaviour
    {
        public string PublicNoTooltip = "No tooltip";

        [Tooltip("This is a property with a tooltip")]
        public string PublicWithTooltip = "With tooltip";

        [SerializeField]
        private string privateWithSerializeFieldShouldAppear = "Private fields with SerializeField attribute should have a tooltip";

        private string privateWithoutSerializeFieldShouldNotAppear = "Private fields without SerializeField attribute don't have a tooltip";
    }
}
