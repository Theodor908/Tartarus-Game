using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public class AIWitchCharacterCombatManager : AICharacterCombatManager
    {

        [Header("Damage Collider")]
        [SerializeField] WitchDamageCollider rightHandSwordCollider;

        [Header("Magic Items")]
        [SerializeField] GameObject magicAttack01;

        [Header("Damage")]
        [SerializeField] int baseDamage = 15;
        [SerializeField] int basePoiseDamage = 7;

        protected override void Awake()
        {
            base.Awake();

        }

        public void SetAttackDamage01()
        {
            rightHandSwordCollider.physicalDamage = baseDamage;
            rightHandSwordCollider.poiseDamage = basePoiseDamage;
        }

        public void SpawnMagicAttack01()
        {
            GameObject mA01 = Instantiate(magicAttack01, leftHandBowPosition.transform.position, Quaternion.identity);
            AIDevilDamageCollider dmgCollider = mA01.GetComponent<AIDevilDamageCollider>();
            if(dmgCollider != null)
            {
                dmgCollider.aiCharacterCausingDamage = characterManager as AICharacterManager;
            }
        }

        public void EnableWitchSwordDamageCollider()
        {
            rightHandSwordCollider.EnableDamageCollider();
        }

        public void DisableWitchSwordDamageCollider()
        {
            rightHandSwordCollider.DisableDamageCollider();
        }

        public void EnableDevilSwordDamageCollider()
        {
            rightHandSwordCollider.EnableDamageCollider();
        }

        public void DisableDevilSwordDamageCollider()
        {
            rightHandSwordCollider.DisableDamageCollider();
        }
    }
}
