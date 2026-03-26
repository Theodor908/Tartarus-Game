using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{

    public class RangedHold : StateMachineBehaviour
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state

        PlayerManager playerManager;
        GameObject previousParrent;
        Vector3 position;
        GameObject bowHandle;
        GameObject bowString;
        float elapsedTime = 0f;

        public float minimumThrowTime = 0.1f;
        

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        { 

            playerManager = animator.GetComponentInParent<PlayerManager>();
            previousParrent = playerManager.playerInventoryManager.currentWeapon.bowString.transform.parent.gameObject;
            bowString = playerManager.playerInventoryManager.currentWeapon.bowString;
            position = bowString.transform.localPosition;
            bowString.transform.SetParent(playerManager.playerEquipmentManager.weaponHandSlot.transform);
            bowString.transform.localPosition = Vector3.zero;
            PlayerCamera.instance.isUsingFirstPersonCamera = true;

        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

            if(playerManager.isChargingAttack == true)
            {
                elapsedTime += Time.deltaTime;
                animator.SetBool("isThrowing", false);
            }

            if (playerManager.isChargingAttack == false && playerManager.specialCharge == true)
            {
              
                playerManager.playerCombatManager.chargeTime = elapsedTime;
                playerManager.playerCombatManager.canThrow = true;
                animator.SetBool("isThrowing", true);

            }

            if(playerManager.isChargingAttack == false && playerManager.specialCharge == false)
            {
                playerManager.playerCombatManager.chargeTime = elapsedTime;
                animator.SetBool("isCancelingThrow", true);
            }

        }
        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            bowString.transform.SetParent(previousParrent.transform);
            bowString.transform.localPosition = position;
            bowString.transform.rotation = Quaternion.identity;
            bowString.transform.localScale = Vector3.one;
        }

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