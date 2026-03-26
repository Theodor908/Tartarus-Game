using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class CharacterAnimationManager : MonoBehaviour
    {
        CharacterManager characterManager;

        [Header("Damage Animations")]
        public string hit_Forward_Medium_01 = "hit_Forward_Medium_01";
        public string hit_Backward_Medium_01 = "hit_Backward_Medium_01";
        public string hit_Left_Medium_01 = "hit_Left_Medium_01";
        public string hit_Right_Medium_01 = "hit_Right_Medium_01";

        [Header ("Flinch Damage Animations")]
        public string hit_Forward_01 = "hit_Forward_01";
        public string hit_Backward_01 = "hit_Backward_01";
        public string hit_Left_01 = "hit_Left_01";
        public string hit_Right_01 = "hit_Right_01";

        [Header ("Flags")]
        public bool applyRootMotion = false;

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
        }

        public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue)
        {


            if (characterManager.isSprinting)
                verticalValue = 2;

            // Check if parameters are defined in the animator

            if (characterManager.animator.GetFloat("Vertical") != verticalValue)
            {
                characterManager.animator.SetFloat("Vertical", verticalValue, 0.1f, Time.deltaTime);
            }

            if (characterManager.animator.GetFloat("Horizontal") != horizontalValue)
            {
                characterManager.animator.SetFloat("Horizontal", horizontalValue, 0.1f, Time.deltaTime);
            }
           
        }

        public virtual void PlayTargetAnimation(string targetAnimation, bool isInteracting, bool applyRootMotion = true, bool canRotate = false, bool canMove = false)
        {
            //Debug.Log("Playing animation " + targetAnimation);
            characterManager.characterAnimationManager.applyRootMotion = applyRootMotion;
            characterManager.animator.applyRootMotion = applyRootMotion;
            characterManager.animator.CrossFade(targetAnimation, 0.2f);
            characterManager.isInteracting = isInteracting; // Allow or stop certain actions while interacting
            characterManager.characterLocomotionManager.canRotate = canRotate;
            characterManager.characterLocomotionManager.canMove = canMove;
        }

        public virtual void PlayTargetAttackActionAnimation(WeaponItem weapon, AttackType attackType,string targetAnimation, bool isInteracting, bool applyRootMotion = true, bool canRotate = false, bool canMove = false)
        {
            //Keep track of the last action performed to look for combos
            //Keep track of current attack type
            //Update animation set to the current weapons animations
            // decide if our attack can be parried
            // we are in an attacking flag

            characterManager.characterCombatManager.currentAttackType = attackType;
            characterManager.characterCombatManager.lastAttackAnimationPerformed = targetAnimation;
            characterManager.characterAnimationManager.applyRootMotion = applyRootMotion;
            UpdateAnimatorController(weapon.weaponAnimatorOverride);

            characterManager.animator.applyRootMotion = applyRootMotion;
            characterManager.animator.CrossFade(targetAnimation, 0.2f);
            characterManager.isInteracting = isInteracting; // Allow or stop certain actions while interacting
            characterManager.characterLocomotionManager.canRotate = canRotate;
            characterManager.characterLocomotionManager.canMove = canMove;
        }

        public void UpdateAnimatorController(AnimatorOverrideController weaponController)
        {
            characterManager.animator.runtimeAnimatorController = weaponController;
        }

    }

}

