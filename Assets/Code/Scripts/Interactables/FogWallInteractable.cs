using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Tartarus
{
    public class FogWallInteractable : Interactable 
    {
        [Header ("Fog Game Objects")]
        [SerializeField] GameObject[] fogGameObjects;


        [Header ("Fog Wall Collider")]
        [SerializeField] Collider fogWallCollider;

        [Header("I.D")]
        public int fogWallID;

        [Header ("Active")]
        public bool isActive = false;

        protected override void Start()
        {
            base.Start();
            WorldObjectManager.instance.fogWalls.Add(this);
        }

        private void Update()
        {
            ActivateFogWall();
        }

        private void ActivateFogWall()
        {
            if(isActive)
            {
                foreach(GameObject fog in fogGameObjects)
                {
                    fog.SetActive(true);
                }
            }
            else
            {
                foreach(GameObject fog in fogGameObjects)
                {
                    fog.SetActive(false);
                }
            }
        }

        public override void Interact(PlayerManager playerManager)
        {

            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward);
            playerManager.transform.rotation = targetRotation;

            StartCoroutine(DisableCollisionForTime(playerManager));
            // 3. Walk through the fog wall

        }

        private IEnumerator DisableCollisionForTime(PlayerManager playerManager)
        {
            Physics.IgnoreCollision(playerManager.characterController, fogWallCollider, true);
            yield return new WaitForSeconds(3);
            Physics.IgnoreCollision(playerManager.characterController, fogWallCollider, false);
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
        }

        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);
        }

    }
}