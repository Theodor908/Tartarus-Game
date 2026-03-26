using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Tartarus
{
    public class AIDevilCombatManager : AICharacterCombatManager
    {

        AICharacterManager aiCharacterManager;

        [Header("Damage Collider")]
        [SerializeField] AIDevilDamageCollider rightHandSwordCollider;

        [Header ("Magic Items")]
        [SerializeField] GameObject magicAttack01;
        [SerializeField] GameObject magicAttack02;
        [SerializeField] GameObject magicAttack03;

        [Header("Damage")]
        [SerializeField] int baseDamage = 15;
        [SerializeField] int basePoiseDamage = 7;
        [SerializeField] float attack01DamageModifier = 1.0f;
        [SerializeField] float attack02DamageModifier = 1.4f;
        [SerializeField] float attack03DamageModifier = 1.8f;
        [SerializeField] float attack04DamageModifier = 2.2f;
        [SerializeField] float attack05DamageModifier = 2.6f;
        [SerializeField] float attack06DamageModifier = 3.0f;

        protected override void Awake()
        {
            base.Awake();
            aiCharacterManager = characterManager as AICharacterManager;
        }

        public void SetAttack01Damage()
        {
            rightHandSwordCollider.physicalDamage = baseDamage * attack01DamageModifier;
            rightHandSwordCollider.poiseDamage = basePoiseDamage * attack01DamageModifier;
        }

        public void SetAttack02Damage()
        {
            rightHandSwordCollider.physicalDamage = baseDamage * attack02DamageModifier;
            rightHandSwordCollider.poiseDamage = basePoiseDamage * attack02DamageModifier;
        }

        public void SetAttack03Damage()
        {
            rightHandSwordCollider.physicalDamage = baseDamage * attack03DamageModifier;
            rightHandSwordCollider.poiseDamage = basePoiseDamage * attack03DamageModifier;
        }

        public void SetAttack04Damage()
        {
            rightHandSwordCollider.physicalDamage = baseDamage * attack04DamageModifier;
            rightHandSwordCollider.poiseDamage = basePoiseDamage * attack04DamageModifier;
        }

        public void SetAttack05Damage()
        {
            rightHandSwordCollider.physicalDamage = baseDamage * attack05DamageModifier;
            rightHandSwordCollider.poiseDamage = basePoiseDamage * attack05DamageModifier;
        }

        public void SetAttack06Damage()
        {
            rightHandSwordCollider.physicalDamage = baseDamage * attack06DamageModifier;
            rightHandSwordCollider.poiseDamage = basePoiseDamage * attack06DamageModifier;
        }

        public void SpawnMagicAttack01()
        {
            GameObject mA01 = Instantiate(magicAttack01, rightHandSwordCollider.transform.position, Quaternion.identity);
            mA01.GetComponent<AIDevilDamageCollider>().aiCharacterCausingDamage = aiCharacterManager;
        }

        public void SpawnMagicAttack02()
        {
            GameObject mA02 = Instantiate(magicAttack02, rightHandSwordCollider.transform.position, Quaternion.identity);
            mA02.GetComponent<AIDevilDamageCollider>().aiCharacterCausingDamage = aiCharacterManager;
        }

        public void SpawnMagicAttack03()
        {
            GameObject mA03 = Instantiate(magicAttack03, rightHandSwordCollider.transform.position, Quaternion.identity);
            mA03.GetComponent<AIDevilDamageCollider>().aiCharacterCausingDamage = aiCharacterManager;
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
