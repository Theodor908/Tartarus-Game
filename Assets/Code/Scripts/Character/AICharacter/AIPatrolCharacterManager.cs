using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Tartarus
{
    public class AIPatrolCharacterManager : AICharacterManager
    {
        [Header ("Patrol Checkpoints")]
        public List<Transform> patrolCheckPoints = new List<Transform>();
        public int ListIndex = 0;
        protected override void Awake()
        {
            base.Awake();
            aiCharacterCombatManager = GetComponent<AICharacterCombatManager>();
            aiCharacterLocomotionManager = GetComponent<AICharacterLocomotionManager>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();

            idleState = Instantiate(idleState);
            pursueTargetState = Instantiate(pursueTargetState);
            CombatStanceState = Instantiate(CombatStanceState);
            attackState = Instantiate(attackState);

            currentState = patrolState;

            patrolCheckPoints = WorldPathManager.instance.GetPath(ListIndex);
        }

        protected override void Update()
        {
            base.Update();
            aiCharacterCombatManager.HandleActionRecovery(this);

        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            ProcessStateMachine();
        }

        private void ProcessStateMachine()
        {
            AIState nextState = currentState?.Tick(this);

            if (nextState != null)
            {
                currentState = nextState;
            }

            navMeshAgent.transform.localPosition = Vector3.zero;
            navMeshAgent.transform.localRotation = Quaternion.identity;

            if (aiCharacterCombatManager.currentTarget != null)
            {
                aiCharacterCombatManager.targetDirection = aiCharacterCombatManager.currentTarget.transform.position - transform.position;
                aiCharacterCombatManager.viewableAngle = WorldUtilityManager.instance.GetAngleOfTarget(transform, aiCharacterCombatManager.targetDirection);
                aiCharacterCombatManager.distanceFromTarget = Vector3.Distance(transform.position, aiCharacterCombatManager.currentTarget.transform.position);
            }

            if (navMeshAgent.enabled)
            {
                Vector3 targetDestination = navMeshAgent.destination;
                float remainingDistance = Vector3.Distance(targetDestination, transform.position);

                if (remainingDistance > navMeshAgent.stoppingDistance)
                {
                    isMoving = true;
                }
                else
                {
                    isMoving = false;
                }

            }

        }

    }
}
