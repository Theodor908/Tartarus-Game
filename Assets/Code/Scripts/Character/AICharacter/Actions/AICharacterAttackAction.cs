using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    [CreateAssetMenu(menuName = "A.I/Actions/Attack")]
    public class AICharacterAttackAction : AIState
    {

        [Header("Attack")]
        [SerializeField] private string attackAnimation;

        [Header("Combo Action")]
        public AICharacterAttackAction comboAction;

        [Header("Action values")]
        public int attackWeight = 50;
        [SerializeField] AttackType attackType;
        // attack can be repeated
        public float actionRecoveryTime = 1.5f;
        public float minimumAttackAngle = -35;
        public float maximumAttackAngle = 35;
        public float minimumAttackDistance = 0;
        public float maximumAttackDistance = 4;

        public virtual void AttemptToPerformAttackAction(AICharacterManager aiCharacter)
        {
            aiCharacter.characterAnimationManager.PlayTargetAnimation(attackAnimation, true);
        }
    }
}