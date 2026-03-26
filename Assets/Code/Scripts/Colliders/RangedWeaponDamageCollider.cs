using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class RangedWeaponDamageCollider : DamageCollider
    {

        [Header("Attacking character")]
        public CharacterManager characterCausingDamage;

        [Header("Projectile Attack Modifiers")]
        public float projectile_Attack_Modifier;
      

        protected override void Awake()
        {
            base.Awake();

            if (damageCollider == null)
            {
                damageCollider = GetComponent<Collider>();
            }

            damageCollider.enabled = false;

        }

        protected override void OnTriggerEnter(Collider other)
        {
            CharacterManager damageTarget = other.GetComponentInParent<CharacterManager>();

            if (damageTarget != null)
            {
                // Dont damage ourselves
                if (damageTarget == characterCausingDamage)
                    return;

                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

                // Check if we can damage the target

                if (damageTarget.isInvulnerable)
                {
                    return;
                }

                // BLocking

                // Damage the target

                //Debug.Log(other);

                DamageTarget(damageTarget);

            }
        }

        protected override void DamageTarget(CharacterManager damageTarget)
        {
            Debug.Log("DamageTarget");
            if (charactersDamaged.Contains(damageTarget))
            {
                return;
            }

            charactersDamaged.Add(damageTarget);

            TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
            damageEffect.physicalDamage = physicalDamage;
            damageEffect.magicalDamage = magicalDamage;
            damageEffect.fireDamage = fireDamage;
            damageEffect.poiseDamage = poiseDamage;
            damageEffect.contactPoint = contactPoint;
            damageEffect.angleOfDamage = Vector3.SignedAngle(characterCausingDamage.transform.forward, damageTarget.transform.forward, Vector3.up);

            switch (characterCausingDamage.characterCombatManager.currentAttackType)
            {
                case AttackType.LightAttack01:
                    ApplyAttackDamageModifiers(projectile_Attack_Modifier, damageEffect);
                    break;
                default:
                    break;
            }

            damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);
            DisableDamageCollider();
        }

        private void ApplyAttackDamageModifiers(float modifier, TakeDamageEffect damageEffect)
        {
            damageEffect.physicalDamage *= modifier;
            damageEffect.fireDamage *= modifier;
            damageEffect.frostDamage *= modifier;
            damageEffect.poiseDamage *= modifier;

            // Attack is fully charged heavy then reapply modifiers
        }

    }
}
