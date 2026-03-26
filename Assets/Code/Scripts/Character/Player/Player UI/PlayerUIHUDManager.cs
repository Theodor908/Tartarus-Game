using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tartarus
{
    public class PlayerUIHUDManager : MonoBehaviour
    {

        [Header ("Stat bars")]
        [SerializeField] UI_StatusBar healthBar;
        public UI_StatusBar focusPointsBar;
        [SerializeField] UI_StatusBar staminaBar;

        [Header ("Essence")]
        [SerializeField] TextMeshProUGUI essenceText;

        [Header("Quick slots")]
        [SerializeField] Image specialAttackSlotIcon;
        [SerializeField] Image weaponQuickSlotIcon;
        [SerializeField] Image potionQuickSlotIcon;

        [Header("Boss Health Bar")]
        public Transform bossHealthBarParent;
        public GameObject bossHealthBarObject;

        public void RefreshHud()
        {
            healthBar.gameObject.SetActive(false);
            healthBar.gameObject.SetActive(true);

            focusPointsBar.gameObject.SetActive(false);
            focusPointsBar.gameObject.SetActive(true);

            staminaBar.gameObject.SetActive(false);
            staminaBar.gameObject.SetActive(true);
        }

        public void setNewHealthValue(float value)
        {
            healthBar.SetStatus(Mathf.RoundToInt(value));
        }

        public void setMaxHealthValue(float value)
        {
            healthBar.SetMaxStatus(value);
            healthBar.SetStatus(Mathf.RoundToInt(value));
        }

        public void setNewFocusPointsValue(float value)
        {
            focusPointsBar.SetStatus(Mathf.RoundToInt(value));
        }

        public void setMaxFocusPointsValue(float value)
        {
            focusPointsBar.SetMaxStatus(value);
            focusPointsBar.SetStatus(Mathf.RoundToInt(value));
        }

        public void setNewStaminaValue(float value)
        {
            staminaBar.SetStatus(Mathf.RoundToInt(value));
        }

        public void setMaxStaminaValue(float value)
        {
            staminaBar.SetMaxStatus(value);
            staminaBar.SetStatus(Mathf.RoundToInt(value));
        }

        public void setNewEssenceValue(int value)
        {
            essenceText.text = value.ToString();
        }

        public void setSpellSlotIcon(int specialAttackID)
        { 

            SpellItem SpellItem = WorldItemDatabase.instance.GetSpellItem(specialAttackID);

            if(SpellItem == null)
            {
                specialAttackSlotIcon.gameObject.SetActive(false);
                specialAttackSlotIcon.sprite = null;
                return;
            }

            if(SpellItem.itemIcon == null)
            {
                specialAttackSlotIcon.gameObject.SetActive(false);
                specialAttackSlotIcon.sprite = null;
                return;
            }

            specialAttackSlotIcon.gameObject.SetActive(true);
            specialAttackSlotIcon.sprite = SpellItem.itemIcon;

        }

        public void setWeaponQuickSlotIcon(int weaponID)
        {
            WeaponItem weaponItem = WorldItemDatabase.instance.GetWeaponItem(weaponID);

            if(weaponItem == null)
            {
                weaponQuickSlotIcon.gameObject.SetActive(false);
                weaponQuickSlotIcon.sprite = null;
                return;
            }
            
            if(weaponItem.itemIcon == null)
            {
                weaponQuickSlotIcon.gameObject.SetActive(false);
                weaponQuickSlotIcon.sprite = null;
                return;
            }
            weaponQuickSlotIcon.gameObject.SetActive(true);
            weaponQuickSlotIcon.sprite = weaponItem.itemIcon;

        }
        public void setPotionQuickSlotIcon(int potionID)
        {
            PotionItem potionItem = WorldItemDatabase.instance.GetPotionItem(potionID);

            if(potionItem == null)
            {
                potionQuickSlotIcon.gameObject.SetActive(false);
                potionQuickSlotIcon.sprite = null;
                return;
            }

            if(potionItem.itemIcon == null)
            {
                potionQuickSlotIcon.gameObject.SetActive(false);
                potionQuickSlotIcon.sprite = null;
                return;
            }

            potionQuickSlotIcon.gameObject.SetActive(true);
            potionQuickSlotIcon.sprite = potionItem.itemIcon;

        }
    }
}
