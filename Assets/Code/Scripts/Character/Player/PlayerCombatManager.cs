using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class PlayerCombatManager : CharacterCombatManager
    {
        PlayerManager playerManager;
        public SpellItem currentSpell;
        public WeaponItem currentWeapon;
        public WeaponItem curentShield;
        public PotionItem currentPotion;
        public GameObject currentThrowableWeapon;
        public bool spellActive = false;

        public float currentChargeTime;

        [Header("Flags")]
        public bool canComboWithCurrentWeapon;

        protected override void Awake()
        {
            base.Awake();
            playerManager = GetComponent<PlayerManager>();
        }

        public void PerformWeaponBasedAction(WeaponItemAction weaponAction, WeaponItem weaponPerformingAction)
        {
            weaponAction.AttemptToPerformAction(playerManager, weaponPerformingAction);
            playerManager.PerformWeaponBasedAction(weaponAction.actionID, weaponPerformingAction.itemID);
        }

        public void PerformSpellBasedAction(SpellItemAction spellAction, SpellItem spellPerformingAction)
        {
            spellAction.AttemptToPerformAction(playerManager, spellPerformingAction);
            playerManager.PerformSpellBasedAction(spellAction.actionID, spellPerformingAction.itemID);
        }

        public void PerformPotionBasedAction(PotionItemAction potionAction, PotionItem potionPerformingAction)
        {
            potionAction.AttemptToPerformAction(playerManager, potionPerformingAction);
            playerManager.PerformPotionBasedAction(potionAction.actionID, potionPerformingAction.itemID);
        }

        public virtual void DrainStaminaBasedOnAttack()
        {
            float staminaDeducted = 0;

            if(currentWeapon == null)
            {
                Debug.Log("No weapon assigned to player");
                return;
            }

            switch(currentAttackType)
            {
                case AttackType.LightAttack01:
                    staminaDeducted = currentWeapon.baseStaminaCost * currentWeapon.lightAttackStaminaCostMultiplier;
                    break;
                case AttackType.LightAttack02:
                    staminaDeducted = currentWeapon.baseStaminaCost * currentWeapon.lightAttackStaminaCostMultiplier;
                    break;
                case AttackType.LightAttack03:
                    staminaDeducted = currentWeapon.baseStaminaCost * currentWeapon.lightAttackStaminaCostMultiplier;
                    break;
                case AttackType.LightAttack04:
                    staminaDeducted = currentWeapon.baseStaminaCost * currentWeapon.lightAttackStaminaCostMultiplier;
                    break;
                case AttackType.HeavyAttack01:
                    staminaDeducted = currentWeapon.baseStaminaCost * currentWeapon.heavyAttackStaminaCostMultiplier;
                    break;
                case AttackType.HeavyAttack02:
                    staminaDeducted = currentWeapon.baseStaminaCost * currentWeapon.heavyAttackStaminaCostMultiplier;
                    break;
                case AttackType.HeavyAttack03:
                    staminaDeducted = currentWeapon.baseStaminaCost * currentWeapon.heavyAttackStaminaCostMultiplier;
                    break;
                case AttackType.ChargedAttack01:
                    staminaDeducted = currentWeapon.baseStaminaCost * currentWeapon.chargedHeavyAttackStaminaCostMultiplier;
                    break;
                case AttackType.ChargedAttack02:
                    staminaDeducted = currentWeapon.baseStaminaCost * currentWeapon.chargedHeavyAttackStaminaCostMultiplier;
                    break;
                case AttackType.RunningAttack01:
                    staminaDeducted = currentWeapon.baseStaminaCost * currentWeapon.runningAttackStaminaCostMultiplier;
                    break;
                case AttackType.RollingAttack01:
                    staminaDeducted = currentWeapon.baseStaminaCost * currentWeapon.rollingAttackStaminaCostMultiplier;
                    break;
                case AttackType.BackstepAttack01:
                    staminaDeducted = currentWeapon.baseStaminaCost * currentWeapon.backstepAttackStaminaCostMultiplier;
                    break;
                default:
                    break;
            }

            playerManager.currentStamina -= Mathf.RoundToInt(staminaDeducted);
        }

        public override void SetLockOnTarget(CharacterManager newTarget)
        {
            base.SetLockOnTarget(newTarget);
            //Debug.Log("Setting lock on target");
            PlayerCamera.instance.SetLockCameraHeight();
        }

        public override void EnableCanDoCombo()
        {
            base.EnableCanDoCombo();
            canComboWithCurrentWeapon = true;
        }

        public override void DisableCanDoCombo()
        {
            base.DisableCanDoCombo();
            canComboWithCurrentWeapon = false;
        }

        public override void CanThrow()
        {
            base.CanThrow();
            playerManager.playerCombatManager.canThrow = true;

            StartCoroutine(DestroyAfterTime(currentThrowableWeapon, 7));

        }

    }
}