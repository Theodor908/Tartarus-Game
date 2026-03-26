using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    [CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Heavy Attack Action")]
    public class HeavyAttackWeaponItemAction : WeaponItemAction
    {
        [SerializeField] string heavy_Attack_01 = "RightHand_Heavy_Attack_01";
        [SerializeField] string heavy_Attack_02 = "RightHand_Heavy_Attack_02";
        [SerializeField] string heavy_Attack_03 = "RightHand_Heavy_Attack_03";

        public override void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            base.AttemptToPerformAction(playerPerformingAction, weaponPerformingAction);

            if (heavy_Attack_01 == "")
            {
                return;
            }

            //Check for stops

            if (playerPerformingAction.currentStamina <= weaponPerformingAction.baseStaminaCost * weaponPerformingAction.heavyAttackStaminaCostMultiplier)
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

            playerPerformingAction.isAttacking = true;

            PerformHeavyAttack(playerPerformingAction, weaponPerformingAction);

        }

        private void PerformHeavyAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            // Currently attacking and can combo
            if (playerPerformingAction.isInteracting && playerPerformingAction.playerCombatManager.canComboWithCurrentWeapon)
            {
                playerPerformingAction.playerCombatManager.canComboWithCurrentWeapon = false;
                // Perform attack based on previous attack

                if(playerPerformingAction.playerCombatManager.lastAttackAnimationPerformed == heavy_Attack_01)
                {
                    playerPerformingAction.playerAnimationManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.HeavyAttack02, heavy_Attack_02, true);
                }
                else if(playerPerformingAction.playerCombatManager.lastAttackAnimationPerformed == heavy_Attack_02)
                {
                    playerPerformingAction.playerAnimationManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.HeavyAttack03, heavy_Attack_03, true);
                }
                else if(playerPerformingAction.playerCombatManager.lastAttackAnimationPerformed == heavy_Attack_03)
                {
                    playerPerformingAction.playerAnimationManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.HeavyAttack01, heavy_Attack_01, true);
                }
                else
                {
                    playerPerformingAction.playerAnimationManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.HeavyAttack01, heavy_Attack_01, true); 
                }
            }
            // Regular attack
            else if (!playerPerformingAction.isInteracting)
            {
                playerPerformingAction.playerAnimationManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.HeavyAttack01, heavy_Attack_01, true);
            }

        }

    }
}
