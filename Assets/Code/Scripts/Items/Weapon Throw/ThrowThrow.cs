using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class ThrowThrow : StateMachineBehaviour
    {

        PlayerManager playerManager;
        GameObject projectile;
        Transform source;

        float chargeTime = 0f;
        float throwForce = 0f;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            playerManager = animator.GetComponent<PlayerManager>();
            projectile = playerManager.playerCombatManager.currentWeapon.throwableWeaponModel;
            source = playerManager.playerCombatManager.projectileSource;
            chargeTime = playerManager.playerCombatManager.chargeTime;
            playerManager.animator.SetBool("isCancelingThrow", false);
            playerManager.animator.SetBool("isThrowing", false);
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
  
            if (playerManager.playerCombatManager.canThrow)
            {
                if(chargeTime < 1.0f)
                {
                    throwForce = 20f;
                }
                else if(chargeTime >= 1.0f && chargeTime < 1.5f)
                {
                    throwForce = 40f;
                }
                else if(chargeTime >= 1.5f)
                {
                    throwForce = 60f;
                }

                GameObject thrownProjectile = Instantiate(projectile, source);
                thrownProjectile.transform.SetParent(null);

                Quaternion rotation = Quaternion.LookRotation(Vector3.up, PlayerCamera.instance.cameraObject.transform.forward);

                thrownProjectile.transform.rotation = rotation;

                playerManager.playerCombatManager.currentThrowableWeapon = thrownProjectile;

                Rigidbody rb = thrownProjectile.GetComponent<Rigidbody>();

                playerManager.playerCombatManager.canThrow = false;

                WeaponManager mainWeaponManager = playerManager.playerCombatManager.currentWeapon.weaponModel.GetComponent<WeaponManager>();
                WeaponManager projectileWeaponManager = thrownProjectile.GetComponent<WeaponManager>();
                
                mainWeaponManager.rangedWeaponDamageCollider = projectileWeaponManager.rangedWeaponDamageCollider;

                mainWeaponManager.SetRangedWeaponDamage(playerManager, playerManager.playerCombatManager.currentWeapon);
                
                projectileWeaponManager.GetComponentInChildren<Collider>().enabled = true;
                
                rb.AddForce(PlayerCamera.instance.cameraObject.transform.forward * throwForce, ForceMode.Impulse);
            }

        }

        //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            PlayerCamera.instance.isUsingFirstPersonCamera = false;
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