using System.Collections;
using System.Collections.Generic;
using Tartarus;
using UnityEngine;

namespace Tartarus
{
    public class ThrowLoop : StateMachineBehaviour
    {
        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

        PlayerManager playerManager;

        public float minimumThrowTime = 0.5f;
        public float elapsedTime = 0f;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            playerManager = animator.GetComponent<PlayerManager>();
            playerManager.playerCombatManager.isThrowing = true;
            elapsedTime = 0f;

            playerManager.playerLocomotionManager.canMove = true;
            playerManager.playerLocomotionManager.canRotate = true;
            PlayerCamera.instance.isUsingFirstPersonCamera = true;

        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if(playerManager.isChargingAttack == true)
            {
                elapsedTime += Time.deltaTime;
            }

            if (playerManager.isChargingAttack == false && elapsedTime < minimumThrowTime)
            {
                animator.SetBool("isCancelingThrow", true);
            }

            if (playerManager.isChargingAttack == false && elapsedTime >= minimumThrowTime)
            {
                playerManager.playerCombatManager.chargeTime = elapsedTime;
                animator.SetBool("isThrowing", true);
            }

        }

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
