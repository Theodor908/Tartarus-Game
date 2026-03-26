using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class CharacterCombatManager : MonoBehaviour
    {

        protected CharacterManager characterManager;

        [Header ("Last Attack Animation Performed")]
        public string lastAttackAnimationPerformed;

        [Header("Attack Target")]
        public CharacterManager currentTarget;

        [Header("Attack Type")]
        public AttackType currentAttackType;

        [Header ("Lock on transform")]
        public Transform lockOnTransform;

        [Header ("Projectile Source")]
        public Transform projectileSource;

        [Header ("Bow Positions Snapping")]
        public Transform leftHandBowPosition;
        public Transform rightHandBowPosition;

        [Header ("Attack Flags")]
        public bool canPerformRollingAttack = false;
        public bool canPerformBackstepAttack = false;
        public bool isThrowing = false;
        public bool canBlock = false;
        public bool canThrow = false;
        public float chargeTime = 0f;

        protected virtual void Awake()
        {
            characterManager = GetComponent<CharacterManager>();
        }

        public virtual void SetLockOnTarget(CharacterManager newTarget)
        {
            if(newTarget != null)
            {
                currentTarget = newTarget;
            }
        }

        public void EnableIsInvlunerable()
        {
            characterManager.isInvulnerable = true;
        }

        public void DisableIsInvulnerable()
        {
            characterManager.isInvulnerable = false;
        }

        public void EnableCanDoRollingAttack()
        {
            canPerformRollingAttack = true;
        }

        public void DisableCanDoRollingAttack()
        {
            canPerformRollingAttack = false;
        }

        public void EnableCanDoBackstepAttack()
        {
            canPerformBackstepAttack = true;
        }

        public void DisableCanDoBackstepAttack()
        {
            canPerformBackstepAttack = false;
        }

        public virtual void EnableCanDoCombo()
        {

        }

        public virtual void DisableCanDoCombo()
        {

        }

        public virtual void CanThrow()
        {
            
        }

        public IEnumerator DestroyAfterTime(GameObject gameObject, float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }

    }
}