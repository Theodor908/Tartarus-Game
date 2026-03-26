using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tartarus
{
    public class PlayerUIInventoryManager : MonoBehaviour
    {

        public static PlayerUIInventoryManager instance;
        public PlayerUIMenuManager menuManager;
        public GameObject inventoryMenu;

        [Header("Inventory")]
        public PlayerInventoryManager inventoryManager;


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void RefreshCurrentInventory()
        {

           

        }
     

    }



}
