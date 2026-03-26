using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    [CreateAssetMenu(menuName = "Character Actions/Weapon Actions/OffHandMeleeAction")]
    public class OffHandMeleeAction : WeaponItemAction
    {

        public override void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            base.AttemptToPerformAction(playerPerformingAction, weaponPerformingAction);

            if(!playerPerformingAction.playerCombatManager.canBlock)
            {
                return;
            }

            // Check for attack status
            if (playerPerformingAction.isAttacking)
            {
                playerPerformingAction.isBlocking = false;
                return;
            }

            if (playerPerformingAction.isBlocking)
            {
                return;
            }

            playerPerformingAction.isBlocking = true;

        }

    }
}