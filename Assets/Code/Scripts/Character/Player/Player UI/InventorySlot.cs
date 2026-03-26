using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

namespace Tartarus
{
    public class InventorySlot : MonoBehaviour, IDropHandler
    {
        public int ID;
        public int type;
        public Image itemImage;

        public bool potionSwitch = false;
        public InventorySlot otherPotionSlot;
        public PotionItem potionItem;
        public TextMeshProUGUI itemQuantityText;
        public TextMeshProUGUI otherItemQuantityText;
        private PotionItem otherPotionItem;

        private void OnEnable()
        {
            if (!potionSwitch)
                return;
            itemImage.sprite = potionItem.itemIcon;
            itemQuantityText.text = potionItem.maxNumberOfUses.ToString();
        }

        public void OnDrop(PointerEventData eventData)
        {


            GameObject droppedItem = eventData.pointerDrag;
            DraggableItem draggableItem = droppedItem.GetComponent<DraggableItem>();

            if (potionSwitch == true)
            {
                return;

            }
            else
            {
                if (draggableItem != null)
                {
                    Item[] thisArray = null;
                    Item[] draggableArray = null;

                    if (PlayerUIMenuManager.instance.spellsMenu.gameObject.activeSelf)
                    {
                        thisArray = WorldUtilityManager.instance.GetSpellItemArrayFromInventory(type);
                        draggableArray = WorldUtilityManager.instance.GetSpellItemArrayFromInventory(draggableItem.type);
                    }

                    if (PlayerUIMenuManager.instance.weaponsMenu.gameObject.activeSelf)
                    {

                        thisArray = WorldUtilityManager.instance.GetWeaponItemArrayFromInventory(type);
                        draggableArray = WorldUtilityManager.instance.GetWeaponItemArrayFromInventory(draggableItem.type);
                    }

                    if (PlayerUIMenuManager.instance.potionsMenu.gameObject.activeSelf)
                    {
                        thisArray = WorldUtilityManager.instance.GetPotionItemArrayFromInventory(type);
                        draggableArray = WorldUtilityManager.instance.GetPotionItemArrayFromInventory(draggableItem.type);
                    }

                    PlayerInventoryManager.instance.SwapItemPlaces(ID, draggableItem.ID, thisArray, draggableArray);

                    droppedItem.transform.SetParent(draggableItem.parentTransform);
                    draggableItem.canvasGroup.blocksRaycasts = true;
                }

            }    

        }

        public void FirstPotionAdd()
        {
            otherPotionItem = otherPotionSlot.potionItem;
            if (potionItem != null && otherPotionItem.maxNumberOfUses > 0)
            {
                potionItem.maxNumberOfUses++;
                otherPotionItem.maxNumberOfUses--;
                potionItem.numberOfUses = potionItem.maxNumberOfUses;   
                otherPotionItem.numberOfUses = otherPotionItem.maxNumberOfUses;
                itemQuantityText.text = potionItem.maxNumberOfUses.ToString();
                otherItemQuantityText.text = otherPotionItem.maxNumberOfUses.ToString();
            }

        }

        public void SecondPotionAdd()
        {
            otherPotionItem = otherPotionSlot.potionItem;
            if (otherPotionItem != null && potionItem.maxNumberOfUses > 0)
            {
                potionItem.maxNumberOfUses--;
                otherPotionItem.maxNumberOfUses++;
                potionItem.numberOfUses = potionItem.maxNumberOfUses;
                otherPotionItem.numberOfUses = otherPotionItem.maxNumberOfUses;
                itemQuantityText.text = potionItem.maxNumberOfUses.ToString();
                otherItemQuantityText.text = otherPotionItem.maxNumberOfUses.ToString();
            }
        }

    }
}