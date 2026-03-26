using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    [CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Light Attack Action")]
    public class LightAttackWeaponItemAction : WeaponItemAction
    {
        [Header ("Light Attack")]
        [SerializeField] string light_Attack_01 = "RightHand_Light_Attack_01";
        [SerializeField] string light_Attack_02 = "RightHand_Light_Attack_02";
        [SerializeField] string light_Attack_03 = "RightHand_Light_Attack_03";
        [SerializeField] string light_Attack_04 = "RightHand_Light_Attack_04";

        [Header ("Running Attack")]
        [SerializeField] string run_attack_01 = "Main_Run_Attack_01";

        [Header ("Rolling Attack")]
        [SerializeField] string roll_attack_01 = "Main_Roll_Attack_01";

        [Header ("Backstep Attack")]
        [SerializeField] string backstep_attack_01 = "Main_Backstep_Attack_01";

        public override void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            base.AttemptToPerformAction(playerPerformingAction, weaponPerformingAction);

            if(light_Attack_01 == "")
            {
                return;
            }

            //Check for stops

            if(playerPerformingAction.currentStamina <= weaponPerformingAction.baseStaminaCost * weaponPerformingAction.lightAttackStaminaCostMultiplier)
            {
                return;
            }

            if(!playerPerformingAction.playerLocomotionManager.isGrounded)
            {
                return;
            }

            playerPerformingAction.isAttacking = true;

            if(playerPerformingAction.isSprinting)
            {
                PerformRunningAttack(playerPerformingAction, weaponPerformingAction);
                return;
            }

            if(playerPerformingAction.characterCombatManager.canPerformRollingAttack)
            {
                PerformRollingAttack(playerPerformingAction, weaponPerformingAction);
                return;
            }

            if(playerPerformingAction.characterCombatManager.canPerformBackstepAttack)
            {
                PerformBackstepAttack(playerPerformingAction, weaponPerformingAction);
                return;
            }


            PerformLightAttack(playerPerformingAction, weaponPerformingAction);

        }

        private void PerformLightAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {

            // Currently attacking and can combo
            if(playerPerformingAction.isInteracting && playerPerformingAction.playerCombatManager.canComboWithCurrentWeapon)
            {
                playerPerformingAction.playerCombatManager.canComboWithCurrentWeapon = false;
                // Perform attack based on previous attack

               if(playerPerformingAction.playerCombatManager.lastAttackAnimationPerformed == light_Attack_01)
               {
                   playerPerformingAction.playerAnimationManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.LightAttack02, light_Attack_02, true, true, true);
               }
               else if(playerPerformingAction.playerCombatManager.lastAttackAnimationPerformed == light_Attack_02)
               {
                   playerPerformingAction.playerAnimationManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.LightAttack03, light_Attack_03, true, true, true);
               }
               else if(playerPerformingAction.playerCombatManager.lastAttackAnimationPerformed == light_Attack_03)
               {
                   playerPerformingAction.playerAnimationManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.LightAttack04, light_Attack_04, true, true, true);
               }
               else
               {
                   playerPerformingAction.playerAnimationManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.LightAttack01, light_Attack_01, true, true, true);
               }

            }
            // Regular attack
            else if(!playerPerformingAction.isInteracting)
            {
                playerPerformingAction.playerAnimationManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.LightAttack01, light_Attack_01, true, true, true);
            }

        }


        private void PerformRunningAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            // If we are running and attacking
            playerPerformingAction.playerAnimationManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.RunningAttack01, run_attack_01, true, true, true);
        }

        private void PerformRollingAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            // If we are rolling and attacking
            playerPerformingAction.playerCombatManager.canPerformRollingAttack = false;
            playerPerformingAction.playerAnimationManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.RollingAttack01, roll_attack_01, true, true, true);
        }

        private void PerformBackstepAttack(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            playerPerformingAction.playerCombatManager.canPerformBackstepAttack = false;
            playerPerformingAction.playerAnimationManager.PlayTargetAttackActionAnimation(weaponPerformingAction, AttackType.BackstepAttack01, backstep_attack_01, true, true, true);
        }


    }
}
