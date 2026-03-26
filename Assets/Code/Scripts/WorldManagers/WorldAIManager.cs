using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Tartarus
{
    public class WorldAIManager : MonoBehaviour
    {
        public static WorldAIManager instance;

        [Header("Characters")]
        [SerializeField] List<AICharacterSpawner> aiCharacterSpawners;
        [SerializeField] List<AICharacterManager> spawnedCharacters;
        [SerializeField] List<AICharacterBossManager> spawnedBosses;

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

        private void Start()
        {

        }

        private void Update()
        {
     
        }

        private void RefreshSpawnedAICharacterList()
        {

        }

        public void SpawnCharacter(AICharacterSpawner aiCharacterSpawner )
        {
            aiCharacterSpawners.Add(aiCharacterSpawner);
            aiCharacterSpawner.AttemptToSpawnCharacter();
        }

        public void AddCharacterToSpawnedCharactersList(AICharacterManager character)
        {
            if(spawnedCharacters.Contains(character))
            {
                return;
            }

            spawnedCharacters.Add(character);

            AICharacterBossManager boss = character as AICharacterBossManager;

            if(boss != null)
            {
                if(spawnedBosses.Contains(boss))
                {
                    return;
                }
                spawnedBosses.Add(boss);
            }

        }

        public AICharacterBossManager GetBossByID(int id)
        {
            return spawnedBosses.FirstOrDefault(x => x.bossID == id);
        }

        public void ResetAllCharacters()
        {
            DisableAllCharacters();
            foreach(AICharacterSpawner spawner in aiCharacterSpawners)
            {
                spawner.AttemptToSpawnCharacter();
            }
        }

        private void DisableAllCharacters()
        {
           foreach(AICharacterManager character in spawnedCharacters)
            {
                    if(character != null)
                        Destroy(character.gameObject);
            }   

           spawnedCharacters.Clear();

        }

    }
}