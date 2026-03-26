using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    [CreateAssetMenu(fileName = "SpellItemAction", menuName = "Items/Spell Actions/Spell Item Action")]
    public class SpellItemAction : ScriptableObject
    {
        public int actionID;
        public virtual void AttemptToPerformAction(PlayerManager playerPerformingAction, SpellItem spellPerformingAction)
        {

        }

    }
}