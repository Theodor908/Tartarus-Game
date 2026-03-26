using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class PlayerStatsManager : CharacterStatsManager
    {
        
        public static PlayerStatsManager instance;
        public PlayerManager playerManager;

        protected override void Awake()
        {

            base.Awake();

            playerManager = GetComponent<PlayerManager>();

            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        protected override void Start()
        {
            playerManager.maxHealth = CalculateHealthBasedOnVitalityLevel(playerManager.vitality);
            playerManager.maxStamina = CalculateStaminaBasedOnEnduranceLevel(playerManager.endurance);
            playerManager.maxFocusPoints = CalculateFocusPointsBasedOnAttunementLevel(playerManager.attunement);
        }

        protected override void Update()
        {
            base.Update();
            RegenerateStamina();
        }


        public void LevelUpVitality(int level)
        {

            playerManager.vitality += level;
            playerManager.maxHealth = CalculateHealthBasedOnVitalityLevel(playerManager.vitality);
            playerManager.currentHealth = playerManager.maxHealth;
            PlayerUIManager.instance.playerUIHudManager.setMaxHealthValue(playerManager.maxHealth);
            PlayerUIManager.instance.playerUIHudManager.setNewHealthValue(playerManager.currentHealth);

        }

        public void LevelUpEndurance(int level)
        {

            playerManager.endurance += level;
            playerManager.maxStamina = CalculateStaminaBasedOnEnduranceLevel(playerManager.endurance);
            playerManager.currentStamina = playerManager.maxStamina;
            PlayerUIManager.instance.playerUIHudManager.setMaxStaminaValue(playerManager.maxStamina);
            PlayerUIManager.instance.playerUIHudManager.setNewStaminaValue(playerManager.currentStamina);

        }

        public void LevelUpAttunement(int level)
        {
            playerManager.attunement += level;
            playerManager.maxFocusPoints = CalculateFocusPointsBasedOnAttunementLevel(playerManager.attunement);
            playerManager.currentFocusPoints = playerManager.maxFocusPoints;
            PlayerUIManager.instance.playerUIHudManager.setMaxFocusPointsValue(playerManager.maxFocusPoints);
            PlayerUIManager.instance.playerUIHudManager.setNewFocusPointsValue(playerManager.currentFocusPoints);
        }

        protected override int CalculateHealthBasedOnVitalityLevel(int vitality)
        {
            int maxHealth = base.CalculateHealthBasedOnVitalityLevel(vitality);
            PlayerUIManager.instance.playerUIHudManager.setMaxHealthValue(playerManager.maxHealth);
            return maxHealth;
        }

        protected override int CalculateStaminaBasedOnEnduranceLevel(int endurance)
        {
            int maxStamina = base.CalculateStaminaBasedOnEnduranceLevel(endurance);
            PlayerUIManager.instance.playerUIHudManager.setMaxStaminaValue(playerManager.maxStamina);
            return maxStamina;
        }

        protected override int CalculateFocusPointsBasedOnAttunementLevel(int attunement)
        {
            int maxFocusPoints = base.CalculateFocusPointsBasedOnAttunementLevel(attunement);
            PlayerUIManager.instance.playerUIHudManager.setMaxFocusPointsValue(playerManager.maxFocusPoints);
            return maxFocusPoints;
        }

    }
}
