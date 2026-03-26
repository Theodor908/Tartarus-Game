using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class SiteOfGraceInteractable : Interactable
    {
        [Header("Site Of Grace Info")]
        [SerializeField] int siteOfGraceID;
        [SerializeField] Transform playerSpawnPoint;

        [Header ("Activated")]
        public bool isActivated = false;

        [Header("Site Of Grace Effect")]
        [SerializeField] GameObject siteOfGraceEffect;

        [Header("Interaction Text")]
        [SerializeField] string unactivatedInteractionText = "Restore Site Of Grace";
        [SerializeField] string activatedInteractionText = "Rest";

        protected override void Start()
        {
            base.Start();

            if(WorldSaveManager.instance.currentCharacterData.sitesOfGrace.ContainsKey(siteOfGraceID))
            {
                isActivated = WorldSaveManager.instance.currentCharacterData.sitesOfGrace[siteOfGraceID];
            }
            else
            {
                isActivated = false;
            }


            if(isActivated)
            {
                // Play Effect
                siteOfGraceEffect.SetActive(true);
                interactableText = activatedInteractionText;

            }
            else
            {
                siteOfGraceEffect.SetActive(false);
                interactableText = unactivatedInteractionText;
            }



        }

        private void RestoreSiteOfGrace(PlayerManager playerManager)
        {

            isActivated = true;

            if (WorldSaveManager.instance.currentCharacterData.sitesOfGrace.ContainsKey(siteOfGraceID))
            {
                WorldSaveManager.instance.currentCharacterData.sitesOfGrace.Remove(siteOfGraceID);
            }
            // This is now activated
            WorldSaveManager.instance.currentCharacterData.sitesOfGrace.Add(siteOfGraceID, isActivated);

            // Play Animation
            playerManager.playerAnimationManager.PlayTargetAnimation("Activate_Site_Of_Grace", true);

            // Send Pop Up

            PlayerUIManager.instance.playerUIPopUpManager.SendSiteOfGraceRestoredPopUp("Site Of Grace Restored");

            // Play Effect

            siteOfGraceEffect.SetActive(true);

            StartCoroutine(WaitForAnimationAndPopUpThenRestoreCollider());

            interactableText = activatedInteractionText;

        }

        private void RestAtSiteOfGrace(PlayerManager playerManager)
        {
            playerManager.animator.SetBool("menuOpen", true);
            playerManager.lastRespawnPoint = playerSpawnPoint;
            playerManager.playerAnimationManager.PlayTargetAnimation("Rest", true);
            PlayerUIMenuManager.instance.OpenDefaultMenu();
            PlayerUIMenuManager.instance.potionsMenu.canUsePotionExchange = true;
            PlayerUIMenuManager.instance.levelUpMenu.ShowLevelUpMenu();
            WorldAIManager.instance.ResetAllCharacters();
            RestoreVitals(playerManager);
            RestoreFlaskAmount(playerManager);

            StartCoroutine(EnableInteractableCollider(playerManager));

        }

        private void RestoreFlaskAmount(PlayerManager playerManager)
        {
            for(int i = 0; i < playerManager.playerInventoryManager.potionsInQuickSlots.Length; i++)
            {
                playerManager.playerInventoryManager.potionsInQuickSlots[i].numberOfUses = playerManager.playerInventoryManager.potionsInQuickSlots[i].maxNumberOfUses;
            }

            for (int i = 0; i < playerManager.playerInventoryManager.potionsInInventory.Length; i++)
            {
                playerManager.playerInventoryManager.potionsInInventory[i].numberOfUses = playerManager.playerInventoryManager.potionsInInventory[i].maxNumberOfUses;
            }

        }

        private void RestoreVitals(PlayerManager playerManager)
        {

            playerManager.currentHealth = playerManager.maxHealth;
            playerManager.currentFocusPoints = playerManager.maxFocusPoints;
            playerManager.currentStamina = playerManager.maxStamina;

        }

        private IEnumerator EnableInteractableCollider(PlayerManager playerManager)
        {
            while(true)
            {
                if(PlayerUIMenuManager.instance.isMenuOpen == false)
                {
                    interactableCollider.enabled = true;
                    playerManager.animator.SetBool("menuOpen", false);
                    PlayerUIMenuManager.instance.potionsMenu.canUsePotionExchange = false;
                    PlayerUIMenuManager.instance.levelUpMenu.canUseLevelUp = false;
                    PlayerUIMenuManager.instance.levelUpMenu.CloseLevelUpMenu();
                    WorldSaveManager.instance.SaveGame();
                    yield break;
                }
                yield return null;
            }

        }

        private IEnumerator WaitForAnimationAndPopUpThenRestoreCollider()
        {
            yield return new WaitForSeconds(6f);
            interactableCollider.enabled = true;
        }

        public override void Interact(PlayerManager playerManager)
        {
            base.Interact(playerManager);

            if(!isActivated)
            {
                RestoreSiteOfGrace(playerManager);
            }
            else
            {
                RestAtSiteOfGrace(playerManager);
            }

        }

    }
}

