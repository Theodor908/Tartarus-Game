using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class RangedRelease : StateMachineBehaviour
    {

        PlayerManager playerManager;
        GameObject projectile;
        Transform source;

        public float throwForce = 30f;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            playerManager = animator.GetComponent<PlayerManager>();
            projectile = playerManager.playerCombatManager.currentWeapon.throwableWeaponModel;
            source = playerManager.playerCombatManager.projectileSource;
            PlayerCamera.instance.isUsingFirstPersonCamera = true;
            if (playerManager.playerCombatManager.canThrow)
            {

                if (playerManager.playerCombatManager.chargeTime >= 1.0f && playerManager.playerCombatManager.chargeTime < 1.5f)
                {
                    throwForce *= 1.5f;
                }
                else if (playerManager.playerCombatManager.chargeTime >= 1.5f)
                {
                    throwForce *= 2;
                }

                GameObject thrownProjectile = Instantiate(projectile, source);
                thrownProjectile.transform.SetParent(null);

                Quaternion rotation = Quaternion.LookRotation(PlayerCamera.instance.cameraObject.transform.forward, Vector3.up);

                thrownProjectile.transform.rotation = rotation;

                playerManager.playerCombatManager.currentThrowableWeapon = thrownProjectile;

                Rigidbody rb = thrownProjectile.GetComponent<Rigidbody>();

                playerManager.playerCombatManager.canThrow = false;

                WeaponManager mainWeaponManager = playerManager.playerCombatManager.currentWeapon.weaponModel.GetComponent<WeaponManager>();
                WeaponManager projectileWeaponManager = thrownProjectile.GetComponent<WeaponManager>();


                mainWeaponManager.rangedWeaponDamageCollider = projectileWeaponManager.rangedWeaponDamageCollider;
                mainWeaponManager.rangedWeaponDamageCollider.DisableDamageCollider();
                mainWeaponManager.SetRangedWeaponDamage(playerManager, playerManager.playerCombatManager.currentWeapon);

                projectileWeaponManager.GetComponentInChildren<Collider>().enabled = true;

                rb.AddForce(PlayerCamera.instance.cameraObject.transform.forward * throwForce, ForceMode.Impulse);


            }

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