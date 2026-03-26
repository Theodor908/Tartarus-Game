using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    [CreateAssetMenu(fileName = "PotionFocusPointsItemAction", menuName = "Items/Potion Actions/Potion Focus Points Item Action")]
    public class PotionFocusPointsItemAction : PotionItemAction
    {

        public override void AttemptToPerformAction(PlayerManager playerPerformingAction, PotionItem potionPerformingAction)
        {
            base.AttemptToPerformAction(playerPerformingAction, potionPerformingAction);

            if (potionPerformingAction.numberOfUses <= 0)
            {
                return;
            }

            if(playerPerformingAction.currentFocusPoints == playerPerformingAction.maxFocusPoints)
            {
                return;
            }

            if (!playerPerformingAction.playerLocomotionManager.isGrounded)
            {
                return;
            }

            if (playerPerformingAction.isInteracting && !playerPerformingAction.playerCombatManager.canComboWithCurrentWeapon)
            {
                return;
            }

            PerformPotionDrink(playerPerformingAction, potionPerformingAction);

        }

        protected override void PerformPotionDrink(PlayerManager playerPerformingAction, PotionItem potionPerformingAction)
        {

            base.PerformPotionDrink(playerPerformingAction, potionPerformingAction);
            potionPerformingAction.numberOfUses--;
            playerPerformingAction.currentFocusPoints += potionPerformingAction.mitigatedQuantitiy;

        }

    }
}