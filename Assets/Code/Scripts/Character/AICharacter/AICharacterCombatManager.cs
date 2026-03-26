using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class AICharacterCombatManager : CharacterCombatManager
    {

        [Header("Target information")]
        public float viewableAngle;
        public Vector3 targetDirection;
        public float distanceFromTarget;

        [Header ("Detection")]
        [SerializeField] float detectionRadius = 15;
        public float minimumDetectionAngle = -35;
        public float maximumDetectionAngle = 35;

        [Header("Action Recovery Timer")]
        public float actionRecoveryTimer;

        [Header("Attack Rotation Speed")]
        public float attackRotationSpeed = 25;

        [Header("Engagement distance")]
        public float maximumEngagementDistance;

        protected override void Awake()
        {
            base.Awake();
            LockOnTransform lockOnTransfrm = GetComponentInChildren<LockOnTransform>();
            if(lockOnTransfrm != null)
            {
                lockOnTransform = lockOnTransfrm.transform;
            }
        }

        public void FindATargetViaLineOfSight(AICharacterManager aiCharacter)
        {
            if (currentTarget != null)
                return;
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, WorldUtilityManager.instance.GetCharacterLayers());

            for(int i = 0; i < colliders.Length; i++)
            {
                CharacterManager targetCharacter = colliders[i].GetComponent<CharacterManager>();

                if (targetCharacter == null) continue;

                if (targetCharacter == aiCharacter) continue;

                if (targetCharacter.isDead) continue;

                if(WorldUtilityManager.instance.CanIDamageThisTarget(aiCharacter.characterGroup, targetCharacter.characterGroup))
                {
                    // Check if it is in line of sight
                    Vector3 targetDirection = targetCharacter.transform.position - aiCharacter.transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
                    if(viewableAngle > minimumDetectionAngle && viewableAngle < maximumDetectionAngle)
                    {
                        // Check if there is a clear line of sight
                        if(!Physics.Linecast(aiCharacter.characterCombatManager.lockOnTransform.position, targetCharacter.characterCombatManager.lockOnTransform.position, WorldUtilityManager.instance.GetEnviromentalLayers()))
                        {
                            aiCharacter.isLockedOn = true;
                            aiCharacter.characterCombatManager.SetLockOnTarget(targetCharacter);
                            PivotTowardsTarget(aiCharacter);
                            //Debug.Log("Found target");
                        }
                    }
                }
                
            }

        }

        public virtual void PivotTowardsTarget(AICharacterManager aiCharacter)
        {
            if(aiCharacter.isInteracting)
            {
                return;
            }
            
            Vector3 direction = aiCharacter.aiCharacterCombatManager.currentTarget.transform.position - aiCharacter.transform.position;
            direction.y = 0;
            direction.Normalize();

            if(direction == Vector3.zero)
            {
                direction = aiCharacter.transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            aiCharacter.transform.rotation = Quaternion.Slerp(aiCharacter.transform.rotation, targetRotation,  50f);

        }

        public void RotateTowardsAgent(AICharacterManager aiCharacter)
        {
            if(aiCharacter.isMoving)
            {
                aiCharacter.transform.rotation = aiCharacter.navMeshAgent.transform.rotation;
            }
        }

        public void RotateTowardsTargetWhilstAttacking(AICharacterManager aiCharacter)
        {
            if (currentTarget == null)
                return;

            if (!aiCharacter.aiCharacterLocomotionManager.canRotate)
                return;

            if (!aiCharacter.isInteracting)
                return;

            Vector3 targetDirection = currentTarget.transform.position - aiCharacter.transform.position;
            targetDirection.y = 0;
            targetDirection.Normalize();

            if (targetDirection == Vector3.zero)
            {
                targetDirection = aiCharacter.transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            aiCharacter.transform.rotation = Quaternion.Slerp(aiCharacter.transform.rotation, targetRotation, attackRotationSpeed * Time.deltaTime);
            // specified frames in animation to rotate towards target

        }

        public void HandleActionRecovery(AICharacterManager aICharacter)
        {
            if(actionRecoveryTimer > 0)
            {
                if(!aICharacter.isInteracting)
                {
                    actionRecoveryTimer -= Time.deltaTime;
                }
            }
        }

    }
}