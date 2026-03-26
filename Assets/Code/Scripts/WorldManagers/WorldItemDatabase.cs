using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tartarus
{
    public class WorldItemDatabase : MonoBehaviour
    {
        public static WorldItemDatabase instance;

        public SpellItem nopeSpellItem;
        public WeaponItem unarmedWeapon;
        public PotionItem emptyPotion;

        public WeaponItem defaultWeaponItem;
        public PotionItem healthPotionItem;
        public PotionItem focusPointsPotionItem;

        // Used to generate unique IDs for items
        [Header("Spells")]
        [SerializeField] List<SpellItem> spells = new List<SpellItem>();
        [Header("Weapons")]
        [SerializeField] List<WeaponItem> weapons = new List<WeaponItem>();
        [Header("Potions")]
        [SerializeField] List<PotionItem> potions = new List<PotionItem>();

        // List of all items in the game
        private List<Item> items = new List<Item>();
        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            foreach (var spell in spells)
            {
                items.Add(spell);
            }

            foreach (var weapon in weapons)
            {
                items.Add(weapon);
            }

            foreach (var potion in potions)
            {
                items.Add(potion);
            }

            // Assign unique IDs to items
            for(int i = 0; i < items.Count; i++)
            {
                items[i].itemID = i;
            }

        }

        public SpellItem GetSpellItem(int id)
        {
            return spells.FirstOrDefault(specialAttack => specialAttack.itemID == id);
        }

        public WeaponItem GetWeaponItem(int id)
        {
            return weapons.FirstOrDefault(weapon => weapon.itemID == id);
        }

        public PotionItem GetPotionItem(int id)
        {
            return potions.FirstOrDefault(potion => potion.itemID == id);
        }

    }
}