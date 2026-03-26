using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    [CreateAssetMenu(menuName = "Items/Magic Attack Item")]
    public class MagicAttackItem : MonoBehaviour
    {
        PlayerManager playerManager;
        public float travelSpeed = 5f;
        public float secondsToDestroy = 5f;
        DamageCollider damageCollider;

        public bool followPlayer = true;
        public bool explodeOnGroundImpact = true;

        private void Awake()
        {
            playerManager = FindObjectOfType<PlayerManager>();
        }

        private void Update()
        {
            if (followPlayer)
                HeadToPlayer();
            if (explodeOnGroundImpact)
                ExplodeOnGroundImpact();
        }

        private void HeadToPlayer()
        {

            Vector3 targetDirection = playerManager.transform.position - transform.position;
            float singleStep = travelSpeed * Time.deltaTime;
            transform.rotation = Quaternion.FromToRotation(Vector3.left, targetDirection);
            Vector3 targetPosition = playerManager.transform.position;
            targetPosition.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, travelSpeed * Time.deltaTime);

            if(Vector3.Distance(transform.position, playerManager.transform.position) < 0.3 || secondsToDestroy <= 0)
            { 
                Destroy(gameObject);
            }

            secondsToDestroy -= Time.deltaTime;

        }

        private void ExplodeOnGroundImpact()
        {
            // Go straight down from where the projectile is

            transform.Translate(Vector3.down * travelSpeed * Time.deltaTime);

            // Check if ground layer is hit

            Destroy(gameObject, 5f);
        }


    }
}