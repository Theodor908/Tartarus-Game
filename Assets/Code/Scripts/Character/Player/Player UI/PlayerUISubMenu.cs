using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class PlayerUISubMenu : MonoBehaviour
    {

        [Header("Player Inventory and Equipment")]
        public PlayerManager playerManager;
        [Header ("Inventory Type")]
        public bool isWeaponIventory = false;
        public bool isSpellInventory = false;
        public bool isPotionInventory = false;

        [Header ("PotionExchange")]
        public bool canUsePotionExchange = false;
        [SerializeField] GameObject potionExchangePanel;

        [Header ("LevelUp")]
        public bool canUseLevelUp = false;
        [SerializeField] GameObject levelUpPanel;

        [Header("Inventory Slots")]
        public InventorySlot[] quickSlots = new InventorySlot[3];
        public InventorySlot[] inventorySlots = new InventorySlot[8];


        private void OnEnable()
        {
            RefreshInventory();

            if(potionExchangePanel == null)
                return;

            if(canUsePotionExchange)
            {
                potionExchangePanel.SetActive(true);
            }
            else
            {
                potionExchangePanel.SetActive(false);
            }

        }

        public void ShowLevelUpMenu()
        {
            levelUpPanel.SetActive(true);
        }

        public void CloseLevelUpMenu()
        {
            levelUpPanel.SetActive(false);
        }

        public void RefreshInventory()
        {

            if(isSpellInventory)
            {
                RefreshSpellInventory();
                playerManager.playerEquipmentManager.RefreshSpellModelSlots();
            }

            if(isWeaponIventory)
            {
                RefreshWeaponInventory();
                playerManager.playerEquipmentManager.RefreshWeaponModelSlots();
            }

            if(isPotionInventory)
            {
                RefreshPotionInventory();
                playerManager.playerEquipmentManager.RefreshPotionModelSlots();
            }

        }

        public void RefreshSpellInventory()
        {

            // Quick slots

            for (int i = 0; i < quickSlots.Length; i++)
            {

                if (quickSlots[i].itemImage.gameObject.activeSelf)
                {
                    quickSlots[i].itemImage.gameObject.SetActive(false);
                }

                if (playerManager.playerInventoryManager.spellInQuickSlots[i] != WorldItemDatabase.instance.nopeSpellItem)
                {
                    quickSlots[i].itemImage.gameObject.SetActive(true);
                    quickSlots[i].itemImage.sprite = playerManager.playerInventoryManager.spellInQuickSlots[i].itemIcon;
                }

            }

            // Inventory slots

            for (int i = 0; i < inventorySlots.Length; i++)
            {

                if (inventorySlots[i].itemImage.gameObject.activeSelf)
                {
                    inventorySlots[i].itemImage.gameObject.SetActive(false);
                }

                if (playerManager.playerInventoryManager.spellsInInventory[i] != WorldItemDatabase.instance.nopeSpellItem)
                {
                    inventorySlots[i].itemImage.gameObject.SetActive(true);
                    inventorySlots[i].itemImage.sprite = playerManager.playerInventoryManager.spellsInInventory[i].itemIcon;
                }

            }

        }

        public void RefreshWeaponInventory()
        {

            // Quick slots

            for (int i = 0; i < quickSlots.Length; i++)
            {

                if (quickSlots[i].itemImage.gameObject.activeSelf)
                {
                    quickSlots[i].itemImage.gameObject.SetActive(false);
                }

                if (playerManager.playerInventoryManager.weaponsInHandSlots[i] != WorldItemDatabase.instance.unarmedWeapon)
                {
                    quickSlots[i].itemImage.gameObject.SetActive(true);
                    quickSlots[i].itemImage.sprite = playerManager.playerInventoryManager.weaponsInHandSlots[i].itemIcon;
                }

            }         

            // Inventory slots

            for (int i = 0; i < inventorySlots.Length; i++)
            {

                if (inventorySlots[i].itemImage.gameObject.activeSelf)
                {
                    inventorySlots[i].itemImage.gameObject.SetActive(false);
                }

                if (playerManager.playerInventoryManager.weaponsInInventory[i] != WorldItemDatabase.instance.unarmedWeapon)
                {
                    inventorySlots[i].itemImage.gameObject.SetActive(true);
                    inventorySlots[i].itemImage.sprite = playerManager.playerInventoryManager.weaponsInInventory[i].itemIcon;
                }

            }

        }

        public void RefreshPotionInventory()
        {

            // Quick slots

            for (int i = 0; i < quickSlots.Length; i++)
            {

                if (quickSlots[i].itemImage.gameObject.activeSelf)
                {
                    quickSlots[i].itemImage.gameObject.SetActive(false);
                }

                if (playerManager.playerInventoryManager.potionsInQuickSlots[i] != WorldItemDatabase.instance.emptyPotion)  
                {
                    quickSlots[i].itemImage.gameObject.SetActive(true);
                    quickSlots[i].itemImage.sprite = playerManager.playerInventoryManager.potionsInQuickSlots[i].itemIcon;
                }

            }

            // Inventory slots

            for (int i = 0; i < inventorySlots.Length; i++)
            {

                if (inventorySlots[i].itemImage.gameObject.activeSelf)
                {
                    inventorySlots[i].itemImage.gameObject.SetActive(false);
                }

                if (playerManager.playerInventoryManager.potionsInInventory[i] != WorldItemDatabase.instance.emptyPotion)
                {
                    inventorySlots[i].itemImage.gameObject.SetActive(true);
                    inventorySlots[i].itemImage.sprite = playerManager.playerInventoryManager.potionsInInventory[i].itemIcon;
                }

            }

        }

        public void OpenPotionExchangePanel()
        {
            potionExchangePanel.SetActive(true);
        }

        public void ClosePotionExchangePanel()
        {
            potionExchangePanel.SetActive(false);
        }

    }

}

