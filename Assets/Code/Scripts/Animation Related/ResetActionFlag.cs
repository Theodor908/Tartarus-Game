using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class ResetActionFlag : StateMachineBehaviour
    {

        CharacterManager characterManager;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

            CharacterManager characterManager = animator.GetComponent<CharacterManager>();

            if(characterManager == null)
            {

                characterManager = animator.GetComponent<CharacterManager>();

            }

            if(characterManager == null)
            {

                return;

            }

            // Action flag reset
            characterManager.isInteracting = false;
            characterManager.isInvulnerable = false;
            characterManager.characterLocomotionManager.canMove = true;
            characterManager.characterLocomotionManager.canRotate = true;
            characterManager.characterAnimationManager.applyRootMotion = false;
            characterManager.characterLocomotionManager.isRolling = false; 
            characterManager.isChargingAttack = false;
            characterManager.specialCharge = false;
            characterManager.isAttacking = false;
            characterManager.characterCombatManager.isThrowing = false;
            characterManager.characterCombatManager.canThrow = false;
            characterManager.characterCombatManager.DisableCanDoCombo();
            characterManager.characterCombatManager.DisableCanDoRollingAttack();
            characterManager.characterCombatManager.DisableCanDoBackstepAttack();
            PlayerCamera.instance.isUsingFirstPersonCamera = false;

        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}
