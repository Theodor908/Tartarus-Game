using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

namespace Tartarus
{
    public class PlayerManager : CharacterManager
    {

        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
        [HideInInspector] public PlayerAnimationManager playerAnimationManager;
        [HideInInspector] public PlayerStatsManager playerStatsManager;
        [HideInInspector] public PlayerInventoryManager playerInventoryManager;
        [HideInInspector] public PlayerEquipmentManager playerEquipmentManager;
        [HideInInspector] public PlayerCombatManager playerCombatManager;
        [HideInInspector] public PlayerInteractionManager playerInteractionManager;

        [Header("Equipment")]
        public int previousSpellInHandID = -1;
        public int currentSpellInHandID = -1;
        public int previousWeaponInHandID = -1;
        public int currentWeaponInHandID = -1;
        public int previousPotionInHandID = -1;
        public int currentPotionInHandID = -1;

        [Header ("Player Spells")]
        public int currentSpellBeingUsed = 0;

        [Header("Player Weapons")]
        public int currentWeaponBeingUsed = 0;

        [Header("Player Potions")]
        public int currentPotionBeingUsed = 0;

        [Header("Player Last Respawn Point")]
        public Transform lastRespawnPoint;


        [Header ("Debug")]
        public bool saveGame = false;

        protected override void Awake()
        {
            base.Awake();
            WorldSaveManager.instance.playerManager = this;

            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            playerAnimationManager = GetComponent<PlayerAnimationManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
            playerCombatManager = GetComponent<PlayerCombatManager>();
            playerInteractionManager = GetComponent<PlayerInteractionManager>();

        }

        protected override void Start()
        {
            base.Start();
            PlayerUIManager.instance.playerUIHudManager.setMaxHealthValue(maxHealth);
            PlayerUIManager.instance.playerUIHudManager.setMaxFocusPointsValue(maxFocusPoints);
            PlayerUIManager.instance.playerUIHudManager.setMaxStaminaValue(maxStamina);

            currentHealth = maxHealth;
            currentFocusPoints = maxFocusPoints;
            currentStamina = maxStamina;

            PlayerUIManager.instance.playerUIHudManager.setNewEssenceValue(playerStatsManager.essence);

            PlayerStatsManager.instance.ResetStaminaRegenTimer();

        }

        protected override void Update()
        {
            base.Update();
            playerLocomotionManager.HandleAllMovement();

            HandleSpellChange();
            HandleWeaponChange();
            HandlePotionChange();

            PlayerUIManager.instance.playerUIHudManager.setNewHealthValue(currentHealth);
            PlayerUIManager.instance.playerUIHudManager.setNewFocusPointsValue(currentFocusPoints);
            PlayerUIManager.instance.playerUIHudManager.setNewStaminaValue(currentStamina);

            PlayerUIManager.instance.playerUIHudManager.setNewEssenceValue(playerStatsManager.essence);

            if (isBlocking == true)
            {

                animator.SetBool("isBlocking", true);
                playerStatsManager.physicalAbsorbtion = playerInventoryManager.currentShield.physicalAbsorbtion;
                playerStatsManager.frostAbsorbtion = playerInventoryManager.currentShield.frostAbsorbtion;

            }
            else
            {
                animator.SetBool("isBlocking", false);
                playerStatsManager.physicalAbsorbtion = 0;
                playerStatsManager.frostAbsorbtion = 0;
            }

            CheckHealthPoints();

            if(saveGame)
            {
                WorldSaveManager.instance.SaveGame();
                saveGame = false;
            }

        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            PlayerCamera.instance.HandleAllCameraActions();
        }

        #region Death Event
        public override IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
        {
            PlayerUIManager.instance.playerUIPopUpManager.ShowYouDiedPopUp();
            StartCoroutine(RespawnCoroutine(lastRespawnPoint));
            PlayerUIManager.instance.playerUIMenuManager.CloseAllMenus();

            currentHealth = 0;
            isDead = true;
            animator.SetBool("isDead", true);

            // Reset all flags

            // if in air death air anim

            // if on ground death ground anim

            if (!manuallySelectDeathAnimation)
            {
                characterAnimationManager.PlayTargetAnimation("Dead_01", true);
            }
            else
            {
                // Play default death animation
            }

            // Random death animation

            yield return new WaitForSeconds(5);

            // Award players with runes
            WorldAIManager.instance.ResetAllCharacters();
            PlayerManager playerManager = FindObjectOfType<PlayerManager>();

            if (playerManager != null)
            {
                playerManager.playerStatsManager.essence += characterStatsManager.essence;
            }

        }

        public IEnumerator RespawnCoroutine(Transform respawnPoint)
        {
            yield return new WaitForSeconds(8);
            if(PlayerUIManager.instance.playerUIHudManager.bossHealthBarParent.childCount > 0)
            {
                Destroy(PlayerUIManager.instance.playerUIHudManager.bossHealthBarParent.GetChild(0).gameObject);
                isLockedOn = false;
            }
            RespawnPlayer(respawnPoint);
        }

        public void RespawnPlayer(Transform respawnPoint)
        {
            isDead = false;

            currentHealth = maxHealth;
            currentFocusPoints = maxFocusPoints;
            currentStamina = maxStamina;

            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;

            animator.SetBool("isDead", false);

        }

        #endregion

        #region Item Actions

        public void HandleSpellChange()
        {
            if (previousSpellInHandID != currentSpellInHandID)
            {
                previousSpellInHandID = currentSpellInHandID;
                OnCurrentHandSpellIDChange(currentSpellInHandID);
            }
        }

        public void HandleWeaponChange()
        {

            if (previousWeaponInHandID != currentWeaponInHandID)
            {
                previousWeaponInHandID = currentWeaponInHandID;
                OnCurrentHandWeaponIDChange(currentWeaponInHandID);
            }
        
        }

        public void HandlePotionChange()
        {
            
            if (previousPotionInHandID != currentPotionInHandID)
            {
                previousPotionInHandID = currentPotionInHandID;
                OnCurrentHandPotionIDChange(currentPotionInHandID);
            }

        }

        public void OnCurrentHandSpellIDChange(int newID)
        {
            SpellItem newSpell = Instantiate(WorldItemDatabase.instance.GetSpellItem(newID));
            playerInventoryManager.currentSpell = newSpell;
            PlayerUIManager.instance.playerUIHudManager.setSpellSlotIcon(newID);

        }

        public void OnCurrentHandWeaponIDChange(int newID)
        {
            WeaponItem newWeapon = Instantiate(WorldItemDatabase.instance.GetWeaponItem(newID));
            playerInventoryManager.currentWeapon = newWeapon; 
            playerCombatManager.currentWeapon = newWeapon;
            playerEquipmentManager.LoadWeaponOnHand();
            PlayerUIManager.instance.playerUIHudManager.setWeaponQuickSlotIcon(newID);

        }

        public void OnCurrentHandPotionIDChange(int newID)
        {
            PotionItem newPotion = Instantiate(WorldItemDatabase.instance.GetPotionItem(newID));
            playerInventoryManager.currentPotion = newPotion;
            playerCombatManager.currentPotion = newPotion;
            PlayerUIManager.instance.playerUIHudManager.setPotionQuickSlotIcon(newID);

        }

        public void PerformWeaponBasedAction(int actionID, int weaponID)
        {
            WeaponItemAction weaponAction = WorldActionManager.instance.GetWeaponItemAction(actionID);

            if(weaponAction != null)
            {
                weaponAction.AttemptToPerformAction(this, WorldItemDatabase.instance.GetWeaponItem(weaponID));
            }
            else
            {
                Debug.Log("Weapon action not found");
            }

        }

        public void PerformSpellBasedAction(int actionID, int spellID)
        {
            SpellItemAction spellAction = WorldActionManager.instance.GetSpellItemAction(actionID);

            if (spellAction != null)
            {
                spellAction.AttemptToPerformAction(this, WorldItemDatabase.instance.GetSpellItem(spellID));
            }
            else
            {
                Debug.Log("Spell action not found");
            }

        }

        public void PerformPotionBasedAction(int actionID, int potionID)
        {
            PotionItemAction potionAction = WorldActionManager.instance.GetPotionItemAction(actionID);

            if(potionAction != null)
            {
                potionAction.AttemptToPerformAction(this, WorldItemDatabase.instance.GetPotionItem(potionID));
            }
            else
            {
                Debug.Log("Potion action not found");
            }

        }

        #endregion

        #region Save and Load
        public void SaveGameToCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            currentCharacterData.sceneIndex = SceneManager.GetActiveScene().buildIndex;
            currentCharacterData.characterName = characterName;
            currentCharacterData.xPosition = transform.position.x;
            currentCharacterData.yPosition = transform.position.y;
            currentCharacterData.zPosition = transform.position.z;

            currentCharacterData.lastRespawnPoint = lastRespawnPoint;

            currentCharacterData.characterEssence = playerStatsManager.essence;

            currentCharacterData.vitality = vitality;
            currentCharacterData.endurance = endurance;
            currentCharacterData.attunement = attunement;

            currentCharacterData.currentHealth = currentHealth;
            currentCharacterData.currentFocusPoints = currentFocusPoints;
            currentCharacterData.currentStamina = currentStamina;

            currentCharacterData.currentSpell = playerInventoryManager.currentSpell;
            currentCharacterData.spellQuickSlots = playerInventoryManager.spellInQuickSlots;
            currentCharacterData.spellsInInventory = playerInventoryManager.spellsInInventory;

            currentCharacterData.currentWeapon = playerInventoryManager.currentWeapon;
            currentCharacterData.handQuickSlots = playerInventoryManager.weaponsInHandSlots;
            currentCharacterData. weaponsInInventory = playerInventoryManager.weaponsInInventory;

            currentCharacterData.currentPotion = playerInventoryManager.currentPotion;
            currentCharacterData.potionQuickSlots = playerInventoryManager.potionsInQuickSlots;
            currentCharacterData.potionsInInventory = playerInventoryManager.potionsInInventory;


        }
        public void LoadGameFromCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            characterName = currentCharacterData.characterName;
            transform.position = new Vector3(currentCharacterData.xPosition, currentCharacterData.yPosition, currentCharacterData.zPosition);

            lastRespawnPoint = currentCharacterData.lastRespawnPoint;

            vitality = currentCharacterData.vitality;
            endurance = currentCharacterData.endurance;
            attunement = currentCharacterData.attunement;

            currentHealth = currentCharacterData.currentHealth;
            currentStamina = currentCharacterData.currentStamina;
            currentFocusPoints = currentCharacterData.currentFocusPoints;

            playerStatsManager.essence = currentCharacterData.characterEssence;

            playerInventoryManager.currentSpell = currentCharacterData.currentSpell;
            playerInventoryManager.spellInQuickSlots = currentCharacterData.spellQuickSlots;
            playerInventoryManager.spellsInInventory = currentCharacterData.spellsInInventory;

            playerInventoryManager.currentWeapon = currentCharacterData.currentWeapon;
            playerInventoryManager.weaponsInHandSlots = currentCharacterData.handQuickSlots;
            playerInventoryManager.weaponsInInventory = currentCharacterData.weaponsInInventory;

            playerInventoryManager.currentPotion = currentCharacterData.currentPotion;
            playerInventoryManager.potionsInQuickSlots = currentCharacterData.potionQuickSlots;
            playerInventoryManager.potionsInInventory = currentCharacterData.potionsInInventory;

        }
        #endregion

    }
}
