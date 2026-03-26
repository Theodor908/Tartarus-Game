using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class WorldObjectManager : MonoBehaviour
    {
        public static WorldObjectManager instance;

        [Header("Objects")]
        [SerializeField] List<ObjectSpawner> objectSpawners;
        [SerializeField] List<GameObject> spawnedObjects;

        [Header("Fog Walls")]
        public List<FogWallInteractable> fogWalls;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        public void SpawnObject(ObjectSpawner objectSpawner)
        {
            objectSpawners.Add(objectSpawner);
            GameObject spawnedGameObject = objectSpawner.AttemptToSpawnObject();
            if(spawnedGameObject != null)
            {
                spawnedObjects.Add(spawnedGameObject);
            }
        }

        public void AddFogWall(FogWallInteractable fogWall)
        {
            if (!fogWalls.Contains(fogWall))
            {
                fogWalls.Add(fogWall);
            }
        }

        public void RemoveFogWall(FogWallInteractable fogWall)
        {
            if (fogWalls.Contains(fogWall))
            {
                fogWalls.Remove(fogWall);
            }
        }


    }
}