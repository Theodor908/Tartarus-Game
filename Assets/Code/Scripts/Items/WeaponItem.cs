using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Tartarus
{
    public class WeaponItem : Item
    {
        
        [Header ("Animations")]
        public AnimatorOverrideController weaponAnimatorOverride;

        [Header ("Weapon model")]
        public GameObject weaponModel;
        public GameObject throwableWeaponModel;
        public GameObject bowHandle;
        public GameObject bowString;
        public bool hasShield;
        public bool isRangedType;
        public bool isBowType;

        [Header("Weapon requirements")]
        public int strengthRequirement = 0;
        public int dexterityRequirement = 0;

        [Header("Weapon base damage")]
        public int physicalDamage = 0;
        public int frostDamage = 0;

        [Header ("Weapon Absorbtions")]
        public float physicalAbsorbtion = 0.0f;
        public float frostAbsorbtion = 0.0f;

        // Weapon guard break (blocking system)

        [Header("Weapon poise")]
        public float poiseDamage = 10;

        [Header ("Attack modifiers")]
        [Header ("Melee")]
        [Header ("Light Attacks Modifiers")]
        public float light_Attack_01_Modifier = 1.0f;
        public float light_Attack_02_Modifier = 1.2f;
        public float light_Attack_03_Modifier = 1.4f;
        public float light_Attack_04_Modifier = 1.6f;
        [Header ("Heavy Attacks Modifiers")]
        public float heavy_Attack_01_Modifier = 1.4f;
        public float heavy_Attack_02_Modifier = 1.6f;
        public float heavy_Attack_03_Modifier = 1.8f;
        [Header ("Charged Heavy Attacks Modifiers")]
        public float charged_Heavy_Attack_01_Modifier = 2.0f;
        public float charged_Heavy_Attack_02_Modifier = 2.2f;
        [Header ("Running Attacks Modifiers")]
        public float running_Attack_01_Modifier = 1.2f;
        [Header ("Rolling Attacks Modifiers")]
        public float rolling_Attack_01_Modifier = 1.2f;
        [Header ("Backstep Attacks Modifiers")]
        public float backstep_Attack_01_Modifier = 1.2f;
        [Header ("Ranged")]
        public float projectile_Attack_Modifier = 1.0f;
        // Light attack
        // Heavy attack
        // Critical attack

        [Header("Stamina cost modifiers")]
        public int baseStaminaCost = 20;
        public float lightAttackStaminaCostMultiplier = 0.9f;
        public float heavyAttackStaminaCostMultiplier = 1.5f;
        public float chargedHeavyAttackStaminaCostMultiplier = 2.0f;
        public float runningAttackStaminaCostMultiplier = 1.2f;
        public float rollingAttackStaminaCostMultiplier = 1.2f;
        public float backstepAttackStaminaCostMultiplier = 1.2f;
        // Running attack stamina cost
        // Light attack stamina cost
        // Heavy attack stamina cost

        // Weapon deflection (melee) - bounce off shields

        // Item based actions
        [Header("Weapon actions")]
        public WeaponItemAction RMB_Action;
        public WeaponItemAction LMB_Action;
        public WeaponItemAction LBMB_Action;

       
    }
}