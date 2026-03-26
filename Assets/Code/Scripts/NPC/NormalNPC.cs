using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class NormalNPC : NPC, ITalkable
    {
        [SerializeField] DialogText _dialogText;
        [SerializeField] DialogController _dialogController;
        public override void Interact()
        {
            Talk(_dialogText);
        }

        public void Talk(DialogText dialogText)
        {
            _dialogController.DisplayNextParagraph(dialogText);
        }
    }
}