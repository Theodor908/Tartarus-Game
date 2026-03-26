using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class ObjectSpawner : MonoBehaviour
    {
        [Header("Object")]
        [SerializeField] GameObject gmObject;
        [SerializeField] GameObject instantiatedGameObject;

        private void Awake()
        {

        }

        private void Start()
        {
            WorldObjectManager.instance.SpawnObject(this);
        }

        public GameObject AttemptToSpawnObject()
        {
            if (gmObject != null)
            {
                instantiatedGameObject = Instantiate(gmObject, transform.position, transform.rotation);
                return instantiatedGameObject;
            }

            return null;

        }

    }
}