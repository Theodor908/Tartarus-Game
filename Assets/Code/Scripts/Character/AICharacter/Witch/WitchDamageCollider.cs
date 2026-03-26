using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class WitchDamageCollider : DamageCollider
    {
        public AICharacterManager aiCharacterCausingDamage;

        protected override void Awake()
        {
            base.Awake();
            damageCollider = GetComponent<Collider>();
            aiCharacterCausingDamage = GetComponentInParent<AICharacterManager>();

        }

        protected override void DamageTarget(CharacterManager damageTarget)
        {
            if (damageTarget == aiCharacterCausingDamage)
            {
                return;
            }

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
            damageEffect.angleOfDamage = 90f;
            // Vector3.SignedAngle(aiCharacterCausingDamage.transform.forward, damageTarget.transform.forward, Vector3.up);

            damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);

        }
    }
}
