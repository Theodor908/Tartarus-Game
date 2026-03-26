using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class PotionItemAction : ScriptableObject
    {
        public int actionID;
        [SerializeField] string potion_drink_01 = "potion_drink_01";

        public virtual void AttemptToPerformAction(PlayerManager playerPerformingAction, PotionItem potionPerformingAction)
        {
            //Need to keep track of the potion that is performing the action
            playerPerformingAction.currentPotionBeingUsed = potionPerformingAction.itemID;
        }

        protected virtual void PerformPotionDrink(PlayerManager playerPerformingAction, PotionItem potionPerformingAction)
        {

            if (potion_drink_01 == "")
            {
                return;
            }

            if(potionPerformingAction.numberOfUses <= 0)
            {
                playerPerformingAction.playerAnimationManager.PlayTargetAnimation("Potion_Empty", true);
                return;
            }

            playerPerformingAction.playerAnimationManager.PlayTargetAnimation(potion_drink_01, true);

        }

    }
}