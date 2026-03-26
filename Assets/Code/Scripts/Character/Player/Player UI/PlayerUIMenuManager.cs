using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tartarus
{
    public class PlayerUIMenuManager : MonoBehaviour
    {
        public static PlayerUIMenuManager instance;
        [SerializeField] PlayerManager playerManager;
        public PlayerUIInventoryManager inventoryManager;

        [Header("Inventory Menus")]
        public PlayerUISubMenu spellsMenu;
        public PlayerUISubMenu weaponsMenu;
        public PlayerUISubMenu potionsMenu;
        public PlayerUISubMenu optionsMenu;
        public PlayerUISubMenu levelUpMenu;
        public PlayerUISubMenu controlsMenu;
        public List<Button> buttons = new List<Button>();

        public bool isMenuOpen = false;

        private void Awake()
        {
            CloseAllMenus();
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }

            spellsMenu.playerManager = playerManager;
            weaponsMenu.playerManager = playerManager;
            potionsMenu.playerManager = playerManager;
            optionsMenu.playerManager = playerManager;
            levelUpMenu.playerManager = playerManager;

        }

        private void Update()
        {
            PlayerUIManager.instance.menuWindowIsOpen = isMenuOpen;
            if(isMenuOpen == true)
                DisplayButtons();
        }


        public void OpenDefaultMenu()
        {
            CloseAllMenus();
            if (instance.isMenuOpen == false)
            {
                isMenuOpen = true;
                weaponsMenu.gameObject.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                return;
            }
            isMenuOpen = !isMenuOpen;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void OpenSpellInventory()
        {
            CloseAllMenus();
            spellsMenu.gameObject.SetActive(true);
        }

        public void OpenWeaponInventory()
        {
           CloseAllMenus();
            weaponsMenu.gameObject.SetActive(true);
        }

        public void OpenPotionInventory()
        {
            CloseAllMenus();
            potionsMenu.gameObject.SetActive(true);
        }

        public void OpenOptionsMenu()
        {
            CloseAllMenus();
            optionsMenu.gameObject.SetActive(true);
        }

        public void OpenLevelUpMenu()
        {
            CloseAllMenus();
            levelUpMenu.gameObject.SetActive(true);
        }

        public void OpenControlsMenu()
        {
            CloseAllMenus();
            controlsMenu.gameObject.SetActive(true);
        }

        public void CloseAllMenus()
        {
            HideButtons();
            spellsMenu.gameObject.SetActive(false);
            weaponsMenu.gameObject.SetActive(false);
            potionsMenu.gameObject.SetActive(false);
            optionsMenu.gameObject.SetActive(false);
            levelUpMenu.gameObject.SetActive(false);
            controlsMenu.gameObject.SetActive(false);

        }

        public void RefreshCurrentMenu()
        {

            if (spellsMenu.gameObject.activeSelf)
            {
                spellsMenu.RefreshInventory();
            }

            if (weaponsMenu.gameObject.activeSelf)
            {
                weaponsMenu.RefreshInventory();
            }

            if (potionsMenu.gameObject.activeSelf)
            {
                potionsMenu.RefreshInventory();
            }

            if (optionsMenu.gameObject.activeSelf)
            {
                optionsMenu.RefreshInventory();
            }

            if(levelUpMenu.gameObject.activeSelf)
            {
                levelUpMenu.RefreshInventory();
            }

            if(controlsMenu.gameObject.activeSelf)
            {
                controlsMenu.RefreshInventory();
            }

        }

        public void SaveGame()
        {
            WorldSaveManager.instance.SaveGame();
        }

        public void ExitToTitleMenu()
        {

            WorldSaveManager.instance.SaveGame();
            WorldSaveManager.instance.LoadTitleMenu();

        }

        public void DisplayButtons()
        {

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].gameObject.SetActive(true);
            }

        }

        public void HideButtons()
        {

            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].gameObject.SetActive(false);
            }

        }


    }
}
