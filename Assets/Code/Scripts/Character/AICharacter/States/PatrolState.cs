using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

namespace Tartarus
{
    [CreateAssetMenu(menuName = "A.I/States/PatrolState")]
    public class PatrolState : AIState
    {

        [SerializeField] bool patrolComplete = false;
        [SerializeField] bool repeatPatrol = false;
        [SerializeField] float endOfPatrolResetTimer = 3f;
        [SerializeField] float endOfPatrolTimer = 0f;
        [SerializeField] bool hasPatrolDestination = false;
        [SerializeField] int currentPatrolIndex = 0;
        [SerializeField] Transform currentPatrolDestination = null;
        [SerializeField] float distanceToCheckPoint = 0f;
        [SerializeField] List<Transform> patrolCheckPoints = new List<Transform>();

        public override AIState Tick(AICharacterManager aiCharacter)
        {
            AIPatrolCharacterManager patrolCharacter = aiCharacter as AIPatrolCharacterManager;
            patrolCheckPoints = patrolCharacter.patrolCheckPoints;

            FindTargetViaLineOfSight(aiCharacter);

            if(aiCharacter.isInteracting)
            {
                return SwitchState(aiCharacter, aiCharacter.pursueTargetState);
            }

            if(aiCharacter.aiCharacterCombatManager.currentTarget != null)
            {
                return SwitchState(aiCharacter, aiCharacter.pursueTargetState);
            }

            if(patrolComplete && repeatPatrol)
            {
                if(endOfPatrolResetTimer > endOfPatrolTimer)
                {
                    aiCharacter.animator.SetBool("patrol", false);
                    endOfPatrolTimer += Time.deltaTime;
                }
                else if(endOfPatrolTimer >= endOfPatrolResetTimer)
                {
                    currentPatrolIndex = -1;
                    hasPatrolDestination = false;
                    currentPatrolDestination = null;
                    patrolComplete = false;
                    endOfPatrolTimer = 0f;
                }
            }
            else if(patrolComplete && !repeatPatrol)
            {
                aiCharacter.navMeshAgent.enabled = false;
                aiCharacter.animator.SetBool("patrol", false);
            }
            if (hasPatrolDestination)
            {

                if(currentPatrolDestination != null)
                {
                    distanceToCheckPoint = Vector3.Distance(aiCharacter.transform.position, patrolCheckPoints[currentPatrolIndex].position);

                    if(distanceToCheckPoint > 2f)
                    {
                        aiCharacter.navMeshAgent.enabled = true;
                        NavMeshPath path = new NavMeshPath();
                        aiCharacter.navMeshAgent.CalculatePath(patrolCheckPoints[currentPatrolIndex].position, path);
                        aiCharacter.navMeshAgent.SetPath(path);
                        Quaternion targetRotation = Quaternion.Lerp(aiCharacter.transform.rotation, aiCharacter.navMeshAgent.transform.rotation, 0.5f);
                        aiCharacter.transform.rotation = targetRotation;
                        aiCharacter.animator.SetBool("patrol", true);
                    }
                    else
                    {
                        currentPatrolDestination = null;
                        hasPatrolDestination = false;
                    }
                }
            }

            if(!hasPatrolDestination)
            {
                if(patrolCheckPoints.Count > 0)
                {
                    currentPatrolIndex += 1;

                    if(currentPatrolIndex >= patrolCheckPoints.Count)
                    {
                        patrolComplete = true;
                        currentPatrolIndex = 0;
                        return this;
                    }

                    currentPatrolDestination = patrolCheckPoints[currentPatrolIndex];
                    hasPatrolDestination = true;
                }
            }

            return this;

        }

        private void FindTargetViaLineOfSight(AICharacterManager aiCharacter)
        {
            if (aiCharacter.characterCombatManager.currentTarget != null)
            {
                return;
            }
            else
            {
                aiCharacter.aiCharacterCombatManager.FindATargetViaLineOfSight(aiCharacter);
                aiCharacter.animator.SetBool("patrol", false);
                return;
            }
        }

    }
}