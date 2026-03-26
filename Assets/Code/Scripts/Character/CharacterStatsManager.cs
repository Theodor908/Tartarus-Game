using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class CharacterStatsManager : MonoBehaviour
    {

        CharacterManager characterManager;

        [Header ("Character Essence")]
        public int essence = 0;

        [Header("Stats regeneration")]
        private float staminaRegenTimer = 0f;
        private float staminaTickTimer = 0f;
        private float poiseRegenTimer = 0f;
        [Header("Stamina")]
        public float staminaRegenDelay = 3f;
        public float staminaRegenRate = 1f;

        [Header("Blocking Absorbtions")]
        public float physicalAbsorbtion;
        public float fireAbsorbtion;
        public float frostAbsorbtion;

        [Header ("Poise")]
        public float totalPoiseDamage; // this resets
        public float offensivePoiseDamage; // some weapons give additional poise damage during attacks
        public float basePoiseDefense; // Armor and shields give poise defense
        public float defaultPoiseResetTimer = 8f; // 8 seconds until poise resets
        public float poiseResetTimer = 0f;

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {
            characterManager.maxHealth = CalculateHealthBasedOnVitalityLevel(characterManager.vitality);
            characterManager.maxStamina = CalculateStaminaBasedOnEnduranceLevel(characterManager.endurance);
        }

        protected virtual void Update()
        {
            HandlePoiseResetTimer();
        }

        protected virtual int CalculateHealthBasedOnVitalityLevel(int vitality)
        {

            float health = 100;
            health += (vitality - 1) * 10;

            return Mathf.RoundToInt(health);

        }

        protected virtual int CalculateStaminaBasedOnEnduranceLevel(int endurance)
        {

            float stamina = 100;
            stamina += (endurance - 1) * 10;

            return Mathf.RoundToInt(stamina);

        }

        protected virtual int CalculateFocusPointsBasedOnAttunementLevel(int attunement)
        {

            float focusPoints = 100;
            focusPoints += (attunement - 1) * 10;

            return Mathf.RoundToInt(focusPoints);

        }

        public virtual void RegenerateStamina()
        {
            if(characterManager.isSprinting || characterManager.isInteracting || characterManager.characterLocomotionManager.isJumping)
            {
                ResetStaminaRegenTimer();
                return;
            }

            staminaRegenTimer += Time.deltaTime;

            if (staminaRegenTimer > staminaRegenDelay)
            {
                if (characterManager.currentStamina < characterManager.maxStamina)
                {
                    staminaTickTimer += Time.deltaTime;
                    if (staminaTickTimer > 0.1f)
                    {
                        staminaTickTimer = 0f;
                        characterManager.currentStamina += staminaRegenRate;
                        PlayerUIManager.instance.playerUIHudManager.setNewStaminaValue(characterManager.currentStamina);
                    }
                }

            }

        }

        public virtual void ResetStaminaRegenTimer()
        {
            staminaRegenTimer = 0f;
        }

        protected virtual void HandlePoiseResetTimer()
        {
            if(poiseResetTimer > 0)
            {
                poiseResetTimer -= Time.deltaTime;
            }
            else
            {
                totalPoiseDamage = 0;
            }
        }

    }

}
