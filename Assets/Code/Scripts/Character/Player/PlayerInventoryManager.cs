using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class PlayerInventoryManager : CharacterInventoryManager
    {

        public static PlayerInventoryManager instance;

        [Header("Current Items")]
        public SpellItem currentSpell;
        public WeaponItem currentWeapon;
        public WeaponItem currentShield;
        public PotionItem currentPotion;

        [Header("Quick slots")]
        public SpellItem[] spellInQuickSlots = new SpellItem[3];
        public WeaponItem[] weaponsInHandSlots = new WeaponItem[3];
        public PotionItem[] potionsInQuickSlots = new PotionItem[3];
        public int spellIndex = 0;
        public int handSlotIndex = 0;
        public int potionIndex = 0;

        [Header ("Inventory")]
        public SpellItem[] spellsInInventory = new SpellItem[8];
        public WeaponItem[] weaponsInInventory = new WeaponItem[8];
        public PotionItem[] potionsInInventory = new PotionItem[8];

        protected override void Awake()
        {
            base.Awake();

            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }

        }

        public void AddSpecialSpellToQuickSlot(SpellItem SpellItem)
        {
            for(int i = 0; i < spellInQuickSlots.Length; i++)
            {
                if (spellsInInventory[i] == null)
                {
                    spellsInInventory[i] = SpellItem;
                    PlayerUIMenuManager.instance.RefreshCurrentMenu();
                    return;
                }
            }

        }

        public void AddSpellToInventory(SpellItem spellItem)
        {
            for(int i = 0; i < spellsInInventory.Length; i++)
            {
                if (spellsInInventory[i] == WorldItemDatabase.instance.nopeSpellItem)
                {
                    spellsInInventory[i] = spellItem;
                    PlayerUIMenuManager.instance.RefreshCurrentMenu();
                    return;
                }
            }

        }

        public void AddWeaponToInventory(WeaponItem weaponItem)
        {
            for(int i = 0; i < weaponsInInventory.Length; i++)
            {
                if (weaponsInInventory[i] == WorldItemDatabase.instance.unarmedWeapon)
                {
                    weaponsInInventory[i] = weaponItem;
                    PlayerUIMenuManager.instance.RefreshCurrentMenu();
                    return;
                }
            }

        }

        public void AddPotionToInventory(PotionItem potionItem)
        {
            for (int i = 0; i < potionsInInventory.Length; i++)
            {
                if (potionsInInventory[i] == null)
                {
                    potionsInInventory[i] = potionItem;
                    PlayerUIMenuManager.instance.RefreshCurrentMenu();
                    return;
                }
            }

        }

        public void SwapItemPlaces(int firstPostion, int secondPosition, Item[] firstLocation, Item[] secondLocation)
        {
            Item temp = firstLocation[firstPostion];
            firstLocation[firstPostion] = secondLocation[secondPosition];
            secondLocation[secondPosition] = temp;

            PlayerUIMenuManager.instance.RefreshCurrentMenu();
        }

    }

}