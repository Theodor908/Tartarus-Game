using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    [CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Blocked Damage")]
    public class TakeBlockedDamageEffect : InstantCharacterEffect
    {

        [Header("Character causing damage")]
        public CharacterManager characterCausingDamage; //Take the associated damage from this character

        [Header("Damage")]
        public float physicalDamage = 0; // Standard, strike, slash, pierce
        public float magicalDamage = 0;
        public float fireDamage = 0;
        public float frostDamage = 0;

        [Header("Direction of damage taken")]
        public float angleOfDamage = 0; // For choosing animations
        public Vector3 contactPoint;

        [Header("Final damage")]
        [SerializeField] int totalDamage = 0;

        [Header("Poise")]
        public float poiseDamage = 0;
        public bool poiseIsBroken = false;

        [Header("Animation")]
        public bool playDamageAnimation = true;
        public bool manuallySelectDamageAnimation = false;
        public string damageAnimation;

        [Header("Sound effects")]
        public bool playDamageSound = true;
        public AudioClip damageSound;

        public override void ProcessEffect(CharacterManager characterManager)
        {
            if (characterManager.isInvulnerable)
            {
                return;
            }

            base.ProcessEffect(characterManager);

            // Character is dead
            if (characterManager.isDead)
            {
                return;
            }

            // Is invulnerable

            if (characterManager.isInvulnerable)
            {
                return;
            }

            CalculateDamage(characterManager);
            PlayDirectionalBasedBlockDamageAnimation(characterManager);

            PlayDamageVFX(characterManager);

        }

        private void CalculateDamage(CharacterManager characterManager)
        {
            if (characterCausingDamage != null)
            {
                // Calculate damage based on the character causing damage
            }

            // Add flat damage ? If I add fire resistance, I can add a flat damage to the fire damage

            
            physicalDamage -= (physicalDamage * (characterManager.characterStatsManager.physicalAbsorbtion / 100));
            fireDamage -= (fireDamage * (characterManager.characterStatsManager.fireAbsorbtion / 100));
            frostDamage -= (frostDamage * (characterManager.characterStatsManager.frostAbsorbtion / 100));

            totalDamage = Mathf.RoundToInt(physicalDamage + magicalDamage + fireDamage + frostDamage);

            if (totalDamage <= 0)
            {
                totalDamage = 1;
            }

            characterManager.currentHealth -= totalDamage;
            characterManager.characterStatsManager.totalPoiseDamage -= poiseDamage;

            float remainingPoiseDamage = characterManager.characterStatsManager.basePoiseDefense + characterManager.characterStatsManager.offensivePoiseDamage + characterManager.characterStatsManager.totalPoiseDamage;


            if (remainingPoiseDamage <= 0)
            {
                poiseIsBroken = true;
            }

            characterManager.characterStatsManager.poiseResetTimer = characterManager.characterStatsManager.defaultPoiseResetTimer;

        }

        private void PlayDamageVFX(CharacterManager characterManager)
        {
            // Block SFX
            // Block VFX
        }

        private void PlayDirectionalBasedBlockDamageAnimation(CharacterManager characterManager)
        {

            if (characterManager.isDead)
            {
                return;
            }

            DamageIntensity damageIntensity = WorldUtilityManager.instance.GetDamageIntensityBasedOnPoiseDamage(poiseDamage);

            switch (damageIntensity)
            {
                case DamageIntensity.Tiny:
                    damageAnimation = "BlockTinyDamage";
                    break;
                case DamageIntensity.Light:
                    damageAnimation = "BlockLightDamage";
                    break;
                case DamageIntensity.Medium:
                    damageAnimation = "BlockMediumDamage";
                    break;
                case DamageIntensity.Heavy:
                    damageAnimation = "BlockHeavyDamage";
                    break;
                case DamageIntensity.Collosal:
                    damageAnimation = "BlockCollosalDamage";
                    break;
                default:
                    break;
            }

            characterManager.characterAnimationManager.PlayTargetAnimation(damageAnimation, true);

        }


    }
}
