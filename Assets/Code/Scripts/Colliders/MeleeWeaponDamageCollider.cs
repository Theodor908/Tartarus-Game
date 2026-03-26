using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class MeleeWeaponDamageCollider : DamageCollider
    {

        [Header("Attacking character")]
        public CharacterManager characterCausingDamage;

        [Header("Weapon Attack Modifiers")]
        [Header("Light Attack Modifiers")]
        public float light_Attack_01_Modifier;
        public float light_Attack_02_Modifier;
        public float light_Attack_03_Modifier;
        public float light_Attack_04_Modifier;
        [Header("Heavy Attack Modifiers")]
        public float heavy_Attack_01_Modifier;
        public float heavy_Attack_02_Modifier;
        public float heavy_Attack_03_Modifier;
        [Header("Charged Attack Modifiers")]
        public float charged_Heavy_Attack_01_Modifier;
        public float charged_Heavy_Attack_02_Modifier;
        [Header("Running Attack Modifiers")]
        public float running_Attack_01_Modifier;
        [Header("Rolling Attack Modifiers")]
        public float rolling_Attack_01_Modifier;
        [Header("Backstep Attack Modifiers")]
        public float backstep_Attack_01_Modifier;

        protected override void Awake()
        {
            base.Awake();

            if(damageCollider == null)
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

                if(damageTarget.isInvulnerable)
                {
                    return;
                }

                // BLocking

                // Damage the target

                //Debug.Log(other);

                DamageTarget(damageTarget);

            }
        }

        protected override void CheckForBlock(CharacterManager damageTarget)
        {
            if (charactersDamaged.Contains(damageTarget))
            {
                return;
            }

            Vector3 directionFromAttackToDamageTarget = characterCausingDamage.transform.position - damageTarget.transform.position;
            float dotValueFromAttackToDamageTarget = Vector3.Dot(directionFromAttackToDamageTarget.normalized, damageTarget.transform.forward);
            // 1. Determine the direction of the attack (check for correct block direction)
            if (damageTarget.isBlocking && dotValueFromAttackToDamageTarget > 0.3f)
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

            // 3. Apply blocked character damage to target

        }

        protected override void DamageTarget(CharacterManager damageTarget)
        {



            if (charactersDamaged.Contains(damageTarget))
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
            damageEffect.angleOfDamage = Vector3.SignedAngle(characterCausingDamage.transform.forward, damageTarget.transform.forward, Vector3.up);

            switch (characterCausingDamage.characterCombatManager.currentAttackType)
            {
                case AttackType.LightAttack01:
                    ApplyAttackDamageModifiers(light_Attack_01_Modifier, damageEffect);
                    break;
                case AttackType.LightAttack02:
                    ApplyAttackDamageModifiers(light_Attack_02_Modifier, damageEffect);
                    break;
                case AttackType.LightAttack03:
                    ApplyAttackDamageModifiers(light_Attack_03_Modifier, damageEffect);
                    break;
                case AttackType.LightAttack04:
                    ApplyAttackDamageModifiers(light_Attack_04_Modifier, damageEffect);
                    break;
                case AttackType.HeavyAttack01:
                    ApplyAttackDamageModifiers(heavy_Attack_01_Modifier, damageEffect);
                    break;
                case AttackType.HeavyAttack02:
                    ApplyAttackDamageModifiers(heavy_Attack_02_Modifier, damageEffect);
                    break;
                case AttackType.HeavyAttack03:
                    ApplyAttackDamageModifiers(heavy_Attack_03_Modifier, damageEffect);
                    break;
                case AttackType.ChargedAttack01:
                    ApplyAttackDamageModifiers(charged_Heavy_Attack_01_Modifier, damageEffect);
                    break;
                case AttackType.ChargedAttack02:
                    ApplyAttackDamageModifiers(charged_Heavy_Attack_02_Modifier, damageEffect);
                    break;
                case AttackType.RunningAttack01:
                    ApplyAttackDamageModifiers(running_Attack_01_Modifier, damageEffect);
                    break;
                case AttackType.RollingAttack01:
                    ApplyAttackDamageModifiers(rolling_Attack_01_Modifier, damageEffect);
                    break;
                case AttackType.BackstepAttack01:
                    ApplyAttackDamageModifiers(backstep_Attack_01_Modifier, damageEffect);
                    break;
                default:
                    break;
            }

            damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);

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