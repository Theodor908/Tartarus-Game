using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class CursedAbominationDamageCollider : DamageCollider
    {
        [SerializeField] AICharacterBossManager aiCharacterBossCausingDamage;

        protected override void Awake()
        {
            base.Awake();
            damageCollider = GetComponent<Collider>();
            aiCharacterBossCausingDamage = GetComponentInParent<AICharacterBossManager>();

        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
        }

        protected override void CheckForBlock(CharacterManager damageTarget)
        {
            base.CheckForBlock(damageTarget);
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
            damageEffect.angleOfDamage = Vector3.SignedAngle(aiCharacterBossCausingDamage.transform.forward, damageTarget.transform.forward, Vector3.up);

            damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);

        }
    }
}
