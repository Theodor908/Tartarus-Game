using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    [CreateAssetMenu(fileName = "PotionHealItemAction", menuName = "Items/Potion Actions/Potion Heal Item Action")]
    public class PotionHealItemAction : PotionItemAction
    {

        public override void AttemptToPerformAction(PlayerManager playerPerformingAction, PotionItem potionPerformingAction)
        {
            base.AttemptToPerformAction(playerPerformingAction, potionPerformingAction);

            if (!playerPerformingAction.playerLocomotionManager.isGrounded)
            {
                return;
            }

            if (playerPerformingAction.isInteracting && !playerPerformingAction.playerCombatManager.canComboWithCurrentWeapon)
            {
                return;
            }

            if(playerPerformingAction.currentHealth < playerPerformingAction.maxHealth)
                PerformPotionDrink(playerPerformingAction, potionPerformingAction);

        }

        protected override void PerformPotionDrink(PlayerManager playerPerformingAction, PotionItem potionPerformingAction)
        {
               
            base.PerformPotionDrink(playerPerformingAction, potionPerformingAction);

            if (potionPerformingAction.numberOfUses <= 0)
            {
                return;
            }

            potionPerformingAction.numberOfUses--;
            playerPerformingAction.currentHealth += potionPerformingAction.mitigatedQuantitiy;

        }

    }
}