using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    [CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog/DialogText")]
    public class DialogText : ScriptableObject
    {

        public string speakerName;
        [TextArea (5,10)]
        public string[] paragraphs;

    }
}