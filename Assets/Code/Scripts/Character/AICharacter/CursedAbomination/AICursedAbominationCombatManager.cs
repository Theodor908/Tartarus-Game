using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{

    public class AICursedAbominationCombatManager : AICharacterCombatManager
    {

        [Header("Damage Collider")]
        [SerializeField] List<CursedAbominationDamageCollider> rightHandDamageColliders;
        [SerializeField] List<CursedAbominationDamageCollider> leftHandDamageColliders;

        [SerializeField] StompDamageCollider armsSmashStomp;
        [SerializeField] StompDamageCollider rightFootSmashStomp;
        [SerializeField] StompDamageCollider leftFootSmashStomp;

        [Header("Damage")]
        [SerializeField] int baseDamage = 16;
        [SerializeField] int basePoiseDamage = 16;
        [SerializeField] float attack01DamageModifier = 1.2f;
        [SerializeField] float attack02DamageModifier = 1.6f;
        [SerializeField] float attack03DamageModifier = 2.0f;

        [Header ("VFX")]
        [SerializeField] GameObject cursedAbominationImpact;

        protected override void Awake()
        {
            base.Awake();
        }

        public void SetAttack01Damage()
        {
            foreach (CursedAbominationDamageCollider collider in rightHandDamageColliders)
            {
                collider.physicalDamage = baseDamage * attack01DamageModifier;
                collider.poiseDamage = basePoiseDamage * attack01DamageModifier;
            }

            foreach (CursedAbominationDamageCollider collider in leftHandDamageColliders)
            {
                collider.physicalDamage = baseDamage * attack01DamageModifier;
                collider.poiseDamage = basePoiseDamage * attack01DamageModifier;
            }

        }

        public void SetAttack02Damage()
        {
            foreach (CursedAbominationDamageCollider collider in rightHandDamageColliders)
            {
                collider.physicalDamage = baseDamage * attack02DamageModifier;
                collider.poiseDamage = basePoiseDamage * attack02DamageModifier;
            }

            foreach (CursedAbominationDamageCollider collider in leftHandDamageColliders)
            {
                collider.physicalDamage = baseDamage * attack02DamageModifier;
                collider.poiseDamage = basePoiseDamage * attack02DamageModifier;
            }

        }

        public void SetAttack03Damage()
        {
            foreach (CursedAbominationDamageCollider collider in rightHandDamageColliders)
            {
                collider.physicalDamage = baseDamage * attack01DamageModifier;
                collider.poiseDamage = basePoiseDamage * attack01DamageModifier;
            }

            foreach (CursedAbominationDamageCollider collider in leftHandDamageColliders)
            {
                collider.physicalDamage = baseDamage * attack01DamageModifier;
                collider.poiseDamage = basePoiseDamage * attack01DamageModifier;
            }
        }

        public void OpenCursedRightHandDamageCollider()
        {
            foreach (CursedAbominationDamageCollider collider in rightHandDamageColliders)
            {
                collider.EnableDamageCollider();
            }
        }

        public void OpenCursedLeftHandDamageCollider()
        {
            foreach (CursedAbominationDamageCollider collider in leftHandDamageColliders)
            {
                collider.EnableDamageCollider();
            }
        }

        public void DisableCursedRightHandDamageCollider()
        {
            foreach (CursedAbominationDamageCollider collider in rightHandDamageColliders)
            {
                collider.DisableDamageCollider();
            }
        }

        public void DisableCursedLeftHandDamageCollider()
        {
            foreach (CursedAbominationDamageCollider collider in leftHandDamageColliders)
            {
                collider.DisableDamageCollider();
            }
        }

        public void EnableArmsStomp()
        {
            armsSmashStomp.gameObject.SetActive(true);
        }

        public void EnableRightFootStomp()
        {
            rightFootSmashStomp.gameObject.SetActive(true);
        }

        public void EnableLeftFootStomp()
        {
            leftFootSmashStomp.gameObject.SetActive(true);
        }

        public void DisableArmsStomp()
        {
            armsSmashStomp.gameObject.SetActive(false);
        }

        public void DisableRightFootStomp()
        {
            rightFootSmashStomp.gameObject.SetActive(false);
        }

        public void DisableLeftFootStomp()
        {
            leftFootSmashStomp.gameObject.SetActive(false);
        }

        public override void PivotTowardsTarget(AICharacterManager aiCharacter)
        {
            if (aiCharacter.isInteracting)
                return;
          
            if(viewableAngle >= 61 && viewableAngle <= 110)
            {
                aiCharacter.characterAnimationManager.PlayTargetAnimation("TurnRight_90", true);
            }
            else if(viewableAngle <= -61 && viewableAngle >= 180)
            {
                aiCharacter.characterAnimationManager.PlayTargetAnimation("TurnLeft_90", true);
            }

            Vector3 direction = aiCharacter.aiCharacterCombatManager.currentTarget.transform.position - aiCharacter.transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = aiCharacter.transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            aiCharacter.transform.rotation = Quaternion.Slerp(aiCharacter.transform.rotation, targetRotation, 7.5f * Time.deltaTime);
        }

    }

}