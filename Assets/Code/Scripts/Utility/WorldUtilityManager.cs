using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class WorldUtilityManager : MonoBehaviour
    {
        public static WorldUtilityManager instance;

        [Header ("Layers")]
        [SerializeField] LayerMask characterLayers;
        [SerializeField] LayerMask enviromentalLayers;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        public LayerMask GetCharacterLayers()
        {
            return characterLayers;
        }

        public LayerMask GetEnviromentalLayers()
        {
            return enviromentalLayers;
        }

        public bool CanIDamageThisTarget(CharacterGroup attackingCharacter, CharacterGroup targetCharacter)
        {

            if(attackingCharacter == CharacterGroup.UndeadWarrior)
            {
                switch (targetCharacter)
                {
                    case CharacterGroup.UndeadWarrior:
                        return false;
                    case CharacterGroup.CursedAbomination:
                        return true;
                    case CharacterGroup.Witcher:
                        return true;
                    case CharacterGroup.DeathKnight:
                        return true;
                    case CharacterGroup.Player:
                        return true;
                }
            }

            if(attackingCharacter == CharacterGroup.CursedAbomination)
            {
                switch (targetCharacter)
                {
                    case CharacterGroup.UndeadWarrior:
                        return true;
                    case CharacterGroup.CursedAbomination:
                        return false;
                    case CharacterGroup.Witcher:
                        return true;
                    case CharacterGroup.DeathKnight:
                        return true;
                    case CharacterGroup.Player:
                        return true;
                }
            }

            if(attackingCharacter == CharacterGroup.Witcher)
            {
                switch (targetCharacter)
                {
                    case CharacterGroup.UndeadWarrior:
                        return true;
                    case CharacterGroup.CursedAbomination:
                        return true;
                    case CharacterGroup.Witcher:
                        return false;
                    case CharacterGroup.DeathKnight:
                        return true;
                    case CharacterGroup.Player:
                        return true;
                }
            }

            if(attackingCharacter == CharacterGroup.DeathKnight)
            {
                switch (targetCharacter)
                {
                    case CharacterGroup.UndeadWarrior:
                        return true;
                    case CharacterGroup.CursedAbomination:
                        return true;
                    case CharacterGroup.Witcher:
                        return true;
                    case CharacterGroup.DeathKnight:
                        return false;
                    case CharacterGroup.Player:
                        return true;
                }
            }

            if(attackingCharacter == CharacterGroup.Player)
            {
                return true;
            }

            return false;

        }

        public float GetAngleOfTarget(Transform characterTransform, Vector3 targetDirection)
        {
            targetDirection.y = 0;
            float viewableAngle = Vector3.Angle(characterTransform.forward, targetDirection);
            Vector3 cross = Vector3.Cross(characterTransform.forward, targetDirection);

            if(cross.y < 0)
            {
                viewableAngle = -viewableAngle;
            }

            return viewableAngle;
        }

        public DamageIntensity GetDamageIntensityBasedOnPoiseDamage(float poiseDamage)
        {
            DamageIntensity damageIntensity = DamageIntensity.Tiny;

            if(poiseDamage >= 10)
                damageIntensity = DamageIntensity.Light;

            if(poiseDamage >= 30)
                damageIntensity = DamageIntensity.Medium;

            if(poiseDamage >= 70)
                damageIntensity = DamageIntensity.Heavy;

            if(poiseDamage >= 110)
                damageIntensity = DamageIntensity.Collosal;

            return damageIntensity;
        }

        public SpellItem[] GetSpellItemArrayFromInventory(int type)
        {
            switch(type)
            {
                case 0:
                    return PlayerInventoryManager.instance.spellInQuickSlots;
                case 1:
                    return PlayerInventoryManager.instance.spellInQuickSlots;
                case 2:
                    return PlayerInventoryManager.instance.spellsInInventory;
                default:
                    return null;
            }
        }

        public WeaponItem[] GetWeaponItemArrayFromInventory(int type)
        {
            switch(type)
            {
                case 0:
                    return PlayerInventoryManager.instance.weaponsInHandSlots;
                case 1:
                    return PlayerInventoryManager.instance.weaponsInHandSlots;
                case 2:
                    return PlayerInventoryManager.instance.weaponsInInventory;
                default:
                    return null;
            }
        }

        public PotionItem[] GetPotionItemArrayFromInventory(int type)
        {
            switch(type)
            {
                case 0:
                    return PlayerInventoryManager.instance.potionsInQuickSlots;
                case 1:
                    return PlayerInventoryManager.instance.potionsInQuickSlots;
                case 2:
                    return PlayerInventoryManager.instance.potionsInInventory;
                default:
                    return null;
            }
        }

    }
}