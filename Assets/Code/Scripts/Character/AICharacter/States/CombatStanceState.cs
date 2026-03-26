using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Tartarus
{
    [CreateAssetMenu(menuName = "A.I/States/Combat Stance")]
    public class CombatStanceState : AIState
    {

        // 1. select an attack for the attack state depending on distance, angle and attack cooldown
        // 2. process any combat logic
        // 3. if target moves out of range, switch to pursue state
        // 4. if the target is no longer present, switch to idle state

        [Header("Attacks")]
        public List<AICharacterAttackAction> aiCharacterAttacks; // all attacks the character can do
        [SerializeField] protected List<AICharacterAttackAction> potentialAttacks; // a list that is created during this state, all attacks possible for the context
        [SerializeField] AICharacterAttackAction previousAttack;
        [SerializeField] AICharacterAttackAction currentAttack;
        protected bool hasAttack = false;

        [Header ("Combo")]
        [SerializeField] protected bool canPerformCombo = false;
        [SerializeField] protected int chanceToPerformCombo = 25;
        protected bool hasRolledForComboChance = false;

        [Header("Pivot")]
        [SerializeField] protected bool enablePivot;

        public override AIState Tick(AICharacterManager aiCharacter)
        {
            if(aiCharacter.isInteracting)
            {
                return this;
            }

            if(!aiCharacter.navMeshAgent.enabled)
            {
                aiCharacter.navMeshAgent.enabled = true;
            }

            if(enablePivot)
            {
                if (!aiCharacter.isMoving)
                    if (aiCharacter.aiCharacterCombatManager.viewableAngle < aiCharacter.aiCharacterCombatManager.minimumDetectionAngle || aiCharacter.aiCharacterCombatManager.viewableAngle > aiCharacter.aiCharacterCombatManager.maximumDetectionAngle)
                    {
                        aiCharacter.aiCharacterCombatManager.PivotTowardsTarget(aiCharacter);
                    }
            }

            aiCharacter.aiCharacterCombatManager.RotateTowardsAgent(aiCharacter);

            if(aiCharacter.aiCharacterCombatManager.currentTarget == null)
            {
                return SwitchState(aiCharacter, aiCharacter.idleState);
            }

            if(!hasAttack)
                GetNewAttack(aiCharacter);
            else
            {
                aiCharacter.attackState.currentAttack = currentAttack;
                // roll for combo
                return SwitchState(aiCharacter, aiCharacter.attackState);
            }
           
            if(aiCharacter.aiCharacterCombatManager.distanceFromTarget > aiCharacter.aiCharacterCombatManager.maximumEngagementDistance)
            {
                return SwitchState(aiCharacter, aiCharacter.pursueTargetState);
            }


            NavMeshPath path = new NavMeshPath();
            aiCharacter.navMeshAgent.CalculatePath(aiCharacter.characterCombatManager.currentTarget.transform.position, path);
            aiCharacter.navMeshAgent.SetPath(path);

            return this;


        }

        protected virtual void GetNewAttack(AICharacterManager aiCharacter)
        {
           
            potentialAttacks = new List<AICharacterAttackAction>();



            foreach (AICharacterAttackAction potentialAttack in aiCharacterAttacks)
            {
                if (potentialAttack.minimumAttackDistance > aiCharacter.aiCharacterCombatManager.distanceFromTarget)
                    continue;

                if(potentialAttack.maximumAttackDistance < aiCharacter.aiCharacterCombatManager.distanceFromTarget)
                    continue;

                if (potentialAttack.minimumAttackAngle > aiCharacter.aiCharacterCombatManager.viewableAngle)
                    continue;

                if (potentialAttack.maximumAttackAngle < aiCharacter.aiCharacterCombatManager.viewableAngle)
                    continue;

                potentialAttacks.Add(potentialAttack);

            }
            
            if (potentialAttacks.Count <= 0)
            {
                return;
            }

            float totalWeight = 0;

            foreach (AICharacterAttackAction attack in potentialAttacks)
            {
                totalWeight += attack.attackWeight;
            }

            float randomWeightValue = Random.Range(1, totalWeight + 1);
            float processedWeightValue = 0;

            foreach (AICharacterAttackAction attack in potentialAttacks)
            {
                processedWeightValue += attack.attackWeight;

                if (randomWeightValue <= processedWeightValue)
                {
                    currentAttack = attack;
                    previousAttack = currentAttack;
                    hasAttack = true;
                    return;
                }
            }
            // remove attacks that cant be perfomed based on context
            // place remaining attacks in availableAttacks
            // select an attack from availableAttacks and pass it to attack state
        }

        protected virtual bool RollForOutcomeChance(int chance)
        {
            bool outComeWillBePerformed = false;
            int randommPercentage = Random.Range(0, 100);

            if(randommPercentage < chance)
                outComeWillBePerformed = true;

            return outComeWillBePerformed;
        }

        protected override void ResetStateFlags(AICharacterManager aiCharacter)
        {
            base.ResetStateFlags(aiCharacter);
            hasRolledForComboChance = false;
            hasAttack = false;
        }


    }
}
