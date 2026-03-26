using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Tartarus
{
    [CreateAssetMenu(menuName = "A.I/States/Pursue Target")]
    public class PursueState : AIState
    {
        [SerializeField] protected bool enablePivot = true;
        public override AIState Tick(AICharacterManager aiCharacter)
        {
            if(aiCharacter.isInteracting)
            {
                return this;
            }

            if(aiCharacter.characterCombatManager.currentTarget == null)
            {
                return SwitchState(aiCharacter, aiCharacter.idleState);
            }

            if(!aiCharacter.navMeshAgent.enabled)
            {
               aiCharacter.navMeshAgent.enabled = true;
            }

            // Pivot towards target
            if (enablePivot)
            {
                if (aiCharacter.aiCharacterCombatManager.viewableAngle < aiCharacter.aiCharacterCombatManager.minimumDetectionAngle || aiCharacter.aiCharacterCombatManager.viewableAngle > aiCharacter.aiCharacterCombatManager.maximumDetectionAngle)
                {
                    aiCharacter.aiCharacterCombatManager.PivotTowardsTarget(aiCharacter);
                }
            }
         
            aiCharacter.aiCharacterLocomotionManager.RotateTowardsAgent(aiCharacter);

            if(aiCharacter.aiCharacterCombatManager.distanceFromTarget <= aiCharacter.aiCharacterCombatManager.maximumEngagementDistance)
                return SwitchState(aiCharacter, aiCharacter.CombatStanceState);

            NavMeshPath path = new NavMeshPath();
            aiCharacter.navMeshAgent.CalculatePath(aiCharacter.characterCombatManager.currentTarget.transform.position, path);
            aiCharacter.navMeshAgent.SetPath(path);

            return this;
        }
    }
}