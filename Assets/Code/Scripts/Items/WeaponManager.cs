using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class WeaponManager : MonoBehaviour
    {
        public MeleeWeaponDamageCollider meleeWeaponDamageCollider;
        public RangedWeaponDamageCollider rangedWeaponDamageCollider;
        public bool ranged;
       
        public void Awake()
        {
            if(!ranged) // If it is a melee or throwable weapon then set the melee weapon damage collider on the weapon 
                meleeWeaponDamageCollider = GetComponentInChildren<MeleeWeaponDamageCollider>();
        }

        public void SetMeleeWeaponDamage(CharacterManager characterWieldingWeapon, WeaponItem weapon)
        {
            // Light Attack
            meleeWeaponDamageCollider.characterCausingDamage = characterWieldingWeapon;

            meleeWeaponDamageCollider.physicalDamage = weapon.physicalDamage;
            meleeWeaponDamageCollider.frostDamage = weapon.frostDamage;
            meleeWeaponDamageCollider.poiseDamage = weapon.poiseDamage;

            meleeWeaponDamageCollider.light_Attack_01_Modifier = weapon.light_Attack_01_Modifier;
            meleeWeaponDamageCollider.light_Attack_02_Modifier = weapon.light_Attack_02_Modifier;
            meleeWeaponDamageCollider.light_Attack_03_Modifier = weapon.light_Attack_03_Modifier;
            meleeWeaponDamageCollider.light_Attack_04_Modifier = weapon.light_Attack_04_Modifier;
            // Heavy Attack
            meleeWeaponDamageCollider.heavy_Attack_01_Modifier = weapon.heavy_Attack_01_Modifier;
            meleeWeaponDamageCollider.heavy_Attack_02_Modifier = weapon.heavy_Attack_02_Modifier;
            meleeWeaponDamageCollider.heavy_Attack_03_Modifier = weapon.heavy_Attack_03_Modifier;
            // Charged Heavy Attack
            meleeWeaponDamageCollider.charged_Heavy_Attack_01_Modifier = weapon.charged_Heavy_Attack_01_Modifier;
            meleeWeaponDamageCollider.charged_Heavy_Attack_02_Modifier = weapon.charged_Heavy_Attack_02_Modifier;
            // Running Attack
            meleeWeaponDamageCollider.running_Attack_01_Modifier = weapon.running_Attack_01_Modifier;
            // Rolling Attack
            meleeWeaponDamageCollider.rolling_Attack_01_Modifier = weapon.rolling_Attack_01_Modifier;
            // Backstep Attack
            meleeWeaponDamageCollider.backstep_Attack_01_Modifier = weapon.backstep_Attack_01_Modifier;
        }

        public void SetRangedWeaponDamage(CharacterManager characterWieldingWeapon, WeaponItem weapon)
        {

            rangedWeaponDamageCollider.characterCausingDamage = characterWieldingWeapon;

            rangedWeaponDamageCollider.physicalDamage = weapon.physicalDamage;
            rangedWeaponDamageCollider.poiseDamage = weapon.poiseDamage;
                
            rangedWeaponDamageCollider.projectile_Attack_Modifier = weapon.projectile_Attack_Modifier;

        }

    }
}