using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Tartarus
{
    public class StompDamageCollider : DamageCollider
    {
        [SerializeField] AICharacterManager aiCharacterCausingDamage;
        [SerializeField] Transform stompTransform;

        [SerializeField] GameObject stompEffect;

        [SerializeField] float stompAOERadius = 2;
        [SerializeField] float stompDamage = 20;
        [SerializeField] float stompPoiseDamage = 20;

        protected override void Awake()
        {
            base.Awake();
            aiCharacterCausingDamage = GetComponentInParent<AICharacterManager>();

        }

        private void OnEnable()
        {
            DamageEnemies();
        }

        private void OnDisable()
        {
            charactersDamaged.Clear();
        }

        private void DamageEnemies()
        {

            if (stompEffect != null)
            {
                Instantiate(stompEffect, stompTransform.position, stompTransform.rotation);
            }

            Collider[] colliders = Physics.OverlapSphere(stompTransform.position, stompAOERadius, WorldUtilityManager.instance.GetCharacterLayers());

            foreach (Collider collider in colliders)
            {
                // Check for blocking

                CharacterManager damageTarget = collider.GetComponentInParent<CharacterManager>();

                if (damageTarget != null)
                {

                    if(damageTarget == aiCharacterCausingDamage)
                    {
                        continue;
                    }

                    if (charactersDamaged.Contains(damageTarget))
                        continue;

                    charactersDamaged.Add(damageTarget);

                    TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);
                    damageEffect.physicalDamage = stompDamage;
                    damageEffect.poiseDamage = stompPoiseDamage;

                    damageTarget.characterEffectsManager.ProcessInstantEffect(damageEffect);

                }

            }

        }

    }

}