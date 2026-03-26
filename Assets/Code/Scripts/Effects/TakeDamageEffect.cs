using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    [CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Damage")]
    public class TakeDamageEffect : InstantCharacterEffect
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

        [Header ("Animation")]
        public bool playDamageAnimation = true;
        public bool manuallySelectDamageAnimation = false;
        public string damageAnimation;

        [Header ("Sound effects")]
        public bool playDamageSound = true;
        public AudioClip damageSound;

        public override void ProcessEffect(CharacterManager characterManager)
        {
            if(characterManager.isInvulnerable)
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
            PlayDirectionalBasedDamageAnimation(characterManager);

            PlayDamageVFX(characterManager);

        }

        private void CalculateDamage(CharacterManager characterManager)
        {
            if (characterCausingDamage != null)
            {
                // Calculate damage based on the character causing damage
            }

            // Add flat damage ? If I add fire resistance, I can add a flat damage to the fire damage

            totalDamage = Mathf.RoundToInt(physicalDamage + magicalDamage + fireDamage + frostDamage);

            if(totalDamage <= 0)
            {
                totalDamage = 1;
            }

            characterManager.currentHealth -= totalDamage;
            characterManager.characterStatsManager.totalPoiseDamage -= poiseDamage;

            float remainingPoiseDamage = characterManager.characterStatsManager.basePoiseDefense + characterManager.characterStatsManager.offensivePoiseDamage + characterManager.characterStatsManager.totalPoiseDamage;

            if(remainingPoiseDamage <= 0)
            {
                poiseIsBroken = true;
            }

            characterManager.characterStatsManager.poiseResetTimer = characterManager.characterStatsManager.defaultPoiseResetTimer;

        }

        private void PlayDamageVFX(CharacterManager characterManager)
        {
            characterManager.characterEffectsManager.PlayBloodSplatterVFX(contactPoint);
        }

        private void PlayDirectionalBasedDamageAnimation(CharacterManager characterManager)
        {

            if(characterManager.isDead)
            {
                return;
            }

            if (poiseIsBroken)
            {
                if (angleOfDamage >= 145 && angleOfDamage <= 180)
                {
                    damageAnimation = characterManager.characterAnimationManager.hit_Forward_Medium_01;
                }
                else if (angleOfDamage <= -145 && angleOfDamage >= -180)
                {
                    damageAnimation = characterManager.characterAnimationManager.hit_Forward_Medium_01;
                }
                else if (angleOfDamage >= -45 && angleOfDamage <= 45)
                {
                    damageAnimation = characterManager.characterAnimationManager.hit_Backward_Medium_01;
                }
                else if (angleOfDamage >= -144 && angleOfDamage <= 45)
                {
                    damageAnimation = characterManager.characterAnimationManager.hit_Left_Medium_01;
                }
                else if (angleOfDamage >= 45 && angleOfDamage <= 144)
                {
                    damageAnimation = characterManager.characterAnimationManager.hit_Right_Medium_01;
                }
            }
            else
            {
                if(angleOfDamage >= 145 && angleOfDamage <= 180)
                {
                    damageAnimation = characterManager.characterAnimationManager.hit_Forward_01;
                }
                else if(angleOfDamage <= -145 && angleOfDamage >= -180)
                {
                    damageAnimation = characterManager.characterAnimationManager.hit_Forward_01;
                }
                else if(angleOfDamage >= -45 && angleOfDamage <= 45)
                {
                    damageAnimation = characterManager.characterAnimationManager.hit_Backward_01;
                }
                else if(angleOfDamage >= -144 && angleOfDamage <= 45)
                {
                    damageAnimation = characterManager.characterAnimationManager.hit_Left_01;
                }
                else if(angleOfDamage >= 45 && angleOfDamage <= 144)
                {
                    damageAnimation = characterManager.characterAnimationManager.hit_Right_01;
                }
            }
           

            if(poiseIsBroken)
            {
                characterManager.characterAnimationManager.PlayTargetAnimation(damageAnimation, true);
            }
            else
            {
                characterManager.characterAnimationManager.PlayTargetAnimation(damageAnimation, false, false, true, true);
            }

        }

    }
}
