using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    [CreateAssetMenu(menuName = "A.I/States/Attack State")]
    public class AttackState : AIState
    {
        [Header ("Current Attack")]
        [HideInInspector] public AICharacterAttackAction currentAttack;
        [HideInInspector] public bool willPerfromCombo = false;

        [Header ("State Flags")]
        protected bool hasPerformedAttack = false;
        protected bool hasPerformedCombo = false;

        [Header ("Pivot After Attack")]
        [SerializeField] protected bool pivotAfterAttack = false;

        public override AIState Tick(AICharacterManager aiCharacter)
        {
            if(aiCharacter.aiCharacterCombatManager.currentTarget == null)
            {
                return SwitchState(aiCharacter, aiCharacter.idleState);
            }

            if(aiCharacter.aiCharacterCombatManager.currentTarget.isDead)
            {
                return SwitchState(aiCharacter, aiCharacter.idleState);
            }

            if(aiCharacter.aiCharacterCombatManager.distanceFromTarget > currentAttack.maximumAttackDistance)
            {
                return SwitchState(aiCharacter, aiCharacter.pursueTargetState);
            }

            aiCharacter.aiCharacterCombatManager.RotateTowardsTargetWhilstAttacking(aiCharacter);

            aiCharacter.characterAnimationManager.UpdateAnimatorMovementParameters(0,0);
            
            // Perform a combo attack
            if(willPerfromCombo && !hasPerformedCombo)
            {
                if(currentAttack.comboAction != null)
                {
                    hasPerformedCombo = true;
                    currentAttack.comboAction.AttemptToPerformAttackAction(aiCharacter);
                }
            }

            if (aiCharacter.isInteracting)
                return this;

            if (!hasPerformedAttack)
            {
                if(aiCharacter.aiCharacterCombatManager.actionRecoveryTimer > 0)
                    return this;

                PerfromAttack(aiCharacter);

                return this;

            }

            if(pivotAfterAttack)
            {
                aiCharacter.aiCharacterCombatManager.PivotTowardsTarget(aiCharacter);
            }

            return SwitchState(aiCharacter, aiCharacter.CombatStanceState);

        }

        protected void PerfromAttack(AICharacterManager aiCharacter)
        {
            hasPerformedAttack = true;
            currentAttack.AttemptToPerformAttackAction(aiCharacter);
            aiCharacter.aiCharacterCombatManager.actionRecoveryTimer = currentAttack.actionRecoveryTime;
        }

        protected override void ResetStateFlags(AICharacterManager aiCharacter)
        {
            base.ResetStateFlags(aiCharacter);
            hasPerformedAttack = false;
            hasPerformedCombo = false;
        }

    }
    
}
