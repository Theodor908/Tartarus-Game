using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Tartarus
{
    public class AICharacterSpawner : MonoBehaviour
    {
        [Header ("Character")]
        [SerializeField] protected GameObject characterGameObject;
        [SerializeField] protected GameObject instantiatedGameObject;
        bool notSpawnedBoss = true;
        public bool isBoss = false;

        private void Awake()
        {
            
        }

        private void Start()
        {
            WorldAIManager.instance.SpawnCharacter(this);
            gameObject.SetActive(false);
        }

        public void AttemptToSpawnCharacter()
        {
           if(characterGameObject != null)
           {
                instantiatedGameObject = Instantiate(characterGameObject, transform.position, transform.rotation);
                WorldAIManager.instance.AddCharacterToSpawnedCharactersList(instantiatedGameObject.GetComponent<AICharacterManager>());
           }

        }
    }
}