using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

namespace Tartarus
{
    public class PlayerEquipmentManager : CharacterEquipmentManager, IInventorySubscriber
    {

        public static PlayerEquipmentManager instance;

        PlayerManager playerManager;
        public ItemModelInstantiationSlot weaponHandSlot;
        public ItemModelInstantiationSlot shieldHandSlot;

        public List<IInventorySubscriber> inventorySubscriber;

        [SerializeField] WeaponManager weaponManager;

        private GameObject weaponModel;
        private GameObject shieldModel;

        protected override void Awake()
        {
            base.Awake();

            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }

            playerManager = GetComponent<PlayerManager>();
        }

        protected override void Start()
        {
            base.Start();
            InitializeSpellSlots();
            InitializemodelSlots();
            IntializePotionSlots();
        }

        private void InitializeSpellSlots()
        {

            SpellItem spellItem = null;
            for (int i = 0; i < playerManager.playerInventoryManager.spellInQuickSlots.Length; i++)
            {

                if (playerManager.playerInventoryManager.spellInQuickSlots[i].itemID != WorldItemDatabase.instance.nopeSpellItem.itemID)
                {
                    spellItem = playerManager.playerInventoryManager.spellInQuickSlots[i];
                    playerManager.playerInventoryManager.spellIndex = i;
                    playerManager.currentSpellInHandID = spellItem.itemID;
                    break; 
                }
            }

        }

        private void InitializemodelSlots()
        {
            ItemModelInstantiationSlot[] modelSlots = GetComponentsInChildren<ItemModelInstantiationSlot>();

            foreach (var modelSlot in modelSlots)
            {
                if (modelSlot.modelSlot == ModelSlot.RightHand)
                {
                    weaponHandSlot = modelSlot;
                }
                else if (modelSlot.modelSlot == ModelSlot.LeftHand)
                {
                    shieldHandSlot = modelSlot;
                }
            }

            WeaponItem weapon = null;
            for (int i = 0; i < playerManager.playerInventoryManager.weaponsInHandSlots.Length; i++)
            {

                if (playerManager.playerInventoryManager.weaponsInHandSlots[i].itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
                {
                    
                    if (weapon == null)
                    {
                        weapon = playerManager.playerInventoryManager.weaponsInHandSlots[i];
                        playerManager.playerInventoryManager.handSlotIndex = i;
                        playerManager.currentWeaponInHandID = weapon.itemID;
                        break;
                    }
                }
            }

        }

        public void IntializePotionSlots()
        {

            PotionItem potionItem = null;
            for (int i = 0; i < playerManager.playerInventoryManager.potionsInQuickSlots.Length; i++)
            {

                if (playerManager.playerInventoryManager.potionsInQuickSlots[i].itemID != WorldItemDatabase.instance.emptyPotion.itemID)
                {
                    potionItem = playerManager.playerInventoryManager.potionsInQuickSlots[i];
                    playerManager.playerInventoryManager.potionIndex = i;
                    playerManager.currentPotionInHandID = potionItem.itemID;
                    break;
                }
            }

        }

        public void RefreshSpellModelSlots()
        {

            if(playerManager.playerInventoryManager.spellIndex != -1)
            {
                if (playerManager.playerInventoryManager.spellInQuickSlots[playerManager.playerInventoryManager.spellIndex].itemID != WorldItemDatabase.instance.nopeSpellItem.itemID)
                {
                    playerManager.playerInventoryManager.currentSpell = playerManager.playerInventoryManager.spellInQuickSlots[playerManager.playerInventoryManager.spellIndex];
                    playerManager.currentSpellInHandID = playerManager.playerInventoryManager.spellInQuickSlots[playerManager.playerInventoryManager.spellIndex].itemID;
                }
                else
                {
                    playerManager.playerInventoryManager.currentSpell = WorldItemDatabase.instance.nopeSpellItem;
                    playerManager.currentSpellInHandID = WorldItemDatabase.instance.nopeSpellItem.itemID;
                    playerManager.playerInventoryManager.spellIndex = -1;
                }
            }
            else
            {
                playerManager.playerInventoryManager.currentSpell = WorldItemDatabase.instance.nopeSpellItem;
                playerManager.currentSpellInHandID = WorldItemDatabase.instance.nopeSpellItem.itemID;
            }

        }

        public void RefreshWeaponModelSlots()
        {

            if(playerManager.playerInventoryManager.handSlotIndex != -1)
            {
                if (playerManager.playerInventoryManager.weaponsInHandSlots[playerManager.playerInventoryManager.handSlotIndex].itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
                {
                    playerManager.playerInventoryManager.currentWeapon = playerManager.playerInventoryManager.weaponsInHandSlots[playerManager.playerInventoryManager.handSlotIndex];
                    playerManager.currentWeaponInHandID = playerManager.playerInventoryManager.currentWeapon.itemID;
                
                }
                else
                {
                    playerManager.playerInventoryManager.currentWeapon = WorldItemDatabase.instance.unarmedWeapon;
                    playerManager.currentWeaponInHandID = WorldItemDatabase.instance.unarmedWeapon.itemID;
                    playerManager.playerInventoryManager.handSlotIndex = -1;
                }
            }
            else
            {
                playerManager.playerInventoryManager.currentWeapon = WorldItemDatabase.instance.unarmedWeapon;
                playerManager.currentWeaponInHandID = WorldItemDatabase.instance.unarmedWeapon.itemID;
            }

        }

        public void RefreshPotionModelSlots()
        {

            if(playerManager.playerInventoryManager.potionIndex != -1)
            {
                if (playerManager.playerInventoryManager.potionsInQuickSlots[playerManager.playerInventoryManager.potionIndex].itemID != WorldItemDatabase.instance.emptyPotion.itemID)
                {
                    playerManager.playerInventoryManager.currentPotion = playerManager.playerInventoryManager.potionsInQuickSlots[playerManager.playerInventoryManager.potionIndex];
                    playerManager.currentPotionInHandID = playerManager.playerInventoryManager.currentPotion.itemID;
                }
                else
                {
                    playerManager.playerInventoryManager.currentPotion = WorldItemDatabase.instance.emptyPotion;
                    playerManager.currentPotionInHandID = WorldItemDatabase.instance.emptyPotion.itemID;
                    playerManager.playerInventoryManager.potionIndex = -1;
                }
            }
            else
            {
                playerManager.playerInventoryManager.currentPotion = WorldItemDatabase.instance.emptyPotion;
                playerManager.currentPotionInHandID = WorldItemDatabase.instance.emptyPotion.itemID;
            }

        }

        #region  Hand

        public void SwitchWeapon()
        {

            if(playerManager.isInteracting)
            {
                return;
            }

            playerManager.playerAnimationManager.PlayTargetAnimation("Swap_Right_Weapon_01", false, false, true, true);
            WeaponItem selectedWeapon = null;

            // Get next possible weapon
            playerManager.playerInventoryManager.handSlotIndex++;
            // Reset index if it goes out of bounds
            if (playerManager.playerInventoryManager.handSlotIndex < 0 || playerManager.playerInventoryManager.handSlotIndex > 2)
            {
                playerManager.playerInventoryManager.handSlotIndex = 0;

                // If there are more than one weapons dont go to unarmed
                float weaponCount = 0;
                WeaponItem firstWeapon = null;
                int firstWeaponIndex = 0;

                for (int i = 0; i < playerManager.playerInventoryManager.weaponsInHandSlots.Length; i++)
                {

                    if (playerManager.playerInventoryManager.weaponsInHandSlots[i].itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
                    {
                        weaponCount++;
                        if (firstWeapon == null)
                        {
                            firstWeapon = playerManager.playerInventoryManager.weaponsInHandSlots[i];
                            firstWeaponIndex = i;
                        }
                    }
                }

                if (weaponCount <= 1)
                {

                    playerManager.playerInventoryManager.handSlotIndex = -1;
                    selectedWeapon = Instantiate(WorldItemDatabase.instance.unarmedWeapon);
                    playerManager.currentWeaponInHandID = selectedWeapon.itemID;
                }
                else
                {

                    playerManager.playerInventoryManager.handSlotIndex = firstWeaponIndex;
                    playerManager.currentWeaponInHandID = firstWeapon.itemID;

                }

                return;

            }

            // Get the weapon from the inventory

            foreach (WeaponItem weapon in playerManager.playerInventoryManager.weaponsInHandSlots)
            {
                // IF the weapon is not unarmed we proceed

                if (playerManager.playerInventoryManager.weaponsInHandSlots[playerManager.playerInventoryManager.handSlotIndex].itemID != WorldItemDatabase.instance.unarmedWeapon.itemID)
                {
                    selectedWeapon = playerManager.playerInventoryManager.weaponsInHandSlots[playerManager.playerInventoryManager.handSlotIndex];
                    playerManager.currentWeaponInHandID = selectedWeapon.itemID;
                    return;
                }
            }

            if(selectedWeapon == null && playerManager.playerInventoryManager.handSlotIndex <= 2)
            {
                SwitchWeapon();
            }

        }

        public void LoadWeaponOnHand()
        {

            if(playerManager.playerInventoryManager.currentWeapon != null)
            {

                if(playerManager.playerInventoryManager.currentWeapon.hasShield)
                {

                    if (shieldHandSlot.gameObject.transform.childCount > 0)
                    {
                        shieldHandSlot.UnloadWeapon();
                    }

                    shieldModel = Instantiate(playerManager.playerInventoryManager.currentShield.weaponModel);
                    shieldHandSlot.LoadWeapon(shieldModel);

                }

                weaponHandSlot.UnloadWeapon();
                weaponModel = Instantiate(playerManager.playerInventoryManager.currentWeapon.weaponModel);

                if (shieldHandSlot.gameObject.transform.childCount > 0 && playerManager.playerInventoryManager.currentWeapon.hasShield == false)
                {
                    shieldHandSlot.UnloadWeapon();
                }

                if (playerManager.playerInventoryManager.currentWeapon.isBowType)
                {
                    shieldHandSlot.LoadWeapon(weaponModel);
                    playerManager.playerInventoryManager.currentWeapon.bowHandle =  weaponModel.GetComponentInChildren<BowHolder>().gameObject;
                    playerManager.playerInventoryManager.currentWeapon.bowString = weaponModel.GetComponentInChildren<BowString>().gameObject;

                }
                else
                {
                    weaponHandSlot.LoadWeapon(weaponModel);
                }
               
                weaponManager = weaponModel.GetComponent<WeaponManager>();
                if(playerManager.playerInventoryManager.currentWeapon.isBowType  == false)
                    weaponManager.SetMeleeWeaponDamage(playerManager, playerManager.playerInventoryManager.currentWeapon);

                playerManager.playerAnimationManager.UpdateAnimatorController(playerManager.playerInventoryManager.currentWeapon.weaponAnimatorOverride);
            }
        }

        public void SwitchSpell()
        {

            SpellItem selectedSpell = null;

            // Get next possible spell

            playerManager.playerInventoryManager.spellIndex++;

            // Reset index if it goes out of bounds

            if (playerManager.playerInventoryManager.spellIndex < 0 || playerManager.playerInventoryManager.spellIndex > 2)
            {
                playerManager.playerInventoryManager.spellIndex = 0;

                // If there is a spell dont let the slot empty

                float spellCount = 0;
                SpellItem firstSpell = null;
                int firstSpellIndex = 0;

                for (int i = 0; i < playerManager.playerInventoryManager.spellInQuickSlots.Length; i++)
                {

                    if (playerManager.playerInventoryManager.spellInQuickSlots[i].itemID != WorldItemDatabase.instance.nopeSpellItem.itemID)
                    {
                        spellCount++;
                        if (firstSpell == null)
                        {
                            firstSpell = playerManager.playerInventoryManager.spellInQuickSlots[i];
                            firstSpellIndex = i;
                        }
                    }

                    if (spellCount <= 1)
                    {
                        playerManager.playerInventoryManager.spellIndex = -1;
                        selectedSpell = Instantiate(WorldItemDatabase.instance.nopeSpellItem);
                        playerManager.currentSpellInHandID = selectedSpell.itemID;
                    }
                    else
                    {
                        playerManager.playerInventoryManager.spellIndex = firstSpellIndex;
                        playerManager.currentSpellInHandID = firstSpell.itemID;
                    }

                }

                return;

            }

            // Get the spell from the inventory

            foreach (SpellItem spell in playerManager.playerInventoryManager.spellInQuickSlots)
            {
                // IF the spell is not nope we proceed

                if (playerManager.playerInventoryManager.spellInQuickSlots[playerManager.playerInventoryManager.spellIndex].itemID != WorldItemDatabase.instance.nopeSpellItem.itemID)
                {
                    selectedSpell = playerManager.playerInventoryManager.spellInQuickSlots[playerManager.playerInventoryManager.spellIndex];
                    playerManager.currentSpellInHandID = selectedSpell.itemID;
                    return;
                }
            }

            if (selectedSpell == null && playerManager.playerInventoryManager.spellIndex <= 2)
            {
                SwitchSpell();
            }


        }

        public void SwitchPotion()
        {

            // Get next possible potion
            PotionItem selectedPotion = null;

            playerManager.playerInventoryManager.potionIndex++;

            // Reset index if it goes out of bounds

            if (playerManager.playerInventoryManager.potionIndex < 0 || playerManager.playerInventoryManager.potionIndex > 2)
            {
                playerManager.playerInventoryManager.potionIndex = 0;

                // If there is a potion dont let the slot empty

                float potionCount = 0;
                PotionItem firstPotion = null;
                int firstPotionIndex = 0;

                for (int i = 0; i < playerManager.playerInventoryManager.potionsInQuickSlots.Length; i++)
                {

                    if (playerManager.playerInventoryManager.potionsInQuickSlots[i].itemID != WorldItemDatabase.instance.emptyPotion.itemID)
                    {
                        potionCount++;
                        if (firstPotion == null)
                        {
                            firstPotion = playerManager.playerInventoryManager.potionsInQuickSlots[i];
                            firstPotionIndex = i;
                        }
                    }

                    if (potionCount <= 1)
                    {
                        playerManager.playerInventoryManager.potionIndex = -1;
                        selectedPotion = Instantiate(WorldItemDatabase.instance.emptyPotion);
                        playerManager.currentPotionInHandID = selectedPotion.itemID;
                    }
                    else
                    {
                        playerManager.playerInventoryManager.potionIndex = firstPotionIndex;
                        playerManager.currentPotionInHandID = firstPotion.itemID;
                    }

                }

                return;

            }

            // Get the potion from the inventory

            foreach (PotionItem potion in playerManager.playerInventoryManager.potionsInQuickSlots)
            {
                // IF the potion is not empty we proceed

                if (playerManager.playerInventoryManager.potionsInQuickSlots[playerManager.playerInventoryManager.potionIndex].itemID != WorldItemDatabase.instance.emptyPotion.itemID)
                {
                    selectedPotion = playerManager.playerInventoryManager.potionsInQuickSlots[playerManager.playerInventoryManager.potionIndex];
                    playerManager.currentPotionInHandID = selectedPotion.itemID;
                    return;
                }
            }

            if (playerManager.playerInventoryManager.potionsInQuickSlots[playerManager.playerInventoryManager.potionIndex].itemID == WorldItemDatabase.instance.emptyPotion.itemID)
            {
                SwitchPotion();
            }

        }

        #endregion

        #region Damage Colliders

        public void OpenDamageCollider()
        {
            // Open right weapon damage collider
            weaponManager.meleeWeaponDamageCollider.EnableDamageCollider();

            // Maybe whoosh sfx?

        }

        public void CloseDamageCollider()
        {
            // Close right weapon damage collider
            weaponManager.meleeWeaponDamageCollider.DisableDamageCollider();
        }

        public void RefreshInventory()
        {
            // If the player is modifing the order of items in the UI we need to refresh the weapon slots in the manager as well

           // RefreshModelSlots();

        }

        #endregion

    }
}