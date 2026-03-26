using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class AICharacterBossManager : AICharacterManager
    {
        public int bossID = 0;
        [Header ("Status")]
        
        [SerializeField] bool bossFightIsActive = false;
        [SerializeField] bool previousBossFightState = false;
        [SerializeField] bool hasBeenDefeated = false;
        [SerializeField] bool hasBeenAwakened = false;
        [SerializeField] bool hasBeenPhaseShifted = false;
        [SerializeField] List<FogWallInteractable> fogWalls;
        [SerializeField] string sleepAnimation = "Sleep";
        [SerializeField] string wakeAnimation = "Wake";

        [Header ("Phase Shift")]
        public float minimumHealthPercentageToShift = 50;
        [SerializeField] string phaseShiftAnimation = "PhaseShift_01";
        [SerializeField] CombatStanceState phaseShiftCombatState;

        [Header ("States")]
        [SerializeField] BossSleepState sleepState;
        // Give unique ID
        // whne this ai is spanwed, check save file
        // if the save file doesnt contain the id of this boss add it
        // if the boss has been defeated, dont spawn it

        protected override void Awake()
        {
            base.Awake();
            sleepState = Instantiate(sleepState);
            currentState = sleepState;
        }

        protected override void Start()
        {

            bossFightIsActive = false;

            // If the boss has not been awakened, add it to the save file
            if(!WorldSaveManager.instance.currentCharacterData.bossesAwakened.ContainsKey(bossID))
            {
                WorldSaveManager.instance.currentCharacterData.bossesAwakened.Add(bossID, false);
                WorldSaveManager.instance.currentCharacterData.bossesDefeated.Add(bossID, false);
            }
            // Otherwise load the data
            else
            {
                hasBeenDefeated = WorldSaveManager.instance.currentCharacterData.bossesDefeated[bossID];
                hasBeenAwakened = WorldSaveManager.instance.currentCharacterData.bossesAwakened[bossID];

            }

            StartCoroutine(GetFogWallsFromWorldObjectManager());

            if (hasBeenDefeated)
            {
                for (int i = 0; i < fogWalls.Count; i++)
                {
                    fogWalls[i].isActive = false;
                }
                Destroy(gameObject);
            }

            if(!hasBeenAwakened)
            {
                characterAnimationManager.PlayTargetAnimation("Sleep", true);
            }


        }

        protected override void Update()
        {
            base.Update();
            BossFight();
        }

        public override void CheckHealthPoints()
        {
            base.CheckHealthPoints();
            if (!hasBeenPhaseShifted && currentHealth <= minimumHealthPercentageToShift / 100 * maxHealth)
            {
                PhaseShift();
            }
        }

        public override IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
        {

            PlayerUIManager.instance.playerUIPopUpManager.SendBossDefeatedPopUp("DEMON FELLED");

            currentHealth = 0;
            isDead = true;
            bossFightIsActive = false;
            // Reset all flags

            // if in air death air anim

            // if on ground death ground anim

            if (!manuallySelectDeathAnimation)
            {
                characterAnimationManager.PlayTargetAnimation("Dead_01", true);
            }
            else
            {
                // Play default death animation
            }

            hasBeenDefeated = true;

            if (!WorldSaveManager.instance.currentCharacterData.bossesAwakened.ContainsKey(bossID))
            {
                WorldSaveManager.instance.currentCharacterData.bossesAwakened.Add(bossID, true);
                WorldSaveManager.instance.currentCharacterData.bossesDefeated.Add(bossID, true);
            }
            else
            {
                WorldSaveManager.instance.currentCharacterData.bossesAwakened.Remove(bossID);
                WorldSaveManager.instance.currentCharacterData.bossesDefeated.Remove(bossID);

                WorldSaveManager.instance.currentCharacterData.bossesAwakened.Add(bossID, true);
                WorldSaveManager.instance.currentCharacterData.bossesDefeated.Add(bossID, true);
            }

                WorldSaveManager.instance.SaveGame();

            yield return new WaitForSeconds(5);

            for (int i = 0; i < fogWalls.Count; i++)
            {
                fogWalls[i].isActive = false;
            }
            Destroy(gameObject);

            // Award players with runes

            // Disable character
        }

        private IEnumerator GetFogWallsFromWorldObjectManager()
        {
            while(WorldObjectManager.instance.fogWalls.Count == 0)
            {
                yield return new WaitForEndOfFrame();
            }

            fogWalls = new List<FogWallInteractable>();
            foreach (FogWallInteractable fogWall in WorldObjectManager.instance.fogWalls)
            {
                if (fogWall.fogWallID == bossID)
                {
                    fogWalls.Add(fogWall);
                }
            }

            if (hasBeenAwakened)
            {
                for (int i = 0; i < fogWalls.Count; i++)
                {
                    fogWalls[i].isActive = true;
                }
            }

        }

        public void WakeBoss()
        {

            if(!hasBeenAwakened)
            {
                characterAnimationManager.PlayTargetAnimation("Wake", true);
            }

            bossFightIsActive = true;
            hasBeenAwakened = true;
            currentState = idleState;

            if (!WorldSaveManager.instance.currentCharacterData.bossesAwakened.ContainsKey(bossID))
            {
                WorldSaveManager.instance.currentCharacterData.bossesAwakened.Add(bossID, true);
            }
            else
            {
                WorldSaveManager.instance.currentCharacterData.bossesAwakened.Remove(bossID);

                WorldSaveManager.instance.currentCharacterData.bossesAwakened.Add(bossID, true);
            }

            for(int i = 0; i < fogWalls.Count; i++)
            {
                fogWalls[i].isActive = true;
            }

        }

        private void BossFight()
        {

            if (previousBossFightState != bossFightIsActive)
            {
                previousBossFightState = bossFightIsActive;
                if (bossFightIsActive)
                {
                    GameObject bossHealthBar = Instantiate(PlayerUIManager.instance.playerUIHudManager.bossHealthBarObject, PlayerUIManager.instance.playerUIHudManager.bossHealthBarParent);
                    UI_Boss_Hp_Bar bossHPBar = bossHealthBar.GetComponent<UI_Boss_Hp_Bar>();
                    bossHPBar.EnableBossHPBar(this);
                }
            }
          
        }

        public void PhaseShift()
        {
            characterAnimationManager.PlayTargetAnimation(phaseShiftAnimation, true);
            CombatStanceState = Instantiate(phaseShiftCombatState);
            currentState = CombatStanceState;
            hasBeenPhaseShifted = true;
        }

    }
}
