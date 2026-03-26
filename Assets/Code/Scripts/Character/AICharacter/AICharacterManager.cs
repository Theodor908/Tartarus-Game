using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Tartarus
{
    public class AICharacterManager : CharacterManager
    {
        [HideInInspector] public AICharacterCombatManager aiCharacterCombatManager;
        [HideInInspector] public AICharacterLocomotionManager aiCharacterLocomotionManager;

        [Header ("Navmesh Agent")]
        public NavMeshAgent navMeshAgent;

        [Header ("Current State")]
        [SerializeField]protected AIState currentState;

        [Header ("States")]
        public IdleState idleState;
        public PursueState pursueTargetState;
        public CombatStanceState CombatStanceState;
        public PatrolState patrolState;
        public AttackState attackState;

        protected override void Awake()
        {
            base.Awake();
            aiCharacterCombatManager = GetComponent<AICharacterCombatManager>();
            aiCharacterLocomotionManager = GetComponent<AICharacterLocomotionManager>();
            navMeshAgent = GetComponentInChildren<NavMeshAgent>();

            idleState = Instantiate(idleState);
            pursueTargetState = Instantiate(pursueTargetState);
            CombatStanceState = Instantiate(CombatStanceState);
            patrolState = Instantiate(patrolState);
            attackState = Instantiate(attackState);

            currentState = idleState;
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

            if(aiCharacterCombatManager.currentTarget != null)
            {
                aiCharacterCombatManager.targetDirection = aiCharacterCombatManager.currentTarget.transform.position - transform.position;
                aiCharacterCombatManager.viewableAngle = WorldUtilityManager.instance.GetAngleOfTarget(transform, aiCharacterCombatManager.targetDirection);
                aiCharacterCombatManager.distanceFromTarget = Vector3.Distance(transform.position, aiCharacterCombatManager.currentTarget.transform.position);
            }

            if(navMeshAgent.enabled)
            {
                Vector3 targetDestination = navMeshAgent.destination;
                float remainingDistance = Vector3.Distance(targetDestination, transform.position);

                if(remainingDistance > navMeshAgent.stoppingDistance)
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