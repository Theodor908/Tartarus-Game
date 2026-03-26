using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{

    [System.Serializable]
    // Reference of data
    public class CharacterSaveData
    {
        [Header ("Scene Index")]
        public int sceneIndex;

        [Header("Character Name")]
        public string characterName = "Blank";

        [Header ("Character Essence")]
        public int characterEssence = 0;

        [Header("Total time played")]
        public float secondsPlayed;

        // Json needs basic data types
        [Header("World Position")]
        public float xPosition = 160;
        public float yPosition = 30;
        public float zPosition = 102;

        [Header ("Last Respawnpoint")]
        public Transform lastRespawnPoint;

        [Header("Resources")]
        public float currentHealth = 100;
        public float currentFocusPoints = 100;
        public float currentStamina = 100;

        [Header("Character Stats")]
        public int vitality = 1;
        public int endurance = 1;
        public int attunement = 1;


        [Header("Inventory")]
        [Header("Spells")]
        public SpellItem currentSpell;
        public SpellItem[] spellQuickSlots = new SpellItem[3];
        public SpellItem[] spellsInInventory = new SpellItem[8];
        [Header ("Weapons")]
        public WeaponItem currentWeapon;
        public WeaponItem[] handQuickSlots = new WeaponItem[3];
        public WeaponItem[] weaponsInInventory = new WeaponItem[8];
        [Header("Potions")]
        public PotionItem currentPotion;
        public PotionItem[] potionQuickSlots = new PotionItem[3];
        public PotionItem[] potionsInInventory = new PotionItem[8];

        [Header("Sites Of Grace")]
        public SerializableDictionary<int, bool> sitesOfGrace;

        [Header("Bosses")]
        public SerializableDictionary<int, bool> bossesAwakened;
        public SerializableDictionary<int, bool> bossesDefeated;

        public CharacterSaveData()
        {

            sitesOfGrace = new SerializableDictionary<int, bool>();

            bossesAwakened = new SerializableDictionary<int, bool>();
            bossesDefeated = new SerializableDictionary<int, bool>();

        }

    }
}
