using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class DamageCollider : MonoBehaviour
    {
        [Header("Colliders")]
        public Collider damageCollider;

        [Header ("Damage")]
        public float physicalDamage = 0; // Standard, strike, slash, pierce
        public float magicalDamage = 0;
        public float fireDamage = 0;
        public float frostDamage = 0;   
        public float poiseDamage = 0;

        [Header ("Contact points")]
        protected Vector3 contactPoint;

        [Header ("Characters damaged")]
        protected List<CharacterManager> charactersDamaged = new List<CharacterManager>();

        protected virtual void Awake()
        {
            
        }

        protected virtual void OnTriggerEnter(Collider other)
        {

            CharacterManager damageTarget = other.GetComponentInParent<CharacterManager>();

            if(damageTarget != null)
            {
                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

                // Check if we can damage the target

                // Invluernable

                // BLocking

                // Damage the target

                CheckForBlock(damageTarget);

                DamageTarget(damageTarget);

            }
        }

        protected virtual void CheckForBlock(CharacterManager damageTarget)
        {
            if(charactersDamaged.Contains(damageTarget))
            {
                return;
            }


            Vector3 directionFromAttackToDamageTarget = transform.position - damageTarget.transform.position;
            float dotValueFromAttackToDamageTarget = Vector3.Dot(directionFromAttackToDamageTarget.normalized, damageTarget.transform.forward);
            // 1. Determine the direction of the attack (check for correct block direction)

            if(damageTarget.isBlocking && dotValueFromAttackToDamageTarget > 0.3f)
            {
                // 2. Check if the target is blocking
                charactersDamaged.Add(damageTarget);
                TakeBlockedDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeBlockedDamageEffect);

                damageEffect.physicalDamage = physicalDamage;
                damageEffect.magicalDamage = magicalDamage;
                damageEffect.fireDamage = fireDamage;
                damageEffect.frostDamage = frostDamage;
                damageEffect.poiseDamage = poiseDamage;
                damageEffect.contactPoint = contactPoint;

                damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);

            }

        }

        protected virtual void DamageTarget(CharacterManager damageTarget)
        {
            // Instantiate damage effect
            // Must check if multiple colliders are hit to not instantiate multiple effects

            if(charactersDamaged.Contains(damageTarget))
            {
                return;
            }

            charactersDamaged.Add(damageTarget);

            TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
            damageEffect.physicalDamage = physicalDamage;
            damageEffect.magicalDamage = magicalDamage;
            damageEffect.fireDamage = fireDamage;
            damageEffect.frostDamage = frostDamage;
            damageEffect.poiseDamage = poiseDamage;
            damageEffect.contactPoint = contactPoint;

            damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);
        }   

        public virtual void EnableDamageCollider()
        {
            damageCollider.enabled = true;
        }

        public virtual void DisableDamageCollider()
        {
            damageCollider.enabled = false;
            charactersDamaged.Clear();
        }

    }
}