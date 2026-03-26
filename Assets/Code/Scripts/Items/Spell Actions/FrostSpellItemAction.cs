using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Tartarus
{
    [CreateAssetMenu(fileName = "FrostSpellItemAction", menuName = "Items/Spell Actions/Frost Spell Item Action")]
    public class FrostSpellItemAction : SpellItemAction
    {

        [Header("Frost Spell Item Action Specific")]
        public string frostSpellCastAnimation = "Frost_Spell_Cast";
        public float frostSpellCastManaCost = 10f;
        public float frostSpellCastStaminaCost = 10f;
        public float forstSpellDuration = 5f;
        private float frostSpellDurationCounter = 0f;
        public int frostModifier = 23;

        private GameObject frostVFX;

        public override void AttemptToPerformAction(PlayerManager playerPerformingAction, SpellItem spellPerformingAction)
        {

            if (playerPerformingAction.playerCombatManager.spellActive)
            {
                return;
            }

            base.AttemptToPerformAction(playerPerformingAction, spellPerformingAction);

            if (frostSpellCastAnimation == "")
            {
                return;
            }

            //Check for stops

            if(playerPerformingAction.playerCombatManager.currentWeapon.isBowType)
            {
                return;
            }

            if(playerPerformingAction.currentFocusPoints <= frostSpellCastManaCost)
            {
                return;
            }

            if (playerPerformingAction.currentStamina <= frostSpellCastStaminaCost)
            {
                return;
            }

            if (!playerPerformingAction.playerLocomotionManager.isGrounded)
            {
                return;
            }

            // Check if there is a running frost coroutine and if there is dont start another one

            PerformSpell(playerPerformingAction, spellPerformingAction);

        }

        private void PerformSpell(PlayerManager playerPerformingAction, SpellItem spellPerformingAction)
        {
            playerPerformingAction.playerCombatManager.spellActive = true;

            playerPerformingAction.currentFocusPoints -= frostSpellCastManaCost;
            playerPerformingAction.currentStamina -= frostSpellCastStaminaCost;

            playerPerformingAction.playerAnimationManager.PlayTargetAnimation(frostSpellCastAnimation, true);

            WeaponManager weaponManager = playerPerformingAction.playerEquipmentManager.weaponHandSlot.GetComponentInChildren<WeaponManager>();

            playerPerformingAction.StartCoroutine(FrostSpellDuration(playerPerformingAction, weaponManager));
            frostVFX = Instantiate(WorldCharacterEffectsManager.instance.frostVFX, weaponManager.transform.position, weaponManager.transform.rotation, weaponManager.transform.parent);
            Destroy(frostVFX, forstSpellDuration);

        }

        IEnumerator FrostSpellDuration(PlayerManager playerPerformingAction, WeaponManager weaponManager)
        {

            frostSpellDurationCounter = forstSpellDuration;

            weaponManager.meleeWeaponDamageCollider.frostDamage = frostModifier;

            while (frostSpellDurationCounter > 0)
            {
                frostSpellDurationCounter -= Time.deltaTime;
                yield return null;
            }
            weaponManager.meleeWeaponDamageCollider.frostDamage = 0;

            if(frostSpellDurationCounter <= 0)
            {
                playerPerformingAction.playerCombatManager.spellActive = false;
            }

            yield return null;
            
        }

    }
}
